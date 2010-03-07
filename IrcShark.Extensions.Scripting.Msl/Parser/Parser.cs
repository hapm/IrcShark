
using System;

namespace IrcShark.Extensions.Scripting.Msl.Parser {



public class Parser {
	public const int _EOF = 0;
	public const int _number = 1;
	public const int _word = 2;
	public const int _idcall = 3;
	public const int _varname = 4;
	public const int _sp = 5;
	public const int maxT = 10;

    const bool T = true;
    const bool x = false;
    const int minErrDist = 2;
    
    public Scanner scanner;
    public Errors  errors;

    public Token t;    // last recognized token
    public Token la;   // lookahead token
    int errDist = minErrDist;



    public Parser(Scanner scanner) {
        this.scanner = scanner;
        errors = new Errors();
    }

    void SynErr (int n) {
        if (errDist >= minErrDist) errors.SynErr(la.line, la.col, n);
        errDist = 0;
    }

    public void SemErr (string msg) {
        if (errDist >= minErrDist) errors.SemErr(t.line, t.col, msg);
        errDist = 0;
    }
    
    void Get () {
        for (;;) {
            t = la;
            la = scanner.Scan();
            if (la.kind <= maxT) { ++errDist; break; }

            la = t;
        }
    }
    
    void Expect (int n) {
        if (la.kind==n) Get(); else { SynErr(n); }
    }
    
    bool StartOf (int s) {
        return set[s, la.kind];
    }
    
    void ExpectWeak (int n, int follow) {
        if (la.kind == n) Get();
        else {
            SynErr(n);
            while (!StartOf(follow)) Get();
        }
    }


    bool WeakSeparator(int n, int syFol, int repFol) {
        int kind = la.kind;
        if (kind == n) {Get(); return true;}
        else if (StartOf(repFol)) {return false;}
        else {
            SynErr(n);
            while (!(set[syFol, kind] || set[repFol, kind] || set[0, kind])) {
                Get();
                kind = la.kind;
            }
            return StartOf(syFol);
        }
    }

    
	void Parser() {
		Expression();
	}

	void Expression() {
		if (la.kind == 2) {
			Get();
		} else if (la.kind == 3) {
			IdentifierCall();
		} else if (la.kind == 4) {
			Get();
		} else SynErr(11);
	}

	void IdentifierCall() {
		Expect(3);
		if (la.kind == 6) {
			Get();
			if (la.kind == 2 || la.kind == 3 || la.kind == 4) {
				ParameterExpressions();
				while (la.kind == 7) {
					Get();
					if (la.kind == 2 || la.kind == 3 || la.kind == 4) {
						ParameterExpressions();
					}
				}
			}
			Expect(8);
			if (la.kind == 9) {
				Get();
				Expect(2);
			}
		}
	}

	void ParameterExpressions() {
		Expression();
		while (la.kind == 5) {
			Get();
			if (la.kind == 2 || la.kind == 3 || la.kind == 4) {
				Expression();
			}
		}
	}



    public void Parse() {
        la = new Token();
        la.val = "";        
        Get();
		Parser();

    Expect(0);
    }
    
    static readonly bool[,] set = {
		{T,x,x,x, x,x,x,x, x,x,x,x}

    };
} // end Parser


public class Errors {
    public int count = 0;                                    // number of errors detected
    public System.IO.TextWriter errorStream = Console.Out;   // error messages go to this stream
    public string errMsgFormat = "-- line {0} col {1}: {2}"; // 0=line, 1=column, 2=text
  
    public void SynErr (int line, int col, int n) {
        string s;
        switch (n) {
			case 0: s = "EOF expected"; break;
			case 1: s = "number expected"; break;
			case 2: s = "word expected"; break;
			case 3: s = "idcall expected"; break;
			case 4: s = "varname expected"; break;
			case 5: s = "sp expected"; break;
			case 6: s = "\"(\" expected"; break;
			case 7: s = "\",\" expected"; break;
			case 8: s = "\")\" expected"; break;
			case 9: s = "\".\" expected"; break;
			case 10: s = "??? expected"; break;
			case 11: s = "invalid Expression"; break;

            default: s = "error " + n; break;
        }
        errorStream.WriteLine(errMsgFormat, line, col, s);
        count++;
    }

    public void SemErr (int line, int col, string s) {
        errorStream.WriteLine(errMsgFormat, line, col, s);
        count++;
    }
    
    public void SemErr (string s) {
        errorStream.WriteLine(s);
        count++;
    }
    
    public void Warning (int line, int col, string s) {
        errorStream.WriteLine(errMsgFormat, line, col, s);
    }
    
    public void Warning(string s) {
        errorStream.WriteLine(s);
    }
} // Errors


public class FatalError: Exception {
    public FatalError(string m): base(m) {}
}

}
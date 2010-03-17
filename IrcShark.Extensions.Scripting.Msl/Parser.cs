using System.Text;
using System.Collections.Generic;



using System;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace IrcShark.Extensions.Scripting.Msl {



public partial class Parser : ICodeParser {
	public const int _EOF = 0;
	public const int _number = 1;
	public const int _word = 2;
	public const int _strconcat = 3;
	public const int _idcall = 4;
	public const int _varname = 5;
	public const int _sp = 6;
	public const int _EOL = 7;
	public const int maxT = 18;

    const bool T = true;
    const bool x = false;
    const int minErrDist = 2;
    
    public Scanner scanner;
    public Errors  errors;

    public Token t;    // last recognized token
    public Token la;   // lookahead token
    int errDist = minErrDist;



    public Parser() {
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

    
	void MslParser() {
		CodeMemberMethod alias; 
		
		while (la.kind == 6 || la.kind == 7 || la.kind == 8) {
			if (la.kind == 8) {
				AliasDecl(out alias);
				script.Members.Add(alias); 
			} else if (la.kind == 6) {
				Get();
			} else {
				Get();
			}
		}
		Expect(0);
	}

	void AliasDecl(out CodeMemberMethod method) {
		method = SetupAlias();
		List<CodeStatement> stmts = new List<CodeStatement>();
		string name;
		
		Expect(8);
		Expect(6);
		if (la.kind == 9) {
			Get();
			Expect(6);
			method.Attributes = MemberAttributes.Private | MemberAttributes.Final; 
		}
		AliasName(out name);
		method.Name = "Alias" + name; 
		Expect(6);
		if (la.kind == 14) {
			CommandBlock(method.Statements);
		} else if (StartOf(1)) {
			CommandLine(method.Statements);
		} else SynErr(19);
	}

	void AliasName(out string name) {
		StringBuilder result = new StringBuilder(); 
		if (la.kind == 1) {
			Get();
			result.Append(t.val); 
		} else if (la.kind == 2) {
			Get();
			result.Append(t.val); 
		} else SynErr(20);
		while (la.kind == 1 || la.kind == 2) {
			if (la.kind == 1) {
				Get();
				result.Append(t.val); 
			} else {
				Get();
				result.Append(t.val); 
			}
		}
		name = result.ToString(); 
	}

	void CommandBlock(CodeStatementCollection result) {
		Expect(14);
		if (la.kind == 6) {
			Get();
			if (StartOf(1)) {
				CommandLine(result);
			}
		}
		while (la.kind == 7) {
			Get();
			if (la.kind == 6) {
				Get();
			}
			if (StartOf(1)) {
				CommandLine(result);
			}
		}
		Expect(15);
		if (la.kind == 6) {
			Get();
		}
		if (StartOf(1)) {
			Command(result);
		}
	}

	void CommandLine(CodeStatementCollection result) {
		Command(result);
		while (la.kind == 13) {
			Get();
			Expect(6);
			Command(result);
		}
		if (la.kind == 6) {
			Get();
		}
	}

	void Command(CodeStatementCollection stmts) {
		CodeConditionStatement ifStmt; 
		if (StartOf(2)) {
			ExpressionLine(stmts);
		} else if (la.kind == 10) {
			IfClause(out ifStmt);
			stmts.Add(ifStmt); 
		} else SynErr(21);
	}

	void ExpressionLine(CodeStatementCollection result) {
		result.Add(GetBuffer());
		string currentSpace; 
		Expression(result);
		while (StartOf(3)) {
			if (la.kind == 6) {
				Get();
				currentSpace = t.val; 
				if (la.kind == 3) {
					Get();
					Expect(6);
				} else if (StartOf(4)) {
					result.Add(AppendBuffer(new CodePrimitiveExpression(currentSpace))); 
				} else SynErr(22);
			} else {
				Expression(result);
			}
		}
		result.Add(ReleaseBuffer());
		result.Add(CallAlias());
		
	}

	void IfClause(out CodeConditionStatement ifStmt) {
		ifStmt = SetupIfStatement(); 
		Expect(10);
		Expect(6);
		Expect(11);
		Expect(12);
		CommandBlock(ifStmt.TrueStatements);
		if (la.kind == 6) {
			Get();
		}
	}

	void Expression(CodeStatementCollection result) {
		if (la.kind == 1 || la.kind == 2 || la.kind == 16) {
			StaticExpression(result);
		} else if (la.kind == 4) {
			IdentifierCall(result);
		} else if (la.kind == 5) {
			Get();
			string temp = "v_" + t.val.Substring(1);
			result.Add(AppendBuffer(new CodeVariableReferenceExpression(temp))); 
			
		} else SynErr(23);
	}

	void StaticExpression(CodeStatementCollection result) {
		StringBuilder data = new StringBuilder(); 
		if (la.kind == 2) {
			Get();
			data.Append(t.val); 
		} else if (la.kind == 1) {
			Get();
			data.Append(t.val); 
		} else if (la.kind == 16) {
			Get();
			data.Append(t.val); 
		} else SynErr(24);
		while (StartOf(5)) {
			if (la.kind == 2) {
				Get();
				data.Append(t.val); 
			} else if (la.kind == 6) {
				Get();
				data.Append(t.val); 
			} else if (la.kind == 1) {
				Get();
				data.Append(t.val); 
			} else {
				Get();
				data.Append(t.val); 
			}
		}
		result.Add(AppendBuffer(new CodePrimitiveExpression(data.ToString()))); 
	}

	void IdentifierCall(CodeStatementCollection result) {
		string idname;
		string prop = null;
		int paramCount = 0;
		
		Expect(4);
		idname = t.val; 
		if (la.kind == 11) {
			Get();
			if (StartOf(2)) {
				result.Add(GetBuffer()); 
				ExpressionParameter(result);
				result.Add(ReleaseBuffer());
				result.Add(PushText());
				paramCount++;
				
				while (la.kind == 17) {
					Get();
					if (la.kind == 6) {
						Get();
					}
					if (StartOf(2)) {
						result.Add(GetBuffer()); 
						ExpressionParameter(result);
						result.Add(ReleaseBuffer());
						result.Add(PushText());
						paramCount++;
						
					}
				}
			}
			Expect(12);
			if (la.kind == 16) {
				Get();
				Expect(2);
				prop = t.val; 
			}
		}
		CallIdentifier(result, idname, paramCount, prop); 
	}

	void ExpressionParameter(CodeStatementCollection result) {
		string currentSpace; 
		Expression(result);
		while (StartOf(3)) {
			if (StartOf(2)) {
				Expression(result);
			} else {
				Get();
				currentSpace = t.val; 
				if (la.kind == 3) {
					Get();
					Expect(6);
				} else if (StartOf(6)) {
					result.Add(AppendBuffer(new CodePrimitiveExpression(currentSpace))); 
				} else SynErr(25);
			}
		}
	}



    public CodeCompileUnit Parse(TextReader reader) {
        scanner = new Scanner(reader);
        SetupScript();
        la = new Token();
        la.val = "";        
        Get();
		MslParser();

        Expect(0);
        return dom;
    }
    
    static readonly bool[,] set = {
		{T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x},
		{x,T,T,x, T,T,x,x, x,x,T,x, x,x,x,x, T,x,x,x},
		{x,T,T,x, T,T,x,x, x,x,x,x, x,x,x,x, T,x,x,x},
		{x,T,T,x, T,T,T,x, x,x,x,x, x,x,x,x, T,x,x,x},
		{T,T,T,x, T,T,T,T, T,x,x,x, x,T,x,T, T,x,x,x},
		{x,T,T,x, x,x,T,x, x,x,x,x, x,x,x,x, T,x,x,x},
		{x,T,T,x, T,T,T,x, x,x,x,x, T,x,x,x, T,T,x,x}

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
			case 3: s = "strconcat expected"; break;
			case 4: s = "idcall expected"; break;
			case 5: s = "varname expected"; break;
			case 6: s = "sp expected"; break;
			case 7: s = "EOL expected"; break;
			case 8: s = "\"alias\" expected"; break;
			case 9: s = "\"-l\" expected"; break;
			case 10: s = "\"if\" expected"; break;
			case 11: s = "\"(\" expected"; break;
			case 12: s = "\")\" expected"; break;
			case 13: s = "\"|\" expected"; break;
			case 14: s = "\"{\" expected"; break;
			case 15: s = "\"}\" expected"; break;
			case 16: s = "\".\" expected"; break;
			case 17: s = "\",\" expected"; break;
			case 18: s = "??? expected"; break;
			case 19: s = "invalid AliasDecl"; break;
			case 20: s = "invalid AliasName"; break;
			case 21: s = "invalid Command"; break;
			case 22: s = "invalid ExpressionLine"; break;
			case 23: s = "invalid Expression"; break;
			case 24: s = "invalid StaticExpression"; break;
			case 25: s = "invalid ExpressionParameter"; break;

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

}
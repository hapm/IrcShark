using System.Text;
using System.Collections.Generic;



using System;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace IrcShark.Extensions.Scripting.Msl {



public partial class Parser : ICodeParser {
	public const int _EOF = 0;
	public const int _multiop = 1;
	public const int _singleop = 2;
	public const int _number = 3;
	public const int _word = 4;
	public const int _strconcat = 5;
	public const int _idcall = 6;
	public const int _varname = 7;
	public const int _sp = 8;
	public const int _EOL = 9;
	public const int maxT = 20;

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
		
		while (la.kind == 8 || la.kind == 9 || la.kind == 10) {
			if (la.kind == 10) {
				AliasDecl(out alias);
				script.Members.Add(alias); 
			} else if (la.kind == 8) {
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
		
		Expect(10);
		Expect(8);
		if (la.kind == 11) {
			Get();
			Expect(8);
			method.Attributes = MemberAttributes.Private | MemberAttributes.Final; 
		}
		AliasName(out name);
		method.Name = "Alias" + name; 
		Expect(8);
		if (la.kind == 16) {
			CommandBlock(method.Statements);
		} else if (StartOf(1)) {
			CommandLine(method.Statements);
		} else SynErr(21);
	}

	void AliasName(out string name) {
		StringBuilder result = new StringBuilder(); 
		if (la.kind == 3) {
			Get();
			result.Append(t.val); 
		} else if (la.kind == 4) {
			Get();
			result.Append(t.val); 
		} else SynErr(22);
		while (la.kind == 3 || la.kind == 4) {
			if (la.kind == 3) {
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
		Expect(16);
		if (la.kind == 8) {
			Get();
			if (StartOf(1)) {
				CommandLine(result);
			}
		}
		while (la.kind == 9) {
			Get();
			if (la.kind == 8) {
				Get();
			}
			if (StartOf(1)) {
				CommandLine(result);
			}
		}
		Expect(17);
		if (la.kind == 8) {
			Get();
		}
		if (StartOf(1)) {
			Command(result);
		}
	}

	void CommandLine(CodeStatementCollection result) {
		Command(result);
		while (la.kind == 15) {
			Get();
			Expect(8);
			Command(result);
		}
		if (la.kind == 8) {
			Get();
		}
	}

	void Command(CodeStatementCollection stmts) {
		CodeStatement temp = null; 
		if (StartOf(2)) {
			ExpressionLine(out temp);
		} else if (la.kind == 12) {
			IfClause(out temp);
		} else SynErr(23);
		stmts.Add(temp); 
	}

	void ExpressionLine(out CodeStatement result) {
		string currentSpace;
		CodeExpression line;
		CodeExpression temp; 
		Expression(out line);
		while (StartOf(3)) {
			if (StartOf(2)) {
				Expression(out temp);
				line = new CodeBinaryOperatorExpression(line, CodeBinaryOperatorType.Add, temp); 
			} else {
				Get();
				currentSpace = t.val; 
				if (la.kind == 5) {
					Get();
					Expect(8);
				} else if (StartOf(4)) {
					line = new CodeBinaryOperatorExpression(line, CodeBinaryOperatorType.Add, new CodePrimitiveExpression(currentSpace)); 
				} else SynErr(24);
			}
		}
		result = CallAlias(line); 
	}

	void IfClause(out CodeStatement result) {
		CodeConditionStatement ifStmt = SetupIfStatement(); 
		CodeExpression boolExp;
		
		Expect(12);
		Expect(8);
		Expect(13);
		BooleanExpression(out boolExp);
		Expect(14);
		ifStmt.Condition = boolExp; 
		Expect(8);
		if (la.kind == 16) {
			CommandBlock(ifStmt.TrueStatements);
		} else if (StartOf(1)) {
			CommandLine(ifStmt.TrueStatements);
		} else SynErr(25);
		if (la.kind == 8) {
			Get();
		}
		result = ifStmt; 
	}

	void BooleanExpression(out CodeExpression boolExp) {
		string op = null;
		CodeExpression leftExpression = null;
		CodeExpression rightExpression = null;
		boolExp = null;
		
		if (la.kind == 13) {
			Get();
			if (la.kind == 8) {
				Get();
			}
			BooleanExpression(out boolExp);
			if (la.kind == 8) {
				Get();
			}
			Expect(14);
		} else if (StartOf(2)) {
			BooleanExpressionParameter(out leftExpression);
			boolExp = BooleanEvaluation(leftExpression); 
			if (la.kind == 1 || la.kind == 2) {
				if (la.kind == 1 || la.kind == 8 || la.kind == 14) {
					while (la.kind == 1) {
						Get();
						op = t.val; 
						BooleanExpressionParameter(out rightExpression);
						boolExp = BooleanEvaluation(leftExpression, t.val, rightExpression); 
						leftExpression = boolExp;
						
					}
				} else {
					Get();
					boolExp = BooleanEvaluation(leftExpression, t.val); 
				}
			}
		} else SynErr(26);
	}

	void BooleanExpressionParameter(out CodeExpression result) {
		string currentSpace;
		CodeExpression temp; 
		Expression(out result);
		while (StartOf(5)) {
			if (StartOf(2)) {
				Expression(out temp);
				result = new CodeBinaryOperatorExpression(result, CodeBinaryOperatorType.Add, temp); 
			} else if (la.kind == 19) {
				Get();
				result = new CodeBinaryOperatorExpression(result, CodeBinaryOperatorType.Add, new CodePrimitiveExpression(t.val)); 
			} else {
				Get();
				currentSpace = t.val; 
				if (la.kind == 5) {
					Get();
					Expect(8);
				} else if (StartOf(6)) {
					result = new CodeBinaryOperatorExpression(result, CodeBinaryOperatorType.Add, new CodePrimitiveExpression(currentSpace)); 
				} else SynErr(27);
			}
		}
	}

	void Expression(out CodeExpression result) {
		result = null; 
		if (la.kind == 3 || la.kind == 4 || la.kind == 18) {
			StaticExpression(out result);
		} else if (la.kind == 6) {
			IdentifierCall(out result);
		} else if (la.kind == 7) {
			Get();
			string temp = "v_" + t.val.Substring(1);
			result = new CodeVariableReferenceExpression(temp); 
			
		} else SynErr(28);
	}

	void StaticExpression(out CodeExpression result) {
		StringBuilder data = new StringBuilder(); 
		if (la.kind == 4) {
			Get();
			data.Append(t.val); 
		} else if (la.kind == 3) {
			Get();
			data.Append(t.val); 
		} else if (la.kind == 18) {
			Get();
			data.Append(t.val); 
		} else SynErr(29);
		while (StartOf(7)) {
			if (la.kind == 4) {
				Get();
				data.Append(t.val); 
			} else if (la.kind == 8) {
				Get();
				data.Append(t.val); 
			} else if (la.kind == 3) {
				Get();
				data.Append(t.val); 
			} else {
				Get();
				data.Append(t.val); 
			}
		}
		result = new CodePrimitiveExpression(data.ToString()); 
	}

	void IdentifierCall(out CodeExpression result) {
		string idname;
		string prop = null;
		List<CodeExpression> parameters = new List<CodeExpression>();
		CodeExpression temp;
		
		Expect(6);
		idname = t.val; 
		if (la.kind == 13) {
			Get();
			if (StartOf(2)) {
				ExpressionParameter(out temp);
				parameters.Add(temp); 
				while (la.kind == 19) {
					Get();
					if (la.kind == 8) {
						Get();
					}
					if (StartOf(2)) {
						ExpressionParameter(out temp);
						parameters.Add(temp); 
					}
				}
			}
			Expect(14);
			if (la.kind == 18) {
				Get();
				Expect(4);
				prop = t.val; 
			}
		}
		result = CallIdentifier(idname, parameters.ToArray(), prop); 
	}

	void ExpressionParameter(out CodeExpression result) {
		string currentSpace;
		CodeExpression temp; 
		Expression(out result);
		while (StartOf(3)) {
			if (StartOf(2)) {
				Expression(out temp);
				result = new CodeBinaryOperatorExpression(result, CodeBinaryOperatorType.Add, temp); 
			} else {
				Get();
				currentSpace = t.val; 
				if (la.kind == 5) {
					Get();
					Expect(8);
				} else if (StartOf(8)) {
					result = new CodeBinaryOperatorExpression(result, CodeBinaryOperatorType.Add, new CodePrimitiveExpression(currentSpace)); 
				} else SynErr(30);
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
		{T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x},
		{x,x,x,T, T,x,T,T, x,x,x,x, T,x,x,x, x,x,T,x, x,x},
		{x,x,x,T, T,x,T,T, x,x,x,x, x,x,x,x, x,x,T,x, x,x},
		{x,x,x,T, T,x,T,T, T,x,x,x, x,x,x,x, x,x,T,x, x,x},
		{T,x,x,T, T,x,T,T, T,T,T,x, x,x,x,T, x,T,T,x, x,x},
		{x,x,x,T, T,x,T,T, T,x,x,x, x,x,x,x, x,x,T,T, x,x},
		{x,T,T,T, T,x,T,T, T,x,x,x, x,x,T,x, x,x,T,T, x,x},
		{x,x,x,T, T,x,x,x, T,x,x,x, x,x,x,x, x,x,T,x, x,x},
		{x,x,x,T, T,x,T,T, T,x,x,x, x,x,T,x, x,x,T,T, x,x}

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
			case 1: s = "multiop expected"; break;
			case 2: s = "singleop expected"; break;
			case 3: s = "number expected"; break;
			case 4: s = "word expected"; break;
			case 5: s = "strconcat expected"; break;
			case 6: s = "idcall expected"; break;
			case 7: s = "varname expected"; break;
			case 8: s = "sp expected"; break;
			case 9: s = "EOL expected"; break;
			case 10: s = "\"alias\" expected"; break;
			case 11: s = "\"-l\" expected"; break;
			case 12: s = "\"if\" expected"; break;
			case 13: s = "\"(\" expected"; break;
			case 14: s = "\")\" expected"; break;
			case 15: s = "\"|\" expected"; break;
			case 16: s = "\"{\" expected"; break;
			case 17: s = "\"}\" expected"; break;
			case 18: s = "\".\" expected"; break;
			case 19: s = "\",\" expected"; break;
			case 20: s = "??? expected"; break;
			case 21: s = "invalid AliasDecl"; break;
			case 22: s = "invalid AliasName"; break;
			case 23: s = "invalid Command"; break;
			case 24: s = "invalid ExpressionLine"; break;
			case 25: s = "invalid IfClause"; break;
			case 26: s = "invalid BooleanExpression"; break;
			case 27: s = "invalid BooleanExpressionParameter"; break;
			case 28: s = "invalid Expression"; break;
			case 29: s = "invalid StaticExpression"; break;
			case 30: s = "invalid ExpressionParameter"; break;

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
using System.Text;
using System.Collections.Generic;



using System;
using System.IO;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace IrcShark.Extensions.Scripting.Msl {



public partial class Parser : ICodeParser {
	public const int _EOF = 0;
	public const int _assign = 1;
	public const int _multiop = 2;
	public const int _singleop = 3;
	public const int _number = 4;
	public const int _word = 5;
	public const int _strconcat = 6;
	public const int _idcall = 7;
	public const int _varname = 8;
	public const int _sp = 9;
	public const int _else = 10;
	public const int _elseif = 11;
	public const int _EOL = 12;
	public const int maxT = 26;

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
		while (la.kind == 9 || la.kind == 12 || la.kind == 13) {
			if (la.kind == 13) {
				AliasDecl(out alias);
				script.Members.Add(alias); 
			} else if (la.kind == 9) {
				Get();
			} else {
				Get();
			}
		}
		Expect(0);
	}

	void AliasDecl(out CodeMemberMethod method) {
		method = SetupAlias();
		ParserState state = new ParserState();
		List<CodeStatement> stmts = new List<CodeStatement>();
		List<string> localVariables = new List<string>();
		string name;
		
		Expect(13);
		Expect(9);
		if (la.kind == 14) {
			Get();
			Expect(9);
			method.Attributes = MemberAttributes.Private | MemberAttributes.Final; 
		}
		AliasName(out name);
		method.Name = "Alias" + name; 
		Expect(9);
		if (la.kind == 23) {
			CommandBlock(method.Statements, state);
		} else if (StartOf(1)) {
			CommandLine(method.Statements, state);
		} else SynErr(27);
	}

	void AliasName(out string name) {
		StringBuilder result = new StringBuilder(); 
		if (la.kind == 4) {
			Get();
			result.Append(t.val); 
		} else if (la.kind == 5) {
			Get();
			result.Append(t.val); 
		} else SynErr(28);
		while (la.kind == 4 || la.kind == 5) {
			if (la.kind == 4) {
				Get();
				result.Append(t.val); 
			} else {
				Get();
				result.Append(t.val); 
			}
		}
		name = result.ToString(); 
	}

	void CommandBlock(CodeStatementCollection result, ParserState state) {
		Expect(23);
		if (la.kind == 9) {
			Get();
			if (StartOf(1)) {
				CommandLine(result, state);
			}
		}
		while (la.kind == 12) {
			Get();
			if (la.kind == 9) {
				Get();
			}
			if (StartOf(1)) {
				CommandLine(result, state);
			}
		}
		Expect(24);
		if (la.kind == 9) {
			Get();
		}
		if (StartOf(1)) {
			Command(result, state);
		}
	}

	void CommandLine(CodeStatementCollection result, ParserState state) {
		Command(result, state);
		while (la.kind == 22) {
			Get();
			Expect(9);
			Command(result, state);
		}
		if (la.kind == 9) {
			Get();
		}
	}

	void Command(CodeStatementCollection stmts, ParserState state) {
		CodeStatement temp = null; 
		if (IsAssignment()) {
			VarAssignment(out temp, state);
		} else if (StartOf(2)) {
			ExpressionLine(out temp, state);
		} else if (la.kind == 21) {
			IfClause(out temp, state);
		} else if (la.kind == 18) {
			WhileClause(out temp, state);
		} else if (la.kind == 15) {
			LocalVarDecl(stmts, state);
		} else SynErr(29);
		if (temp != null) 
		{
		    stmts.Add(temp);
		}
		
	}

	void VarAssignment(out CodeStatement result, ParserState state) {
		CodeExpression varValue = null; 
		CodeExpression temp = null;
		string currentSpace;
		
		Expect(8);
		string varname = t.val; 
		Expect(9);
		Expect(16);
		Expect(9);
		Expression(out varValue, state);
		while (StartOf(3)) {
			if (StartOf(2)) {
				Expression(out temp, state);
				varValue = new CodeBinaryOperatorExpression(varValue, CodeBinaryOperatorType.Add, temp); 
			} else if (la.kind == 17) {
				Get();
				varValue = new CodeBinaryOperatorExpression(varValue, CodeBinaryOperatorType.Add, new CodePrimitiveExpression(t.val)); 
			} else {
				Get();
				currentSpace = t.val; 
				if (la.kind == 6) {
					Get();
					Expect(9);
				} else if (StartOf(4)) {
					varValue = new CodeBinaryOperatorExpression(varValue, CodeBinaryOperatorType.Add, new CodePrimitiveExpression(currentSpace)); 
				} else SynErr(30);
			}
		}
		result = SetVariableValue(varname, varValue, state); 
	}

	void ExpressionLine(out CodeStatement result, ParserState state) {
		string currentSpace;
		CodeExpression line;
		CodeExpression temp; 
		Expression(out line, state);
		while (StartOf(5)) {
			if (StartOf(2)) {
				Expression(out temp, state);
				line = new CodeBinaryOperatorExpression(line, CodeBinaryOperatorType.Add, temp); 
			} else {
				Get();
				currentSpace = t.val; 
				if (la.kind == 6) {
					Get();
					Expect(9);
				} else if (StartOf(6)) {
					line = new CodeBinaryOperatorExpression(line, CodeBinaryOperatorType.Add, new CodePrimitiveExpression(currentSpace)); 
				} else SynErr(31);
			}
		}
		result = CallAlias(line); 
	}

	void IfClause(out CodeStatement result, ParserState state) {
		CodeConditionStatement ifStmt = SetupIfStatement();
		CodeConditionStatement elseifStmt;
		CodeExpression boolExp;
		result = ifStmt;
		
		Expect(21);
		Expect(9);
		Expect(19);
		BooleanExpression(out boolExp, state);
		Expect(20);
		ifStmt.Condition = boolExp; 
		Expect(9);
		if (la.kind == 23) {
			CommandBlock(ifStmt.TrueStatements, state);
		} else if (StartOf(1)) {
			CommandLine(ifStmt.TrueStatements, state);
		} else SynErr(32);
		if (la.kind == 9) {
			Get();
		}
		if (IsElseIf()) {
			Expect(12);
			if (la.kind == 9) {
				Get();
			}
			ElseIfClause(out elseifStmt, state);
			ifStmt.FalseStatements.Add(elseifStmt); 
		} else if (IsElse()) {
			Expect(12);
			if (la.kind == 9) {
				Get();
			}
			ElseClause(ifStmt.FalseStatements, state);
		} else SynErr(33);
	}

	void WhileClause(out CodeStatement result, ParserState state) {
		CodeIterationStatement whileStmt = SetupWhileStatement(); 
		CodeExpression boolExp;
		result = whileStmt;
		
		Expect(18);
		Expect(9);
		Expect(19);
		BooleanExpression(out boolExp, state);
		Expect(20);
		whileStmt.TestExpression = boolExp; 
		Expect(9);
		if (la.kind == 23) {
			CommandBlock(whileStmt.Statements, state);
		} else if (StartOf(1)) {
			CommandLine(whileStmt.Statements, state);
		} else SynErr(34);
		if (la.kind == 9) {
			Get();
		}
	}

	void LocalVarDecl(CodeStatementCollection stmts, ParserState state) {
		string varname; 
		CodeExpression expr = null;
		CodeExpression tmp = null;
		string currentSpace1 = "";
		string currentSpace2 = "";
		
		Expect(15);
		Expect(9);
		while (la.kind == 8) {
			Get();
			varname = t.val; 
			Expect(9);
			if (StartOf(7)) {
				if (la.kind == 16) {
					Get();
					Expect(9);
				}
				ExpressionParameter(out tmp, state);
				expr = tmp; 
			}
			if (la.kind == 9) {
				Get();
				currentSpace1 = t.val; 
			} else if (StartOf(8)) {
				currentSpace1 = ""; 
			} else SynErr(35);
			while (la.kind == 17) {
				Get();
				if (la.kind == 9) {
					Get();
					currentSpace1 = t.val; 
				} else if (StartOf(4)) {
					currentSpace1 = ""; 
				} else SynErr(36);
				if (la.kind != _varname) {
					ExpressionParameter(out tmp, state);
					expr = new CodeBinaryOperatorExpression(expr, CodeBinaryOperatorType.Add, new CodePrimitiveExpression(currentSpace1 + "," + currentSpace2));
					expr = new CodeBinaryOperatorExpression(expr, CodeBinaryOperatorType.Add, tmp);
					
				} else if (StartOf(8)) {
					stmts.Add(DeclareLocalVariable(varname, expr, state)); 
				} else SynErr(37);
			}
		}
	}

	void ExpressionParameter(out CodeExpression result, ParserState state) {
		string currentSpace;
		CodeExpression temp; 
		Expression(out result, state);
		while (StartOf(5)) {
			if (StartOf(2)) {
				Expression(out temp, state);
				result = new CodeBinaryOperatorExpression(result, CodeBinaryOperatorType.Add, temp); 
			} else {
				Get();
				currentSpace = t.val; 
				if (la.kind == 6) {
					Get();
					Expect(9);
				} else if (StartOf(9)) {
					result = new CodeBinaryOperatorExpression(result, CodeBinaryOperatorType.Add, new CodePrimitiveExpression(currentSpace)); 
				} else SynErr(38);
			}
		}
	}

	void BooleanExpression(out CodeExpression boolExp, ParserState state) {
		string op = null;
		CodeExpression leftExpression = null;
		CodeExpression rightExpression = null;
		boolExp = null;
		
		if (la.kind == 19) {
			Get();
			if (la.kind == 9) {
				Get();
			}
			BooleanExpression(out boolExp, state);
			if (la.kind == 9) {
				Get();
			}
			Expect(20);
		} else if (StartOf(2)) {
			BooleanExpressionParameter(out leftExpression, state);
			boolExp = BooleanEvaluation(leftExpression); 
			if (la.kind == 2 || la.kind == 3) {
				if (la.kind == 2 || la.kind == 9 || la.kind == 20) {
					while (la.kind == 2) {
						Get();
						op = t.val; 
						BooleanExpressionParameter(out rightExpression, state);
						boolExp = BooleanEvaluation(leftExpression, t.val, rightExpression); 
						leftExpression = boolExp;
						
					}
				} else {
					Get();
					boolExp = BooleanEvaluation(leftExpression, t.val); 
				}
			}
		} else SynErr(39);
	}

	void ElseIfClause(out CodeConditionStatement result, ParserState state) {
		CodeConditionStatement ifStmt = SetupIfStatement();
		CodeConditionStatement elseifStmt;
		CodeExpression boolExp;
		result = ifStmt;
		
		Expect(11);
		Expect(9);
		Expect(19);
		BooleanExpression(out boolExp, state);
		Expect(20);
		ifStmt.Condition = boolExp; 
		Expect(9);
		if (la.kind == 23) {
			CommandBlock(ifStmt.TrueStatements, state);
		} else if (StartOf(1)) {
			CommandLine(ifStmt.TrueStatements, state);
		} else SynErr(40);
		if (la.kind == 9) {
			Get();
		}
		if (IsElseIf()) {
			Expect(12);
			if (la.kind == 9) {
				Get();
			}
			ElseIfClause(out elseifStmt, state);
			ifStmt.FalseStatements.Add(elseifStmt); 
		} else if (IsElse()) {
			Expect(12);
			if (la.kind == 9) {
				Get();
			}
			ElseClause(ifStmt.FalseStatements, state);
		} else SynErr(41);
	}

	void ElseClause(CodeStatementCollection result, ParserState state) {
		Expect(10);
		Expect(9);
		if (la.kind == 23) {
			CommandBlock(result, state);
		} else if (StartOf(1)) {
			CommandLine(result, state);
		} else SynErr(42);
	}

	void BooleanExpressionParameter(out CodeExpression result, ParserState state) {
		string currentSpace;
		CodeExpression temp; 
		Expression(out result, state);
		while (StartOf(3)) {
			if (StartOf(2)) {
				Expression(out temp, state);
				result = new CodeBinaryOperatorExpression(result, CodeBinaryOperatorType.Add, temp); 
			} else if (la.kind == 17) {
				Get();
				result = new CodeBinaryOperatorExpression(result, CodeBinaryOperatorType.Add, new CodePrimitiveExpression(t.val)); 
			} else {
				Get();
				currentSpace = t.val; 
				if (la.kind == 6) {
					Get();
					Expect(9);
				} else if (StartOf(10)) {
					result = new CodeBinaryOperatorExpression(result, CodeBinaryOperatorType.Add, new CodePrimitiveExpression(currentSpace)); 
				} else SynErr(43);
			}
		}
	}

	void Expression(out CodeExpression result, ParserState state) {
		result = null; 
		if (la.kind == 4 || la.kind == 5 || la.kind == 25) {
			StaticExpression(out result);
		} else if (la.kind == 7) {
			IdentifierCall(out result, state);
		} else if (la.kind == 8) {
			VarReference(out result, state);
		} else SynErr(44);
	}

	void StaticExpression(out CodeExpression result) {
		StringBuilder data = new StringBuilder(); 
		if (la.kind == 5) {
			Get();
			data.Append(t.val); 
		} else if (la.kind == 4) {
			Get();
			data.Append(t.val); 
		} else if (la.kind == 25) {
			Get();
			data.Append(t.val); 
		} else SynErr(45);
		while (StartOf(11)) {
			if (la.kind == 5) {
				Get();
				data.Append(t.val); 
			} else if (la.kind == 9) {
				Get();
				data.Append(t.val); 
			} else if (la.kind == 4) {
				Get();
				data.Append(t.val); 
			} else {
				Get();
				data.Append(t.val); 
			}
		}
		result = new CodePrimitiveExpression(data.ToString()); 
	}

	void IdentifierCall(out CodeExpression result, ParserState state) {
		string idname;
		string prop = null;
		List<CodeExpression> parameters = new List<CodeExpression>();
		CodeExpression temp;
		
		Expect(7);
		idname = t.val; 
		if (la.kind == 19) {
			Get();
			if (StartOf(2)) {
				ExpressionParameter(out temp, state);
				parameters.Add(temp); 
				while (la.kind == 17) {
					Get();
					if (la.kind == 9) {
						Get();
					}
					if (StartOf(2)) {
						ExpressionParameter(out temp, state);
						parameters.Add(temp); 
					}
				}
			}
			Expect(20);
			if (la.kind == 25) {
				Get();
				Expect(5);
				prop = t.val; 
			}
		}
		result = CallIdentifier(idname, parameters.ToArray(), prop); 
	}

	void VarReference(out CodeExpression result, ParserState state) {
		result = null; 
		string varname;
		
		Expect(8);
		varname = t.val; 
		result = GetVariableValue(varname, state); 
		
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
		{T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x},
		{x,x,x,x, T,T,x,T, T,x,x,x, x,x,x,T, x,x,T,x, x,T,x,x, x,T,x,x},
		{x,x,x,x, T,T,x,T, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x},
		{x,x,x,x, T,T,x,T, T,T,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,T,x,x},
		{T,x,x,x, T,T,x,T, T,T,x,x, T,T,x,x, x,T,x,x, x,x,T,x, T,T,x,x},
		{x,x,x,x, T,T,x,T, T,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x},
		{T,x,x,x, T,T,x,T, T,T,x,x, T,T,x,x, x,x,x,x, x,x,T,x, T,T,x,x},
		{x,x,x,x, T,T,x,T, T,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,T,x,x},
		{T,x,x,x, x,x,x,x, T,T,x,x, T,T,x,x, x,T,x,x, x,x,T,x, T,x,x,x},
		{T,x,x,x, T,T,x,T, T,T,x,x, T,T,x,x, x,T,x,x, T,x,T,x, T,T,x,x},
		{x,x,T,T, T,T,x,T, T,T,x,x, x,x,x,x, x,T,x,x, T,x,x,x, x,T,x,x},
		{x,x,x,x, T,T,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x}

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
			case 1: s = "assign expected"; break;
			case 2: s = "multiop expected"; break;
			case 3: s = "singleop expected"; break;
			case 4: s = "number expected"; break;
			case 5: s = "word expected"; break;
			case 6: s = "strconcat expected"; break;
			case 7: s = "idcall expected"; break;
			case 8: s = "varname expected"; break;
			case 9: s = "sp expected"; break;
			case 10: s = "else expected"; break;
			case 11: s = "elseif expected"; break;
			case 12: s = "EOL expected"; break;
			case 13: s = "\"alias\" expected"; break;
			case 14: s = "\"-l\" expected"; break;
			case 15: s = "\"var\" expected"; break;
			case 16: s = "\"=\" expected"; break;
			case 17: s = "\",\" expected"; break;
			case 18: s = "\"while\" expected"; break;
			case 19: s = "\"(\" expected"; break;
			case 20: s = "\")\" expected"; break;
			case 21: s = "\"if\" expected"; break;
			case 22: s = "\"|\" expected"; break;
			case 23: s = "\"{\" expected"; break;
			case 24: s = "\"}\" expected"; break;
			case 25: s = "\".\" expected"; break;
			case 26: s = "??? expected"; break;
			case 27: s = "invalid AliasDecl"; break;
			case 28: s = "invalid AliasName"; break;
			case 29: s = "invalid Command"; break;
			case 30: s = "invalid VarAssignment"; break;
			case 31: s = "invalid ExpressionLine"; break;
			case 32: s = "invalid IfClause"; break;
			case 33: s = "invalid IfClause"; break;
			case 34: s = "invalid WhileClause"; break;
			case 35: s = "invalid LocalVarDecl"; break;
			case 36: s = "invalid LocalVarDecl"; break;
			case 37: s = "invalid LocalVarDecl"; break;
			case 38: s = "invalid ExpressionParameter"; break;
			case 39: s = "invalid BooleanExpression"; break;
			case 40: s = "invalid ElseIfClause"; break;
			case 41: s = "invalid ElseIfClause"; break;
			case 42: s = "invalid ElseClause"; break;
			case 43: s = "invalid BooleanExpressionParameter"; break;
			case 44: s = "invalid Expression"; break;
			case 45: s = "invalid StaticExpression"; break;

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
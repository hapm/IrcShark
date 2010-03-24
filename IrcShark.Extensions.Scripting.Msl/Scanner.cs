
using System;
using System.IO;
using System.Collections.Generic;
using IrcShark.Extensions.Scripting;

namespace IrcShark.Extensions.Scripting.Msl {

public class Token {
    public int kind;    // token kind
    public int pos;     // token position in the source text (starting at 0)
    public int col;     // token column (starting at 1)
    public int line;    // token line (starting at 1)
    public string val;  // token value
    public Token next;  // ML 2005-03-11 Tokens are kept in linked list
}

//-----------------------------------------------------------------------------------
// Scanner
//-----------------------------------------------------------------------------------
public class Scanner {
    const char EOL = '\n';
    const int eofSym = 0; /* pdt */
	const int maxT = 20;
	const int noSym = 20;


    public Buffer buffer; // scanner buffer
    
    Token t;          // current token
    int ch;           // current input character
    int pos;          // byte position of current character
    int col;          // column number of current character
    int line;         // line number of current character
    int oldEols;      // EOLs that appeared in a comment;
    static readonly Dictionary<int, int> start; // maps first token character to start state

    Token tokens;     // list of tokens already peeked (first token is a dummy)
    Token pt;         // current peek token
    
    char[] tval = new char[128]; // text of current token
    int tlen;         // length of current token
    
    static Scanner() {
        start = new Dictionary<int, int>(128);
		for (int i = 48; i <= 57; ++i) start[i] = 6;
		for (int i = 0; i <= 8; ++i) start[i] = 7;
		for (int i = 11; i <= 12; ++i) start[i] = 7;
		for (int i = 14; i <= 31; ++i) start[i] = 7;
		for (int i = 35; i <= 35; ++i) start[i] = 7;
		for (int i = 39; i <= 39; ++i) start[i] = 7;
		for (int i = 42; i <= 43; ++i) start[i] = 7;
		for (int i = 45; i <= 46; ++i) start[i] = 7;
		for (int i = 59; i <= 59; ++i) start[i] = 7;
		for (int i = 63; i <= 90; ++i) start[i] = 7;
		for (int i = 94; i <= 104; ++i) start[i] = 7;
		for (int i = 106; i <= 122; ++i) start[i] = 7;
		for (int i = 124; i <= 124; ++i) start[i] = 7;
		for (int i = 126; i <= 65535; ++i) start[i] = 7;
		for (int i = 36; i <= 36; ++i) start[i] = 14;
		for (int i = 37; i <= 37; ++i) start[i] = 10;
		for (int i = 32; i <= 32; ++i) start[i] = 11;
		for (int i = 10; i <= 10; ++i) start[i] = 13;
		for (int i = 13; i <= 13; ++i) start[i] = 12;
		start[61] = 15; 
		start[33] = 2; 
		for (int i = 38; i <= 38; ++i) start[i] = 16;
		for (int i = 60; i <= 60; ++i) start[i] = 16;
		for (int i = 62; i <= 62; ++i) start[i] = 16;
		start[47] = 3; 
		start[92] = 17; 
		start[105] = 18; 
		start[40] = 49; 
		start[41] = 50; 
		start[123] = 51; 
		start[125] = 52; 
		start[44] = 53; 
		start[Buffer.EOF] = -1;

    }
    
    public Scanner(string fileName) {
        try {
            Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            buffer = new StreamBuffer(stream, false);
            Init();
        } catch (IOException) {
            throw new FatalError("Cannot open file " + fileName);
        }
    }
    
    public Scanner(Stream s) {
        buffer = new StreamBuffer(s, true);
        Init();
    }
    
    public Scanner(TextReader reader) {
        buffer = new TextReaderBuffer(reader);
        Init();
    }
    
    void Init() {
        pos = -1; line = 1; col = 0;
        oldEols = 0;
        NextCh();
        /*if (ch == 0xEF) { // check optional byte order mark for UTF-8
            NextCh(); int ch1 = ch;
            NextCh(); int ch2 = ch;
            if (ch1 != 0xBB || ch2 != 0xBF) {
                throw new FatalError(String.Format("illegal byte order mark: EF {0,2:X} {1,2:X}", ch1, ch2));
            }
            buffer = new UTF8Buffer(buffer); col = 0;
            NextCh();
        }*/
        pt = tokens = new Token();  // first token is a dummy
    }
    
    void NextCh() {
        if (oldEols > 0) { ch = EOL; oldEols--; } 
        else {
            pos = buffer.Pos;
            ch = buffer.Read(); col++;
            // replace isolated '\r' by '\n' in order to make
            // eol handling uniform across Windows, Unix and Mac
            if (ch == '\r' && buffer.Peek() != '\n') ch = EOL;
            if (ch == EOL) { line++; col = 0; }
        }

    }

    void AddCh() {
        if (tlen >= tval.Length) {
            char[] newBuf = new char[2 * tval.Length];
            Array.Copy(tval, 0, newBuf, 0, tval.Length);
            tval = newBuf;
        }
        if (ch != Buffer.EOF) {
			tval[tlen++] = (char) ch;
            NextCh();
        }
    }



	bool Comment0() {
		int level = 1, pos0 = pos, line0 = line, col0 = col;
		NextCh();
			for(;;) {
				if (ch == 10) {
					level--;
					if (level == 0) { oldEols = line - line0; NextCh(); return true; }
					NextCh();
				} else if (ch == Buffer.EOF) return false;
				else NextCh();
			}
	}

	bool Comment1() {
		int level = 1, pos0 = pos, line0 = line, col0 = col;
		NextCh();
		if (ch == '*') {
			NextCh();
			for(;;) {
				if (ch == '*') {
					NextCh();
					if (ch == '/') {
						level--;
						if (level == 0) { oldEols = line - line0; NextCh(); return true; }
						NextCh();
					}
				} else if (ch == '/') {
					NextCh();
					if (ch == '*') {
						level++; NextCh();
					}
				} else if (ch == Buffer.EOF) return false;
				else NextCh();
			}
		} else {
			buffer.Pos = pos0; NextCh(); line = line0; col = col0;
		}
		return false;
	}


    void CheckLiteral() {
		switch (t.val) {
			case "alias": t.kind = 10; break;
			case "-l": t.kind = 11; break;
			case "if": t.kind = 12; break;
			case "|": t.kind = 15; break;
			case ".": t.kind = 18; break;
			default: break;
		}
    }

    Token NextToken() {
        while (/*ch == ' ' || we don't ignore spaces */
			false
        ) NextCh();
		if (ch == ';' && Comment0() ||ch == '/' && Comment1()) return NextToken();
		int apx = 0;
        t = new Token();
        t.pos = pos; t.col = col; t.line = line; 
        int state;
        if (!start.TryGetValue(ch, out state))
            state = 0;
        tlen = 0; AddCh();
        
        switch (state) {
            case -1: { t.kind = eofSym; break; } // NextCh already done
            case 0: { t.kind = noSym; break; }   // NextCh already done
			case 1:
				if (ch == ' ') {apx++; AddCh(); goto case 4;}
				else {t.kind = noSym; break;}
			case 2:
				if (ch == '=') {AddCh(); goto case 1;}
				else {t.kind = noSym; break;}
			case 3:
				if (ch == '/') {AddCh(); goto case 1;}
				else {t.kind = noSym; break;}
			case 4:
				{
					tlen -= apx;
					buffer.Pos = t.pos; NextCh(); line = t.line; col = t.col;
					for (int i = 0; i < tlen; i++) NextCh();
					t.kind = 1; break;}
			case 5:
				{
					tlen -= apx;
					buffer.Pos = t.pos; NextCh(); line = t.line; col = t.col;
					for (int i = 0; i < tlen; i++) NextCh();
					t.kind = 2; break;}
			case 6:
				if (ch >= '0' && ch <= '9') {AddCh(); goto case 6;}
				else {t.kind = 3; break;}
			case 7:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 8:
				{
					tlen -= apx;
					buffer.Pos = t.pos; NextCh(); line = t.line; col = t.col;
					for (int i = 0; i < tlen; i++) NextCh();
					t.kind = 5; break;}
			case 9:
				if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= 31 || ch >= '!' && ch <= 39 || ch >= '*' && ch <= '+' || ch >= '-' && ch <= '9' || ch >= ';' && ch <= 65535) {AddCh(); goto case 9;}
				else {t.kind = 6; break;}
			case 10:
				if (ch <= 8 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= 31 || ch >= '!' && ch <= 39 || ch >= '*' && ch <= '+' || ch >= '-' && ch <= '9' || ch >= ';' && ch <= 65535) {AddCh(); goto case 10;}
				else {t.kind = 7; break;}
			case 11:
				if (ch == ' ') {AddCh(); goto case 11;}
				else {t.kind = 8; break;}
			case 12:
				if (ch == 10) {AddCh(); goto case 13;}
				else {t.kind = noSym; break;}
			case 13:
				{t.kind = 9; break;}
			case 14:
				if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= 31 || ch >= '!' && ch <= 39 || ch == '*' || ch >= '-' && ch <= '9' || ch >= ';' && ch <= 65535) {AddCh(); goto case 9;}
				else if (ch == '+') {AddCh(); goto case 19;}
				else {t.kind = 6; break;}
			case 15:
				if (ch == '=') {AddCh(); goto case 20;}
				else {t.kind = noSym; break;}
			case 16:
				if (ch == ' ') {apx++; AddCh(); goto case 4;}
				else if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 17:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 92) {AddCh(); goto case 1;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 18:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'r' || ch >= 't' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 's') {AddCh(); goto case 21;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 19:
				if (ch == ' ') {apx++; AddCh(); goto case 8;}
				else if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= 31 || ch >= '!' && ch <= 39 || ch >= '*' && ch <= '+' || ch >= '-' && ch <= '9' || ch >= ';' && ch <= 65535) {AddCh(); goto case 9;}
				else {t.kind = 6; break;}
			case 20:
				if (ch == ' ') {apx++; AddCh(); goto case 4;}
				else if (ch == '=') {AddCh(); goto case 1;}
				else {t.kind = noSym; break;}
			case 21:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'b' && ch <= 'h' || ch >= 'j' && ch <= 'k' || ch == 'm' || ch >= 'o' && ch <= 't' || ch == 'v' || ch >= 'x' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'i') {AddCh(); goto case 22;}
				else if (ch == 'w') {AddCh(); goto case 23;}
				else if (ch == 'n') {AddCh(); goto case 24;}
				else if (ch == 'l') {AddCh(); goto case 25;}
				else if (ch == 'a') {AddCh(); goto case 26;}
				else if (ch == 'u') {AddCh(); goto case 27;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 22:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'm' || ch >= 'o' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'n') {AddCh(); goto case 28;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 23:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'l' || ch >= 'n' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'm') {AddCh(); goto case 29;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 24:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 't' || ch >= 'v' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'u') {AddCh(); goto case 30;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 25:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'd' || ch >= 'f' && ch <= 'n' || ch >= 'p' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'e') {AddCh(); goto case 31;}
				else if (ch == 'o') {AddCh(); goto case 32;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 26:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'k' || ch >= 'm' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'l') {AddCh(); goto case 33;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 27:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'o' || ch >= 'q' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'p') {AddCh(); goto case 34;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 28:
				if (ch == ' ') {apx++; AddCh(); goto case 4;}
				else if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'b' || ch >= 'd' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'c') {AddCh(); goto case 35;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 29:
				if (ch == ' ') {apx++; AddCh(); goto case 4;}
				else if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'b' || ch >= 'd' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'c') {AddCh(); goto case 36;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 30:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'l' || ch >= 'n' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'm') {AddCh(); goto case 37;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 31:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 's' || ch >= 'u' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 't') {AddCh(); goto case 38;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 32:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'v' || ch >= 'x' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'w') {AddCh(); goto case 39;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 33:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'm' || ch == 'o' || ch >= 'q' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'n') {AddCh(); goto case 40;}
				else if (ch == 'p') {AddCh(); goto case 41;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 34:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'o' || ch >= 'q' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'p') {AddCh(); goto case 42;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 35:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'r' || ch >= 't' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 's') {AddCh(); goto case 16;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 36:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'r' || ch >= 't' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 's') {AddCh(); goto case 16;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 37:
				if (ch == ' ') {apx++; AddCh(); goto case 5;}
				else if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 38:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 's' || ch >= 'u' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 't') {AddCh(); goto case 43;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 39:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'd' || ch >= 'f' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'e') {AddCh(); goto case 44;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 40:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 't' || ch >= 'v' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'u') {AddCh(); goto case 45;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 41:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'g' || ch >= 'i' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'h') {AddCh(); goto case 46;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 42:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'd' || ch >= 'f' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'e') {AddCh(); goto case 47;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 43:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'd' || ch >= 'f' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'e') {AddCh(); goto case 48;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 44:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'q' || ch >= 's' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'r') {AddCh(); goto case 37;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 45:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'l' || ch >= 'n' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'm') {AddCh(); goto case 37;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 46:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'b' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'a') {AddCh(); goto case 37;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 47:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'q' || ch >= 's' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'r') {AddCh(); goto case 37;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 48:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= 'q' || ch >= 's' && ch <= '{' || ch == '}') {AddCh(); goto case 7;}
				else if (ch == 'r') {AddCh(); goto case 37;}
				else {t.kind = 4; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 49:
				{t.kind = 13; break;}
			case 50:
				{t.kind = 14; break;}
			case 51:
				{t.kind = 16; break;}
			case 52:
				{t.kind = 17; break;}
			case 53:
				{t.kind = 19; break;}

        }
        t.val = new String(tval, 0, tlen);
        return t;
    }
    
    // get the next token (possibly a token already seen during peeking)
    public Token Scan () {
        if (tokens.next == null) {
            return NextToken();
        } else {
            pt = tokens = tokens.next;
            return tokens;
        }
    }

    // peek for the next token, ignore pragmas
    public Token Peek () {
        do {
            if (pt.next == null) {
                pt.next = NextToken();
            }
            pt = pt.next;
        } while (pt.kind > maxT); // skip pragmas
    
        return pt;
    }

    // make sure that peeking starts at the current scan position
    public void ResetPeek () { pt = tokens; }

} // end Scanner

}
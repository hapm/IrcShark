
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
	const int maxT = 18;
	const int noSym = 18;


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
		for (int i = 48; i <= 57; ++i) start[i] = 1;
		for (int i = 0; i <= 8; ++i) start[i] = 2;
		for (int i = 11; i <= 12; ++i) start[i] = 2;
		for (int i = 14; i <= 31; ++i) start[i] = 2;
		for (int i = 35; i <= 35; ++i) start[i] = 2;
		for (int i = 38; i <= 39; ++i) start[i] = 2;
		for (int i = 42; i <= 43; ++i) start[i] = 2;
		for (int i = 45; i <= 46; ++i) start[i] = 2;
		for (int i = 59; i <= 60; ++i) start[i] = 2;
		for (int i = 62; i <= 90; ++i) start[i] = 2;
		for (int i = 92; i <= 92; ++i) start[i] = 2;
		for (int i = 94; i <= 122; ++i) start[i] = 2;
		for (int i = 124; i <= 124; ++i) start[i] = 2;
		for (int i = 126; i <= 65535; ++i) start[i] = 2;
		for (int i = 36; i <= 36; ++i) start[i] = 9;
		for (int i = 37; i <= 37; ++i) start[i] = 5;
		for (int i = 32; i <= 32; ++i) start[i] = 6;
		for (int i = 10; i <= 10; ++i) start[i] = 8;
		for (int i = 13; i <= 13; ++i) start[i] = 7;
		start[40] = 11; 
		start[41] = 12; 
		start[123] = 13; 
		start[125] = 14; 
		start[44] = 15; 
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
			case "alias": t.kind = 8; break;
			case "-l": t.kind = 9; break;
			case "if": t.kind = 10; break;
			case "|": t.kind = 13; break;
			case ".": t.kind = 16; break;
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
				if (ch >= '0' && ch <= '9') {AddCh(); goto case 1;}
				else {t.kind = 1; break;}
			case 2:
				if (ch >= '!' && ch <= '"' || ch >= '$' && ch <= '%' || ch >= '/' && ch <= '9' || ch == '=' || ch >= 'A' && ch <= '[' || ch == ']' || ch >= 'a' && ch <= '{' || ch == '}') {AddCh(); goto case 2;}
				else {t.kind = 2; t.val = new String(tval, 0, tlen); CheckLiteral(); return t;}
			case 3:
				{
					tlen -= apx;
					buffer.Pos = t.pos; NextCh(); line = t.line; col = t.col;
					for (int i = 0; i < tlen; i++) NextCh();
					t.kind = 3; break;}
			case 4:
				if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= 31 || ch >= '!' && ch <= 39 || ch >= '*' && ch <= '+' || ch >= '-' && ch <= '9' || ch >= ';' && ch <= 65535) {AddCh(); goto case 4;}
				else {t.kind = 4; break;}
			case 5:
				if (ch <= 8 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= 31 || ch >= '!' && ch <= 39 || ch >= '*' && ch <= '+' || ch >= '-' && ch <= '9' || ch >= ';' && ch <= 65535) {AddCh(); goto case 5;}
				else {t.kind = 5; break;}
			case 6:
				if (ch == ' ') {AddCh(); goto case 6;}
				else {t.kind = 6; break;}
			case 7:
				if (ch == 10) {AddCh(); goto case 8;}
				else {t.kind = noSym; break;}
			case 8:
				{t.kind = 7; break;}
			case 9:
				if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= 31 || ch >= '!' && ch <= 39 || ch == '*' || ch >= '-' && ch <= '9' || ch >= ';' && ch <= 65535) {AddCh(); goto case 4;}
				else if (ch == '+') {AddCh(); goto case 10;}
				else {t.kind = 4; break;}
			case 10:
				if (ch == ' ') {apx++; AddCh(); goto case 3;}
				else if (ch <= 9 || ch >= 11 && ch <= 12 || ch >= 14 && ch <= 31 || ch >= '!' && ch <= 39 || ch >= '*' && ch <= '+' || ch >= '-' && ch <= '9' || ch >= ';' && ch <= 65535) {AddCh(); goto case 4;}
				else {t.kind = 4; break;}
			case 11:
				{t.kind = 11; break;}
			case 12:
				{t.kind = 12; break;}
			case 13:
				{t.kind = 14; break;}
			case 14:
				{t.kind = 15; break;}
			case 15:
				{t.kind = 17; break;}

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
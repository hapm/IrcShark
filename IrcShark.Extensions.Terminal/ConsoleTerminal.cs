// <copyright file="ConsoleTerminal.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the ConsoleTerminal class.</summary>

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
namespace IrcShark.Extensions.Terminal
{
    using System;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// This class allows the TerminalExtension to use the local Console as
    /// as a terminal for a user.
    /// </summary>
    public class ConsoleTerminal : ITerminal
    {
		/// <summary>
		/// 
		/// </summary>
		private AutoCompleteHandler autoCompleteEvent;
		
		/// <summary>
		/// Used for internal key handlings.
		/// </summary>
		private delegate void KeyHandler ();
		
		/// <summary>
		/// The text being edited.
		/// </summary>
		private StringBuilder text;

		/// <summary>
		/// The text as it is rendered (replaces (char)1 with ^A on display for example).
		/// </summary>
		private StringBuilder rendered_text;

		/// <summary>
		/// The prompt specified, and the prompt shown to the user.
		/// </summary>
		private string prompt;
		private string shown_prompt;
		
		/// <summary>
		/// The current cursor position in text.
		/// </summary>
		/// <remarks>
		/// For an index into rendered_text, use TextToRenderPos.
		/// </remarks>
		private int cursor;

		/// <summary>
		/// The row where we started displaying data.
		/// </summary>
		private int home_row;

		/// <summary>
		/// The maximum length that has been displayed on the screen.
		/// </summary>
		private int max_rendered;

		/// <summary>
		/// If we are done editing, this breaks the interactive loop.
		/// </summary>
		private bool done = false;

		/// <summary>
		/// The thread where the editing started taking place.
		/// </summary>
		private Thread edit_thread;

		/// <summary>
		/// Our object that tracks history.
		/// </summary>
		private History history;

		/// <summary>
		/// The contents of the kill buffer (cut/paste in Emacs parlance).
		/// </summary>
		private string kill_buffer = "";

		/// <summary>
		/// The string being searched for.
		/// </summary>
		private string search;
		private string last_search;

		/// <summary>
		/// Whether we are searching (-1= reverse; 0 = no; 1 = forward).
		/// </summary>
		private int searching;

		/// <summary>
		/// The position where we found the match.
		/// </summary>
		private int match_at;
		
		/// <summary>
		/// Used to implement the Kill semantics (multiple Alt-Ds accumulate).
		/// </summary>
		private KeyHandler last_handler;
		
		/// <summary>
		/// Saves all registred console key handlers.
		/// </summary>
		private static Handler[] handlers;
		
        /// <summary>
        /// Saves the currently selected foreground color.
        /// </summary>
        private ConsoleColor foregroundColor;
		
		/// <summary>
		/// The Handler struct is used to save a special key and its handlermethod.
		/// </summary>
		private struct Handler {
		    /// <summary>
		    /// Saves the key, that is watched.
		    /// </summary>
			public ConsoleKeyInfo CKI;
			
			/// <summary>
			/// Saves the handler that is called when the key is pressed.
			/// </summary>
			public KeyHandler KeyHandler;

			/// <summary>
			/// Initializes a new instance of the Handler struct.
			/// </summary>
			/// <param name="key">The key to look for.</param>
			/// <param name="h">The handler to call.</param>
			public Handler (ConsoleKey key, KeyHandler h)
			{
				CKI = new ConsoleKeyInfo ((char) 0, key, false, false, false);
				KeyHandler = h;
			}

			/// <summary>
			/// Initializes a new instance of the Handler struct.
			/// </summary>
			/// <param name="c">The character of the key to look for.</param>
			/// <param name="h">The handler to call.</param>
			public Handler (char c, KeyHandler h)
			{
				KeyHandler = h;
				// Use the "Zoom" as a flag that we only have a character.
				CKI = new ConsoleKeyInfo (c, ConsoleKey.Zoom, false, false, false);
			}

			/// <summary>
			/// Initializes a new instance of the Handler struct.
			/// </summary>
			/// <param name="cki">The key combination to look for.</param>
			/// <param name="h">The handler to call.</param>
			public Handler (ConsoleKeyInfo cki, KeyHandler h)
			{
				CKI = cki;
				KeyHandler = h;
			}
			
			/// <summary>
			/// Creates a Handler, that reacts on a key pressed in combination with the control key.
			/// </summary>
			/// <param name="c">The character of the key to look for.</param>
			/// <param name="h">The handler to call.</param>
			/// <returns>The produced Handler instance.</returns>
			public static Handler Control (char c, KeyHandler h)
			{
				return new Handler ((char) (c - 'A' + 1), h);
			}

			/// <summary>
			/// Creates a Handler, that reacts on a key pressed in combination with the alt key.
			/// </summary>
			/// <param name="c">The character of the key to look for.</param>
			/// <param name="key">The key to look for.</param>
			/// <param name="h">The handler to call.</param>
			/// <returns>The produced Handler instance.</returns>
			public static Handler Alt (char c, ConsoleKey k, KeyHandler h)
			{
				ConsoleKeyInfo cki = new ConsoleKeyInfo ((char) c, k, false, true, false);
				return new Handler (cki, h);
			}
		}
		
		/// <summary>
		/// This is true if the edit line is active.
		/// </summary>
		private bool isEditing;
		
        /// <summary>
        /// Initializes a new instance of the ConsoleTerminal class.
        /// </summary>
		public ConsoleTerminal() : this(10) {}
        
        /// <summary>
        /// Initializes a new instance of the ConsoleTerminal class.
        /// </summary>
        /// <param name="histsize">The size of the command history.</param>
        public ConsoleTerminal(int histsize)
        {
            Console.Title = "IrcShark Terminal";
            ResetColor();
            
			handlers = new Handler [] {
				new Handler (ConsoleKey.Home,       CmdHome),
				new Handler (ConsoleKey.End,        CmdEnd),
				new Handler (ConsoleKey.LeftArrow,  CmdLeft),
				new Handler (ConsoleKey.RightArrow, CmdRight),
				new Handler (ConsoleKey.UpArrow,    CmdHistoryPrev),
				new Handler (ConsoleKey.DownArrow,  CmdHistoryNext),
				new Handler (ConsoleKey.Enter,      CmdDone),
				new Handler (ConsoleKey.Backspace,  CmdBackspace),
				new Handler (ConsoleKey.Delete,     CmdDeleteChar),
				new Handler (ConsoleKey.Tab,        CmdTabOrComplete),
				
				// Emacs keys
				Handler.Control ('A', CmdHome),
				Handler.Control ('E', CmdEnd),
				Handler.Control ('B', CmdLeft),
				Handler.Control ('F', CmdRight),
				Handler.Control ('P', CmdHistoryPrev),
				Handler.Control ('N', CmdHistoryNext),
				Handler.Control ('K', CmdKillToEOF),
				Handler.Control ('Y', CmdYank),
				Handler.Control ('D', CmdDeleteChar),
				Handler.Control ('L', CmdRefresh),
				Handler.Control ('R', CmdReverseSearch),
				Handler.Control ('G', delegate {} ),
				Handler.Alt ('B', ConsoleKey.B, CmdBackwardWord),
				Handler.Alt ('F', ConsoleKey.F, CmdForwardWord),
				
				Handler.Alt ('D', ConsoleKey.D, CmdDeleteWord),
				Handler.Alt ((char) 8, ConsoleKey.Backspace, CmdDeleteBackword),
				
				// DEBUG
				//Handler.Control ('T', CmdDebug),

				// quote
				Handler.Control ('Q', delegate { HandleChar (Console.ReadKey (true).KeyChar); })
			};

			rendered_text = new StringBuilder();
			text = new StringBuilder();

			history = new History(histsize);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the tab ky completes without having typed anything.
        /// </summary>
        /// <value>
        /// If true, the autocomplete handler is called whenever tab is pressed,
        /// and false if it is only called when there is some text to complete.
        /// </value>
		public bool TabAtStartCompletes { get; set; }
        
		/// <summary>
		/// Gets or sets the current foreground color to use when drawing text on the console.
		/// </summary>
		/// <value>The selected ConsoleColor.</value>
        public ConsoleColor ForegroundColor {
            get {
                return foregroundColor;
            }
            set {
                foregroundColor = value;
            }
        }
		
        /// <summary>
		/// Gets or sets a handler that is invoked when the user requests 
		/// auto-completion using the tab character.
		/// </summary>
		/// <remarks>
		/// The result is null for no values found, an array with a single
		/// string, in that case the string should be the text to be inserted
		/// for example if the word at pos is "T", the result for a completion
		/// of "ToString" should be "oString", not "ToString".
		///
		/// When there are multiple results, the result should be the full text.
		/// </remarks>
        public AutoCompleteHandler AutoCompleteEvent {
            get {
                return autoCompleteEvent;
            }
            set {
                autoCompleteEvent = value;
            }
        }
		
        /// <summary>
        /// Gets a value indicating whether the terminal is currently reading
        /// a command.
        /// </summary>
		public bool IsReading
		{
		    get { return isEditing; }
		}
        
        /// <summary>
        /// Resets the foreground and background color of the terminal.
        /// </summary>
        public void ResetColor()
        {
            Console.ResetColor();
            foregroundColor = Console.ForegroundColor;
        }
		
        /// <summary>
        /// Writes a complete line and appends a linebreak at the end.
        /// </summary>
        /// <param name="line">The line to write.</param>
        public void WriteLine(string line) 
        {
        	if (!isEditing)
        	{
        		Console.WriteLine(line);
        	}
        } 
        
        /// <summary>
        /// Writes a complete formated line and appends a linebreak at the end.
        /// </summary>
        /// <param name="format">The format to write.</param>
        /// <param name="arg">The objects to use when formating the line.</param>
        public void WriteLine(string format, params object[] arg) 
        {
        	WriteLine(string.Format(format, arg));
        }
        
        /// <summary>
        /// Writes a linebreak to the terminal.
        /// </summary>
        public void WriteLine() 
        {
        	WriteLine(string.Empty);
        }
        
        /// <summary>
        /// Reads a command from the terminal.
        /// </summary>
        /// <returns>
        /// The CommandCall instance for the command or null, if the user din't type a command.
        /// </returns>
        public CommandCall ReadCommand() 
        {
        	string command = Edit("shell>", "");
            if (!string.IsNullOrEmpty(command))
            {
                CommandCall call = new CommandCall(command);
                return call;
            }
            return null;
        }
        
        /// <summary>
        /// Stops to read a command from the terminal if it is reading at the moment.
        /// </summary>
        public void StopReading() 
        {
            done = true;
            edit_thread.Abort();
        }
        
        /// <summary>
        /// Starts the console editing.
        /// </summary>
        /// <param name="prompt">The prompt in front of the edit space.</param>
        /// <param name="initial">The text, that should be shown at the beginning.</param>
        /// <returns>The text, that was typed.</returns>
		public string Edit (string prompt, string initial)
		{
			isEditing = true;
			edit_thread = Thread.CurrentThread;
			searching = 0;
			Console.CancelKeyPress += InterruptEdit;
			
			done = false;
			history.CursorToEnd ();
			max_rendered = 0;
			
			Prompt = prompt;
			shown_prompt = prompt;
			InitText (initial);
			history.Append (initial);

			do {
				try {
					EditLoop ();
				} catch (ThreadAbortException){
					searching = 0;
					Thread.ResetAbort ();
					Console.WriteLine ();
					SetPrompt (prompt);
					SetText ("");
				}
			} while (!done);
			Console.WriteLine ();
			
			Console.CancelKeyPress -= InterruptEdit;

			if (text == null)
			{
				isEditing = false;
				return null;
			}

			string result = text.ToString ();
			if (result != "")
				history.Accept (result);
			else
				history.RemoveLast ();
			isEditing = false;
			return result;
		}

		void Render ()
		{
			Console.Write (shown_prompt);
			Console.Write (rendered_text);

			int max = System.Math.Max (rendered_text.Length + shown_prompt.Length, max_rendered);
			
			for (int i = rendered_text.Length + shown_prompt.Length; i < max_rendered; i++)
				Console.Write (' ');
			max_rendered = shown_prompt.Length + rendered_text.Length;

			// Write one more to ensure that we always wrap around properly if we are at the
			// end of a line.
			Console.Write (' ');

			UpdateHomeRow (max);
		}

		void UpdateHomeRow (int screenpos)
		{
			int lines = 1 + (screenpos / Console.WindowWidth);

			home_row = Console.CursorTop - (lines - 1);
			if (home_row < 0)
				home_row = 0;
		}
		
		void RenderFrom (int pos)
		{
			int rpos = TextToRenderPos (pos);
			int i;
			
			for (i = rpos; i < rendered_text.Length; i++)
				Console.Write (rendered_text [i]);

			if ((shown_prompt.Length + rendered_text.Length) > max_rendered)
				max_rendered = shown_prompt.Length + rendered_text.Length;
			else {
				int max_extra = max_rendered - shown_prompt.Length;
				for (; i < max_extra; i++)
					Console.Write (' ');
			}
		}

		void ComputeRendered ()
		{
			rendered_text.Length = 0;

			for (int i = 0; i < text.Length; i++){
				int c = (int) text [i];
				if (c < 26){
					if (c == '\t')
						rendered_text.Append ("    ");
					else {
						rendered_text.Append ('^');
						rendered_text.Append ((char) (c + (int) 'A' - 1));
					}
				} else
					rendered_text.Append ((char)c);
			}
		}

		int TextToRenderPos (int pos)
		{
			int p = 0;

			for (int i = 0; i < pos; i++){
				int c;

				c = (int) text [i];
				
				if (c < 26){
					if (c == 9)
						p += 4;
					else
						p += 2;
				} else
					p++;
			}

			return p;
		}

		int TextToScreenPos (int pos)
		{
			return shown_prompt.Length + TextToRenderPos (pos);
		}
		
		string Prompt {
			get { return prompt; }
			set { prompt = value; }
		}

		int LineCount {
			get {
				return (shown_prompt.Length + rendered_text.Length)/Console.WindowWidth;
			}
		}
		
		void ForceCursor (int newpos)
		{
			cursor = newpos;

			int actual_pos = shown_prompt.Length + TextToRenderPos (cursor);
			int row = home_row + (actual_pos/Console.WindowWidth);
			int col = actual_pos % Console.WindowWidth;

			if (row >= Console.BufferHeight)
				row = Console.BufferHeight-1;
			Console.SetCursorPosition (col, row);
			
			//log.WriteLine ("Going to cursor={0} row={1} col={2} actual={3} prompt={4} ttr={5} old={6}", newpos, row, col, actual_pos, prompt.Length, TextToRenderPos (cursor), cursor);
			//log.Flush ();
		}

		void UpdateCursor (int newpos)
		{
			if (cursor == newpos)
				return;

			ForceCursor (newpos);
		}

		void InsertChar (char c)
		{
			int prev_lines = LineCount;
			text = text.Insert (cursor, c);
			ComputeRendered ();
			if (prev_lines != LineCount){

				Console.SetCursorPosition (0, home_row);
				Render ();
				ForceCursor (++cursor);
			} else {
				RenderFrom (cursor);
				ForceCursor (++cursor);
				UpdateHomeRow (TextToScreenPos (cursor));
			}
		}

		#region "Commands"
		void CmdDebug ()
		{
			Console.WriteLine ();
			Render ();
		}
		
		void CmdDone ()
		{
			done = true;
		}

		void CmdTabOrComplete ()
		{
			bool complete = false;

			if (autoCompleteEvent != null){
				if (TabAtStartCompletes)
					complete = true;
				else {
					for (int i = 0; i < cursor; i++){
						if (!Char.IsWhiteSpace (text [i])){
							complete = true;
							break;
						}
					}
				}

				if (complete){
					Completion completion = autoCompleteEvent(text.ToString (), cursor);
					string [] completions = completion.Result;
					if (completions == null)
						return;
					
					int ncompletions = completions.Length;
					if (ncompletions == 0)
						return;
					
					if (completions.Length == 1){
						InsertTextAtCursor (completions [0]);
					} else {
						int last = -1;
						
						for (int p = 0; p < completions [0].Length; p++){
							char c = completions [0][p];


							for (int i = 1; i < ncompletions; i++){
								if (completions [i].Length < p)
									goto mismatch;
							
								if (completions [i][p] != c){
									goto mismatch;
								}
							}
							last = p;
						}
					mismatch:
						if (last != -1){
							InsertTextAtCursor (completions [0].Substring (0, last+1));
						}
						Console.WriteLine ();
						foreach (string s in completions){
							Console.Write (completion.Prefix);
							Console.Write (s);
							Console.Write (' ');
						}
						Console.WriteLine ();
						Render ();
						ForceCursor (cursor);
					}
				} else
					HandleChar ('\t');
			} else
				HandleChar ('t');
		}
		
		void CmdHome ()
		{
			UpdateCursor (0);
		}

		void CmdEnd ()
		{
			UpdateCursor (text.Length);
		}
		
		void CmdLeft ()
		{
			if (cursor == 0)
				return;

			UpdateCursor (cursor-1);
		}

		void CmdBackwardWord ()
		{
			int p = WordBackward (cursor);
			if (p == -1)
				return;
			UpdateCursor (p);
		}

		void CmdForwardWord ()
		{
			int p = WordForward (cursor);
			if (p == -1)
				return;
			UpdateCursor (p);
		}

		void CmdRight ()
		{
			if (cursor == text.Length)
				return;

			UpdateCursor (cursor+1);
		}

		void RenderAfter (int p)
		{
			ForceCursor (p);
			RenderFrom (p);
			ForceCursor (cursor);
		}
		
		void CmdBackspace ()
		{
			if (cursor == 0)
				return;

			text.Remove (--cursor, 1);
			ComputeRendered ();
			RenderAfter (cursor);
		}

		void CmdDeleteChar ()
		{
			// If there is no input, this behaves like EOF
			if (text.Length == 0){
				done = true;
				text = null;
				Console.WriteLine ();
				return;
			}
			
			if (cursor == text.Length)
				return;
			text.Remove (cursor, 1);
			ComputeRendered ();
			RenderAfter (cursor);
		}

		int WordForward (int p)
		{
			if (p >= text.Length)
				return -1;

			int i = p;
			if (Char.IsPunctuation (text [p]) || Char.IsWhiteSpace (text[p])){
				for (; i < text.Length; i++){
					if (Char.IsLetterOrDigit (text [i]))
					    break;
				}
				for (; i < text.Length; i++){
					if (!Char.IsLetterOrDigit (text [i]))
					    break;
				}
			} else {
				for (; i < text.Length; i++){
					if (!Char.IsLetterOrDigit (text [i]))
					    break;
				}
			}
			if (i != p)
				return i;
			return -1;
		}

		int WordBackward (int p)
		{
			if (p == 0)
				return -1;

			int i = p-1;
			if (i == 0)
				return 0;
			
			if (Char.IsPunctuation (text [i]) || Char.IsSymbol (text [i]) || Char.IsWhiteSpace (text[i])){
				for (; i >= 0; i--){
					if (Char.IsLetterOrDigit (text [i]))
						break;
				}
				for (; i >= 0; i--){
					if (!Char.IsLetterOrDigit (text[i]))
						break;
				}
			} else {
				for (; i >= 0; i--){
					if (!Char.IsLetterOrDigit (text [i]))
						break;
				}
			}
			i++;
			
			if (i != p)
				return i;

			return -1;
		}
		
		void CmdDeleteWord ()
		{
			int pos = WordForward (cursor);

			if (pos == -1)
				return;

			string k = text.ToString (cursor, pos-cursor);
			
			if (last_handler == CmdDeleteWord)
				kill_buffer = kill_buffer + k;
			else
				kill_buffer = k;
			
			text.Remove (cursor, pos-cursor);
			ComputeRendered ();
			RenderAfter (cursor);
		}
		
		void CmdDeleteBackword ()
		{
			int pos = WordBackward (cursor);
			if (pos == -1)
				return;

			string k = text.ToString (pos, cursor-pos);
			
			if (last_handler == CmdDeleteBackword)
				kill_buffer = k + kill_buffer;
			else
				kill_buffer = k;
			
			text.Remove (pos, cursor-pos);
			ComputeRendered ();
			RenderAfter (pos);
		}
		
		//
		// Adds the current line to the history if needed
		//
		void HistoryUpdateLine ()
		{
			history.Update (text.ToString ());
		}
		
		void CmdHistoryPrev ()
		{
			if (!history.PreviousAvailable ())
				return;

			HistoryUpdateLine ();
			
			SetText (history.Previous ());
		}

		void CmdHistoryNext ()
		{
			if (!history.NextAvailable())
				return;

			history.Update (text.ToString ());
			SetText (history.Next ());
			
		}

		void CmdKillToEOF ()
		{
			kill_buffer = text.ToString (cursor, text.Length-cursor);
			text.Length = cursor;
			ComputeRendered ();
			RenderAfter (cursor);
		}

		void CmdYank ()
		{
			InsertTextAtCursor (kill_buffer);
		}

		void InsertTextAtCursor (string str)
		{
			int prev_lines = LineCount;
			text.Insert (cursor, str);
			ComputeRendered ();
			if (prev_lines != LineCount){
				Console.SetCursorPosition (0, home_row);
				Render ();
				cursor += str.Length;
				ForceCursor (cursor);
			} else {
				RenderFrom (cursor);
				cursor += str.Length;
				ForceCursor (cursor);
				UpdateHomeRow (TextToScreenPos (cursor));
			}
		}
		
		void SetSearchPrompt (string s)
		{
			SetPrompt ("(reverse-i-search)`" + s + "': ");
		}

		void ReverseSearch ()
		{
			int p;

			if (cursor == text.Length){
				// The cursor is at the end of the string
				
				p = text.ToString ().LastIndexOf (search);
				if (p != -1){
					match_at = p;
					cursor = p;
					ForceCursor (cursor);
					return;
				}
			} else {
				// The cursor is somewhere in the middle of the string
				int start = (cursor == match_at) ? cursor - 1 : cursor;
				if (start != -1){
					p = text.ToString ().LastIndexOf (search, start);
					if (p != -1){
						match_at = p;
						cursor = p;
						ForceCursor (cursor);
						return;
					}
				}
			}

			// Need to search backwards in history
			HistoryUpdateLine ();
			string s = history.SearchBackward (search);
			if (s != null){
				match_at = -1;
				SetText (s);
				ReverseSearch ();
			}
		}
		
		void CmdReverseSearch ()
		{
			if (searching == 0){
				match_at = -1;
				last_search = search;
				searching = -1;
				search = "";
				SetSearchPrompt ("");
			} else {
				if (search == ""){
					if (last_search != "" && last_search != null){
						search = last_search;
						SetSearchPrompt (search);

						ReverseSearch ();
					}
					return;
				}
				ReverseSearch ();
			} 
		}

		void SearchAppend (char c)
		{
			search = search + c;
			SetSearchPrompt (search);

			//
			// If the new typed data still matches the current text, stay here
			//
			if (cursor < text.Length){
				string r = text.ToString (cursor, text.Length - cursor);
				if (r.StartsWith (search))
					return;
			}

			ReverseSearch ();
		}
		
		void CmdRefresh ()
		{
			Console.Clear ();
			max_rendered = 0;
			Render ();
			ForceCursor (cursor);
		}
		#endregion

		void InterruptEdit (object sender, ConsoleCancelEventArgs a)
		{
			// Do not abort our program:
			a.Cancel = true;

			// Interrupt the editor
			edit_thread.Abort();
		}

		void HandleChar (char c)
		{
			if (searching != 0)
				SearchAppend (c);
			else
				InsertChar (c);
		}

		void EditLoop ()
		{
			ConsoleKeyInfo cki;

			while (!done){
				ConsoleModifiers mod;
				
				cki = Console.ReadKey (true);
				if (cki.Key == ConsoleKey.Escape){
					cki = Console.ReadKey (true);

					mod = ConsoleModifiers.Alt;
				} else
					mod = cki.Modifiers;
				
				bool handled = false;

				foreach (Handler handler in handlers){
					ConsoleKeyInfo t = handler.CKI;

					if (t.Key == cki.Key && t.Modifiers == mod){
						handled = true;
						handler.KeyHandler ();
						last_handler = handler.KeyHandler;
						break;
					} else if (t.KeyChar == cki.KeyChar && t.Key == ConsoleKey.Zoom){
						handled = true;
						handler.KeyHandler ();
						last_handler = handler.KeyHandler;
						break;
					}
				}
				if (handled){
					if (searching != 0){
						if (last_handler != CmdReverseSearch){
							searching = 0;
							SetPrompt (prompt);
						}
					}
					continue;
				}

				if (cki.KeyChar != (char) 0)
					HandleChar (cki.KeyChar);
			} 
		}

		void InitText (string initial)
		{
			text = new StringBuilder (initial);
			ComputeRendered ();
			cursor = text.Length;
			Render ();
			ForceCursor (cursor);
		}

		void SetText (string newtext)
		{
			Console.SetCursorPosition (0, home_row);
			InitText (newtext);
		}

		void SetPrompt (string newprompt)
		{
			shown_prompt = newprompt;
			Console.SetCursorPosition (0, home_row);
			Render ();
			ForceCursor (cursor);
		}
		
    }
}

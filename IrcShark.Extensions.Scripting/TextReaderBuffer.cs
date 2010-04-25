// <copyright file="TextReaderBuffer.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Place a summary here.</summary>

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
namespace IrcShark.Extensions.Scripting
{
    using System;
    using System.IO;

    /// <summary>
    /// The TextReaderBuffer implements the Buffer class to read from a TextReader.
    /// </summary>
    public class TextReaderBuffer : Buffer
    {
        const int MIN_BUFFER_LENGTH = 1024; // 1KB
        const int MAX_BUFFER_LENGTH = MIN_BUFFER_LENGTH * 64; // 64KB
        private TextReader reader;
        int[] buf;         // input buffer
        int bufStart;       // position of first byte in buffer relative to input stream
        int bufLen;         // length of buffer
        int fileLen;        // length of input stream (may change if the stream is no file)
        int bufPos;         // current position in buffer
            
        public TextReaderBuffer(TextReader reader) : base()
        {
            this.reader = reader;
            fileLen = bufLen = bufStart = 0;
            buf = new int[(bufLen > 0) ? bufLen : MIN_BUFFER_LENGTH];
            if (fileLen > 0)
            {
                Pos = 0; // setup buffer to position 0 (start)
            }
            else
            {
                bufPos = 0; // index 0 is already after the file, thus Pos = 0 is invalid
            }
        }
        
        ~TextReaderBuffer()
        {
            Close();
        }
        
        protected override void Close()
        {
        }
        
        public override string GetString(int beg, int end)
        {
            int len = 0;
            char[] buf = new char[end - beg];
            int oldPos = Pos;
            Pos = beg;
            while (Pos < end) 
            {
                buf[len++] = (char) Read();
            }
            
            Pos = oldPos;
            return new string(buf, 0, len);
        }
        
        public override int Peek()
        {
            return reader.Peek();
        }
        
        public override int Read()
        {
            if (bufPos < bufLen) 
            {
                return buf[bufPos++];
            } 
            else if (Pos < fileLen) 
            {
                Pos = Pos; // shift buffer start to Pos
                return buf[bufPos++];
            } 
            else if (reader != null && ReadNextStreamChunk() > 0) 
            {
                return buf[bufPos++];
            } 
            else 
            {
                return EOF;
            }
        }
        
        public override int Pos
        {
            get 
            { 
                return bufPos + bufStart; 
            }
            
            set 
            {
                if (value >= fileLen && reader != null)
                {
                    // Wanted position is after buffer and the stream
                    // is not seek-able e.g. network or console,
                    // thus we have to read the stream manually till
                    // the wanted position is in sight.
                    while (value >= fileLen && ReadNextStreamChunk() > 0)
                    {
                    }
                }
    
                if (value < 0 || value > fileLen)
                {
                    throw new FatalError("buffer out of bounds access, position: " + value);
                }
    
                if (value >= bufStart && value < bufStart + bufLen) 
                { // already in buffer
                    bufPos = value - bufStart;
                }
                else 
                {
                    // set the position to the end of the file, Pos will return fileLen.
                    bufPos = fileLen - bufStart;
                }
            }
        }
        
        // Read the next chunk of bytes from the stream, increases the buffer
        // if needed and updates the fields fileLen and bufLen.
        // Returns the number of bytes read.
        private int ReadNextStreamChunk()
        {
            int free = buf.Length - bufLen;
            if (free == 0)
            {
                // in the case of a growing input stream
                // we can neither seek in the stream, nor can we
                // foresee the maximum length, thus we must adapt
                // the buffer size on demand.
                int[] newBuf = new int[bufLen * 2];
                Array.Copy(buf, newBuf, bufLen);
                buf = newBuf;
                free = bufLen;
            }
            
            char[] temp = new char[free];
            int read = reader.Read(temp, 0, free);
            for (int i = 0; i < read; i++)
            {
                buf[bufLen + i] = (int)temp[i];
            }
            
            if (read > 0)
            {
                fileLen = bufLen = bufLen + read;
                return read;
            }
            
            // end of stream reached
            return 0;
        }
    }
}

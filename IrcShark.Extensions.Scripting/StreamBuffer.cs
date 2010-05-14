// <copyright file="StreamBuffer.cs" company="IrcShark Team">
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
    /// Description of StreamBuffer.
    /// </summary>
    public class StreamBuffer : Buffer
    {
        // This Buffer supports the following cases:
        // 1) seekable stream (file)
        // a) whole stream in buffer
        // b) part of stream in buffer
        // 2) non seekable stream (network, console)
        private const int MIN_BUFFER_LENGTH = 1024; // 1KB
        private const int MAX_BUFFER_LENGTH = MIN_BUFFER_LENGTH * 64; // 64KB
        private byte[] buf;         // input buffer
        private int bufStart;       // position of first byte in buffer relative to input stream
        private int bufLen;         // length of buffer
        private int fileLen;        // length of input stream (may change if the stream is no file)
        private int bufPos;         // current position in buffer
        private Stream stream;      // input stream (seekable)
        private bool isUserStream;  // was the stream opened by the user?
        
        public override int Pos
        {
            get
            {
                return bufPos + bufStart;
            }
            
            set
            {
                if (value >= fileLen && stream != null && !stream.CanSeek)
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
                else if (stream != null)
                { // must be swapped in
                    stream.Seek(value, SeekOrigin.Begin);
                    bufLen = stream.Read(buf, 0, buf.Length);
                    bufStart = value;
                    bufPos = 0;
                }
                else
                {
                    // set the position to the end of the file, Pos will return fileLen.
                    bufPos = fileLen - bufStart;
                }
            }
        }
        
        public StreamBuffer(Stream s, bool isUserStream)
        {
            stream = s;
            this.isUserStream = isUserStream;
            if (stream.CanSeek)
            {
                fileLen = (int) stream.Length;
                bufLen = Math.Min(fileLen, MAX_BUFFER_LENGTH);
                bufStart = Int32.MaxValue; // nothing in the buffer so far
            }
            else
            {
                fileLen = bufLen = bufStart = 0;
            }
    
            buf = new byte[(bufLen > 0) ? bufLen : MIN_BUFFER_LENGTH];
            if (fileLen > 0)
            {
                Pos = 0; // setup buffer to position 0 (start)
            }
            else
            {
                bufPos = 0; // index 0 is already after the file, thus Pos = 0 is invalid
            }
            
            if (bufLen == fileLen && stream.CanSeek)
            {
                Close();
            }
        }
        
        protected StreamBuffer(StreamBuffer b)
        { // called in UTF8Buffer constructor
            buf = b.buf;
            bufStart = b.bufStart;
            bufLen = b.bufLen;
            fileLen = b.fileLen;
            bufPos = b.bufPos;
            stream = b.stream;
            
            // keep destructor from closing the stream
            b.stream = null;
            isUserStream = b.isUserStream;
        }
    
        ~StreamBuffer()
        {
            Close();
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
            else if (stream != null && !stream.CanSeek && ReadNextStreamChunk() > 0)
            {
                return buf[bufPos++];
            }
            else
            {
                return EOF;
            }
        }
    
        public override int Peek()
        {
            int curPos = Pos;
            int ch = Read();
            Pos = curPos;
            return ch;
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
        
        protected override void Close()
        {
            if (!isUserStream && stream != null)
            {
                stream.Close();
                stream = null;
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
                byte[] newBuf = new byte[bufLen * 2];
                Array.Copy(buf, newBuf, bufLen);
                buf = newBuf;
                free = bufLen;
            }
            
            int read = stream.Read(buf, bufLen, free);
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

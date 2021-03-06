// <copyright file="Logger.cs" company="IrcShark Team">
// Copyright (C) 2009 IrcShark Team
// </copyright>
// <author>$Author$</author>
// <date>$LastChangedDate$</date>
// <summary>Contains the Logger class.</summary>

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
namespace IrcShark
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// This delegate is used by the Logger.LoggedMessage event.
    /// </summary>
    /// <param name="sender">The Logger sending raising this event.</param>
    /// <param name="msg">The logged message.</param>
    public delegate void LoggedMessageEventHandler(object sender, LogMessage msg);
    
    /// <summary>
    /// The Logger class is the entrypoint to the logging system of IrcShark.
    /// </summary>
    /// <remarks>
    /// Logging a message is quite easy: simply call the Log method with the give <see cref="IrcShark.LogMessage"/>.
    /// The message is then send to all log writers by fireing the LoggedMessage event.
    /// </remarks>
    public class Logger : IDisposable
    {
        /// <summary>
        /// The core channel specifier.
        /// </summary>
        public const string CoreChannel = "Core";
        
        /// <summary>
        /// Saves if the Logger is running or not.
        /// </summary>
        private bool running;
        
        /// <summary>
        /// Holds the instance of the IrcSharkApplication, this Logger is used for.
        /// </summary>
        private IrcSharkApplication application;
        
        /// <summary>
        /// Queue for the logmessages.
        /// </summary>
        private Queue<LogMessage> logMessageQueue;
        
        /// <summary>
        /// AutoResetEvent for <see cref="logThread"/>.
        /// </summary>
        private AutoResetEvent logAutoResetEvent;
        
        /// <summary>
        /// The thread work on <see cref="logQuene"/>.
        /// </summary>
        private Thread logThread;
        
        /// <summary>
        /// Initializes a new instance of the Logger class.
        /// </summary>
        /// <param name="app">
        /// The <see cref="IrcSharkApplication"/>, this Logger logs messages for.
        /// </param>
        public Logger(IrcSharkApplication app)
        {
            application = app;
            logAutoResetEvent = new AutoResetEvent(false);
            logMessageQueue = new Queue<LogMessage>();
            logThread = new Thread(MessageWatcher);
            running = true;
        }
        
        /// <summary>
        /// The LoggedMessage event is fired when anyone logs a new message. Feel
        /// free to register your own event handler here to get all log messages of the system.
        /// </summary>
        public event LoggedMessageEventHandler LoggedMessage;
        
        /// <summary>
        /// Logs a new message to the logging system.
        /// </summary>
        /// <param name="msg">
        /// The <see cref="LogMessage"/> to log.
        /// </param>
        public void Log(LogMessage msg)
        {
            logMessageQueue.Enqueue(msg);
            if (logThread.ThreadState == ThreadState.Unstarted)
            {
                if (application.Settings == null)
                {
                    return;
                }
                
                logThread.Start();
            }
            
            if (logMessageQueue.Count > 0) 
            {
                logAutoResetEvent.Set();
            }
        }
        
        /// <summary>
        /// Logs a debug message to the given channel.
        /// </summary>
        /// <param name="channel">The channel to log to.</param>
        /// <param name="id">The id of the message to log.</param>
        /// <param name="message">The text message to log.</param>
        public void Debug(string channel, int id, string message)
        {
            Log(new LogMessage(channel, id, LogLevel.Debug, message));
        }
        
        /// <summary>
        /// Logs a parameterized debug message to the given channel.
        /// </summary>
        /// <param name="channel">The channel to log to.</param>
        /// <param name="id">The id of the message to log.</param>
        /// <param name="message">The parameterized text message to log.</param>
        /// <param name="args">The format parameters for the parameterized message.</param>
        public void Debug(string channel, int id, string message, params string[] args)
        {
            Log(new LogMessage(channel, id, LogLevel.Debug, message, args));
        }
        
        /// <summary>
        /// Logs an info message to the given channel.
        /// </summary>
        /// <param name="channel">The channel to log to.</param>
        /// <param name="id">The id of the message to log.</param>
        /// <param name="message">The text message to log.</param>
        public void Info(string channel, int id, string message)
        {
            Log(new LogMessage(channel, id, message));
        }
        
        /// <summary>
        /// Logs a parameterized info message to the given channel.
        /// </summary>
        /// <param name="channel">The channel to log to.</param>
        /// <param name="id">The id of the message to log.</param>
        /// <param name="message">The parameterized text message to log.</param>
        /// <param name="args">The format parameters for the parameterized message.</param>
        public void Info(string channel, int id, string message, params string[] args)
        {
            Log(new LogMessage(channel, id, LogLevel.Information, message, args));
        }
        
        /// <summary>
        /// Logs a warning message to the given channel.
        /// </summary>
        /// <param name="channel">The channel to log to.</param>
        /// <param name="id">The id of the message to log.</param>
        /// <param name="message">The text message to log.</param>
        public void Warning(string channel, int id, string message)
        {
            Log(new LogMessage(channel, id, LogLevel.Warning, message));
        }
        
        /// <summary>
        /// Logs a parameterized warning message to the given channel.
        /// </summary>
        /// <param name="channel">The channel to log to.</param>
        /// <param name="id">The id of the message to log.</param>
        /// <param name="message">The parameterized text message to log.</param>
        /// <param name="args">The format parameters for the parameterized message.</param>
        public void Warning(string channel, int id, string message, params string[] args)
        {
            Log(new LogMessage(channel, id, LogLevel.Warning, message, args));
        }
        
        /// <summary>
        /// Logs an error message to the given channel.
        /// </summary>
        /// <param name="channel">The channel to log to.</param>
        /// <param name="id">The id of the message to log.</param>
        /// <param name="message">The text message to log.</param>
        public void Error(string channel, int id, string message)
        {
            Log(new LogMessage(channel, id, LogLevel.Error, message));
        }
        
        /// <summary>
        /// Logs a parameterized error message to the given channel.
        /// </summary>
        /// <param name="channel">The channel to log to.</param>
        /// <param name="id">The id of the message to log.</param>
        /// <param name="message">The parameterized text message to log.</param>
        /// <param name="args">The format parameters for the parameterized message.</param>
        public void Error(string channel, int id, string message, params string[] args)
        {
            Log(new LogMessage(channel, id, LogLevel.Error, message, args));
        }
        
        /// <summary>
        /// Disposes the Logger instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
                
        /// <summary>
        /// Disposes the Logger instance.
        /// </summary>
        /// <param name="disposed">If true, the managed resources are disposed too.</param>
        protected virtual void Dispose(bool disposed)
        {
            if (disposed) 
            {
                running = false;
                logAutoResetEvent.Set();
                logThread.Join();
                logAutoResetEvent.Close();
            }
        }
        
        /// <summary>
        /// Method called of <see cref="logThread"/>.
        /// </summary>
        private void MessageWatcher()
        {
            while (running)
            {
                if (logMessageQueue.Count == 0)
                {
                    logAutoResetEvent.WaitOne();
                }
                
                while (logMessageQueue.Count > 0)
                {
                    LogMessage msg = logMessageQueue.Dequeue();
                
                    if (LoggedMessage != null && msg != null)
                    {
                        LoggedMessage(this, msg);
                    }
                }
            }
        }
    }
}

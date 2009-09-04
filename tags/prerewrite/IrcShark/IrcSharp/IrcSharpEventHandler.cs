// $Id$
// 
// Note:
// 
// Copyright (C) 2009 Full Name
//  
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

namespace IrcSharp
{
    #region "IrcClient EventHandler"
    public delegate void ConnectEventHandler(object sender, ConnectEventArgs e);
    public delegate void LoginEventHandler(object sender, LoginEventArgs e);
    public delegate void LineReceivedEventHandler(object sender, LineReceivedEventArgs e);
    public delegate void PingReceivedEventHandler(object sender, PingReceivedEventArgs e);
    public delegate void JoinReceivedEventHandler(object sender, JoinReceivedEventArgs e);
    public delegate void PartReceivedEventHandler(object sender, PartReceivedEventArgs e);
    public delegate void QuitReceivedEventHandler(object sender, QuitReceivedEventArgs e);
    public delegate void NickChangeReceivedEventHandler(object sender, NickChangeReceivedEventArgs e);
    public delegate void ModeReceivedEventHandler(object sender, ModeReceivedEventArgs e);
    public delegate void NoticeReceivedEventHandler(object sender, NoticeReceivedEventArgs e);
    public delegate void PrivateMessageReceivedEventHandler(object sender, PrivateMessageReceivedEventArgs e);
    public delegate void NumericReceivedEventHandler(object sender, NumericReceivedEventArgs e);
    public delegate void KickReceivedEventHandler(object sender, KickReceivedEventArgs e);
    public delegate void ErrorEventHandler(object sender, ErrorEventArgs e);
    #endregion
}
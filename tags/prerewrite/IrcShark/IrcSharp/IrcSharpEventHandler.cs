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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IrcSharp;
using IrcShark;

namespace IrcCloneShark
{
    public partial class StatusWindow : IrcCloneShark.BaseWindow
    {
        private MotdListener MotdValue;

        public StatusWindow()
        {
            InitializeComponent();
        }

        public StatusWindow(GUIIrcConnection baseCon) : base(baseCon)
        {
            InitializeComponent();
            AssociatedConnection.BaseConnection.PingReceived += new PingReceivedEventHandler(Client_OnPing);
            AssociatedConnection.BaseConnection.ModeReceived += new ModeReceivedEventHandler(Client_ModeReceived);
            MotdValue = new MotdListener(AssociatedConnection.BaseConnection);
            MotdValue.MotdEnd += new MotdEndEventHandler(MotdValue_MotdEnd);
            Input += new InputEventHandler(StatusWindow_Input);
            Disposed += new EventHandler(StatusWindow_Disposed);
        }

        void Client_ModeReceived(Object sender, ModeReceivedEventArgs e)
        {
            IrcClient client = (IrcClient)sender;
            if (e.Aim != client.CurrentNick) return;
            StringBuilder modeLine = new StringBuilder();
            for (int i = 1; i < e.BaseLine.Parameters.Length; i++)
            {
                modeLine.Append(e.BaseLine.Parameters[i]);
                modeLine.Append(' ');
            }
            AddLine(String.Format("{0} sets mode: {1}", e.Setter, modeLine));
        }

        void StatusWindow_Disposed(Object sender, EventArgs e)
        {
            MotdValue.MotdEnd -= new MotdEndEventHandler(MotdValue_MotdEnd);
            MotdValue = null;
        }

        void StatusWindow_Input(BaseWindow sender, InputEventArgs args)
        {
            if (AssociatedConnection.BaseConnection.IsConnected)
                AssociatedConnection.BaseConnection.SendLine(args.Line);
        }

        void MotdValue_MotdEnd(Object sender, MotdEndEventArgs args)
        {
            foreach (IrcLine line in args.MotdLines)
            {
                AddLine(line.Parameters[line.Parameters.Length - 1]);
            }
        }

        void Client_OnPing(Object sender, IrcSharp.PingReceivedEventArgs e)
        {
            AddLine("PING? PONG!");
        }

        private void StatusWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall) return;
            if (ParentForm != null && ParentForm.Connections.Length <= 1) e.Cancel = true;
        }
    }
}

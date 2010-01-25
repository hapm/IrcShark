using System;
using System.Collections.Generic;
using System.Text;
using IrcShark.Extensions;
using IrcShark;
using IrcSharp;

using System.Windows.Forms;

namespace IrcCloneShark
{
    public class IrcCloneSharkExtension : Extension
    {
        private IrcSharkApplication IrcSharkApp;
        private MainForm MainFormValue;

        public IrcCloneSharkExtension(IrcSharkApplication sharkApp, ExtensionInfo myInfo)
            : base("IrcShark GUI (mIRC-Clone)", myInfo)
        {
            IrcSharkApp = sharkApp;
            
            IrcSharkApp.ShowGUI = false;
			Console.Out.WriteLine("IrcCloneShark loaded!");
            MainFormValue = new MainForm(this);
            MainFormValue.Show();
        }

        public IrcSharkApplication IrcShark
        {
            get { return IrcSharkApp; }
        }

        public MainForm MainForm
        {
            get { return MainFormValue; }
        }
    }
}

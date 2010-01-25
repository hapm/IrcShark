using System;
using System.Collections.Generic;
using System.Windows.Forms;
using IrcShark;

namespace IrcSharkStarter
{
    static class Program
    {
        private static IrcSharkApplication app;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            app = IrcSharkApplication.Instance;
            app.Start();
            Application.Run();
        }
    }
}

using System;
using System.Windows.Forms;
using System.Drawing;
using IrcShark.Extensions;

namespace IrcShark
{
    public partial class MainForm : Form
    {
        private IrcSharkApplication AppValue;
        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(IrcSharkApplication app)
        {
            InitializeComponent();
            AppValue = app;
        }

        private void ExitTrayItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ConfigurationTrayItem_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized) WindowState = FormWindowState.Normal;
            if (!Visible) Visible = true;
            Activate();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Visible = false;
                e.Cancel = true;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadSettingTabs();
			IrcSharkIcon.Visible = true;
        }

        private void LoadSettingTabs()
        {
            SettingTabs.TabPages.Clear();
            foreach (SettingPanel setting in AppValue.SettingPanels)
            {
                TabPage tab = new TabPage(setting.Text);
                tab.Controls.Add(setting);
                SettingTabs.TabPages.Add(tab);
            }
        }

        public bool ShowTrayIcon
        {
            get { return IrcSharkIcon.Visible; }
            set { IrcSharkIcon.Visible = value; }
        }
    }
}

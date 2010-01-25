using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace IrcShark
{
    /// <summary>
    /// A panel to represent settings over the IrcShark gui.
    /// </summary>
    /// <remarks>You can write your own setting panels by deriving them from this class. To add your panal to the gui see <see cref="IrcSharkApplication.SettingPanels"/></remarks>
    public partial class SettingPanel : UserControl
    {
        private IrcSharkApplication IrcSharkValue;
        private String TextValue;

        public SettingPanel()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        public SettingPanel(IrcSharkApplication app)
        {
            IrcSharkValue = app;
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        /// <summary>
        /// The IrcSharkApplication, this panel was added to.
        /// </summary>
        public IrcSharkApplication IrcShark
        {
            get { return IrcSharkValue; }
        }

        /// <summary>
        /// The text to be shown as a short descriptor for this panel.
        /// </summary>
        /// <value>a short text</value>
        [Browsable(true)][Category("Appearance")]
        public override String Text
        {
            get { return TextValue; }
            set { TextValue = value; }
        }
    }
}

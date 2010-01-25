using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IrcShark.Extensions;

namespace IrcShark
{
    /// <summary>
    /// A panel to configure the extension manager.
    /// </summary>
    public partial class ExtensionManagerPanel : SettingPanel, IDisposable
    {
        ExtensionManager Extensions;

        public ExtensionManagerPanel()
        {
            InitializeComponent();
            Text = "Extensions";
        }

        public ExtensionManagerPanel(IrcSharkApplication app)
            : base(app)
        {
            InitializeComponent();
            Text = "Extensions";
            IrcShark.Extensions.StatusChanged += new ExtensionManager.StatusChangedEventHandler(Extensions_StatusChanged);
        }

        void Extensions_StatusChanged(ExtensionManager sender, StatusChangedEventArgs args)
        {
            foreach (ListViewItem item in ExtensionsList.Items)
            {
                if (item.Tag is ExtensionInfo)
                {
                    ExtensionInfo ext = (ExtensionInfo)item.Tag;
                    if (ext == args.Extension)
                    {
                        switch (args.Status)
                        {
                            case ExtensionStates.Available:
                                item.SubItems[3].Text = "not loaded";
                                break;

                            case ExtensionStates.Loaded:
                                item.SubItems[3].Text = "loaded";
                                break;

                            case ExtensionStates.MarkedForUnload:
                                item.SubItems[3].Text = "wait for unload";
                                break;
                        }
                    }
                }
            }
        }

        private void LoadExtensionList()
        {
            ExtensionsList.Items.Clear();
            ListViewItem lvi;
            foreach (ExtensionInfo extInfo in IrcShark.Extensions.AviableExtensions)
            {
                String status;
                if (IrcShark.Extensions.IsLoaded(extInfo) && !IrcShark.Extensions.IsMarkedForUnload(extInfo)) status = "loaded";
                else if (IrcShark.Extensions.IsLoaded(extInfo) && IrcShark.Extensions.IsMarkedForUnload(extInfo)) status = "wait for unload";
                else status = "not loaded";
                lvi = new ListViewItem(new String[] { extInfo.TypeName, "0", extInfo.SourceAssembly, status });
                lvi.Tag = extInfo;
                ExtensionsList.Items.Add(lvi);
            }
        }

        private void LoadExtensionItem_Click(object sender, EventArgs e)
        {
            if (!(ExtensionsList.SelectedItems[0].Tag is ExtensionInfo))
            {
                return;
            }
            ExtensionInfo extInfo = (ExtensionInfo)ExtensionsList.SelectedItems[0].Tag;
            IrcShark.Extensions.Load(extInfo);
        }

        private void ExtensionContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ExtensionsList.SelectedItems.Count == 0)
            {
                e.Cancel = true;
                return;
            }
            if (!(ExtensionsList.SelectedItems[0].Tag is ExtensionInfo))
            {
                e.Cancel = true;
                return;
            }
            ExtensionInfo extInfo = (ExtensionInfo)ExtensionsList.SelectedItems[0].Tag;
            LoadExtensionItem.Enabled = !IrcShark.Extensions.IsLoaded(extInfo) || IrcShark.Extensions.IsMarkedForUnload(extInfo);
            UnloadExtensionItem.Enabled = IrcShark.Extensions.IsLoaded(extInfo) && !IrcShark.Extensions.IsMarkedForUnload(extInfo);
        }

        private void ExtensionManagerPanel_Load(object sender, EventArgs e)
        {
            LoadExtensionList();
        }

        private void UnloadExtensionItem_Click(object sender, EventArgs e)
        {
            if (!(ExtensionsList.SelectedItems[0].Tag is ExtensionInfo))
            {
                return;
            }
            ExtensionInfo extInfo = (ExtensionInfo)ExtensionsList.SelectedItems[0].Tag;
            IrcShark.Extensions.Unload(extInfo);
        }

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            base.Dispose();
            IrcShark.Extensions.StatusChanged -= new ExtensionManager.StatusChangedEventHandler(Extensions_StatusChanged);
        }

        #endregion
    }
}

namespace IrcShark
{
    partial class NetworkManagerPanel
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetworkManagerPanel));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ServerList = new System.Windows.Forms.TreeView();
            this.NetworkListPopup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewNetworkItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteNetworkItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.NewServerItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteServerItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingTabs = new System.Windows.Forms.TabControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.NetworkListPopup.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AccessibleDescription = null;
            this.tableLayoutPanel1.AccessibleName = null;
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackgroundImage = null;
            this.tableLayoutPanel1.Controls.Add(this.ServerList, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.SettingTabs, 1, 0);
            this.tableLayoutPanel1.Font = null;
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // ServerList
            // 
            this.ServerList.AccessibleDescription = null;
            this.ServerList.AccessibleName = null;
            resources.ApplyResources(this.ServerList, "ServerList");
            this.ServerList.BackgroundImage = null;
            this.ServerList.ContextMenuStrip = this.NetworkListPopup;
            this.ServerList.Font = null;
            this.ServerList.HideSelection = false;
            this.ServerList.LabelEdit = true;
            this.ServerList.Name = "ServerList";
            this.ServerList.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.ServerList_AfterLabelEdit);
            this.ServerList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ServerList_AfterSelect);
            this.ServerList.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.ServerList_BeforeLabelEdit);
            // 
            // NetworkListPopup
            // 
            this.NetworkListPopup.AccessibleDescription = null;
            this.NetworkListPopup.AccessibleName = null;
            resources.ApplyResources(this.NetworkListPopup, "NetworkListPopup");
            this.NetworkListPopup.BackgroundImage = null;
            this.NetworkListPopup.Font = null;
            this.NetworkListPopup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewNetworkItem,
            this.DeleteNetworkItem,
            this.toolStripMenuItem1,
            this.NewServerItem,
            this.DeleteServerItem});
            this.NetworkListPopup.Name = "ServerListPopup";
            this.NetworkListPopup.Opening += new System.ComponentModel.CancelEventHandler(this.ServerListPopup_Opening);
            // 
            // NewNetworkItem
            // 
            this.NewNetworkItem.AccessibleDescription = null;
            this.NewNetworkItem.AccessibleName = null;
            resources.ApplyResources(this.NewNetworkItem, "NewNetworkItem");
            this.NewNetworkItem.BackgroundImage = null;
            this.NewNetworkItem.Name = "NewNetworkItem";
            this.NewNetworkItem.ShortcutKeyDisplayString = null;
            this.NewNetworkItem.Click += new System.EventHandler(this.NewNetworkItem_Click);
            // 
            // DeleteNetworkItem
            // 
            this.DeleteNetworkItem.AccessibleDescription = null;
            this.DeleteNetworkItem.AccessibleName = null;
            resources.ApplyResources(this.DeleteNetworkItem, "DeleteNetworkItem");
            this.DeleteNetworkItem.BackgroundImage = null;
            this.DeleteNetworkItem.Name = "DeleteNetworkItem";
            this.DeleteNetworkItem.ShortcutKeyDisplayString = null;
            this.DeleteNetworkItem.Click += new System.EventHandler(this.DeleteNetworkItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.AccessibleDescription = null;
            this.toolStripMenuItem1.AccessibleName = null;
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            // 
            // NewServerItem
            // 
            this.NewServerItem.AccessibleDescription = null;
            this.NewServerItem.AccessibleName = null;
            resources.ApplyResources(this.NewServerItem, "NewServerItem");
            this.NewServerItem.BackgroundImage = null;
            this.NewServerItem.Name = "NewServerItem";
            this.NewServerItem.ShortcutKeyDisplayString = null;
            this.NewServerItem.Click += new System.EventHandler(this.NewServerItem_Click);
            // 
            // DeleteServerItem
            // 
            this.DeleteServerItem.AccessibleDescription = null;
            this.DeleteServerItem.AccessibleName = null;
            resources.ApplyResources(this.DeleteServerItem, "DeleteServerItem");
            this.DeleteServerItem.BackgroundImage = null;
            this.DeleteServerItem.Name = "DeleteServerItem";
            this.DeleteServerItem.ShortcutKeyDisplayString = null;
            this.DeleteServerItem.Click += new System.EventHandler(this.DeleteServerItem_Click);
            // 
            // SettingTabs
            // 
            this.SettingTabs.AccessibleDescription = null;
            this.SettingTabs.AccessibleName = null;
            resources.ApplyResources(this.SettingTabs, "SettingTabs");
            this.SettingTabs.BackgroundImage = null;
            this.SettingTabs.Font = null;
            this.SettingTabs.Name = "SettingTabs";
            this.SettingTabs.SelectedIndex = 0;
            // 
            // NetworkManagerPanel
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Font = null;
            this.Name = "NetworkManagerPanel";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.NetworkListPopup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView ServerList;
        private System.Windows.Forms.TabControl SettingTabs;
        private System.Windows.Forms.ContextMenuStrip NetworkListPopup;
        private System.Windows.Forms.ToolStripMenuItem NewServerItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteServerItem;
        private System.Windows.Forms.ToolStripMenuItem NewNetworkItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteNetworkItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    }
}

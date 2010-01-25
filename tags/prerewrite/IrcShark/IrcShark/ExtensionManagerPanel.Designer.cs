namespace IrcShark
{
    partial class ExtensionManagerPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtensionManagerPanel));
            this.ExtensionContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.LoadExtensionItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UnloadExtensionItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.ExtensionsList = new System.Windows.Forms.ListView();
            this.NameHeader = new System.Windows.Forms.ColumnHeader();
            this.VersionHeader = new System.Windows.Forms.ColumnHeader();
            this.AssemblyHeader = new System.Windows.Forms.ColumnHeader();
            this.StatusHeader = new System.Windows.Forms.ColumnHeader();
            this.NotificationPanel = new System.Windows.Forms.Panel();
            this.ExtensionContextMenu.SuspendLayout();
            this.MainLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // ExtensionContextMenu
            // 
            this.ExtensionContextMenu.AccessibleDescription = null;
            this.ExtensionContextMenu.AccessibleName = null;
            resources.ApplyResources(this.ExtensionContextMenu, "ExtensionContextMenu");
            this.ExtensionContextMenu.BackgroundImage = null;
            this.ExtensionContextMenu.Font = null;
            this.ExtensionContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadExtensionItem,
            this.UnloadExtensionItem});
            this.ExtensionContextMenu.Name = "ExtensionContextMenu";
            this.ExtensionContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ExtensionContextMenu_Opening);
            // 
            // LoadExtensionItem
            // 
            this.LoadExtensionItem.AccessibleDescription = null;
            this.LoadExtensionItem.AccessibleName = null;
            resources.ApplyResources(this.LoadExtensionItem, "LoadExtensionItem");
            this.LoadExtensionItem.BackgroundImage = null;
            this.LoadExtensionItem.Name = "LoadExtensionItem";
            this.LoadExtensionItem.ShortcutKeyDisplayString = null;
            this.LoadExtensionItem.Click += new System.EventHandler(this.LoadExtensionItem_Click);
            // 
            // UnloadExtensionItem
            // 
            this.UnloadExtensionItem.AccessibleDescription = null;
            this.UnloadExtensionItem.AccessibleName = null;
            resources.ApplyResources(this.UnloadExtensionItem, "UnloadExtensionItem");
            this.UnloadExtensionItem.BackgroundImage = null;
            this.UnloadExtensionItem.Name = "UnloadExtensionItem";
            this.UnloadExtensionItem.ShortcutKeyDisplayString = null;
            this.UnloadExtensionItem.Click += new System.EventHandler(this.UnloadExtensionItem_Click);
            // 
            // MainLayout
            // 
            this.MainLayout.AccessibleDescription = null;
            this.MainLayout.AccessibleName = null;
            resources.ApplyResources(this.MainLayout, "MainLayout");
            this.MainLayout.BackgroundImage = null;
            this.MainLayout.Controls.Add(this.ExtensionsList, 0, 1);
            this.MainLayout.Controls.Add(this.NotificationPanel, 0, 0);
            this.MainLayout.Font = null;
            this.MainLayout.Name = "MainLayout";
            // 
            // ExtensionsList
            // 
            this.ExtensionsList.AccessibleDescription = null;
            this.ExtensionsList.AccessibleName = null;
            resources.ApplyResources(this.ExtensionsList, "ExtensionsList");
            this.ExtensionsList.BackgroundImage = null;
            this.ExtensionsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameHeader,
            this.VersionHeader,
            this.AssemblyHeader,
            this.StatusHeader});
            this.ExtensionsList.ContextMenuStrip = this.ExtensionContextMenu;
            this.ExtensionsList.Font = null;
            this.ExtensionsList.FullRowSelect = true;
            this.ExtensionsList.HideSelection = false;
            this.ExtensionsList.MultiSelect = false;
            this.ExtensionsList.Name = "ExtensionsList";
            this.ExtensionsList.ShowGroups = false;
            this.ExtensionsList.UseCompatibleStateImageBehavior = false;
            this.ExtensionsList.View = System.Windows.Forms.View.Details;
            // 
            // NameHeader
            // 
            resources.ApplyResources(this.NameHeader, "NameHeader");
            // 
            // VersionHeader
            // 
            resources.ApplyResources(this.VersionHeader, "VersionHeader");
            // 
            // AssemblyHeader
            // 
            resources.ApplyResources(this.AssemblyHeader, "AssemblyHeader");
            // 
            // StatusHeader
            // 
            resources.ApplyResources(this.StatusHeader, "StatusHeader");
            // 
            // NotificationPanel
            // 
            this.NotificationPanel.AccessibleDescription = null;
            this.NotificationPanel.AccessibleName = null;
            resources.ApplyResources(this.NotificationPanel, "NotificationPanel");
            this.NotificationPanel.BackgroundImage = null;
            this.NotificationPanel.Font = null;
            this.NotificationPanel.Name = "NotificationPanel";
            // 
            // ExtensionManagerPanel
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = null;
            this.Controls.Add(this.MainLayout);
            this.DoubleBuffered = true;
            this.Font = null;
            this.Name = "ExtensionManagerPanel";
            this.Load += new System.EventHandler(this.ExtensionManagerPanel_Load);
            this.ExtensionContextMenu.ResumeLayout(false);
            this.MainLayout.ResumeLayout(false);
            this.MainLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip ExtensionContextMenu;
        private System.Windows.Forms.ToolStripMenuItem LoadExtensionItem;
        private System.Windows.Forms.ToolStripMenuItem UnloadExtensionItem;
        private System.Windows.Forms.TableLayoutPanel MainLayout;
        private System.Windows.Forms.ListView ExtensionsList;
        private System.Windows.Forms.ColumnHeader NameHeader;
        private System.Windows.Forms.ColumnHeader VersionHeader;
        private System.Windows.Forms.ColumnHeader AssemblyHeader;
        private System.Windows.Forms.ColumnHeader StatusHeader;
        private System.Windows.Forms.Panel NotificationPanel;
    }
}

namespace IrcShark
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.IrcSharkIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayPopupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ConfigurationTrayItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitTrayItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionGroupsView = new System.Windows.Forms.ListView();
            this.SettingTabs = new System.Windows.Forms.TabControl();
            this.TrayPopupMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // IrcSharkIcon
            // 
            resources.ApplyResources(this.IrcSharkIcon, "IrcSharkIcon");
            this.IrcSharkIcon.ContextMenuStrip = this.TrayPopupMenu;
            // 
            // TrayPopupMenu
            // 
            this.TrayPopupMenu.AccessibleDescription = null;
            this.TrayPopupMenu.AccessibleName = null;
            resources.ApplyResources(this.TrayPopupMenu, "TrayPopupMenu");
            this.TrayPopupMenu.BackgroundImage = null;
            this.TrayPopupMenu.Font = null;
            this.TrayPopupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConfigurationTrayItem,
            this.ExitTrayItem});
            this.TrayPopupMenu.Name = "TrayPopupMenu";
            // 
            // ConfigurationTrayItem
            // 
            this.ConfigurationTrayItem.AccessibleDescription = null;
            this.ConfigurationTrayItem.AccessibleName = null;
            resources.ApplyResources(this.ConfigurationTrayItem, "ConfigurationTrayItem");
            this.ConfigurationTrayItem.BackgroundImage = null;
            this.ConfigurationTrayItem.Name = "ConfigurationTrayItem";
            this.ConfigurationTrayItem.ShortcutKeyDisplayString = null;
            this.ConfigurationTrayItem.Click += new System.EventHandler(this.ConfigurationTrayItem_Click);
            // 
            // ExitTrayItem
            // 
            this.ExitTrayItem.AccessibleDescription = null;
            this.ExitTrayItem.AccessibleName = null;
            resources.ApplyResources(this.ExitTrayItem, "ExitTrayItem");
            this.ExitTrayItem.BackgroundImage = null;
            this.ExitTrayItem.Name = "ExitTrayItem";
            this.ExitTrayItem.ShortcutKeyDisplayString = null;
            this.ExitTrayItem.Click += new System.EventHandler(this.ExitTrayItem_Click);
            // 
            // OptionGroupsView
            // 
            this.OptionGroupsView.AccessibleDescription = null;
            this.OptionGroupsView.AccessibleName = null;
            this.OptionGroupsView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            resources.ApplyResources(this.OptionGroupsView, "OptionGroupsView");
            this.OptionGroupsView.BackgroundImage = null;
            this.OptionGroupsView.Font = null;
            this.OptionGroupsView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.OptionGroupsView.HideSelection = false;
            this.OptionGroupsView.MultiSelect = false;
            this.OptionGroupsView.Name = "OptionGroupsView";
            this.OptionGroupsView.ShowGroups = false;
            this.OptionGroupsView.UseCompatibleStateImageBehavior = false;
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
            // MainForm
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.SettingTabs);
            this.Controls.Add(this.OptionGroupsView);
            this.DoubleBuffered = true;
            this.Font = null;
            this.Icon = null;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.TrayPopupMenu.ResumeLayout(false);
            this.ResumeLayout(false);

            }

        #endregion

        private System.Windows.Forms.NotifyIcon IrcSharkIcon;
        private System.Windows.Forms.ContextMenuStrip TrayPopupMenu;
        private System.Windows.Forms.ToolStripMenuItem ExitTrayItem;
        private System.Windows.Forms.ToolStripMenuItem ConfigurationTrayItem;
        private System.Windows.Forms.ListView OptionGroupsView;
        private System.Windows.Forms.TabControl SettingTabs;

    }
}
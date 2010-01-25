using IrcShark;

namespace IrcCloneShark
{
    partial class NetworkManagerForm
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
            this.NetManager = new IrcShark.NetworkManagerPanel();
            this.SuspendLayout();
            // 
            // NetManager
            // 
            this.NetManager.BoundedNetworkManager = null;
            this.NetManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NetManager.Location = new System.Drawing.Point(0, 0);
            this.NetManager.Name = "NetManager";
            this.NetManager.Size = new System.Drawing.Size(662, 396);
            this.NetManager.TabIndex = 0;
            this.NetManager.ServerSelected += new IrcShark.ServerSelectedEventHandler(this.SrvManager_ServerSelected);
            // 
            // NetworkManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 396);
            this.Controls.Add(this.NetManager);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NetworkManagerForm";
            this.ShowInTaskbar = false;
            this.Text = "Server-Manager";
            this.Load += new System.EventHandler(this.NetworkManagerForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private NetworkManagerPanel NetManager;
    }
}
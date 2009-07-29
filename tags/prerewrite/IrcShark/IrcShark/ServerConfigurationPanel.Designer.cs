namespace IrcShark
{
    partial class ServerConfigurationPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerConfigurationPanel));
            this.AddressLabel = new System.Windows.Forms.Label();
            this.Address = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.Port = new System.Windows.Forms.TextBox();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.SelectServerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AddressLabel
            // 
            this.AddressLabel.AccessibleDescription = null;
            this.AddressLabel.AccessibleName = null;
            resources.ApplyResources(this.AddressLabel, "AddressLabel");
            this.AddressLabel.Font = null;
            this.AddressLabel.Name = "AddressLabel";
            // 
            // Address
            // 
            this.Address.AccessibleDescription = null;
            this.Address.AccessibleName = null;
            resources.ApplyResources(this.Address, "Address");
            this.Address.BackgroundImage = null;
            this.Address.Font = null;
            this.Address.Name = "Address";
            this.Address.TextChanged += new System.EventHandler(this.Address_TextChanged);
            // 
            // PortLabel
            // 
            this.PortLabel.AccessibleDescription = null;
            this.PortLabel.AccessibleName = null;
            resources.ApplyResources(this.PortLabel, "PortLabel");
            this.PortLabel.Font = null;
            this.PortLabel.Name = "PortLabel";
            // 
            // Port
            // 
            this.Port.AccessibleDescription = null;
            this.Port.AccessibleName = null;
            resources.ApplyResources(this.Port, "Port");
            this.Port.BackgroundImage = null;
            this.Port.Font = null;
            this.Port.Name = "Port";
            this.Port.TextChanged += new System.EventHandler(this.Port_TextChanged);
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AccessibleDescription = null;
            this.PasswordLabel.AccessibleName = null;
            resources.ApplyResources(this.PasswordLabel, "PasswordLabel");
            this.PasswordLabel.Font = null;
            this.PasswordLabel.Name = "PasswordLabel";
            // 
            // Password
            // 
            this.Password.AccessibleDescription = null;
            this.Password.AccessibleName = null;
            resources.ApplyResources(this.Password, "Password");
            this.Password.BackgroundImage = null;
            this.Password.Font = null;
            this.Password.Name = "Password";
            this.Password.UseSystemPasswordChar = true;
            this.Password.TextChanged += new System.EventHandler(this.Password_TextChanged);
            // 
            // SelectServerButton
            // 
            this.SelectServerButton.AccessibleDescription = null;
            this.SelectServerButton.AccessibleName = null;
            resources.ApplyResources(this.SelectServerButton, "SelectServerButton");
            this.SelectServerButton.BackgroundImage = null;
            this.SelectServerButton.Font = null;
            this.SelectServerButton.Name = "SelectServerButton";
            this.SelectServerButton.UseVisualStyleBackColor = true;
            this.SelectServerButton.Click += new System.EventHandler(this.SelectServerButton_Click);
            // 
            // ServerConfigurationPanel
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = null;
            this.Controls.Add(this.SelectServerButton);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.PortLabel);
            this.Controls.Add(this.Port);
            this.Controls.Add(this.AddressLabel);
            this.Controls.Add(this.Address);
            this.Font = null;
            this.Name = "ServerConfigurationPanel";
            this.Load += new System.EventHandler(this.ServerConfigurationPanel_Load);
            this.CurrentConfigurationUnitChanged += new System.EventHandler(this.ServerConfigurationPanel_CurrentConfigurationUnitChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AddressLabel;
        private System.Windows.Forms.TextBox Address;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.TextBox Port;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Button SelectServerButton;
    }
}

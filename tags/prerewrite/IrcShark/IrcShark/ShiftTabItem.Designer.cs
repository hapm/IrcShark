namespace IrcShark
{
    partial class ShiftTabItem
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DrawingContainer = new System.Windows.Forms.Panel();
            this.ActivateButton = new System.Windows.Forms.Button();
            // 
            // DrawingContainer
            // 
            this.DrawingContainer.Location = new System.Drawing.Point(0, 0);
            this.DrawingContainer.Name = "DrawingContainer";
            this.DrawingContainer.Size = new System.Drawing.Size(200, 100);
            this.DrawingContainer.TabIndex = 0;
            // 
            // ActivateButton
            // 
            this.ActivateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ActivateButton.Location = new System.Drawing.Point(0, 0);
            this.ActivateButton.Name = "ActivateButton";
            this.ActivateButton.Size = new System.Drawing.Size(75, 23);
            this.ActivateButton.TabIndex = 0;
            this.ActivateButton.UseVisualStyleBackColor = true;

        }

        #endregion

        private System.Windows.Forms.Panel DrawingContainer;
        private System.Windows.Forms.Button ActivateButton;
    }
}

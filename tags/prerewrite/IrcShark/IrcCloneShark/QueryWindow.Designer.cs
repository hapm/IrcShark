namespace IrcCloneShark
{
    partial class QueryWindow
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
            this.SuspendLayout();
            // 
            // QueryWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(1123, 626);
            this.HasInputBox = true;
            this.HasSideList = false;
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "QueryWindow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.QueryWindow_FormClosed);
            this.Input += new IrcCloneShark.BaseWindow.InputEventHandler(this.QueryWindow_Input);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}

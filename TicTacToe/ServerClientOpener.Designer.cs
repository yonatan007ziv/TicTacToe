namespace TicTacToe
{
    partial class ServerClientOpener
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
            FormClosing += CloseAll;
            this.serverButton = new System.Windows.Forms.Button();
            this.clientButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serverButton
            // 
            this.serverButton.Location = new System.Drawing.Point(12, 12);
            this.serverButton.Name = "serverButton";
            this.serverButton.Size = new System.Drawing.Size(125, 25);
            this.serverButton.TabIndex = 0;
            this.serverButton.Text = "Server";
            this.serverButton.UseVisualStyleBackColor = true;
            this.serverButton.Click += new System.EventHandler(this.openServerButton);
            // 
            // clientButton
            // 
            this.clientButton.Location = new System.Drawing.Point(143, 12);
            this.clientButton.Name = "clientButton";
            this.clientButton.Size = new System.Drawing.Size(125, 25);
            this.clientButton.TabIndex = 1;
            this.clientButton.Text = "Client";
            this.clientButton.UseVisualStyleBackColor = true;
            this.clientButton.Click += new System.EventHandler(this.openClientButton);
            // 
            // ServerClientOpener
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 46);
            this.Controls.Add(this.clientButton);
            this.Controls.Add(this.serverButton);
            this.Name = "ServerClientOpener";
            this.Text = "ServerClientOpener";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button serverButton;
        private System.Windows.Forms.Button clientButton;
    }
}
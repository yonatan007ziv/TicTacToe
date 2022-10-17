namespace TicTacToe
{
    partial class TicTacToeServer
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
            this.createBoardBtn = new System.Windows.Forms.Button();
            this.enterSizeLabel = new System.Windows.Forms.Label();
            this.sizeInput = new System.Windows.Forms.TextBox();
            this.ServerLog = new System.Windows.Forms.RichTextBox();
            this.consoleLabel = new System.Windows.Forms.Label();
            this.portText = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // createBoardBtn
            // 
            this.createBoardBtn.Location = new System.Drawing.Point(366, 370);
            this.createBoardBtn.Name = "createBoardBtn";
            this.createBoardBtn.Size = new System.Drawing.Size(151, 23);
            this.createBoardBtn.TabIndex = 15;
            this.createBoardBtn.Text = "Start Server";
            this.createBoardBtn.UseVisualStyleBackColor = true;
            this.createBoardBtn.Click += new System.EventHandler(this.startServer);
            // 
            // enterSizeLabel
            // 
            this.enterSizeLabel.AutoSize = true;
            this.enterSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.enterSizeLabel.Location = new System.Drawing.Point(12, 402);
            this.enterSizeLabel.Name = "enterSizeLabel";
            this.enterSizeLabel.Size = new System.Drawing.Size(300, 39);
            this.enterSizeLabel.TabIndex = 14;
            this.enterSizeLabel.Text = "Enter Board\'s size:";
            // 
            // sizeInput
            // 
            this.sizeInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F);
            this.sizeInput.Location = new System.Drawing.Point(318, 399);
            this.sizeInput.Name = "sizeInput";
            this.sizeInput.Size = new System.Drawing.Size(34, 45);
            this.sizeInput.TabIndex = 13;
            this.sizeInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.sizeInput.TextChanged += new System.EventHandler(this.OnSizeChanged);
            // 
            // ServerLog
            // 
            this.ServerLog.Location = new System.Drawing.Point(12, 29);
            this.ServerLog.Name = "ServerLog";
            this.ServerLog.ReadOnly = true;
            this.ServerLog.Size = new System.Drawing.Size(769, 335);
            this.ServerLog.TabIndex = 16;
            this.ServerLog.Text = "";
            // 
            // consoleLabel
            // 
            this.consoleLabel.AutoSize = true;
            this.consoleLabel.Location = new System.Drawing.Point(16, 13);
            this.consoleLabel.Name = "consoleLabel";
            this.consoleLabel.Size = new System.Drawing.Size(45, 13);
            this.consoleLabel.TabIndex = 17;
            this.consoleLabel.Text = "Console";
            // 
            // portText
            // 
            this.portText.AutoSize = true;
            this.portText.Location = new System.Drawing.Point(16, 382);
            this.portText.Name = "portText";
            this.portText.Size = new System.Drawing.Size(29, 13);
            this.portText.TabIndex = 19;
            this.portText.Text = "Port:";
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(51, 379);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(100, 20);
            this.portTextBox.TabIndex = 21;
            this.portTextBox.Text = "65500";
            this.portTextBox.TextChanged += new System.EventHandler(this.portTextBoxChanged);
            // 
            // TicTacToeServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.portText);
            this.Controls.Add(this.consoleLabel);
            this.Controls.Add(this.ServerLog);
            this.Controls.Add(this.createBoardBtn);
            this.Controls.Add(this.enterSizeLabel);
            this.Controls.Add(this.sizeInput);
            this.Name = "TicTacToeServer";
            this.Text = "TicTacToeServer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button createBoardBtn;
        private System.Windows.Forms.Label enterSizeLabel;
        private System.Windows.Forms.TextBox sizeInput;
        private System.Windows.Forms.RichTextBox ServerLog;
        private System.Windows.Forms.Label consoleLabel;
        private System.Windows.Forms.Label portText;
        private System.Windows.Forms.TextBox portTextBox;
    }
}
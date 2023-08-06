namespace TicTacToe.Client.Controls
{
    partial class ConnectMenu
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
            connectButton = new Button();
            portInputField = new TextBox();
            ipInputField = new TextBox();
            SuspendLayout();
            // 
            // connectButton
            // 
            connectButton.Location = new Point(9, 58);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(75, 23);
            connectButton.TabIndex = 0;
            connectButton.Text = "Connect";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += OnConnectButton;
            // 
            // portInputField
            // 
            portInputField.Location = new Point(0, 29);
            portInputField.Name = "portInputField";
            portInputField.Size = new Size(100, 23);
            portInputField.TabIndex = 1;
            portInputField.Text = "65500";
            // 
            // ipInputField
            // 
            ipInputField.Location = new Point(0, 0);
            ipInputField.Name = "ipInputField";
            ipInputField.Size = new Size(100, 23);
            ipInputField.TabIndex = 2;
            ipInputField.Text = "127.0.0.1";
            // 
            // ConnectMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(ipInputField);
            Controls.Add(portInputField);
            Controls.Add(connectButton);
            Name = "ConnectMenu";
            Size = new Size(500, 500);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button connectButton;
        private TextBox portInputField;
        private TextBox ipInputField;
    }
}

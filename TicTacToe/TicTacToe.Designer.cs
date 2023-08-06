using System.Windows.Forms;

namespace TicTacToe
{
    partial class TicTacToe
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
            FormClosing += LeaveGameButtonAfter;
            this.currentTurnLabel = new System.Windows.Forms.Label();
            this.leaveGameButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.playerSymbol = new System.Windows.Forms.Label();
            this.winnerLabel = new System.Windows.Forms.Label();
            this.ipBox = new System.Windows.Forms.TextBox();
            this.portBox = new System.Windows.Forms.TextBox();
            this.ipText = new System.Windows.Forms.Label();
            this.portText = new System.Windows.Forms.Label();
            this.resetButton = new System.Windows.Forms.Button();
            this.waitingForPlayerText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // currentTurnLabel
            // 
            this.currentTurnLabel.AutoSize = true;
            this.currentTurnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.currentTurnLabel.Location = new System.Drawing.Point(7, 363);
            this.currentTurnLabel.Name = "currentTurnLabel";
            this.currentTurnLabel.Size = new System.Drawing.Size(0, 26);
            this.currentTurnLabel.TabIndex = 9;
            // 
            // leaveGameButton
            // 
            this.leaveGameButton.Location = new System.Drawing.Point(198, 367);
            this.leaveGameButton.Name = "leaveGameButton";
            this.leaveGameButton.Size = new System.Drawing.Size(125, 23);
            this.leaveGameButton.TabIndex = 13;
            this.leaveGameButton.Text = "Leave Game";
            this.leaveGameButton.UseVisualStyleBackColor = true;
            this.leaveGameButton.Click += new System.EventHandler(this.LeaveGameButton);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(84, 135);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(125, 23);
            this.connectButton.TabIndex = 16;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectToServerAsync);
            // 
            // playerSymbol
            // 
            this.playerSymbol.AutoSize = true;
            this.playerSymbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.playerSymbol.Location = new System.Drawing.Point(7, 337);
            this.playerSymbol.Name = "playerSymbol";
            this.playerSymbol.Size = new System.Drawing.Size(0, 26);
            this.playerSymbol.TabIndex = 20;
            // 
            // winnerLabel
            // 
            this.winnerLabel.AutoSize = true;
            this.winnerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.winnerLabel.Location = new System.Drawing.Point(9, 314);
            this.winnerLabel.Name = "winnerLabel";
            this.winnerLabel.Size = new System.Drawing.Size(0, 17);
            this.winnerLabel.TabIndex = 21;
            // 
            // ipBox
            // 
            this.ipBox.Location = new System.Drawing.Point(104, 74);
            this.ipBox.Name = "ipBox";
            this.ipBox.Size = new System.Drawing.Size(100, 20);
            this.ipBox.TabIndex = 22;
            this.ipBox.TextChanged += new System.EventHandler(this.IpTextChanged);
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(104, 100);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(100, 20);
            this.portBox.TabIndex = 23;
            this.portBox.TextChanged += new System.EventHandler(this.PortTextChanged);
            // 
            // ipText
            // 
            this.ipText.AutoSize = true;
            this.ipText.Location = new System.Drawing.Point(81, 74);
            this.ipText.Name = "ipText";
            this.ipText.Size = new System.Drawing.Size(17, 13);
            this.ipText.TabIndex = 24;
            this.ipText.Text = "IP";
            // 
            // portText
            // 
            this.portText.AutoSize = true;
            this.portText.Location = new System.Drawing.Point(72, 103);
            this.portText.Name = "portText";
            this.portText.Size = new System.Drawing.Size(26, 13);
            this.portText.TabIndex = 25;
            this.portText.Text = "Port";
            // 
            // resetButton
            // 
            this.resetButton.Enabled = false;
            this.resetButton.Location = new System.Drawing.Point(198, 341);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(125, 23);
            this.resetButton.TabIndex = 26;
            this.resetButton.Text = "Reset Game";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.ResetGameButton);
            // 
            // waitingForPlayerText
            // 
            this.waitingForPlayerText.AutoSize = true;
            this.waitingForPlayerText.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.waitingForPlayerText.Location = new System.Drawing.Point(40, 178);
            this.waitingForPlayerText.Name = "waitingForPlayerText";
            this.waitingForPlayerText.Size = new System.Drawing.Size(0, 25);
            this.waitingForPlayerText.TabIndex = 27;
            // 
            // TicTacToe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 398);
            this.Controls.Add(this.waitingForPlayerText);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.portText);
            this.Controls.Add(this.ipText);
            this.Controls.Add(this.portBox);
            this.Controls.Add(this.ipBox);
            this.Controls.Add(this.winnerLabel);
            this.Controls.Add(this.playerSymbol);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.leaveGameButton);
            this.Controls.Add(this.currentTurnLabel);
            this.Name = "TicTacToe";
            this.Text = "Tic Tac Toe";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label currentTurnLabel;
        private System.Windows.Forms.Button leaveGameButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label playerSymbol;
        private System.Windows.Forms.Label winnerLabel;
        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.Label ipText;
        private System.Windows.Forms.Label portText;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Label waitingForPlayerText;
    }
}


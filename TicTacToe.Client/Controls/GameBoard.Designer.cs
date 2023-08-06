namespace TicTacToe.Client.Controls
{
    partial class GameBoard
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
            topLeft = new Button();
            topMiddle = new Button();
            topRight = new Button();
            centerLeft = new Button();
            centerMiddle = new Button();
            centerRight = new Button();
            bottomLeft = new Button();
            bottomMiddle = new Button();
            bottomRight = new Button();
            leaveButton = new Button();
            turnLabel = new Label();
            playerLabel = new Label();
            waitingLabel = new Label();
            warningLabel = new Label();
            winnerLabel = new Label();
            restartButton = new Button();
            SuspendLayout();
            // 
            // topLeft
            // 
            topLeft.Enabled = false;
            topLeft.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            topLeft.Location = new Point(111, 108);
            topLeft.Name = "topLeft";
            topLeft.Size = new Size(75, 75);
            topLeft.TabIndex = 0;
            topLeft.Tag = "0";
            topLeft.UseVisualStyleBackColor = true;
            topLeft.Click += PositionClick;
            // 
            // topMiddle
            // 
            topMiddle.Enabled = false;
            topMiddle.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            topMiddle.Location = new Point(192, 108);
            topMiddle.Name = "topMiddle";
            topMiddle.Size = new Size(75, 75);
            topMiddle.TabIndex = 1;
            topMiddle.Tag = "1";
            topMiddle.UseVisualStyleBackColor = true;
            topMiddle.Click += PositionClick;
            // 
            // topRight
            // 
            topRight.Enabled = false;
            topRight.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            topRight.Location = new Point(273, 108);
            topRight.Name = "topRight";
            topRight.Size = new Size(75, 75);
            topRight.TabIndex = 2;
            topRight.Tag = "2";
            topRight.UseVisualStyleBackColor = true;
            topRight.Click += PositionClick;
            // 
            // centerLeft
            // 
            centerLeft.Enabled = false;
            centerLeft.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            centerLeft.Location = new Point(111, 189);
            centerLeft.Name = "centerLeft";
            centerLeft.Size = new Size(75, 75);
            centerLeft.TabIndex = 3;
            centerLeft.Tag = "3";
            centerLeft.UseVisualStyleBackColor = true;
            centerLeft.Click += PositionClick;
            // 
            // centerMiddle
            // 
            centerMiddle.Enabled = false;
            centerMiddle.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            centerMiddle.Location = new Point(192, 189);
            centerMiddle.Name = "centerMiddle";
            centerMiddle.Size = new Size(75, 75);
            centerMiddle.TabIndex = 4;
            centerMiddle.Tag = "4";
            centerMiddle.UseVisualStyleBackColor = true;
            centerMiddle.Click += PositionClick;
            // 
            // centerRight
            // 
            centerRight.Enabled = false;
            centerRight.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            centerRight.Location = new Point(273, 189);
            centerRight.Name = "centerRight";
            centerRight.Size = new Size(75, 75);
            centerRight.TabIndex = 5;
            centerRight.Tag = "5";
            centerRight.UseVisualStyleBackColor = true;
            centerRight.Click += PositionClick;
            // 
            // bottomLeft
            // 
            bottomLeft.Enabled = false;
            bottomLeft.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            bottomLeft.Location = new Point(111, 270);
            bottomLeft.Name = "bottomLeft";
            bottomLeft.Size = new Size(75, 75);
            bottomLeft.TabIndex = 6;
            bottomLeft.Tag = "6";
            bottomLeft.UseVisualStyleBackColor = true;
            bottomLeft.Click += PositionClick;
            // 
            // bottomMiddle
            // 
            bottomMiddle.Enabled = false;
            bottomMiddle.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            bottomMiddle.Location = new Point(192, 270);
            bottomMiddle.Name = "bottomMiddle";
            bottomMiddle.Size = new Size(75, 75);
            bottomMiddle.TabIndex = 7;
            bottomMiddle.Tag = "7";
            bottomMiddle.UseVisualStyleBackColor = true;
            bottomMiddle.Click += PositionClick;
            // 
            // bottomRight
            // 
            bottomRight.Enabled = false;
            bottomRight.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            bottomRight.Location = new Point(273, 270);
            bottomRight.Name = "bottomRight";
            bottomRight.Size = new Size(75, 75);
            bottomRight.TabIndex = 8;
            bottomRight.Tag = "8";
            bottomRight.UseVisualStyleBackColor = true;
            bottomRight.Click += PositionClick;
            // 
            // leaveButton
            // 
            leaveButton.Location = new Point(3, 474);
            leaveButton.Name = "leaveButton";
            leaveButton.Size = new Size(75, 23);
            leaveButton.TabIndex = 9;
            leaveButton.Text = "Leave";
            leaveButton.UseVisualStyleBackColor = true;
            leaveButton.Click += OnLeaveButton;
            // 
            // turnLabel
            // 
            turnLabel.AutoSize = true;
            turnLabel.Font = new Font("Segoe UI", 25F, FontStyle.Regular, GraphicsUnit.Point);
            turnLabel.Location = new Point(3, 0);
            turnLabel.Name = "turnLabel";
            turnLabel.Size = new Size(0, 46);
            turnLabel.TabIndex = 10;
            // 
            // playerLabel
            // 
            playerLabel.AutoSize = true;
            playerLabel.Font = new Font("Segoe UI", 25F, FontStyle.Regular, GraphicsUnit.Point);
            playerLabel.Location = new Point(3, 46);
            playerLabel.Name = "playerLabel";
            playerLabel.Size = new Size(0, 46);
            playerLabel.TabIndex = 11;
            // 
            // waitingLabel
            // 
            waitingLabel.AutoSize = true;
            waitingLabel.Font = new Font("Segoe UI", 25F, FontStyle.Regular, GraphicsUnit.Point);
            waitingLabel.Location = new Point(111, 367);
            waitingLabel.Name = "waitingLabel";
            waitingLabel.Size = new Size(0, 46);
            waitingLabel.TabIndex = 12;
            // 
            // warningLabel
            // 
            warningLabel.AutoSize = true;
            warningLabel.BackColor = Color.Red;
            warningLabel.Font = new Font("Segoe UI", 25F, FontStyle.Regular, GraphicsUnit.Point);
            warningLabel.Location = new Point(111, 451);
            warningLabel.Name = "warningLabel";
            warningLabel.Size = new Size(0, 46);
            warningLabel.TabIndex = 13;
            // 
            // winnerLabel
            // 
            winnerLabel.AutoSize = true;
            winnerLabel.Font = new Font("Segoe UI", 25F, FontStyle.Regular, GraphicsUnit.Point);
            winnerLabel.Location = new Point(186, 28);
            winnerLabel.Name = "winnerLabel";
            winnerLabel.Size = new Size(0, 46);
            winnerLabel.TabIndex = 14;
            // 
            // restartButton
            // 
            restartButton.Enabled = false;
            restartButton.Location = new Point(422, 471);
            restartButton.Name = "restartButton";
            restartButton.Size = new Size(75, 23);
            restartButton.TabIndex = 15;
            restartButton.Text = "Restart";
            restartButton.UseVisualStyleBackColor = true;
            restartButton.Click += OnRestartButton;
            // 
            // GameBoard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(restartButton);
            Controls.Add(winnerLabel);
            Controls.Add(warningLabel);
            Controls.Add(waitingLabel);
            Controls.Add(playerLabel);
            Controls.Add(turnLabel);
            Controls.Add(leaveButton);
            Controls.Add(bottomRight);
            Controls.Add(bottomMiddle);
            Controls.Add(bottomLeft);
            Controls.Add(centerRight);
            Controls.Add(centerMiddle);
            Controls.Add(centerLeft);
            Controls.Add(topRight);
            Controls.Add(topMiddle);
            Controls.Add(topLeft);
            Name = "GameBoard";
            Size = new Size(500, 500);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button topLeft;
        private Button topMiddle;
        private Button topRight;
        private Button centerRight;
        private Button centerMiddle;
        private Button centerLeft;
        private Button bottomRight;
        private Button bottomMiddle;
        private Button bottomLeft;
        private Button leaveButton;
        private Label turnLabel;
        private Label playerLabel;
        private Label waitingLabel;
        private Label warningLabel;
        private Label winnerLabel;
        private Button restartButton;
    }
}

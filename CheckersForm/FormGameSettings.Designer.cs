﻿
namespace CheckersForm
{
    partial class FormGameSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.RadioButtonBoardSize6X6 = new System.Windows.Forms.RadioButton();
            this.RadioButtonBoardSize8X8 = new System.Windows.Forms.RadioButton();
            this.RadioButtonBoardSize10X10 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Player2CheckBox = new System.Windows.Forms.CheckBox();
            this.DoneButton = new System.Windows.Forms.Button();
            this.Player1 = new System.Windows.Forms.TextBox();
            this.Player2TextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Board Size:";
            // 
            // RadioButtonBoardSize6X6
            // 
            this.RadioButtonBoardSize6X6.AutoSize = true;
            this.RadioButtonBoardSize6X6.Checked = true;
            this.RadioButtonBoardSize6X6.Location = new System.Drawing.Point(23, 30);
            this.RadioButtonBoardSize6X6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RadioButtonBoardSize6X6.Name = "RadioButtonBoardSize6X6";
            this.RadioButtonBoardSize6X6.Size = new System.Drawing.Size(48, 17);
            this.RadioButtonBoardSize6X6.TabIndex = 1;
            this.RadioButtonBoardSize6X6.TabStop = true;
            this.RadioButtonBoardSize6X6.Text = "6 x 6";
            this.RadioButtonBoardSize6X6.UseVisualStyleBackColor = true;
            // 
            // RadioButtonBoardSize8X8
            // 
            this.RadioButtonBoardSize8X8.AutoSize = true;
            this.RadioButtonBoardSize8X8.Location = new System.Drawing.Point(86, 30);
            this.RadioButtonBoardSize8X8.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RadioButtonBoardSize8X8.Name = "RadioButtonBoardSize8X8";
            this.RadioButtonBoardSize8X8.Size = new System.Drawing.Size(48, 17);
            this.RadioButtonBoardSize8X8.TabIndex = 2;
            this.RadioButtonBoardSize8X8.Text = "8 x 8";
            this.RadioButtonBoardSize8X8.UseVisualStyleBackColor = true;
            // 
            // RadioButtonBoardSize10X10
            // 
            this.RadioButtonBoardSize10X10.AutoSize = true;
            this.RadioButtonBoardSize10X10.Location = new System.Drawing.Point(155, 30);
            this.RadioButtonBoardSize10X10.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RadioButtonBoardSize10X10.Name = "RadioButtonBoardSize10X10";
            this.RadioButtonBoardSize10X10.Size = new System.Drawing.Size(60, 17);
            this.RadioButtonBoardSize10X10.TabIndex = 3;
            this.RadioButtonBoardSize10X10.Text = "10 x 10";
            this.RadioButtonBoardSize10X10.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Players:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 73);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Player 1:";
            // 
            // Player2CheckBox
            // 
            this.Player2CheckBox.AutoSize = true;
            this.Player2CheckBox.Location = new System.Drawing.Point(25, 99);
            this.Player2CheckBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Player2CheckBox.Name = "Player2CheckBox";
            this.Player2CheckBox.Size = new System.Drawing.Size(67, 17);
            this.Player2CheckBox.TabIndex = 6;
            this.Player2CheckBox.Text = "Player 2:";
            this.Player2CheckBox.UseVisualStyleBackColor = true;
            this.Player2CheckBox.Click += new System.EventHandler(this.Player2CheckBox_Click);
            // 
            // DoneButton
            // 
            this.DoneButton.Location = new System.Drawing.Point(128, 127);
            this.DoneButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(83, 23);
            this.DoneButton.TabIndex = 7;
            this.DoneButton.Text = "Done";
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            this.DoneButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.submitSettings);
            // 
            // Player1
            // 
            this.Player1.Location = new System.Drawing.Point(99, 71);
            this.Player1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Player1.Name = "Player1";
            this.Player1.Size = new System.Drawing.Size(114, 20);
            this.Player1.TabIndex = 8;
            // 
            // Player2TextBox
            // 
            this.Player2TextBox.BackColor = System.Drawing.SystemColors.HighlightText;
            this.Player2TextBox.Enabled = false;
            this.Player2TextBox.Location = new System.Drawing.Point(99, 97);
            this.Player2TextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Player2TextBox.Name = "Player2TextBox";
            this.Player2TextBox.Size = new System.Drawing.Size(114, 20);
            this.Player2TextBox.TabIndex = 9;
            this.Player2TextBox.Text = "[Computer]";
            // 
            // FormGameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 164);
            this.Controls.Add(this.Player2TextBox);
            this.Controls.Add(this.Player1);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.Player2CheckBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RadioButtonBoardSize10X10);
            this.Controls.Add(this.RadioButtonBoardSize8X8);
            this.Controls.Add(this.RadioButtonBoardSize6X6);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGameSettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Game Settings";
            this.Load += new System.EventHandler(this.FormGameSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton RadioButtonBoardSize6X6;
        private System.Windows.Forms.RadioButton RadioButtonBoardSize8X8;
        private System.Windows.Forms.RadioButton RadioButtonBoardSize10X10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox Player2CheckBox;
        private System.Windows.Forms.Button DoneButton;
        private System.Windows.Forms.TextBox Player1;
        private System.Windows.Forms.TextBox Player2TextBox;
    }
}


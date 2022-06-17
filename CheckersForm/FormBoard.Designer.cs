
namespace CheckersForm
{
    partial class FormBoard
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
            this.LabelPlayer2Score = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PanelScore = new System.Windows.Forms.Panel();
            this.LabelPlayer1Score = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PanelScore.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(2, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Player 1: ";
            // 
            // LabelPlayer2Score
            // 
            this.LabelPlayer2Score.AutoSize = true;
            this.LabelPlayer2Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.LabelPlayer2Score.Location = new System.Drawing.Point(178, 4);
            this.LabelPlayer2Score.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelPlayer2Score.Name = "LabelPlayer2Score";
            this.LabelPlayer2Score.Size = new System.Drawing.Size(14, 13);
            this.LabelPlayer2Score.TabIndex = 4;
            this.LabelPlayer2Score.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(119, 4);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Player 2: ";
            // 
            // PanelScore
            // 
            this.PanelScore.Controls.Add(this.LabelPlayer1Score);
            this.PanelScore.Controls.Add(this.LabelPlayer2Score);
            this.PanelScore.Controls.Add(this.label1);
            this.PanelScore.Controls.Add(this.label4);
            this.PanelScore.Location = new System.Drawing.Point(59, 8);
            this.PanelScore.Margin = new System.Windows.Forms.Padding(2);
            this.PanelScore.Name = "PanelScore";
            this.PanelScore.Size = new System.Drawing.Size(197, 23);
            this.PanelScore.TabIndex = 5;
            // 
            // LabelPlayer1Score
            // 
            this.LabelPlayer1Score.AutoSize = true;
            this.LabelPlayer1Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.LabelPlayer1Score.Location = new System.Drawing.Point(61, 4);
            this.LabelPlayer1Score.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LabelPlayer1Score.Name = "LabelPlayer1Score";
            this.LabelPlayer1Score.Size = new System.Drawing.Size(14, 13);
            this.LabelPlayer1Score.TabIndex = 5;
            this.LabelPlayer1Score.Text = "0";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 319);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(319, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // FormBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 341);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.PanelScore);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormBoard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Damka";
            this.Load += new System.EventHandler(this.FormBoard_Load);
            this.PanelScore.ResumeLayout(false);
            this.PanelScore.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LabelPlayer2Score;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel PanelScore;
        private System.Windows.Forms.Label LabelPlayer1Score;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    }
}
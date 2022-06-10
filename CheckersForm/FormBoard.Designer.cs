
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
            this.TableBoard = new System.Windows.Forms.DataGridView();
            this.PanelScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Player 1: ";
            // 
            // LabelPlayer2Score
            // 
            this.LabelPlayer2Score.AutoSize = true;
            this.LabelPlayer2Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.LabelPlayer2Score.Location = new System.Drawing.Point(267, 6);
            this.LabelPlayer2Score.Name = "LabelPlayer2Score";
            this.LabelPlayer2Score.Size = new System.Drawing.Size(19, 20);
            this.LabelPlayer2Score.TabIndex = 4;
            this.LabelPlayer2Score.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.Location = new System.Drawing.Point(178, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Player 2: ";
            // 
            // PanelScore
            // 
            this.PanelScore.Controls.Add(this.LabelPlayer1Score);
            this.PanelScore.Controls.Add(this.LabelPlayer2Score);
            this.PanelScore.Controls.Add(this.label1);
            this.PanelScore.Controls.Add(this.label4);
            this.PanelScore.Location = new System.Drawing.Point(88, 12);
            this.PanelScore.Name = "PanelScore";
            this.PanelScore.Size = new System.Drawing.Size(295, 36);
            this.PanelScore.TabIndex = 5;
            // 
            // LabelPlayer1Score
            // 
            this.LabelPlayer1Score.AutoSize = true;
            this.LabelPlayer1Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.LabelPlayer1Score.Location = new System.Drawing.Point(92, 6);
            this.LabelPlayer1Score.Name = "LabelPlayer1Score";
            this.LabelPlayer1Score.Size = new System.Drawing.Size(19, 20);
            this.LabelPlayer1Score.TabIndex = 5;
            this.LabelPlayer1Score.Text = "0";
            // 
            // TableBoard
            // 
            this.TableBoard.ColumnHeadersHeight = 34;
            this.TableBoard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.TableBoard.ColumnHeadersVisible = false;
            this.TableBoard.Location = new System.Drawing.Point(12, 57);
            this.TableBoard.Name = "TableBoard";
            this.TableBoard.RowHeadersVisible = false;
            this.TableBoard.RowHeadersWidth = 62;
            this.TableBoard.RowTemplate.Height = 28;
            this.TableBoard.Size = new System.Drawing.Size(450, 462);
            this.TableBoard.TabIndex = 6;
            this.TableBoard.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TableBoard_CellClick);
            this.TableBoard.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.TableBoard_CellPainting);
            // 
            // FormBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 524);
            this.Controls.Add(this.TableBoard);
            this.Controls.Add(this.PanelScore);
            this.Name = "FormBoard";
            this.Text = "Damka";
            this.Load += new System.EventHandler(this.FormBoard_Load);
            this.PanelScore.ResumeLayout(false);
            this.PanelScore.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableBoard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LabelPlayer2Score;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel PanelScore;
        private System.Windows.Forms.Label LabelPlayer1Score;
        private System.Windows.Forms.DataGridView TableBoard;
    }
}
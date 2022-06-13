using Checkers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CheckersForm
{
    public partial class FormBoard : Form
    {
        private FormGameSettings m_FormGameSettings = new FormGameSettings();
        private System.Drawing.Point? m_FirstClicked = null;
        public FormBoard()
        {
            m_FormGameSettings = new FormGameSettings();
            InitializeComponent();
            setCellSelectionDefaultColor();

            if (ensureFormGameSettingsSubmitted())
            {
                ShowDialog();
            }
        }

        private void setCellSelectionDefaultColor()
        {
            TableBoard.DefaultCellStyle.SelectionBackColor = Color.Transparent;
            TableBoard.DefaultCellStyle.SelectionForeColor = Color.Transparent;
        }

        private void resizeBoard(int i_BoardSize)
        {
            float spreadPrecent = 100 / i_BoardSize;

            TableBoard.ColumnCount = i_BoardSize;
            TableBoard.RowCount = i_BoardSize;

            TableBoard.Width = this.Width / 6 * i_BoardSize - 20;//Tochange
            TableBoard.Height = TableBoard.Width;//Tochange
            this.Width = this.Width / 6 * i_BoardSize + 12;
            this.Height = this.Width + 55;
 

            for (int i = 0; i < i_BoardSize; i++)
            {
                //TableBoard.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                //TableBoard.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                TableBoard.Columns[i].Width = TableBoard.Width / i_BoardSize;
                TableBoard.Rows[i].Height = TableBoard.Height / i_BoardSize;
            }

            this.PanelScore.Left = Width / 2 - PanelScore.Width / 2;
        }

        private void FormBoard_Load(object sender, EventArgs e)
        {

        }

        private bool ensureFormGameSettingsSubmitted()
        {
            bool formGameSettingsOk = false;

            if (m_FormGameSettings.ShowDialog() == DialogResult.OK)
            {
                resizeBoard(m_FormGameSettings.Board.m_Board.GetLength(0));
                printBoard();
                formGameSettingsOk = true;
            }

            return formGameSettingsOk;
        }

        private void paintDefaultColorCell(int i_rowIndex, int i_ColumnIndex)
        {
            if ((i_ColumnIndex + i_rowIndex) % 2 == 1)
            {
                TableBoard.Rows[i_rowIndex].Cells[i_ColumnIndex].Style.BackColor = Color.Black;
            }
            else
            {
                TableBoard.Rows[i_rowIndex].Cells[i_ColumnIndex].Style.BackColor = Color.White;
            }
        }

        private void printBoard()
        {
            for (int i = 0; i < TableBoard.RowCount; i++)
            {
                for (int j = 0; j < TableBoard.ColumnCount; j++)
                {
                    paintDefaultColorCell(i, j);
                }
            }
        }

        private void TableBoard_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //paintDefaultColorCell(e.RowIndex, e.ColumnIndex);
        }

        private bool validateClick(int i_rowIndex, int i_ColumnIndex)
        {
            bool isValidClick = true;

            if ((i_ColumnIndex + i_rowIndex) % 2 == 1)
            {
                isValidClick = false;
            } else if (false) //fix - check player tool
            {
                isValidClick = false;
            }

            return isValidClick;
        }

        private void userFirstSelection(int i_rowIndex, int i_ColumnIndex)
        {
            TableBoard.Rows[i_rowIndex].Cells[i_ColumnIndex].Style.BackColor = Color.LightBlue;
            TableBoard.Rows[i_rowIndex].Cells[i_ColumnIndex].Style.ForeColor = Color.LightBlue;
            m_FirstClicked = new System.Drawing.Point(i_rowIndex, i_ColumnIndex);
        }

        private void userRemovesSelection(int i_rowIndex, int i_ColumnIndex)
        {
            TableBoard.Rows[i_rowIndex].Cells[i_ColumnIndex].Style.BackColor = Color.White;
            TableBoard.Rows[i_rowIndex].Cells[i_ColumnIndex].Style.ForeColor = Color.White;
            m_FirstClicked = null;
        }

        private void userSelectedDestination(int i_rowIndex, int i_ColumnIndex)
        {
            TableBoard.Rows[i_rowIndex].Cells[i_ColumnIndex].Style.BackColor = Color.White;
            TableBoard.Rows[i_rowIndex].Cells[i_ColumnIndex].Style.ForeColor = Color.White;
            m_FirstClicked = null;
            //logic move
        }

        private void TableBoard_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (validateClick(e.RowIndex, e.ColumnIndex))
            {
                if (m_FirstClicked == null)
                {
                    userFirstSelection(e.RowIndex, e.ColumnIndex);
                }
                else if (m_FirstClicked == new System.Drawing.Point(e.RowIndex, e.ColumnIndex))
                {
                    userRemovesSelection(e.RowIndex, e.ColumnIndex);
                }
                else
                {
                    userSelectedDestination(e.RowIndex, e.ColumnIndex);
                }
            }
        }
    }
}

using Checkers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersForm
{
    public partial class FormBoard : Form
    {
        private FormGameSettings m_FormGameSettings = new FormGameSettings();
        private bool m_FirstClicked = false;
        public FormBoard()
        {
            m_FormGameSettings = new FormGameSettings();
            InitializeComponent();

            if (ensureFormGameSettingsSubmitted())
            {
                ShowDialog();
            }
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
 

         //   for (int i = 0; i < i_BoardSize; i++)
            //{
                TableBoard.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                TableBoard.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                //TableBoard.Columns[i].Width = TableBoard.Width / i_BoardSize;
                //TableBoard.Rows[i].Height = TableBoard.Height / i_BoardSize;
           // }

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
                formGameSettingsOk = true;
            }

            return formGameSettingsOk;
        }



        //private void Board_Click(object sender, EventArgs e)
        //{
        //    System.Drawing.Point cellPos;
        //    MouseEventArgs mouseEvent = e as MouseEventArgs;
        //    bool ValidCellPos = TryGetRowColIndex(
        //            TableBoard,
        //            new System.Drawing.Point(mouseEvent.X, mouseEvent.Y),
        //            out cellPos);

        //    if (!ValidCellPos)
        //    {
        //        return;
        //    }
        //    //validate Point


        //    if (m_SelectedCell == null)
        //    {
        //        //TableBoard.GetControlFromPosition(cellPos.X, cellPos.Y).BackColor = Color.LightBlue;
        //        Control ctrl = TableBoard.GetControlFromPosition(mouseEvent.X, mouseEvent.Y);

        //        m_SelectedCell = TableBoard.GetControlFromPosition(cellPos.X, cellPos.Y);
        //    }
        //    else
        //    {
        //        if (TableBoard.GetControlFromPosition(cellPos.X, cellPos.Y) != m_SelectedCell)
        //        {
        //            // start logic
        //            // print board
        //        }

        //        TableBoard.GetControlFromPosition(cellPos.X, cellPos.Y).BackColor = Color.White;
        //        m_SelectedCell = null;
        //    }
        //}

        private void TableBoard_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if ((e.ColumnIndex + e.RowIndex) % 2 == 1)
            {
                e.CellStyle.BackColor = Color.Black;
            }
            else
            {
                e.CellStyle.BackColor = Color.White;
            }
        }

        private void markCell(int i_rowIndex, int i_ColumnIndex)
        {
            if (m_FirstClicked)
            {
                if (TableBoard.Rows[i_rowIndex].Cells[i_ColumnIndex].Style.BackColor == Color.LightBlue)
                {
                    TableBoard.Rows[i_rowIndex].Cells[i_ColumnIndex].Style.BackColor = Color.White;
                    m_FirstClicked = false;
                }

                TableBoard.Rows[i_rowIndex].Cells[i_ColumnIndex].Style.BackColor = Color.LightBlue;
                m_FirstClicked = true;
            }
        }


        private void TableBoard_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            markCell(e.RowIndex, e.ColumnIndex);
        }
    }
}

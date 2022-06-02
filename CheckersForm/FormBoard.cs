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
        private Control m_SelectedCell = null;

        public FormBoard(Board i_Board)
        {
            InitializeComponent();
            resizeBoard(i_Board.m_Board.Length);
        }

        private void resizeBoard(int i_BoardSize)
        {
            float spreadPrecent = 100 / i_BoardSize;

            TableBoard.ColumnCount = i_BoardSize;
            TableBoard.RowCount = i_BoardSize;

            for (int i = 0; i < i_BoardSize - 1; i++) 
            {
                TableBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, spreadPrecent));
                TableBoard.RowStyles.Add(new RowStyle(SizeType.Percent, spreadPrecent));
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormBoard_Load(object sender, EventArgs e)
        {

        }

        private void TableBoard_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if ((e.Column + e.Row) % 2 == 1)
            {
                e.Graphics.FillRectangle(Brushes.Black, e.CellBounds);
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.White, e.CellBounds);
            }
        }

        System.Drawing.Point GetRowColIndex(TableLayoutPanel i_TableLayoutPanel, System.Drawing.Point i_Point)
        {
            int width = i_TableLayoutPanel.Width;
            int height = i_TableLayoutPanel.Height;
            int[] widths = i_TableLayoutPanel.GetColumnWidths();

            int i;
            for (i = widths.Length - 1; i >= 0 && i_Point.X < width; i--)
                width -= widths[i];
            int col = i + 1;

            int[] heights = i_TableLayoutPanel.GetRowHeights();
            for (i = heights.Length - 1; i >= 0 && i_Point.Y < height; i--)
                height -= heights[i];

            int row = i + 1;

            return new System.Drawing.Point(col, row);
        }

        private void Board_Click(object sender, EventArgs e)
        {
            System.Drawing.Point cellPos = GetRowColIndex(
                    TableBoard,
                    TableBoard.PointToClient(Cursor.Position));
            //validate Point         
            
            if (m_SelectedCell == null)
            {
                TableBoard.GetControlFromPosition(cellPos.X, cellPos.Y).BackColor = Color.LightBlue;
                m_SelectedCell = TableBoard.GetControlFromPosition(cellPos.X, cellPos.Y);
            }
            else
            {
                if (TableBoard.GetControlFromPosition(cellPos.X, cellPos.Y) != m_SelectedCell)
                { 
                    // start logic
                    // print board
                }

                TableBoard.GetControlFromPosition(cellPos.X, cellPos.Y).BackColor = Color.White;
                m_SelectedCell = null;
            }
        }
    }
}

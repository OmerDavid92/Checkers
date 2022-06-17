using Checkers;
using System;
using System.Drawing;
using System.Windows.Forms;
using Point = Checkers.Point;

namespace CheckersForm
{
    public partial class FormBoard : Form
    {
        private FormGameSettings m_FormGameSettings = new FormGameSettings();
        private Button[,] m_BoardButtons = null;
        private System.Drawing.Point? m_FirstClicked = null;
        private Game m_LogicGame = null;
        private Player m_CurrentPlayingPlayer = null;
        private Player m_MatchWinner = null;
        private bool m_IsPlayerPlayed = true;
        private string m_ErrorMessage = string.Empty;
        public FormBoard()
        {
            m_FormGameSettings = new FormGameSettings();
            InitializeComponent();
            setCellSelectionDefaultColor();

            if (ensureFormGameSettingsSubmitted())
            {
                m_LogicGame.ResetPreviousTurn();
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

        private void InitLogicGame()
        {
            Checkers.Enum.BoardSize boardSize = (Checkers.Enum.BoardSize)m_FormGameSettings.GetBoardSize();
            Player player1 = m_FormGameSettings.GetPlayer1();
            Player player2 = m_FormGameSettings.GetPlayer2();

            m_LogicGame = new Game(boardSize, player1, player2);
            m_CurrentPlayingPlayer = m_LogicGame.m_Player1;
        }

        private void endMatch(Player i_MatchWinner) //UI
        {
            Player lostPlayer = m_LogicGame.switchPlayer(i_MatchWinner);

            if (i_MatchWinner != null)
            {
                m_LogicGame.calculateMatchWinnerScore(i_MatchWinner);
            }

            UserInterface.PrintWinnerMatch(i_MatchWinner, lostPlayer);

            if (UserInterface.GetUserInputIsRematch())
            {
                m_LogicGame.InitBoard();
                startLogicGame();
            }
            else
            {
                UserInterface.PrintWinnerGame(m_LogicGame.getGameWinner());
                if (ensureFormGameSettingsSubmitted())
                {
                    ShowDialog();
                }
                InitialGameAndStart();
            }
        }

        private void InitialGameAndStart()
        {
            InitLogicGame();
            startLogicGame();
        }

        private void startLogicGame()
        {
            m_LogicGame.ResetPreviousTurn();

            while (!m_LogicGame.isMatchOver(m_CurrentPlayingPlayer, ref m_MatchWinner) && m_IsPlayerPlayed)
            {
                //printState(currentPlayingPlayer, errorMessage); // UI - copy from Game / develop
                m_IsPlayerPlayed = m_LogicGame.tryPlay(ref m_CurrentPlayingPlayer, ref m_ErrorMessage);
            }

            //printState(currentPlayingPlayer, errorMessage); // UI - copy from Game / develop

            if (!m_IsPlayerPlayed)
            {
                m_MatchWinner = m_LogicGame.switchPlayer(m_CurrentPlayingPlayer);
            }

            //m_LogicGame.endMatch(matchWinner); // UI - copy from Game / develop
        }

        private bool ensureFormGameSettingsSubmitted()
        {
            bool formGameSettingsOk = false;

            if (m_FormGameSettings.ShowDialog() == DialogResult.OK)
            {
                InitLogicGame();
                resizeBoard(m_FormGameSettings.GetBoardSize());
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

        private Turn getCurrentTurnForLogicGame(System.Drawing.Point i_Source, System.Drawing.Point i_Destination)
        {
            //get Turn from user move on UI
        }

        private void userSelectedDestination(int i_rowIndex, int i_ColumnIndex)
        {
            TableBoard.Rows[i_rowIndex].Cells[i_ColumnIndex].Style.BackColor = Color.White;
            TableBoard.Rows[i_rowIndex].Cells[i_ColumnIndex].Style.ForeColor = Color.White;
            m_FirstClicked = null;
            //logic move
            m_IsPlayerPlayed = m_LogicGame.tryPlay(ref m_CurrentPlayingPlayer, ref m_ErrorMessage);
            //printState(currentPlayingPlayer, errorMessage); // UI - copy from Game / develop

            if (m_LogicGame.isMatchOver(m_CurrentPlayingPlayer, ref m_MatchWinner))
            {
                if (!m_IsPlayerPlayed)
                {
                    m_MatchWinner = m_LogicGame.switchPlayer(m_CurrentPlayingPlayer);
                }

                endMatch(m_MatchWinner); // UI - copy from Game / develop
            }
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

        private void paintButton(int i_rowIndex, int i_ColumnIndex, Button i_button)
        {
            if ((i_ColumnIndex + i_rowIndex) % 2 == 1)
            {
                i_button.BackColor = Color.Black;
            }
            else
            {
                i_button.BackColor = Color.White;
            }
        }

        private void FormBoard_Buttons(int i_BoardSize)
        {
            int x_Location = 0;
            int y_Location = 0;

            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    m_BoardButtons[i, j] = new Button();
                    m_BoardButtons[i, j].Width = TableBoard.Width / i_BoardSize;
                    m_BoardButtons[i, j].Height = m_BoardButtons[i, j].Width;
                    x_Location = m_BoardButtons[i, j].Width * i + 300;
                    y_Location += m_BoardButtons[i, j].Height * j + 300;
                    m_BoardButtons[i, j].Location = new System.Drawing.Point(x_Location, y_Location);
                    paintButton(i, j, m_BoardButtons[i, j]);
                }
            }
        }

        private void FormBoard_Load(object sender, EventArgs e)
        {

        }
    }
}

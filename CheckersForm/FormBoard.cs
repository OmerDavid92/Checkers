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
        private int m_BoardSize;
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

            if (ensureFormGameSettingsSubmitted())
            {
                m_LogicGame.ResetPreviousTurn();
                ShowDialog();
            }
        }

        private void InitLogicGame()
        {
            Checkers.Enum.BoardSize boardSize = (Checkers.Enum.BoardSize)m_BoardSize;
            Player player1 = m_FormGameSettings.GetPlayer1();
            Player player2 = m_FormGameSettings.GetPlayer2();

            m_LogicGame = new Game(boardSize, player1, player2);
            m_CurrentPlayingPlayer = m_LogicGame.m_Player1;
        }

        private void endMatch(Player i_MatchWinner)
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
                //startLogicGame();
            }
            else
            {
                UserInterface.PrintWinnerGame(m_LogicGame.getGameWinner());
                if (ensureFormGameSettingsSubmitted())
                {
                    ShowDialog();
                }
            }
        }

        private void startLogicGame()
        {
            m_LogicGame.ResetPreviousTurn();

            while (!m_LogicGame.isMatchOver(m_CurrentPlayingPlayer, ref m_MatchWinner) && m_IsPlayerPlayed)
            {
                //printState(currentPlayingPlayer, errorMessage); // UI - copy from Game / develop
                //m_IsPlayerPlayed = m_LogicGame.tryPlay(ref m_CurrentPlayingPlayer, ref m_ErrorMessage);
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
                m_BoardSize = m_FormGameSettings.GetBoardSize();
                InitLogicGame();
                initBoard();
                PaintDefaultColorBoard();
                updateBoardTroopers();
                formGameSettingsOk = true;
            }

            return formGameSettingsOk;
        }

        private void paintDefaultColorCell(int i_rowIndex, int i_ColumnIndex)
        {
            if ((i_ColumnIndex + i_rowIndex) % 2 == 1)
            {
                m_BoardButtons[i_rowIndex, i_ColumnIndex].BackColor = Color.Black;
            }
            else
            {
                m_BoardButtons[i_rowIndex, i_ColumnIndex].BackColor = Color.White;
            }
        }

        private void PaintDefaultColorBoard()
        {
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    paintDefaultColorCell(i, j);
                }
            }
        }

        private void updateBoardTroopers()
        {
            char[,] board = m_LogicGame.m_Board.m_Board;

            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    m_BoardButtons[i, j].Text = board[i, j].ToString();
                }
            }
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

        private Turn getCurrentTurn(System.Drawing.Point i_Source, System.Drawing.Point i_Destination)
        {
            //get Turn from user move on UI
            return null;
        }

        private void userFirstSelection(int i_rowIndex, int i_ColumnIndex)
        {
            m_BoardButtons[i_rowIndex, i_ColumnIndex].BackColor = Color.LightBlue;
            m_FirstClicked = new System.Drawing.Point(i_rowIndex, i_ColumnIndex);
        }

        private void userRemovesSelection(int i_rowIndex, int i_ColumnIndex)
        {
            paintDefaultColorCell(i_rowIndex, i_ColumnIndex);
            m_FirstClicked = null;
        }

        private void userSelectedDestination(int i_rowIndex, int i_ColumnIndex)
        {
            Turn currentTurn = null;
            System.Drawing.Point destination = new System.Drawing.Point(i_rowIndex, i_ColumnIndex);
            System.Drawing.Point source = m_FirstClicked ?? System.Drawing.Point.Empty;

            paintDefaultColorCell(i_rowIndex, i_ColumnIndex);
            m_FirstClicked = null;
            currentTurn = getCurrentTurn(source, destination);
            m_IsPlayerPlayed = m_LogicGame.tryPlay(ref m_CurrentPlayingPlayer, ref m_ErrorMessage, currentTurn);
            //printState(currentPlayingPlayer, errorMessage); // UI - copy from Game / develop

            if (m_LogicGame.isMatchOver(m_CurrentPlayingPlayer, ref m_MatchWinner))
            {
                if (!m_IsPlayerPlayed)
                {
                    m_MatchWinner = m_LogicGame.switchPlayer(m_CurrentPlayingPlayer);
                }

                endMatch(m_MatchWinner);
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

        private void initBoard()
        {
            m_BoardButtons = new Button[m_BoardSize, m_BoardSize];
            int startX = 10;
            int startY = 40;
            int x_Location = startX;
            int y_Location = startY;
            int buttonSize = 0;

            this.Width = m_BoardSize * 70;
            this.Height = this.Width + 50;
            buttonSize = (this.Width - 35) / m_BoardSize;

            for (int i = 0; i < m_BoardSize; i++)
            {
                y_Location = startY + buttonSize * i;

                for (int j = 0; j < m_BoardSize; j++)
                {
                    m_BoardButtons[i, j] = new Button();
                    m_BoardButtons[i, j].Width = buttonSize;
                    m_BoardButtons[i, j].Height = buttonSize;
                    x_Location = startX + buttonSize * j;
                    
                    m_BoardButtons[i, j].Location = new System.Drawing.Point(x_Location, y_Location);
                    this.Controls.Add(m_BoardButtons[i, j]);
                }
            }

            PaintDefaultColorBoard();
        }

        private void FormBoard_Load(object sender, EventArgs e)
        {

        }
    }
}

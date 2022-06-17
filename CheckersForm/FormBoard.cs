using Checkers;
using System;
using System.Drawing;
using System.Windows.Forms;
using Enum = Checkers.Enum;
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

        private void updateStatusStrip()
        {
            string playerName = m_CurrentPlayingPlayer.m_PlayerName;
            char trooperSign = m_CurrentPlayingPlayer.m_ToolSign.m_TrooperSign;
            string status = string.Format("{0} turn: {1}", playerName, trooperSign);

            statusLabel.Text = status;
        }

        private void InitLogicGame()
        {
            Checkers.Enum.BoardSize boardSize = (Checkers.Enum.BoardSize)m_BoardSize;
            Player player1 = m_FormGameSettings.GetPlayer1();
            Player player2 = m_FormGameSettings.GetPlayer2();

            m_LogicGame = new Game(boardSize, player1, player2);
            m_CurrentPlayingPlayer = m_LogicGame.m_Player1;
            updateStatusStrip();
        }

        private void updateScore()
        {
            LabelPlayer1Score.Text = m_LogicGame.m_Player1.m_Score.ToString();
            LabelPlayer2Score.Text = m_LogicGame.m_Player2.m_Score.ToString();
        }

        private void endMatchDialog(Player i_MatchWinner)
        {
            string message = string.Format("Tie!{0}Another Round?", System.Environment.NewLine);
            DialogResult result;

            if (i_MatchWinner != null)
            {
                message = string.Format("{0} Won the Match!{1}Another Round?", i_MatchWinner.m_PlayerName, System.Environment.NewLine);
            }

            result = MessageBox.Show(message, "Damka", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                m_LogicGame.InitBoard();
                updateBoardTroopers();
                updateScore();
            }
            else
            {
                this.Close();
            }
        }

        private void endMatch(Player i_MatchWinner)
        {
            Player lostPlayer = m_LogicGame.switchPlayer(i_MatchWinner);

            if (i_MatchWinner != null)
            {
                m_LogicGame.calculateMatchWinnerScore(i_MatchWinner);
            }

            endMatchDialog(i_MatchWinner);
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
            if ((i_ColumnIndex + i_rowIndex) % 2 == 0)
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

            Application.DoEvents();
        }

        private bool validateClick(int i_rowIndex, int i_ColumnIndex)
        {
            bool isValidClick = true;

            if ((i_ColumnIndex + i_rowIndex) % 2 == 0)
            {
                isValidClick = false;
            } else if (m_FirstClicked == null &&
                       m_BoardButtons[i_rowIndex, i_ColumnIndex].Text != m_CurrentPlayingPlayer.m_ToolSign.m_TrooperSign.ToString() &&
                       m_BoardButtons[i_rowIndex, i_ColumnIndex].Text != m_CurrentPlayingPlayer.m_ToolSign.m_KingSign.ToString())
            {
                isValidClick = false;
            }

            return isValidClick;
        }

        private Turn getCurrentTurn(System.Drawing.Point i_Source, System.Drawing.Point i_Destination)
        {
            Point source = new Point(i_Source.X, i_Source.Y);
            Point destination = new Point(i_Destination.X, i_Destination.Y);

            return new Turn(source, destination);
        }

        private void updateGameState(string i_ErrorMessage)
        {
            Turn previousTurn = m_LogicGame.m_PreviousTurn;
            Player previousPlayer = m_CurrentPlayingPlayer;

            updateBoardTroopers();

            if (i_ErrorMessage != null)
            {

            }

            if (previousTurn != null && !previousTurn.m_ShouldCaptureAgain)
            {
                previousPlayer = m_LogicGame.switchPlayer(m_CurrentPlayingPlayer);
            }
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

            currentTurn = getCurrentTurn(source, destination);
            m_ErrorMessage = string.Empty;
            m_IsPlayerPlayed = m_LogicGame.tryPlay(ref m_CurrentPlayingPlayer, ref m_ErrorMessage, currentTurn);
            updateStatusStrip();
            m_FirstClicked = null;
            paintDefaultColorCell(source.X, source.Y);
            endOfTurn();
        }

        private void pcPlay()
        {
            m_ErrorMessage = string.Empty;
            m_IsPlayerPlayed = m_LogicGame.tryPlay(ref m_CurrentPlayingPlayer, ref m_ErrorMessage);
            updateStatusStrip();
            endOfTurn();
        }
        
        private void endOfTurn()
        {
            if (m_ErrorMessage == string.Empty)
            {
                updateGameState(m_ErrorMessage);

                if (m_LogicGame.isMatchOver(m_CurrentPlayingPlayer, ref m_MatchWinner))
                {
                    if (!m_IsPlayerPlayed)
                    {
                        m_MatchWinner = m_LogicGame.switchPlayer(m_CurrentPlayingPlayer);
                    }

                    endMatch(m_MatchWinner);
                }
                else if (m_LogicGame.m_Player2 == m_CurrentPlayingPlayer && m_LogicGame.m_Player2.m_PlayerType == Enum.PlayerType.PC)
                {
                    pcPlay();
                }
            }
            else
            {
                MessageBox.Show(m_ErrorMessage, "Move Error", MessageBoxButtons.OK);
            }
        }

        private void BoardButtons_CellClick(object sender, EventArgs e)
        {
            object buttonTag = (sender as Button).Tag;
            System.Drawing.Point currentCell = (System.Drawing.Point)buttonTag;

            if (validateClick(currentCell.X, currentCell.Y))
            {
                if (m_FirstClicked == null)
                {
                    userFirstSelection(currentCell.X, currentCell.Y);
                }
                else if (m_FirstClicked == new System.Drawing.Point(currentCell.X, currentCell.Y))
                {
                    userRemovesSelection(currentCell.X, currentCell.Y);
                }
                else
                {
                    userSelectedDestination(currentCell.X, currentCell.Y);
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
            this.Height = this.Width + 70;
            buttonSize = (this.Width - 35) / m_BoardSize;
            PanelScore.Left = this.Width / 2 - PanelScore.Width / 2;

            for (int i = 0; i < m_BoardSize; i++)
            {
                y_Location = startY + buttonSize * i;

                for (int j = 0; j < m_BoardSize; j++)
                {
                    m_BoardButtons[i, j] = new Button();
                    m_BoardButtons[i, j].Tag = new System.Drawing.Point(i, j);
                    m_BoardButtons[i, j].Click += BoardButtons_CellClick;
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

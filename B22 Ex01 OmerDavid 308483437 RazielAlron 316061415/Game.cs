using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Game
    {
        private Enum.GameStatus m_GameStatus { get; set; }
        private Board m_Board { get; set; }
        private Player m_Player1 { get; set; }
        private Player m_Player2 { get; set; }

        public Game() 
        {
            InitialGameAndStart();
        }

        public void InitialGameAndStart()
        {
            InitialGame();
            Start();
        }

        public void InitialGame()
        {
            string playerName;
            Enum.BoardSize boardSize;
            Enum.PlayerType PlayerType;

            playerName = UserInterface.GetUserInputPlayerName();
            m_Player1 = new Player((char)Enum.Player1Tools.Trooper, (char)Enum.Player1Tools.King, Enum.PlayerType.Human, true, playerName);

            boardSize = UserInterface.GetUserInputBoardSize();
            m_Board = new Board((int)boardSize);

            PlayerType = UserInterface.GetUserInputPlayerType();

            if (PlayerType == Enum.PlayerType.Human)
            {
                playerName = UserInterface.GetUserInputPlayerName();
            }
            else
            {
                playerName = "PC";
            }

            m_Player2 = new Player((char)Enum.Player2Tools.Trooper, (char)Enum.Player2Tools.King, PlayerType, false, playerName);
            m_Board.InitBoard(m_Player1.m_ToolSign, m_Player2.m_ToolSign);
        }

        private bool tryPlay(ref Player i_currentPlayingPlayer)
        {
            Point sourcePosition = new Point(0, 0);
            Point destinationPosition = new Point(0, 0);
            bool isPlayerPlayed = true;

            isPlayerPlayed = i_currentPlayingPlayer.TryGetTurn(ref sourcePosition, ref destinationPosition);

            if (isPlayerPlayed)
            {
                if (m_Board.ValidateMove(sourcePosition, destinationPosition, i_currentPlayingPlayer))
                {
                    m_Board.MakeTurn(sourcePosition, destinationPosition);

                    if (!m_Board.IsJumpedMoreThanOneTile(sourcePosition, destinationPosition)
                        || !m_Board.IsToolCanCapture(i_currentPlayingPlayer, destinationPosition))
                    {
                        i_currentPlayingPlayer = switchPlayer(i_currentPlayingPlayer);
                    }
                }
                else
                {
                    UserInterface.InvalidTurnMessage();
                }
            }

            return isPlayerPlayed;
        }

        public void Start()
        {
            Player currentPlayingPlayer = m_Player1;
            Player matchWinner = null;
            bool isPlayerPlayed = true;

            while (isMatchOver() || isPlayerPlayed)
            {
                UserInterface.PrintBoard(m_Board);
                isPlayerPlayed = tryPlay(ref currentPlayingPlayer);
            }

            if (isPlayerPlayed)
            {
                matchWinner = getMatchWinner();
            }
            else
            {
                matchWinner = switchPlayer(currentPlayingPlayer);
            }

            endMatch(matchWinner);
        }

        private Player switchPlayer(Player i_CurrentPlayingPlayer)
        {
            Player nextPlayingPlayer = m_Player1;

            if (i_CurrentPlayingPlayer == m_Player1)
            {
                return m_Player2;
            }

            return nextPlayingPlayer;
        }

        private void endMatch(Player i_MatchWinner)
        {
            UserInterface.PrintWinnerMatch(i_MatchWinner);

            if (i_MatchWinner != null)
            {
                i_MatchWinner.m_Score++;
            }

            if (UserInterface.GetUserInputIsRematch())
            {
                m_Board.InitBoard(m_Player1.m_ToolSign, m_Player2.m_ToolSign);
                Start();
            }
            else
            {
                UserInterface.PrintWinnerGame(getGameWinner());
                InitialGameAndStart();
            }
        }

        private bool isMatchOver()
        {
            return (!m_Board.IsValidMoveExist(m_Player1) && !m_Board.IsValidMoveExist(m_Player2))
                || m_Board.SumOfPointsOnBoard(m_Player1) == 0
                || m_Board.SumOfPointsOnBoard(m_Player2) == 0;
        }

        private Player getMatchWinner()
        {
            Player matchWinner = null;
            int player1PointsOnBoard = m_Board.SumOfPointsOnBoard(m_Player1);
            int player2PointsOnBoard = m_Board.SumOfPointsOnBoard(m_Player2);


            if(player1PointsOnBoard > player2PointsOnBoard)
            {
                matchWinner = m_Player1;
            }
            else if(player2PointsOnBoard > player1PointsOnBoard)
            {
                matchWinner = m_Player2;
            }

            return matchWinner;
        }

        private Player getGameWinner()
        {
            Player winner = null;

            if (m_Player1.m_Score > m_Player2.m_Score)
            {
                winner = m_Player1;
            }
            else if (m_Player2.m_Score > m_Player1.m_Score)
            {
                winner = m_Player2;
            }

            return winner;
        }
    }
}

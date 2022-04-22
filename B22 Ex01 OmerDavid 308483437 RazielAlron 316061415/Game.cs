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
            //gameInit();    
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
        }

        private bool tryPlay(Player i_currentPlayingPlayer)
        {
            Point sourcePosition = new Point(0, 0);
            Point destinationPosition = new Point(0, 0);
            bool isPlayerPlayed = true;

            isPlayerPlayed = i_currentPlayingPlayer.TryGetTurn(ref sourcePosition, ref destinationPosition);

            if (isPlayerPlayed)
            {
                if (m_Board.ValidateTurn(sourcePosition, destinationPosition, i_currentPlayingPlayer.m_ToolSign))
                {
                    m_Board.performTurn(sourcePosition, destinationPosition);

                    if (!m_Board.isPlayerCanCapture(i_currentPlayingPlayer.m_ToolSign, destinationPosition))
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

        private bool isTie()
        {
            return true;
        }

        public void Start()
        {
            Player currentPlayingPlayer = m_Player1;
            Player winner = null;

            while (!isTie() || winner == null)
            {
                if (tryPlay(currentPlayingPlayer))
                {
                    winner = m_Board.GetWinner();
                }
                else
                {
                    winner = switchPlayer(currentPlayingPlayer);
                }
            }

            endMatch(winner);
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
                m_Board.init();
                Start();
            }
            else
            {
                UserInterface.PrintWinnerGame(m_Player1, m_Player2);
                InitialGameAndStart();
            }
        }

    }
}

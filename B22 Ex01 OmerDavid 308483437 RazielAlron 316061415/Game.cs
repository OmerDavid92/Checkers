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
        public Turn m_previousTurn { get; set; } = null;

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

            UserInterface.MainMenuMessage();
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

        private bool tryPlay(ref Player o_currentPlayingPlayer, ref string i_ErrorMessage)
        {
            bool isPlayerPlayed = true;
            Turn currentTurn = null;

            isPlayerPlayed = o_currentPlayingPlayer.TryGetTurn(m_Board, ref currentTurn);

            if (isPlayerPlayed)
            {
                if (validateCaptureAgain(currentTurn)
                    && m_Board.ValidateMove(currentTurn.m_Source, currentTurn.m_Destination, o_currentPlayingPlayer))
                {
                    m_Board.MakeTurn(currentTurn.m_Source, currentTurn.m_Destination);

                    if (!m_Board.IsJumpedMoreThanOneTile(currentTurn.m_Source, currentTurn.m_Destination)
                        || !m_Board.IsToolCanCapture(o_currentPlayingPlayer, currentTurn.m_Destination))
                    {
                        o_currentPlayingPlayer = switchPlayer(o_currentPlayingPlayer);
                        currentTurn.m_ShouldCaptureAgain = false;
                    }
                    else
                    {
                        currentTurn.m_ShouldCaptureAgain = true;
                    }

                    m_previousTurn = currentTurn;
                    i_ErrorMessage = "";
                }
            }

            return isPlayerPlayed;
        }

        private bool validateCaptureAgain(Turn i_CurrentTurn)
        {
            bool isValid = true;

            if (m_previousTurn != null)
            {
                isValid = m_previousTurn.m_ShouldCaptureAgain
                    && m_previousTurn.m_Destination.m_X == i_CurrentTurn.m_Source.m_X
                    && m_previousTurn.m_Destination.m_Y == i_CurrentTurn.m_Source.m_Y;
            }

            return isValid;
        }

        public void Start()
        {
            Player currentPlayingPlayer = m_Player1;
            Player matchWinner = null;
            bool isPlayerPlayed = true;
            string errorMessage = "";

            while (!isMatchOver(currentPlayingPlayer) && isPlayerPlayed)
            {
                Ex02.ConsoleUtils.Screen.Clear();
                UserInterface.PrintBoard(m_Board);
                UserInterface.PrintErrorMessage(errorMessage);
                UserInterface.PrintLastPlay(m_previousTurn);
                isPlayerPlayed = tryPlay(ref currentPlayingPlayer, ref errorMessage);
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
            Player lostPlayer = switchPlayer(i_MatchWinner);

            if (i_MatchWinner != null)
            {
                i_MatchWinner.m_Score++;
            }

            UserInterface.PrintWinnerMatch(i_MatchWinner, lostPlayer);

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

        private bool isMatchOver(Player i_CurrentPlayingPlayer)
        {
            return !m_Board.IsValidMoveExist(i_CurrentPlayingPlayer)
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

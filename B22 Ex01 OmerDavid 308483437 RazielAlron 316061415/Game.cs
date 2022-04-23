﻿using System;
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
        public Turn m_PreviousTurn { get; set; } = null;

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

            if (isPlayerPlayed
                && validateCaptureAgain(currentTurn)
                && m_Board.ValidateMove(currentTurn.m_Source, currentTurn.m_Destination, o_currentPlayingPlayer, ref i_ErrorMessage))
            {
                m_Board.MakeTurn(currentTurn.m_Source, currentTurn.m_Destination, o_currentPlayingPlayer);

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

                m_PreviousTurn = currentTurn;
                i_ErrorMessage = "";
            }

            return isPlayerPlayed;
        }

        private bool validateCaptureAgain(Turn i_CurrentTurn)
        {
            bool isValid = true;

            if (m_PreviousTurn != null && m_PreviousTurn.m_ShouldCaptureAgain)
            {
                isValid = m_PreviousTurn.m_Destination.m_X == i_CurrentTurn.m_Source.m_X
                    && m_PreviousTurn.m_Destination.m_Y == i_CurrentTurn.m_Source.m_Y;
            }

            return isValid;
        }

        private void PrintState(string i_ErrorMessage)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            UserInterface.PrintBoard(m_Board);
            UserInterface.PrintErrorMessage(i_ErrorMessage);
            UserInterface.PrintLastPlay(m_PreviousTurn);
        }

        public void Start()
        {
            Player currentPlayingPlayer = m_Player1;
            Player matchWinner = null;
            bool isPlayerPlayed = true;
            string errorMessage = "";

            while (!isMatchOver(currentPlayingPlayer, ref matchWinner) && isPlayerPlayed)
            {
                PrintState(errorMessage);
                isPlayerPlayed = tryPlay(ref currentPlayingPlayer, ref errorMessage);
            }

            PrintState(errorMessage);

            if (!isPlayerPlayed)
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
                calculateMatchWinnerScore(i_MatchWinner);
            }

            UserInterface.PrintWinnerMatch(i_MatchWinner, lostPlayer);
            m_PreviousTurn = null;

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

        private bool isMatchOver(Player i_CurrentPlayingPlayer, ref Player o_Winner)
        {
            bool isOver = false;

            if (!m_Board.IsValidMoveExist(i_CurrentPlayingPlayer))
            {
                isOver = true;

                if (!m_Board.IsValidMoveExist(switchPlayer(i_CurrentPlayingPlayer)))
                {
                    o_Winner = null;
                }
                else
                {
                    o_Winner = switchPlayer(i_CurrentPlayingPlayer);
                }
            }

            return isOver;
        }

        private void calculateMatchWinnerScore(Player i_Winner)
        {
            int winnerPointsOnBoard = m_Board.SumOfPointsOnBoard(i_Winner);
            int loserPointsOnBoard = m_Board.SumOfPointsOnBoard(switchPlayer(i_Winner));

            i_Winner.m_Score += winnerPointsOnBoard - loserPointsOnBoard;
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

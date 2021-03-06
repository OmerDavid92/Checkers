namespace Checkers
{
    public class Game
    {
        public Board m_Board { get; private set; }
        public Player m_Player1 { get; private set; }
        public Player m_Player2 { get; private set; }
        public Turn m_PreviousTurn { get; private set; } = null;

        public Game(Enum.BoardSize i_BoardSize, Player i_Player1, Player i_Player2)
        {
            m_Player1 = i_Player1;
            m_Player2 = i_Player2;
            m_Board = new Board((int)i_BoardSize);
            InitBoard();
            //InitialGameAndStart();
        }

        public void InitBoard()
        {
            m_Board.InitBoard(m_Player1.m_ToolSign, m_Player2.m_ToolSign);
        }

        public void ResetPreviousTurn()
        {
            m_PreviousTurn = null;
        }

        public void    InitialGameAndStart() // UI
        {
            m_PreviousTurn = null;
            //InitialGame();
            Start();
        }

        public void    InitialGame() // UI
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

        public void    Start() // UI
        {
            Player currentPlayingPlayer = m_Player1;
            Player matchWinner = null;
            bool isPlayerPlayed = true;
            string errorMessage = string.Empty;

            while (!isMatchOver(currentPlayingPlayer, ref matchWinner) && isPlayerPlayed)
            {
                printState(currentPlayingPlayer, errorMessage);
                isPlayerPlayed = tryPlay(ref currentPlayingPlayer, ref errorMessage);
            }

            printState(currentPlayingPlayer, errorMessage);

            if (!isPlayerPlayed)
            {
                matchWinner = switchPlayer(currentPlayingPlayer);
            }

            endMatch(matchWinner);
        }

        public bool   tryPlay(ref Player o_currentPlayingPlayer, ref string i_ErrorMessage, Turn i_UiTurn = null)
        {
            bool isPlayerPlayed = true;
            Turn currentTurn = null;

            if (i_UiTurn != null)
            {
                currentTurn = i_UiTurn;
            }
            else
            {
                isPlayerPlayed = o_currentPlayingPlayer.TryGetTurn(m_Board, ref currentTurn);
            }

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
                i_ErrorMessage = string.Empty;
            }

            return isPlayerPlayed;
        }

        private bool   validateCaptureAgain(Turn i_CurrentTurn)
        {
            bool isValid = true;

            if (m_PreviousTurn != null && m_PreviousTurn.m_ShouldCaptureAgain)
            {
                isValid = m_PreviousTurn.m_Destination.m_X == i_CurrentTurn.m_Source.m_X
                    && m_PreviousTurn.m_Destination.m_Y == i_CurrentTurn.m_Source.m_Y;
            }

            return isValid;
        }

        private void   printState(Player i_CurrentPlayingPlayer, string i_ErrorMessage) //UI
        {
            Player previousPlayer = i_CurrentPlayingPlayer;

            //Ex02.ConsoleUtils.Screen.Clear();
            UserInterface.PrintBoard(m_Board);
            UserInterface.PrintErrorMessage(i_ErrorMessage);

            if(m_PreviousTurn != null && !m_PreviousTurn.m_ShouldCaptureAgain)
            {
                previousPlayer = switchPlayer(i_CurrentPlayingPlayer);
            }

            UserInterface.PrintLastPlay(previousPlayer, m_PreviousTurn);
        }

        public Player switchPlayer(Player i_CurrentPlayingPlayer)
        {
            Player nextPlayingPlayer = m_Player1;

            if (i_CurrentPlayingPlayer == m_Player1)
            {
                return m_Player2;
            }

            return nextPlayingPlayer;
        }

        private void   endMatch(Player i_MatchWinner) //UI
        {
            Player lostPlayer = switchPlayer(i_MatchWinner);

            if (i_MatchWinner != null)
            {
                calculateMatchWinnerScore(i_MatchWinner);
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

        public bool   isMatchOver(Player i_CurrentPlayingPlayer, ref Player o_Winner)
        {
            bool isOver = false;

            if (!m_Board.IsValidMoveExist(i_CurrentPlayingPlayer))
            {
                isOver = true;

                if (m_Board.IsValidMoveExist(switchPlayer(i_CurrentPlayingPlayer)))
                {
                    o_Winner = switchPlayer(i_CurrentPlayingPlayer);
                }
                else
                {
                    o_Winner = null;
                }
            }

            return isOver;
        }

        public void   calculateMatchWinnerScore(Player i_Winner)
        {
            int winnerPointsOnBoard = m_Board.SumOfPointsOnBoard(i_Winner);
            int loserPointsOnBoard = m_Board.SumOfPointsOnBoard(switchPlayer(i_Winner));

            i_Winner.m_Score += winnerPointsOnBoard - loserPointsOnBoard;
        }

        public Player getGameWinner()
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

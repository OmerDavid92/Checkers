using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Board
    {
        public char[,] m_Board { get; }

        private int m_BoardSize { get; }

        public Board(int i_BoardSize)
        {
            m_Board = new char[i_BoardSize, i_BoardSize];
            m_BoardSize = i_BoardSize;
        }

        public Player getMatchWinner()
        {
            return new Player('x', 'x', true, true);
        }

        private void insertPlayerSignToTheBoard(char i_PlayerSign, int i_lineToStart)
        {
            int numberOfRowsToOccupie = m_BoardSize / 2 - 1;
            for (int i = i_lineToStart; i < numberOfRowsToOccupie; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        this.m_Board[i, j] = ' ';
                    }
                    else
                    {
                        this.m_Board[i, j] = i_PlayerSign;
                    }
                }
            }
        }

        private void initBoard(ToolSign i_Player1, ToolSign i_Player2)
        {
            insertPlayerSignToTheBoard(i_Player1.m_trooperSign, 0);
            insertPlayerSignToTheBoard(i_Player2.m_trooperSign, this.m_BoardSize / 2 + 1);
        }

        private Point getCapturedPosition(Point i_Source, Point i_Destination)
        {
            Point eatenToolPosition = new Point();
            eatenToolPosition.m_X = (i_Destination.m_X - i_Source.m_X) / 2 + i_Source.m_X;
            eatenToolPosition.m_Y = (i_Destination.m_Y - i_Source.m_Y) / 2 + i_Source.m_Y;
            return eatenToolPosition;
        }

        private bool boundariesCheck(Point i_point)
        {
            bool isValid = false;
            if (i_point.m_X >= 0 && i_point.m_X < this.m_BoardSize)
            {
                if (i_point.m_Y >= 0 && i_point.m_Y < this.m_BoardSize)
                {
                    isValid = true;
                }
            }

            return isValid;
        }

        private bool TilesColorCheck(Point i_point)
        {
            bool isValid = false;
            if ((i_point.m_X + i_point.m_Y) % 2 != 0)
            {
                isValid = true;
            }

            return isValid;
        }
        private bool isInBoundaries(Point i_Source, Point i_Destination)
        {
            bool isInBoundaries = false;
            if (boundariesCheck(i_Source))
            {
                if (boundariesCheck(i_Destination))
                {
                    if (TilesColorCheck(i_Source))
                    {
                        if (TilesColorCheck(i_Destination))
                        {
                            isInBoundaries = true;
                        }
                    }
                }
            }
            return isInBoundaries;
        }

        private bool isToolOwnedByPlayer(Player i_Player, Point i_Source)
        {
            bool isOwnedByPlayer = false;
            int xPos = i_Source.m_X;
            int yPos = i_Source.m_Y;
            char playerTrooperSign = i_Player.m_ToolSign.m_trooperSign;
            char playerKingSign = i_Player.m_ToolSign.m_kingSign;
            if (m_Board[xPos, yPos] == playerTrooperSign || m_Board[xPos, yPos] == playerKingSign)
            {
                isOwnedByPlayer = true;
            }

            return isOwnedByPlayer;
        }

        private bool isDestinationOccupied(Point i_Destination)
        {
            bool isOccupied = true;
            if (m_Board[i_Destination.m_X, i_Destination.m_Y].CompareTo(" ") == 0)
            {
                isOccupied = false;
            }
            return isOccupied;
        }

        int directionFactorCalculator(Player i_Player)
        {
            int directionFactor = -1;
            if (i_Player.m_IsMoovingUp)
            {
                directionFactor = 1;
            }
            return directionFactor;
        }

        private bool isMoovingForward(Player i_PlayingPlayer, Point i_Source, Point i_Destination)
        {
            bool isMoovingForward = false;
            double xColumDifference = Math.Abs(i_Source.m_X - i_Destination.m_X);
            int directionFactor = directionFactorCalculator(i_PlayingPlayer);

            if (xColumDifference < 3 && xColumDifference > 0)
            {
                if (xColumDifference == 2)
                {
                    directionFactor *= 2;
                }

                if (!isDestinationOccupied(i_Destination))
                {
                    if (i_Source.m_X + directionFactor == i_Destination.m_X)
                    {
                        isMoovingForward = true;
                    }
                    else if (m_Board[i_Source.m_X, i_Source.m_Y].CompareTo(i_PlayingPlayer.m_ToolSign.m_kingSign) == 0)
                    {
                        directionFactor *= -1;
                        if (i_Source.m_X + directionFactor == i_Destination.m_X)
                        {
                            isMoovingForward = true;
                        }
                    }
                }
            }

            return isMoovingForward;
        }

        private bool isCaptureMade(Point i_Source, Point i_Destination)
        {
            bool isCaptureMade = false;
            Point eatenToolPosition;

            if (Math.Abs(i_Source.m_X - i_Destination.m_X) == 2)
            {
                eatenToolPosition = getCapturedPosition(i_Source, i_Destination);
                if (!(m_Board[i_Source.m_X, i_Source.m_Y].CompareTo(m_Board[eatenToolPosition.m_X, eatenToolPosition.m_Y]) == 0 ||
                  m_Board[eatenToolPosition.m_X, eatenToolPosition.m_Y].CompareTo(" ") == 0))
                {
                    isCaptureMade = true;
                }
            }

            return isCaptureMade;
        }

        private bool ToolCaptureOptionsCheck(int i_SourceXPosition, int i_SourceYPosition, int i_XColumnFactor, int i_YRowFactor)
        {
            bool captureOptionAvailable = false;
            int xPosOfEatenTool = i_SourceXPosition + i_XColumnFactor;
            int yPosOfEatenTool = i_SourceYPosition + i_YRowFactor;
            int captureToolFutureXPosition = i_SourceXPosition + i_XColumnFactor * 2;
            int captureToolFutureYPosition = i_SourceYPosition + i_YRowFactor * 2;

            if (captureToolFutureXPosition >= 0 && captureToolFutureXPosition <= (m_BoardSize - 1) &&
                captureToolFutureYPosition >= 0 && captureToolFutureYPosition <= (m_BoardSize - 1) &&
                 m_Board[captureToolFutureXPosition, captureToolFutureYPosition].CompareTo(" ") == 0
                )
            {
                if (!(m_Board[i_SourceXPosition, i_SourceYPosition].CompareTo(m_Board[xPosOfEatenTool, yPosOfEatenTool]) == 0 ||
              m_Board[xPosOfEatenTool, yPosOfEatenTool].CompareTo(" ") == 0))
                {
                    captureOptionAvailable = true;
                }
            }

            return captureOptionAvailable;
        }

        private bool ToolMoovingOptionsCheck(int i_SourceXPosition, int i_SourceYPosition, int i_XColumnFactor, int i_YRowFactor)
        {
            bool moovingOptionAvailable = false;
            int toolFutureXPosition = i_SourceXPosition + i_XColumnFactor;
            int toolFutureYPosition = i_SourceYPosition + i_YRowFactor;

            if (toolFutureXPosition >= 0 && toolFutureXPosition <= (m_BoardSize - 1) &&
                toolFutureYPosition >= 0 && toolFutureYPosition <= (m_BoardSize - 1) &&
                m_Board[toolFutureXPosition, toolFutureYPosition].CompareTo(" ") == 0)
            {
                moovingOptionAvailable = true;
            }

            return moovingOptionAvailable;
        }


        private bool isPlayerCanCapture(Player i_PlayingPlayer)
        {
            int directionFactor = directionFactorCalculator(i_PlayingPlayer);
            char playingPlayerTrooperSign = i_PlayingPlayer.m_ToolSign.m_trooperSign;
            char playingPlayerKingSign = i_PlayingPlayer.m_ToolSign.m_kingSign;
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    if (m_Board[i, j].CompareTo(playingPlayerTrooperSign) == 0)
                    {
                        if (ToolCaptureOptionsCheck(i, j, 1, directionFactor) ||
                            ToolCaptureOptionsCheck(i, j, -1, directionFactor))
                        {
                            return true;
                        }
                    }

                    if (m_Board[i, j].CompareTo(playingPlayerKingSign) == 0)
                    {
                        if (ToolCaptureOptionsCheck(i, j, 1, 1) ||
                            ToolCaptureOptionsCheck(i, j, -1, 1) ||
                            ToolCaptureOptionsCheck(i, j, 1, -1) ||
                            ToolCaptureOptionsCheck(i, j, -1, -1))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool isPlayerCanMove(Player i_PlayingPlayer)
        {
            int directionFactor = directionFactorCalculator(i_PlayingPlayer);
            char playingPlayerTrooperSign = i_PlayingPlayer.m_ToolSign.m_trooperSign;
            char playingPlayerKingSign = i_PlayingPlayer.m_ToolSign.m_kingSign;
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    if (m_Board[i, j].CompareTo(playingPlayerTrooperSign) == 0)
                    {
                        if (ToolMoovingOptionsCheck(i, j, 1, directionFactor) ||
                            ToolMoovingOptionsCheck(i, j, 1, directionFactor))
                            return true;
                    }

                    if (m_Board[i, j].CompareTo(playingPlayerKingSign) == 0)
                    {
                        if (ToolMoovingOptionsCheck(i, j, 1, 1) ||
                            ToolMoovingOptionsCheck(i, j, -1, 1) ||
                            ToolMoovingOptionsCheck(i, j, 1, -1) ||
                            ToolMoovingOptionsCheck(i, j, -1, -1))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool validateMove(Point i_Source, Point i_Destination, Player i_PlayingPlayer)
        {
            bool isValid = false;
            if (isInBoundaries(i_Source, i_Destination))
            {
                if (isToolOwnedByPlayer(i_PlayingPlayer, i_Source))
                {
                    if (isMoovingForward(i_PlayingPlayer, i_Source, i_Destination))
                    {
                        if (isCaptureMade(i_Source, i_Destination))
                        {
                            isValid = true;
                        }
                        else
                        {
                            if (!isPlayerCanCapture(i_PlayingPlayer))
                            {
                                isValid = true;
                            }
                        }
                    }
                }
            }

            return isValid;
        }

        private void updateBoard(Point i_Source, Point i_Destination)
        {
            Point eatenToolPosition;
            if (Math.Abs(i_Source.m_X - i_Destination.m_X) == 2) // A capture performed
            {
                eatenToolPosition = getCapturedPosition(i_Source, i_Destination);
                m_Board[eatenToolPosition.m_X, eatenToolPosition.m_Y] = ' ';
                m_Board[i_Destination.m_X, i_Destination.m_Y] = m_Board[i_Source.m_X, i_Source.m_Y];
                m_Board[i_Source.m_X, i_Source.m_Y] = ' ';
            }
            else
            {
                m_Board[i_Destination.m_X, i_Destination.m_Y] = m_Board[i_Source.m_X, i_Source.m_Y];
                m_Board[i_Source.m_X, i_Source.m_Y] = ' ';
            }
        }

        public bool MakeTurn(Point i_Source, Point i_Destination, Player i_PlayingPlayer)
        {
            bool isTurnPerformed = false;

            if (validateMove(i_Source, i_Destination, i_PlayingPlayer))
            {
                updateBoard(i_Source, i_Destination);
                isTurnPerformed = true;
            }

            return isTurnPerformed;
        }

        public bool IsValidMoveExist(Player i_PlayingPlayer)
        {
            bool isValidMoveExist = false;
            if (isPlayerCanCapture(i_PlayingPlayer))
            {
                isValidMoveExist = true;
            }
            else if (isPlayerCanMove(i_PlayingPlayer))
            {
                isValidMoveExist = true;
            }
            return isValidMoveExist;
        }
        public int SumOfPointsOnBoard(Player i_PlayingPlayer)
        {
            int sumOfPointsOnBoard = 0;
            char playingPlayerTrooperSign = i_PlayingPlayer.m_ToolSign.m_trooperSign;
            char playingPlayerKingSign = i_PlayingPlayer.m_ToolSign.m_kingSign;
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    if (m_Board[i, j].CompareTo(playingPlayerTrooperSign) == 0)
                    {
                        sumOfPointsOnBoard += 1;
                    }
                    else if (m_Board[i, j].CompareTo(playingPlayerKingSign) == 0)
                    {
                        sumOfPointsOnBoard += 4;
                    }
                }
            }
            return sumOfPointsOnBoard;
        }
    }
}

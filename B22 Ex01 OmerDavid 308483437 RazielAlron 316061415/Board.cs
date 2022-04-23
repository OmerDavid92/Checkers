using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Board
    {
        public char[,] m_Board { get; private set; }

        public Board(int i_BoardSize)
        {
            m_Board = new char[i_BoardSize, i_BoardSize];
        }

        private void insertPlayerSignToTheBoard(char i_PlayerSign, int i_lineToStart)
        {
            int numberOfRowsToOccupie = (m_Board.GetLength(0) / 2) - 1 + i_lineToStart;

            for (int i = i_lineToStart; i < numberOfRowsToOccupie; i++)
            {
                for (int j = 0; j < m_Board.GetLength(1); j++)
                {
                    if (tilesColorCheck(new Point(i, j)))
                    {
                        m_Board[i, j] = i_PlayerSign;
                    }
                }
            }
        }

        public void InitBoard(ToolSign i_Player1, ToolSign i_Player2)
        {
            /*m_Board = new char[6, 6]
                      { { '\0', 'K', '\0', '\0', '\0', '\0' },
                        { 'O', '\0', '\0', '\0', '\0', '\0' },
                        { '\0', 'O', '\0', 'O', '\0', '\0' },
                        { '\0', '\0', '\0', '\0', '\0', '\0' },
                        { '\0', '\0', '\0', '\0', '\0', '\0' },
                        { '\0', '\0', '\0', '\0', '\0', '\0' }
                      };*/

            insertPlayerSignToTheBoard(i_Player1.m_TrooperSign, m_Board.GetLength(0) / 2 + 1);
            insertPlayerSignToTheBoard(i_Player2.m_TrooperSign, 0);
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

            if (i_point.m_X >= 0 && i_point.m_X < m_Board.GetLength(0))
            {
                if (i_point.m_Y >= 0 && i_point.m_Y < m_Board.GetLength(0))
                {
                    isValid = true;
                }
            }

            return isValid;
        }

        private bool tilesColorCheck(Point i_point)
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
            bool isInBoundaries = true;

            if (!boundariesCheck(i_Source))
            {
                isInBoundaries = false;
            }
            else if (!boundariesCheck(i_Destination))
            {
                isInBoundaries = false;
            }
            else if (!tilesColorCheck(i_Source))
            {
                isInBoundaries = false;
            }
            else if (!tilesColorCheck(i_Destination))
            {
                isInBoundaries = true;
            }

            return isInBoundaries;
        }

        private bool isToolOwnedByPlayer(Player i_Player, Point i_Source)
        {
            int xPos = i_Source.m_X;
            int yPos = i_Source.m_Y;
            char playerTrooperSign = i_Player.m_ToolSign.m_TrooperSign;
            char playerKingSign = i_Player.m_ToolSign.m_KingSign;

            return m_Board[xPos, yPos] == playerTrooperSign || m_Board[xPos, yPos] == playerKingSign;
        }

        private bool isDestinationOccupied(Point i_Destination)
        {
            return m_Board[i_Destination.m_X, i_Destination.m_Y] == '\0';
        }

        int directionFactorCalculator(Player i_Player)
        {
            int directionFactor = 1;
            if (i_Player.m_IsMoovingUp)
            {
                directionFactor = -1;
            }
            return directionFactor;
        }

        private bool distanceCheck(Point i_Source, Point i_Destination)
        {
            int xDistance = Math.Abs(i_Source.m_X - i_Destination.m_X);
            int yDistance = Math.Abs(i_Source.m_Y - i_Destination.m_Y);

            return (xDistance == 1 || xDistance == 2)
                && (yDistance == 1 || yDistance == 2);
        }

        private bool isMovingForward(Player i_PlayingPlayer, Point i_Source, Point i_Destination)
        {
            bool isMovingForward = true;
            int directionFactor = directionFactorCalculator(i_PlayingPlayer);

            if (m_Board[i_Source.m_X, i_Source.m_Y] == i_PlayingPlayer.m_ToolSign.m_TrooperSign)
            {
                isMovingForward = (i_Destination.m_X - i_Source.m_X) * directionFactor > 0;
            }

            return isMovingForward;
        }

        public bool IsJumpedMoreThanOneTile(Point i_Source, Point i_Destination)
        {
            int xDistance = Math.Abs(i_Source.m_X - i_Destination.m_X);
            int yDistance = Math.Abs(i_Source.m_Y - i_Destination.m_Y);

            return xDistance > 1 || yDistance > 1;
        }

        private bool isCaptureMade(Point i_Source, Point i_Destination)
        {
            bool isCaptureMade = false;
            Point eatenToolPosition;

            if (Math.Abs(i_Source.m_X - i_Destination.m_X) == 2)
            {
                eatenToolPosition = getCapturedPosition(i_Source, i_Destination);
                if (!(m_Board[i_Source.m_X, i_Source.m_Y] == m_Board[eatenToolPosition.m_X, eatenToolPosition.m_Y]
                    || m_Board[eatenToolPosition.m_X, eatenToolPosition.m_Y] == '\0'))
                {
                    isCaptureMade = true;
                }
            }

            return isCaptureMade;
        }

        public bool ToolCaptureOptionsCheck(int i_SourceXPosition, int i_SourceYPosition, int i_XColumnFactor, int i_YRowFactor)
        {
            bool captureOptionAvailable = false;
            int xPosOfEatenTool = i_SourceXPosition + i_XColumnFactor;
            int yPosOfEatenTool = i_SourceYPosition + i_YRowFactor;
            int captureToolFutureXPosition = i_SourceXPosition + i_XColumnFactor * 2;
            int captureToolFutureYPosition = i_SourceYPosition + i_YRowFactor * 2;

            if (captureToolFutureXPosition >= 0 && captureToolFutureXPosition <= (m_Board.GetLength(0) - 1) &&
                captureToolFutureYPosition >= 0 && captureToolFutureYPosition <= (m_Board.GetLength(0) - 1) &&
                 m_Board[captureToolFutureXPosition, captureToolFutureYPosition] == '\0')
            {
                if (!(m_Board[i_SourceXPosition, i_SourceYPosition] == m_Board[xPosOfEatenTool, yPosOfEatenTool]
                    || m_Board[xPosOfEatenTool, yPosOfEatenTool] == '\0'))
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

            if (toolFutureXPosition >= 0 && toolFutureXPosition <= (m_Board.GetLength(0) - 1) &&
                toolFutureYPosition >= 0 && toolFutureYPosition <= (m_Board.GetLength(0) - 1) &&
                m_Board[toolFutureXPosition, toolFutureYPosition] == '\0')
            {
                moovingOptionAvailable = true;
            }

            return moovingOptionAvailable;
        }

        public bool IsToolCanCapture(Player i_PlayingPlayer, Point i_SourcePosition)
        {
            int directionFactor = directionFactorCalculator(i_PlayingPlayer);
            char playingPlayerTrooperSign = i_PlayingPlayer.m_ToolSign.m_TrooperSign;
            char playingPlayerKingSign = i_PlayingPlayer.m_ToolSign.m_KingSign;
            char currentTool = m_Board[i_SourcePosition.m_X, i_SourcePosition.m_Y];

            if (currentTool == playingPlayerTrooperSign)
            {
                if (ToolCaptureOptionsCheck(i_SourcePosition.m_X, i_SourcePosition.m_Y, directionFactor, 1) ||
                    ToolCaptureOptionsCheck(i_SourcePosition.m_X, i_SourcePosition.m_Y, directionFactor, -1))
                {
                    return true;
                }
            }
            else if(currentTool == playingPlayerKingSign)
            {
                if (ToolCaptureOptionsCheck(i_SourcePosition.m_X, i_SourcePosition.m_Y, 1, 1) ||
                    ToolCaptureOptionsCheck(i_SourcePosition.m_X, i_SourcePosition.m_Y, -1, 1) ||
                    ToolCaptureOptionsCheck(i_SourcePosition.m_X, i_SourcePosition.m_Y, 1, -1) ||
                    ToolCaptureOptionsCheck(i_SourcePosition.m_X, i_SourcePosition.m_Y, -1, -1))
                {
                    return true;
                }
            }

            return false;

        }

        public bool IsPlayerCanCapture(Player i_PlayingPlayer)
        {
            int directionFactor = directionFactorCalculator(i_PlayingPlayer);
            char playingPlayerTrooperSign = i_PlayingPlayer.m_ToolSign.m_TrooperSign;
            char playingPlayerKingSign = i_PlayingPlayer.m_ToolSign.m_KingSign;
            for (int i = 0; i < m_Board.GetLength(0); i++)
            {
                for (int j = 0; j < m_Board.GetLength(0); j++)
                {
                    if(IsToolCanCapture(i_PlayingPlayer, new Point(i, j)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool isPlayerCanMove(Player i_PlayingPlayer)
        {
            int directionFactor = directionFactorCalculator(i_PlayingPlayer);
            char playingPlayerTrooperSign = i_PlayingPlayer.m_ToolSign.m_TrooperSign;
            char playingPlayerKingSign = i_PlayingPlayer.m_ToolSign.m_KingSign;
            for (int i = 0; i < m_Board.GetLength(0); i++)
            {
                for (int j = 0; j < m_Board.GetLength(0); j++)
                {
                    if (m_Board[i, j] == playingPlayerTrooperSign)
                    {
                        if (ToolMoovingOptionsCheck(i, j, directionFactor, 1) ||
                            ToolMoovingOptionsCheck(i, j, directionFactor, -1))
                            return true;
                    }
                    else if (m_Board[i, j] == playingPlayerKingSign)
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

        public static string GetTurnErrorMessage(string i_ErrorMassage)
        {
            return String.Format("Error: {0}", i_ErrorMassage);
        }

        public bool ValidateMove(Point i_Source, Point i_Destination, Player i_PlayingPlayer, ref string o_ErrorMessage)
        {
            bool isValid = true;

            if (!isInBoundaries(i_Source, i_Destination))
            {
                o_ErrorMessage = GetTurnErrorMessage("Destination is not in boundaries");
                isValid = false;
            }
            else if (!distanceCheck(i_Source, i_Destination))
            {
                o_ErrorMessage = GetTurnErrorMessage("Distance error");
                isValid = false;
            }
            else if (!isToolOwnedByPlayer(i_PlayingPlayer, i_Source))
            {
                o_ErrorMessage = GetTurnErrorMessage("The tool is not owned By the player");
                isValid = false;
            }
            else if(!isMovingForward(i_PlayingPlayer, i_Source, i_Destination))
            {
                o_ErrorMessage = GetTurnErrorMessage("Tool must move forward");
                isValid = false;
            }
            else if(!isDestinationOccupied(i_Destination))
            {
                o_ErrorMessage = GetTurnErrorMessage("Destination position occupied");
                isValid = false;
            }
            else if(!isCaptureMade(i_Source, i_Destination) && IsJumpedMoreThanOneTile(i_Source, i_Destination))
            {
                o_ErrorMessage = GetTurnErrorMessage("Invalid jump");
                isValid = false;
            }
            else if(!isCaptureMade(i_Source, i_Destination) && IsPlayerCanCapture(i_PlayingPlayer))
            {
                o_ErrorMessage = GetTurnErrorMessage("A capture option is available");
                isValid = false;
            }

            return isValid;
        }

        private bool isTrooperBecomesAKing(Point i_Destination, Player i_PlayingPlayer)
        {
            bool isTrooperBecomesAKing = false;
            if (i_PlayingPlayer.m_IsMoovingUp && i_Destination.m_X == 0 ||
                (!i_PlayingPlayer.m_IsMoovingUp) && i_Destination.m_X == (m_Board.GetLength(0)-1))
            {
                isTrooperBecomesAKing = true;
            }
            return isTrooperBecomesAKing;
        }

        public void MakeTurn(Point i_Source, Point i_Destination, Player i_PlayingPlayer)
        {
            Point eatenToolPosition;
            char currentTool = m_Board[i_Source.m_X, i_Source.m_Y];
            m_Board[i_Source.m_X, i_Source.m_Y] = '\0';

            if (Math.Abs(i_Source.m_X - i_Destination.m_X) == 2) // A capture performed
            {
                eatenToolPosition = getCapturedPosition(i_Source, i_Destination);
                m_Board[eatenToolPosition.m_X, eatenToolPosition.m_Y] = '\0';
            }

            if (isTrooperBecomesAKing(i_Destination, i_PlayingPlayer))
            {
                m_Board[i_Destination.m_X, i_Destination.m_Y] = i_PlayingPlayer.m_ToolSign.m_KingSign;
            }
            else
            {
                m_Board[i_Destination.m_X, i_Destination.m_Y] = currentTool;
            }            
        }

        public bool IsValidMoveExist(Player i_PlayingPlayer)
        {
            bool isValidMoveExist = false;
            if (IsPlayerCanCapture(i_PlayingPlayer))
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
            char playingPlayerTrooperSign = i_PlayingPlayer.m_ToolSign.m_TrooperSign;
            int playingPlayerTrooperScore = i_PlayingPlayer.m_ToolSign.m_TrooperScore;
            char playingPlayerKingSign = i_PlayingPlayer.m_ToolSign.m_KingSign;
            int playingPlayerKingScore = i_PlayingPlayer.m_ToolSign.m_KingScore;

            for (int i = 0; i < m_Board.GetLength(0); i++)
            {
                for (int j = 0; j < m_Board.GetLength(0); j++)
                {
                    if (m_Board[i, j] == playingPlayerTrooperSign)
                    {
                        sumOfPointsOnBoard += playingPlayerTrooperScore;
                    }
                    else if (m_Board[i, j] == playingPlayerKingSign)
                    {
                        sumOfPointsOnBoard += playingPlayerKingScore;
                    }
                }
            }
            return sumOfPointsOnBoard;
        }
    }
}

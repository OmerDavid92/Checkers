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

        private void insertPlayerSignToTheBoard(char i_PlayerSign, int i_lineToStart)
        {
            int numberOfRowsToOccupie = this.m_BoardSize / 2 - 1;
            for (int i = i_lineToStart; i < numberOfRowsToOccupie; i++)
            {
                for (int j = 0; j < this.m_BoardSize; j++)
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
            insertPlayerSignToTheBoard(i_Player2.m_trooperSign, this.m_BoardSize/2+1);
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
            if ((i_point.m_X+ i_point.m_Y)%2!=0)
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
            if (m_Board[xPos,yPos] == playerTrooperSign || m_Board[xPos, yPos] == playerKingSign)
            {
                isOwnedByPlayer = true;
            }

            return isOwnedByPlayer;
        }

        private bool isMoovingForward(Point i_Source, Point i_Destination)
        {
            return true;
        }

        private bool isPlayerCanCapture(ToolSign i_ToolSign)
        {
            return true;
        }

        private bool isCaptureMade(Point i_Source, Point i_Destination)
        {
            return true;
        }


        private bool validateMove(Point i_Source, Point i_Destination, Player i_PlayingPlayer, ToolSign i_ToolSign)
        {
            bool isValid = false;
            if (isInBoundaries(i_Source, i_Destination))
            {
                if (isToolOwnedByPlayer(i_PlayingPlayer, i_Source))
                {
                    if (isMoovingForward(i_Source, i_Destination))
                    {
                        if (isPlayerCanCapture(i_ToolSign))
                        {
                            if (isCaptureMade(i_Source, i_Destination))
                            {
                                isValid = true;
                            }
                        }
                    }
                }
            }

            return isValid;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Player
    {
        public string m_PlayerName { get; }
        public ToolSign m_ToolSign { get; }
        public Enum.PlayerType m_PlayerType { get; } //To check if private is needed
        public bool m_IsMoovingUp { get; }
        public int m_Score { get; set; }

        public Player(char i_TrooperSign, char i_kingSign, Enum.PlayerType i_PlayerType, bool i_IsMoovingUp, string i_PlayerName = "")
        {
            m_PlayerName = i_PlayerName;
            m_ToolSign = new ToolSign(i_TrooperSign, i_kingSign);
            m_PlayerType = i_PlayerType;
            m_IsMoovingUp = i_IsMoovingUp;
        }

        public bool TryGetTurn(Board i_Board, ref Turn o_Turn)
        {
            bool isPlayerPlayed = true;

            if(m_PlayerType == Enum.PlayerType.Human)
            {
                isPlayerPlayed = UserInterface.TryGetUserInputTurn(this, ref o_Turn);
            }
            else
            {
                UserInterface.PrintWaitForTurn(this);
                System.Threading.Thread.Sleep(3000);
                calculateTurn(i_Board, ref o_Turn);
                UserInterface.PrintPCTurn(o_Turn);
            }

            return isPlayerPlayed;
        }

        private void calculateTurn(Board i_Board, ref Turn o_Turn)
        {
            Random random = new Random();
            int randomNumber = 0;
            List<Turn> capturableTurns = getCapturableTurns(i_Board);
            List<Turn> uncapturableTurns = null;

            if (capturableTurns.Count == 0)
            {
                uncapturableTurns = getUcapturableTurns(i_Board);
                if (uncapturableTurns.Count > 0)
                {
                    randomNumber = random.Next(uncapturableTurns.Count);
                    o_Turn = uncapturableTurns[randomNumber];
                }
            }
            else
            {
                randomNumber = random.Next(capturableTurns.Count);
                o_Turn = capturableTurns[randomNumber];
            }
        }

        private List<Turn> getUcapturableTurns(Board i_Board)
        {
            List<Turn> validTurns = new List<Turn>();
            Point temporarySource;

            for (int i = 0; i < i_Board.m_Board.GetLength(0); i++)
            {
                for (int j = 0; j < i_Board.m_Board.GetLength(1); j++)
                {
                    temporarySource = new Point(i, j);
                    fillUncapturableTurnsBySourcePoint(i_Board, temporarySource, validTurns);
                }
            }

            return validTurns;
        }

        private List<Turn> getCapturableTurns(Board i_Board)
        {
            List<Turn> validTurns = new List<Turn>();
            Point temporarySource;

            for (int i = 0; i < i_Board.m_Board.GetLength(0); i++)
            {
                for (int j = 0; j < i_Board.m_Board.GetLength(1); j++)
                {
                    temporarySource = new Point(i, j);
                    fillCapturableTurnsBySourcePoint(i_Board, temporarySource, validTurns);
                }
            }

            return validTurns;
        }

        private void appendTurnIfValid(Board i_Board, Turn i_Turn, List<Turn> o_ValidTurns)
        {
            string errorMessage = "";

            if (i_Board.ValidateMove(i_Turn.m_Source, i_Turn.m_Destination, this, ref errorMessage))
            {
                o_ValidTurns.Add(i_Turn);
            }
        }

        private void fillUncapturableTurnsBySourcePoint(Board i_Board, Point i_Source, List<Turn> o_ValidTurns)
        {
            Turn TemporaryTurn = null;
            int moveFactor = getFactor();

            if (i_Board.m_Board[i_Source.m_X, i_Source.m_Y] == m_ToolSign.m_TrooperSign ||
                i_Board.m_Board[i_Source.m_X, i_Source.m_Y] == m_ToolSign.m_KingSign)
            {
                TemporaryTurn = new Turn(i_Source, new Point(i_Source.m_X + moveFactor, i_Source.m_Y + moveFactor));
                appendTurnIfValid(i_Board, TemporaryTurn, o_ValidTurns);
                TemporaryTurn = new Turn(i_Source, new Point(i_Source.m_X + moveFactor, i_Source.m_Y - moveFactor));
                appendTurnIfValid(i_Board, TemporaryTurn, o_ValidTurns);
            }

            if (i_Board.m_Board[i_Source.m_X, i_Source.m_Y] == m_ToolSign.m_KingSign)
            {
                TemporaryTurn = new Turn(i_Source, new Point(i_Source.m_X - moveFactor, i_Source.m_Y + moveFactor));
                appendTurnIfValid(i_Board, TemporaryTurn, o_ValidTurns);
                TemporaryTurn = new Turn(i_Source, new Point(i_Source.m_X - moveFactor, i_Source.m_Y - moveFactor));
                appendTurnIfValid(i_Board, TemporaryTurn, o_ValidTurns);
            }
        }

        private void fillCapturableTurnsBySourcePoint(Board i_Board, Point i_Source, List<Turn> o_ValidTurns)
        {
            Turn TemporaryTurn = null;
            int eatFactor = getFactor() * 2;

            if (i_Board.m_Board[i_Source.m_X, i_Source.m_Y] == m_ToolSign.m_TrooperSign ||
                i_Board.m_Board[i_Source.m_X, i_Source.m_Y] == m_ToolSign.m_KingSign)
            {
                TemporaryTurn = new Turn(i_Source, new Point(i_Source.m_X + eatFactor, i_Source.m_Y + eatFactor));
                appendTurnIfValid(i_Board, TemporaryTurn, o_ValidTurns);
                TemporaryTurn = new Turn(i_Source, new Point(i_Source.m_X + eatFactor, i_Source.m_Y - eatFactor));
                appendTurnIfValid(i_Board, TemporaryTurn, o_ValidTurns);
            }

            if (i_Board.m_Board[i_Source.m_X, i_Source.m_Y] == m_ToolSign.m_KingSign)
            {
                TemporaryTurn = new Turn(i_Source, new Point(i_Source.m_X - eatFactor, i_Source.m_Y + eatFactor));
                appendTurnIfValid(i_Board, TemporaryTurn, o_ValidTurns);
                TemporaryTurn = new Turn(i_Source, new Point(i_Source.m_X - eatFactor, i_Source.m_Y - eatFactor));
                appendTurnIfValid(i_Board, TemporaryTurn, o_ValidTurns);
            }
        }

        private int getFactor()
        {
            int factor = 1;

            if(m_IsMoovingUp)
            {
                factor = -1;
            }

            return factor;
        }
    }
}

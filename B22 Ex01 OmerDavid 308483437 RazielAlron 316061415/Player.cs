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

        public void GetTurn(ref Point o_SourcePosition, ref Point o_DestinationPosition)
        {
            if(m_PlayerType == Enum.PlayerType.Human)
            {
                UserInterface.GetUserInputTurn(m_PlayerName, ref o_SourcePosition, ref o_DestinationPosition);
            }
            else
            {
                this.calculateTurn(o_SourcePosition, o_DestinationPosition);
            }
        }

        private void calculateTurn(Point o_SourcePosition, Point o_DestinationPosition)
        {

        }
    }
}

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
        public bool m_IsHuman { get; } //To check if private is needed

        public Player(char i_TrooperSign, char i_kingSign, bool i_IsHuman, string i_PlayerName = "")
        {
            m_PlayerName = i_PlayerName;
            m_ToolSign = new ToolSign(i_TrooperSign, i_kingSign);
            m_IsHuman = i_IsHuman;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class ToolSign
    {
        public char m_TrooperSign { get; }
        public int m_TrooperScore { get; } = 1;
        public char m_KingSign { get; }
        public int m_KingScore { get; } = 4;

        public ToolSign(char i_trooperSign, char i_KingSign)
        {
            m_TrooperSign = i_trooperSign;
            m_KingSign = i_KingSign;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class ToolSign
    {
        public char m_trooperSign { get; }
        public int m_trooperPoint = 1;
        public char m_kingSign { get; }
        public int m_KingPoint = 4;
        public ToolSign(char i_trooperSign, char i_KingSign)
        {
            m_trooperSign = i_trooperSign;
            m_kingSign = i_KingSign;
        }

    }
}

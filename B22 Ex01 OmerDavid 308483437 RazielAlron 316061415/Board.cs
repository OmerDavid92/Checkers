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

        public Board(int i_BoardSize)
        {
            m_Board = new char[i_BoardSize, i_BoardSize];
        }
    }
}

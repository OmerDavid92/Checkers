using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Game
    {
        enum GameStatus
        {
            Player1Wins,
            Player2Wins,
            Tie,
            Playing
        }

        private GameStatus m_GameStatus { get; set; }
        private Board m_Board { get; }
        private Player m_Player1 { get; }
        private Player m_Player2 { get; }

        public Game() 
        {
            //gameInit();    
        }
    }
}

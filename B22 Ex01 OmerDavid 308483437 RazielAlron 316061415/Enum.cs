using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Enum
    {
        public enum GameStatus
        {
            Player1Wins,
            Player2Wins,
            Tie,
            Playing
        }
        public enum BoardSize
        {
            Small = 6,
            Medium = 8,
            Large = 10
        }
        public enum PlayerType
        {
            Human = 1,
            PC
        }
        public enum Player1Tools
        {
            Trooper = 'O',
            King = 'U'
        }
        public enum Player2Tools
        {
            Trooper = 'X',
            King = 'K'
        }
    }
}

namespace Checkers
{
    public class Enum
    {
        public enum BoardSize
        {
            Small = 6,
            Medium = 8,
            Large = 10
        }

        public enum PlayerType
        {
            PC = 1,
            Human
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

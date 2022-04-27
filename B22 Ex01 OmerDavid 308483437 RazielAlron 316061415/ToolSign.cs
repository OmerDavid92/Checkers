namespace Checkers
{
    public class ToolSign
    {
        public char m_TrooperSign { get; private set; }

        public int m_TrooperScore { get; } = 1;

        public char m_KingSign { get; private set; }

        public int m_KingScore { get; } = 4;

        public ToolSign(char i_trooperSign, char i_KingSign)
        {
            m_TrooperSign = i_trooperSign;
            m_KingSign = i_KingSign;
        }
    }
}

namespace Checkers
{
    struct Point
    {
        public int m_X { get; set; }
        public int m_Y { get; set; }
        public Point(string i_UserInput)
        {
            m_X = i_UserInput[0] - 'A';
            m_Y = i_UserInput[1] - 'a';
        }
    }
}

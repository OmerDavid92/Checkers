namespace Checkers
{
    public class Turn
    {
        public Point m_Source { get; private set; }

        public Point m_Destination { get; private set; }

        public bool m_ShouldCaptureAgain { get; set; } = false;

        public Turn(Point i_Source, Point i_Destination)
        {
            m_Source = i_Source;
            m_Destination = i_Destination;
        }
    }
}

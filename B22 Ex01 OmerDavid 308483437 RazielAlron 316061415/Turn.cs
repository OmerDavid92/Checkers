using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Turn
    {
        public Point m_Source { get; private set; }
        public Point m_Destination { get; private set; }
        public bool m_ValidBoundries { get; set; } = false;
        public bool m_ValidDistance { get; set; } = false;
        public bool m_ValidToolOwner { get; set; } = false;
        public bool m_ValidJumpDistance { get; set; } = false;
        public bool m_ValidCapture { get; set; } = false;
        public bool m_ShouldCaptureAgain { get; set; } = false;

        public Turn(Point i_Source, Point i_Destination)
        {
            m_Source = i_Source;
            m_Destination = i_Destination;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLibrary.Exceptions
{
    public class FrameCompleteException : InvalidOperationException
    {
        public FrameCompleteException()
        :base("The frame is complete since you already earned a strike.")
        {
        }
    }
}
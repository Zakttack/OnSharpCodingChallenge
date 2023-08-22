using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLibrary.Models
{
    public struct Turn
    {
        public Turn(int frameNumber = 0, int shotNumber = 0)
        {
            FrameNumber = frameNumber;
            ShotNumber = shotNumber;
        }

        public int FrameNumber
        {
            get;
            private set;
        }

        public int ShotNumber
        {
            get;
            private set;
        }

        public static Turn Complete
        {
            get
            {
                return new(10,0);
            }
        }

        public override readonly bool Equals(object obj)
        {
            if (obj is not Turn)
            {
                return false;
            }
            return this == (Turn)obj;
        }

        public override readonly int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static Turn operator++(Turn a)
        {
            int shotMax = a.FrameNumber == 9 ? 2 : 1;
            if (a.ShotNumber < shotMax)
            {
                a.ShotNumber++;
            }
            else
            {
                a.FrameNumber++;
                a.ShotNumber = 0;
            }
            return a;
        }

        public static Turn operator--(Turn a)
        {
            throw new InvalidOperationException("Can't go back in Turns");
        }

        public static bool operator==(Turn a, Turn b)
        {
            return a.FrameNumber == b.FrameNumber && a.ShotNumber == b.ShotNumber;
        }

        public static bool operator!=(Turn a, Turn b)
        {
            return a.FrameNumber != b.FrameNumber || a.ShotNumber != b.ShotNumber;
        }
    }
}
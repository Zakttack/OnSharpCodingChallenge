using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLibrary.Models
{
    public class Frame
    {
        public Frame(bool isTenthFrame)
        {
            Shots = new Shot[isTenthFrame ? 3 : 2];
            Score = 0;
        }

        public Shot[] Shots
        {
            get;
            private set;
        }

        public int Score
        {
            get;
            set;
        }
    }
}
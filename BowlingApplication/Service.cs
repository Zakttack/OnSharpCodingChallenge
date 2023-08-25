using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using BowlingApplication.Models;
using BowlingLibrary;
using BowlingLibrary.Models;

namespace BowlingApplication
{
    public class Service
    {
        public static Round Players
        {
            get;
            set;
        }

        public static int CurrentIndex
        {
            get;
            set;
        }

        public static bool PlayerHasRolled
        {
            get;
            set;
        }

        public static bool HaveSameFrameNumber
        {
            get
            {
                IList<Player> bowlers = Players.ToList();
                int frameNumber = bowlers[0].TurnStatus.FrameNumber;
                for (int i = 1; i < bowlers.Count; i++)
                {
                    if (bowlers[i].TurnStatus.FrameNumber != frameNumber)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
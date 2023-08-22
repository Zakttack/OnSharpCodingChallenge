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

        public static Player ChooseRandom()
        {
            IList<Player> bowlers = Players.ToList();
            if (CurrentIndex == bowlers.Count - 1)
            {
                return null;
            }
            Random randPlayer = new();
            int index;
            do
            {
                index = randPlayer.Next(bowlers.Count);
            } while (index <= CurrentIndex);
            return bowlers[index];
        }
    }
}
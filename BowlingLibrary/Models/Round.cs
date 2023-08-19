using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLibrary.Models
{
    public class Round
    {
        public Round()
        {
            Game = new Dictionary<string,Frame[]>();
        }

        public IDictionary<string,Frame[]> Game
        {
            get;
            private set;
        }
    }
}
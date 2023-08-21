using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLibrary.Exceptions
{
    public class PlayerNotFoundException : ArgumentException
    {
        public PlayerNotFoundException(string name)
        :base($"{name} doesn't exist in the game.")
        {

        }

        public PlayerNotFoundException(string name, Exception exception)
        :base($"{name} doesn't exist in the game.", exception)
        {
            
        }
    }
}
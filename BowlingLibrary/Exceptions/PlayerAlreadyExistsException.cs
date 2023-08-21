using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLibrary.Exceptions
{
    public class PlayerAlreadyExistsException : ArgumentException
    {
        public PlayerAlreadyExistsException(string name)
        :base($"{name} already exists in the game.")
        {

        }

        public PlayerAlreadyExistsException(string name, Exception exception)
        :base($"{name} already exists in the game.", exception)
        {
            
        }
    }
}
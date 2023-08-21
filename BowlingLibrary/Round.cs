using BowlingLibrary.Exceptions;
using BowlingLibrary.Models;
using System.Collections;

namespace BowlingLibrary
{
    public class Round : IEnumerable<Player>
    {
        private readonly IDictionary<string,Frame[]> game;

        public Round()
        {
            game = new Dictionary<string,Frame[]>();
        }

        public Player this[string name]
        {
            get
            {
                foreach (KeyValuePair<string,Frame[]> p in game)
                {
                    if (p.Key == name)
                    {
                        return new(p.Key, p.Value);
                    }
                }
                throw new PlayerNotFoundException(name);
            }
        }

        public void AddPlayer(string name)
        {
            try
            {
                Player p = new(name);
                game.Add(p.Info);
            }
            catch (NotSupportedException ex)
            {
                throw new PlayerAlreadyExistsException(name, ex);
            }
        }

        public IEnumerator<Player> GetEnumerator()
        {
            ICollection<Player> players = new List<Player>();
            foreach (KeyValuePair<string,Frame[]> p in game)
            {
                players.Add(new(p.Key, p.Value));
            }
            return players.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontEatTheChiliLib.Models
{
    /// <summary>
    /// An AI Class that always chooses a random number of remaining candies
    /// </summary>
    public class RandomGameAI : IGameAI
    {
        static Random r = new Random();
        public Moves TakeTurn(Moves previousMove, int candyCount)
        {
            int max = candyCount < 3 ? candyCount + 1 : 4;
            int randomChoice = r.Next(1, max); // max is exclusive
            return (Moves)Enum.Parse(typeof(Moves), randomChoice.ToString());
        }
    }
}

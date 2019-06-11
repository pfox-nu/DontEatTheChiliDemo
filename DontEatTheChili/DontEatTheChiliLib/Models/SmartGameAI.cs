using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontEatTheChiliLib.Models
{
    public class SmartGameAI : IGameAI
    {
        /// <summary>
        ///  An AI that should always win if it goes first.
        /// </summary>
        public Moves TakeTurn(Moves previousMove, int candyCount)
        {
            Moves aiChoice = Moves.Three;
            if (previousMove == Moves.One)
            {
                aiChoice = Moves.Three;
            }
            else if (previousMove == Moves.Two)
            {
                aiChoice = Moves.Two;
            }
            else if (previousMove == Moves.Three)
            {
                aiChoice = Moves.One;
            }
            return aiChoice;
        }
    }
}

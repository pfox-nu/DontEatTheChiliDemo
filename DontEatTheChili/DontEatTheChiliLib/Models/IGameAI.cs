using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontEatTheChiliLib.Models
{
    interface IGameAI
    {
        Moves TakeTurn(Moves previousMove, int candyCount);
    }
}

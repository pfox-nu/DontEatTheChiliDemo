using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DontEatTheChiliLib
{
    public enum GameModes
    {
        TwoPlayer,
        SinglePlayer,
        SinglePlayerHard
    }

    public enum Players
    {
        PlayerOne,
        PlayerTwo
    }

    public enum Moves
    {
        One = 1,
        Two = 2,
        Three = 3
    }

    public delegate void MessageDelegate(string message);
}

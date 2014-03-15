using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokePlanet
{
    class Clock
    {
        public const int TargetFPS = 60;
        public static int FramesToMillis(int frames)
        {
            return 1000*frames/TargetFPS;
        }
    }
}

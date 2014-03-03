using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PokePlanet
{
    class OverworldState : GameState
    {
        public static Player player;

        

        public void Initialize()
        {
            //player and movement
            player = new Player();
            
        }
    }
}

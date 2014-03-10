using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace PokePlanet
{
    class SpriteManager
    {
        public static Dictionary<String, SpriteSheet> SpriteSheetDictionary;
        public static Dictionary<String, Animation> AnimationDictionary;

        public static void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            SpriteSheet playerSheet = SpriteSheet.FromFile(content, "SpriteXML\\LinkSheet.sprites");
            SpriteSheet linkSheet = SpriteSheet.FromFile(content, "Content/sprites/LinkSheet.sprites");
            //playerAnimation.Initialize(playerTexture, Vector2.Zero, 115, 69, 8, 30, Color.White, scale, true);
            Vector2 playerPosition = new Vector2(graphicsDevice.Viewport.TitleSafeArea.X + graphicsDevice.Viewport.TitleSafeArea.Width / 2,
                graphicsDevice.Viewport.TitleSafeArea.Y + graphicsDevice.Viewport.TitleSafeArea.Height / 2);
            OverworldState.player.Initialize(content, playerSheet, playerPosition);

            //Texture2D cuboneTexture = Content.Load<Texture2D>("Content\\Graphics\\cuboneMarowak");
            SpriteSheet cuboneSheet = SpriteSheet.FromFile(content, "Content\\sprites\\CuboneMarowak.sprites");

            //background
            Game1.bgLayer1.Initialize(content, "Graphics/bgLayer1", graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, -1);
            Game1.bgLayer2.Initialize(content, "Graphics/bgLayer2", graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, -2);
            Game1._mainBackground = content.Load<Texture2D>("Graphics/mainbackground");
            Game1._rectBackground = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
        }

        public static void LoadFromFile(String stemName)
        {
        }
    }
}

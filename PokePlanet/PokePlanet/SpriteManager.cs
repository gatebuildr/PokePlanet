﻿using System;
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
        public static Dictionary<String, SpriteSheet> SpriteSheetDictionary = new Dictionary<string,SpriteSheet>();
        public static Dictionary<String, Animation> AnimationDictionary = new Dictionary<string, Animation>();

        public static void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            LoadFromFile(content, "LinkSheet");
            LoadFromFile(content, "CuboneMarowak");
//            SpriteSheet playerSheet = SpriteSheetDictionary["LinkSheet.sprites"];
            SpriteSheet playerSheet = SpriteSheetDictionary["CuboneMarowak.sprites"];
            Vector2 playerPosition = new Vector2(graphicsDevice.Viewport.TitleSafeArea.X + graphicsDevice.Viewport.TitleSafeArea.Width / 2,
                graphicsDevice.Viewport.TitleSafeArea.Y + graphicsDevice.Viewport.TitleSafeArea.Height / 2);
            OverworldState.player.Initialize(playerSheet, playerPosition);

            SpriteSheet cuboneSheet = SpriteSheet.FromFile(content, "Content\\sprites\\CuboneMarowak.sprites");
            
            //background
            Game1.bgLayer1.Initialize(content, "Graphics/bgLayer1", graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, -1);
            Game1.bgLayer2.Initialize(content, "Graphics/bgLayer2", graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, -2);
            Game1._mainBackground = content.Load<Texture2D>("Graphics/mainbackground");
            Game1._rectBackground = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
        }

        public static void LoadFromFile(ContentManager content, String stemName)
        {
            string basePath = "Content/sprites/" + stemName;
            SpriteSheet sheet = SpriteSheet.FromFile(content, basePath + ".sprites");
            SpriteSheetDictionary.Add(stemName + ".sprites", sheet);
            List<Animation> animations = AnimationSet.LoadAnimations(basePath + ".anim");
            foreach (Animation animation in animations)
            {
                Console.WriteLine("Loading animation " + animation.Name);
                animation.Texture = sheet.Texture;
                string animPath = stemName + "/" + animation.Name;
                AnimationDictionary.Add(animPath, animation);
                Console.WriteLine("added " + animPath);
            }
        }

        public static Animation GetAnimation(String stemName, String animationName)
        {
            return AnimationDictionary[stemName + "/" + animationName];
        }

        public static SpriteCoords GetSprite(String stemName, String spriteName)
        {
            return SpriteSheetDictionary[stemName].GetSprite(spriteName);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PokePlanet
{
    public class Player
    {
        public SpriteSheet PlayerSheet;
        public AnimatedSprite CurrentSprite;
        public const float PlayerMoveSpeed = 8.0f;
        //public Animation PlayerAnimation;
        public Texture2D PlayerTexture;
        public Vector2 Position;
        public bool Active;
        public int Health;
        public int Width
        {
            get { return CurrentSprite.FrameWidth; }
        }

        public int Height
        {
            get { return CurrentSprite.FrameHeight; }
        }

        public void Initialize(ContentManager content, SpriteSheet sheet, Vector2 position)
        {
            PlayerSheet = sheet;
            PlayerTexture = content.Load<Texture2D>(sheet.SheetPath);
            SetAnimation("unarmed_down");
            Position = position;
            Active = true;
            Health = 100;
        }

        public void SetAnimation(String name)
        {
            CurrentSprite = PlayerSheet.GetAnimation("unarmed_down");
            CurrentSprite.Initialize(PlayerTexture, Position, 1f);
        }

        public void Update(GameTime gameTime)
        {
            CurrentSprite.Position = Position;
            CurrentSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, float scale)
        {
            CurrentSprite.Draw(PlayerTexture, spriteBatch, scale);
        }
    }
}

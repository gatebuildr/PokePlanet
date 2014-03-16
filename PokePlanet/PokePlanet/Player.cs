using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PokePlanet
{
    public class Player
    {
        public SpriteSheet PlayerSheet;
        public MovingAnimatedSprite CurrentSprite;
        public Animation CurrentAnimation;
        public const float PlayerMoveSpeed = 8.0f;
        public Geometry.Direction CurrentDirection;
        public Texture2D PlayerTexture;
        public Vector2 Position;
        public int Width
        {
            get { return CurrentSprite.FrameWidth; }
        }

        public int Height
        {
            get { return CurrentSprite.FrameHeight; }
        }

        public void Initialize(SpriteSheet sheet, Vector2 position)
        {
            PlayerSheet = sheet;
            PlayerTexture = sheet.Texture;
            Position = position;
            CurrentDirection = Geometry.Direction.Down;
            Console.WriteLine("setting currentsprite");
            CurrentSprite = new MovingAnimatedSprite("CuboneMarowak", Position, CurrentDirection);
            SetAnimation("Asleep");
        }

        public void SetAnimation(String name)
        {
            CurrentSprite.UpdateAnimation(name);
        }

        public void Update(GameTime gameTime)
        {
            CurrentSprite.Position = Position;
            CurrentSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, float scale)
        {
            CurrentSprite.Draw(spriteBatch, scale);
        }

        public void Move(Vector2 moveVector)
        {
            CurrentDirection = Geometry.VectorToQuarterDirection(moveVector);
            CurrentSprite.UpdateDirection(CurrentDirection);
            Position += moveVector;
        }
    }
}

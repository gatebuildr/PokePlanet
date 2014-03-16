using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PokePlanet
{
    public class AnimatedSprite
    {
        public Animation AnimationReference;

        public int Loops;

        public String Name;

        public Int32 DurationMillis;

        public Int32 X;

        public Int32 Y;

        public bool Active = true;

        private int _currentIndex;
        private float _elapsedTimeMilliseconds;

        public Texture2D Texture;
        public Vector2 Position;
        private Rectangle _sourceRect;
        private Rectangle _destinationRect;
        public Cell CurrentCell;
        public SpriteCoords CurrentSpriteCoords;

        public int FrameWidth
        {
            get { return (int) (CurrentSpriteCoords.Width * Game1.Scale); }
        }

        public int FrameHeight
        {
            get { return (int)(CurrentSpriteCoords.Height * Game1.Scale); }
        }

        public AnimatedSprite(string name, Vector2 position)
        {
            Name = name;
            Position = position;
        }

        public void UpdateAnimation(string animation)
        {
                AnimationReference = SpriteManager.GetAnimation(Name, animation);
                Texture = AnimationReference.Texture;
                Loops = AnimationReference.Loops;
                UpdateCell();
                UpdateSourceRect();
                _currentIndex = 0;
                _elapsedTimeMilliseconds = 0;
        }

        public void Update(GameTime gameTime)
        {
            //don't update if we're not active
            if (Active == false) return;

            _elapsedTimeMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
            if (_elapsedTimeMilliseconds > DurationMillis)
            {
                AdvanceFrame();
                UpdateSourceRect();
                _elapsedTimeMilliseconds = 0;
            }

            UpdateDestinationRect();
        }

        private void UpdateDestinationRect()
        {
            _destinationRect = new Rectangle((int)Position.X - FrameWidth / 2,
                (int)Position.Y - FrameHeight / 2,
                FrameWidth, FrameHeight);
        }

        private void AdvanceFrame()
        {
//            Console.WriteLine("advancin tha frame");
            _currentIndex++;
            if (_currentIndex == AnimationReference.Cells.Count)
            {
                _currentIndex = 0;
                if (Loops == 0)
                    Active = false;
            }
            UpdateCell();
        }

        private void UpdateCell()
        {
            CurrentCell = AnimationReference.Cells[_currentIndex];
            CurrentSpriteCoords = SpriteManager.GetSprite(AnimationReference.SpriteSheetName, CurrentCell.Sprite.Name);
            DurationMillis = Clock.FramesToMillis(CurrentCell.Delay);
        }

        private void UpdateSourceRect()
        {
            _sourceRect = new Rectangle(CurrentSpriteCoords.X, CurrentSpriteCoords.Y, CurrentSpriteCoords.Width, CurrentSpriteCoords.Height);
//            sourceRect = new Rectangle(X + Rows[currentRow].X + FrameWidth * rowIndex + Rows[currentRow].Gap * rowIndex,
//                Y + Rows[currentRow].Y, FrameWidth, FrameHeight);
        }

        public void Draw(SpriteBatch spriteBatch, float scale)
        {
            if (Active)
            {
                UpdateDestinationRect();
                spriteBatch.Draw(Texture, _destinationRect, _sourceRect, Color.White);
            }
        }
    }
}

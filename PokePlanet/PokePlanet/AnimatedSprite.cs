using System;
using System.Collections.Generic;
using System.Xml.Serialization;
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

//        int currentFrame = 0;
//        int totalFrames = 0;
        int _currentRow = 0;
        int _rowIndex = 0;
        private int _currentIndex = 0;
        float _elapsedTimeMilliseconds = 0;

        public Texture2D Texture;
        public Vector2 Position;
        private Rectangle _sourceRect;
        private Rectangle _destinationRect;
        public Cell CurrentCell;
        public SpriteCoords CurrentSpriteCoords;

        public int FrameWidth;
        public int FrameHeight;
        float _scale = 1f;

        public AnimatedSprite(Animation animationReference, Vector2 position)
        {
            Console.WriteLine("Constructing AnimatedSprite for " + animationReference.Name);
            Texture = animationReference.Texture;
            AnimationReference = animationReference;
            Loops = AnimationReference.Loops;
            Position = position;
            UpdateCell();
            UpdateSourceRect();
            
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
            _destinationRect = new Rectangle((int)Position.X - (int)(FrameWidth * _scale) / 2,
                (int)Position.Y - (int)(FrameHeight * _scale) / 2,
                (int)(FrameWidth * _scale), (int)(FrameHeight * _scale));
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
            _sourceRect = new Rectangle(CurrentSpriteCoords.x, CurrentSpriteCoords.y, CurrentSpriteCoords.Width, CurrentSpriteCoords.Height);
            FrameWidth = _sourceRect.Width;
            FrameHeight = _sourceRect.Height;
//            sourceRect = new Rectangle(X + Rows[currentRow].X + FrameWidth * rowIndex + Rows[currentRow].Gap * rowIndex,
//                Y + Rows[currentRow].Y, FrameWidth, FrameHeight);
        }

        public void Draw(SpriteBatch spriteBatch, float scale)
        {
            this._scale = scale;
            if (Active)
            {
                UpdateDestinationRect();
                spriteBatch.Draw(Texture, _destinationRect, _sourceRect, Color.White);
            }
        }
    }
}

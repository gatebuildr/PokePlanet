using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PokePlanet
{
    public class AnimatedSprite
    {
        [XmlAttribute("loops")]
        public int Loops;

        [XmlAttribute("name")]
        public String Name;

        [XmlAttribute("duration")]
        public Int32 Duration;

        [XmlAttribute("x")]
        public Int32 X;

        [XmlAttribute("y")]
        public Int32 Y;

        [XmlElement("spriterow")]
        public List<SpriteRow> Rows;

        public bool Active = true;

//        int currentFrame = 0;
//        int totalFrames = 0;
        int currentRow = 0;
        int rowIndex = 0;
        int elapsedTime = 0;

        //public Texture2D Texture;
        public Vector2 Position;
        Rectangle sourceRect = new Rectangle();
        Rectangle destinationRect = new Rectangle();

        public int FrameWidth;
        public int FrameHeight;
        float scale = 1f;
        
        public void Initialize(Texture2D Texture, Vector2 position, float scale)
        {
            //this.Texture = Texture;
            this.scale = scale;
            Position = position;
            FrameWidth = Rows[currentRow].Width;
            FrameHeight = Rows[currentRow].Height;
        }

        public void Update(GameTime gameTime)
        {
            //don't update if we're not active
            if (Active == false) return;

            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedTime > Duration)
            {
                AdvanceFrame();
                UpdateSourceRect();
                elapsedTime = 0;
            }

            //set source rect to grab correct chunk of sprite sheet
            //sourceRect = new Rectangle(rowIndex * FrameWidth, 0, FrameWidth, FrameHeight);
            UpdateDestinationRect(scale);
        }

        private void UpdateDestinationRect(float scale)
        {
            destinationRect = new Rectangle((int)Position.X - (int)(FrameWidth * scale) / 2,
                (int)Position.Y - (int)(FrameHeight * scale) / 2,
                (int)(FrameWidth * scale), (int)(FrameHeight * scale));
        }

        private void AdvanceFrame()
        {
            rowIndex++;
            if (rowIndex == Rows[currentRow].Count)
            {
                currentRow++;
                rowIndex = 0;
            }
            if (currentRow == Rows.Count)
            {
                currentRow = 0;
                if (Loops == 0)
                    Active = false;
            }
            FrameWidth = Rows[currentRow].Width;
            FrameHeight = Rows[currentRow].Height;
        }

        private void UpdateSourceRect()
        {
            sourceRect = new Rectangle(X + Rows[currentRow].X + FrameWidth * rowIndex + Rows[currentRow].Gap * rowIndex,
                Y + Rows[currentRow].Y, FrameWidth, FrameHeight);
        }

        public void Draw(Texture2D Texture, SpriteBatch spriteBatch, float scale)
        {
            if (Active)
            {
                UpdateDestinationRect(scale);
                spriteBatch.Draw(Texture, destinationRect, sourceRect, Color.White);
            }
        }
    }

    public class SpriteRow
    {
        [XmlAttribute("n")]
        public Int32 Count;

        [XmlAttribute("x")]
        public Int32 X;

        [XmlAttribute("y")]
        public Int32 Y;

        [XmlAttribute("w")]
        public Int32 Width;

        [XmlAttribute("h")]
        public Int32 Height;

        [XmlAttribute("gap")]
        public Int32 Gap=0;
    }
}

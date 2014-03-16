using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace PokePlanet
{
    public class ParallaxingBackground
    {
        Texture2D _texture;
        Vector2[] _positions;
        int _speed;
        int _bgHeight;
        int _bgWidth;

        public void Initialize(ContentManager content, String texturePath, int screenWidth, int screenHeight, int speed)
        {
            _bgHeight = screenHeight;
            _bgWidth = screenWidth;

            _texture = content.Load<Texture2D>(texturePath);
            _speed = speed;
            _positions = new Vector2[screenWidth / _texture.Width + 1];
            for (int i = 0; i < _positions.Length; i++)
            {
                _positions[i] = new Vector2(i * _texture.Width, 0);
            }
        }
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < _positions.Length; i++)
            {
                _positions[i].X += _speed;
                //move background based on forward or backward speed
                if (_speed <= 0)
                {
                    if (_positions[i].X <= -_texture.Width)
                    {
                        _positions[i].X = _texture.Width * (_positions.Length - 1);
                    }
                }
                else
                {
                    if (_positions[i].X >= _texture.Width * (_positions.Length - 1))
                    {
                        _positions[i].X = -_texture.Width;
                    }
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _positions.Length; i++)
            {
                var rectBg = new Rectangle((int)_positions[i].X, (int)_positions[i].Y, _bgWidth, _bgHeight);
                spriteBatch.Draw(_texture, rectBg, Color.White);
            }
        }

    }
}

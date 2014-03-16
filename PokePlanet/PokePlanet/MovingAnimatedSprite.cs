using System;
using Microsoft.Xna.Framework;

namespace PokePlanet
{
    public class MovingAnimatedSprite : AnimatedSprite
    {
        private Geometry.Direction _currentDirection = Geometry.Direction.Down;
        private string _currentAnimationName;

        public MovingAnimatedSprite(string name, Vector2 position) : base(name, position)
        {
            _currentDirection = Geometry.Direction.Neutral;
        }

        public MovingAnimatedSprite(string name, Vector2 position, Geometry.Direction direction) : base(name, position)
        {
            _currentDirection = direction;
        }

        public void UpdateDirection(Geometry.Direction direction)
        {
            if (direction != _currentDirection && direction != Geometry.Direction.Neutral)
            {
                _currentDirection = direction;
                UpdateAnimation();
            }
        }

        public new void UpdateAnimation(string animation)
        {
            _currentAnimationName = animation;
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            try
            {
                base.UpdateAnimation(_currentAnimationName + _currentDirection.ToString());
            }
            catch (MissingAnimationException e)
            {
                Console.WriteLine(e.Message + "\nDefaulting to other animation");
                base.UpdateAnimation(_currentAnimationName);
            }
        }
    }
}

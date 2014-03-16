using System;
using Microsoft.Xna.Framework;

namespace PokePlanet
{
    public class Geometry
    {
        public enum Direction
        {
// ReSharper disable InconsistentNaming
            Up=0, RU=45, Right=90, RD=135, Down=180, LD=225, Left=270, LU=315, Neutral=Int32.MaxValue
// ReSharper restore InconsistentNaming
        }

        /// (0,1) is down, (0,-1) is up, (1, 0) is right, (-1,0) is left
        /// 0 is up, 90 is right, 180 is down, 270 is right
        public static Vector2 AngleToVector(float angle)
        {
            return new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle));
        }

        static double VectorToDegrees(Vector2 vector)
        {
            double degrees = VectorToRadians(vector)*180/Math.PI;
            if (degrees < 0) degrees += 360;
            return degrees;
        }

        /// <summary>
        /// Converts a vector2 to a direction from up
        /// </summary>
        /// <param name="vector"></param>
        /// <returns>Angle in radians, clockwise from up</returns>
        static float VectorToRadians(Vector2 vector)
        {
            return (float)Math.Atan2(vector.X, -vector.Y);
        }

        public static Direction VectorToOrthogonalDirection(Vector2 vector)
        {
            return VectorToDirection(vector, 45);
        }

        public static Direction VectorToQuarterDirection(Vector2 vector)
        {
            return VectorToDirection(vector, 90);
        }

        private static Direction VectorToDirection(Vector2 vector, int precision)
        {
            if (vector.Equals(Vector2.Zero))
                return Direction.Neutral;
            int angle = (((int) VectorToDegrees(vector) + precision/2)/precision)*precision % 360;
            Console.WriteLine("Adjusted angle to be " + angle);
            var direction = (Direction)Enum.ToObject(typeof(Direction), angle);
            Console.WriteLine("Current direction " + direction);
            return direction;
        }
    }
}

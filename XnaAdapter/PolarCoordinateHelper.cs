using System;
using Microsoft.Xna.Framework;

namespace XnaAdapter
{
    public class PolarCoordinateHelper
    {
        public static Vector2 AngleToVector2(float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        public static float GetAngle(Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }

        public static float GetAngle(Vector2 vectorA, Vector2 vectorB)
        {
            return (float)(Math.Atan2(vectorA.Y, vectorA.X) - Math.Atan2(vectorB.Y, vectorB.X));
        }

        // Поворот против часовой стрелки
        public static Vector2 TurnVector2(Vector2 vector, float angle)
        {
            var resultAngle = GetAngle(vector);
            var vectorLength = vector.Length();
            resultAngle += angle;

            return AngleToVector2(resultAngle)*vectorLength;
        }

        public static Vector2 GetDirection(Vector2 A, Vector2 B)
        {
            return (B - A);
        }
    }
}

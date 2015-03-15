using System.Runtime.Serialization;

namespace SeaBattle.Common.Session
{
    [DataContract]
    public class GameLevel
    {
        public readonly int Width;
        public readonly int Height;

        public GameLevel(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaBattle.Common.Session
{
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

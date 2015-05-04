using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SeaBattle.StaticObjects
{
    public class Background
    {
        public Texture2D BackgroundTexture;
        public Rectangle BackgrounRectangle;

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundTexture, BackgrounRectangle, Color.White);
        }
    }
}

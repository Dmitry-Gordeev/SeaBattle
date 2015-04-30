using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SeaBattle.View
{
    public static class Textures
    {
        /// <summary>
        /// current graphic device
        /// </summary>
        public static GraphicsDevice GraphicsDevice;

        // Landscape textures
        public static Texture2D SeaFromAir;

        // cursor textures
        public static Texture2D Arrow;
        public static Texture2D Plus;
        public static Texture2D Cross;
        public static Texture2D Target;

        // other textures
        public static Texture2D CompassArrow;

        public static Texture2D HealthRect(int width, int heigth, Color c)
        {
            return Create(width, heigth < 1 ? 1 : heigth, c);
        }

        public static Texture2D HeaterProjectile
        {
            get { return Create(2, 8, Color.Black); }
        }

        public static Texture2D LaserProjectile
        {
            get { return Create(2, 5, Color.Red); }
        }

        /// <summary>
        /// create a colored rectangle
        /// </summary>
        public static Texture2D Create(int width, int height, Color color)
        {
            // create the rectangle texture without colors
            var texture = new Texture2D(GraphicsDevice, width, height);

            // create a color array for the pixels
            var colors = new Color[width * height];
            var newColor = new Color(color.ToVector3());
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = newColor;
            }

            // set the color data for the texture
            texture.SetData(colors);

            return texture;
        }

        /// <summary>
        /// copy a texture
        /// </summary>
        public static Texture2D Clone(Texture2D texture)
        {
            // get pixels from texture
            var textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);

            // create clone texture
            var clone = new Texture2D(GraphicsDevice, texture.Width, texture.Height);
            clone.SetData(textureData);

            return clone;
        }

        /// <summary>
        /// add small texture into big texture at Vector2D position
        /// </summary>
        public static void Merge(Texture2D big, Texture2D small, Vector2 position)
        {
            // get pixels from big texture
            var bigData = new Color[big.Width * big.Height];
            big.GetData(bigData);

            // get pixels from small texture
            var smallData = new Color[small.Width * small.Height];
            small.GetData(smallData);

            // replace transparent pixels
            for (int i = 0; i < small.Height; i++)
                for (int j = 0; j < small.Width; j++)
                    if (smallData[i * small.Width + j] == Color.Transparent)
                        smallData[i * small.Width + j] = bigData[((int)position.Y + i) * big.Width + ((int)position.X + j)];

            // set the new data
            big.SetData(
                0,
                new Rectangle((int)position.X, (int)position.Y, small.Width, small.Height),
                smallData,
                0,
                small.Width * small.Height);
        }

        public static Texture2D ActiveCursor
        {
            get
            {
                switch (Settings.Default.Cursor)
                {
                    case 1:
                        return Arrow;
                    case 2:
                        return Plus;
                    case 3:
                        return Cross;
                    case 4:
                        return Target;
                    default:
                        return Arrow;
                }
            }
        }

        public static Vector2 GetCursorPosition(float x, float y)
        {
            switch (Settings.Default.Cursor)
            {
                case 1:
                    return new Vector2(x - 11f, y - 2.5f);
                case 2:
                    return new Vector2(x - 23f, y - 23f);
                case 3:
                    return new Vector2(x - 23f, y - 23f);
                case 4:
                    return new Vector2(x - 24f, y - 23f);
                default:
                    return new Vector2(x - 11f, y - 2.5f);
            }
        }
    }
}

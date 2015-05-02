using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Session;

namespace SeaBattle.View
{
    public class DrawableGameObject : IDrawable
    {
        public Animation2D Animation { get; set; }

        protected Texture2D StaticTexture { get; set; }
        public virtual Vector2 Coordinates { get; set; }
        public virtual Vector2 MoveVector { get; set; }

        public Guid ID { get; set; }

        public DrawableGameObject(IObject other, Animation2D animation)
        {
            Animation = animation;
        }

        public DrawableGameObject(IObject other, Texture2D staticTexture)
        {
            Animation = null;
            StaticTexture = staticTexture;
        }

        public DrawableGameObject()
        {

        }

        public void Copy(DrawableGameObject other)
        {
            Animation = other.Animation;
            StaticTexture = other.StaticTexture;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            var rotation = (float)Math.Atan2(MoveVector.Y, MoveVector.X);

            float scale = 0.5f;
            float textureLayer = Constants.MOVING_GAME_OBJECTS_TEXTURE_LAYER;


            if (Animation != null)
            {
                spriteBatch.Draw(Animation.CurrentTexture,
                                 Coordinates,
                                 null,
                                 Color.White,
                                 rotation,
                                 new Vector2(Animation.CurrentTexture.Width / 2f, Animation.CurrentTexture.Height / 2f),
                                 scale,
                                 SpriteEffects.None,
                                 textureLayer);
            }
            else
            {
                spriteBatch.Draw(StaticTexture,
                                 Coordinates,
                                 null,
                                 Color.White,
                                 rotation,
                                 new Vector2(StaticTexture.Width / 2f, StaticTexture.Height / 2f),
                                 scale,
                                 SpriteEffects.None,
                                 textureLayer);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeaBattle.Common.Objects;

namespace SeaBattle.View
{
    public class DrawableGameObject : IObject, IDrawable
    {
        public Animation2D Animation { get; set; }

        protected Texture2D StaticTexture { get; set; }
        public Vector2 CoordinatesM { get; set; }

        public Vector2 MoveVectorM { get; set; }

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
            
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public bool IsStatic { get; private set; }
        public int ID { get; private set; }
    }
}

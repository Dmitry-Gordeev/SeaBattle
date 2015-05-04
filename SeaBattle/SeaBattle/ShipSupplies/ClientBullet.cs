using Microsoft.Xna.Framework;
using SeaBattle.Common.Objects;
using SeaBattle.View;

namespace SeaBattle.ShipSupplies
{
    public class ClientBullet :DrawableGameObject
    {
        public IBullet Bullet;

        public override Vector2 Coordinates
        {
            get { return Bullet.Coordinates; }
        }

        public ClientBullet(IBullet bullet)
        {
            Bullet = bullet;
            StaticTexture = Textures.Target;
        }
    }
}

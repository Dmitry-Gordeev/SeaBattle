using Microsoft.Xna.Framework;
using SeaBattle.Service.Ships;
using SeaBattle.View;

namespace SeaBattle.Ships
{
    public class ClientShip : DrawableGameObject
    {
        public ShipBase Ship { get; set; }

        public override Vector2 Coordinates
        {
            get { return Ship.Coordinates; }
        }

        public override Vector2 MoveVector { get { return Ship.MoveVector; } }

        internal ClientShip(ShipBase ship)
        {
            Ship = ship;
            StaticTexture = Textures.Lugger;
        }
    }
}

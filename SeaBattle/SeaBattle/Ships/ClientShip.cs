using System;
using Microsoft.Xna.Framework;
using SeaBattle.Service.Ships;
using SeaBattle.View;
using XnaAdapter;

namespace SeaBattle.Ships
{
    public class ClientShip : DrawableGameObject
    {
        public ShipBase Ship { get; set; }

        public override Vector2 Coordinates
        {
            get { return Ship.Coordinates; }
        }

        public override Vector2 MoveVector { get { return PolarCoordinateHelper.TurnVector2(Ship.MoveVector, (float)(Math.PI/2)); } }

        internal ClientShip(ShipBase ship)
        {
            Ship = ship;
            StaticTexture = Textures.Corvette;
        }
    }
}

using Microsoft.Xna.Framework;
using SeaBattle.Service.ShipSupplies;
using SeaBattle.View;

namespace SeaBattle.ShipSupplies
{
    public sealed class ClientCompass : DrawableGameObject
    {
        public Compass Compass { get; set; }

        public override Vector2 MoveVector
        {
            get { return Compass.Direction; }
        }

        public ClientCompass()
        {
            Compass = new Compass(false);
            Coordinates = new Vector2(500f, 50f);
            StaticTexture = Textures.CompassArrow;
        }
    }
}

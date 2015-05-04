using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeaBattle.Common.Session;
using SeaBattle.Game;
using SeaBattle.Screens;
using SeaBattle.Service.ShipSupplies;
using SeaBattle.View;

namespace SeaBattle.ShipSupplies
{
    public sealed class ClientWindVane : DrawableGameObject
    {
        public WindVane WindVane { get; set; }

        public override Vector2 Coordinates
        {
            get { return Camera2D.RelativePosition(new Vector2(890f, 75f)); }
        }

        public override Vector2 MoveVector
        {
            get { return WindVane.Direction; }
        }

        public ClientWindVane()
        {
            WindVane = new WindVane(false);
            StaticTexture = Textures.CompassArrow;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var rotation = (float)Math.Atan2(MoveVector.Y, MoveVector.X);

            float scale = 0.5f;
            float textureLayer = Constants.MOVING_GAME_OBJECTS_TEXTURE_LAYER;

            spriteBatch.Draw(StaticTexture,
                                 Coordinates,
                                 null,
                                 Color.White,
                                 rotation,
                                 new Vector2(StaticTexture.Width / 2f, StaticTexture.Height / 2f),
                                 scale,
                                 SpriteEffects.None,
                                 textureLayer);

            spriteBatch.DrawString(
                ScreenManager.Instance.Font,
                "WindVane",
                Camera2D.RelativePosition(new Vector2(865f, 10f)),
                Color.Red, 0, new Vector2(0f, 0f), 0.5f, SpriteEffects.None,
                layerDepth: Constants.TEXT_TEXTURE_LAYER);

            spriteBatch.DrawString(
                ScreenManager.Instance.Font,
                GameController.Instance.ClientWindVane.WindVane.ForceOfWind.ToString("F"),
                Camera2D.RelativePosition(new Vector2(880f, 30f)),
                Color.Red, 0, new Vector2(0f, 0f), 0.5f, SpriteEffects.None,
                layerDepth: Constants.TEXT_TEXTURE_LAYER);
        }
    }
}

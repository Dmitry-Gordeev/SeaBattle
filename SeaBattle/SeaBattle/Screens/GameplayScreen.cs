using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeaBattle.Game;
using SeaBattle.Input;
using SeaBattle.NetWork;
using SeaBattle.View;

namespace SeaBattle.Screens
{
    internal class GameplayScreen : GameScreen
    {
        public GameplayScreen()
        {

        }

        public override ScreenManager.ScreenEnum ScreenType
        {
            get { return ScreenManager.ScreenEnum.GameplayScreen; }
        }

        public override void LoadContent()
        {
            // load landscapes
            Textures.SeaFromAir = ContentManager.Load<Texture2D>("Textures/Landscapes/SeaFromAir");
            Textures.CompassArrow = ContentManager.Load<Texture2D>("Textures/OtherObjects/CompassArrow");
            Textures.Lugger = ContentManager.Load<Texture2D>("Textures/Ships/Lugger");

            // load stones
            /*for (int i = 1; i <= Textures.STONES_AMOUNT; i++)
                Textures.Stones[i - 1] = ContentManager.Load<Texture2D>("Textures/Landscapes/Stone" + i);
            Textures.OneStone = ContentManager.Load<Texture2D>("Textures/Landscapes/Stone" + 1);*/


            ScreenManager.Instance.Game.ResetElapsedTime();
        }
        private int _countOfUpdates;

        public override void HandleInput(Controller controller)
        {
            GameController.Instance.HandleInput(controller);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            GameController.Instance.UpdateWorld(gameTime);

            byte[] dataBytes = null;

            if (_countOfUpdates++%5 == 0)
            {
                dataBytes = ConnectionManager.Instance.GetInfo();
            }
            GameController.Instance.UpdateWorld(dataBytes);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            GraphicsDevice graphicsDevice = ScreenManager.Instance.GraphicsDevice;
            graphicsDevice.Clear(Color.SkyBlue);

            GameController.Instance.DrawWorld(SpriteBatch);
            GameController.Instance.ClientWindVane.Draw(SpriteBatch);

            for (int i = 0; i < GameController.Instance.Ships.Count(); i++)
            {
                if (GameController.Instance.Ships[i] == null) continue;
                GameController.Instance.Ships[i].Draw(SpriteBatch);

                // Если наш корабль - отображаем состояние парусов
                if (GameController.Instance.Ships[i].Ship.Player.Name == GameController.Instance.MyLogin)
                {
                    DrawString("Sails state:", 860f, 100f, Color.Red, 0.5f);
                    DrawString(GameController.Instance.Ships[i].Ship.ShipSupplies.Sails.SailsState[0].ToString(), 895f, 120f, Color.Red, 0.5f);
                    DrawString(GameController.Instance.Ships[i].Ship.ShipSupplies.Sails.SailsState[1].ToString(), 895f, 140f, Color.Red, 0.5f);
                }
            }
            
            // Флюгер
            DrawString("WindVane", 865f, 10f, Color.Red, 0.5f);
            DrawString(GameController.Instance.ClientWindVane.WindVane.ForceOfWind.ToString("F"), 880, 30f, Color.Red, 0.5f);

            SpriteBatch.End();

            
            
            //DrawString(GameController.Instance.Ships[1].Coordinates.ToString(), 280f, 260f, Color.Red);
            //DrawString(GameController.Instance.Ships[1].Player.Name, 280f, 290f, Color.Red);
        }

        public override void Destroy()
        {
            base.Destroy();
            GameController.Instance.Destroy();
        }
    }
}

using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeaBattle.Game;
using SeaBattle.Input;
using SeaBattle.NetWork;
using SeaBattle.StaticObjects;
using SeaBattle.View;

namespace SeaBattle.Screens
{
    internal class GameplayScreen : GameScreen
    {
        public Camera2D Camera2D { get; private set; }
        public GameplayBackground GameplayBackground { get; private set; }

        public GameplayScreen()
        {
            Camera2D = new Camera2D();
        }

        public override ScreenManager.ScreenEnum ScreenType
        {
            get { return ScreenManager.ScreenEnum.GameplayScreen; }
        }

        public override void LoadContent()
        {
            Textures.GameplayBackground = ContentManager.Load<Texture2D>("Textures/Landscapes/SeaBackground");
            Textures.CompassArrow = ContentManager.Load<Texture2D>("Textures/OtherObjects/CompassArrow");
            Textures.Lugger = ContentManager.Load<Texture2D>("Textures/Ships/Lugger");
            Textures.Bullet = ContentManager.Load<Texture2D>("Textures/Landscapes/Stone1");

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
            Camera2D.Update();

            if (GameplayBackground == null)
            {
                GameplayBackground = new GameplayBackground(Textures.GameplayBackground, 
                    new Point((int)GameController.Instance.MyShip.Coordinates.X,
                    (int)GameController.Instance.MyShip.Coordinates.Y));
            }
            GameplayBackground.Update(new Point((int)GameController.Instance.MyShip.Coordinates.X, 
                                        (int)GameController.Instance.MyShip.Coordinates.Y));
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, 
                null,null,null,null,
                Camera2D.MatrixScreen);
            
            if (GameplayBackground != null)
            {
                GameplayBackground.Draw(SpriteBatch);
            }

            GraphicsDevice graphicsDevice = ScreenManager.Instance.GraphicsDevice;
            graphicsDevice.Clear(Color.SkyBlue);

            GameController.Instance.DrawWorld(SpriteBatch);

            // Флюгер
            GameController.Instance.ClientWindVane.Draw(SpriteBatch);

            // Корабли
            for (int i = 0; i < GameController.Instance.Ships.Count(); i++)
            {
                if (GameController.Instance.Ships[i] == null) continue;
                GameController.Instance.Ships[i].Draw(SpriteBatch);

                // Если наш корабль - отображаем данные
                if (GameController.Instance.Ships[i].Ship.Player.Name != GameController.Instance.MyLogin)
                {
                    DrawString(GameController.Instance.Ships[i].Ship.Player.Name, 10f, 560f, Color.Red, 0.5f);
                    DrawString(GameController.Instance.Ships[i].Ship.Coordinates.ToString(), 5f, 580f, Color.Red, 0.5f);
                    continue;
                }
                
                DrawSalsState(GameController.Instance.Ships[i].Ship.ShipSupplies.Sails.SailsState[0],
                    GameController.Instance.Ships[i].Ship.ShipSupplies.Sails.SailsState[1]);
                DrawHealth(GameController.Instance.Ships[i].Ship.Health);
                // Координаты
                DrawString("Coordinates", 10f, 60f, Color.Red, 0.5f);
                DrawString(GameController.Instance.Ships[i].Ship.Coordinates.ToString(), 10f, 80f, Color.Red, 0.5f);
            }
            
            if (GameController.Instance.Bullets != null)
            {
                foreach (var bullet in GameController.Instance.Bullets)
                {
                    bullet.Draw(SpriteBatch);
                }
            }


            
            SpriteBatch.End();
        }

        private void DrawHealth(float health)
        {
            DrawString("Health:", 20f, 10f, Color.Red, 0.5f);
            DrawString(health.ToString(), 30f, 30f, Color.Red, 0.5f);
        }

        private void DrawSalsState(int firstSail, int secondSail)
        {
            DrawString("Sails state:", 860f, 100f, Color.Red, 0.5f);
            DrawString(firstSail.ToString(), 895f, 120f, Color.Red, 0.5f);
            DrawString(secondSail.ToString(), 895f, 140f, Color.Red, 0.5f);
        }

        public override void Destroy()
        {
            base.Destroy();
            GameController.Instance.Destroy();
        }
    }
}

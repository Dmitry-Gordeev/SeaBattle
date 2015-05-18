using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeaBattle.Common.Utils;
using SeaBattle.Game;
using SeaBattle.Input;
using SeaBattle.NetWork;
using SeaBattle.StaticObjects;
using SeaBattle.View;
using XnaAdapter;

namespace SeaBattle.Screens
{
    internal class GameplayScreen : GameScreen
    {
        public Camera2D Camera2D { get; private set; }
        public GameplayBackground GameplayBackground { get; private set; }
        private bool _isStarted;

        public GameplayScreen()
        {
            Camera2D = new Camera2D();
            GarbageCollector = new GarbageCollector(20);
            Textures.GameplayBackgroundAnimation = new Animation2D();
            GameplayBackground = new GameplayBackground(Textures.GameplayBackgroundTexture,
                                                        Textures.GameplayBackgroundAnimation);
        }

        public override ScreenManager.ScreenEnum ScreenType
        {
            get { return ScreenManager.ScreenEnum.GameplayScreen; }
        }

        public override void LoadContent()
        {
            Textures.GameplayBackgroundTexture = ContentManager.Load<Texture2D>("Textures/Landscapes/SeaBackground");
            Textures.CompassArrow = ContentManager.Load<Texture2D>("Textures/OtherObjects/CompassArrow");
            Textures.Corvette = ContentManager.Load<Texture2D>("Textures/Ships/120");
            Textures.Bullet = ContentManager.Load<Texture2D>("Textures/Landscapes/Bullet");

            for (int i = 1; i < 22; i++)
            {
                Textures.GameplayBackgroundAnimation.AddFrame(
                    ContentManager.Load<Texture2D>("Textures/Landscapes/Animation/0" + i.ToString("D2")));
            }

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
            GameplayBackground.BackgroundAnimation.Update(gameTime);

            byte[] dataBytes = null;

            if (_countOfUpdates % 5 == 0)
            {
                dataBytes = ConnectionManager.Instance.GetInfo();
            }
            
            GameController.Instance.UpdateWorld(dataBytes, _countOfUpdates++ % 5, _isStarted);

            Camera2D.Update();

            if (!_isStarted)
            {
                GameplayBackground.Initialize(new Point((int)GameController.Instance.MyShip.Coordinates.X,
                                              (int)GameController.Instance.MyShip.Coordinates.Y));
                GameplayBackground.BackgroundAnimation.Initialize(100, true);

                _isStarted = !_isStarted;
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
            for (int i = 0; i < GameController.Instance.CurrentShips.Count(); i++)
            {
                if (GameController.Instance.CurrentShips[i] == null) continue;
                GameController.Instance.CurrentShips[i].Draw(SpriteBatch);

                // Если наш корабль - отображаем данные
                if (GameController.Instance.CurrentShips[i].Ship.Player.Name != GameController.Instance.MyLogin)
                {
                    DrawString(GameController.Instance.CurrentShips[i].Ship.Player.Name, 10f, 835f, Color.Red, 0.8f);
                    DrawString(PolarCoordinateHelper.Vector2ToString(GameController.Instance.CurrentShips[i].Ship.Coordinates), 5f, 860f, Color.Red, 0.8f);
                    continue;
                }

                DrawSalsState(GameController.Instance.CurrentShips[i].Ship.ShipSupplies.Sails.SailsState[0],
                    GameController.Instance.CurrentShips[i].Ship.ShipSupplies.Sails.SailsState[1]);
                DrawHealth(GameController.Instance.CurrentShips[i].Ship.Health);
                // Координаты
                DrawString("Coordinates", 10f, 70f, Color.Red, 0.8f);
                DrawString(PolarCoordinateHelper.Vector2ToString(GameController.Instance.CurrentShips[i].Ship.Coordinates), 10f, 100f, Color.Red, 0.8f);
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
            DrawString("Health", 20f, 10f, Color.Red, 0.8f);
            DrawString(health.ToString(), 30f, 35f, Color.Red, 0.8f);
        }

        private void DrawSalsState(int firstSail, int secondSail)
        {
            DrawString("Sails state", 870f, 110f, Color.Red, 0.8f);
            DrawString(firstSail.ToString(), 925f, 135f, Color.Red, 0.8f);
            DrawString(secondSail.ToString(), 925f, 160f, Color.Red, 0.8f);
        }

        public override void Destroy()
        {
            base.Destroy();
            GameController.Instance.Destroy();
        }
    }
}

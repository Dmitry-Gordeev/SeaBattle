using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeaBattle.Common.Session;
using SeaBattle.Game;
using SeaBattle.Input;
using SeaBattle.NetWork;
using SeaBattle.View;

namespace SeaBattle.Screens
{
    internal class GameplayScreen : GameScreen
    {
        public Camera2D Camera2D { get; private set; }

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
            Textures.SeaFromAir = ContentManager.Load<Texture2D>("Textures/Landscapes/SeaFromAir");
            Textures.CompassArrow = ContentManager.Load<Texture2D>("Textures/OtherObjects/CompassArrow");
            Textures.Lugger = ContentManager.Load<Texture2D>("Textures/Ships/Lugger");

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
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, 
                null,null,null,null,
                Camera2D.MatrixScreen);

            SpriteBatch.Draw(Textures.SeaFromAir, new Vector2(), null, Color.White);
            
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
            

            SpriteBatch.End();
        }

        public override void Destroy()
        {
            base.Destroy();
            GameController.Instance.Destroy();
        }
    }
}

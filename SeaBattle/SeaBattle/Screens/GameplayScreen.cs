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

        public override void LoadContent()
        {
            // load landscapes
            Textures.SeaFromAir = ContentManager.Load<Texture2D>("Textures/Landscapes/SeaFromAir");

            // load stones
            /*for (int i = 1; i <= Textures.STONES_AMOUNT; i++)
                Textures.Stones[i - 1] = ContentManager.Load<Texture2D>("Textures/Landscapes/Stone" + i);
            Textures.OneStone = ContentManager.Load<Texture2D>("Textures/Landscapes/Stone" + 1);*/

            // load bricks
            Textures.Brick = ContentManager.Load<Texture2D>("Textures/Landscapes/Brick");

            ScreenManager.Instance.Game.ResetElapsedTime();
        }

        public override void HandleInput(Controller controller)
        {
            GameController.Instance.HandleInput(controller);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            GameController.Instance.UpdateWorld(gameTime);

            GameController.Instance.UpdateWorld(ConnectionManager.Instance.GetInfo());
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();

            GraphicsDevice graphicsDevice = ScreenManager.Instance.GraphicsDevice;
            graphicsDevice.Clear(Color.SkyBlue);

            GameController.Instance.DrawWorld(SpriteBatch);

            /*
            DrawString(GameController.Instance.Borders[0].Coordinates.ToString(), 20f, 25f, Color.Red);
            DrawString(GameController.Instance.Borders[1].Coordinates.ToString(), 50f, 25f, Color.Red);
            DrawString(GameController.Instance.Borders[2].Coordinates.ToString(), 70f, 25f, Color.Red);
            DrawString(GameController.Instance.Borders[3].Coordinates.ToString(), 100f, 25f, Color.Red);
            */

            for (int i = 0; i < GameController.Instance.Ships.Count(); i++)
            {
                if (GameController.Instance.Ships[i] == null) continue;

                DrawString(GameController.Instance.Ships[i].Coordinates.ToString(), 80f + i * 100f, 60f + i * 100f, Color.Red);
                DrawString(GameController.Instance.Ships[i].Player.Name, 80f + i * 100f, 90f + i * 100f, Color.Red);
            }

            
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

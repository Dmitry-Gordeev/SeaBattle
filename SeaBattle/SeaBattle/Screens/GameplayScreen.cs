using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeaBattle.Game;
using SeaBattle.Input;
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
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice graphicsDevice = ScreenManager.Instance.GraphicsDevice;
            graphicsDevice.Clear(Color.SkyBlue);

            GameController.Instance.DrawWorld(SpriteBatch);

            
        }

        public override void Destroy()
        {
            base.Destroy();
            GameController.Instance.Destroy();
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeaBattle.Common.Service;
using SeaBattle.Screens;
using SeaBattle.Service.Ships;
using SeaBattle.View;
using SeaBattle.Common.Session;

namespace SeaBattle
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SeaBattleGame : Microsoft.Xna.Framework.Game
    {
        private ScreenManager _screenManager;
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        Lugger _myLugger;
        readonly ISeaBattleService _client;
        
        public SeaBattleGame()
        {
            //IsMouseVisible = false;

            // Graphics init
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = Constants.LevelWidth;
            _graphics.PreferredBackBufferHeight = Constants.LevelHeigh;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Textures.GraphicsDevice = GraphicsDevice;
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ScreenManager.Init(this);
            _screenManager = ScreenManager.Instance;
            Components.Add(_screenManager);

            base.Initialize();

            ScreenManager.Instance.SetActiveScreen(ScreenManager.ScreenEnum.LoginScreen);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Textures.Arrow = Content.Load<Texture2D>("Textures/Cursors/Arrow");
            Textures.Plus = Content.Load<Texture2D>("Textures/Cursors/Plus");
            Textures.Cross = Content.Load<Texture2D>("Textures/Cursors/Cross");
            Textures.Target = Content.Load<Texture2D>("Textures/Cursors/Target");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);

            _spriteBatch.Begin(SpriteSortMode.Texture, BlendState.AlphaBlend);
            Vector2 mousePosition = ScreenManager.Instance.GetMousePosition();
            _spriteBatch.Draw(Textures.ActiveCursor, Textures.GetCursorPosition(mousePosition.X, mousePosition.Y), Color.White);
            _spriteBatch.End();
        }
    }
}

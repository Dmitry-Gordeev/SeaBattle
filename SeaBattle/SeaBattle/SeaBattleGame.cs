using System;
using System.ServiceModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SeaBattle.Common.Service;
using SeaBattle.NetWork;
using SeaBattle.Screens;
using SeaBattle.Service.Ships;
using SeaBattle.View;

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

        private MouseState _currentMouseState, _lastMouseState;

        
        public SeaBattleGame()
        {
            // Mouse init
            _currentMouseState = Mouse.GetState();
            _lastMouseState = Mouse.GetState();
            IsMouseVisible = false;

            // Graphics init
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;

            /* Connection init
            var myChannelFactory = new ChannelFactory<ISeaBattleService>("SeaBattleEndpoint");
            try
            {
                _client = myChannelFactory.CreateChannel();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }*/
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
            _lastMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();
            /*
            if (_currentMouseState.LeftButton == ButtonState.Pressed && _lastMouseState.LeftButton == ButtonState.Released)
            {
                _client.LeaveGame(_currentMouseState.X, _currentMouseState.Y);
            }*/
            
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

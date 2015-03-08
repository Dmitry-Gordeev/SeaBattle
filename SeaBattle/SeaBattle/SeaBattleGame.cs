using System;
using System.ServiceModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SeaBattle.Common.Service;
using SeaBattle.Service.Ships;

namespace SeaBattle
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SeaBattleGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        Lugger _myLugger;
        ChannelFactory<ISeaBattleService> myChannelFactory;
        ISeaBattleService client;

        private MouseState _currentMouseState, _lastMouseState;

        
        public SeaBattleGame()
        {
            _currentMouseState = Mouse.GetState();
            _lastMouseState = Mouse.GetState();


            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            myChannelFactory = new ChannelFactory<ISeaBattleService>("SeaBattleEndpoint");
            try
            {
                client = myChannelFactory.CreateChannel();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
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

            if (_currentMouseState.LeftButton == ButtonState.Pressed && _lastMouseState.LeftButton == ButtonState.Released)
            {
                client.LeaveGame(_currentMouseState.X, _currentMouseState.Y);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

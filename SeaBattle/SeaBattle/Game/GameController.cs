using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeaBattle.Common.Service;
using SeaBattle.Input;
using SeaBattle.NetWork;

namespace SeaBattle.Game
{
    public sealed class GameController
    {
        #region singleton

        private static GameController _localInstance;

        public static GameController Instance
        {
            get { return _localInstance ?? (_localInstance = new GameController()); }
        }

        private GameController()
        {
            
        }

        #endregion

        //private GameModel _gameModel;

        public static Guid MyId { get; private set; }

        // todo temporary
        public static long StartTime { get; set; }

        private int _weaponIndex;

        public bool IsGameStarted { get; private set; }

        private void Shoot(Vector2 direction)
        {
            //ConnectionManager.Instance.Shoot(TypeConverter.Xna2XnaLite(direction));
        }

        private void Move(Vector2 direction)
        {
            //ConnectionManager.Instance.Move(TypeConverter.Xna2XnaLite(direction));
        }

        public void GameStart(int gameId)
        {
            /*ScreenManager.Instance.SetActiveScreen(ScreenManager.ScreenEnum.GameplayScreen);

            var timeHelper = new TimeHelper(StartTime);

            var logger = new Logger(Logger.SolutionPath + "\\logs\\client_game_" + gameId + ".txt", timeHelper);

            _gameModel = new GameModel(GameFactory.CreateClientGameLevel(arena), timeHelper);

            _gameModel.Update(new GameTime());

            Trace.Listeners.Add(logger);
            Trace.WriteLine("Game initialized (model created, first synchroframe applied)");
            Trace.Listeners.Remove(Logger.ClientLogger);

            // GameModel initialized, set boolean flag  
            IsGameStarted = true;*/
        }

        public void GameOver()
        {
            ConnectionManager.Instance.Stop();
            //_gameModel = null;
            //ScreenManager.Instance.SetActiveScreen(ScreenManager.ScreenEnum.MainMenuScreen);

            IsGameStarted = false;
        }

        public void Destroy()
        {
            if (IsGameStarted)
            {
                // todo leave game
                ConnectionManager.Instance.Stop();
            }
        }

        public void DrawWorld(SpriteBatch spriteBatch)
        {
            //_gameModel.Draw(spriteBatch);
        }

        public void UpdateWorld(GameTime gameTime)
        {
            //_gameModel.Update(gameTime);
        }

        #region обработка ввода

        private DateTime _dateTime;
        private DateTime _lastWeaponChanged;

        public void HandleInput(Controller controller)
        {
            
        }

        #endregion

        public Guid? Login(string username, string password, out AccountManagerErrorCode errorCode)
        {
            // TODO check for null
            Guid? id = ConnectionManager.Instance.Login(username, password, out errorCode);
            if (id.HasValue)
            {
                MyId = id.Value;
            }
            return MyId;
        }

        public AccountManagerErrorCode Logout()
        {
            return ConnectionManager.Instance.Logout();
        }
    }
}
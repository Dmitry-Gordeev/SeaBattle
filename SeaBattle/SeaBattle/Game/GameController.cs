using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeaBattle.Common;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Service;
using SeaBattle.Common.Session;
using SeaBattle.Common.Utils;
using SeaBattle.Input;
using SeaBattle.NetWork;
using SeaBattle.Screens;
using SeaBattle.Service.Ships;
using SeaBattle.Service.ShipSupplies;
using SeaBattle.Service.StaticObjects;
using SeaBattle.Ships;
using SeaBattle.ShipSupplies;

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
            Borders = new IStaticObject[4];
            Ships = new ClientShip[10];
        }

        #endregion

        //private GameModel _gameModel;

        // todo temporary
        public static long StartTime { get; set; }

        private int _countOfUpdates;

        public bool IsGameStarted { get; private set; }

        public int DataSize;

        public int MyID { get; private set; }
        public IStaticObject[] Borders;
        public ClientShip[] Ships;
        public ClientCompass ClientCompass;

        private void Shoot(Vector2 direction)
        {
            //ConnectionManager.Instance.Shoot(TypeConverter.Xna2XnaLite(direction));
        }

        private void Move(Vector2 direction)
        {
            //ConnectionManager.Instance.Move(TypeConverter.Xna2XnaLite(direction));
        }

        public void StartGame(int gameId, byte[] dataBytes)
        {
            var timeHelper = new TimeHelper(StartTime);
            UpdateWorld(dataBytes);
            
            // GameModel initialized, set boolean flag
            IsGameStarted = true;

            ScreenManager.Instance.SetActiveScreen(ScreenManager.ScreenEnum.GameplayScreen);
        }

        public void UpdateWorld(byte[] dataBytes)
        {
            if (dataBytes == null) return;

            DataSize += dataBytes.Count();
            int pos = 0;
            for (int i = 0; i < 4; i++)
            {
                if (Borders[i] == null)
                    Borders[i] = new Border(Side.Bottom);
                Borders[i].DeSerialize(ref pos, dataBytes);
            }
            if (ClientCompass == null)
            {
                ClientCompass = new ClientCompass();
            }
            ClientCompass.Compass.DeSerialize(ref pos, dataBytes);

            int countOfShips = CommonSerializer.GetInt(ref pos, dataBytes);

            for (int i = 0; i < countOfShips; i++)
            {
                if (Ships[i] == null)
                {
                    Ships[i] = new ClientShip(new Lugger());
                }
                Ships[i].Ship.DeSerialize(ref pos, dataBytes);
            }
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

        public AccountManagerErrorCode Login(string username, string password)
        {
            return ConnectionManager.Instance.Login(username, password);;
        }

        public void Logout()
        {
            ConnectionManager.Instance.Logout();
        }
    }
}
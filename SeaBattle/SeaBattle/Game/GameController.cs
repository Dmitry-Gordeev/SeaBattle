using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SeaBattle.Common.GameEvent;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Service;
using SeaBattle.Common.Utils;
using SeaBattle.Input;
using SeaBattle.NetWork;
using SeaBattle.Screens;
using SeaBattle.Service.Ships;
using SeaBattle.Service.ShipSupplies;
using SeaBattle.Ships;
using SeaBattle.ShipSupplies;
using SeaBattle.View;

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

        public bool IsGameStarted { get; private set; }

        public string MyLogin { get; set; }

        public ClientShip MyShip
        {
            get
            {
                return Ships.FirstOrDefault(ship => ship.Ship.Player.Name == MyLogin);
            }
        }

        public IStaticObject[] Borders;
        public ClientShip[] Ships;
        public List<ClientBullet> Bullets;
        public ClientWindVane ClientWindVane;

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

            ConnectionManager.Instance.InitializeThreadAndTimers();

            ScreenManager.Instance.SetActiveScreen(ScreenManager.ScreenEnum.GameplayScreen);
        }

        public void UpdateWorld(byte[] dataBytes)
        {
            if (dataBytes == null) return;

            int pos = 0;

            // Корабли
            int countOfShips = CommonSerializer.GetInt(ref pos, dataBytes);
            for (int i = 0; i < countOfShips; i++)
            {
                if (Ships[i] == null)
                {
                    Ships[i] = new ClientShip(new Corvette());
                }
                Ships[i].Ship.DeSerialize(ref pos, dataBytes);
            }

            // Флюгер
            if (ClientWindVane == null)
            {
                ClientWindVane = new ClientWindVane();
            }
            ClientWindVane.WindVane.DeSerialize(ref pos, dataBytes);

            // Ядра
            Bullets = new List<ClientBullet>();
            int countOfBullets = CommonSerializer.GetInt(ref pos, dataBytes);
            for (int i = 0; i < countOfBullets; i++)
            {
                var bullet = new Bullet();
                bullet.DeSerialize(ref pos, dataBytes);
                Bullets.Add(new ClientBullet(bullet));
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
            CheckKeyboard(controller);
            CheckMouse(controller);
        }

        private void CheckKeyboard(Controller controller)
        {
            var keyboard = controller as KeyboardAndMouse;
            if (keyboard == null) return;
            
            switch (Settings.Default.KeyboardLayout)
            {
                case 0:
                    if (keyboard.IsNewKeyPressed(Keys.W)) ConnectionManager.Instance.AddClientGameEvent(new GameEvent(0, EventType.SailsUp));
                    if (keyboard.IsNewKeyPressed(Keys.S)) ConnectionManager.Instance.AddClientGameEvent(new GameEvent(0, EventType.SailsDown));
                    if (keyboard.IsNewKeyPressed(Keys.A)) ConnectionManager.Instance.AddClientGameEvent(new GameEvent(0, EventType.TurnLeftBegin));
                    if (keyboard.IsNewKeyPressed(Keys.D)) ConnectionManager.Instance.AddClientGameEvent(new GameEvent(0, EventType.TurnRightBegin));
                    if (keyboard.IsUnpressed(Keys.A)) ConnectionManager.Instance.AddClientGameEvent(new GameEvent(0, EventType.TurnLeftEnd));
                    if (keyboard.IsUnpressed(Keys.D)) ConnectionManager.Instance.AddClientGameEvent(new GameEvent(0, EventType.TurnRightEnd));
                    break;
                case 1:
                    if (keyboard.IsNewKeyPressed(Keys.Up)) ConnectionManager.Instance.AddClientGameEvent(new GameEvent(0, EventType.SailsUp));
                    if (keyboard.IsNewKeyPressed(Keys.Down)) ConnectionManager.Instance.AddClientGameEvent(new GameEvent(0, EventType.SailsDown));
                    if (keyboard.IsNewKeyPressed(Keys.Left)) ConnectionManager.Instance.AddClientGameEvent(new GameEvent(0, EventType.TurnLeftBegin));
                    if (keyboard.IsNewKeyPressed(Keys.Right)) ConnectionManager.Instance.AddClientGameEvent(new GameEvent(0, EventType.TurnRightBegin));
                    if (keyboard.IsUnpressed(Keys.Left)) ConnectionManager.Instance.AddClientGameEvent(new GameEvent(0, EventType.TurnLeftEnd));
                    if (keyboard.IsUnpressed(Keys.Right)) ConnectionManager.Instance.AddClientGameEvent(new GameEvent(0, EventType.TurnRightEnd));
                    break;
            }
        }

        private void CheckMouse(Controller controller)
        {
            var mouse = controller as KeyboardAndMouse;
            if (mouse == null) return;

            if (mouse.ShootButtonPressed)
            {
                var coords = CommonSerializer.Vector2ToBytes(Camera2D.RelativePosition(mouse.SightPosition)).ToArray();
                ConnectionManager.Instance.AddClientGameEvent(new GameEvent(0, EventType.Shoot, coords));
            }
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
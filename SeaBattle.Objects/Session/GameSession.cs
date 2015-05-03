using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Microsoft.Xna.Framework;
using SeaBattle.Common.GameEvent;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Service;
using SeaBattle.Common.Session;
using SeaBattle.Common.Utils;
using SeaBattle.Service.Ships;
using SeaBattle.Service.ShipSupplies;
using SeaBattle.Service.StaticObjects;

namespace SeaBattle.Service.Session
{
    class GameSession
    {
        #region private fields

        private readonly List<ICustomSerializable> StaticObjects;
        private readonly List<ShipBase> _ships;
        private readonly List<IBullet> _bullets;

        private bool IsFirstData = true;

        private long _timerCounter;

        private long _lastUpdate;
        private long _updateDelay;

        private Timer _gameTimer;
        private object _updating;

        private TimeHelper _timeHelper;

        #endregion

        public GameDescription LocalGameDescription { get; private set; }
        public GameLevel GameLevel { get; private set; }
        public WindVane WindVane { get; private set; }

        public GameSession(GameDescription gameDescription)
        {
            #region инициализация объектов

            WindVane = new WindVane(true);
            GameLevel = new GameLevel(Constants.LevelWidth, Constants.LevelHeigh);
            LocalGameDescription = gameDescription;
            StaticObjects = InitializeBorders();
            _ships = InitializeShips();
            _gameTimer = new Timer();

            #endregion

            Start();
        }

        public byte[] GetInfo()
        {
            var result = new byte[] { };

            result = StaticObjects.Aggregate(result, (current, staticObject) => current.Concat(staticObject.Serialize()).ToArray());
            result = result.Concat(WindVane.Serialize()).ToArray();
            result = result.Concat(BitConverter.GetBytes(_ships.Count)).ToArray();
            result = _ships.Aggregate(result, (current, ship) => current.Concat(ship.Serialize()).ToArray());

            return result;
        }

        public void HandleGameEvent(GameEvent gameEvent, string playerName)
        {
            var ship = _ships.FirstOrDefault(s => s.Player.Name == playerName);
            if (ship == null)
                return;

            // Если поворот корабля
            if (gameEvent.Type == EventType.TurnLeftBegin ||
                gameEvent.Type == EventType.TurnLeftEnd ||
                gameEvent.Type == EventType.TurnRightBegin ||
                gameEvent.Type == EventType.TurnRightEnd)
            {
                ship.TurnTheShip(gameEvent);
            }
            
            // Если операция с парусами
            if (gameEvent.Type == EventType.SailsDown ||
                gameEvent.Type == EventType.SailsUp)
            {
                ship.ShipSupplies.Sails.UpdateSailsState(gameEvent);
            }
        }

        #region private methods

        private void Start()
        {
            _timeHelper = new TimeHelper(TimeHelper.NowMilliseconds);

            _timerCounter = 0;
            _updating = false;

            _lastUpdate = 0;
            _updateDelay = 0;

            _gameTimer.Start();

            LocalGameDescription.IsGameStarted = true;

            Console.WriteLine("Game " + LocalGameDescription.GameId + " Started");

        }

        private List<ICustomSerializable> InitializeBorders()
        {
            var borders = new List<ICustomSerializable>
            {
                new Border(Side.Top),
                new Border(Side.Right),
                new Border(Side.Bottom),
                new Border(Side.Left)
            };

            return borders;
        }

        private List<ShipBase> InitializeShips()
        {
            var ships = new List<ShipBase>{};
            var rnd = new Random();

            ships.AddRange(LocalGameDescription.Players.Select(player =>
                new Lugger(player, WindVane)
                {
                    Coordinates = new Vector2(rnd.Next(100, Constants.LevelWidth - 100), 
                        rnd.Next(100, Constants.LevelHeigh - 100))
                }));

            return ships;
        }

        #endregion
    }
}

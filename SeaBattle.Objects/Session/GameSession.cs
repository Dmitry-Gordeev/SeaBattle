using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Session;
using SeaBattle.Common.Utils;

namespace SeaBattle.Service.Session
{
    class GameSession
    {
        #region private fields

        protected readonly List<ICustomSerializable> _staticObjects;
        protected readonly List<IShip> _ships;
        protected readonly List<IBullet> _bullets;

        protected long _timerCounter;

        protected long _lastUpdate;
        protected long _updateDelay;

        protected Timer _gameTimer;
        protected object _updating;

        protected TimeHelper _timeHelper;

        #endregion

        public GameDescription LocalGameDescription { get; private set; }
        public bool IsStarted { get; protected set; }
        public GameLevel GameLevel { get; private set; }

        public GameSession(int maxPlayersAllowed,
            GameModes gameMode, int gameID, MapSet mapType)
        {
            IsStarted = false;
            GameLevel = new GameLevel(Constants.LevelWidth, Constants.LevelHeigh);

            var players = new List<IPlayer>();

            LocalGameDescription = new GameDescription(players, maxPlayersAllowed, gameID, mapType, gameMode);
        }

        #region private methods

        public virtual void Start()
        {
            #region инициализация объектов



            #endregion

            _timeHelper = new TimeHelper(TimeHelper.NowMilliseconds);

            _timerCounter = 0;
            _updating = false;

            _lastUpdate = 0;
            _updateDelay = 0;

            _gameTimer.Start();

            Trace.WriteLine("Game " + LocalGameDescription.GameId + " Started");

            IsStarted = true;
        }
        #endregion
    }
}

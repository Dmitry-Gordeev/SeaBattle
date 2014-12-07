using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;
using Microsoft.Xna.Framework;
using SeaBattle.Common.Session;
using SeaBattle.Common.Utils;

namespace SeaBattle.Service.Session
{
    class GameSession
    {
        #region private fields

        protected readonly List<AGameObject> _gameObjects;
        protected readonly List<AGameObject> _newObjects;

        protected readonly SpiderFactory _spiderFactory;
        protected readonly BonusFactory _bonusFactory;
        protected readonly WallFactory _wallFactory;

        protected readonly TeamsList _sessionTeamsList = new TeamsList();

        protected long _timerCounter;
        protected long _intervalToSpawn;

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
            GameMode gameType, int gameID, int teams)
        {
            IsStarted = false;
            GameLevel = new GameLevel(Constants.LevelWidth, Constants.LevelHeigh);

            var playerNames = new List<string>();

            LocalGameDescription = new GameDescription(playerNames, maxPlayersAllowed, gameID);
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

            Trace.WriteLine("Game Started");

            IsStarted = true;
        }
        #endregion
    }
}

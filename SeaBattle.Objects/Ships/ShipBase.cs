using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using SeaBattle.Common;
using SeaBattle.Common.GameEvent;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Service;
using SeaBattle.Common.Utils;
using SeaBattle.Service.ShipSupplies;
using XnaAdapter;

namespace SeaBattle.Service.Ships
{
    public abstract class ShipBase : IShip
    {
        #region Timers

        protected Timer UpdateCoordinatesTimer;
        protected Timer UpdateDirectionToTheLeftTimer;
        protected Timer UpdateDirectionToTheRightTimer;
        private Timer _cooldounOfShootTimer;

        #endregion
        #region Constructors

        protected ShipBase()
        {
            Player = new Player();
        }

        protected ShipBase(Player player, WindVane windVane)
        {
            Player = player;
            var rnd = new Random();

            // Init random direction
            _moveVector = new Vector2((float)rnd.NextDouble() - 0.5f, (float)rnd.NextDouble() - 0.5f);
            _moveVector.Normalize();

            // Init random coordinates
            _coordinates = new Vector2(200, 600);
            _isEnableForShoot = true;

            UpdateCoordinatesTimer = new Timer(UpdateCoordinates, null, 1000, 50);
        }

        #endregion

        #region Fields

        protected string Name;
        protected float ShipWeight;

        protected ShipCrew ShipCrew;
        public Supplies ShipSupplies;

        private Vector2 _moveVector;
        private Vector2 _coordinates;

        private bool _isEnableForShoot;

        #endregion

        #region Properties

        public bool SomethingChanged { get; set; }
        public object Lock { get; set; }
        public abstract float Height { get; }

        public float Health { get; set; }

        public float FullWeight { get { return ShipWeight + ShipSupplies.ShipHold.LoadWeight; } }

        public float Speed
        {
            get
            {
                var angle = PolarCoordinateHelper.GetAngle(ShipSupplies.WindVane.Direction, MoveVector);
                var relativeSpeed = ShipSupplies.WindVane.ForceOfWind * (float)Math.Cos(angle);
                relativeSpeed *= ShipSupplies.Sails.SailsSpeed;
                return relativeSpeed > 0 ? relativeSpeed : 0;
            }
        }

        public Vector2 MoveVector
        {
            get { return _moveVector; }
            set { _moveVector = value; }
        }

        public Vector2 Coordinates
        {
            get { return _coordinates; } 
            set { _coordinates = value; }
        }

        public Player Player { get; set; }

        #endregion

        #region Methods

        public void Shoot(List<IBullet> bullets, GameEvent gameEvent)
        {
            if (!_isEnableForShoot)
                return;

            //_isEnableForShoot = false;
            _cooldounOfShootTimer = new Timer(ShootingTimer, null, 3000, 10000);
            int pos = 0;
            var vectorTo = CommonSerializer.GetVector2(ref pos, gameEvent.ExtraData);
            bullets.Add(new Bullet(BulletType.Cannonball, Player.Name, Coordinates, vectorTo));
            Console.WriteLine(vectorTo);
        }
        protected abstract void InicializeFields(WindVane windVane);
        protected abstract void TurnToTheLeft(object obj);
        protected abstract void TurnToTheRight(object obj);
        
        protected void UpdateCoordinates(object obj)
        {
            _coordinates += _moveVector * Speed;
        }

        public void TurnTheShip(GameEvent gameEvent)
        {
            switch (gameEvent.Type)
            {
                case EventType.TurnLeftBegin:
                    UpdateDirectionToTheLeftTimer = new Timer(TurnToTheLeft, null, 0, 50);
                    break;
                case EventType.TurnLeftEnd:
                    UpdateDirectionToTheLeftTimer.Dispose();
                    break;
                case EventType.TurnRightBegin:
                    UpdateDirectionToTheRightTimer = new Timer(TurnToTheRight, null, 0, 50);
                    break;
                case EventType.TurnRightEnd:
                    UpdateDirectionToTheRightTimer.Dispose();
                    break;
            }
        }

        private void ShootingTimer(object obj)
        {
            _isEnableForShoot = true;
            _cooldounOfShootTimer.Dispose();
        }

        #endregion

        #region Serialization

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            if (dataBytes[position++] == 0) return;

            Player.Name = CommonSerializer.GetString(ref position, dataBytes);
            Coordinates = CommonSerializer.GetVector2(ref position, dataBytes);
            MoveVector = CommonSerializer.GetVector2(ref position, dataBytes);
            Health = CommonSerializer.GetFloat(ref position, dataBytes);
            ShipCrew.DeSerialize(ref position, dataBytes);
            ShipSupplies.DeSerialize(ref position, dataBytes);
        }

        public byte[] Serialize()
        {
            //if (!SomethingChanged) return new byte[]{0};
            var result = new byte[] {1};

            result = result.Concat(CommonSerializer.StringToBytesArr(Player.Name)).ToArray();
            result = result.Concat(CommonSerializer.Vector2ToBytesArr(Coordinates)).ToArray();
            result = result.Concat(CommonSerializer.Vector2ToBytesArr(MoveVector)).ToArray();
            result = result.Concat(BitConverter.GetBytes(Health)).ToArray();
            result = result.Concat(ShipCrew.Serialize()).ToArray();
            result = result.Concat(ShipSupplies.Serialize()).ToArray();

            SomethingChanged = false;
            return result;
        }

        #endregion
    }
}

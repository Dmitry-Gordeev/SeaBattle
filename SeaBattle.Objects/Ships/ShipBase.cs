using System;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using SeaBattle.Common;
using SeaBattle.Common.GameEvent;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Utils;
using SeaBattle.Service.ShipSupplies;
using XnaAdapter;

namespace SeaBattle.Service.Ships
{
    public abstract class ShipBase : IShip
    {
        #region Constructors

        protected Timer UpdateCoordinatesTimer;
        protected Timer UpdateDirectionToTheLeftTimer;
        protected Timer UpdateDirectionToTheRightTimer;

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

        #endregion

        #region Properties

        public bool SomethingChanged { get; set; }
        public object Lock { get; set; }
        public bool IsStatic { get { return false; } }
        public int ID { get; private set; }
        public abstract float Height { get; }

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

        #endregion

        #region Serialization

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            if (dataBytes[position++] == 0) return;

            Player.Name = CommonSerializer.GetString(ref position, dataBytes);
            Coordinates = CommonSerializer.GetVector2(ref position, dataBytes);
            MoveVector = CommonSerializer.GetVector2(ref position, dataBytes);
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
            result = result.Concat(ShipCrew.Serialize()).ToArray();
            result = result.Concat(ShipSupplies.Serialize()).ToArray();

            SomethingChanged = false;
            return result;
        }

        #endregion
    }
}

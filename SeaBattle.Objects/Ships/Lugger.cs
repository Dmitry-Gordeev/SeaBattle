using System.Threading;
using Microsoft.Xna.Framework;
using SeaBattle.Common;
using SeaBattle.Common.GameEvent;
using SeaBattle.Service.ShipSupplies;
using XnaAdapter;

namespace SeaBattle.Service.Ships
{
    public class Lugger : ShipBase
    {
        public Lugger(Player player)
            : base(player)
        {
            InicializeFields();
        }

        public Lugger()
        {
            InicializeFields();
        }

        #region Methods

        public override float Height
        {
            get { return 10f; }
        }
        
        protected override sealed void InicializeFields()
        {
            ShipCrew = new ShipCrew(10, 24, 16, 8);
            Name = "Lugger";
            ShipWeight = 1000;
            ShipSupplies = new Supplies(new Cannons(4, 4, 2, 2), new ShipHold(), new Sails());
        }

        public override void TurnTheShip(GameEvent gameEvent)
        {
            switch (gameEvent.Type)
            {
                case EventType.TurnLeftBegin:
                    UpdateDirectionTimer = new Timer(UpdateDirection, true, 0, 50);
                    break;
                case EventType.TurnLeftEnd:
                    UpdateDirectionTimer.Dispose();
                    break;
                case EventType.TurnRightBegin:
                    UpdateDirectionTimer = new Timer(UpdateDirection, false, 0, 50);
                    break;
                case EventType.TurnRightEnd:
                    UpdateDirectionTimer.Dispose();
                    break;
            }
        }

        protected override void UpdateDirection(object toTheRight)
        {
            MoveVector = PolarCoordinateHelper.TurnVector2(MoveVector, (bool)toTheRight ? -0.05f : 0.05f);
        }

        #endregion
    }
}

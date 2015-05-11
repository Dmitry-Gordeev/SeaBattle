using SeaBattle.Common;
using SeaBattle.Service.ShipSupplies;
using XnaAdapter;

namespace SeaBattle.Service.Ships
{
    public sealed class Corvette : ShipBase
    {
        public Corvette(Player player, WindVane windVane)
            : base(player, windVane)
        {
            InicializeFields(windVane);
        }

        public Corvette()
        {
            InicializeFields(null);
        }


        public override float Height
        {
            get { return 10f; }
        }

        #region Methods
        
        #region Protected methods
        protected override void InicializeFields(WindVane windVane)
        {
            ShipCrew = new ShipCrew(10, 24, 16, 8);
            Name = "Corvette";
            ShipWeight = 1000;
            Health = 2000f;
            ShipSupplies = new Supplies(new Cannons(4, 4, 2, 2), new ShipHold(), new Sails(), windVane);
        }

        protected override void TurnToTheLeft(object obj)
        {
            MoveVector = PolarCoordinateHelper.TurnVector2(MoveVector, -0.05f);
        }

        protected override void TurnToTheRight(object obj)
        {
            MoveVector = PolarCoordinateHelper.TurnVector2(MoveVector, 0.05f);
        }
        #endregion

        #endregion
    }
}

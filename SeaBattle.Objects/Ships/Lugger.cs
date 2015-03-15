using SeaBattle.Common.Objects;
using SeaBattle.Service.ShipSupplies;

namespace SeaBattle.Service.Ships
{
    public class Lugger : ShipBase
    {
        public Lugger(IPlayer player)
            : base(player)
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

        #endregion
    }
}

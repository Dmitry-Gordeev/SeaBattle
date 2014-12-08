﻿namespace SeaBattle.Service.Ships
{
    public class Lugger : ShipBase
    {
        public Lugger()
        {
            typeOfShip = (typeof(Lugger));
            InicializeFields();
        }

        #region Methods

        public override float Height
        {
            get { return 10f; }
        }
        
        protected override sealed void InicializeFields()
        {
            Name = "Lugger";
            ShipWeight = 1000;
            ShipCrew.Rowers = 8;
            ShipCrew.Sailors = 24;
            ShipCrew.PirateFighters = 10;
            ShipCrew.Gunners = 16;
        }

        #endregion
    }
}

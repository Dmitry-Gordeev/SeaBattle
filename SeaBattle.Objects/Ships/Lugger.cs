﻿using System.Threading;
using Microsoft.Xna.Framework;
using SeaBattle.Common;
using SeaBattle.Common.GameEvent;
using SeaBattle.Service.ShipSupplies;
using XnaAdapter;

namespace SeaBattle.Service.Ships
{
    public class Lugger : ShipBase
    {
        public Lugger(Player player, WindVane windVane)
            : base(player, windVane)
        {
            InicializeFields(windVane);
        }

        public Lugger()
        {
            InicializeFields(null);
        }

        #region Methods

        public override float Height
        {
            get { return 10f; }
        }
        
        protected override sealed void InicializeFields(WindVane windVane)
        {
            ShipCrew = new ShipCrew(10, 24, 16, 8);
            Name = "Lugger";
            ShipWeight = 1000;
            ShipSupplies = new Supplies(new Cannons(4, 4, 2, 2), new ShipHold(), new Sails(), windVane);
        }

        protected override void TurnToTheLeft(object obj)
        {
            MoveVector = PolarCoordinateHelper.TurnVector2(MoveVector, 0.05f);
        }

        protected override void TurnToTheRight(object obj)
        {
            MoveVector = PolarCoordinateHelper.TurnVector2(MoveVector, -0.05f);
        }

        #endregion
    }
}

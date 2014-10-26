using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Graphics;
using SeaBattle;

namespace SeaBattle.Objects.Ships
{
    public class Lugger : ShipBase
    {
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
            Name = "Lugger";
            ShipWeight = 1000;
            Rowers = 8;
            Sailors = 24;
            PirateFighters = 10;
            Gunners = 16;
        }

        #endregion
    }
}

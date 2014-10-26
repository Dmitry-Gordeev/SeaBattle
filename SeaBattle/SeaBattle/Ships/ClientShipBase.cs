using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeaBattle.Objects.Ships;

namespace SeaBattle.Ships
{
    class ClientShipBase : ShipBase
    {
        public override float Height
        {
            get { throw new NotImplementedException(); }
        }

        protected override void InicializeFields()
        {
            throw new NotImplementedException();
        }
    }
}

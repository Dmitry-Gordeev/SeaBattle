using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SeaBattle.Common.Objects
{
    interface IStaticObject : ICustomSerializable, IObject
    {
        Vector2 Coordinates { get; set; }
    }
}

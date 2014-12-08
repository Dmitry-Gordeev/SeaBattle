using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeaBattle.Common.Service;

namespace SeaBattle.Common.Objects
{
    public interface IBullet : IObject
    {
        BulletType Type { get; }
    }
}

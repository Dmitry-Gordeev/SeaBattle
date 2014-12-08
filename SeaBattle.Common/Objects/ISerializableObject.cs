using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SeaBattle.Common.Objects
{
    public interface ISerializableObject
    {
        bool SomethingChanged { get; set; }
        void DeSerialize(ref int position, byte[] dataBytes);
        byte[] Serialize();
    }
}

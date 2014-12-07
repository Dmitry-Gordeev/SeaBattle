using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SeaBattle.Common.Objects
{
    public interface ISerializableObject
    {
        object DeSerialize(ref long position, byte[] dataBytes);
        byte[] Serialize(ref long position);
        void GetObjectData();
    }
}

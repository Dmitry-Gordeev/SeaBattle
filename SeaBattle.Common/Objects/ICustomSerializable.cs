using System.Collections.Generic;

namespace SeaBattle.Common.Objects
{
    public interface ICustomSerializable
    {
        void DeSerialize(ref int position, byte[] dataBytes);
        IEnumerable<byte> Serialize();
    }
}


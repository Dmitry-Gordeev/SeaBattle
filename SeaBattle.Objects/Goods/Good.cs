using System;
using System.Collections.Generic;
using System.Linq;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Utils;

namespace SeaBattle.Service.Goods
{
    public class Good : ICustomSerializable
    {
        public bool SomethingChanged { get; set; }
        public object Lock { get; set; }
        public string Name;
        public int Count;
        public float Weight;

        public float FullWeight
        {
            get { return Weight*Count; }
        }

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            Name = CommonSerializer.GetString(ref position, dataBytes);
            Count = CommonSerializer.GetInt(ref position, dataBytes);
            Weight = CommonSerializer.GetFloat(ref position, dataBytes);
        }

        public IEnumerable<byte> Serialize()
        {
            var result = CommonSerializer.StringToBytes(Name);
            result = result.Concat(BitConverter.GetBytes(Count));
            result = result.Concat(BitConverter.GetBytes(Weight));

            return result;
        }
    }
}

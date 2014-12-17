using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeaBattle.Common.Objects;
using SeaBattle.Common.Utils;

namespace SeaBattle.Service.Goods
{
    public class Good : ISerializableObject
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

        public byte[] Serialize()
        {
            var result = new byte[] {};

            result = result.Concat(BitConverter.GetBytes(Name.Length)).ToArray();
            result = result.Concat(Encoding.Unicode.GetBytes(Name)).ToArray();
            result = result.Concat(BitConverter.GetBytes(Count)).ToArray();
            result = result.Concat(BitConverter.GetBytes(Weight)).ToArray();

            return result;
        }
    }
}

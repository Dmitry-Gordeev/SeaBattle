using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeaBattle.Common.Objects;

namespace SeaBattle.Service.Goods
{
    public class Good : ISerializableObject
    {
        public bool SomethingChanged { get; set; }
        public string Name;
        public int Count;
        public float Weight;

        public float FullWeight
        {
            get { return Weight*Count; }
        }

        public void DeSerialize(ref int position, byte[] dataBytes)
        {
            throw new NotImplementedException();
        }

        public byte[] Serialize()
        {
            var result = new byte[] {};

            result = (byte[])result.Concat(BitConverter.GetBytes(Name.Length));
            result = (byte[])result.Concat(Encoding.Unicode.GetBytes(Name));
            result = (byte[])result.Concat(BitConverter.GetBytes(Count));
            result = (byte[])result.Concat(BitConverter.GetBytes(Weight));

            return result;
        }
    }
}

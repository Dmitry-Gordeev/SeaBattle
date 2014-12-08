using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaBattle.Common.Utils
{
    public class CommonSerializer
    {
        public static float GetFloat(ref int position, byte[] dataBytes)
        {
            var result = BitConverter.ToSingle(dataBytes, position);
            position += 4;
            return result;
        }

        public static int GetInt(ref int position, byte[] dataBytes)
        {
            var result = BitConverter.ToInt32(dataBytes, position);
            position += 4;
            return result;
        }

        public static string GetString(ref int position, byte[] dataBytes)
        {
            var length = GetInt(ref position, dataBytes);
            position += 4;

            var result = Encoding.Unicode.GetString(dataBytes, position, length);
            position += length;

            return result;
        }
        
        public static bool[] BytesArrToBools(ref int position, byte[] bools, int boolsLength)
        {
            return null;
        }
    }
}

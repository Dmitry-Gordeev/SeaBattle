using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeaBattle.Common.Utils
{
    public class Converter
    {
        public static byte[] BoolArrToBytes(bool[] bools)
        {
            int bytes = bools.Length/8;
            if ((bools.Length%8) != 0) bytes++;
            var result = new byte[bytes];
            int bitIndex = 0, byteIndex = 0;
            for (int i = 0; i < bools.Length; i++)
            {
                if (bools[i])
                {
                    result[byteIndex] |= (byte) (1 << bitIndex);
                }
                bitIndex++;
                if (bitIndex != 8) continue;
                bitIndex = 0;
                byteIndex++;
            }
            return result;
        }

        public static bool[] BytesArrToBools(bool[] bools, int boolsLength)
        {
            return null;
        }
    }
}

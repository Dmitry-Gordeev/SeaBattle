using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SeaBattle.Common.Utils
{
    public class CommonSerializer
    {
        public static double GetDouble(ref int position, byte[] dataBytes)
        {
            var result = BitConverter.ToDouble(dataBytes, position);
            position += 8;
            return result;
        }

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
            var length = 2 * GetInt(ref position, dataBytes); // In Unicode 1 symbol = 2 bytes

            var result = Encoding.Unicode.GetString(dataBytes, position, length); 
            position += length;

            return result;
        }

        public static bool[] BytesToBools(ref int position, byte[] bools, int boolsLength)
        {
            return null;
        }

        public static IEnumerable<byte> StringToBytes(string str)
        {
            return BitConverter.GetBytes(str.Length).Concat(Encoding.Unicode.GetBytes(str));
        }

        public static IEnumerable<byte> Vector2ToBytes(Vector2 vector)
        {
            return BitConverter.GetBytes(vector.X).Concat(BitConverter.GetBytes(vector.Y));
        }

        public static Vector2 GetVector2(ref int position, byte[] bytes)
        {
            var result = new Vector2();

            result.X = GetFloat(ref position, bytes);
            result.Y = GetFloat(ref position, bytes);

            return result;
        }

        public static IEnumerable<byte> BoolArrToBytes(bool[] bools)
        {
            int bytes = bools.Length / 8;
            if ((bools.Length % 8) != 0) bytes++;
            var result = new byte[bytes];
            int bitIndex = 0, byteIndex = 0;
            for (int i = 0; i < bools.Length; i++)
            {
                if (bools[i])
                {
                    result[byteIndex] |= (byte)(1 << bitIndex);
                }
                bitIndex++;
                if (bitIndex != 8) continue;
                bitIndex = 0;
                byteIndex++;
            }
            return result;
        }
    }
}

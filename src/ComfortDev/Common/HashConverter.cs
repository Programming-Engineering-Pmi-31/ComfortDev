using System;
using System.Collections.Generic;
using System.Text;

namespace ComfortDev.BLL.Helpers
{
    public static class HashConverter
    {
        public static string ToString(byte[] bytes)
        {
            return BitConverter.ToString(bytes);
        }
        public static byte[] ToBytes(string str)
        {
            string[] hexDigits = str.Split('-');
            byte[] result = new byte[hexDigits.Length];
            for (int i = 0; i < hexDigits.Length; ++i)
            {
                result[i] = byte.Parse(hexDigits[i], System.Globalization.NumberStyles.HexNumber);
            }
            return result;
        }
        public static byte[] PlainTextToBytes(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }
    }
}

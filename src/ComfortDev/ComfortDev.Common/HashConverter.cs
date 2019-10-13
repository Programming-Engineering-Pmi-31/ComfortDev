using System;
using System.Collections.Generic;
using System.Text;

namespace ComfortDev.Common
{
    public static class HashConverter
    {
        public static string ToString(byte[] bytes)
        {
            return BitConverter.ToString(bytes);
        }
        public static byte[] PlainTextToBytes(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }
    }
}

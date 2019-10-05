using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ComfortDev.Common
{
    public static class Secrets
    {
        public static string ConnectionString { get; } = "Host=localhost;Port=5432;Database=ComfortDev;Username=postgres;Password=1058";
        public static string MD5Key { get; } = "JaNdRgUjXn2r5u8x/A?D(G+KbPeShVmYp3s6v9y$B&E)H@McQfTjWnZr4t7w!z%C";
        public static string JwtKey { get; } = "9y$B&E)H@McQfTjW";
    }
}

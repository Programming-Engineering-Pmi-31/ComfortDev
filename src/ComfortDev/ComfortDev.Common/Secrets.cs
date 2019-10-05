

namespace ComfortDev.Common
{
    public static class Secrets
    {
        public static string ConnectionString { get; } = "Host=localhost;Port=5432;Database=ComfortDev;Username=postgres;Password=1058";
        public static string MD5Key { get; } = "JaNdRgUjXn2r5u8x/A?D(G+KbPeShVmYp3s6v9y$B&E)H@McQfTjWnZr4t7w!z%C";
        public static string JwtKey { get; } = "6w9z$C&E)H@McQfTjWnZr4u7x!A%D*G-JaNdRgUkXp2s5v8y/B?E(H+MbPeShVmY";
    }
}

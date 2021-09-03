using MlkPwgen;

namespace intSoft.MVC.Core.Utilities
{
    public static class Sets
    {
        public const string Lower = "abcdefghijklmnopqrstuvwxyz";
        public const string Upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string Digits = "0123456789";
        public const string Alphanumerics = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public const string Symbols = "!@#$%^&*()";
        public const string FullSymbols = "!\"#$%&\'()*+,-./:;<=>?@[\\]^_`{|}~";
    }

    public static class InvitationCodeGenerator
    {
        public static string GenerateCode(int length = 10, string allowed = Sets.Alphanumerics)
        {
            return PasswordGenerator.Generate(length, allowed);
        }
    }
}
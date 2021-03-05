using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ModuleStatusInterface
{
    class Tokenizer
    {
        public static string GetToken(ModuleStatus module)
        {

            var salt = "ThisAppIsMadeForCokesburyAndPacMessageIsASillyNameMike";
            var time = FormatTime(module.CreateDate);
            var hash = MD5.Create();
            var token = hash.ComputeHash(Encoding.UTF8.GetBytes(salt + module.AppId + time));
            return ByteArrToString(token);
        }

        private static string FormatTime(DateTime time)
        {
            return $"{time.Year}/{time.Month}/{time.Day}/{time.Hour}";
        }

        private static string ByteArrToString(byte[] bytes)
        {
            var sb = new StringBuilder();
            Array.ForEach<byte>(bytes, x => sb.Append(x.ToString("x2")));
            return sb.ToString();
        }
    }
}

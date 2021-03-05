using System;
using System.Collections.Generic;
using System.Configuration;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Dashboard.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Dashboard.Models
{
    public class TokenGenerator
    {
        public static string GetToken(ModuleStatu module)
        {
            
            var salt = ConfigurationManager.AppSettings["SaltiestSalt"];
            var time =FormatTime(module.CreateDate.Value);
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
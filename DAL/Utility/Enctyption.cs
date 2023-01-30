using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace DAL.Utility;

public static class Enctyption
{
    public static readonly string? LocalSalt = new ConfigurationBuilder() // todo: make dir dynamic
        .AddJsonFile("D:/MINE/ПРОГРАММИРОВАНИЕ/C#/Projects/iThoughtWebApi/DAL/config.json")
        .Build()
        .GetSection("local_salt").Value;

    public static string Sha256HashSumOf(string str, string? salt = null)
    {
        str += salt;
        
        var builder = new StringBuilder();
        using (var hash = SHA256.Create())
        {
            var enc = Encoding.UTF8;
            var result = hash.ComputeHash(enc.GetBytes(str));

            foreach (byte b in result)
                builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }
}
using System;
using System.Security.Cryptography;
using System.Text;

namespace CoWorkingApp.Data.Tools
{
    public static class EncrypData
    {
        public static string EncryptText(string Text)
        {
            using(var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Text));
                var has = BitConverter.ToString(hashBytes).Replace("-","").ToLower();
                return has;
            }
        }
    public static string GetPassword()
    {
        string passwordInput = "";
        while(true)
        {
            var keyPress = Console.ReadKey(true);
            if(keyPress.Key == ConsoleKey.Enter)
            {
                Console.WriteLine(" ");
                break;
            }
            else
            {
                Console.Write("*");
                passwordInput +=  keyPress.KeyChar;
            }
            
        }
        return passwordInput;
    }
    }

    
}
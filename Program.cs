
//Basic RSA encryption tool in C#

//TO-DO
//-add a visual GUI
//-add other encryption methods


using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Encryption_Console_App
{

    public class RsaEncryption {

        private static RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);
        private RSAParameters _privateKey;
        private RSAParameters _publicKey;

        public RsaEncryption()
        {
            _privateKey = csp.ExportParameters(true);
            _publicKey = csp.ExportParameters(false);
        }

        public string GetPublicKey()
        {
            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw,_publicKey);
            return sw.ToString();
        }

        public string Encrypt(string plainText)
        {
            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(_publicKey);
            var data = Encoding.Unicode.GetBytes(plainText);
            var cypher = csp.Encrypt(data,false);
            return Convert.ToBase64String(cypher);
        }

        public string Decrypt(string cypherText)
        {
            var dataBytes = Convert.FromBase64String(cypherText);
            csp.ImportParameters(_privateKey);
            var plainText; _ = csp.Decrypt(dataBytes, false);
            return Encoding.Unicode.GetString(plainText);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            RsaEncryption rsa = new RsaEncryption();
            string cypher = string.Empty;

            Console.WriteLine("input block to encrypt:");
            var text = Console.ReadLine();
            if (!string.IsNullOrEmpty(text)) {
                cypher = rsa.Encrypt(text);
                Console.WriteLine($"Encrypted Block: {cypher}");
            }

            Console.WriteLine("press any key to decrypt block");
            Console.ReadLine();
            string plainText = rsa.Decrypt(cypher);

            Console.WriteLine("Hello");
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ControlerAtm.Utilitario
{
    public class Seguridad
    {
        private Rijndael rijndael;
        private byte[] key;
        private byte[] iv;

        public Seguridad() {
            rijndael = Rijndael.Create();
            key = UTF8Encoding.UTF8.GetBytes("C80BA5028D1E81E3042EBF1649D8ED31");//LLave maestra
            iv = UTF8Encoding.UTF8.GetBytes("serviciosWeb2015");// Semilla
        }

        public string encriptar_informacion(string informacion) {
            byte[] inputBytes = Encoding.ASCII.GetBytes(informacion);

            byte[] encripted;

            using (MemoryStream ms =

            new MemoryStream(inputBytes.Length))
            {

                using (CryptoStream objCryptoStream =

                new CryptoStream(ms,

                rijndael.CreateEncryptor(key, iv),

                CryptoStreamMode.Write))
                {

                    objCryptoStream.Write(inputBytes, 0, inputBytes.Length);

                    objCryptoStream.FlushFinalBlock();

                    objCryptoStream.Close();

                }

                encripted = ms.ToArray();

            }

            return Convert.ToBase64String(encripted);
        }

        public string desencriptar_informacion(string encriptadoInformacion) {
            byte[] inputBytes = Convert.FromBase64String(encriptadoInformacion);

            byte[] resultBytes = new byte[inputBytes.Length];

            string textoLimpio = String.Empty;

            using (MemoryStream ms = new MemoryStream(inputBytes))
            {

                using (CryptoStream objCryptoStream =

                new CryptoStream(ms, rijndael.CreateDecryptor(key, iv),

                CryptoStreamMode.Read))
                {

                    using (StreamReader sr =

                    new StreamReader(objCryptoStream, true))
                    {

                        textoLimpio = sr.ReadToEnd();

                    }

                }

            }

            return textoLimpio;
            
        }

    }
}

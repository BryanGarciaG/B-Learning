using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace BLearning.Models.Negocio
{
    public class LogicEncriptacionMetodos
    {
        /// <summary>
        /// Encripta una cadena string.
        /// </summary>
        /// <param name="_cadena_no_encriptada">cadena no encriptada</param>
        /// <returns>cadena encriptada</returns>
        public string Encrypt(string _cadena_no_encriptada)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadena_no_encriptada);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        /// <summary>
        /// Desencripta una cadena string.
        /// </summary>
        /// <param name="_cadena_encriptada">cadena encriptada</param>
        /// <returns>cadena desencriptada</returns>
        public string Decrypt(string _cadena_encriptada)
        {
            try
            {

                string result = string.Empty;
                byte[] decryted = Convert.FromBase64String(_cadena_encriptada);
                //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
                result = System.Text.Encoding.Unicode.GetString(decryted);
                return result;
            }
            catch (Exception)
            {

                return "0";
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SSC.SBOSDK
{
    public static class ModelAPP
    {
        private const string privateKey = "<RSAKeyValue><Modulus>vG/d7oA9MB0uYo3cxts3dQsf/l+POZVIaKPlGQAWxVzgK1VzifAFgMJUd0NKLMiqvRCJ6rhCLzYCKDUxpWyMmC4BlmWX/HogfSzBMKT9iBaU/2Cyin+nklA+NZ3EarGJL1ZJS9e3VfS61ZPF/94PWZuULBtJUNF02BZ6OVNOtdM=</Modulus><Exponent>AQAB</Exponent><P>62TmtFVP96xQV2YqItXraZkF3x5obR/reNI7NeUmCvgEIvHxfEq+U8smRRJB14MoPZZl8IEcS2FNBwu8Y2ebUw==</P><Q>zO6sI15mqSEjLDONxh4X+cu7pqFXiR6Cblih+ype8JLnp321XpHsRP83MW9NC2CYDMuvNCzrxRmDVDjrqSOrgQ==</Q><DP>wIXRDtLryZS8TQ85DS7LEJ3gKIE1RXMi4rmHReg5+iSpNW+OY2q6ScxQa5OoGDykT+LPUyo12w0ks8uMc/zMEw==</DP><DQ>mYWTX0uxDLLObqaQZwLUY0XE5ieoNAivHYs4jbhIN2FWOZtq69XVcjrfViFTTlqmja9pKWUdmyJpyAZ8RNF8AQ==</DQ><InverseQ>Pd+mXm7w9YQ6BmtIGWYBWIV1p0vSfnOoYryjfp3UMGAQ35ytQoaBkwKESY4dMtRufGVIwwMXpDyuKW1WjP9G5g==</InverseQ><D>fS/IfhFeFR/d2AWtHcM3VZ+9co3jpfrCLxprMi+38QhhuQg9CO+XiFISMWVX2ua5X3+kUHZ3Kcw0pKeqQt8Ziz/FCoFynODjZLCnE3PA0xMEQqO51spxOopAGuelSlKdCBzZy91cHKmo2POAq5qB/YpOv6i3wx7JPOenj8s2PQE=</D></RSAKeyValue>";
        private const string Deckey = "<RSAKeyValue><Modulus>vG/d7oA9MB0uYo3cxts3dQsf/l+POZVIaKPlGQAWxVzgK1VzifAFgMJUd0NKLMiqvRCJ6rhCLzYCKDUxpWyMmC4BlmWX/HogfSzBMKT9iBaU/2Cyin+nklA+NZ3EarGJL1ZJS9e3VfS61ZPF/94PWZuULBtJUNF02BZ6OVNOtdM=</Modulus><Exponent>AQAB</Exponent><P>62TmtFVP96xQV2YqItXraZkF3x5obR/reNI7NeUmCvgEIvHxfEq+U8smRRJB14MoPZZl8IEcS2FNBwu8Y2ebUw==</P><Q>zO6sI15mqSEjLDONxh4X+cu7pqFXiR6Cblih+ype8JLnp321XpHsRP83MW9NC2CYDMuvNCzrxRmDVDjrqSOrgQ==</Q><DP>wIXRDtLryZS8TQ85DS7LEJ3gKIE1RXMi4rmHReg5+iSpNW+OY2q6ScxQa5OoGDykT+LPUyo12w0ks8uMc/zMEw==</DP><DQ>mYWTX0uxDLLObqaQZwLUY0XE5ieoNAivHYs4jbhIN2FWOZtq69XVcjrfViFTTlqmja9pKWUdmyJpyAZ8RNF8AQ==</DQ><InverseQ>Pd+mXm7w9YQ6BmtIGWYBWIV1p0vSfnOoYryjfp3UMGAQ35ytQoaBkwKESY4dMtRufGVIwwMXpDyuKW1WjP9G5g==</InverseQ><D>fS/IfhFeFR/d2AWtHcM3VZ+9co3jpfrCLxprMi+38QhhuQg9CO+XiFISMWVX2ua5X3+kUHZ3Kcw0pKeqQt8Ziz/FCoFynODjZLCnE3PA0xMEQqO51spxOopAGuelSlKdCBzZy91cHKmo2POAq5qB/YpOv6i3wx7JPOenj8s2PQE=</D></RSAKeyValue>";
        private const string myKey = "DWGTYJVMP";
        public static MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        public static TripleDESCryptoServiceProvider des1 = new TripleDESCryptoServiceProvider();

        public static RSACryptoServiceProvider sec;
        public static string llave = string.Empty;

        public static string Encriptar(string texto)
        {
            des1.Key = hashmd5.ComputeHash((new UnicodeEncoding()).GetBytes(myKey));
            des1.Mode = CipherMode.ECB;
            if (texto.ToString().Trim().Equals(""))
            {
                return "";
            }
            else
            {
                ICryptoTransform desdencrypt = des1.CreateEncryptor();
                ASCIIEncoding MyASCIIEncoding = new ASCIIEncoding();
                Byte[] buff = UnicodeEncoding.ASCII.GetBytes(texto);
                return Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));
            }
        }
        public static string Desencriptar(string texto)
        {
            try
            {
                if (texto.ToString().Trim().Equals(""))
                {
                    return "";
                }
                else
                {
                    des1.Key = hashmd5.ComputeHash((new UnicodeEncoding()).GetBytes(myKey));
                    des1.Mode = CipherMode.ECB;
                    ICryptoTransform desdencrypt = des1.CreateDecryptor();
                    ASCIIEncoding MyASCIIEncoding = new ASCIIEncoding();
                    Byte[] buff = Convert.FromBase64String(texto);
                    return UnicodeEncoding.ASCII.GetString(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));//Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));
                }
                
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public static string Decrypt(string pTextToDecrypt)
        {
            sec = new RSACryptoServiceProvider();
            string cadena = Deckey;
            sec.FromXmlString(cadena);
            UTF8Encoding ue = new UTF8Encoding();
            byte[] bytDesEncriptar;
            string strDesencriptar = string.Empty;
            if ((pTextToDecrypt is DBNull) == false)
            {
                if (!pTextToDecrypt.Equals(string.Empty))
                {
                    try
                    {
                        bytDesEncriptar = sec.Decrypt(Convert.FromBase64String(pTextToDecrypt), false);
                        strDesencriptar = ue.GetString(bytDesEncriptar);
                    }
                    catch (SystemException Exception)
                    {

                    }
                }
            }
            return strDesencriptar;
        }
        public static string Encrypt(string pTextToEncript)
        {
            sec = new RSACryptoServiceProvider();
            sec.FromXmlString(privateKey);
            UTF8Encoding ue = new UTF8Encoding();
            byte[] bytString;
            byte[] bytEncriptar;
            string strEncriptar = string.Empty;
            if (!pTextToEncript.Equals(string.Empty))
            {
                try
                {
                    bytString = ue.GetBytes(pTextToEncript);
                    bytEncriptar = sec.Encrypt(bytString, false);
                    strEncriptar = Convert.ToBase64String(bytEncriptar);
                }
                catch (SystemException Exception)
                {
                    return pTextToEncript;
                }

            }
            return strEncriptar;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using BCrypt.Net;

/// <summary>
/// Descripción breve de seguridad
/// </summary>
public class seguridad
{
    private readonly static string _clave = "PMS|250494"; // Clave de cifrado.  

    public static string DesEncriptar(string _cadenaAdesencriptar)
    {
        string result = string.Empty;
        byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
        //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
        result = Encoding.Unicode.GetString(decryted);
        return result;
    }

    public static string Encriptar(string _cadenaAencriptar)
    {
        string result = string.Empty;
        byte[] encryted = Encoding.Unicode.GetBytes(_cadenaAencriptar);
        result = Convert.ToBase64String(encryted);

        string encriptado = result;

        result = null;

        return encriptado;
    }
    /// <summary>
    /// Encriptar password de usuario para validar inicio de sesión o al crear usuario.
    /// </summary>
    public string passwordUser(string password)
    {

            string clave = "309253934_alml|slm|rcmt";
            SHA512 sha512 = new SHA512CryptoServiceProvider();

            byte[] inputBytes = (new UnicodeEncoding()).GetBytes(password + clave);

            byte[] hash = sha512.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        
    }

    // Función para cifrar una cadena.
    public static string cifrar(string cadena)
    {
        byte[] llave; //Arreglo donde guardaremos la llave para el cifrado 3DES.
        byte[] arreglo = UTF8Encoding.UTF8.GetBytes(cadena); //Arreglo donde guardaremos la cadena descifrada.
                                                             // Ciframos utilizando el Algoritmo MD5.
        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_clave));
        md5.Clear();
        //Ciframos utilizando el Algoritmo 3DES.
        TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
        tripledes.Key = llave;
        tripledes.Mode = CipherMode.ECB;
        tripledes.Padding = PaddingMode.PKCS7;
        ICryptoTransform convertir = tripledes.CreateEncryptor(); // Iniciamos la conversión de la cadena
        byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length); //Arreglo de bytes donde guardaremos la cadena cifrada.
        tripledes.Clear();
        return Convert.ToBase64String(resultado, 0, resultado.Length); // Convertimos la cadena y la regresamos.
    }
}
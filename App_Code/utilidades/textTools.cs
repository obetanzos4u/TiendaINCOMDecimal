using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;
using Newtonsoft.Json;
/// <summary>
/// Descripción breve de textTools
/// </summary>
public class textTools
{
    public static string DataTableToJSON(DataTable table) {
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(table);
        return JSONString;
        }
    private static Random random = new Random();
    /// <summary>
    /// Regresa una cadena Random
    /// </summary>
    public static string RandomString(int length) {
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    /// <summary>
    /// Elimina espacios dobles y al inicio y final de una cadena de texto
    /// </summary>
    public static string depurarEspacios(string cadenaOriginal)
    {

        string cadenaLimpia = cadenaOriginal.Replace("  ", " ").Trim(' ');
        cadenaLimpia = cadenaLimpia.Replace("   ", " ").Trim('\t');
        return cadenaLimpia;
    }

    /// <summary>
    /// Elimina espacios dobles, espacios al inicio y final, saltos de linea y tabuladores
    /// </summary>
    public static string lineSimple(string cadenaOriginal)
    {
        if (cadenaOriginal != null && cadenaOriginal != "")
            {
            cadenaOriginal = cadenaOriginal.Replace("\n", "");
            cadenaOriginal = cadenaOriginal.Replace("\r", "");
            cadenaOriginal = cadenaOriginal.Replace("\n\r", "");
            cadenaOriginal = cadenaOriginal.Replace("\v", "");
            cadenaOriginal = cadenaOriginal.Replace("\t", "");
            string cadenaLimpia = cadenaOriginal.Replace("  ", " ").Trim(' ');

            return cadenaLimpia;
            } else return cadenaOriginal;
    }
    /// <summary>
    /// Elimina espacios dobles y tabuladores
    /// </summary>
    public static string lineMulti(string cadenaOriginal) {

        if (cadenaOriginal != null && cadenaOriginal != "") {
            cadenaOriginal = cadenaOriginal.Replace("\t", "");
        string cadenaLimpia = cadenaOriginal.Replace("  ", " ").Trim(' ');

        return cadenaLimpia;
            } 
        else return cadenaOriginal;
        }
    /// <summary>
    /// Elimina letras, espacios dobles, saltos de linea y tabuladores 
    /// </summary>
    public static double soloNumeros(string cadenaOriginal)
    {
        cadenaOriginal = depurarEspacios(cadenaOriginal);

        string cadenaLimpia = Regex.Replace(cadenaOriginal, @"[a-zA-Z]+", "");
        cadenaLimpia = lineSimple(cadenaLimpia).Replace("$", "").Replace(",", "").Replace("'", "").Replace("\\","").Replace(" ", ""); ;


        return float.Parse(cadenaLimpia);
    }
    /// <summary>
    /// Elimina letras, espacios dobles, saltos de linea y tabuladores 
    /// </summary>
    public static int soloNumerosInt(string cadenaOriginal)
    {
        cadenaOriginal = depurarEspacios(cadenaOriginal);

        string cadenaLimpia = Regex.Replace(cadenaOriginal, @"[a-zA-Z]+", "");
        cadenaLimpia = lineSimple(cadenaLimpia).Replace("$", "").Replace(",", "").Replace("'", "").Replace("\\", "").Replace(" ", ""); ;


        return int.Parse(cadenaLimpia);
    }
    /// <summary>
    /// Elimina letras, espacios dobles, saltos de linea y tabuladores 
    /// </summary>
    public static float soloNumerosF(string cadenaOriginal)
    {
        cadenaOriginal = depurarEspacios(cadenaOriginal);

        string cadenaLimpia = Regex.Replace(cadenaOriginal, @"[a-zA-Z]+", "");
        cadenaLimpia = lineSimple(cadenaLimpia).Replace("$", "").Replace(",", "").Replace("'", "").Replace("\\", "").Replace(" ", "");

        try { 
            return float.Parse(cadenaLimpia); } catch { return float.NaN; }

    }    /// <summary>
         /// Elimina letras, espacios dobles, saltos de linea y tabuladores y simbolos Act.20190102
         /// </summary>
         /// Act. 20190102
    public static decimal toDecimal(string cadenaOriginal)
    {
        cadenaOriginal = depurarEspacios(cadenaOriginal);

        string cadenaLimpia = Regex.Replace(cadenaOriginal, @"[a-zA-Z]+", "");

        cadenaLimpia = String.Concat(cadenaLimpia.Where(c => !Char.IsWhiteSpace(c)));

        cadenaLimpia = lineSimple(cadenaLimpia)
               .Replace("%", "")
                .Replace("$", "")
                .Replace(",", "")
                 .Replace("\"", "")
                .Replace("'", "")
                .Replace("\\", "")
                .Replace("  ", "")
                .Replace(" ", "")
              .Replace("-", "");
    
            return decimal.Parse(cadenaLimpia);
        
      
    }
    /// <summary>
    /// Elimina letras, espacios dobles, saltos de linea y tabuladores y simbolos Act.20190102
    /// </summary>
    /// Act. 20190102
    public static decimal soloNumerosD(string cadenaOriginal) {
        cadenaOriginal = depurarEspacios(cadenaOriginal);

        string cadenaLimpia = Regex.Replace(cadenaOriginal, @"[a-zA-Z]+", "");

          cadenaLimpia = String.Concat(cadenaLimpia.Where(c => !Char.IsWhiteSpace(c)));

        cadenaLimpia = lineSimple(cadenaLimpia)
               .Replace("%", "")
                .Replace("$", "")
                .Replace(",", "")
                 .Replace("\"", "")
                .Replace("'", "")
                .Replace("\\", "")
                .Replace("  ", "")
                .Replace(" ", "")
              .Replace("-", "");
        try { 
            return decimal.Parse(cadenaLimpia); 
        } catch { 
            return decimal.MinusOne; 
        }

        }
    /// <summary>
    /// Si el email es válido, devuelve true.
    /// </summary>
    public static Boolean validarEmail(String emailV)
    {
        String expresion;
        expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
        if (Regex.IsMatch(emailV, expresion))
        {
            if (Regex.Replace(emailV, expresion, String.Empty).Length == 0) { return true; }
            else { return false; }
        }
        else { return false; }
    }
    /// <summary>
    /// Convierte un array a uns tring separado por comas
    /// </summary>
    public static string arrayToString(string[] arr) {
        if(arr!= null) { 
        StringBuilder builder = new StringBuilder();
        foreach (string s in arr) {
            builder.Append(s).Append(",");
        }
        return builder.ToString().TrimEnd(new char[] { ',' });
        } return "";
    }

    /// <summary>
    /// Ayuda a convertir un tipo [Object] proveniente de un DataRow(de DataTable) en [Decimal],[DateTime],[Int32],[Single][Boolean]
    /// si no tiene valor alguno, lo devolvera como nulo
    /// </summary>
    public static dynamic nullableParse(object obj) {
        String tipo = obj.GetType().Name.ToString();
        string valor = obj.ToString();

        switch (tipo) {
            case "Decimal": { if (!string.IsNullOrWhiteSpace(valor)) return decimal.Parse(valor); else return null; }
            case "DateTime": { if (!string.IsNullOrWhiteSpace(valor)) return DateTime.Parse(valor); else return null; }
            case "Int32": { if (!string.IsNullOrWhiteSpace(valor)) return int.Parse(valor); else return null; }
            case "Single": { if (!string.IsNullOrWhiteSpace(valor)) return float.Parse(valor); else return null; }
            case "Boolean": { if (!string.IsNullOrWhiteSpace(valor)) return decimal.Parse(valor); else return null; }
            default: return null;
        }
    }



    public static string QuitarAcentos(string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    /// <summary>
    /// Obtiene todos los nodos descendientes de un nodo especificado
    ///  
    /// </summary>
    public static List<string> XMLobtenerNodo(XDocument xml, string nodo)
    {

        List<string> values = new List<string>();
        var query = xml.Descendants(nodo);

        foreach (XElement element in query)
        {

            string keyName = element.Name.LocalName;

            values.Add(element.Value);

         
        }
        return values;

    }


    public static string limpiarURL(string url) {
        string urlLimpia = url.Replace(" ", "-").Replace(",", "").Replace("/", "-").Replace(".", "").Replace("'", "").Replace("\"", "").Replace("&", "-Y-").Replace("+", "").Replace(":", "-");
        return urlLimpia;
    }
    public static string limpiarURL_NumeroParte(string url) {
        string urlLimpia = url.Replace(".", ",").Replace("/", "~").Replace("-", "_");
        return urlLimpia;
        }
    public static string recuperarURL_NumeroParte(string url) {
        string urlLimpia = url.Replace(",", ".").Replace("~", "/").Replace("_", "-");
        return urlLimpia;
        }
    }
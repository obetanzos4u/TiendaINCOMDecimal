using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de utilidad_fechas
/// </summary>
public class utilidad_fechas
{
    public utilidad_fechas()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    /// <summary>
    /// Obtiene la fecha Central sumando o restando días en el formato "yyy-MM-dd"
    /// </summary>
    public static string obtenerFechaSQL(int dias) {
        DateTime fecha = obtenerCentral(); // Genera la fecha actual menos 1 hora para el uso horario del servidor
        fecha = fecha.AddDays(dias);
        string fechaSQL = fecha.ToString(fecha.ToString("yyy-MM-dd"));  // No utilizable al insertar

        return fechaSQL;
    }
    /// <summary>
    /// Obtiene la fecha Central actual  en el formato "yyy-MM-dd"
    /// </summary>
    public static string obtenerFechaSQL() {
        DateTime fecha = obtenerCentral(); // Genera la fecha actual menos 1 hora para el uso horario del servidor
        string fechaSQL = fecha.ToString(fecha.ToString("yyy-MM-dd"));  // No utilizable al insertar

        return fechaSQL;
    }
    /// <summary>
    /// Obtiene la fecha  Central Standard Time (-6 CDMX)
    /// </summary>
    public static DateTime obtenerCentral() {
          
        // Coordinated Universal Time string from 
        // DateTime.Now.ToUniversalTime().ToString("u");

        // Local .NET timeZone.
        // DateTime localDateTime = DateTime.UtcNow;
        // DateTime utcDateTime = localDateTime.ToUniversalTime();

        // ID from: 
        // "HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion\Time Zone"
        // See http://msdn.microsoft.com/en-us/library/system.timezoneinfo.id.aspx
        string nzTimeZoneKey = "Central Standard Time (Mexico)";
        TimeZoneInfo nzTimeZone = TimeZoneInfo.FindSystemTimeZoneById(nzTimeZoneKey);
        DateTime nzDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, nzTimeZone);

        return nzDateTime;

    }
    /// <summary>
    /// Convierte una fecha a la hora central =  Central Standard Time (-6 CDMX)
    /// </summary>
    public static DateTime ConvertirFechaToCentral(DateTime fechaToConvert) {

        // Coordinated Universal Time string from 
        // DateTime.Now.ToUniversalTime().ToString("u");

        // Local .NET timeZone.
        // DateTime localDateTime = DateTime.UtcNow;
        // DateTime utcDateTime = localDateTime.ToUniversalTime();

        // ID from: 
        // "HKEY_LOCAL_MACHINE\Software\Microsoft\Windows NT\CurrentVersion\Time Zone"
        // See http://msdn.microsoft.com/en-us/library/system.timezoneinfo.id.aspx
        string nzTimeZoneKey = "Central Standard Time";
        TimeZoneInfo nzTimeZone = TimeZoneInfo.FindSystemTimeZoneById(nzTimeZoneKey);
        DateTime nzDateTime = TimeZoneInfo.ConvertTimeFromUtc(fechaToConvert, nzTimeZone);

        return nzDateTime;

        }
    /// <summary>
    /// Devuelve un entero entre días de una fecha recibida contra la fecha actual
    /// </summary>
    public static int calcularDiferenciaDias(DateTime fecha) {
        TimeSpan ts = obtenerCentral() - fecha;
        return ts.Days;
        }
    /// <summary>
    /// Devuelve la fecha actual en el siguiente formato [AAAMMDD] o [yyyyMMdd]
    /// </summary>
    public static string AAAMMDD() {
        DateTime DateTime = obtenerCentral();
        return DateTime.ToString("yyyyMMdd") ;
        }
   
    }
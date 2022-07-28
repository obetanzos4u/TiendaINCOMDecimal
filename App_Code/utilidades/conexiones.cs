using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Descripción breve de conexiones
/// </summary>
public class conexiones
{
    static public string conexionTienda()
    {
       
        return System.Configuration.ConfigurationManager.ConnectionStrings["tiendaIncom"].ToString(); 
    }

    public static SmtpClient smtp(string usuario, string password)
    {
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = true;
        NetworkCredential emailAcceso = new NetworkCredential(usuario, password); // Credenciales de Usuario
        smtp.Credentials = emailAcceso;
        smtp.Port = 587;
        return smtp;
    }

    public static SmtpClient smtp()
    {
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = true;
        NetworkCredential emailAcceso = new NetworkCredential("cmiranda@it4u.com.mx", "lydwgdtzufoshycw"); // Credenciales de Usuario ktkbvtbumplmjaov
        smtp.Credentials = emailAcceso;
        smtp.Port = 587;
        return smtp;
    }
}
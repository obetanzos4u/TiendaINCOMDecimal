using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Descripción breve de email_Cotizaciones
/// </summary>
[ObsoleteAttribute("Esta clase es obsoleta ya no usar.", false)]

public class email
{

    public string resultadoMensaje;
    public bool resultado;

    public static string remitenteCredenciales = "serviciosweb@incom.mx";
    protected static string passwordCredenciales = "qvetsakzonfdmknn";

    protected static string remitenteCredencialesDevelopment = "serviciosweb@incom.mx"; // cmiranda@it4u.com.mx
    protected static string passwordCredencialesDevelopment = "qvetsakzonfdmknn"; // Credenciales de usuario ktkbvtbumplmjaov - lydwgdtzufoshycw

    protected string asunto { get; set; }
    protected string destinatarios { get; set; }
    protected string mensaje { get; set; }
    protected string remitente { get; set; }
    protected List<Attachment> attachment { get; set; }
    protected MailMessage mm { get; set; }

    public email(string _asunto, string _destinatarios, string _mensaje, string _remitente, List<Attachment> _attachment)
    {
        asunto = _asunto;
        destinatarios = destinatarios = textTools.lineSimple(_destinatarios).Trim(','); ;
        mensaje = _mensaje;
        remitente = _remitente;
        attachment = _attachment;

    }

    public email(string _asunto, string _destinatarios, string _mensaje, string _remitente)
    {
        asunto = _asunto;
        destinatarios = destinatarios = textTools.lineSimple(_destinatarios).Trim(','); ;
        mensaje = _mensaje;
        remitente = _remitente;
    }
    protected static SmtpClient smtp()
    {

        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = true;
        NetworkCredential emailAcceso = new NetworkCredential(remitenteCredenciales, passwordCredenciales); // Credenciales de Usuario
        smtp.Credentials = emailAcceso;

        smtp.Port = 587;
        return smtp;
    }
    protected static SmtpClient smtpDevelopment()
    {
        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = true;
        NetworkCredential emailAcceso = new NetworkCredential(remitenteCredencialesDevelopment, passwordCredencialesDevelopment); // Credenciales de Usuario
        smtp.Credentials = emailAcceso;

        smtp.Port = 587;
        return smtp;
    }
    /// <summary>
    /// Únicamente valida los destinatarios en el formato correcto
    /// </summary>
    protected bool validarDestinatarios()
    {
        if (string.IsNullOrEmpty(destinatarios))
        {
            return false;
        }
        else
        {
            destinatarios = destinatarios.Trim(' ').Replace("  ", "").Replace(" ", "").Replace("\t", "").TrimEnd(',');
            // si son múltiples destintarios validamos uno por uno convirtiendolo a array
            if (destinatarios.Contains(","))
            {
                string[] destinatariosAR = destinatarios.Split(',');

                foreach (string em in destinatariosAR)
                {

                    if (!textTools.validarEmail(em))
                    {
                        resultadoMensaje = "Email con formato incorrecto";
                        return false;
                    }
                }

                return true;
            }
            else if (!textTools.validarEmail(destinatarios))
            {
                resultadoMensaje = "Email con formato incorrecto";
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Añade los destinatarios de manera correcta, si es uno solo o si son varios (separados por coma) 
    /// </summary>
    protected void depurarDestinatarios()
    {
        destinatarios = destinatarios.Replace("  ", "").Replace("\t", "").Replace(" ", "").TrimEnd(',');

        if (destinatarios.Contains(","))
        {
            string[] destinatariosAR = destinatarios.Split(',');

            foreach (string email in destinatariosAR)
            {
                mm.To.Add(email);
            }
        }
        else
        {
            mm.To.Add(destinatarios);
        }
    }
}
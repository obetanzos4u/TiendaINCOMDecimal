using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Descripción breve de EmailCredentials
/// </summary>
public class EmailCredentials {


    readonly static string Sender = "retail@incom.mx";
    readonly static string SenderPass = "Serafincin_Retail@2021";
    public EmailCredentials() {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public static string getSender() { return Sender; }
    public static string getSenderPass() { return SenderPass; }

    public static SmtpClient getSmtp() {

        SmtpClient smtp = new SmtpClient();
        smtp.Host = "smtp.gmail.com";
        smtp.EnableSsl = true;
        smtp.UseDefaultCredentials = true;
        NetworkCredential emailAcceso = new NetworkCredential(getSender(), getSenderPass()); // Credenciales de Usuario
        smtp.Credentials = emailAcceso;

        smtp.Port = 587;
        return smtp;
    }
}
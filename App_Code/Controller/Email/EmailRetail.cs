using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Descripción breve de EmailRegistroUsuario
/// </summary>
public class EmailRetail {
    private static string nombreDe = "Incom Retail";
    private static string paraEmail { get; set; }
    private static string paraNombre { get; set; }
    private static string Subject { get; set; }
    private static string Body { get; set; }
    private static MailMessage Mensaje { get; set; }
    private static SmtpClient Smtp { get; set; }

public EmailRetail( string _paraEmail, string _paraNombre, string _Subject, string _Body) {

        paraEmail = _paraEmail;
        paraNombre = _paraNombre;
        Subject = _Subject;
        Body = _Body;
        paraEmail = _paraEmail;
        Smtp = EmailCredentials.getSmtp();
        Mensaje = new MailMessage();
    }
    public void ReplyToList(string emailList) {
        Mensaje.ReplyToList.Add(emailList);
    }

        public  void AddCC(string email) {
        Mensaje.CC.Add(email);
    }

    public  void AddCCO(string email) {
        Mensaje.Bcc.Add(email);
    }
    public  json_respuestas  Send() {

        try {
            Mensaje.To.Add(new MailAddress(paraEmail, paraNombre));
            Mensaje.From = new MailAddress(EmailCredentials.getSender(), nombreDe);
            Mensaje.Subject = Subject;
            Mensaje.Body = Body;
            Mensaje.IsBodyHtml = true;

            using (var smtp = Smtp) {
                smtp.Send(Mensaje);

            }

            return new json_respuestas(true, "Email enviado con éxito", false, null);
        }
        catch(Exception ex) {
            return new json_respuestas(false, "Error al enviar email", true, ex);
        }
 
    }
}

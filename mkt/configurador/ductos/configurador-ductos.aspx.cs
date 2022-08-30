using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class configurador_ductos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
     

    }

    protected void cotizar_Click(object sender, EventArgs e) {

        bool estatus = false;
        try
        {
            using (MailMessage mm = new MailMessage("cmiranda@incom.mx", correo.Text))
            {
                mm.Subject = "Nueva configuración de Ducto por: " + correo.Text;
                mm.IsBodyHtml = true;
                mm.Body = plantillaU(correo.Text, lugar_envio.Text, caja_comentarios.Text, _descripcion_parte.Text.Replace("\r\n", "<br>"));

                mm.Bcc.Add("ralbert@incom.mx, telemarketing@incom.mx,jespinoza@incom.mx, omunguia@incom.mx ");

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("cmiranda@incom.mx", "30925_andrelisandro"); // Credenciales de Usuario
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);

                estatus = true;
            }
        }
        catch {
            estatus = false;
        }

        if(estatus == true ) { Response.Redirect("gracias.html"); }
    }
    private string plantillaU(string correo, string lugar_envio, string comentarios, string configurador)
{
    string body = string.Empty;
    using (StreamReader reader = new StreamReader(Server.MapPath("") + @"/email_base.html")) // Importante Cambiar
    {
        body = reader.ReadToEnd();
    }
    body = body.Replace("{correo}", correo);
    body = body.Replace("{lugar_envio}", lugar_envio);
    body = body.Replace("{comentarios}", comentarios);
    body = body.Replace("{configurador}", configurador);

        return body;
}
}
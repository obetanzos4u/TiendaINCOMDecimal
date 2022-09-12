using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Net;
using System.Net.Mail;

public partial class aviso_de_privacidad : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
          
        }

    }

    protected bool validar() {
        string nombre = txt_nombre.Text;
        string telefono = txt_telefono.Text;
        string email = txt_email.Text;
        string mensaje = txt_mensaje.Text;


        if (email.Length < 2 || email.Length > 60) { lbl_mensaje.Text = "El email no cumple con la longitud"; return false; }
        if (mensaje.Length > 500) { lbl_mensaje.Text = "El mensaje excede la longitud de 500 caracteres"; return false; }
        if (nombre.Length < 2 || nombre.Length > 50) { lbl_mensaje.Text = "El nombre no cumple con la longitud"; return false; }


        return true;
    }
    protected void btn_enviar_Click(object sender, EventArgs e) {

        if (validar()) { 
        string nombre = txt_nombre.Text;
        string telefono = txt_telefono.Text;
        string email = txt_email.Text;
        string mensaje = txt_mensaje.Text;


        using (MailMessage mm = new MailMessage("development@incom.mx", "telemarketing@incom.mx")) {
            mm.Subject = "Contacto Curso Intellinet";
            mm.IsBodyHtml = true;
            mm.Body = string.Format(@"Nombre: {0} <br>
                                      Teléfono: {1} <br>
                                      Email: {2} <br>        
                                      Mensaje: {3} <br> 
                                      ", nombre,telefono,email,mensaje);

            mm.Bcc.Add("jespinoza@incom.mx, amartinez@incom.mx, rafaelf@incom.mx");

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential("cmiranda@incom.mx", "30925_andrelisandro"); // Credenciales de Usuario
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
        }
            lbl_mensaje.Text = "Contacto enviado con éxito";
        }
    }

    /*
    private string plantillaU(string var1, string var2, string var3, string var4, string var5, string var6, string var7) {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(@"<<<------Ruta de la plantilla ------>>>")) // Importante Cambiar
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{var1}", var1);
        body = body.Replace("{var2}", var2);
        body = body.Replace("{var3}", var3);
        body = body.Replace("{var4}", var4);
        body = body.Replace("{var5}", var5);
        body = body.Replace("{var6}", var6);
        body = body.Replace("{var7}", var7);

        return body;
    }

    */
}
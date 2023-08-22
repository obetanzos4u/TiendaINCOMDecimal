using System;
using System.Net;
using System.Net.Mail;
using System.Web.UI;

public partial class soplado_fremco_2022 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected bool validar()
    {
        string nombre = txt_nombre.Text;
        string correo = txt_email.Text;
        string telefono = txt_telefono.Text;
        string empresa = txt_empresa.Text;

        if (correo.Length < 2 || correo.Length > 60) { lbl_mensaje.Text = "El email no cumple con la longitud"; lbl_mensaje.ForeColor = System.Drawing.Color.Red; return false; }
        //if (empresa.Length > 1 || empresa.Length > 60) { lbl_mensaje.Text = "La empresa no cumple con la longitud"; lbl_mensaje.ForeColor = System.Drawing.Color.Red; return false; }
        if (nombre.Length < 2 || nombre.Length > 50) { lbl_mensaje.Text = "El nombre no cumple con la longitud"; lbl_mensaje.ForeColor = System.Drawing.Color.Red; return false; }

        return true;
    }

    protected void restablecerForm()
    {
        txt_nombre.Text = "";
        txt_email.Text = "";
        txt_telefono.Text = "";
        txt_empresa.Text = "";
    }

    protected void btn_enviar_Click(object sender, EventArgs e)
    {
        if (validar())
        {
            string nombre = txt_nombre.Text;
            string telefono = txt_telefono.Text;
            string empresa = txt_empresa.Text;
            string email = txt_email.Text;

            using (MailMessage mm = new MailMessage("serviciosweb@incom.mx", "telemarketing@incom.mx"))
            {
                // Agregar dirección de correo CC
                mm.CC.Add("publicidad@incom.mx");

                mm.Subject = "Cotización renta Microzanjadora";
                mm.IsBodyHtml = true;
                mm.Body = $@"Hola {nombre}, nuestro equipo de soporte se pondrá en contacto contigo para ofrecerte una cotización de acuerdo a tus necesidades.<br />
                            Datos de contacto:<br />
                            Email: {email}<br />
                            Teléfono: {telefono}<br />
                            Empresa: {empresa}";

                SmtpClient smtp = new SmtpClient();
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential("serviciosweb@incom.mx", "zfjbphbpnyqzwjvs");

                restablecerForm();
                smtp.Send(mm);
            }
            lbl_mensaje.Text = "Solicitud enviada";
            lbl_mensaje.ForeColor = System.Drawing.Color.Green;
        }
    }
}
y
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        if (empresa.Length < 2 || empresa.Length > 60) { lbl_mensaje.Text = "La empresa no cumple con la longitud"; lbl_mensaje.ForeColor = System.Drawing.Color.Red; return false; }
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

            using (MailMessage mm = new MailMessage("sistemasweb@incom.mx", "comunicacion@incom.mx"))
            {
                mm.Subject = "Contacto webinar FREMCO soplado de fibra óptica";
                mm.IsBodyHtml = true;
                mm.Body = String.Format(@"Nombre: {0} <br/>
                                        Correo: {1} <br/>
                                        Teléfono: {2} <br/>
                                        Empresa: {3}", nombre, email, telefono, empresa);

                SmtpClient smtp = new SmtpClient();
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential("serviciosweb@incom.mx", "dyrvntbdmenuqhpx");

                restablecerForm();
                smtp.Send(mm);
            }
            lbl_mensaje.Text = "Registrado con éxito";
            lbl_mensaje.ForeColor = System.Drawing.Color.Green;
        }
    }
}
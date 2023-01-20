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
        string asistencia = ddl_asistencia.SelectedValue;

        if (correo.Length < 2 || correo.Length > 60) { lbl_mensaje.Text = "El email no cumple con la longitud"; lbl_mensaje.ForeColor = System.Drawing.Color.Red; return false; }
        //if (empresa.Length > 1 || empresa.Length > 60) { lbl_mensaje.Text = "La empresa no cumple con la longitud"; lbl_mensaje.ForeColor = System.Drawing.Color.Red; return false; }
        if (nombre.Length < 2 || nombre.Length > 50) { lbl_mensaje.Text = "El nombre no cumple con la longitud"; lbl_mensaje.ForeColor = System.Drawing.Color.Red; return false; }
        if (asistencia.Length == 0 || asistencia == "") { lbl_mensaje.Text = "Debes seleccionar un tipo de asistencia"; lbl_mensaje.ForeColor = System.Drawing.Color.Red; return false; }

        return true;
    }
    protected void restablecerForm()
    {
        txt_nombre.Text = "";
        txt_email.Text = "";
        txt_telefono.Text = "";
        txt_empresa.Text = "";
        ddl_asistencia.SelectedIndex = 0;
    }
    protected void btn_enviar_Click(object sender, EventArgs e)
    {
        if (validar())
        {
            string nombre = txt_nombre.Text;
            string telefono = txt_telefono.Text;
            string empresa = txt_empresa.Text;
            string email = txt_email.Text;
            string asistencia = ddl_asistencia.SelectedValue;

            using (MailMessage mm = new MailMessage("serviciosweb@incom.mx", "comunicacion@incom.mx"))
            {
                mm.Subject = "Contacto evento CUPRUM 27-01-2023";
                mm.IsBodyHtml = true;
                mm.Body = String.Format(@"Nombre: {0} <br/>
                                        Correo: {1} <br/>
                                        Teléfono: {2} <br/>
                                        Empresa: {3} <br/>
                                        Tipo de asistencia: {4}", nombre, email, telefono, empresa, asistencia);

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
            restablecerForm();
            lbl_mensaje.Text = "Registrado con éxito, te esperamos en nuestro evento!";
            lbl_mensaje.ForeColor = System.Drawing.Color.Green;
        }
    }
}
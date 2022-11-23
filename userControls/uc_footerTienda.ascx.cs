using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class uc_footerTienda : System.Web.UI.UserControl
{
    string numero_parte { get; set; }

    protected void Page_Init(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {



        }
        else
        {


        }


    }
    protected bool validar()
    {
        Regex re = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        string email = txt_email_boletin.Text;
        if (!re.IsMatch(email))
        {
            return false;
        }
        return true;
    }
    protected void btn_enviar_boletin_Click(object sender, EventArgs e)
    {
        NotiflixJS.Loading(this, NotiflixJS.LoadingType.loading);
        if (validar())
        {
            string email = txt_email_boletin.Text;
            using (MailMessage mm = new MailMessage("sistemasweb@incom.mx", "comunicacion@incom.mx"))
            {
                mm.Subject = "Nuevo registro a boletín de noticias!";
                mm.IsBodyHtml = true;
                mm.Body = String.Format(@"Correo: {0}", email);
                SmtpClient smtp = conexiones.smtp("serviciosweb@incom.mx", "dyrvntbdmenuqhpx");
                smtp.Send(mm);
                BootstrapCSS.RedirectJs(this, Request.Url.GetLeftPart(UriPartial.Authority));
                NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
                NotiflixJS.Message(this, NotiflixJS.MessageType.success, "Registrado con éxito");
            }
        }
        else
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.warning, "El correo no es válido");
        }
        NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
    }
}
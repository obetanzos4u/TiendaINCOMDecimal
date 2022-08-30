using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Net;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        using (MailMessage mm = new MailMessage(new MailAddress("serviciosweb@incom.mx", "Cotización Icoptiks"), new MailAddress(correo.Text)))
        {
            if (__email.Text.Length < 5)
            {
                errorLista.Visible = true;
            }
            else
            {
                errorLista.Visible = false;
                mm.Subject = "Cotización Icoptiks de: " + correo.Text;//txtSubject.Text;
                mm.IsBodyHtml = true;
                string correo1 = "<table width='100%' border='0' cellspacing='0' cellpadding='0' bgcolor='#EDEDED'  style='font-size:16px;'><tbody style='font-family: Segoe, 'Segoe UI', 'DejaVu Sans', 'Trebuchet MS', Verdana, sans-serif'><tr><td><table width='578' border='0' align='center' cellpadding='0' cellspacing='0'><tbody><tr><td><p>&nbsp;</p><p>&nbsp;</p></td></tr><tr><td background='https://www.incom.mx/mkt/configurador/img/header.png' >&nbsp; </td></tr><tr><td bgcolor='#FFFFFF'><table width='520' border='0' align='center' cellpadding='0' cellspacing='0'><tbody><tr><td><img src='https://www.incom.mx/mkt/configurador/img/logo_headerConfig_Jump.gif' width='533' height='102' alt=''/></td></tr><tr><td>&nbsp;</td></tr><tr><td height='33'><p>Gracias <strong>" + correo.Text + "</strong> por cotizar con nosotros, en breve recibirás respuesta.</p></td></tr><tr><td height='130' valign='top'>Tu solicitud:";
                string correo2 = " </td></tr><tr style='text-align:center; font-size: 12px;'><td height='34'>  <hr><p><img src='https://www.incom.mx/mkt/configurador/img/Logo-incom-solo.png'/><br>Cualquier duda contáctanos al correo: <strong>telemarketing@incom.mx</strong> o a los Teléfonos: <br><strong>Para el D.F. y área metropolitana: </strong>(55) 5243-6900 <br><strong>Del interior sin costo:</strong> 800-INCOM(46266)-00<br></p></td></tr><tr><td style='text-align: center; font-size: 12px;'></td></tr></tbody></table></td></tr><tr><td><img src='https://www.incom.mx/mkt/configurador/img/footer.png' width='578' height='20' alt=''/></td></tr><tr><td style='text-align: center; font-size: 10px;'><p><strong><span style='font-size:18px;'><a href='https://www.incom.mx'>www.incom.mx</a></span><br> </strong><strong>Distribuido por Insumos Comerciales Occidente S.A. DE C.V. </strong><br>Plutarco Elías Calles 276, Colonia Tlazintla, CP 08710, Iztacalco, México CDMX Horario de atención: Lunes a Jueves de 8:00 a 19:00 hrs y Viernes de 8:00 a 17:00<br></p><p>Powered by IT4U 2022</p></td></tr></tbody></table><p>&nbsp;</p></td></tr></tbody></table>";
                mm.Body = correo1 + "<ol>" + __email.Text + "</ol>" + correo2;
                mm.Bcc.Add("serviciosweb@incom.mx");
                mm.Bcc.Add("telemarketing@incom.mx,ralbert@incom.mx,omunguia@incom.mx,isoria@incom.mx");
                //mm.Bcc.Add("rpreza@it4u.com.mx");
                //mm.Bcc.Add("cmiranda@it4u.com.mx");
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(/*txtEmail.Text*/"serviciosweb@incom.mx", "Ksyxwwtwjdvfaata");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Tu solicitud se ha enviado con éxito');", true);
                coti_exitosa.Visible = true;
                string script = @"<script type='text/javascript'>
                        
                            setTimeout(function () { $('#cuentaRegresiva').html('7') }, 1000);
                            setTimeout(function () { $('#cuentaRegresiva').html('6') }, 2000);
                            setTimeout(function () { $('#cuentaRegresiva').html('5') }, 3000);
                            setTimeout(function () { $('#cuentaRegresiva').html('4') }, 4000);
                            setTimeout(function () { $('#cuentaRegresiva').html('3') }, 5000);
                            setTimeout(function () { $('#cuentaRegresiva').html('2') }, 6000);
                            setTimeout(function () { $('#cuentaRegresiva').html('1') }, 7000);
                            setTimeout(function () { location.reload(); }, 8000);
                        </script>";

                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            }
        }
    }
}
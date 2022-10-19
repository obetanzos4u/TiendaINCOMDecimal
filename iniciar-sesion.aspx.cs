using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class iniciar_sesion : System.Web.UI.Page
{

    private readonly string Dominio = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Title = "Inicia Sesión";
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/usuario/mi-cuenta/mi-cuenta.aspx");
            }
        }
    }

    protected async void btn_iniciar_sesion_Click(object sender, EventArgs e)
    {

        string email = textTools.lineSimple(txt_email.Text);

        usuario UsuarioLogin = UsuariosEF.Obtener(email);

        if (UsuarioLogin != null)
        {
            var UsuarioInfo = UsuariosEF.ObtenerInfo(UsuarioLogin.id);
            if (UsuarioInfo != null && UsuarioInfo.registroMetodo == 3)
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.info, "Tu usuario fue creado con una cuenta de Google, inicia sesión con ese método");
                //materializeCSS.crear_toast(this, "Tu usuario fué creado con una cuenta de Google, inicia sesión con ese método", false);
                return;
            }
        }

        if (validadCampos())
        {
            usuarios validar = new usuarios();
            bool resultadoLogin = validar.validar_inicio_sesión(email, txt_password.Text);



            if (resultadoLogin == false)
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Contraseña o usuario incorrecto");
                //materializeCSS.crear_toast(this, "Contraseña o usuario incorrecto", false);
                return;
            }

            #region Valida que la cuenta este activada.
            var cuentaActiva = UsuariosEF.ValidarCuentaActiva(email);

            if (cuentaActiva.exception == false && cuentaActiva.result == false)
            {
                var cantidadEmails = await UsuariosEF.ObtenerCantidadLigasCreadas(email);

                if (cantidadEmails.response < 3)
                {
                    lbl_msg.ForeColor = System.Drawing.Color.Red;
                    lbl_msg.Text = "Tu usuario no ha sido activado, se ha enviado un email de activación";
                    lbl_msg.Visible = true;

                    var Usuario = UsuariosEF.Obtener(email);
                    var result = await UsuariosEF.EnviarEmailActivacion(Usuario, Dominio);
                    return;
                }
                else if (cantidadEmails.response >= 3)
                {
                    lbl_msg.ForeColor = System.Drawing.Color.Red;
                    lbl_msg.Text = "Tu cuenta no esta confirmada y haz superado el envío de emails de activación." +
                        "Envía un correo a @incom.mx para la activación de tu cuenta";
                    lbl_msg.Visible = true;
                }
            }
            #endregion
            usuarios usuario = usuarios.recuperar_DatosUsuario(email);

            // Create and tuck away the cookie
            FormsAuthenticationTicket authTicket =
              new FormsAuthenticationTicket(1,
                                            usuario.email,
                                            DateTime.Now,
                                            DateTime.Now.AddHours(12),
                                            true,
                                            usuario.tipo_de_usuario);
            string encTicket = FormsAuthentication.Encrypt(authTicket);

            HttpCookie faCookie =
              new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);




            validar.establecer_DatosUsuario(txt_email.Text);
            usuarios.ultimo_login(email);

            // And send the user where they were heading
            string redirectUrl =
              FormsAuthentication.GetRedirectUrl(email, false);
            Response.Redirect(Dominio + redirectUrl, false);




            // Server.Transfer("~/inicio.aspx");
        }
    }

    bool validadCampos()
    {
        string email = textTools.lineSimple(txt_email.Text);
        if (email.Length < 2 || email.Length > 60)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.info, "El correo no cumple con los requerimientos de longitud");
            //materializeCSS.crear_toast(this, "El campo email no cumple con los requerimientos de longitud.", false);
            return false;
        };
        if (textTools.validarEmail(email) == false)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.warning, "El correo no tiene el formato correcto");
            //materializeCSS.crear_toast(this, "El campo email no tiene el formato correcto", false); 
            return false;
        }
        if (txt_password.Text.Length < 6 || txt_password.Text.Length > 20)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.info, "La contraseña no cumple con los requerimientos de longitud");
            //materializeCSS.crear_toast(this, "La contraseña no cumple con los requerimientos de longitud.", false);
            return false;
        };
        return true;
    }

    protected void txt_email_TextChanged(object sender, EventArgs e)
    {
        string email = textTools.lineSimple(txt_email.Text);
        usuario UsuarioLogin = UsuariosEF.Obtener(email);

        if (UsuarioLogin != null)
        {
            var UsuarioInfo = UsuariosEF.ObtenerInfo(UsuarioLogin.id);
            if (UsuarioInfo.registroMetodo == 3)
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.info, "Tu usuario fue registrado con Google, inicia sesión con ese método");
                //materializeCSS.crear_toast(this, "Tu usuario fué creado con una cuenta de Google, inicia sesión con ese método", false);
            }
            return;
        }
    }
}
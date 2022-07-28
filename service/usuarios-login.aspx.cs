using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usuarios_login : System.Web.UI.Page
{
    private readonly string Dominio = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack) login();

    }

    public async void login()
    {

        try
        {
            if (Request.Form.Count != 2)
            {
                json_respuestas responseNullValues = new json_respuestas(false, "No se han recibido parámetros");

                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(responseNullValues));
                return; 
            
            
            }

            string email = null;
            string password = null;

            if (Request.Form[0] != null) {
            email = Request.Form["user"].ToString();
            }

            if (Request.Form[1] != null) {
            password = Request.Form["password"].ToString();
            }
            
          
         

            email = textTools.lineSimple(email);
            json_respuestas validacion = validadCampos(email, password);

            if (!validacion.result)
            {
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(validacion));
            
            }

            usuario UsuarioLogin = UsuariosEF.Obtener(email);

            if (UsuarioLogin != null) {
                var UsuarioInfo = UsuariosEF.ObtenerInfo(UsuarioLogin.id);
                if (UsuarioInfo != null && UsuarioInfo.registroMetodo == 3) {

                    var r = new json_respuestas(false,
                        "Tu usuario fué registrado con una cuenta de Google, inicia sesión de esa manera ó establece una contraseña", 
                        false).ToJson();
                    HttpContext.Current.Response.Write(r);
                    return;
                }
                   
            }


            usuarios validar = new usuarios();
            bool resultadoLogin = validar.validar_inicio_sesión(email, password);

            if (resultadoLogin == false)
            {
                json_respuestas r = new json_respuestas(false, "Contraseña o usuario incorrecto");

                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(r));
                return;
            }




            #region Valida que la cuenta este activada.
            var cuentaActiva = UsuariosEF.ValidarCuentaActiva(email);

            if (cuentaActiva.exception == false && cuentaActiva.result == false)
            {


                var cantidadEmails = await UsuariosEF.ObtenerCantidadLigasCreadas(email);

                if (cantidadEmails.response <= 3)
                {
                    var Usuario = UsuariosEF.Obtener(email);
                    var result = await UsuariosEF.EnviarEmailActivacion(Usuario, Dominio);
                    json_respuestas r = new json_respuestas(false, "Tu usuario no ha sido activado, se ha enviado un email de activación");
 

                    HttpContext.Current.Response.Write(JsonConvert.SerializeObject(r));
 

                  
                    return;
                }
                else if (cantidadEmails.response > 3)
                {
                 
                  
                    json_respuestas responseEmailsSuperados = new json_respuestas(false, "Tu cuenta no esta confirmada y haz superado el envío de emails de activación." +
                        "Envía un correo a development@incom.mx para la activación de tu cuenta");


                    HttpContext.Current.Response.Write(JsonConvert.SerializeObject(responseEmailsSuperados));
                    return;
                }


            }
            #endregion


 






            // Creamos la variable Session llamada
            validar.establecer_DatosUsuario(email);


                usuarios userLogin = usuarios.userLogin();
                // Create and tuck away the cookie
                FormsAuthenticationTicket authTicket =
                  new FormsAuthenticationTicket(1,
                                                userLogin.email,
                                                DateTime.Now,
                                                DateTime.Now.AddHours(12),
                                                true,
                                                userLogin.tipo_de_usuario);
                string encTicket = FormsAuthentication.Encrypt(authTicket);

                HttpCookie faCookie =
                  new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);


                usuarios.ultimo_login(email);

                json_respuestas responseLoginCorrecto = new json_respuestas(resultadoLogin, "Login correcto");

                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(responseLoginCorrecto));

          
           


        }
        catch (Exception ex)
        {

            json_respuestas respuesta = new json_respuestas(false, "Error al iniciar sesión",true,ex);

            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(respuesta));
        }

    }

    static json_respuestas  validadCampos(string email, string password)
    {
        json_respuestas respuesta = new json_respuestas(false, "");

        if (email.Length < 2 || email.Length > 60) { respuesta.message = "El campo email no cumple con los requerimientos de longitud."; respuesta.result = false; };
        if (textTools.validarEmail(email) == false) { respuesta.message = "El campo email no tiene el formato correcto"; respuesta.result = false; }
        if (password.Length < 6 || password.Length > 20) { respuesta.message = "La contraseña no cumple con los requerimientos de longitud."; respuesta.result = false; };


        respuesta.message = "ok"; respuesta.result = true;
        return respuesta;
    }
}
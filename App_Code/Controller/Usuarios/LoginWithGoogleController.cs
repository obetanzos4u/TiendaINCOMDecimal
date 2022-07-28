using Google.Apis.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Security;
 

public class LoginWithGoogleController : ApiController {
    private readonly string Dominio = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
    private readonly string _CLIENT_ID = "735817781293-gj8bqpplf0jt9au330hcejcp06js20di";
/*
    // GET api/<controller>
    public IEnumerable<string> Get() {
        return new string[] { "value1", "value2" };
    }

    // GET api/<controller>/5
    public string Get(int id) {
        return "value";
    }
*/
    // POST api/<controller>
    [System.Web.Http.HttpPost]
    public async Task<string> Post(dynamic AccessToken) {

        string strToken = AccessToken.AccessToken;

          String Email = null;
        String Nombre = null;
        
        try { 
        var validPayload = await GoogleJsonWebSignature.ValidateAsync(strToken);

         Email = validPayload.Email;
         Nombre = validPayload.GivenName;

        }
        catch(Exception ex) {

        return JsonConvert.SerializeObject(new json_respuestas(false, "Contraseña o usuario incorrecto"));
        }

        var ExistenciaCuenta = UsuariosEF.ValidarExistenciaUsuario(Email);

        if(ExistenciaCuenta.result == false) {

            return JsonConvert.SerializeObject(new json_respuestas(false, "El usuario no existe, regístrate primero."));
        }

        #region Valida que la cuenta este activada.
        var cuentaActiva = UsuariosEF.ValidarCuentaActiva(Email);

        if (cuentaActiva.exception == false && cuentaActiva.result == false) {


            var cantidadEmails = await UsuariosEF.ObtenerCantidadLigasCreadas(Email);

            if (cantidadEmails.response <= 3) {
                var Usuario = UsuariosEF.Obtener(Email);
                var result = await UsuariosEF.EnviarEmailActivacion(Usuario, Dominio);
               

                return  JsonConvert.SerializeObject(new json_respuestas(false, "Tu usuario no esta activado, ya se envío un examen de confirmación"));
            }
            else if (cantidadEmails.response > 3) {


                json_respuestas responseEmailsSuperados = new json_respuestas(false, "Tu cuenta no esta confirmada y haz superado el envío de emails de activación." +
                    "Envía un correo a development@incom.mx para la activación de tu cuenta");

                return JsonConvert.SerializeObject(responseEmailsSuperados);
            }


        }
        #endregion


        // Si llegó hasta este punto hacemos le login


        // Creamos la variable Session llamada
        //   validar.establecer_DatosUsuario(Email);


        usuarios userLogin = usuarios.recuperar_DatosUsuario(Email);
        // Create and tuck away the cookie
        FormsAuthenticationTicket authTicket =
          new FormsAuthenticationTicket(1,
                                        userLogin.email,
                                        DateTime.Now,
                                        DateTime.Now.AddDays(60),
                                        false,
                                        userLogin.tipo_de_usuario);
        string encTicket = FormsAuthentication.Encrypt(authTicket);

        HttpCookie faCookie =
          new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
        HttpContext.Current.Response.Cookies.Add(faCookie);


        usuarios.ultimo_login(Email);

        json_respuestas responseLoginCorrecto = new json_respuestas(true, "Login correcto");

         
        return JsonConvert.SerializeObject(responseLoginCorrecto);


    }
    /*
    // PUT api/<controller>/5
    public void Put(int id, [FromBody] string value) {
    }

    // DELETE api/<controller>/5
    public void Delete(int id) {
    }*/
}

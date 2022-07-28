using Google.Apis.Auth;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

using static Google.Apis.Auth.GoogleJsonWebSignature;

public class CrearUsuarioController : ApiController {
    private readonly string Dominio = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

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
    [EnableCors(origins: "http://www.bigfont.ca", headers: "*", methods: "*")]

    public async Task<string> Post(dynamic _Usuario) {
        string strToken = "";

       try { 
                strToken = _Usuario.AccessToken;
         } catch(Exception ex) {
            return JsonConvert.SerializeObject(new json_respuestas(false, "No se ha recibido un parámetro válido", true, null));
        }
 

        var validPayload = new Payload();

        try {
             validPayload = await GoogleJsonWebSignature.ValidateAsync(strToken);

        }catch(Exception ex) {

            return JsonConvert.SerializeObject(new json_respuestas(false, "Se creo un error al crear tu usuario", true, null));
        }

            string EmailUsuario = validPayload.Email;


        var existenciaUsuario = UsuariosEF.ValidarExistenciaUsuario(EmailUsuario);

        if (existenciaUsuario.exception == false && existenciaUsuario.result == true) {
            return JsonConvert.SerializeObject(new json_respuestas(false,"El usuario ya exíste, inicia sesión",false,null));
        }

        string Nombre = validPayload.Name;
        string ApellidoPaterno = validPayload.FamilyName;


        usuario Usuario = new usuario();
        Usuario.email = EmailUsuario;
        Usuario.nombre = Nombre;
        Usuario.apellido_paterno = ApellidoPaterno;
        Usuario.rango = 1;
        Usuario.password = "-";
        Usuario.tipo_de_usuario = "cliente";
        Usuario.grupo_asesores_adicional = "general";
        Usuario.fecha_registro = utilidad_fechas.obtenerCentral();
        Usuario.cuenta_confirmada = true;
        // Si no hay registros procede a crear

        // Procedemos a crear el usuario
        var resultCrearUsuario = await UsuariosEF.Crear(Usuario);


        if (resultCrearUsuario.result == false && resultCrearUsuario.exception == true) {

            return JsonConvert.SerializeObject(new json_respuestas(false, "Ocurrio un error al crear tu usuario.", true, null));

        }
        Usuario = resultCrearUsuario.response;

        var InfoUsuario = new usuariosInfo() {
            idUsuario = Usuario.id,
            registroMetodo = 3

        };


        var resultGuardarInfoUsuario = await UsuariosEF.CrearInfo(InfoUsuario);


        EmailRegistroUsuario EmailRegistro = new EmailRegistroUsuario(Usuario);


        var resultEmail = await EmailRegistro.EnviarEmailRegisteredWithGoogle();
        string result = resultEmail.ToJson();

        return result;

    }
    /*
    // PUT api/<controller>/5
    public void Put(int id, [FromBody] string value) {
    }

    // DELETE api/<controller>/5
    public void Delete(int id) {
    }*/
}

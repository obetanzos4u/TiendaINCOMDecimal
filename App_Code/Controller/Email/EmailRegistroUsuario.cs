using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Descripción breve de EmailRegistroUsuario
/// </summary>
public class EmailRegistroUsuario {

    private  usuario Usuario { get; set; }
    public EmailRegistroUsuario(usuario _Usuario) {
        Usuario = _Usuario;
    }


    public async Task<json_respuestas> EnviarEmailRegisteredWithGoogle() {
        try {
            string Body = "";
        string DominioURL = ConfigSite.ObtenerUrl();
        string PlantillaPath = "/email_templates/ui/usuario_RegistroBienvenidaWithGoogle.html";
        string asunto = $"{Usuario.nombre}, Gracias por tu registro en Incom.mx";

        Dictionary<string, string> datos = new Dictionary<string, string>();
        datos.Add("{nombre}", Usuario.nombre);
        datos.Add("{DominioURL}", DominioURL);


        Body = archivosManejador.reemplazarEnArchivo(PlantillaPath, datos);

        EmailRetail Email = new EmailRetail(Usuario.email, Usuario.nombre, asunto,  Body);
        Email.AddCC("serviciosweb@incom.mx");
        // Email.AddCC("cmiranda@it4u.com.mx");
        // Email.AddCC("rpreza@incom.mx");
        // Email.AddCC("development@incom.mx");
        Email.ReplyToList("serviciosweb@incom.mx,ralbert@incom.mx");

       
              Email.Send();
            return new json_respuestas(true, "Envio de email con éxito", false, null);
        }
        catch(Exception ex) {
            return new json_respuestas(false, "Ocurrió un error al enviar un email.", true, ex);

        }
      

     
    }
}
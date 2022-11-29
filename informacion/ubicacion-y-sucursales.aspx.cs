using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using RestSharp;
using System.Dynamic;
using RestSharp.Authenticators;

public partial class aviso_de_privacidad : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            Page.Title = "Ubicación, sucursales y contacto";
            Page.MetaDescription = "Sucursales, contacto y asesoria técnica en México.";

            #region Valida si el usuario ha iniciado sesión y rellna automáticamente ciertos datos
            if (HttpContext.Current.User.Identity.IsAuthenticated) {

                var user = usuarios.userLogin();
                txt_nombre.Text = user.nombre + " " + user.apellido_paterno;
                txt_email.Text = user.email;
                txt_telefono.Text = user.telefono;
            }
            #endregion
        }

    }
    protected void btn_enviarEmailContacto_Click(object sender, EventArgs e) {

        validar_camposContacto validar = new validar_camposContacto();

        bool resultado = validar.contactoSimple(txt_nombre.Text, txt_email.Text, txt_telefono.Text, txt_mensaje.Text);
        

        if (resultado) {



            DateTime fechaSolicitud= utilidad_fechas.obtenerCentral();
            string asunto = " Nombre: " + txt_nombre.Text + "";
            string mensaje = string.Empty;
            string cadenaValidacion = string.Empty;
            string filePathHTML = "/email_templates/ui/usuario_contacto.html";

            string comentario = txt_mensaje.Text;
            string infoReferencia = "";
            comentario = comentario.Replace("\n", "<br/>");
            comentario = comentario.Replace("\r", " ");



            if (Request.QueryString["info"] != null) {
                infoReferencia = infoReferencia + (" ~ " + Request.QueryString["info"].ToString());
            }
            Dictionary<string, string> datosDiccRemplazo = new Dictionary<string, string>();
            datosDiccRemplazo.Add("{dominio}", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority));
            datosDiccRemplazo.Add("{nombre}", txt_nombre.Text);
            datosDiccRemplazo.Add("{email}", txt_email.Text);
            datosDiccRemplazo.Add("{telefono}", txt_telefono.Text);
            datosDiccRemplazo.Add("{fechaSolicitud}", fechaSolicitud.ToString("D"));
            datosDiccRemplazo.Add("{asunto}", "contacto");
            datosDiccRemplazo.Add("{comentario}", comentario + " <br>" + infoReferencia);

            string mensajeCut = txt_mensaje.Text;
            if(mensajeCut.Length> 400)
            {
                mensajeCut = txt_mensaje.Text.Substring(0, 399);
            }
            // Inicio de guardar registro en la base de datos



            solicitudes_usuarios contactoSimple = new solicitudes_usuarios();
            contactoSimple.activo = 0;
            contactoSimple.nombre = textTools.lineSimple(txt_nombre.Text);
            contactoSimple.asunto = "contacto";

            usuarios validarUsuario = new usuarios();
            contactoSimple.registrado = validarUsuario.validar_Existencia_Usuario(txt_email.Text);
            contactoSimple.email = textTools.lineSimple(txt_email.Text);
            contactoSimple.telefono = txt_telefono.Text;
            contactoSimple.fechaSolicitud = fechaSolicitud;
            contactoSimple.comentario = txt_mensaje.Text  + infoReferencia;
            int idContactoIncomMX =  contactoSimple.contactoSimple();


            string url = string.Format("https://quicktask.it4you.mx/herramientas/seguimiento-leads/registrar.aspx?nombreCliente={0}&email={1}&telefono={2}&idContactoIncomMX={3}&comentarios={4}",
            contactoSimple.nombre, contactoSimple.email, txt_telefono.Text, idContactoIncomMX , mensajeCut);

            datosDiccRemplazo.Add("{seguimiento}", url);


            #region WebService Auto registrar leads

            try {
                dynamic lead = new ExpandoObject();
                lead.idContactoIncomMX = idContactoIncomMX;
                lead.nombreCliente = contactoSimple.nombre;

                lead.email = contactoSimple.email;
                lead.comentarios = contactoSimple.comentario;
                lead.teléfonos = contactoSimple.telefono;

                var client = new RestClient("https://quicktask.it4you.mx");
                client.Authenticator = new HttpBasicAuthenticator("rpreza", "RtBb2avgyFp");

                client.AddDefaultHeader("Content-type", "application/json");
                var request = new RestRequest("/api/leadsRegistro", Method.POST);

                request.RequestFormat = DataFormat.Json;

                request.AddJsonBody(lead);

                var response = client.Execute(request);
                if(response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    devNotificaciones.error("Error al crear lead WebApi", "");
                }
            }catch(Exception ex) {

                devNotificaciones.error("Error al crear lead WebApi", ex);
            }
            #endregion



            mensaje = archivosManejador.reemplazarEnArchivo(filePathHTML, datosDiccRemplazo);

            
            emailTienda email = new emailTienda(asunto, "serviciosweb@incom.mx", mensaje, "retail@incom.mx");
           email.contacto();

            materializeCSS.crear_toast(this, email.resultadoMensaje, email.resultado);

           formulario.Visible = false;
           gracias.Visible = true;

         
            } else {
            // Inicio de guardar registro en la base de datos
            materializeCSS.crear_toast(this, validar.mensaje, resultado);
            }
       

        }
    }
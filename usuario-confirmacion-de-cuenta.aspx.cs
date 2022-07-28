 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class usuario_confirmacion_de_cuenta : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


           

            try
            {
               
                validarURL();
            }
            catch (Exception ex)
            {

                var error = new json_respuestas(false, "Error al parsear", true, ex);
                Content_Error_Activar.Visible = true;
                msg_detalle_error.InnerText = "Hubo un error, contacta a un asesor.";
            }

        }


    }
    protected async void btn_generar_nueva_liga_Click(object sender, EventArgs e)
    {

        int LigasTotales = await UsuariosEF.ObtenerCantidadLigasCreadas(hf_usuario.Value).Result.response;


        if(LigasTotales > 3)
        {
            Content_Error_Activar.Visible = true;
            msg_detalle_error.InnerText = "Haz superado el envío de ligas, por favor envia un correo a: development@incom.mx";
        }
        else
        {
            string dominio = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            string usuario = hf_usuario.Value;
            var Clave = await  UsuariosEF.GenerarLigaConfirmacionDeCuenta(usuario);

            if (Clave.result == false && Clave.exception == true)
            {
                materializeCSS.crear_toast(this, "Ocurrio un error por favor envia un correo a: development@incom.mx", false);
                return;
            }


            var Usuario = UsuariosEF.Obtener(usuario);
            // Inicio para preparar email
            string filePath = "/email_templates/ui/usuario_ConfirmarCuenta.html";

            Dictionary<string, string> datos = new Dictionary<string, string>();
            datos.Add("{nombre}", Usuario.nombre + " " + Usuario.apellido_paterno);
            datos.Add("{dominio}", dominio);
            datos.Add("{enlaceConfirmacion}", dominio + "/usuario-confirmacion-de-cuenta.aspx?codigo=" + Clave.response);


            string mensaje = archivosManejador.reemplazarEnArchivo(filePath, datos);

            emailTienda registro = new emailTienda("[Confirma tu cuenta] Gracias por tu registro " + Usuario.nombre + " ", usuario, mensaje, null);
                 registro.general();

            // Fin preparación email 

            materializeCSS.crear_toast(this, "Usuario creado con éxito", true);

            materializeCSS.crear_toast(this, "Redireccionando en 4 segundos...", true);
            // Necesario para redirección
            string script = @"   setTimeout(function () {
            window.location.replace('" + dominio + "/usuario-aviso-confirmar-cuenta.aspx')}, 3500);";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "redirección", script, true);

            materializeCSS.crear_toast(this, "Liga enviada a tu correo enviado con éxito", true);
        }
    }
       
    
        protected async void validarURL()
    {

        // Si no se recibió un parámetro
        if (Request.QueryString["clave"] == null) {
            Content_Error_Activar.Visible = true;
            msg_detalle_error.InnerText = "No se récibió una liga";
            return;
        }

        string clave =  Request.QueryString["clave"].ToString().Replace(" ","+");

        var LigaResult = await UsuariosEF.ObtenerLiga(clave);


        // Si ocurrió un error al obtener el registro de la liga
        if (LigaResult.exception == true)
        {
            Content_Error_Activar.Visible = true;
            msg_detalle_error.InnerText = LigaResult.message;
            return;
        }

        // Si no se encontró la liga
        if (LigaResult.result == false)
        {
            Content_Error_Activar.Visible = true;
            msg_detalle_error.InnerText = "No se encontró una liga de activación";

            if (Request.QueryString["user"] != null)
            {
                string usuario = seguridad.DesEncriptar(Request.QueryString["user"].ToString());

                bool existenciaUsuario = UsuariosEF.ValidarExistenciaUsuario(usuario).result;

                if(existenciaUsuario == true)
                {
                    bool CuentaActivada = UsuariosEF.ValidarCuentaActiva(usuario).result;

                    if (CuentaActivada)
                    {
                        Content_Error_Activar.Visible = true;
                        Title_Error.InnerText = "Cuanta ya activada";
                        msg_detalle_error.InnerText = "Tu usuario ya ha sido activado";
                    }
                
                }
            }

              


                return;
        }
        usuarios_ligas_confirmaciones Liga = LigaResult.response;


        // Si la liga ya caducó
        if (utilidad_fechas.calcularDiferenciaDias(Liga.fecha_creacion) > 2)
        {
            Content_Error_Activar.Visible = true;
            msg_detalle_error.InnerText = "La liga ya expiró, reenvia un email con una nueva liga de activación";
            btn_generar_nueva_liga.Visible = true;
            hf_usuario.Value = Liga.usuario;
            return;
        }


        json_respuestas LigasTotales = await UsuariosEF.ObtenerCantidadLigasCreadas(Liga.usuario);

        if (utilidad_fechas.calcularDiferenciaDias(Liga.fecha_creacion) > 2 && LigasTotales.response > 3){
            Content_Error_Activar.Visible = true;
            msg_detalle_error.InnerText = "La liga ya expiró y haz superado el envío de ligas, por favor envia un correo a: development@incom.mx";
            return;
        }




        var resultActivacion = await UsuariosEF.ConfirmarCuenta(Liga.usuario);

        if(resultActivacion.result == true)
        {
            devNotificaciones.notificacionSimple($"El usuario {Liga.usuario} ha activado correctamente su cuenta.");

          await  UsuariosEF.BorrarLigas(Liga.usuario);
            Content_Activacion_Correcta.Visible = true;


            // Necesario para redirección
           // string script = @"setTimeout(function () { window.location.replace('" + redirectUrl + "')}, 3500);";
           // ScriptManager.RegisterStartupScript(this, typeof(Page), "redirección", script, true);


        }
        else
        {
            Content_Error_Activar.Visible = true;
            msg_detalle_error.InnerText = "Ocurrió un error al activar tu usuario, por favor envia un email a: development@incom.mx";

        }
    }
}
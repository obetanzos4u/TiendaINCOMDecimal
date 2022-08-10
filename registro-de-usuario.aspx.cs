using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class registro_de_usuario : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsPostBack)
            {
                Page.Title = "Crear cuenta";
            }
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority));
            }
        }
    }

    protected async void btn_registrar_ClickAsync(object sender, EventArgs e)
    {
        if (FNvalidarCampos())
        {
            var usuario = new usuario();

            usuario.nombre = txt_nombre.Text;
            usuario.apellido_paterno = txt_apellido_paterno.Text;
            //usuario.apellido_materno =  txt_apellido_materno.Text; 
            usuario.email = txt_email.Text;
            usuario.celular = txt_phone.Text;
            usuario.password = txt_password.Text;
            usuario.rango = 1;
            usuario.tipo_de_usuario = "cliente";
            usuario.grupo_asesores_adicional = "general";
            usuario.fecha_registro = utilidad_fechas.obtenerCentral();
            usuario.cuenta_confirmada = false;


            seguridad cifrar = new seguridad();
            usuario.password = cifrar.passwordUser(usuario.password);


            var existenciaUsuario = UsuariosEF.ValidarExistenciaUsuario(usuario.email);

            if (existenciaUsuario.exception == false && existenciaUsuario.result == true)
            {
                materializeCSS.crear_toast(this, "El usuario se encuentra registrado.", false);
                return;
            }



            // Si no hay registros procede a crear

            // Procedemos a crear el usuario
            var resultCrearUsuario = await UsuariosEF.Crear(usuario);


            if (resultCrearUsuario.result == false && resultCrearUsuario.exception == true)
            {
                materializeCSS.crear_toast(this, resultCrearUsuario.message, false);
                return;
            }

            string dominio = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);




            var resultGeneracionLiga = await UsuariosEF.GenerarLigaConfirmacionDeCuenta(usuario.email);

            if (resultGeneracionLiga.result == false && resultGeneracionLiga.exception == true)
            {
                materializeCSS.crear_toast(this, "Tú usuario fué creado con éxito pero no fué posible enviar el email de activación", false);

                devNotificaciones.notificacionSimple($"El email de activación de cuenta no fue enviado para el usuario {usuario.email} ");
                return;
            }


            usuarios_ligas_confirmaciones Liga = resultGeneracionLiga.response;
            // Inicio para preparar email
            string filePath = "/email_templates/ui/usuario_ConfirmarCuenta.html";

            Dictionary<string, string> datos = new Dictionary<string, string>();
            datos.Add("{nombre}", txt_nombre.Text + " " + txt_apellido_paterno.Text);
            datos.Add("{dominio}", dominio);
            datos.Add("{enlaceConfirmacion}", dominio + "/usuario-confirmacion-de-cuenta.aspx?clave=" + Liga.clave + "&user=" + seguridad.Encriptar(usuario.email));


            string mensaje = archivosManejador.reemplazarEnArchivo(filePath, datos);

            emailTienda registro = new emailTienda("[Confirma tu cuenta] Gracias por tu registro " + txt_nombre.Text + " ", usuario.email, mensaje, null);
            registro.general();

            // Fin preparación email 

            materializeCSS.crear_toast(this, "Usuario creado con éxito", true);

            materializeCSS.crear_toast(this, "Redireccionando en 3 segundos...", true);
            // Necesario para redirección
            string script = @"   setTimeout(function () {
            window.location.replace('" + dominio + "/usuario-aviso-confirmar-cuenta.aspx')}, 2500);";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "redirección", script, true);
        }

    }

    protected bool FNvalidarCampos()
    {
        if (chk_politica_privacidad.Checked == false)
        {
            materializeCSS.crear_toast(this, "Debe aceptar la politicas de privacidad", false);
            return false;
        }

        //bool nombres = validarCampos.nombres(txt_nombre, txt_apellido_paterno, txt_apellido_materno, this);
        bool nombres = validarCampos.nombres(txt_nombre, txt_apellido_paterno, this);
        if (nombres == false) { return false; }

        bool email = validarCampos.email(txt_email.Text, this);
        if (email == false) { return false; }

        bool phone = validarCampos.telefonos(txt_phone, this);
        if (phone == false) { return false; }

        bool password = validarCampos.passsword(txt_password, txt_password_confirma, this);
        if (password == false) { return false; }

        return true;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usuario_mi_cuenta : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            usuarios userLogin = usuarios.userLogin();
            if (userLogin.tipo_de_usuario != "usuario")
            {
                Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority) + "/usuario/mi-cuenta/mi-cuenta.aspx");
            }
        }
    }
    protected void btn_registrar_Click(object sender, EventArgs e)
    {
        if (FNvalidarCampos())
        {
            usuarios usuario = new usuarios();

            usuario.idSAP = txt_id_cliente.Text;
            usuario.nombre = txt_nombre.Text;
            usuario.apellido_paterno = txt_apellido_paterno.Text;
            usuario.apellido_materno = txt_apellido_materno.Text;
            usuario.email = txt_email.Text;
            usuario.password = txt_password.Text;
            usuario.rango = 1;
            usuario.tipo_de_usuario = "cliente";
            usuario.tipo_de_usuario = "cliente";
            usuario.registrado_por = HttpContext.Current.User.Identity.Name;
            usuario.grupo_asesores_adicional = new string[1];
            usuario.grupo_asesores_adicional[0] = "general";

            byte existenciaUsuario = usuario.validar_Existencia_Usuario(usuario.email);

            // Si no hay registros procede a crear
            if (existenciaUsuario == 0)
            {
                // Procedemos a crear el usuario
                bool resultado = usuario.crear_usuarioRegistroPorAsesor();

                if (resultado == false)
                {
                    NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Error al crear el usuario");
                    //materializeCSS.crear_toast(this, "Error al crear tu usuario", false);
                }
                else if (resultado == true)
                {

                    // Necesario para redirección
                    string script = @"   setTimeout(function () {
                    window.location.replace('" + HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/usuario/mi-cuenta/mi-cuenta.aspx')}, 1500);";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "redirección", script, true);

                    // Inicio para preparar email

                    string filePath = "/email_templates/ui/usuario_RegistroBienvenidaAsesor.html";

                    Dictionary<string, string> datos = new Dictionary<string, string>
                    {
                        { "{fecha}", utilidad_fechas.DDMMAAAA() },
                        { "{nombre}", txt_nombre.Text },
                        { "{registrado_por}", usuario.registrado_por },
                        { "{nombreAsesor}", usuarios.userLogin().nombre + " " + usuarios.userLogin().apellido_paterno },
                    };
                    string mensaje = archivosManejador.reemplazarEnArchivo(filePath, datos);

                    emailTienda registro = new emailTienda("Has sido registrado por un asesor en INCOM.MX. Bienvenido " + txt_nombre.Text + " ", textTools.lineSimple(txt_email.Text), mensaje, null);
                    registro.enviarRegistroBienvenida();

                    // Fin preparación email 

                    NotiflixJS.Message(this, NotiflixJS.MessageType.success, "Usuario creado con éxito");
                    //materializeCSS.crear_toast(this, "Usuario creado con éxito", true);
                }
            }
            else
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.info, "El correo ya ha sido registrado");
                //materializeCSS.crear_toast(this, "El email (usuario) ya existe", false);
            }
        }
    }
    protected bool FNvalidarCampos()
    {
        if (chk_politica_privacidad.Checked == false) {
            NotiflixJS.Message(this, NotiflixJS.MessageType.info, "Debes aceptar nuestras políticas de privacidad");
            //materializeCSS.crear_toast(this, "Debe aceptar la politicas de privacidad", false);
            return false;
        }

        bool nombres = validarCampos.nombres(txt_nombre, txt_apellido_paterno, txt_apellido_materno, this);
        if (nombres == false) { return false; }

        bool email = validarCampos.email(txt_email.Text, this);
        if (email == false) { return false; }

        bool password = validarCampos.passsword(txt_password, txt_password_confirma, this);
        if (password == false) { return false; }

        return true;
    }
}
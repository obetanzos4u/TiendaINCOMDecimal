using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class restablecer_password_establecer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack) {

            if (Request.QueryString["codigo"] != null && Request.QueryString["usuario"] != null) {

                string usuario = Request.QueryString["usuario"].ToString();
                string codigo_validacion = Request.QueryString["codigo"].ToString().Replace("/", "");

                


               bool validacion = usuarios.validar_restablecimiento_contraseña(usuario, codigo_validacion);
               bool vigencia = usuarios.validar_restablecimiento_contraseñaVigencia(usuario, codigo_validacion);



                if(validacion && vigencia) {
                    lbl_email_usuario.Text = seguridad.DesEncriptar(usuario);
                    btn_cambiar_password.Enabled = true;
                    txt_password.Enabled = true;
                    } else {
                    lbl_email_usuario.Text = "Ningún usuario disponible/válido";

                    txt_password.Enabled = false;
                    btn_cambiar_password.Enabled = false;
                    materializeCSS.crear_toast(this, "El código no es válido o ha caducado", false);
                    }

                } else {
                lbl_email_usuario.Text = "Ningún usuario disponible";
                txt_password.Enabled = false;
                btn_cambiar_password.Enabled = false;

                materializeCSS.crear_toast(this, "No se recibió ningún usuario", false);

                }
            }

        }
    bool validadPassword() {
        if (txt_password.Text.Length < 6 || txt_password.Text.Length > 20) { materializeCSS.crear_toast(this, "La contraseña no cumple con los requerimientos de longitud.", false); return false; };


        return true;
        }
    protected void btn_cambiar_password_Click(object sender, EventArgs e) {
        usuarios validar = new usuarios();

        if(txt_password.Text != txt_password_confirmacion.Text)
        {
          
            materializeCSS.crear_toast(this, "Las contraseñas no coinciden.", false);
            return;
        }
    if (validadPassword() && validar.validar_Existencia_Usuario(lbl_email_usuario.Text) == 1) {

        usuarios actualizar = new usuarios();
        actualizar.cambiar_password(lbl_email_usuario.Text, txt_password.Text);
        materializeCSS.crear_toast(this, "Contraseña actualizada con éxito", true);

            string usuario = Request.QueryString["usuario"].ToString();
            string codigo_validacion = Request.QueryString["codigo"].ToString().Replace("/", "");

            usuarios.desactivar_restablecimiento_contraseña(usuario, codigo_validacion);
            // Creamos la variable Session llamada
            validar.establecer_DatosUsuario(lbl_email_usuario.Text);
            FormsAuthentication.RedirectFromLoginPage(lbl_email_usuario.Text, true);

            } else {

            }
    }
}
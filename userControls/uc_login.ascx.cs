using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userControls_uc_login : System.Web.UI.UserControl
{

    public string urlReturn {
        get { return hf_urlReturn.Value; }   // get method
        set { hf_urlReturn.Value = value; }  // set method
    }

    public string idModal {
        get { return hf_idModal.Value; }   // get method
        set { hf_idModal.Value = value; }  // set method
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                up_container.Visible = false;
            }
        }
    }

    protected void btn_iniciar_sesion_Click(object sender, EventArgs e)
    {
        if (validadCampos())
        {
            string email = textTools.lineSimple(txt_email.Text);


            usuarios validar = new usuarios();
            bool resultadoLogin = validar.validar_inicio_sesión(email, txt_password.Text);

            if (resultadoLogin)
            {
                // Creamos la variable Session llamada
                validar.establecer_DatosUsuario(txt_email.Text);
            //    FormsAuthentication.RedirectFromLoginPage(email, true);
                FormsAuthentication.SetAuthCookie(email, true);
                usuarios.ultimo_login(email);
                content_sucess_login.Visible = true;
                content_login_form.Visible = false;

                materializeCSS.mostrarModal(up_container, idModal, 1);
                materializeCSS.crear_toast(up_container, "Inicio de sesión correcto", true);

                string script = @" setTimeout(function () { window.location.replace('" + urlReturn + "')}, 2500);";
                ScriptManager.RegisterStartupScript(up_container, typeof(Control), "redirección", script, true);


                up_container.Update();
            }
            else
            {
                materializeCSS.crear_toast(up_container, "Contraseña o usuario incorrecto", false);
                materializeCSS.mostrarModal(up_container, idModal, 1);
               
            }


            // Server.Transfer("~/inicio.aspx");
        }
    }

    bool validadCampos()
    {
        string email = textTools.lineSimple(txt_email.Text);


        if (email.Length < 2 || email.Length > 60) { materializeCSS.crear_toast(this, "El campo email no cumple con los requerimientos de longitud.", false); return false; };
        if (textTools.validarEmail(email) == false) { materializeCSS.crear_toast(this, "El campo email no tiene el formato correcto", false); return false; }
        if (txt_password.Text.Length < 6 || txt_password.Text.Length > 20) { materializeCSS.crear_toast(this, "La contraseña no cumple con los requerimientos de longitud.", false); return false; };


        return true;
    }
}
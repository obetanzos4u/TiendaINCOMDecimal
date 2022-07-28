using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _blank_ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
         
          
            Page.Title = "Completa los datos de contacto de tu cuenta.";
            Page.MetaDescription = "Completa los datos de contacto de tu cuenta.";
            ObtenerDatos();
        }
    }

    protected void ObtenerDatos()
    {
        var User = usuarios.userLogin();
        hf_UserEmail.Value = User.email;
        txt_telefono_fijo.Text = User.telefono;
        txt_celular.Text = User.celular;
    }
    protected async void txt_celular_TextChangedAsync(object sender, EventArgs e)
    {
        var EmailUserLogin = hf_UserEmail.Value;
        var Celular = textTools.lineSimple(txt_celular.Text);
        var result = await UsuariosDatosEF.GuardarCelular(EmailUserLogin, Celular);

        if (result.result)
        {
            lbl_text_result_saved_tel.ForeColor = System.Drawing.Color.Green;
            lbl_text_result_saved_tel.Text = result.message;
           
            var user = new usuarios();
            user.establecer_DatosUsuario(EmailUserLogin);
        }
        else
        {
            lbl_text_result_saved_tel.ForeColor = System.Drawing.Color.Red;
            lbl_text_result_saved_tel.Text = result.message;
        }

    }
    protected async void txt_telefono_fijo_TextChangedAsync(object sender, EventArgs e)
    {
        var EmailUserLogin = hf_UserEmail.Value;
        var Telefono = textTools.lineSimple(txt_telefono_fijo.Text);
        var result = await UsuariosDatosEF.GuardarTelefonoFijo(EmailUserLogin, Telefono);

        if (result.result)
        {
            lbl_text_result_saved_tel.ForeColor = System.Drawing.Color.Green;
            lbl_text_result_saved_tel.Text = result.message;
         
            var user = new usuarios();
            user.establecer_DatosUsuario(EmailUserLogin);
        }
        else
        {
            lbl_text_result_saved_tel.ForeColor = System.Drawing.Color.Red;
            lbl_text_result_saved_tel.Text = result.message;
        }
       
    }

}
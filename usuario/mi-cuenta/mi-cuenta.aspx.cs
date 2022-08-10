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
            cargarInfo();
        }





    }
    protected void cargarInfo()
    {

        usuarios datosUsuario = privacidadAsesores.modoAsesor();
        li_h1_nombre.Text = datosUsuario.nombre;
        li_nombre.Text = datosUsuario.nombre;
        li_apellidos.Text = datosUsuario.apellido_paterno + " " + datosUsuario.apellido_materno;
        li_email.Text = datosUsuario.email;
        li_telefono.Text = datosUsuario.telefono;
        li_celular.Text = datosUsuario.celular;
        li_id_cliente.Text = datosUsuario.idSAP == "" ? "No tienes un ID asignado" : datosUsuario.idSAP;

        txt_nombre.Text = datosUsuario.nombre;
        txt_apellido_paterno.Text = datosUsuario.apellido_paterno;
        txt_apellido_materno.Text = datosUsuario.apellido_materno;
        txt_id_cliente.Text = datosUsuario.idSAP;
        txt_celular.Text = datosUsuario.celular;
        txt_telefono.Text = datosUsuario.telefono;

        if (string.IsNullOrWhiteSpace(datosUsuario.idSAP)) { txt_id_cliente.Text = "   "; }

        Page.Title = "Mi cuenta - " + datosUsuario.nombre + " " + datosUsuario.apellido_paterno + " " + datosUsuario.apellido_materno;
    }


    bool validadCampos()
    {
        bool nombres = validarCampos.nombres(txt_nombre, txt_apellido_paterno, this);
        bool telefonos = validarCampos.telefonos(txt_telefono, txt_celular, this);
        if (nombres == false)
        {
            return false;
        }
        if (telefonos == false)
        {
            return false;
        }
        if (txt_id_cliente.Text.Length < 0 || txt_id_cliente.Text.Length > 15)
        {
            materializeCSS.crear_toast(this, "El id de cliente supera el límite", false); return false;
        };

        return true;
    }

    protected void btn_cambiarDatos_Click(object sender, EventArgs e)
    {

        if (validadCampos())
        {



            string id_cliente = li_id_cliente.Text;
            usuarios loginUser = usuarios.userLogin();

            // Si el usuario es un asesor, usamos lo que este en la caja de texto
            if (loginUser.tipo_de_usuario == "usuario")
            {
                id_cliente = txt_id_cliente.Text;
            }

            try
            {

                using (var db = new tiendaEntities())
                {
                    var Usuario = db.usuarios

                        .Where(s => s.email == li_email.Text)
                        .FirstOrDefault();

                    Usuario.nombre = textTools.lineSimple(txt_nombre.Text);
                    Usuario.apellido_paterno = textTools.lineSimple(txt_apellido_paterno.Text);
                    Usuario.apellido_materno = textTools.lineSimple(txt_apellido_materno.Text);
                    Usuario.telefono = textTools.lineSimple(txt_telefono.Text);
                    Usuario.celular = textTools.lineSimple(txt_celular.Text);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                materializeCSS.crear_toast(this, "Ocurrio un error al actualizar", true);
                return;
            }

            loginUser.establecer_DatosUsuario(usuarios.userLogin().email);
            loginUser.establecer_DatosCliente(li_email.Text);

            materializeCSS.crear_toast(this, "Datos actualizados con éxito", true);

            btn_cancelar_edicion_Click(sender, e);
        }

    }

    protected void btn_cambiar_password_Click(object sender, EventArgs e)
    {
        if (validadPassword())
        {

            usuarios actualizar = new usuarios();
            actualizar.cambiar_password(li_email.Text, txt_password.Text);
            materializeCSS.crear_toast(this, "Contraseña actualizada con éxito", true);
            txt_password_confirmacion.Text = "";
            datosCliente.Visible = true;
            content_password_edit.Visible = false;
        }
    }
    bool validadPassword()
    {
        if (txt_password.Text.Length < 6 || txt_password.Text.Length > 20 || txt_password_confirmacion.Text.Length < 6 || txt_password_confirmacion.Text.Length > 20)
        {
            materializeCSS.crear_toast(this, "La contraseña no cumple con los requerimientos de longitud.", false);
            return false;
        };


        if (txt_password.Text != txt_password_confirmacion.Text)
        {
            materializeCSS.crear_toast(this, "La confirmación no coincide.", false);
            return false;
        }


        return true;





    }
    protected void btn_editarDatosBasicos_Click(object sender, EventArgs e)
    {
        datosCliente.Visible = false;
        datosClienteEdit.Visible = true;

        usuarios loginUser = usuarios.userLogin();

        // Si el usuario es un asesor, usamos lo que este en la caja de texto
        if (loginUser.tipo_de_usuario == "usuario")
        {
            txt_id_cliente.Enabled = true;
        }
    }

    protected void btn_cancelar_edicion_Click(object sender, EventArgs e)
    {
        datosCliente.Visible = true;
        datosClienteEdit.Visible = false;
        content_password_edit.Visible = false;
        cargarInfo();
    }

    protected void btn_CambiarPassword_Click(object sender, EventArgs e)
    {
        datosCliente.Visible = false;
        content_password_edit.Visible = true;
    }
}
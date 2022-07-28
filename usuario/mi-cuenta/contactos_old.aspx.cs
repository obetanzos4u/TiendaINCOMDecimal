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

        if (!IsPostBack) cargarContactos();
                


    }
    protected void cargarContactos()
    {
        usuarios datosUsuario = (usuarios)Session["datosUsuario"];
        contactos obtener = new contactos();
      
        lv_contactos.DataSource = obtener.obtenerContactos(datosUsuario.id);
        lv_contactos.DataBind();
        up_lv_Contactos.Update();
    }
    protected void contacto_ItemEditing(object sender, ListViewEditEventArgs e)
    {
     
        int row = lv_contactos.EditIndex = e.NewEditIndex; // Obtiene el # del elemento a editar

        cargarContactos();

        // Al recargarse la página se pierde el estado de el acordión, con esta función volvemos a abrir dicho estado
        ScriptManager.RegisterStartupScript(this, typeof(Page), "edit", " $('.collapsible').collapsible('open', "+row+");", true);
       
    }

    protected void contacto_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        // To switch from edit mode to display mode, set EditIndex property to -1.
        int row =  e.ItemIndex;
        lv_contactos.EditIndex = -1;
        // Recargar LV
        cargarContactos();

        // Al recargarse la página se pierde el estado de el acordión, con esta función volvemos a abrir dicho estado
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cancelEdit", " $('.collapsible').collapsible('open', " + row + ");", true);
    }
    protected void contacto_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        
            ListViewItem item = this.lv_contactos.Items[e.ItemIndex];

            HiddenField hd_id_contacto = (HiddenField)item.FindControl("hd_id_contacto");
            TextBox txt_email = (TextBox)item.FindControl("txt_email");
            TextBox txt_nombre = (TextBox)item.FindControl("txt_nombre");
            TextBox txt_apellido_paterno = (TextBox)item.FindControl("txt_apellido_paterno");
            TextBox txt_apellido_materno = (TextBox)item.FindControl("txt_apellido_materno");
            TextBox txt_telefono = (TextBox)item.FindControl("txt_telefono");
            TextBox txt_celular = (TextBox)item.FindControl("txt_celular");

        // Iniciamos la validación de campos
        bool nombres = validarCampos.nombres(txt_nombre, txt_apellido_paterno, txt_apellido_paterno,this);
        bool email = validarCampos.email(txt_email.Text,  this);
        bool telefonos = validarCampos.telefonos(txt_telefono, txt_celular, this);

        // Si todo es correcto, procedemos a actualizar
        if (nombres && email && telefonos) {
            contactos actualizar = new contactos();

            actualizar.id = int.Parse(hd_id_contacto.Value);
            actualizar.email = txt_email.Text;
            actualizar.nombre = txt_nombre.Text;
            actualizar.apellido_paterno = txt_apellido_paterno.Text;
            actualizar.apellido_materno = txt_apellido_materno.Text;
            actualizar.telefono = txt_telefono.Text;
            actualizar.celular = txt_celular.Text;

           

            if (actualizar.actualizarContacto() != null){
                materializeCSS.crear_toast(this, "Contacto actualizado con éxito", true);


            } else
            {
                materializeCSS.crear_toast(this, "Error al actualizar contacto", false);
            }
                
        }

        int row = lv_contactos.EditIndex = e.ItemIndex;
        cargarContactos();
        // Al recargarse la página se pierde el estado de el acordión, con esta función volvemos a abrir dicho estado
        ScriptManager.RegisterStartupScript(this, typeof(Page), "edit", " $('.collapsible').collapsible('open', " + row + ");", true);
    }
    protected void contacto_ItemDeleted(object sender, ListViewDeleteEventArgs e)
    {
        try
        {
            ListViewItem item = this.lv_contactos.Items[e.ItemIndex];

            HiddenField hd_id_contacto = (HiddenField)item.FindControl("hd_id_contacto");

            int id = int.Parse(hd_id_contacto.Value);

            contactos eliminar = new contactos();
            if (eliminar.eliminarContacto(id))
            {

              
                materializeCSS.crear_toast(this, "Contacto eliminado con éxito", true);
            }
        }
        catch (Exception ex)
        {

            materializeCSS.crear_toast(this, "Error al eliminar contacto", false);
        }

        // Bind data to ListView Control.
        cargarContactos();

    }

    protected void btn_crearContacto_Click(object sender, EventArgs e)
    {
        // Iniciamos la validación de campos
        bool nombres = validarCampos.nombres(txt_nombre, txt_apellido_paterno, txt_apellido_paterno, this);
        bool email = validarCampos.email(txt_email.Text, this);
        bool telefonos = validarCampos.telefonos(txt_telefono, txt_celular, this);
        usuarios datosUsuario = (usuarios)Session["datosUsuario"];

        // Si todo es correcto, procedemos a actualizar
        if (nombres && email && telefonos)
        {
            contactos actualizar = new contactos {
                id_cliente = datosUsuario.id,
                email = txt_email.Text,
                nombre = txt_nombre.Text,
                apellido_paterno = txt_apellido_paterno.Text,
                apellido_materno = txt_apellido_materno.Text,
                telefono = txt_telefono.Text,
                celular = txt_celular.Text
            };



         

            if (actualizar.crearContacto() != null)
            {
                materializeCSS.crear_toast(this, "Contacto creado con éxito", true);


            }
            else
            {
                materializeCSS.crear_toast(this, "Error al crear contacto", false);
            }

        }
        cargarContactos();
       
    }
}
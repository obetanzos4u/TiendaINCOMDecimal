using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class direcciones_envio : System.Web.UI.Page
{ 
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack) {
            cargar_DireccionesEnvio();
            Page.Title = "Direcciones de envío";
                } else
        {


        }


    }
    protected void cargar_paises(DropDownList ddl) {
        paises obtener = new paises();

        ddl.DataSource = obtener.obtenerPaises();
        ddl.DataTextField = "nombre";
        ddl.DataValueField = "nombre";

        ddl.DataBind();
    }
    protected void cargar_estados(DropDownList ddl)
    {
        paises obtener = new paises();

        ddl.DataSource = obtener.obtenerEstados();
        ddl.DataTextField = "nombre";
        ddl.DataValueField = "nombre";

        ddl.DataBind();
    }

    protected void cargar_DireccionesEnvio()
    {
        usuarios datosUsuario = (usuarios)Session["datosUsuario"];
        direccionesEnvio obtener = new direccionesEnvio();
      
        lv_direcciones.DataSource = obtener.obtenerDirecciones(datosUsuario.id);
        lv_direcciones.DataBind();
        up_lv_Direcciones.Update();
    }
    protected void direccion_ItemEditing(object sender, ListViewEditEventArgs e)
    {
     
        int row = lv_direcciones.EditIndex = e.NewEditIndex; // Obtiene el # del elemento a editar

        cargar_DireccionesEnvio();
      
        // Al recargarse la página se pierde el estado de el acordión, con esta función volvemos a abrir dicho estado
        ScriptManager.RegisterStartupScript(this, typeof(Page), "edit", " $('.collapsible').collapsible('open', "+row+");", true);


        ListViewItem item = this.lv_direcciones.Items[e.NewEditIndex];

        HtmlGenericControl cont_ddl_municipio_estado = (HtmlGenericControl)item.FindControl("cont_ddl_municipio_estado");
        HtmlGenericControl cont_txt_municipio_estado = (HtmlGenericControl)item.FindControl("cont_txt_municipio_estado");


        DropDownList ddl_pais = (DropDownList)item.FindControl("ddl_pais");
        DropDownList ddl_municipio_estado = (DropDownList)item.FindControl("ddl_municipio_estado");

        cargar_paises(ddl_pais);
        cargar_estados(ddl_municipio_estado);

        Label lbl_pais = (Label)item.FindControl("lbl_pais");
        Label lbl_municipio_estado = (Label)item.FindControl("lbl_municipio_estado");

        ddl_pais.SelectedItem.Text = lbl_pais.Text;

        if (lbl_pais.Text == "México")
        {
            cont_txt_municipio_estado.Visible = false;
            cont_ddl_municipio_estado.Visible = true;
            ddl_municipio_estado.SelectedItem.Text = lbl_municipio_estado.Text;

           
        }
        else
        {
            cont_ddl_municipio_estado.Visible = false;
            cont_txt_municipio_estado.Visible = true;
        }
    }

    protected void direccion_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        // To switch from edit mode to display mode, set EditIndex property to -1.
        int row =  e.ItemIndex;
        lv_direcciones.EditIndex = -1;
        // Recargar LV
        cargar_DireccionesEnvio();
      

        // Al recargarse la página se pierde el estado de el acordión, con esta función volvemos a abrir dicho estado
        ScriptManager.RegisterStartupScript(this, typeof(Page), "cancelEdit", " $('.collapsible').collapsible('open', " + row + ");", true);
    }
    protected void direccion_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {
        
        ListViewItem item = this.lv_direcciones.Items[e.ItemIndex];

        HiddenField hd_id_direccion = (HiddenField)item.FindControl("hd_id_direccion");
        TextBox txt_nombre_direccion = (TextBox)item.FindControl("txt_nombre_direccion");
        TextBox txt_calle = (TextBox)item.FindControl("txt_calle");
        TextBox txt_numero = (TextBox)item.FindControl("txt_numero");
        TextBox txt_colonia = (TextBox)item.FindControl("txt_colonia");
        TextBox txt_delegacion = (TextBox)item.FindControl("txt_delegacion");
        TextBox txt_municipio_estado = (TextBox)item.FindControl("txt_municipio_estado");

        TextBox txt_codigo_postal = (TextBox)item.FindControl("txt_codigo_postal");
        TextBox txt_referencias = (TextBox)item.FindControl("txt_referencias");


        DropDownList ddl_pais = (DropDownList)item.FindControl("ddl_pais");
        DropDownList ddl_municipio_estado = (DropDownList)item.FindControl("ddl_municipio_estado");

       
        Label lbl_pais = (Label)item.FindControl("lbl_pais");

        
        string municipio_estado;

        
        if (ddl_pais.SelectedItem.Text == "México")
        {
            municipio_estado = ddl_municipio_estado.SelectedItem.Text;
        }
        else
        {
            municipio_estado = txt_municipio_estado.Text;
        }

        direccionesEnvio direccion = new direccionesEnvio
        {
            id = int.Parse(hd_id_direccion.Value),
            nombre_direccion = txt_nombre_direccion.Text,
            calle = txt_calle.Text,
            numero = txt_numero.Text,
            colonia = txt_colonia.Text,
            delegacion_municipio = txt_delegacion.Text,

            estado = municipio_estado,
            codigo_postal = txt_codigo_postal.Text,
            pais = ddl_pais.SelectedItem.Text,
            referencias = txt_referencias.Text
        };

        if (validarCampos.direccionEnvio(direccion, this))
        {
            usuarios datosUsuario = (usuarios)Session["datosUsuario"];

            if (direccion.actualizarDireccion() != null) {
                materializeCSS.crear_toast(this, "Dirección de envío actualizada con éxito", true);
            }
            else {
                materializeCSS.crear_toast(this, "Error al actualizar dirección de envío", false);
            }

        }




      
    }
    protected void direccion_ItemDeleted(object sender, ListViewDeleteEventArgs e)
    {
        try
        {
            ListViewItem item = this.lv_direcciones.Items[e.ItemIndex];

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
        cargar_DireccionesEnvio();
        

    }



    protected void ddl_pais_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddl_pais = (DropDownList)sender;
        

        if (ddl_pais.SelectedItem.Text == "México")
        {
            cont_txt_municipio_estado.Visible = false;
            cont_ddl_municipio_estado.Visible = true;

            DropDownList ddl_municipio_estado = (DropDownList)ddl_pais.Parent.FindControl("ddl_municipio_estado");
            cargar_estados(ddl_municipio_estado);

        }
        else
        {
            cont_ddl_municipio_estado.Visible = false;
            cont_txt_municipio_estado.Visible = true;
        }

    }
}
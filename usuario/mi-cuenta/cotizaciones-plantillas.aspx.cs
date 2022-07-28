using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class usuario_cotizaciones_plantillas : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack) cargarPlantillas();
 
    }
    
    protected void cargarPlantillas() {
      

        lv_plantillas.DataSource = cotizacionesPlantillas.recuperarPlantillas();
        lv_plantillas.DataBind();
        up_lv_plantillas.Update();
    }
    protected void plantilla_ItemEditing(object sender, ListViewEditEventArgs e) {

        int row = lv_plantillas.EditIndex = e.NewEditIndex; // Obtiene el # del elemento a editar
        cargarPlantillas();
        ListViewItem item = lv_plantillas.Items[e.NewEditIndex];
         
        // Al recargarse la página se pierde el estado de el acordión, con esta función volvemos a abrir dicho estado
        ScriptManager.RegisterStartupScript(item, typeof(Page), "edit", "M.AutoInit();   $('textarea').trigger('autoresize'); $('.collapsible').collapsible('open', " + row + ");", true);

       
    }

    protected void plantilla_ItemCanceling(object sender, ListViewCancelEventArgs e) {
        // To switch from edit mode to display mode, set EditIndex property to -1.
        int row = e.ItemIndex;

        ListViewItem item = lv_plantillas.Items[row];
        // Solo para mostrar el mensaje de materialize
        UpdatePanel UpdatePanel1 = (UpdatePanel)item.FindControl("UpdatePanel1");
        lv_plantillas.EditIndex = -1;
        // Recargar LV
        cargarPlantillas();

        // Al recargarse la página se pierde el estado de el acordión, con esta función volvemos a abrir dicho estado
        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "cancelEdit", "M.AutoInit();    $('.collapsible').collapsible('open', " + row + ");   M.textareaAutoResize($('.materialize-textarea')); ", true);
    }
    protected void plantilla_ItemUpdating(object sender, ListViewUpdateEventArgs e) {


        ListViewItem item = this.lv_plantillas.Items[e.ItemIndex];

        UpdatePanel UpdatePanel1 = (UpdatePanel)item.FindControl("UpdatePanel1");

        HiddenField hf_id_plantilla = (HiddenField)item.FindControl("hf_id_plantilla");
        TextBox txt_productosQuickAdd = (TextBox)item.FindControl("txt_productosQuickAdd");
        TextBox txt_nombre = (TextBox)item.FindControl("txt_nombre");
     

        
        cotizacionesPlantillas actualizar = new cotizacionesPlantillas();

        actualizar.id = int.Parse(hf_id_plantilla.Value);
 
        actualizar.nombre = txt_nombre.Text;
        actualizar.valor = txt_productosQuickAdd.Text;
        actualizar.fechaModificacion = utilidad_fechas.obtenerCentral();

        if (cotizacionesPlantillas.actualizarPlantilla(actualizar) != false) {
            materializeCSS.crear_toast(UpdatePanel1, "Plantilla actualizado con éxito", true);


        } else {
            materializeCSS.crear_toast(UpdatePanel1, "Error al actualizar plantilla", false);
        }

 
    }
    protected void plantilla_ItemDeleted(object sender, ListViewDeleteEventArgs e) {
        ListViewItem item = this.lv_plantillas.Items[e.ItemIndex];

        try {


            HiddenField hf_id_plantilla = (HiddenField)item.FindControl("hf_id_plantilla");

            int id = int.Parse(hf_id_plantilla.Value);

          
            if (cotizacionesPlantillas.eliminarPlantilla(id)) {


                materializeCSS.crear_toast(item, "Plantilla eliminada con éxito", true);
            }
        }
        catch (Exception ex) {

            materializeCSS.crear_toast(item, "Error al eliminar Plantilla", false);
        }

        // Bind data to ListView Control.
        cargarPlantillas();

    }

    protected void btn_crearPlantilla_Click(object sender, EventArgs e) {

       
        string nombre = txt_nombre.Text;
        string valor = txt_productosQuickAdd.Text;

        // Si todo es correcto, procedemos a actualizar
        if (!string.IsNullOrWhiteSpace(valor)) {

            cotizacionesPlantillas plantilla = new cotizacionesPlantillas {
                nombre = nombre,
                usuario_email = HttpContext.Current.User.Identity.Name,
                fechaCreacion = utilidad_fechas.obtenerCentral(),
                valor = valor   
            };

            if (cotizacionesPlantillas.guardarPlantilla(plantilla) == true) {
                materializeCSS.crear_toast(up_crearPlantilla, "Plantilla creada con éxito", true);
                cargarPlantillas();

            } else {
                materializeCSS.crear_toast(up_crearPlantilla, "Error al crear plantilla", false);
            }

        }


    }

}
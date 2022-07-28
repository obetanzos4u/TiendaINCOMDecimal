using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userControls_uc_estatus_cotizacion : System.Web.UI.UserControl {

    public string numero_operacion {
        get {
            return this.hf_numero_operacion.Value;
        }
        set {
            this.hf_numero_operacion.Value = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e) {

        if (usuarios.userLogin().tipo_de_usuario == "cliente") { up_estatus.Visible = false; }
    }
 public void ocultarEncabezado() {
        encabezado.Visible = false;
    }
    //<summary>
    // Obtiene la lista predefinida de posibles estatus
    //</summary>
     public void obtenerEstatusCotizacion() {
        ddl_estatusCotizacion.DataSource = CotizacionesEstatusController.obtenerEstatus();
        ddl_estatusCotizacion.DataValueField = "idEstatus";     
        ddl_estatusCotizacion.DataTextField = "nombreEstatus";
      

        ddl_estatusCotizacion.DataBind();

        ddl_estatusCotizacion.Items.Insert(0, new ListItem("Seleccciona", ""));

        ddl_estatusCotizacion.Items[0].Enabled = false;
    }
    public int ObtenerIDSeleccionado() {


        int idSeleccion = int.Parse( ddl_estatusCotizacion.SelectedValue);
        return idSeleccion;
    }
    public  void EstablecerIDSeleccionado(string  idSeleccion) {
    ddl_estatusCotizacion.Items.FindByValue(idSeleccion).Selected = true;
    }
    protected void ddl_estatusCotizacion_SelectedIndexChanged(object sender, EventArgs e) {

        int idSeleccion = int.Parse(ddl_estatusCotizacion.SelectedValue);

       bool resultado = cotizaciones.actualizarEstatusCotizacion(hf_numero_operacion.Value, idSeleccion);


        if (resultado) {
            materializeCSS.crear_toast(up_estatus, "Estatus actualizado con éxito", true);
        } else {
            materializeCSS.crear_toast(up_estatus, "Error al actualizar estatus", false);

        }

    }
}
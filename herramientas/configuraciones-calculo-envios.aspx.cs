using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class herramientas_configuraciones_calculo_envios : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack) {

            mostrarEstatusApi();
        }
    }
   protected void mostrarEstatusApi()
    {
        bool estatus = operacionesConfiguraciones.obtenerEstatusApiFlete();
        string str_estatus = estatus == true ? "Activado" : "Desactivado";

        lbl_estatus.ForeColor = estatus == true ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        lbl_estatus.Text = str_estatus;
        chk_Estatus_ApiFlete.Checked = estatus;
    }
    protected void chk_Estatus_ApiFlete_CheckedChanged(object sender, EventArgs e)
    {

       bool result = operacionesConfiguraciones.guardarEstatusApiFlete(chk_Estatus_ApiFlete.Checked);
        if (result) {
       
            materializeCSS.crear_toast(this, "Estatus actualizado con éxito", true); 
        }
        else { 
            materializeCSS.crear_toast(this, "Error al actualizar estatus", false); }

        mostrarEstatusApi();
    }
        
     
}
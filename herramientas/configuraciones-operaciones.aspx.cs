using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class herramientas_configuraciones_operaciones : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack) {
            Title = "Actualizar tipo de cambio";
            actualizacionAutomaticaTipoCambio();
        }
    }
    protected void actualizacionAutomaticaTipoCambio() {

      //  string usuario = Request.QueryString["usuario"].ToString();
    }
        protected void obtenerTypoDeCambio() {
        decimal tipoDeCambio = operacionesConfiguraciones.obtenerTipoDeCambio();
        txt_tipoDeCambio.Text = tipoDeCambio.ToString();
        }

    protected void btn_guardar_Click(object sender, EventArgs e) {

        }

    protected void btn_guardarTipodeCambio_Click(object sender, EventArgs e) {

        var permiso = privacidadPaginas.validarPermisoSeccion("establecer_tipo_de_cambio", usuarios.userLogin().id);

        if (permiso.result == false)
        {
            materializeCSS.crear_toast(this, permiso.message, permiso.result);
            return;
        }

        decimal tipoDeCambio =  Math.Round(textTools.soloNumerosD(txt_tipoDeCambio.Text),5);
        bool resultado = operacionesConfiguraciones.guardarTipoDeCambio(tipoDeCambio);
        if(resultado != false) {
            materializeCSS.crear_toast(this, "Tipo de cambio guardado con éxtio", true);
            } else {
            materializeCSS.crear_toast(this, "Error al guardar el tipo de cambio", false);

            }

        }

    protected void btn_obtenerAutomáticamente_Click(object sender, EventArgs e) {

        decimal? tipoDeCambioHoy = tipoDeCambio.obtenerTipoDeCambio();

        if(tipoDeCambioHoy != null) {
            txt_tipoDeCambio.Text = tipoDeCambioHoy.ToString();
            materializeCSS.crear_toast(this, "Tipo de cambio obtenido exitosamente", true);
        } else {
            materializeCSS.crear_toast(this, "Error al obtener tipo de cambio", false);
        }
     
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class herramientas_configuraciones_operaciones : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Title = "Tipo de cambio";
        if (!IsPostBack)
        {
            actualizacionAutomaticaTipoCambio();
        }
    }
    protected void actualizacionAutomaticaTipoCambio()
    {
        //  string usuario = Request.QueryString["usuario"].ToString();
    }
    protected void obtenerTipoDeCambio()
    {
        decimal tipoDeCambio = operacionesConfiguraciones.obtenerTipoDeCambio();
        txt_tipoDeCambio.Text = tipoDeCambio.ToString();
    }
    protected void btn_guardar_Click(object sender, EventArgs e)
    {

    }

    protected void btn_guardarTipodeCambio_Click(object sender, EventArgs e)
    {

        var permiso = privacidadPaginas.validarPermisoSeccion("establecer_tipo_de_cambio", usuarios.userLogin().id);

        if (permiso.result == false)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.warning, permiso.message);
            //materializeCSS.crear_toast(this, permiso.message, permiso.result);
            return;
        }

        decimal? tipoDeCambio = Math.Round(textTools.soloNumerosD(txt_tipoDeCambio.Text), 5);
        if (tipoDeCambio > 0 && tipoDeCambio != null)
        {
            bool resultado = operacionesConfiguraciones.guardarTipoDeCambio((decimal)tipoDeCambio);
            if (resultado != false)
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.success, "Tipo de cambio actualizado");
                //materializeCSS.crear_toast(this, "Tipo de cambio guardado con éxtio", true);
            }
            else
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Error al establecer el tipo de cambio ");
                //materializeCSS.crear_toast(this, "Error al guardar el tipo de cambio", false);
            }
        }
        else
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.warning, "No se puede establecer un tipo de cambio vacío o en 0");
        }
    }
    protected void btn_obtenerAutomáticamente_Click(object sender, EventArgs e)
    {
        decimal? tipoDeCambioHoy = tipoDeCambio.obtenerTipoDeCambio();

        if (tipoDeCambioHoy != null && tipoDeCambioHoy > 0)
        {
            txt_tipoDeCambio.Text = tipoDeCambioHoy.ToString();
            NotiflixJS.Message(this, NotiflixJS.MessageType.success, "Tipo de cambio obtenido");
            //materializeCSS.crear_toast(this, "Tipo de cambio obtenido exitosamente", true);
            btn_guardarTipodeCambio.Enabled = true;
            btn_guardarTipodeCambio.CssClass = "is-btn-blue";
        }
        else if (tipoDeCambioHoy <= 0)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.warning, "El tipo de cambio obtenido es 0, establecerlo manualmente");
            txt_tipoDeCambio.Enabled = true;
            btn_guardarTipodeCambio.Enabled = true;
            btn_guardarTipodeCambio.CssClass = "is-btn-blue";
        }
        else
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Error al obtener el tipo de cambio");
            //materializeCSS.crear_toast(this, "Error al obtener tipo de cambio", false);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class uc_crearDireccionesFacturacion : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            if (ddl_pais.SelectedText == "México")
            {
                cont_ddl_estado.Visible = true;
                cont_txt_estado.Visible = false;
            }
            else
            {
                cont_ddl_estado.Visible = false;
                cont_txt_estado.Visible = true;
            }
        }
    }
    protected void btn_crear_direccion_Click(object sender, EventArgs e)
    {
        string estado;
        string regimen_fiscal = ddl_regimen_fiscal.SelectedValue;

        if (ddl_pais.SelectedText == "México")
        {
            estado = ddl_estado.SelectedText;
        }
        else
        {
            estado = txt_estado.Text;
        }
        direccionesFacturacion direccion = new direccionesFacturacion
        {
            nombre_direccion = txt_nombre_direccion.Text,
            razon_social = txt_razon_social.Text,
            rfc = txt_rfc.Text,
            calle = txt_calle.Text,
            numero = txt_numero.Text,
            colonia = txt_colonia.Text,
            delegacion_municipio = txt_delegacion_municipio.Text,
            estado = estado,
            codigo_postal = txt_codigo_postal.Text,
            pais = ddl_pais.SelectedText,
            regimen_fiscal = regimen_fiscal
        };

        if (validarCampos.direccionFacturacion(direccion, this))
        {
            usuarios datosUsuario = privacidadAsesores.modoAsesor();

            if (direccion.crearDireccion(datosUsuario.id) != null)
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.success, "Dirección de facturación creada");
                BootstrapCSS.RedirectJs(this, Request.Url.GetLeftPart(UriPartial.Authority) + "/usuario/mi-cuenta/direcciones-de-facturacion.aspx", 2000);
                //materializeCSS.crear_toast(this, "Dirección de facturación creada con éxito", true);
            }
            else
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Error al crear dirección de facturación");
                //materializeCSS.crear_toast(this, "Error al crear dirección de facturación", false);
            }
        }
    }
}
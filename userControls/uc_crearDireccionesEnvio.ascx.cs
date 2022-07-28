using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class uc_crearDireccionesEnvio : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack) {
            if (ddl_pais.SelectedText == "México") {
                cont_ddl_estado.Visible = true;
                cont_txt_estado.Visible = false;
                } else {
                cont_ddl_estado.Visible = false;
                cont_txt_estado.Visible = true;
                }

            }

        }



    protected void btn_crear_direccion_Click(object sender, EventArgs e) {

        string estado;

        if (ddl_pais.SelectedText == "México") {
            estado = ddl_estado.SelectedText;
            } else {
            estado = txt_estado.Text;
            }

        string colonia = "";
        if (ddl_colonia.Visible)
        {
            colonia = ddl_colonia.SelectedValue;
        }
        else
        {
            colonia = txt_colonia.Text;
        }


        direccionesEnvio direccion = new direccionesEnvio {
            nombre_direccion = txt_nombre_direccion.Text,
            calle = txt_calle.Text,
            numero = txt_numero.Text,
            colonia = colonia,
            delegacion_municipio = txt_delegacion_municipio.Text,
            ciudad = txt_ciudad.Text,
            estado =estado,
            codigo_postal = txt_codigo_postal.Text,
            pais = ddl_pais.SelectedText,
          referencias = txt_referencias.Text
            };

        if (validarCampos.direccionEnvio(direccion, this)) {
            usuarios datosUsuario = usuarios.modoAsesor() ;

            if (direccion.crearDireccion(datosUsuario.id) != null) {

                direcciones_envio_EF.EstablecerPredeterminada(datosUsuario.id);
                materializeCSS.crear_toast(this, "Dirección de envío creada con éxito", true);
                } else {
                materializeCSS.crear_toast(this, "Error al crear dirección de envío", false);
                }

            }


        }
    protected void EnabledCampos(bool status)
    {
        txt_colonia.Enabled = status;
        txt_delegacion_municipio.Enabled = status;
        txt_ciudad.Enabled = status;
        txt_estado.Enabled = status;
        ddl_pais.Enabled = status;
        ddl_estado.Enabled = status;
    }
    protected async void txt_codigo_postal_TextChanged(object sender, EventArgs e)
    {
        string cp = textTools.lineSimple(txt_codigo_postal.Text);
        if (!string.IsNullOrWhiteSpace(cp))
        {
            json_respuestas result = await DireccionesServiceCP.GetCodigoPostalAsync(cp);


            if(result.result == true)
            {
                dynamic Result = JsonConvert.DeserializeObject<dynamic>(result.response);

                txt_delegacion_municipio.Text = Result[0].Municipio;
                txt_ciudad.Text = Result[0].Ciudad;

                ddl_estado.SelectedText = Result[0].Estado;
                ddl_pais.SelectedText = Result[0].Pais;

                ddl_colonia.Items.Clear();
                foreach (var t in Result)
                {

                    string Asentamiento = t.Asentamiento;
                    ddl_colonia.Items.Add(new ListItem(Asentamiento, Asentamiento));
                }
                ddl_colonia.DataBind();




                cont_ddl_estado.Visible = true;
                cont_txt_estado.Visible = false;

                EnabledCampos(false);

                ddl_colonia.Visible = true;
                txt_colonia.Visible = false;
            }
            else
            {
                ddl_colonia.Items.Clear();
                ddl_colonia.Visible = false;
                txt_colonia.Visible = true;

                EnabledCampos(true);

                materializeCSS.crear_toast(this, result.message, false);
            }

        }
        else
        {
            ddl_colonia.Items.Clear();
            ddl_colonia.Visible = false;
            txt_colonia.Visible = true;
            EnabledCampos(true);
        }   

    }
       
}

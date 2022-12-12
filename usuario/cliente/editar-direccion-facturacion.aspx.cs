using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usuario_cliente_editar_direccion_facturacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Page.RouteData.Values["id_direccion"] != null)
            {
                obtenerDireccion();
            }
            else
            {
                Response.Redirect(HttpContext.Current.Request.Url.Authority, true);
            }
        }
        else

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

    protected async void obtenerDireccion()
    {
        int idDirección = int.Parse(Page.RouteData.Values["id_direccion"].ToString());
        direcciones_facturacion direccion = direcciones_facturacion_EF.Obtener(idDirección);

        txt_nombre_direccion.Text = direccion.nombre_direccion;
        txt_rfc.Text = direccion.rfc;
        txt_razon_social.Text = direccion.razon_social;
        txt_calle.Text = direccion.calle;
        txt_numero.Text = direccion.numero;
        txt_colonia.Text = direccion.colonia;
        txt_delegacion_municipio.Text = direccion.delegacion_municipio;
        txt_codigo_postal.Text = direccion.codigo_postal;
        ddl_regimen_fiscal.SelectedValue = direccion.regimen_fiscal;

        if (direccion.pais == "México")
        {
            ddl_pais.SelectedText = direccion.pais;
            cont_txt_estado.Visible = false;
            cont_ddl_estado.Visible = true;
            ddl_estado.SelectedText = direccion.estado;
        }
        else
        {
            cont_ddl_estado.Visible = false;
            cont_txt_estado.Visible = true;
        }

        // Sección llenado Código Postal
        string cp = textTools.lineSimple(txt_codigo_postal.Text);
        if (!string.IsNullOrWhiteSpace(cp))
        {
            json_respuestas result = await DireccionesServiceCP.GetCodigoPostalAsync(cp);


            if (result.result == true)
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


                ddl_colonia.SelectedValue = txt_colonia.Text;

                cont_ddl_estado.Visible = true;
                cont_txt_estado.Visible = false;

                EnabledCampos(false);
            }
            else
            {
                EnabledCampos(true);


            }

        }
    }
    protected void btn_editar_direccion_Click(object sender, EventArgs e)
    {

        int idDireccion = int.Parse(Page.RouteData.Values["id_direccion"].ToString());
        string estado;
        string colonia = "";
        string regimen_fiscal = ddl_regimen_fiscal.SelectedValue;
        if (ddl_pais.SelectedText == "México") estado = ddl_estado.SelectedText;
        else estado = txt_estado.Text;

        if (ddl_colonia.Visible) colonia = ddl_colonia.SelectedValue;
        else colonia = txt_colonia.Text;

        var direccionModel = new ModelDireccionFacturacionValidador
        {
            id = idDireccion,
            nombre_direccion = txt_nombre_direccion.Text,
            rfc = txt_rfc.Text,
            razon_social = txt_razon_social.Text,
            calle = txt_calle.Text,
            numero = txt_numero.Text,
            colonia = colonia,
            delegacion_municipio = txt_delegacion_municipio.Text,
            ciudad = txt_ciudad.Text,
            estado = estado,
            codigo_postal = txt_codigo_postal.Text,
            pais = ddl_pais.SelectedText,
            regimenFiscal = regimen_fiscal,
        };


        ValidationContext context = new ValidationContext(direccionModel, null, null);
        List<ValidationResult> validationResults = new List<ValidationResult>();
        bool valid = Validator.TryValidateObject(direccionModel, context, validationResults, true);

        if (!valid)
        {
            string resultmsg = string.Empty;
            validationResults.ForEach(n => resultmsg += $"- {n.ErrorMessage} <br>");
            BootstrapCSS.Message(this, "#content_alert", BootstrapCSS.MessageType.danger, "Error al crear dirección", resultmsg);
            return;
        }

        var mapper = new AutoMapper.Mapper(Mapeos.getDireccionFacturacion);
        direcciones_facturacion direccion = mapper.Map<direcciones_facturacion>(direccionModel);
        direccion.id_cliente = usuarios.modoAsesor().id;


        var guardar = direcciones_facturacion_EF.GuardarDireccion(direccion);

        if (guardar.result)
        {
            BootstrapCSS.Message(this, "#content_alert", BootstrapCSS.MessageType.success, "Realizado con éxito", "Dirección de facturación actualizada con éxito");
            ValidarReferenciaPedido(direccion);


        }
        else
        {
            BootstrapCSS.Message(this, "#content_alert", BootstrapCSS.MessageType.danger, "Error al actualizar dirección", guardar.message);

        }

    }

    protected void ValidarReferenciaPedido(direcciones_facturacion direccion)
    {

        if (Request.QueryString["ref"] != null && Request.QueryString["numero_operacion"] != null)
        {
            string idPedido = Request.QueryString["ref"].ToString();
            string numero_operacion = Request.QueryString["numero_operacion"].ToString();



            var pedidoDireccionFacturacion = new pedidos_direccionFacturacion();

            pedidoDireccionFacturacion.numero_operacion = numero_operacion;
            pedidoDireccionFacturacion.idDireccionFacturacion = direccion.id;
            pedidoDireccionFacturacion.calle = direccion.calle;
            pedidoDireccionFacturacion.numero = direccion.numero;
            pedidoDireccionFacturacion.colonia = direccion.colonia;
            pedidoDireccionFacturacion.delegacion_municipio = direccion.delegacion_municipio;
            pedidoDireccionFacturacion.codigo_postal = direccion.codigo_postal;
            pedidoDireccionFacturacion.ciudad = direccion.ciudad;
            pedidoDireccionFacturacion.estado = direccion.estado;
            pedidoDireccionFacturacion.pais = direccion.pais;
            pedidoDireccionFacturacion.razon_social = direccion.razon_social;
            pedidoDireccionFacturacion.rfc = direccion.rfc;
            pedidoDireccionFacturacion.RegimenFiscal = direccion.regimen_fiscal;

            var result = PedidosEF.GuardarDireccionFacturacion(numero_operacion, pedidoDireccionFacturacion);


            string redirectUrl = GetRouteUrl("cliente-pedido-resumen", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",idPedido }
                    });


            BootstrapCSS.RedirectJs(this, redirectUrl, 2000);
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
        if (!status)
        {
            txt_delegacion_municipio.CssClass += "form-select";
        }
    }
    protected async void txt_codigo_postal_TextChanged(object sender, EventArgs e)
    {

        string cp = textTools.lineSimple(txt_codigo_postal.Text);
        if (!string.IsNullOrWhiteSpace(cp))
        {
            json_respuestas result = await DireccionesServiceCP.GetCodigoPostalAsync(cp);


            if (result.result == true)
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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class usuario_cliente_pedido_facturacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Title = "Facturación";
            if (Page.RouteData.Values["id_operacion"].ToString() != null)
            {
                CargarDatosPedido();
                CargarDireccionFacturacion();
                cargarDirecciones();
                EstablecerNavegacion();
                EnabledCampos(false);
            }
        }
    }
    protected void EstablecerNavegacion()
    {

        /*  link_envio.NavigateUrl = GetRouteUrl("cliente-pedido-envio", new System.Web.Routing.RouteValueDictionary {
                          { "id_operacion",seguridad.Encriptar(hf_id_pedido.Value) }
                      });

     */
    }
    protected void CargarDatosPedido()
    {
        string route_id_operacion = Page.RouteData.Values["id_operacion"].ToString();
        route_id_operacion = seguridad.DesEncriptar(route_id_operacion);

        int idPedido = int.Parse(route_id_operacion);

        var pedidos_datos = PedidosEF.ObtenerDatos(idPedido);


        lt_numero_pedido.Text = pedidos_datos.numero_operacion;

        hf_id_pedido.Value = route_id_operacion;


    }

    protected void CargarDireccionFacturacion()
    {


        int idDireccion = int.Parse(hf_id_pedido.Value);

        var result = PedidosEF.ObtenerDireccionFacturacion(lt_numero_pedido.Text);

        if (result.result)
        {

            pedidos_direccionFacturacion direccionFacturacion = result.response;

            if (direccionFacturacion != null)
            {
                hf_id_pedido_direccion_facturacion.Value = direccionFacturacion.idDireccionFacturacion.ToString();
            }



        }
        else
        {
            BootstrapCSS.Message(this, "#", BootstrapCSS.MessageType.danger, "Error", result.message);

        }
    }
    protected void cargarDirecciones()
    {
        usuarios user = usuarios.modoAsesor();
        lv_direcciones.DataSource = direcciones_facturacion_EF.ObtenerTodas(user.id);
        lv_direcciones.DataBind();
    }
    //text-white bg-success
    protected void btn_crear_direccion_Click(object sender, EventArgs e)
    {
        string estado;
        string colonia = "";
        string regimenFiscal = ddl_regimen_fiscal.SelectedValue;

        if (ddl_pais.SelectedText == "México")
        {
            estado = ddl_estado.SelectedText;
        }
        else
        {
            estado = txt_estado.Text;
        }

        if (ddl_colonia.Visible)
        {
            colonia = ddl_colonia.SelectedValue;
        }
        else
        {
            colonia = txt_colonia.Text;
        }

        var direccionModel = new ModelDireccionFacturacionValidador
        {
            nombre_direccion = txt_nombre_direccion.Text,
            rfc = txt_rfc.Text,
            razon_social = txt_razon_social.Text,
            regimenFiscal = regimenFiscal,
            calle = txt_calle.Text,
            numero = txt_numero.Text,
            colonia = txt_colonia.Text,
            delegacion_municipio = txt_delegacion_municipio.Text,
            ciudad = txt_ciudad.Text,
            estado = estado,
            codigo_postal = txt_codigo_postal.Text,
            pais = ddl_pais.SelectedText,
        };

        ValidationContext context = new ValidationContext(direccionModel, null, null);
        List<ValidationResult> validationResults = new List<ValidationResult>();
        bool valid = Validator.TryValidateObject(direccionModel, context, validationResults, true);

        if (!valid)
        {
            string resultmsg = string.Empty;
            validationResults.ForEach(n => resultmsg += $"- {n.ErrorMessage} <br>");
            NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Error al agregar los datos de facturación.");
            //BootstrapCSS.Message(this, "#content_alert", BootstrapCSS.MessageType.danger, "Error al crear dirección", resultmsg);
            return;
        }
        var mapper = new AutoMapper.Mapper(Mapeos.getDireccionFacturacion);
        direcciones_facturacion direccion = mapper.Map<direcciones_facturacion>(direccionModel);
        direccion.id_cliente = usuarios.modoAsesor().id;
        var guardar = direcciones_facturacion_EF.GuardarDireccion(direccion);

        if (guardar.result)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.success, "Datos agregados");
            //BootstrapCSS.Message(this, "#content_alert", BootstrapCSS.MessageType.success, "Creada con éxito", guardar.message);
            cargarDirecciones();
            up_datos_facturacion.Update();
        }
        else
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Error al agregar los datos de facturación");
            //BootstrapCSS.Message(this, "#content_alert", BootstrapCSS.MessageType.danger, "Error al crear dirección", guardar.message);
        }
    }
    protected void btn_usarDirección_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        // Obtenemos el contenedor del objeto que creo el evento
        ListViewItem lvItem = (ListViewItem)btn.NamingContainer;
        HiddenField hf_id_direccion = (HiddenField)lvItem.FindControl("hf_id_direccion");
        int idDireccion = int.Parse(hf_id_direccion.Value);
        string numero_operacion = lt_numero_pedido.Text;
        direcciones_facturacion direccion = direcciones_facturacion_EF.Obtener(idDireccion);
        var pedidoDireccionFact = new pedidos_direccionFacturacion();

        pedidoDireccionFact.numero_operacion = numero_operacion;
        pedidoDireccionFact.idDireccionFacturacion = direccion.id;
        pedidoDireccionFact.calle = direccion.calle;
        pedidoDireccionFact.numero = direccion.numero;
        pedidoDireccionFact.colonia = direccion.colonia;
        pedidoDireccionFact.delegacion_municipio = direccion.delegacion_municipio;
        pedidoDireccionFact.codigo_postal = direccion.codigo_postal;
        pedidoDireccionFact.ciudad = direccion.ciudad;
        pedidoDireccionFact.estado = direccion.estado;
        pedidoDireccionFact.pais = direccion.pais;
        pedidoDireccionFact.razon_social = direccion.razon_social;
        pedidoDireccionFact.rfc = direccion.rfc;

        PedidosEF.ActualizarFacturacion(lt_numero_pedido.Text, true);
        var result = PedidosEF.GuardarDireccionFacturacion(lt_numero_pedido.Text, pedidoDireccionFact);

        if (result.result)
        {
            string redirectUrl = GetRouteUrl("cliente-pedido-resumen", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",seguridad.Encriptar(hf_id_pedido.Value) }
                    });
            NotiflixJS.Message(this, NotiflixJS.MessageType.success, "Datos de facturación guardados");
            BootstrapCSS.RedirectJs(this, redirectUrl, 1500);
        }
        else
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "No fue posible establecer los datos de facturación");
        }
        //msg_succes.Text = "Estableciendo <strong>Dirección de facturación</strong>, redireccionando en 3,2,1....";
        //msg_succes.Visible = true;


        //    Response.Redirect(redirect);
        CargarDatosPedido();
        CargarDireccionFacturacion();
        cargarDirecciones();
    }
    protected void lv_direcciones_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        string id_pedido_direccion_facturacion = hf_id_pedido_direccion_facturacion.Value;
        if (!string.IsNullOrWhiteSpace(id_pedido_direccion_facturacion))
        {
            HiddenField hf_id_direccion = (HiddenField)e.Item.FindControl("hf_id_direccion");
            if (hf_id_direccion.Value == id_pedido_direccion_facturacion)
            {
                HtmlGenericControl contentCard_DireccFact = (HtmlGenericControl)e.Item.FindControl("contentCard_DireccFact");
                contentCard_DireccFact.Attributes["class"] += "bg-success-1";
            }
        }
    }
    protected void btn_Sin_Factura_Click(object sender, EventArgs e)
    {
        string numero_operacion = lt_numero_pedido.Text;
        string id_operacion = hf_id_pedido.Value;

        PedidosEF.ActualizarFacturacion(lt_numero_pedido.Text, false);
        PedidosEF.EliminarDireccionDeFacturacion(lt_numero_pedido.Text);

        string redirectUrl = GetRouteUrl("cliente-pedido-resumen", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion", seguridad.Encriptar(id_operacion )}
                    });
        NotiflixJS.Message(this, NotiflixJS.MessageType.success, "Pedido sin factura");
        //msg_succes.Text = "Estableciendo, <strong>redireccionando en 3,2,1.... </strong>";
        //msg_succes.Visible = true;

        BootstrapCSS.RedirectJs(this, redirectUrl, 1500);

        CargarDatosPedido();
        CargarDireccionFacturacion();
        cargarDirecciones();
    }

    protected async void txt_codigo_postal_TextChanged(object sender, EventArgs e)
    {
        NotiflixJS.Loading(this, NotiflixJS.LoadingType.loading);
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

                EnabledCampos(false, result.result);

                ddl_colonia.Visible = true;
                txt_colonia.Visible = false;
                NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
            }
            else
            {
                ddl_colonia.Items.Clear();
                ddl_colonia.Visible = false;
                txt_colonia.Visible = true;

                EnabledCampos(true);
                NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
                NotiflixJS.Message(this, NotiflixJS.MessageType.failure, result.message);
                //materializeCSS.crear_toast(this, result.message, false);
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
    protected void EnabledCampos(bool status)
    {
        txt_calle.Enabled = status;
        txt_numero.Enabled = status;
        txt_colonia.Enabled = status;
        txt_delegacion_municipio.Enabled = status;
        txt_ciudad.Enabled = status;
        txt_estado.Enabled = status;
        ddl_pais.Enabled = status;
        ddl_estado.Enabled = status;
    }
    protected void EnabledCampos(bool status, bool response)
    {
        txt_calle.Enabled = response;
        txt_numero.Enabled = response;
        txt_colonia.Enabled = status;
        txt_delegacion_municipio.Enabled = status;
        txt_ciudad.Enabled = status;
        txt_estado.Enabled = status;
        ddl_pais.Enabled = status;
        ddl_estado.Enabled = status;
    }
    protected void btn_eliminarDireccion_Click(object sender, EventArgs e)
    {
        LinkButton btnEliminar = (LinkButton)sender;
        // Obtenemos el contenedor del objeto que creo el evento
        ListViewItem lvItem = (ListViewItem)btnEliminar.NamingContainer;
        HiddenField hf_id_direccion = (HiddenField)lvItem.FindControl("hf_id_direccion");

        int IdDireccion = int.Parse(hf_id_direccion.Value);
        var result = direcciones_facturacion_EF.eliminar(IdDireccion);

        if (result.result)
        {
            cargarDirecciones();
        }
    }
}
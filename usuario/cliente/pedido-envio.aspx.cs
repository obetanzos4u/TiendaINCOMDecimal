using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class usuario_cliente_basic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Title = "Método de envío";
            if (Page.RouteData.Values["id_operacion"].ToString() != null)
            {
                CargarDatosPedido();
            }
            CargarDireccionEnvio();
            cargarDirecciones();
            EstablecerNavegacion();
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
        var pedido_montos = PedidosEF.ObtenerNumeros(pedidos_datos.numero_operacion);

        #region Validación de permiso de privacidad
        if (usuarios.userLogin().tipo_de_usuario == "cliente" && usuarios.userLogin().email != pedidos_datos.usuario_cliente)
        {
            Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority));
        }
        #endregion



        lt_numero_pedido.Text = pedidos_datos.numero_operacion;

        hf_id_pedido.Value = route_id_operacion;
        hf_pedido_tipo_envio.Value = pedido_montos.metodoEnvio;
        if (pedido_montos.metodoEnvio == "En Tienda")
        {
            string script = " document.getElementById('card_envio_recoge_en_tienda').classList.add('bg-success-1');";
            ScriptManager.RegisterStartupScript(this, typeof(Control), "CSS_add", script, true);

        }
        if (!string.IsNullOrWhiteSpace(pedido_montos.EnvioNota))
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.info, pedido_montos.EnvioNota);
            //msg_alert.Text = pedido_montos.EnvioNota;
            //msg_alert.Visible = true;
        }
        else
        {
            //NotiflixJS.Message(this, NotiflixJS.MessageType.warning, "Persistente");
            //msg_alert.Text = pedido_montos.EnvioNota;
            //msg_alert.Visible = false;
        }
    }
    protected void CargarDireccionEnvio()
    {
        var result = PedidosEF.ObtenerDireccionEnvio(lt_numero_pedido.Text);

        if (result.result)
        {

            pedidos_direccionEnvio direccionEnvio = result.response;

            if (direccionEnvio != null)
            {
                hf_id_pedido_direccion_envio.Value = direccionEnvio.idDireccionEnvio.ToString();
            }
        }
        else
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.failure, result.message);
            //BootstrapCSS.Message(this, "#", BootstrapCSS.MessageType.danger, "Error", result.message);
        }
    }
    protected void cargarDirecciones()
    {
        usuarios user = usuarios.modoAsesor();
        lv_direcciones.DataSource = direcciones_envio_EF.ObtenerTodas(user.id);
        lv_direcciones.DataBind();
    }
    //text-white bg-success
    protected void btn_entregaDomicilioNuevo_Click(object sender, EventArgs e)
    {
        ContentReferenciaDomicilioNuevo.Visible = true;
    }
    protected void btn_crear_direccion_Click(object sender, EventArgs e)
    {
        string estado;
        string colonia = "";

        if (ddl_pais.SelectedText == "México") estado = ddl_estado.SelectedText;
        else estado = txt_estado.Text;

        if (ddl_colonia.Visible) colonia = ddl_colonia.SelectedValue;
        else colonia = txt_colonia.Text;

        direccionesEnvio direccion = new direccionesEnvio
        {
            nombre_direccion = txt_nombre_direccion.Text,
            calle = txt_calle.Text,
            numero = txt_numero.Text,
            colonia = colonia,
            delegacion_municipio = txt_delegacion_municipio.Text,
            ciudad = txt_ciudad.Text,
            estado = estado,
            codigo_postal = txt_codigo_postal.Text,
            pais = ddl_pais.SelectedText,
            referencias = txt_referencias.Text
        };

        json_respuestas resultValidacion = validarCampos.direccionDeEnvio(direccion);

        if (resultValidacion.result)
        {
            usuarios datosUsuario = usuarios.modoAsesor();
            if (direccion.crearDireccion(datosUsuario.id) != null)
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.success, "Dirección agregada");
                //BootstrapCSS.Message(this, "#content_alert", BootstrapCSS.MessageType.success, "Creada con éxito", "");
                ContentReferenciaDomicilioNuevo.Visible = false;
                ContentReferenciaDomiciliosGuardados.Visible = true;
                direcciones_envio_EF.EstablecerPredeterminada(datosUsuario.id);
                cargarDirecciones();
            }
            else
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Error al agregar la dirección");
                //BootstrapCSS.Message(this, "#content_alert", BootstrapCSS.MessageType.danger, "Error al crear dirección de envío", "");
            }
        }
        else
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Error al agregar la dirección");
            //BootstrapCSS.Message(this, "#content_alert", BootstrapCSS.MessageType.danger, "Error al crear dirección de envío", resultValidacion.message);
        }
    }

    protected void btn_usarDirección_Click(object sender, EventArgs e)
    {
        LinkButton btnEliminar = (LinkButton)sender;
        // Obtenemos el contenedor del objeto que creo el evento
        ListViewItem lvItem = (ListViewItem)btnEliminar.NamingContainer;

        HiddenField hf_id_direccion = (HiddenField)lvItem.FindControl("hf_id_direccion");

        int idDireccion = int.Parse(hf_id_direccion.Value);
        string numero_operacion = lt_numero_pedido.Text;

        direcciones_envio direccionEnvio = direcciones_envio_EF.Obtener(idDireccion);
        pedidos_direccionEnvio pedidoDireccionEnvio = new pedidos_direccionEnvio();

        pedidoDireccionEnvio.numero_operacion = numero_operacion;
        pedidoDireccionEnvio.idDireccionEnvio = direccionEnvio.id;
        pedidoDireccionEnvio.calle = direccionEnvio.calle;
        pedidoDireccionEnvio.numero = direccionEnvio.numero;
        pedidoDireccionEnvio.numero_interior = direccionEnvio.numero_interior;
        pedidoDireccionEnvio.colonia = direccionEnvio.colonia;
        pedidoDireccionEnvio.delegacion_municipio = direccionEnvio.delegacion_municipio;
        pedidoDireccionEnvio.codigo_postal = direccionEnvio.codigo_postal;
        pedidoDireccionEnvio.ciudad = direccionEnvio.ciudad;
        pedidoDireccionEnvio.estado = direccionEnvio.estado;
        pedidoDireccionEnvio.pais = direccionEnvio.pais;
        pedidoDireccionEnvio.referencias = direccionEnvio.referencias;

        var result = PedidosEF.GuardarDireccionEnvio(lt_numero_pedido.Text, pedidoDireccionEnvio);

        pedidosDatos.establecerEstatusCalculo_Costo_Envio(true, numero_operacion);
        string resultado = pedidosDatos.actualizarEnvio(0, "Estándar", numero_operacion);
        var emailUserLogin = usuarios.userLoginName();
        var ProductosPedidos = PedidosEF.ObtenerProductosWithData(numero_operacion);
        var ListProductosEnvio = new List<ProductoEnvioCalculoModel>();

        foreach (var ProductoPedido in ProductosPedidos)
        {
            var p = new ProductoEnvioCalculoModel();
            p.Numero_Parte = ProductoPedido.datos.numero_parte;
            p.Tipo = 1;
            p.Cantidad = ProductoPedido.productos.cantidad;
            p.PesoKg = ProductoPedido.datos.peso;
            p.Largo = ProductoPedido.datos.profundidad;
            p.Ancho = ProductoPedido.datos.ancho;
            p.Alto = ProductoPedido.datos.alto;
            p.RotacionHorizontal = ProductoPedido.datos.RotacionHorizontal;
            p.RotacionVertical = ProductoPedido.datos.RotacionVertical;
            p.DisponibleParaEnvioGratuito = ProductoPedido.datos.disponibleEnvio;
            ListProductosEnvio.Add(p);
        }

        var ValidarRegla = new EnviosIncomReglas(numero_operacion, "pedido", ListProductosEnvio);

        if (ValidarRegla.Resultado.result == false)
        {
            // Si no aplica envio de alguna promoción, calculamos el costo del envío.
            try
            {
                ValidarCalculoEnvioOperacion validar = new ValidarCalculoEnvioOperacion(numero_operacion, "pedido");
            }
            catch (Exception ex)
            {
                devNotificaciones.error("Calcular envio en pedido: " + numero_operacion + " ", ex, emailUserLogin);
            }
        }

        string redirectUrl = GetRouteUrl("cliente-pedido-resumen", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",seguridad.Encriptar(hf_id_pedido.Value) }
                    });

        NotiflixJS.Message(this, NotiflixJS.MessageType.success, "Dirección de envío establecida");
        //msg_succes.Text = "Estableciendo <strong>Dirección de envío</strong>, redireccionando en 3,2,1....";
        //msg_succes.Visible = true;

        BootstrapCSS.RedirectJs(this, redirectUrl, 2000);

        CargarDatosPedido();
        CargarDireccionEnvio();
        cargarDirecciones();
    }
    protected void lv_direcciones_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        string id_pedido_direccion_envio = hf_id_pedido_direccion_envio.Value;
        if (!string.IsNullOrWhiteSpace(id_pedido_direccion_envio) && hf_pedido_tipo_envio.Value != "En Tienda" && hf_pedido_tipo_envio.Value != "Ninguno")
        {
            HiddenField hf_id_direccion = (HiddenField)e.Item.FindControl("hf_id_direccion");
            if (hf_id_direccion.Value == id_pedido_direccion_envio)
            {
                HtmlGenericControl contentCard_DireccEnvio = (HtmlGenericControl)e.Item.FindControl("contentCard_DireccEnvio");
                contentCard_DireccEnvio.Attributes["class"] += " bg-success-2";
            }
        }
    }

    protected void btn_recogeEnTienda_Click(object sender, EventArgs e)
    {
        string numero_operacion = lt_numero_pedido.Text;
        string id_operacion = hf_id_pedido.Value;
        Task.Run(() =>
        {
            try
            {
                string metodoEnvio = "En Tienda";
                decimal envio = 0;


                var resultado = pedidosDatos.actualizarEnvio(envio, metodoEnvio, numero_operacion, "");

                pedidosProductos.actualizarTotalStatic(numero_operacion);
            }
            catch (Exception ex)
            {

                var resultado = pedidosDatos.actualizarEnvio(0, "Ninguno", numero_operacion,
                    "Ocurrio un error al calcular tu envío, no te preocupes en unos momentos un asesor se pondrá en contacto contigo");


                devNotificaciones.error("Establecer envio en tienda: " + numero_operacion + " ", ex);
                materializeCSS.crear_toast(this, "Ocurrio un error", false);

            }
        });

        string redirectUrl = GetRouteUrl("cliente-pedido-resumen", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion", seguridad.Encriptar(id_operacion )}
                    });

        NotiflixJS.Message(this, NotiflixJS.MessageType.success, "Recoger en tienda");
        //msg_succes.Text = "Estableciendo <strong>Dirección de envío </strong>, redireccionando en 3,2,1....";
        //msg_succes.Visible = true;

        BootstrapCSS.RedirectJs(this, redirectUrl, 2500);

        CargarDatosPedido();
        CargarDireccionEnvio();
        cargarDirecciones();
    }

    protected void btn_entregaDomicilio_Click(object sender, EventArgs e)
    {
        ContentReferenciaDomiciliosGuardados.Visible = true;
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
    protected void EnabledCampos(bool status)
    {
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

        var result = direcciones_envio_EF.eliminar(IdDireccion);

        if (result.result)
        {
            //btn_recogeEnTienda_Click(sender, e);
            cargarDirecciones();
        }
        cargarDirecciones();
    }
}
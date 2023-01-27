using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

public partial class usuario_cliente_resumen : System.Web.UI.Page
{
    NumberFormatInfo myNumberFormatInfo = new CultureInfo("es-MX", true).NumberFormat;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Title = "Resumen de pedido";
            if (Page.RouteData.Values["id_operacion"] != null)
            {
                NotiflixJS.Loading(this, NotiflixJS.LoadingType.loading);
                ObtenerCFDI();
                CargarDatosPedido();
                CargarProductos();
                EstablecerNavegacion();
                AsignarUsuarioAsesor.numero_operacion = lt_numero_pedido.Text;
                uc_EdicionDetallesDeEnvioPedido.numero_operacion = lt_numero_pedido.Text;
                ValidarModoAsesor();
            }
            else
            {
                Response.Redirect(HttpContext.Current.Request.Url.Authority, true);
            }
            ValidarEstatusPago();
            NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
        }
    }
    protected void CargarProductos()
    {
        var productos = PedidosEF.ObtenerProductosWithData(lt_numero_pedido.Text);
        lbl_total_productos.Text = productos.Sum(t => t.productos.precio_total).ToString("C2", myNumberFormatInfo) + " " + hf_moneda_pedido.Value;
        lv_productos.DataSource = productos;
        lv_productos.DataBind();
    }
    protected void CargarDatosPedido()
    {
        string route_id_operacion = "";
        try
        {
            route_id_operacion = Page.RouteData.Values["id_operacion"].ToString();
            route_id_operacion = seguridad.DesEncriptar(route_id_operacion);
        }
        catch (Exception ex)
        {
            return;
        }
        int idPedido = int.Parse(route_id_operacion);

        var pedidos_datos = PedidosEF.ObtenerDatos(idPedido);
        var pedido_montos = PedidosEF.ObtenerNumeros(pedidos_datos.numero_operacion);
        direcciones_envio direccionEnvio = direcciones_envio_EF.Obtener(idPedido);

        #region Validación de permiso de privacidad
        if (usuarios.userLogin().tipo_de_usuario == "cliente" && usuarios.userLogin().email != pedidos_datos.usuario_cliente)
        {
            Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority));
        }
        #endregion

        lt_numero_pedido.Text = pedidos_datos.numero_operacion;
        lbl_numero_operacion.Text = pedidos_datos.numero_operacion;
        lbl_fecha_creacion.Text = pedidos_datos.fecha_creacion.ToString();

        if (pedidos_datos.idPedidoSAP != null)
        {
            up_pedidoSAP.Visible = true;
            lbl_pedidoSAP.Text = pedidos_datos.idPedidoSAP;
            //btn_cargaSAP.Enabled = false;
            //btn_cargaSAP.ToolTip = "Pedido creado en SAP con ID: " + pedidos_datos.idPedidoSAP;
            //btn_cargaSAP.CssClass = "is-btn-gray is-select-none";
            //btn_cargaSAP.Attributes.Add("style", "cursor: not-allowed");
        }
        else
        {
            btn_cargaSAP.Enabled = true;
        }

        lt_usuario_cliente.Text = pedidos_datos.usuario_cliente;
        hf_id_pedido.Value = route_id_operacion;
        hf_pedido_tipo_envio.Value = pedido_montos.metodoEnvio;
        hf_moneda_pedido.Value = pedido_montos.monedaPedido;

        lbl_envio.Text = pedido_montos.envio.ToString("C2", myNumberFormatInfo) + " " + pedido_montos.monedaPedido;
        lbl_subtotal.Text = pedido_montos.subtotal.ToString("C2", myNumberFormatInfo) + " " + pedido_montos.monedaPedido;
        lbl_impuestos.Text = pedido_montos.impuestos.ToString("C2", myNumberFormatInfo) + " " + pedido_montos.monedaPedido;
        lbl_total.Text = pedido_montos.total.ToString("C2", myNumberFormatInfo) + " " + pedido_montos.monedaPedido;

        #region Datos de Contacto
        contacto_title.InnerText = $"{pedidos_datos.cliente_nombre} {pedidos_datos.cliente_apellido_paterno}";
        if (string.IsNullOrEmpty(pedidos_datos.telefono) && string.IsNullOrEmpty(pedidos_datos.celular))
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Completa la información de contacto");
            contacto_desc.InnerHtml = $"<ul class='list-none'><li class='is-text-red is-font-semibold'>Teléfono*: {pedidos_datos.celular}</li>" + $"<li class='is-text-red is-font-semibold'>Teléfono alternativo: {pedidos_datos.telefono}</li></ul>" + $"<p class='is-text-red'>*Campos obligatorios</p>";
            btn_continuarMetodoPago.ToolTip += "Debes ingresar un teléfono de contacto";
            btn_continuarMetodoPago.Attributes.Add("style", "cursor: not-allowed;");
            btn_continuarMetodoPago.Enabled = false;
        }
        else if (!string.IsNullOrEmpty(pedidos_datos.celular) && string.IsNullOrEmpty(pedidos_datos.telefono))
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.info, "Te recomendamos agregar el teléfono alternativo.");
            contacto_desc.InnerHtml = $"<ul class='list-none'><li>Teléfono: {pedidos_datos.celular}</li>" + $"<li>Teléfono alternativo: {pedidos_datos.telefono}</li></ul>";
            btn_continuarMetodoPago.Enabled = true;
        }
        else
        {
            contacto_desc.InnerHtml = $"<ul class='list-none'><li>Teléfono: {pedidos_datos.celular}</li>" + $"<li>Teléfono alternativo: {pedidos_datos.telefono}</li></ul>";
            btn_continuarMetodoPago.Enabled = true;
        }
        #endregion

        #region Facturación
        if (pedidos_datos.factura == true)
        {
            CargarDireccionFacturacion();
            ContentFacturacionUsoCFDI.Visible = true;
            regimen_fiscal.Visible = true;
        }
        else
        {
            facturacion_title.InnerText = "No requiero factura";
            facturacion_desc.InnerText = "";
            ContentFacturacionUsoCFDI.Visible = false;
        }
        #endregion

        #region Envio
        if (pedido_montos.metodoEnvio == "En Tienda")
        {
            metodo_envio_title.InnerText = pedido_montos.metodoEnvio;
            metodo_envio_desc.InnerText = "Te esperamos en nuestra sucursal: ";
            localizacionTienda.Visible = true;
        }
        else if (pedido_montos.metodoEnvio == "Ninguno")
        {
            //nombreEnvio.InnerText = direccionEnvio.nombre_direccion;
            metodo_envio_title.InnerText = "Método de envío no seleccionado.";
            NotiflixJS.Message(this, NotiflixJS.MessageType.warning, "Método de envio no seleccionado");
            btn_continuarMetodoPago.Enabled = false;
            btn_continuarMetodoPago.ToolTip += "Asignar un método de envío";
            btn_continuarMetodoPago.Attributes["style"] = "cursor: not-allowed";
        }
        else
        {
            CargarDireccionEnvio();
        }
        if (!string.IsNullOrWhiteSpace(pedido_montos.EnvioNota))
        {
            // Cambiar si se reactiva la oferta de envío gratis.
            if (pedido_montos.EnvioNota == "Tu operación aplica para envío gratis.")
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.info, "Un asesor se contactará para los detalles del envío.");
                //msg_alert_envio.InnerText = "Un asesor se contactará para los detalles del envío.";
                //msg_alert_envio.Visible = false;
                //btn_Borrar_msg_alert_envio.Visible = true;
            }
            else
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.info, pedido_montos.EnvioNota);
                //msg_alert_envio.InnerText = pedido_montos.EnvioNota;
                //msg_alert_envio.Visible = true;
                //btn_Borrar_msg_alert_envio.Visible = true;
            }
        }
        else
        {
            //msg_alert_envio.InnerText = pedido_montos.EnvioNota;
            //msg_alert_envio.Visible = false;
            //btn_Borrar_msg_alert_envio.Visible = false;
        }

        #endregion

        #region Validación cancelación de pedido
        if (pedidos_datos.OperacionCancelada == true)
        {
            BloquearBotones();
            content_pedido_cancelado.Visible = true;
            lbl_motivoCancelacion.Text = pedidos_datos.motivoCancelacion;
            btn_cancelar_pedido.Visible = false;
            link_modal_cancelar_pedido.Visible = false;
            btn_continuarMetodoPago.Enabled = false;
            btn_continuarMetodoPago.CssClass = "is-decoration-none is-btn-gray is-select-none";
            btn_continuarMetodoPago.Attributes.Add("style", "cursor: not-allowed;");
            btn_continuarMetodoPago.ToolTip = "Pedido cancelado";
        }
        #endregion
    }

    protected void ValidarModoAsesor()
    {
        #region Modo Asesor
        // Activa el modo asesor de manera automática si un usuario Incom entra a esta sección
        bool modoAsesorActivado = usuarios.modoAsesorActivadoBool();
        if (usuarios.userLogin().tipo_de_usuario == "usuario")
        {

            //if (msg_alert_envio.InnerText == "")
            //{
            //    btn_Borrar_msg_alert_envio.Visible = false;
            //}
            //else
            //{
            //    btn_Borrar_msg_alert_envio.Visible = true;
            //}
            Content_AsesorSeguimiento.Visible = true;
            Content_Pago_Datos_Transferencia_Asesor.Visible = true;
            System.Web.HttpContext.Current.Session["modoAsesor"] = true;
            HttpContext.Current.Session["datosCliente"] = usuarios.recuperar_DatosUsuario(lt_usuario_cliente.Text);

        }
        #endregion
    }
    protected void CargarDireccionEnvio()
    {
        int idDireccion = int.Parse(hf_id_pedido.Value);
        var result = PedidosEF.ObtenerDireccionEnvio(lt_numero_pedido.Text);
        if (result.result)
        {
            pedidos_direccionEnvio direccionEnvio = result.response;
            if (direccionEnvio != null)
            {
                hf_id_pedido_direccion_envio.Value = direccionEnvio.idDireccionEnvio.ToString();
                metodo_envio_desc.InnerHtml = $"<p class='is-select-all is-m-0 is-text-justify'>{direccionEnvio.calle} {direccionEnvio.numero}, {direccionEnvio.colonia}, {direccionEnvio.codigo_postal} {direccionEnvio.delegacion_municipio}, {direccionEnvio.estado}.</p><p class='is-m-0'>Referencias: {direccionEnvio.referencias}</p>";
                metodo_envio_desc.InnerHtml = $"" +
               $"<span class='text-secondary'>Calle:</span> {direccionEnvio.calle} " +
               $"<span class='text-secondary'>Número:</span>  {direccionEnvio.numero} " +
               $"<span class='text-secondary'>Colonia:</span> {direccionEnvio.colonia}, " +
               $"<span class='text-secondary'>C.P.</span>  {direccionEnvio.codigo_postal}, " +
               $"<span class='text-secondary'>Municipio: </span>{direccionEnvio.delegacion_municipio}, " +
               $"<span class='text-secondary'>Estado:</span> {direccionEnvio.estado}," +
               $" {direccionEnvio.pais} " +
              $"<br><span class='text-secondary'>Referencias:</span> {direccionEnvio.referencias} ";
            }
        }
        else
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "No se ha podido cargar la dirección de envío");
            //BootstrapCSS.Message(this, "#", BootstrapCSS.MessageType.danger, "Error", result.message);
        }
    }
    protected void CargarDireccionFacturacion()
    {
        var result = PedidosEF.ObtenerDireccionFacturacion(lt_numero_pedido.Text);

        if (result.result)
        {
            pedidos_direccionFacturacion direccion = result.response;
            if (direccion != null)
            {
                facturacion_desc.InnerHtml = $"<span class='text-secondary'>Razón social</span>: {direccion.razon_social} - RFC: {direccion.rfc} <br>" +
                    $"<span class='text-secondary'>Calle:</span> {direccion.calle}, " +
                    $"<span class='text-secondary'>Número:</span> {direccion.numero}, " +
                    $"<span class='text-secondary'>Colonia:</span> {direccion.colonia}, " +
                    $"<span class='text-secondary'>C.P.</span>  {direccion.codigo_postal}, " +
                    $"<span class='text-secondary'>Municipio: </span> {direccion.delegacion_municipio}, " +
                    $" <span class='text-secondary'>Estado:</span>  {direccion.estado}, {direccion.pais} ";
                ddl_UsoCFDI.SelectedValue = direccion.UsoCFDI;

                if (string.IsNullOrWhiteSpace(direccion.UsoCFDI))
                {
                    ddl_UsoCFDI.SelectedIndex = 0;
                }
                if (string.IsNullOrEmpty(direccion.RegimenFiscal))
                {
                    regimen_fiscal.InnerHtml = "<strong>Régimen fiscal: </strong><span class='is-text-red'>No registrado</span>";
                    NotiflixJS.Message(this, NotiflixJS.MessageType.info, "Agrega tu régimen fiscal en datos de facturación");
                    btn_continuarMetodoPago.ToolTip += "El régimen fiscal es necesario";
                    btn_continuarMetodoPago.Attributes.Add("style", "cursor: not-allowed;");
                    btn_continuarMetodoPago.Enabled = false;
                }
                else
                {
                    string regimenFiscalCompleto = PedidosEF.obtenerDescripcionRegimenFiscal(direccion.RegimenFiscal);
                    if (!string.IsNullOrEmpty(regimenFiscalCompleto))
                    {
                        regimen_fiscal.InnerHtml = "<strong>Régimen fiscal:</strong> " + regimenFiscalCompleto;
                    }
                    else
                    {
                        regimen_fiscal.InnerHtml = "<strong>Régimen fiscal:</strong> " + direccion.RegimenFiscal;
                    }
                }
            }
        }
        else
        {
            BootstrapCSS.Message(this, "#", BootstrapCSS.MessageType.danger, "Error", result.message);
        }
    }
    protected void lv_productos_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        PedidosProductosDatos producto = e.Item.DataItem as PedidosProductosDatos;
        Literal lt_precio_unitario = (Literal)e.Item.FindControl("lt_precio_unitario");
        lt_precio_unitario.Text = producto.productos.precio_unitario.ToString("C2", myNumberFormatInfo);
        Literal lt_precio_total = (Literal)e.Item.FindControl("lt_precio_total");
        lt_precio_total.Text = producto.productos.precio_total.ToString("C2", myNumberFormatInfo) + " " + hf_moneda_pedido.Value;
        Literal lt_cantidad = (Literal)e.Item.FindControl("lt_cantidad");
        lt_cantidad.Text = Decimal.ToInt32(producto.productos.cantidad).ToString();
        Image img_producto = (Image)e.Item.FindControl("img_producto");

        if (producto.datos != null)
        {
            var imagenes = producto.datos.imagenes;
            if (!string.IsNullOrWhiteSpace(imagenes))

                img_producto.ImageUrl = archivosManejador.imagenProducto(imagenes.Split(',')[0]);
        }
        else img_producto.ImageUrl = archivosManejador.imagenProducto(null);
    }
    protected void EstablecerNavegacion()
    {
        btn_cambiar_metodo_envio.NavigateUrl = GetRouteUrl("cliente-pedido-envio", new System.Web.Routing.RouteValueDictionary
        {
            { "id_operacion",seguridad.Encriptar(hf_id_pedido.Value) }
        });

        link_cambiar_contacto.NavigateUrl = GetRouteUrl("cliente-pedido-datos", new System.Web.Routing.RouteValueDictionary
        {
            { "id_operacion",seguridad.Encriptar(hf_id_pedido.Value) }
        });

        link_cambiar_direcc_facturacion.NavigateUrl = GetRouteUrl("cliente-pedido-facturacion", new System.Web.Routing.RouteValueDictionary
        {
            { "id_operacion",seguridad.Encriptar(hf_id_pedido.Value) }
        });

        link_pago_santander.NavigateUrl = GetRouteUrl("cliente-pedido-pago-santander", new System.Web.Routing.RouteValueDictionary
        {
            { "id_operacion",seguridad.Encriptar(hf_id_pedido.Value) }
        });

        link_pago_paypal.NavigateUrl = GetRouteUrl("cliente-pedido-pago-paypal", new System.Web.Routing.RouteValueDictionary
        {
            { "id_operacion",seguridad.Encriptar(hf_id_pedido.Value) }
        });

        btn_regresar_pedidos.NavigateUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/usuario/mi-cuenta/pedidos.aspx";

        //btn_continuarMetodoPago.NavigateUrl = GetRouteUrl("cliente-pedido-pago", new System.Web.Routing.RouteValueDictionary
        //{
        //    { "id_operacion", seguridad.Encriptar(hf_id_pedido.Value) }
        //});
    }


    protected void btn_sin_factura_Click(object sender, EventArgs e)
    {
        PedidosEF.ActualizarFacturacion(lt_numero_pedido.Text, false);
        PedidosEF.EliminarDireccionDeFacturacion(lt_numero_pedido.Text);
        CargarDatosPedido();
    }
    protected void BloquearBotones()
    {
        btn_cambiar_metodo_envio.Enabled = false;
        btn_cambiar_metodo_envio.CssClass = "is-decoration-none is-select-none is-cursor-not-allowed";
        btn_cambiar_metodo_envio.ToolTip = "Pedido cancelado";
        btn_sin_factura.Enabled = false;
        btn_sin_factura.CssClass = "is-decoration-none is-select-none is-cursor-not-allowed";
        btn_sin_factura.ToolTip = "Pedido cancelado";
        link_cambiar_contacto.Enabled = false;
        link_cambiar_contacto.CssClass = "is-decoration-none is-select-none is-cursor-not-allowed";
        link_cambiar_contacto.ToolTip = "Pedido cancelado";
        link_cambiar_direcc_facturacion.Enabled = false;
        link_cambiar_direcc_facturacion.CssClass = "is-decoration-none is-select-none is-cursor-not-allowed";
        link_cambiar_direcc_facturacion.ToolTip = "Pedido cancelado";
    }
    protected void MostrarMétodosDePago()
    {
        Pago_Pendiente.Visible = true;
    }
    protected void ValidarPagoTransferencia(pedidos_pagos_transferencia Transferencia)
    {
        link_pago_paypal.Visible = false;
        link_pago_santander.Visible = false;

        //ContentGenerarReferenciaTransferencia.Visible = false;
        ContentReferenciaTransferencia.Visible = true;

        chk_TranfenciaConfirmadaAsesor.Checked = (bool)Transferencia.confirmacionAsesor;
        txt_TranfenciaReferenciaAsesor.Text = Transferencia.referencia;

        if ((bool)Transferencia.confirmacionAsesor == true)
            Pago_Pendiente.Visible = false;
        else
        {
            Pago_Pendiente.Visible = true;
        }
    }
    protected async void ValidarEstatusPago()
    {
        string numero_operacion = lt_numero_pedido.Text;
        var status = await PedidosEF.ObtenerPagoPedido(numero_operacion);

        if (status.result && status.exception == false)
        {

            // BootstrapCSS.Message(this, ".content_msg_confirmacion_pedido", BootstrapCSS.MessageType.info,
            //"Pago registrado",
            //$"{status.message} Si deseas realizar algún cambio en el método/dirección de envío solicitalo a un asesor." +
            //"<br><a href=/informacion/ubicacion-y-sucursales.aspx#contacto'>Contactar a un asesor</a>");
            cnt_transferencia_registrada.Visible = true;
            txt_transferencia_mensaje.InnerHtml = $"{status.message}";
            txt_transferencia_contacto.InnerHtml = "Las transferencias pueden tomar tiempo en reflejarse, un asesor comprobará y confirmará tu transferencia. Si deseas realizar algún cambio en el método/dirección de envío solicitalo a un asesor.<br><a href=/informacion/ubicacion-y-sucursales.aspx#contacto'>Contactar a un asesor</a>";
            btn_cambiar_metodo_envio.Enabled = false;
            btn_cambiar_metodo_envio.CssClass = "is-select-none is-cursor-not-allowed";
            btn_cambiar_metodo_envio.ToolTip = $"{status.message}";
            //btn_cambiar_metodo_envio.Text = status.message;
            link_cambiar_direcc_facturacion.Enabled = false;
            link_cambiar_direcc_facturacion.CssClass = "is-select-none is-cursor-not-allowed";
            link_cambiar_direcc_facturacion.ToolTip = $"{status.message}";
            btn_continuarMetodoPago.Enabled = false;
            btn_continuarMetodoPago.CssClass = "is-decoration-none is-btn-gray";
            btn_continuarMetodoPago.Attributes.Add("style", "cursor: not-allowed;");
            btn_continuarMetodoPago.ToolTip = $"{status.message}";
            ddl_UsoCFDI.Enabled = false;
            up_cargaSAP.Visible = true;

            NotiflixJS.Message(this, NotiflixJS.MessageType.info, status.message);

            dynamic Pago = status.response;

            if (Pago.tipo == "Transferencia")
            {
                ValidarPagoTransferencia(Pago.pago);
                up_ConfirmarDeposito.Visible = true;
            }
        }
        else if (status.exception == true)
        {

            BootstrapCSS.Message(this, ".content_msg_confirmacion_pedido", BootstrapCSS.MessageType.danger,
                  "Error interno.",
                status.message + " Contacta a un asesor para que te ayude a validar tu pago." +
           "<br><a href=/informacion/ubicacion-y-sucursales.aspx#contacto'>Contactar a un asesor</a>");

            btn_cambiar_metodo_envio.Enabled = false;
            btn_cambiar_metodo_envio.CssClass = "btn btn-secondary disabled";
            btn_cambiar_metodo_envio.Text = status.message;
        }
        else
        {
            MostrarMétodosDePago();
        }
    }
    protected void btn_cancelar_pedido_Click(object sender, EventArgs e)
    {
        string numero_operacion = lt_numero_pedido.Text;
        string motivo = txt_motivo_cancelacion.Text;
        var resultadoCancelacion = PedidosEF.CancelarPedido(numero_operacion, motivo);
        var pedidoDatos = PedidosEF.ObtenerDatos(int.Parse(hf_id_pedido.Value));
        var pedidoProductos = PedidosEF.ObtenerProductos(lt_numero_pedido.Text);

        string redirectUrl = GetRouteUrl("cliente-pedido-resumen", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion", seguridad.Encriptar( pedidoDatos.id.ToString()) }
                    });

        DateTime fechaSolicitud = utilidad_fechas.obtenerCentral();
        string asunto = "Incom.mx Cancelación de pedido: " + pedidoDatos.nombre_pedido + " ";
        string mensaje = string.Empty;
        string filePathHTML = "/email_templates/operaciones/pedidos/pedido_cancelado.html";



        string productosEmailHTML = string.Empty;
        foreach (var p in pedidoProductos)
        {
            productosEmailHTML += "<strong>" + p.numero_parte + "</strong> - " + p.descripcion + "<br><br>";
        }

        Dictionary<string, string> datosDiccRemplazo = new Dictionary<string, string>
        {
            { "{fecha}", utilidad_fechas.DDMMAAAA() },
            { "{nombre}", pedidoDatos.cliente_nombre },
            { "{numero_operacion}", numero_operacion },
            { "{url_operacion}", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + redirectUrl },
            { "{motivoCancelacion}", motivo }
        };

        mensaje = archivosManejador.reemplazarEnArchivo(filePathHTML, datosDiccRemplazo);

        emailTienda email = new emailTienda(asunto, $"tpavia@incom.mx, jhernandez@incom.mx, pjuarez@incom.mx,  {pedidoDatos.usuario_cliente}", mensaje, "retail@incom.mx");
        email.general();




        if (resultadoCancelacion.result)
        {
            CargarDatosPedido();
            CargarProductos();
            EstablecerNavegacion();
            BootstrapCSS.Message(this, "#content_msg_cancelar_pedido", BootstrapCSS.MessageType.success, "Solicitud enviada", resultadoCancelacion.message);
        }
        else
        {
            BootstrapCSS.Message(this, "#content_msg_cancelar_pedido", BootstrapCSS.MessageType.danger, "Error", resultadoCancelacion.message);
        }

    }

    protected async void btn_pago_transferencia_Click(object sender, EventArgs e)
    {
        var Transferencia = new pedidos_pagos_transferencia();
        Transferencia.numero_operacion = lt_numero_pedido.Text;
        Transferencia.confirmacionAsesor = false;
        Transferencia.fecha_captura = utilidad_fechas.obtenerCentral();

        var result = await PedidosEF.GenerarReferenciaTransferencia(Transferencia);

        if (result.result == false || result.exception == true)
        {
            BootstrapCSS.Message(up_ConfirmarDeposito, "#content_msg_transfrencia", BootstrapCSS.MessageType.warning,
                "Error", result.message + "<br> No te preocupes, un asesor ya ha sido informado de esto.");

            return;
        }
        //ContentGenerarReferenciaTransferencia.Visible = false;
        ContentReferenciaTransferencia.Visible = true;
        up_ConfirmarDeposito.Update();

    }

    protected void ObtenerCFDI()
    {
        using (var db = new tiendaEntities())
        {
            var r = db.PedidosClaveUsoCFDIs.AsNoTracking().ToList();
            ddl_UsoCFDI.AppendDataBoundItems = true;
            ddl_UsoCFDI.DataSource = r;
            ddl_UsoCFDI.DataValueField = "ClaveUsoCFDI";
            ddl_UsoCFDI.DataTextField = "Descripción";
            ddl_UsoCFDI.DataBind();
            ddl_UsoCFDI.Items.Insert(0, new ListItem("-- Selecciona --", ""));
            ddl_UsoCFDI.DataBind();

        }
    }
    protected async void btn_guardarTransferenciaDatosAsesor_Click(object sender, EventArgs e)
    {

        var Result = await PedidosEF.ObtenerReferenciaTransferencia(lt_numero_pedido.Text);


        if (Result.exception == true)
        {
            BootstrapCSS.Message(up_ConfirmarDeposito, "#content_msg_transfrencia", BootstrapCSS.MessageType.warning,
               "Error", Result.message + " <br> No te preocupes, un asesor ya ha sido informado de esto.");

            return;
        }
        pedidos_pagos_transferencia ReferenciaTransferencia = Result.response;

        ReferenciaTransferencia.fecha_confirmacion = utilidad_fechas.obtenerCentral();
        ReferenciaTransferencia.confirmacionAsesor = chk_TranfenciaConfirmadaAsesor.Checked;
        ReferenciaTransferencia.referencia = txt_TranfenciaReferenciaAsesor.Text;
        ReferenciaTransferencia.idUsuario = usuarios.userLogin().id;


        var ResultActualizacion = await PedidosEF.ActualizarReferenciaTransferencia(ReferenciaTransferencia);
        if (ResultActualizacion.exception == true || ResultActualizacion.result == false)

        {
            NotiflixJS.Message(up_ConfirmarDeposito, NotiflixJS.MessageType.failure, "Error al actualizar el pago");
            //BootstrapCSS.Message(up_ConfirmarDeposito, "#content_msg_transfrencia", BootstrapCSS.MessageType.warning,
            //   "Error", Result.message + " <br> Informa a un administrador.");
            return;
        }

        NotiflixJS.Message(up_ConfirmarDeposito, NotiflixJS.MessageType.success, "Referencia registrada");
        //BootstrapCSS.Message(up_ConfirmarDeposito, "#content_msg_transfrencia", BootstrapCSS.MessageType.success,
        //       "Actualizado con éxito", Result.message + " <br> Informa a un administrador.");

        //ContentGenerarReferenciaTransferencia.Visible = false;
        ContentReferenciaTransferencia.Visible = true;
        up_ConfirmarDeposito.Update();
        string redireccionURL = GetRouteUrl("cliente-pedido-resumen", new System.Web.Routing.RouteValueDictionary
        {
            { "id_operacion", seguridad.Encriptar(hf_id_pedido.Value) }
        });
        BootstrapCSS.RedirectJs(this, redireccionURL, 100);
    }

    protected void ddl_UsoCFDI_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            using (var db = new tiendaEntities())
            {
                var Pedido = db.pedidos_direccionFacturacion
                    .Where(x => x.numero_operacion == lt_numero_pedido.Text)
                    .FirstOrDefault();

                Pedido.UsoCFDI = ddl_UsoCFDI.SelectedValue;
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void btn_Borrar_msg_alert_envio_Click(object sender, EventArgs e)
    {
        var Result = PedidosEF.ActualizarEnvioNota(lt_numero_pedido.Text, null);
        if (Result.result == true)
        {

            // Necesario para redirección
            string script = @"setTimeout(function () { window.location.replace(window.location.href)}, 3500);";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "redirección", script, true);

            BootstrapCSS.Message(this, ".Conteng_msg_envioNota", BootstrapCSS.MessageType.success, "Actualizado con éxito", "Nota eliminada con éxito");
        }
        else
        {
            BootstrapCSS.Message(this, ".Conteng_msg_envioNota", BootstrapCSS.MessageType.danger, "Error", "Error al eliminar nota método de envío");
        }
    }
    protected void btn_continuarMetodoPago_Click(object sender, EventArgs e)
    {
        NotiflixJS.Loading(this, NotiflixJS.LoadingType.loading);
        string redireccionUrl = GetRouteUrl("cliente-pedido-pago", new System.Web.Routing.RouteValueDictionary
        {
            { "id_operacion", seguridad.Encriptar(hf_id_pedido.Value) }
        });
        BootstrapCSS.RedirectJs(this, redireccionUrl, 1000);
    }

    protected void btn_cargaSAP_Click(object sender, EventArgs e)
    {
        NotiflixJS.Loading(this, NotiflixJS.LoadingType.loading);
        try
        {
            putOrderinSAP(lt_numero_pedido.Text);
        }
        catch (Exception ex)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Error");
        }
        NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
    }
    protected void putOrderinSAP(string id_pedido)
    {
        HttpWebRequest request = createSOAPWebRequest();
        System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(acceptAllCertifications);

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        string productosXML = string.Empty;
        string moneda = hf_moneda_pedido.Value;
        string dateTime = utilidad_fechas.obtenerCentral().ToString("yyyy-MM-ddTHH:mm:ssZ");
        decimal costoEnvio = PedidosEF.ObtenerNumeros(id_pedido).envio;
        DataTable dtProductos = pedidosProductos.obtenerProductos(id_pedido);
        if (dtProductos.Rows.Count > 0)
        {
            int posicion = 10;
            foreach (DataRow producto in dtProductos.Rows)
            {
                string cantidad = producto["cantidad"].ToString();
                string precio_unitario = producto["precio_unitario"].ToString();
                string noParteSAP = producto["noParteSAP"].ToString();
                productosXML += $@"
                    <Item actionCode=""04"">
                        <!-- ID de posición, debe incrementarse de 10 en 10 -->
                        <ID>{posicion}</ID>
                        <!-- Liberar para ejecutar, corresponde al ATP? -->
                        <ReleaseToExecute>true</ReleaseToExecute>
                        <!-- Número de parte del producto pedido -->
                        <ItemProduct actionCode=""04"">
                            <ProductInternalID>{noParteSAP}</ProductInternalID>
                        </ItemProduct>
                        <!-- Línea de programación de árticulos, revisar -->
                        <ItemScheduleLine actionCode=""04"">
                            <ID>1</ID>
                            <TypeCode>1</TypeCode>
                            <!-- Cantidad de items solicitados en el pedido -->
                            <Quantity>{cantidad}</Quantity>
                        </ItemScheduleLine>
                        <!--Optional:-->
	                    <PriceAndTaxCalculationItem actionCode=""04"">
	                        <ItemMainPrice actionCode=""04"">
	                            <Rate>
	                            <DecimalValue>{precio_unitario}</DecimalValue>
	                            <CurrencyCode>{moneda}</CurrencyCode>
	                            <BaseDecimalValue>1.0</BaseDecimalValue>
	                            </Rate>
	                        </ItemMainPrice>
	                    </PriceAndTaxCalculationItem>
                    </Item>
                ";
                posicion += 10;
            }

            if (costoEnvio > 0)
            {
                productosXML += $@"
                    <Item actionCode=""04"">
                        <!-- ID de posición, debe incrementarse de 10 en 10 -->
                        <ID>{posicion}</ID>
                        <!-- Liberar para ejecutar, corresponde al ATP? -->
                        <ReleaseToExecute>false</ReleaseToExecute>
                        <!-- Número de parte del producto pedido -->
                        <ItemProduct actionCode=""04"">
                            <ProductInternalID>MANEJO_DE_MATERIAL</ProductInternalID>
                        </ItemProduct>
                        <ItemServiceTerms actionCode=""04"">
	                      <ResourceID>ENVIO_CDMX</ResourceID>
	                   </ItemServiceTerms>
                        <!-- Línea de programación de árticulos, revisar -->
                        <ItemScheduleLine actionCode=""04"">
                            <ID>1</ID>
                            <TypeCode>1</TypeCode>
                            <!-- Cantidad de items solicitados en el pedido -->
                            <Quantity>1</Quantity>
                        </ItemScheduleLine>
                        <!--Optional:-->
	                    <PriceAndTaxCalculationItem actionCode=""04"">
	                        <ItemMainPrice actionCode=""04"">
	                            <Rate>
	                            <DecimalValue>{costoEnvio}</DecimalValue>
	                            <CurrencyCode>{moneda}</CurrencyCode>
	                            <BaseDecimalValue>1.0</BaseDecimalValue>
	                            </Rate>
	                        </ItemMainPrice>
	                    </PriceAndTaxCalculationItem>
                    </Item>
                ";
            }

            if (productosXML != null)
            {
                XmlDocument body = new XmlDocument();

                body.LoadXml($@"
                    <soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:glob=""http://sap.com/xi/SAPGlobal20/Global"" xmlns:glob1=""http://sap.com/xi/AP/Globalization"" xmlns:a0v=""http://sap.com/xi/AP/CustomerExtension/BYD/A0VKF"">
                       <soap:Header/>
                       <soap:Body>
                          <glob:SalesOrderBundleMaintainRequest_sync>
                             <BasicMessageHeader>
                             </BasicMessageHeader>
		                    <SalesOrder actionCode=""01"">
                                    <!-- Referencia externa -->
                                    <BuyerID>{id_pedido}</BuyerID>
                                    <!-- Nodo de campaña -->
                                    <BusinessTransactionDocumentReference actionCode=""01"">
                                        <BusinessTransactionDocumentReference>
                                            <!-- ID de campaña -->
                                            <ID>81</ID>
                                            <!-- Código interno en SAP para referirse a Campaña -->
                                            <TypeCode>764</TypeCode>
                                        </BusinessTransactionDocumentReference>
                                        <BusinessTransactionDocumentRelationshipRoleCode>1</BusinessTransactionDocumentRelationshipRoleCode>
                                        <DataProviderIndicator>true</DataProviderIndicator>
                                    </BusinessTransactionDocumentReference>
                                    <!-- Fin de Nodo de campaña -->
                                    <!-- Unidad de venta, 101024 corresponde a TERCEROS -->
                                    <SalesUnitParty actionCode=""04"">
                                        <PartyID>101024</PartyID>
                                    </SalesUnitParty>
                                    <!-- Canal de distribución, Z4 corresponde a Ecommerce -->
                                    <SalesAndServiceBusinessArea actionCode=""04"">
                                        <DistributionChannelCode>Z4</DistributionChannelCode>
                                    </SalesAndServiceBusinessArea>
                                    <!-- Empleado responsable, 1000 corresponde a Francisco -->
                                    <EmployeeResponsibleParty actionCode=""04"">
                                        <PartyID>8065</PartyID>
                                    </EmployeeResponsibleParty>
                                    <!-- Nombre de cliente -->
                                    <AccountParty actionCode=""04"">
                                        <PartyID>1017775</PartyID>
                                    </AccountParty>
                                    <!-- Términos de venta -->
                                    <PricingTerms actionCode=""04"">
                                        <CurrencyCode>{moneda}</CurrencyCode>
                                        <!-- Fecha y hora de operación -->
                                        <PriceDateTime timeZoneCode=""UTC"">{dateTime}</PriceDateTime>
                                        <!-- false indica que el desglose de impuestos lo realiza SAP, en caso de enviar monto bruto deben enviarse los nodos correspondientes -->
                                        <GrossAmountIndicator>false</GrossAmountIndicator>
                                    </PricingTerms>
                                    <!-- Nodo Item en la que se indican las posiciones -->
                                        {productosXML}
                                </SalesOrder>
                          </glob:SalesOrderBundleMaintainRequest_sync>
                       </soap:Body>
                    </soap:Envelope>
                ");
                using (Stream stream = request.GetRequestStream())
                {
                    body.Save(stream);
                }
                using (WebResponse service = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(service.GetResponseStream()))
                    {
                        var serviceResult = reader.ReadToEnd();
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(serviceResult);
                        XmlNode node = doc.DocumentElement.SelectSingleNode("//SalesOrder/ID");
                        if (node != null)
                        {
                            NotiflixJS.Message(this, NotiflixJS.MessageType.info, node.InnerText);
                            //pedidosDatos pedido = new pedidosDatos();
                            //pedido.actualizarPedido_idPedidoSAP(id_pedido, node.InnerText);
                            var actualizarIDPedidoSAP = PedidosEF.ActualizarIDPedidoSAP(id_pedido, node.InnerText);
                            if (actualizarIDPedidoSAP.result)
                            {
                                NotiflixJS.Message(this, NotiflixJS.MessageType.success, "Pedido creado en SAP con el ID: " + node.InnerText + " y almacenado");
                            }
                            else
                            {
                                NotiflixJS.Message(this, NotiflixJS.MessageType.warning, "Pedido creado en SAP pero no almacenado");
                            }
                            up_pedidoSAP.Update();
                            NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
                        }
                        else
                        {
                            NotiflixJS.Message(this, NotiflixJS.MessageType.warning, "No se ha recibido el ID de pedido en SAP");
                        }
                    }
                }
            }
            string redirectURL = GetRouteUrl("cliente-pedido-resumen", new System.Web.Routing.RouteValueDictionary
            {
                { "id_operacion", seguridad.Encriptar(hf_id_pedido.Value) }
            });
            BootstrapCSS.RedirectJs(this, redirectURL, 400);
        }
    }
    protected HttpWebRequest createSOAPWebRequest()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"https://my429740.businessbydesign.cloud.sap/sap/bc/srt/scs/sap/managesalesorderin5?sap-vhost=my429740.businessbydesign.cloud.sap");
        request.ContentType = "application/soap+xml;charset='utf-8'";
        request.Method = "POST";
        request.Credentials = new NetworkCredential("ASALINAS", "Pruebas00");
        return request;
    }
    protected bool acceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPlicyErrors)
    {
        return true;
    }
}
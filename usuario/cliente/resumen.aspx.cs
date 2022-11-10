using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

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
        }
    }
    protected void CargarProductos()
    {
        var productos = PedidosEF.ObtenerProductosWithData(lt_numero_pedido.Text);
        lbl_total_productos.Text = productos.Sum(t => t.productos.precio_total).ToString("C2", myNumberFormatInfo) + hf_moneda_pedido.Value;
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
        lt_nombre_operacion.Text = pedidos_datos.nombre_pedido;
        lbl_fecha_creacion.Text = pedidos_datos.fecha_creacion.ToString();

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
            btn_continuarMetodoPago.ToolTip = "Debes ingresar un teléfono de contacto";
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
            nombreEnvio.InnerText = direccionEnvio.nombre_direccion;
            metodo_envio_title.InnerText = "Método de envío no seleccionado.";
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
            msg_alert_envio.InnerText = pedido_montos.EnvioNota;
            msg_alert_envio.Visible = false;
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
                //  metodo_envio_desc.InnerHtml = $"" +
                // $"<span class='text-secondary'>Calle:</span> {direccionEnvio.calle} " +
                // $"<span class='text-secondary'>Número:</span>  {direccionEnvio.numero} " +
                // $"<span class='text-secondary'>Colonia:</span> {direccionEnvio.colonia}, " +
                // $"<span class='text-secondary'>C.P.</span>  {direccionEnvio.codigo_postal}, " +
                // $"<span class='text-secondary'>Municipio: </span>{direccionEnvio.delegacion_municipio}, " +
                // $"<span class='text-secondary'>Estado:</span> {direccionEnvio.estado}," +
                // $" {direccionEnvio.pais} " +
                //$"<br><span class='text-secondary'>Referencias:</span> {direccionEnvio.referencias} ";
            }
        }
        else
        {
            BootstrapCSS.Message(this, "#", BootstrapCSS.MessageType.danger, "Error", result.message);
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
        btn_sin_factura.Enabled = false;
        link_cambiar_contacto.Enabled = false;
        link_cambiar_direcc_facturacion.Enabled = false;
    }



    protected void MostrarMétodosDePago()
    {
        Pago_Pendiente.Visible = true;
    }

    protected void ValidarPagoTransferencia(pedidos_pagos_transferencia Transferencia)
    {

        link_pago_paypal.Visible = false;
        link_pago_santander.Visible = false;

        ContentGenerarReferenciaTransferencia.Visible = false;
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

            BootstrapCSS.Message(this, ".content_msg_confirmacion_pedido", BootstrapCSS.MessageType.info,
           "Pago registrado",
           $"{status.message} Si deseas realizar algún cambio en el método/dirección de envío solicitalo a un asesor." +
           "<br><a href=/informacion/ubicacion-y-sucursales.aspx#contacto'>Contactar a un asesor</a>");

            btn_cambiar_metodo_envio.Enabled = false;
            btn_cambiar_metodo_envio.CssClass = "btn btn-secondary disabled";
            btn_cambiar_metodo_envio.Text = status.message;

            dynamic Pago = status.response;

            if (Pago.tipo == "Transferencia") ValidarPagoTransferencia(Pago.pago);



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

        Dictionary<string, string> datosDiccRemplazo = new Dictionary<string, string>();

        datosDiccRemplazo.Add("{dominio}", Request.Url.GetLeftPart(UriPartial.Authority));
        datosDiccRemplazo.Add("{tipo_operacion}", "Pedido");
        datosDiccRemplazo.Add("{nombre_cotizacion}", "Pedido");
        datosDiccRemplazo.Add("{usuario_email}", pedidoDatos.usuario_cliente);
        datosDiccRemplazo.Add("{nombre}", pedidoDatos.cliente_nombre);
        datosDiccRemplazo.Add("{numero_operacion}", numero_operacion);
        datosDiccRemplazo.Add("{nombre_operacion}", pedidoDatos.nombre_pedido);
        datosDiccRemplazo.Add("{url_operacion}", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + redirectUrl);
        datosDiccRemplazo.Add("{productos}", productosEmailHTML);
        datosDiccRemplazo.Add("{motivoCancelacion}", motivo);

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
        ContentGenerarReferenciaTransferencia.Visible = false;
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
            BootstrapCSS.Message(up_ConfirmarDeposito, "#content_msg_transfrencia", BootstrapCSS.MessageType.warning,
               "Error", Result.message + " <br> Informa a un administrador.");

            return;
        }

        BootstrapCSS.Message(up_ConfirmarDeposito, "#content_msg_transfrencia", BootstrapCSS.MessageType.success,
               "Actualizado con éxito", Result.message + " <br> Informa a un administrador.");

        ContentGenerarReferenciaTransferencia.Visible = false;
        ContentReferenciaTransferencia.Visible = true;
        up_ConfirmarDeposito.Update();
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

        string redireccionUrl = GetRouteUrl("cliente-pedido-pago", new System.Web.Routing.RouteValueDictionary
        {
            { "id_operacion", seguridad.Encriptar(hf_id_pedido.Value) }
        });
        BootstrapCSS.RedirectJs(this, redireccionUrl, 100);
    }
}
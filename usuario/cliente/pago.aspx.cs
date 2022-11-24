using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Routing;
using System.Threading.Tasks;
using System.Globalization;
using System.Data.Entity;
using Org.BouncyCastle.Crypto;
using System.Web.UI.HtmlControls;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.EnterpriseServices;

public partial class usuario_cliente_pago : System.Web.UI.Page
{
    NumberFormatInfo numberFormatInfo = new CultureInfo("es-MX", true).NumberFormat;
    int diasVigenciaPedidoUSD = 30;
    int diasVigenciaPedidoMXN = 5;
    decimal limiteDiferenciaTipoDeCambio = (decimal)0.5;
    decimal montoMinimoPedidoEnvioGratuito = 3000;
    protected async void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Title = "Completa tu pago";
            if (Page.RouteData.Values["id_operacion"].ToString() != null)
            {
                await CargarDatosPedidoAsync();
                string route_id_operacion = Page.RouteData.Values["id_operacion"].ToString();
                route_id_operacion = seguridad.DesEncriptar(route_id_operacion);
                lbl_numero_pedido.Text = PedidosEF.ObtenerNumeroOperacion(int.Parse(route_id_operacion));
                lbl_numero_pedido_bottom.Text = lbl_numero_pedido.Text;
                hf_numero_operacion.Value = route_id_operacion;
                btn_regresar_resumen.NavigateUrl = GetRouteUrl("cliente-pedido-resumen", new System.Web.Routing.RouteValueDictionary
                {
                    { "id_operacion", seguridad.Encriptar(hf_numero_operacion.Value) }
                });
                //cargarScriptPayPal();
            }
            NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
        }
    }
    #region Botones para activar los páneles
    protected async void btn_tarjeta_Click(Object sender, EventArgs e)
    {
        NotiflixJS.Loading(this, NotiflixJS.LoadingType.loading);
        pnl_tarjeta.Visible = true;
        pnl_paypal.Visible = false;
        pnl_transferencia.Visible = false;

        btn_paypal.Enabled = false;
        btn_transferencia.Enabled = false;

        string route_id_operacion = Page.RouteData.Values["id_operacion"].ToString();
        route_id_operacion = seguridad.DesEncriptar(Page.RouteData.Values["id_operacion"].ToString());
        int idSQL = int.Parse(route_id_operacion);

        pedidos_datos pedidosDatos = null;
        pedidos_datosNumericos pedidosDatosNumericos = null;

        using (var db = new tiendaEntities())
        {
            pedidosDatos = db.pedidos_datos.Where(p => p.id == idSQL).FirstOrDefault();
            pedidosDatosNumericos = db.pedidos_datosNumericos.Where(p => p.id == idSQL).FirstOrDefault();
        }

        bool resultadoPagoBloqueado = bloquearPago(pedidosDatos, pedidosDatosNumericos);
        if (!resultadoPagoBloqueado)
        {
            await generarLinkDePagoAsync(pedidosDatos, pedidosDatosNumericos);
            NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
        }
        NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
    }
    protected void btn_paypal_Click(Object sender, EventArgs es)
    {
        NotiflixJS.Loading(this, NotiflixJS.LoadingType.loading);
        pnl_tarjeta.Visible = false;
        pnl_paypal.Visible = true;
        pnl_transferencia.Visible = false;

        btn_tarjeta.Enabled = false;
        btn_transferencia.Enabled = false;
        string redireccion = GetRouteUrl("cliente-pedido-pago-paypal", new System.Web.Routing.RouteValueDictionary
        {
            { "id_operacion", seguridad.Encriptar(hf_numero_operacion.Value) }
        });
        BootstrapCSS.RedirectJs(this, redireccion, 1000);

        //try
        //{
        //    string route_id_operacion = Page.RouteData.Values["id_operacion"].ToString();

        //    if (string.IsNullOrEmpty(route_id_operacion))
        //    {
        //        NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "No se ha recibido una operación válida");
        //        return;
        //    }
        //    route_id_operacion = seguridad.DesEncriptar(route_id_operacion);

        //    int idSQL = int.Parse(route_id_operacion);
        //    pedidos_datos pedidoDatos = null;
        //    pedidos_datosNumericos pedidoDatoNumericos = null;
        //    using (var db = new tiendaEntities())
        //    {
        //        pedidoDatos = db.pedidos_datos.Where(p => p.id == idSQL).FirstOrDefault();
        //        pedidoDatoNumericos = db.pedidos_datosNumericos.Where(p => p.id == idSQL).FirstOrDefault();
        //    }

        //    if (pedidoDatos == null)
        //    {
        //        NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "No se ha encontrado una operación");
        //        return;
        //    }

        //    string usuarioCliente = pedidoDatos.usuario_cliente;
        //    bool permisoVisualizar = privacidadAsesores.validarOperacion(usuarioCliente);

        //    if (!permisoVisualizar)
        //    {
        //        Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority));
        //    }
        //    string numero_operacion = pedidoDatos.numero_operacion;
        //    var pedidoProductos = PedidosEF.ObtenerProductos(numero_operacion);
        //    if (validarBloqueoPago(pedidoProductos, pedidoDatos, pedidoDatoNumericos))
        //    {
        //        generarBotonPagoPayPal(pedidoDatos, pedidoDatoNumericos, pedidoProductos);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Error al generar el pago de PayPal, intentar más tarde");
        //}
    }
    protected void btn_transferencia_Click(Object sender, EventArgs e)
    {
        pnl_tarjeta.Visible = false;
        pnl_paypal.Visible = false;
        pnl_transferencia.Visible = true;

        btn_tarjeta.Enabled = false;
        btn_paypal.Enabled = false;
        btn_finalizar_compra.Visible = true;
        btn_finalizar_compra.NavigateUrl = GetRouteUrl("cliente-pedido-finalizado", new System.Web.Routing.RouteValueDictionary
        {
            { "id_operacion", seguridad.Encriptar(hf_id_operacion.Value) }
        });
    }
    #endregion
    private async Task CargarDatosPedidoAsync()
    {
        try
        {
            string route_id_operacion = Page.RouteData.Values["id_operacion"].ToString();
            if (string.IsNullOrEmpty(route_id_operacion))
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "No se ha recibido una operación válida");
                return;
            }

            route_id_operacion = seguridad.DesEncriptar(Page.RouteData.Values["id_operacion"].ToString());

            int idSQL = int.Parse(route_id_operacion);
            pedidos_datos pedidosDatos = null;
            pedidos_datosNumericos pedidosDatosNumericos = null;

            using (var db = new tiendaEntities())
            {
                pedidosDatos = db.pedidos_datos.Where(p => p.id == idSQL).FirstOrDefault();
                pedidosDatosNumericos = db.pedidos_datosNumericos.Where(p => p.id == idSQL).FirstOrDefault();
            }

            if (pedidosDatos == null)
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "No se ha encontrado una operación");
                return;
            }

            string usuario_cliente = pedidosDatos.usuario_cliente;
            bool permisoVisualizar = privacidadAsesores.validarOperacion(usuario_cliente);

            if (usuarios.userLogin().tipo_de_usuario == "cliente" && usuarios.userLogin().email != pedidosDatos.usuario_cliente || !permisoVisualizar)
            {
                Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority));
            }

            string numeroOperacion = pedidosDatos.numero_operacion;
            string moneda = pedidosDatosNumericos.monedaPedido;
            string metodoEnvio = pedidosDatosNumericos.metodoEnvio;
            string envio = Math.Round(pedidosDatosNumericos.envio, 2).ToString("C2", numberFormatInfo) + " " + moneda;
            string descuento = string.IsNullOrEmpty(pedidosDatosNumericos.descuento.ToString()) ? "0.0" : pedidosDatosNumericos.descuento.ToString();
            string descuento_porcentaje = string.IsNullOrEmpty(pedidosDatosNumericos.descuento_porcentaje.ToString()) ? "0.0" : pedidosDatosNumericos.descuento_porcentaje.ToString();
            decimal subtotalProductos = Math.Round(pedidosDatosNumericos.subtotal, 2);
            decimal subtotal = Math.Round(pedidosDatosNumericos.subtotal, 2);

            if (descuento != "0.0" || descuento_porcentaje != "0.0")
            {
                subtotal -= decimal.Parse(descuento);
            }

            lbl_productos.Text = subtotalProductos.ToString("C2", numberFormatInfo) + " " + moneda;
            lbl_descuento.Text = "$" + descuento + " " + moneda;
            lbl_envio.Text = envio;
            lbl_subtotal.Text = subtotal.ToString("C2", numberFormatInfo) + " " + moneda;
            lbl_impuestos.Text = pedidosDatosNumericos.impuestos.ToString("C2", numberFormatInfo) + " " + moneda;
            lbl_total.Text = pedidosDatosNumericos.total.ToString("C2", numberFormatInfo) + " " + moneda;
            hf_id_operacion.Value = numeroOperacion;
            hf_moneda.Value = moneda;
        }
        catch (Exception ex)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Error al generar el formulario de pago");
            devNotificaciones.error("Error al generar el formulario de pago Santander (" + Page.RouteData.Values["id_operacion"].ToString() + ")", ex);
            devNotificaciones.ErrorSQL("Error al generar el formulario de pago Santander (" + Page.RouteData.Values["id_operacion"].ToString() + ")", ex, "");
            return;
        }
    }

    #region Funciones para pagar a través de la pasarela de pagos de Santander (Centro de pagos)
    protected bool bloquearPago(pedidos_datos pedidosDatos, pedidos_datosNumericos pedidosDatosNumericos)
    {
        string numeroOperacion = hf_id_operacion.Value;
        decimal envio = Math.Round(pedidosDatosNumericos.envio, 2);
        Decimal tipoCambioActual = operacionesConfiguraciones.obtenerTipoDeCambio();
        List<pedidos_pagos_paypal> historialPagosPaypal = PayPalTienda.obtenerPagos(numeroOperacion);
        List<pedidos_pagos_respuesta_santander> historialPagosSantander = SantanderResponse.ObtenerTodos(numeroOperacion);
        historialPagosSantander = historialPagosSantander.Where(p => p.estatus == "approved").ToList();

        if (historialPagosPaypal.Count >= 1)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.info, "Ya se encuentra un intento de pago en PayPal");
            NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
            return true;
        }

        if (historialPagosSantander.Count >= 1)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.info, "Ya se encuentra un intento de pago por tarjeta");
            NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
            return true;
        }

        if (pedidosDatosNumericos.monedaPedido == "USD")
        {
            bool vigenciaSuperada = utilidad_fechas.calcularDiferenciaDias(pedidosDatos.fecha_creacion) > diasVigenciaPedidoUSD;
            if (vigenciaSuperada)
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.info, "Vigencia superada");
                NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
                btn_renovarPedidoSantanderContenedor.Visible = true;
                return true;
            }
        }
        else
        {
            bool vigenciaSuperada = utilidad_fechas.calcularDiferenciaDias(pedidosDatos.fecha_creacion) > diasVigenciaPedidoMXN;
            if (vigenciaSuperada)
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.info, "Vigencia superada");
                NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
                btn_renovarPedidoSantanderContenedor.Visible = true;
                return true;
            }
        }

        if (tipoCambioActual > pedidosDatosNumericos.tipo_cambio)
        {
            decimal diferenciaTipoDeCambio = tipoCambioActual - pedidosDatosNumericos.tipo_cambio;

            if (diferenciaTipoDeCambio > limiteDiferenciaTipoDeCambio)
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.info, "El tipo de cambio se ha actualizado");
                NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
                btn_renovarPedidoSantanderContenedor.Visible = true;
                return true;
            }
        }

        switch (pedidosDatosNumericos.metodoEnvio)
        {
            case "En Tienda":
                break;
            case "Gratuito":
                break;
            case "Estándar":
                if (envio <= 0)
                {
                    NotiflixJS.Message(this, NotiflixJS.MessageType.info, "No se ha establecido el costo de envio");
                    NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
                    return true;
                }
                break;
            case "Ninguno":
                NotiflixJS.Message(this, NotiflixJS.MessageType.info, "No se ha establecido el costo de envio");
                NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
                break;
        }
        return false;
    }
    private async Task generarLinkDePagoAsync(pedidos_datos pedidosDatos, pedidos_datosNumericos pedidosDatosNumericos)
    {
        try
        {
            int diasVigencia = hf_moneda.Value == "MXN" ? diasVigenciaPedidoMXN : diasVigenciaPedidoUSD;

            using (var db = new tiendaEntities())
            {
                #region Si la liga ha sido utilizada, se genera una nueva.
                var intentoPago = db.pedidos_pagos_respuesta_santander
                    .Where(r => r.numero_operacion == pedidosDatos.numero_operacion && r.estatus == "denied")
                    .AsNoTracking()
                    .OrderByDescending(r => r.fecha_primerIntento)
                    .FirstOrDefault();

                if (intentoPago != null)
                {
                    var result = await crearLinkSantander(pedidosDatos, pedidosDatosNumericos, diasVigencia);
                    if (result != null)
                    {
                        cargarPasarela(result.URL);
                        return;
                    }
                    else
                    {
                        NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Ocurrió un error al generar un nuevo pago");
                        NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
                    }
                }
                #endregion

                var links = db.pedidos_pagos_liga_santander
                    .Where(l => l.numero_operacion == pedidosDatos.numero_operacion)
                    .OrderByDescending(l => l.fecha_creacion)
                    .AsNoTracking()
                    .FirstOrDefault();

                if (links != null)
                {
                    bool vigenciaSuperada = true;
                    if (hf_moneda.Value == "MXN")
                    {
                        vigenciaSuperada = utilidad_fechas.calcularDiferenciaDias(links.fecha_creacion) > diasVigenciaPedidoMXN;
                    }
                    else
                    {
                        vigenciaSuperada = utilidad_fechas.calcularDiferenciaDias(links.fecha_creacion) > diasVigenciaPedidoUSD;
                    }

                    if (vigenciaSuperada == false && links.monto == pedidosDatosNumericos.total)
                    {
                        cargarPasarela(links.liga);
                    }
                    else
                    {
                        var result = await crearLinkSantander(pedidosDatos, pedidosDatosNumericos, diasVigencia);
                        if (result != null)
                        {
                            cargarPasarela(result.URL);
                            return;
                        }
                        else
                        {
                            NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Ocurrió un error al crear un nuevo pago");
                            NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
                        }
                    }
                }
                else
                {
                    var result = await crearLinkSantander(pedidosDatos, pedidosDatosNumericos, diasVigencia);
                    if (result != null)
                    {
                        cargarPasarela(result.URL);
                        return;
                    }
                    else
                    {
                        NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Ocurrió un error 3");
                    }
                    NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
                }
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.ErrorSQL("Generar link Santander", ex, pedidosDatos.numero_operacion);
            devNotificaciones.notificacionSimple(ex.ToString());
            NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Ocurrió un error 4");
            NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
        }
    }
    protected async Task<SantanderLigaCobro> crearLinkSantander(pedidos_datos pedidoDatos, pedidos_datosNumericos pedidoDatosNumericos, int diasVigencia)
    {
        SantanderLigaCobro link = new SantanderLigaCobro(pedidoDatos.numero_operacion, pedidoDatosNumericos.total, hf_moneda.Value, null, utilidad_fechas.obtenerCentral().AddDays(diasVigencia), pedidoDatos.usuario_cliente);
        await link.GenerarLigaAsync();

        if (link.URL != null)
        {
            try
            {
                using (var db = new tiendaEntities())
                {
                    db.pedidos_pagos_liga_santander.Add(new pedidos_pagos_liga_santander()
                    {
                        fecha_creacion = utilidad_fechas.obtenerCentral(),
                        fecha_vigencia = utilidad_fechas.obtenerCentral().AddDays(diasVigencia),
                        liga = link.URL,
                        numero_operacion = pedidoDatos.numero_operacion
                    });
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                devNotificaciones.ErrorSQL("Generar liga Santander: ", ex, pedidoDatos.numero_operacion);
            }
            return link;
        }
        else
        {
            return null;
        }
    }
    protected void cargarPasarela(string URL)
    {
        frm_pagoTarjeta.Attributes.Add("src", URL);
        frm_pagoTarjeta.Visible = true;
        pnl_tarjeta.Visible = true;
        up_pasarelaPago.Update();
    }
    protected void btn_renovarPedidoSantander_Click(object sender, EventArgs e)
    {
        Tuple<bool, List<string>> resultado = pedidosProductos.renovarPedido(hf_id_operacion.Value);
        if (resultado.Item1)
        {
            NotiflixJS.Message(up_pasarelaPago, NotiflixJS.MessageType.success, "Pedido actualizado");
            string redireccionUrl = GetRouteUrl("cliente-pedido-pago", new System.Web.Routing.RouteValueDictionary
        {
            { "id_operacion", seguridad.Encriptar(hf_numero_operacion.Value) }
        });
            BootstrapCSS.RedirectJs(this, redireccionUrl, 2000);
        }
        else
        {
            NotiflixJS.Message(up_pasarelaPago, NotiflixJS.MessageType.failure, "Error al actualizar");
        }
    }
    #endregion

    #region Funciones para pagar a través de PayPal
    protected void cargarScriptPayPal()
    {
        HtmlGenericControl scriptTag = new HtmlGenericControl("script");
        string cliendID = "";
        string host = HttpContext.Current.Request.Url.Host;
        if (host == "localhost" || host == "test1.incom.mx")
        {
            cliendID = PayPalClient.client_id_Sandbox;
        }
        else
        {
            cliendID = PayPalClient.client_id_Productivo;
        }
        scriptTag.Attributes.Add("type", "text/javascript");
        scriptTag.Attributes.Add("src", "https://www.paypal.com/sdk/js?client-id=" + cliendID + "&currency=" + hf_moneda.Value);
        Page.Header.Controls.Add(scriptTag);
        up_paypal.Update();
    }
    protected void generarBotonPagoPayPal(pedidos_datos datos, pedidos_datosNumericos datosNumericos, List<pedidos_productos> productos)
    {
        string numero_operacion = lbl_numero_pedido.Text;
        var direccionEnvio = PedidosEF.ObtenerDireccionEnvio(numero_operacion);

        StringBuilder json = new StringBuilder();

        //string scriptInit = @" paypal.Buttons({ 
        //                        createOrder: function (data, actions) { 
        //                            return actions.order.create ({ 
        //                                'intent': 'CAPTURE', 
        //                                'application_context': { 
        //                                    'shipping_preference': 'tipo_envio' 
        //                                }, 
        //                                'purchase_units': [{";
        string scriptInit = @"  paypal.Buttons({
            createOrder: function(data, actions) {
                return actions.order.create({
                    'intent': 'CAPTURE',
                    'application_context' : {
                    'shipping_preference': '{tipo_envio}'
                     },
                    'purchase_units': [{";

        switch (datosNumericos.metodoEnvio.ToUpper())
        {
            case "EN TIENDA":
                json.Append(scriptInit.Replace("{tipo_envio}", "NO_SHIPPING"));
                break;
            case "GRATUITO":
            case "ESTÁNDAR":
                json.Append(scriptInit.Replace("{tipo_envio}", "SET_PROVIDED_ADDRESS"));
                json.Append(procesarDireccionEnvio(direccionEnvio.response, datos.cliente_nombre + " " + datos.cliente_apellido_paterno));
                break;
            case "NINGUNO":
                json.Append(scriptInit.Replace("{tipo_envio}", "NO_SHIPPING"));
                break;
        }

        //json.Append(@" 'amount': { 
        //                    'currency_code': '" + datosNumericos.monedaPedido + @"',
        //                    'value': '" + Math.Round((datosNumericos.subtotal - datosNumericos.envio) + datosNumericos.impuestos + datosNumericos.envio, 2) + @"',
        //                    'breakdown': {
        //                        'item_total': {
        //                            'currency_code': '" + datosNumericos.monedaPedido + @"',
        //                            'value': '" + decimal.Round((datosNumericos.subtotal - datosNumericos.envio), 2) + @"',
        //                        },
        //                    'shipping': {
        //                        'currency_code': '" + datosNumericos.monedaPedido + @"',
        //                        'value': '" + decimal.Round(datosNumericos.envio, 2) + @"'
        //                    },
        //                    'tax_total': {
        //                        'currency_code': '" + datosNumericos.monedaPedido + @"',
        //                        'value': '" + decimal.Round(datosNumericos.impuestos, 2) + @"'
        //                    },
        //                }
        //            },");
        json.Append(@"

                   'amount' : {
                    'currency_code' : '" + datosNumericos.monedaPedido + @"',
                            'value': '" + Math.Round((datosNumericos.subtotal - datosNumericos.envio) + datosNumericos.impuestos + datosNumericos.envio, 2) + @"',
                            'breakdown': {
                   'item_total': {
                   'currency_code' : '" + datosNumericos.monedaPedido + @"',
                                    'value':  '" + decimal.Round((datosNumericos.subtotal - datosNumericos.envio), 2) + @"',
                                },
                    'shipping': {
                    'currency_code': '" + datosNumericos.monedaPedido + @"',
                                    'value': '" + decimal.Round(datosNumericos.envio, 2) + @"'
                                },
                                'tax_total': {
                    'currency_code' : '" + datosNumericos.monedaPedido + @"',
                                    'value': '" + decimal.Round(datosNumericos.impuestos, 2) + @"'
                                },
                            }

        },");

        //json.Append(@" 'items': [ ");
        json.Append(@" 'items': [ ");

        foreach (var producto in productos)
        {
            //string sku = producto.numero_parte;
            //string name = producto.descripcion;
            //name = name.Replace("'", "ft").Replace("\"", "in");
            //string description = "";
            //string unit_amount = decimal.Round(producto.precio_unitario, 2).ToString();
            //string quantity = decimal.Round(producto.cantidad, 0).ToString();

            //if (name.Length >= 127)
            //{
            //    name = name.Substring(0, 127);
            //}

            //description = name;
            string sku = producto.numero_parte;
            string name = producto.descripcion;
            name = name.Replace("'", "ft").Replace("\"", "in");

            if (name.Length >= 127) { name = name.Substring(0, 127); }

            string description = name;
            string unit_amount = decimal.Round(producto.precio_unitario, 2).ToString();
            string quantity = decimal.Round(producto.cantidad, 0).ToString();

            //json.Append(@"{ 
            //                'name': '" + name + @"',
            //                'sku': '" + sku + @"',
            //                'description': '" + description + @"',
            //                'unit_amount': {
            //                    'currency_code': '" + datosNumericos.monedaPedido + @"',
            //                    'value': '" + unit_amount + @"'
            //                },
            //                'quantity': '" + quantity + @"',
            //                'category': 'PHYSICAL_GOODS'
            //            },");
            json.Append(@"{     'name' : '" + name + @"',
                                'sku' : '" + sku + @"',
                                'description': '" + description + @"',
                                'unit_amount' : {
                                'currency_code' : '" + datosNumericos.monedaPedido + @"',
                                    'value' : '" + unit_amount + @"'
                                },
                               'quantity': '" + quantity + @"',
                                'category' : 'PHYSICAL_GOODS'
                         },");
        }

        //json.Append(@" ], ");
        //json.Append(@"'invoice_id' : '" + numero_operacion + @"', ");
        //json.Append(@"'description': '" + datos.nombre_pedido + @"', ");
        //json.Append(@"'soft_descriptor' : 'PD " + numero_operacion + @"' ");
        json.Append(@"     ], ");
        json.Append(@" 'invoice_id' : '" + numero_operacion + @"', ");
        json.Append(@"  'description': '" + datos.nombre_pedido + @"', ");
        json.Append(@" 'soft_descriptor' : 'PD " + numero_operacion + @"' ");
        //json.Append(@" }]
        //                });
        //                },
        //                onClick: function() {},
        //                onCancel: function (data) {},
        //                onApprove: function(data, actions) {
        //                    return actions.order.capture().then(function(details) {
        //                fetch('/usuario/mi-cuenta/procesar-pago.ashx', {
        //                    method: 'post',
        //                    headers: {
        //                        'content-type': 'application/json'
        //                    },
        //                    credentials: 'same-origin'  ,
        //                    body: JSON.stringify({
        //                        idTransacciónPayPal: data.orderID,
        //                        numero_operacion: '" + numero_operacion + @"' 
        //                    })
        //                });
        //                setTimeout(function(){
        //                    document.getElementById('" + linkActualizarUP.ClientID + @"').click();
        //                }, 1000);
        //            });
        //        }
        //    }).render('.paypal_button_container');");
        json.Append(@" 
                    }]
                });
            },


             // onClick is called when the button is clicked
                onClick: function() {

          
                },

             onCancel: function (data) {
                
                
              },



            onApprove: function(data, actions) {
                return actions.order.capture().then(function(details) {
                  
             
                    console.log(data.orderID);
                    console.log(data);
                    console.log(details);
                    // Call your server to save the transaction}
 
                      fetch('/usuario/mi-cuenta/procesar-pago.ashx', {
                        method: 'post',
                        headers: {
                            'content-type': 'application/json'
                        },
                        credentials: 'same-origin'  ,
                        body: JSON.stringify({
                            idTransacciónPayPal: data.orderID,
                            numero_operacion: '" + numero_operacion + @"' 
                        })

                      

                    });
                document.querySelector('#texto_cargando_informacion').classList.remove('d-none');
                BootstrapAlert('#content_msg_bootstrap', 'success', 'Pago realizado', 'Pago realizado con éxito');


            setTimeout(function(){     document.getElementById('" + linkActualizarUP.ClientID + @"').click();   },5000);
 
                });
            }
        }).render('.paypal_button_container'); ");

        //cargarScriptPayPal(datosNumericos.monedaPedido);
        //cargarScriptPayPal();
        ClientScriptManager cs = Page.ClientScript;
        Type csType = this.GetType();
        cs.RegisterStartupScript(csType, "PayPalButton", json.ToString(), true);
        paypal_button_container.Visible = true;

        NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
        up_paypal.Update();
    }
    protected void limpiarCamposPagoPaypal()
    {
        lbl_paypal_intento.Text = "";
        lbl_paypal_estado.Text = "";
        lbl_paypal_monto.Text = "";
        lbl_paypal_moneda.Text = "";
        lbl_paypal_fecha_primerIntento.Text = "";
        lbl_paypal_fecha_actualizacion.Text = "";
    }
    protected void btn_renovarPedidoPayPal_Click(object sender, EventArgs e)
    {
        Tuple<bool, List<string>> resultado = pedidosProductos.renovarPedido(hf_numero_operacion.Value);

        if (resultado.Item1)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.success, "Pedido actualizado con éxito");
            string script = @" setTimeout(() => { location.reload(); }, 1000)";
            ScriptManager.RegisterStartupScript(up_paypal, typeof(Page), "redirección", script, true);
        }
        else
        {
            NotiflixJS.Message(up_paypal, NotiflixJS.MessageType.failure, "Error al actualizar algunos productos: " + resultado.Item2);
        }
    }
    protected void mostrarEstadoPayPal(List<pedidos_pagos_paypal> historialPagos)
    {
        paypal_button_container.Visible = true;
        dt_desglose_paypal.Visible = true;
        lbl_paypal_intento.Text = historialPagos[0].intento;
        lbl_paypal_estado.Text = historialPagos[0].estado;
        lbl_paypal_monto.Text = decimal.Parse(historialPagos[0].monto).ToString("C2", numberFormatInfo);
        lbl_paypal_moneda.Text = historialPagos[0].moneda;
        lbl_paypal_fecha_primerIntento.Text = String.Format("{0:F}", historialPagos[0].fecha_primerIntento);
        lbl_paypal_fecha_actualizacion.Text = String.Format("{0:F}", historialPagos[0].fecha_actualización);

        up_paypal.Update();
    }
    protected string procesarDireccionEnvio(pedidos_direccionEnvio direccionEnvio, string nombreCompleto)
    {
        string addressLine1 = direccionEnvio.calle + ", " + direccionEnvio.numero + ", " + direccionEnvio.colonia;
        string addressLine2 = direccionEnvio.numero;
        string adminArea1 = direccionEnvio.delegacion_municipio;
        string adminArea2 = direccionEnvio.estado;
        string postalCode = direccionEnvio.codigo_postal;
        string countryCode = paises.obtenerCódigoPais(direccionEnvio.pais);
        if (addressLine1.Length > 300)
        {
            addressLine1 = addressLine1.Substring(0, 300);
        }
        string envio = (@"'shipping': {
            'name': {
                'full_name': '" + nombreCompleto + @"'
                            },
                            'address': {
                                'address_line_1': '" + addressLine1 + @"',
                                'address_line_2': '" + addressLine2 + @"',
                                'admin_area_2': '" + adminArea2 + @"',
                                'admin_area_1': '" + adminArea1 + @"',
                                'postal_code': '" + postalCode + @"',
                                'country_code': '" + countryCode + @"'
                            },
                        },");

        return envio;
    }
    protected bool validarBloqueoPago(List<pedidos_productos> productos, pedidos_datos datos, pedidos_datosNumericos datosNumericos)
    {
        bool pedidoAprobadoPorAsesor = false;
        bool pagoRealizado = false;
        bool montoSuperiorParaEnvioGratuito = false;
        bool fechaPermitidaPago = false;
        bool precioEnvioEstablecido = false;
        bool pedidoElegibleParaEnvioGratuito = true;
        bool direccionEnvioCompleta = false;
        bool diferenciaTipoDeCambio = false;
        bool permitirPago = false;
        string numero_operacion = lbl_numero_pedido.Text;
        int idSQL = int.Parse(hf_numero_operacion.Value);
        decimal envio = Math.Round(datosNumericos.envio, MidpointRounding.ToEven);
        decimal tipoDeCambioPedido = datosNumericos.tipo_cambio;
        string metodoEnvio = datosNumericos.metodoEnvio;
        string monedaPedido = datosNumericos.monedaPedido;
        decimal totalPedido = Math.Round(datosNumericos.total, MidpointRounding.ToEven);
        DateTime fechaPedido = datos.fecha_creacion;
        Decimal tipoCambioActual = operacionesConfiguraciones.obtenerTipoDeCambio();
        string productosNoDisponiblesParaEnvioGratis = "";
        List<pedidos_pagos_respuesta_santander> historialPagosSantander = SantanderResponse.ObtenerTodos(numero_operacion);


        historialPagosSantander = historialPagosSantander.Where(p => p.estatus == "approved").ToList();
        if (historialPagosSantander.Count >= 1)
        {
            motivosNoDisponiblePago.Visible = true;
            pnl_noDisponiblePago.Visible = true;
            motivosNoDisponiblePago.InnerHtml += " Se encuentra un pago por medio de tarjeta";
            return false;
        }

        List<pedidos_pagos_paypal> historialPagosPayPal = PayPalTienda.obtenerPagos(numero_operacion);
        limpiarCamposPagoPaypal();

        if (historialPagosPayPal == null || historialPagosPayPal.Count < 1)
        {
            pagoRealizado = false;
            dt_desglose_paypal.Visible = false;
        }
        else
        {
            dt_desglose_paypal.Visible = true;
            pagoRealizado = true;
            mostrarEstadoPayPal(historialPagosPayPal);
            return false;
        }

        if (monedaPedido == "MXN")
        {
            if (totalPedido > montoMinimoPedidoEnvioGratuito)
            {
                montoSuperiorParaEnvioGratuito = true;
            }
        }
        else
        {
            decimal totalMXN = totalPedido * tipoCambioActual;
            if (totalMXN > montoMinimoPedidoEnvioGratuito)
            {
                montoSuperiorParaEnvioGratuito = true;
            }
        }

        if (monedaPedido == "USD")
        {
            fechaPermitidaPago = utilidad_fechas.calcularDiferenciaDias(fechaPedido) < diasVigenciaPedidoUSD;
        }
        else
        {
            fechaPermitidaPago = utilidad_fechas.calcularDiferenciaDias(fechaPedido) < diasVigenciaPedidoUSD;
        }
        if (!fechaPermitidaPago)
        {
            btn_renovarPedidoPayPal.Visible = true;
        }

        if (tipoCambioActual > tipoDeCambioPedido)
        {
            decimal diferenciaTipoDeCambioPayPal = tipoCambioActual - tipoDeCambioPedido;
            if (diferenciaTipoDeCambioPayPal > limiteDiferenciaTipoDeCambio)
            {
                btn_renovarPedidoPayPal.Visible = true;
                motivosNoDisponiblePago.Visible = true;
                motivosNoDisponiblePago.InnerHtml += " El tipo de cambio del pedido ha cambiado, renueva tu pedido";
                return false;
            }
        }

        if (envio > 0)
        {
            precioEnvioEstablecido = true;
        }

        foreach (var producto in productos)
        {
            string numero_parte = producto.numero_parte;
            bool resultado = productosTienda.productoDisponibleEnvio(numero_parte);
            if (resultado == false)
            {
                productosNoDisponiblesParaEnvioGratis += numero_parte + ", ";
                pedidoElegibleParaEnvioGratuito = false;
            }
        }
        productosNoDisponiblesParaEnvioGratis = productosNoDisponiblesParaEnvioGratis.TrimEnd(' ');
        productosNoDisponiblesParaEnvioGratis = productosNoDisponiblesParaEnvioGratis.TrimEnd(',');

        bool validarEnvio = false;

        switch (metodoEnvio)
        {
            case "En Tienda":
                permitirPago = true;
                pedidoElegibleParaEnvioGratuito = true;
                break;
            case "Gratuito":
                validarEnvio = true;
                pedidoElegibleParaEnvioGratuito = true;
                break;
            case "Estándar":
                validarEnvio = true;
                break;
            case "Ninguno":
                //motivosNoDisponiblePago.Visible = true;
                //pnl_noDisponiblePago.Visible = true;
                //motivosNoDisponiblePago.InnerHtml += "- No se ha establecido ninguna condición de envío. <br>";
                NotiflixJS.Message(this, NotiflixJS.MessageType.warning, "No se ha establecido ninguna condición de envío");
                return false;
        }

        if (validarEnvio)
        {
            model_direccionesEnvio direccionEnvio = pedidosDatos.obtenerPedidoDireccionEnvioStatic(numero_operacion);
            Tuple<bool, string> resultadoValidacion = validarCampos.direccionEnvioCompleta(direccionEnvio);

            if (resultadoValidacion.Item1)
            {
                permitirPago = true;
                direccionEnvioCompleta = true;
            }
            else
            {
                permitirPago = false;
                motivosNoDisponiblePago.Visible = true;
                pnl_noDisponiblePago.Visible = true;
                motivosNoDisponiblePago.InnerHtml += " La dirección de envio no está completa, faltan los siguientes datos: " + resultadoValidacion.Item2 + "<br/>";
                direccionEnvioCompleta = false;
            }
        }

        if (validarEnvio)
        {
            if (montoSuperiorParaEnvioGratuito == false && precioEnvioEstablecido == false && pedidoElegibleParaEnvioGratuito == false && direccionEnvioCompleta)
            {
                motivosNoDisponiblePago.Visible = true;
                pnl_noDisponiblePago.Visible = true;
                motivosNoDisponiblePago.InnerHtml += " El pedido no supera la cantidad de $" + montoMinimoPedidoEnvioGratuito + " para el envio gratuito. <br/>";
                motivosNoDisponiblePago.InnerHtml += "El asesor aún no establece el precio de envio. <br/>";
                permitirPago = false;
            }
        }
        if (fechaPermitidaPago == false)
        {
            motivosNoDisponiblePago.Visible = true;
            pnl_noDisponiblePago.Visible = true;
            motivosNoDisponiblePago.InnerHtml += " La vigencia de los precios y fecha de pago ha vencido. <br/>";
            permitirPago = false;
        }
        if (validarEnvio)
        {
            if (pedidoElegibleParaEnvioGratuito == false && montoSuperiorParaEnvioGratuito == true && precioEnvioEstablecido == false && direccionEnvioCompleta)
            {
                motivosNoDisponiblePago.Visible = true;
                pnl_noDisponiblePago.Visible = true;
                motivosNoDisponiblePago.InnerHtml += " Contiene los siguientes productos que no aplican al envio gratuito: " + productosNoDisponiblesParaEnvioGratis + "<br/>";
                motivosNoDisponiblePago.InnerHtml += " El asesor aún no establece el precio de envio.<br/>";
                permitirPago = false;
            }
        }
        if (validarEnvio)
        {
            if (montoSuperiorParaEnvioGratuito && fechaPermitidaPago && pedidoElegibleParaEnvioGratuito && direccionEnvioCompleta)
            {
                permitirPago = true;
            }
        }
        if (validarEnvio == true)
        {
            if (precioEnvioEstablecido && fechaPermitidaPago && direccionEnvioCompleta)
            {
                permitirPago = true;
            }
        }
        else
        {
            if (fechaPermitidaPago)
            {
                permitirPago = true;
            }
        }
        return permitirPago;
    }
    protected void linkActualizarUP_Click(object sender, EventArgs e)
    {
        string numero_operacion = hf_numero_operacion.Value;
        var pedidoDatos = PedidosEF.ObtenerDatos(numero_operacion);
        var pedidoDatosNumericos = PedidosEF.ObtenerNumeros(numero_operacion);
        var pedidoProductos = PedidosEF.ObtenerProductos(numero_operacion);

        bool bloqueado = validarBloqueoPago(pedidoProductos, pedidoDatos, pedidoDatosNumericos);

        if (bloqueado)
        {
            generarBotonPagoPayPal(pedidoDatos, pedidoDatosNumericos, pedidoProductos);
        }
    }
    #endregion
}
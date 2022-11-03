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

public partial class usuario_cliente_pago : System.Web.UI.Page
{
    NumberFormatInfo numberFormatInfo = new CultureInfo("es-MX", true).NumberFormat;
    int diasVigenciaPedidoUSD = 30;
    int diasVigenciaPedidoMXN = 5;
    decimal limiteDiferenciaTipoDeCambio = (decimal)0.5;
    protected async void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Title = "Método de pago";
            if (Page.RouteData.Values["id_operacion"].ToString() != null)
            {
                await CargarDatosPedidoAsync();
                string route_id_operacion = Page.RouteData.Values["id_operacion"].ToString();
                route_id_operacion = seguridad.DesEncriptar(route_id_operacion);
                lbl_numero_pedido.Text = PedidosEF.ObtenerNumeroOperacion(int.Parse(route_id_operacion));

            }
        }
    }
    #region Botones para activar los páneles
    protected void btn_tarjeta_Click(Object sender, EventArgs e)
    {
        pnl_tarjeta.Visible = true;
        pnl_paypal.Visible = false;
        pnl_transferencia.Visible = false;
    }
    protected void btn_paypal_Click(Object sender, EventArgs es)
    {
        pnl_tarjeta.Visible = false;
        pnl_paypal.Visible = true;
        pnl_transferencia.Visible = false;
    }
    protected void btn_transferencia_Click(Object sender, EventArgs e)
    {
        pnl_tarjeta.Visible = false;
        pnl_paypal.Visible = false;
        pnl_transferencia.Visible = true;
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

            bool resultadoPagoBloqueado = bloquearPago(pedidosDatos, pedidosDatosNumericos);
            if (!resultadoPagoBloqueado)
            {
                await generarLinkDePagoAsync(pedidosDatos, pedidosDatosNumericos);
            }
        }
        catch (Exception ex)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Error al generar el formulario de pago");
            devNotificaciones.error("Error al generar el formulario de pago Santander (" + Page.RouteData.Values["id_operacion"].ToString() + ")", ex);
            devNotificaciones.ErrorSQL("Error al generar el formulario de pago Santander (" + Page.RouteData.Values["id_operacion"].ToString() + ")", ex, "");
            return;
        }
    }
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
            return true;
        }

        if (historialPagosSantander.Count >= 1)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.info, "Ya se encuentra un intento de pago por tarjeta");
            return true;
        }

        if (pedidosDatosNumericos.monedaPedido == "USD")
        {
            bool vigenciaSuperada = utilidad_fechas.calcularDiferenciaDias(pedidosDatos.fecha_creacion) > diasVigenciaPedidoUSD;
            if (vigenciaSuperada)
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.info, "Vigencia superada");
                return true;
            }
        }
        else
        {
            bool vigenciaSuperada = utilidad_fechas.calcularDiferenciaDias(pedidosDatos.fecha_creacion) > diasVigenciaPedidoMXN;
            if (vigenciaSuperada)
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.info, "Vigencia superada");
            }
        }

        if (tipoCambioActual > pedidosDatosNumericos.tipo_cambio)
        {
            decimal diferenciaTipoDeCambio = tipoCambioActual - pedidosDatosNumericos.tipo_cambio;

            if (diferenciaTipoDeCambio > limiteDiferenciaTipoDeCambio)
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.info, "El tipo de cambio se ha actualizado");
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
                    return true;
                }
                break;
            case "Ninguno":
                NotiflixJS.Message(this, NotiflixJS.MessageType.info, "No se ha establecido el costo de envio");
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
                        NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Ocurrió un error 1");
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
                            NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Ocurrió un error 2");
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
                }
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.ErrorSQL("Generar link Santander", ex, pedidosDatos.numero_operacion);
            devNotificaciones.notificacionSimple(ex.ToString());
            NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Ocurrió un error 4");
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
        up_pasarelaPago.Update();
    }
}
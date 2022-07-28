using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usuario_mi_cuenta_pedido_pago_santander : System.Web.UI.Page
{
    NumberFormatInfo myNumberFormatInfo = new CultureInfo("es-MX", true).NumberFormat;
    int diasVigenciaPedidoPagoUSD = 30;
    int diasVigenciaPedidoPagoMXN = 5;
    decimal limiteDiferenciaTipoDeCambio = (decimal)0.5;
    protected void Page_Load(object sender, EventArgs e)
    {

      //  Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri.Replace("3DSecure/", ""));
        if (!IsPostBack)
        {
            CargarDatosOperación();

          
        }
    }

    private  async void CargarLigaDePago(pedidos_datos PedidoDatos, pedidos_datosNumericos PedidoDatosNumericos)
    {
        try
        {
            int DiasVigencia = lbl_moneda.Text == "MXN" ? diasVigenciaPedidoPagoMXN : diasVigenciaPedidoPagoMXN;

            using (var db = new tiendaEntities())
            {


                #region Si la liga ya ha sido intentado pagarse y ha sido errónea, se genera una nueva
                var IntentoPago = db.pedidos_pagos_respuesta_santander
                     .Where(l => l.numero_operacion == PedidoDatos.numero_operacion && l.estatus == "denied")
                     .AsNoTracking()
                     .OrderByDescending(l => l.fecha_primerIntento)
                     .FirstOrDefault();

                if(IntentoPago != null) {

                    var Result = await CrearLigaSantadander(PedidoDatos, PedidoDatosNumericos, DiasVigencia);
                    if (Result != null) {
                        CargarIframe(Result.URL);
                    }

                }
                else // Si la liga fue null
                        {
                    motivosNoDisponiblePago.Visible = true; lbl_NoDisponiblePago.Visible = true;
                    btn_renovarPedido.Visible = true;
                    motivosNoDisponiblePago.InnerHtml += "- Ocurrió un error, el asesor ya fué notificado y en unos momentos se contactará contigo.";
                }
                #endregion


                var ligas = db.pedidos_pagos_liga_santander
                     .Where(l => l.numero_operacion == PedidoDatos.numero_operacion)
                     .AsNoTracking()
                     .OrderByDescending(l => l.fecha_creacion)    
                     .FirstOrDefault();

                if (ligas != null)
                {
               
                    bool VigenciaSuperada = true;

                    if (lbl_moneda.Text == "MXN") VigenciaSuperada = utilidad_fechas.calcularDiferenciaDias(ligas.fecha_creacion) > diasVigenciaPedidoPagoMXN;
                    else VigenciaSuperada = utilidad_fechas.calcularDiferenciaDias(ligas.fecha_creacion) > diasVigenciaPedidoPagoUSD;

                    if (VigenciaSuperada == false)
                    {

                        CargarIframe(ligas.liga);
                    }

                    else  // Si la liga ya perdió su vigencia, generamos una nueva y la guardamos
                    {

                        /* SantanderData3ds Data3DS = new SantanderData3ds(PedidoDatos.email, 5559514677, "317, 935",
                                           "Ciudad de México", "CX", "07420"); */

                        var Result = await CrearLigaSantadander(PedidoDatos, PedidoDatosNumericos, DiasVigencia);
                        if (Result != null) {
                            CargarIframe(Result.URL);
                            return;
                        }
                        else // Si la liga fue null
                        {
                            motivosNoDisponiblePago.Visible = true; lbl_NoDisponiblePago.Visible = true;
                            btn_renovarPedido.Visible = true;
                            motivosNoDisponiblePago.InnerHtml += "- Ocurrió un error, el asesor ya fué notificado y en unos momentos se contactará contigo.";
                        }

                    }
                }
                else
                {

                    var Result = await CrearLigaSantadander(PedidoDatos, PedidoDatosNumericos, DiasVigencia);
                    if(Result != null) {
                        CargarIframe(Result.URL);
                        return;
                    }
                    else // Si la liga fue null
                        {
                        motivosNoDisponiblePago.Visible = true; lbl_NoDisponiblePago.Visible = true;
                        btn_renovarPedido.Visible = true;
                        motivosNoDisponiblePago.InnerHtml += "- Ocurrió un error, el asesor ya fué notificado y en unos momentos se contactará contigo.";
                    }


                }
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.ErrorSQL("Generar liga Santander", ex, PedidoDatos.numero_operacion);
            devNotificaciones.notificacionSimple( ex.ToString());
          
            motivosNoDisponiblePago.Visible = true; lbl_NoDisponiblePago.Visible = true;
            btn_renovarPedido.Visible = true;
            motivosNoDisponiblePago.InnerHtml += "- Ocurrió un error, el asesor ya fué notificado y en unos momentos se contactará contigo.";

        }
    }
    protected   async Task<SantanderLigaCobro> CrearLigaSantadander(pedidos_datos PedidoDatos, pedidos_datosNumericos PedidoDatosNumericos, int DiasVigencia) {
       
        
        SantanderLigaCobro liga =
                           new SantanderLigaCobro(PedidoDatos.numero_operacion, PedidoDatosNumericos.total, lbl_moneda.Text, null,
                          utilidad_fechas.obtenerCentral().AddDays(DiasVigencia),
                           PedidoDatos.usuario_cliente
                           );
        await  liga.GenerarLigaAsync();


        if (liga.URL != null) {


            try {
                using (var db = new tiendaEntities()) {
                    // La guardamos
                    db.pedidos_pagos_liga_santander.Add(new pedidos_pagos_liga_santander() {
                        fecha_creacion = utilidad_fechas.obtenerCentral(),
                        fecha_vigencia = utilidad_fechas.obtenerCentral().AddDays(DiasVigencia),
                        liga = liga.URL,
                        numero_operacion = PedidoDatos.numero_operacion

                    });
                    db.SaveChanges();
                }
            }
            catch (Exception ex) {
                devNotificaciones.ErrorSQL("Generar liga Santander: ", ex, PedidoDatos.numero_operacion);
                devNotificaciones.notificacionSimple(ex.ToString());
            }
            finally {
               
            }
            return liga;
        }
        else {
            return null;
        }
        
    }
    protected void CargarIframe(string URL)
    {
        FramePago.Attributes.Add("src", URL);
        FramePago.Visible = true;
        up_estatus_Santander.Update();
    }
    private void CargarDatosOperación()
    {
        if (Page.RouteData.Values["id_operacion"] == null) {
            materializeCSS.crear_toast(this, "No se ha recibido una operación válida", false);
            return;
        }


        try
        {
            string route_id_operacion = Page.RouteData.Values["id_operacion"].ToString();

            if (string.IsNullOrEmpty(route_id_operacion))
            {
                materializeCSS.crear_toast(this, "No se ha recibido una operación válida", false);
                return;
            }
            route_id_operacion = seguridad.DesEncriptar(Page.RouteData.Values["id_operacion"].ToString());


            int idSQL = int.Parse(route_id_operacion);


            pedidos_datos pedidoDatos = null;
            using (var db = new tiendaEntities())
            {
                pedidoDatos = db.pedidos_datos
                     .Where(l => l.id == idSQL)
                     .FirstOrDefault();
            }
         

 
            if(pedidoDatos == null) {
                materializeCSS.crear_toast(this, "No se ha encontrado una operación", false);
                return;
            }

            pedidos_datosNumericos pedidoDatosNumericos = null;

            using (var db = new tiendaEntities()) {
                pedidoDatosNumericos = db.pedidos_datosNumericos
                     .Where(l => l.id == idSQL)
                     .FirstOrDefault();
            }


            string usuario_cliente = pedidoDatos.usuario_cliente;
            bool permisoVisualizar = privacidadAsesores.validarOperacion(usuario_cliente);


            if (permisoVisualizar) { }
            else
            {
                Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority));
            }
            string numero_operacion = pedidoDatos.numero_operacion;

            


            string moneda = pedidoDatosNumericos.monedaPedido;

            string metodoEnvio = pedidoDatosNumericos.metodoEnvio;
            string envio = Math.Round(pedidoDatosNumericos.envio, 2).ToString("C2", myNumberFormatInfo) + " " + moneda;



          
          
            
            string descuento = pedidoDatosNumericos.descuento.ToString();
            string descuento_porcentaje = pedidoDatosNumericos.descuento_porcentaje.ToString();

            decimal d_subtotal = Math.Round(pedidoDatosNumericos.subtotal, 2);



            hf_numero_operacion.Value = numero_operacion;
            lt_numero_operacion.Text = numero_operacion;

            Page.Title = "Productos de pedido #" + numero_operacion;
            lt_nombre_pedido.Text = pedidoDatos.nombre_pedido;

            hf_id_operacion.Value = pedidoDatos.id.ToString();
            lbl_moneda.Text = moneda;


            // if (subtotal == "" || string.IsNullOrEmpty(subtotal)) subtotal = "0";


            // Validamos que haya un descuento aplicado pues si este se encuentra, es necesario mostrarlo, de lo contrario no, pues ocupario mayor espacio en el desglose
            if (!string.IsNullOrWhiteSpace(descuento) || !string.IsNullOrWhiteSpace(descuento_porcentaje))
            {
                content_descuento.Visible = true;
                content_descuento_subtotal.Visible = true;
                decimal d_descuento = decimal.Parse(descuento);

                lbl_subtotalSinDescuento.Text = Math.Round(d_subtotal + d_descuento, 2).ToString("C2", myNumberFormatInfo) + " " + moneda;



                lbl_descuento_porcentaje.Text = Math.Round(decimal.Parse(descuento_porcentaje), 1).ToString();

            }
            else
            {
                content_descuento_subtotal.Visible = false;
                content_descuento.Visible = false;
            }
            lbl_envio.Text = envio;
            lbl_metodoEnvio.Text = metodoEnvio;
            lbl_subTotal.Text = d_subtotal.ToString("C2", myNumberFormatInfo) + " " + moneda;

            lbl_impuestos.Text = pedidoDatosNumericos.impuestos.ToString("C2", myNumberFormatInfo) + " " + moneda;

            lbl_total.Text = pedidoDatosNumericos.total.ToString("C2", myNumberFormatInfo) + " " + moneda;

            hl_editarDatos.NavigateUrl = GetRouteUrl("usuario-pedido-datos", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",seguridad.Encriptar(pedidoDatos.id.ToString()) }
                    });

            link_enviar.NavigateUrl = GetRouteUrl("usuario-pedido-visualizar", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion", seguridad.Encriptar(hf_id_operacion.Value) },

                     });


            hl_editarProductos.NavigateUrl = GetRouteUrl("usuario-pedido-productos", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",seguridad.Encriptar(hf_id_operacion.Value) }
                    });

            link_pago_paypal.NavigateUrl = GetRouteUrl("usuario-pedido-pago-paypal", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion", seguridad.Encriptar(hf_id_operacion.Value) },

                     });

            bool ResultBloquearPago = BloquearPago(pedidoDatos, pedidoDatosNumericos);
            if(ResultBloquearPago == false)
            {
                CargarLigaDePago(pedidoDatos, pedidoDatosNumericos);
            }

        }
        catch (Exception ex)
        {
            materializeCSS.crear_toast(this, "Error al generar formulario de pago", false);
            Response.Write(ex.ToString());
            devNotificaciones.error("Error al generar formulario de pago Santander, id op:" + Page.RouteData.Values["id_operacion"].ToString(), ex);
            devNotificaciones.ErrorSQL("Error al generar formulario de pago Santander, id op:" + Page.RouteData.Values["id_operacion"].ToString(), ex,  "");

            return;
        }
    }


    /// <summary>
    /// Verifica 4 aspectos para mostrar el boton de pago, [true] para permitir, [false] para no permitir ni generar liga
    /// 
    /// </summary>
    protected bool BloquearPago(pedidos_datos PedidoDatos, pedidos_datosNumericos PedidoDatosNumericos) {

 
        string numero_operacion = hf_numero_operacion.Value;
 

        decimal envio = Math.Round(PedidoDatosNumericos.envio, 2);
        
 
        Decimal tipoCambioActual = operacionesConfiguraciones.obtenerTipoDeCambio();

 
        List<pedidos_pagos_paypal> historialPagosPayPal = PayPalTienda.obtenerPagos(numero_operacion);

        if (historialPagosPayPal.Count >= 1) {
            motivosNoDisponiblePago.Visible = true; lbl_NoDisponiblePago.Visible = true;
            motivosNoDisponiblePago.InnerHtml += "- Ya se encuentra un pago/intento en PayPal";
           
            return true;

        }

 
        List<pedidos_pagos_respuesta_santander> historialPagosSantander = SantanderResponse.ObtenerTodos(numero_operacion);


        historialPagosSantander = historialPagosSantander.Where(p => p.estatus == "approved").ToList();
        if (historialPagosSantander.Count >= 1)
        {
            motivosNoDisponiblePago.Visible = true; lbl_NoDisponiblePago.Visible = true;
            motivosNoDisponiblePago.InnerHtml += "- Ya se encuentra un pago en Santander";
            motivosNoDisponiblePago.InnerHtml += "<br>- Estatus: <strong>"+ historialPagosSantander.OrderByDescending(p => p.fecha_primerIntento).FirstOrDefault().estatus + "</strong>";
            return true;

        }

        // INICIO [fechaPermitidaPago]  valida los días transcurridos y la vigencia del pedido de acuerdo a la establecida a cada moneda.
        {
            if (PedidoDatosNumericos.monedaPedido == "USD")
            {
                bool VigenciaSuperada = utilidad_fechas.calcularDiferenciaDias(PedidoDatos.fecha_creacion) > diasVigenciaPedidoPagoUSD;
                if(VigenciaSuperada)
                {
                    motivosNoDisponiblePago.Visible = true; lbl_NoDisponiblePago.Visible = true;
                    btn_renovarPedido.Visible = true;
                    motivosNoDisponiblePago.InnerHtml += "- Vigencia superada, renovar operación.";
                    return true;
                }
            }
            else
            {
                bool VigenciaSuperada = utilidad_fechas.calcularDiferenciaDias(PedidoDatos.fecha_creacion) > diasVigenciaPedidoPagoMXN;
                if (VigenciaSuperada)
                {
                    motivosNoDisponiblePago.Visible = true; lbl_NoDisponiblePago.Visible = true;
                    btn_renovarPedido.Visible = true;
                    motivosNoDisponiblePago.InnerHtml += "- Vigencia superada, renovar operación.";
                    return true;
                }
                
              
            }

        }


        // Inico diferenciaTipoDeCambio

        if (tipoCambioActual > PedidoDatosNumericos.tipo_cambio)
        {
            decimal DiferenciaTipoDeCambio = tipoCambioActual - PedidoDatosNumericos.tipo_cambio;

            if (DiferenciaTipoDeCambio > limiteDiferenciaTipoDeCambio)
            {
                btn_renovarPedido.Visible = true;
                motivosNoDisponiblePago.Visible = true; lbl_NoDisponiblePago.Visible = true;
                motivosNoDisponiblePago.InnerHtml += "- El tipo de cambio de tu pedido ha cambiado desde que lo realizaste, puedes renovar tu pedido y actualizarlo para proceder al pago. ";
                return true;
            }


        }



        // INICIO [EnvioEstablecido] valida que el envío sea valido


        switch (PedidoDatosNumericos.metodoEnvio)
        {


            case "En Tienda":
               
                break;

            case "Gratuito":
 
                break;

            case "Estándar":
                if (envio <= 0)
                {
                    motivosNoDisponiblePago.Visible = true; lbl_NoDisponiblePago.Visible = true;
                    motivosNoDisponiblePago.InnerHtml += "- No se ha establecido el precio de envio. <br>";
                    return true;
                }
                break;

            case "Ninguno":
                motivosNoDisponiblePago.Visible = true; lbl_NoDisponiblePago.Visible = true;
                motivosNoDisponiblePago.InnerHtml += "- No se ha establecido ninguna condición de envío. <br>";
                return true;
                break;


        }

        return false;
    }
    protected void btn_renovarPedido_Click(object sender, EventArgs e)
    {
        Tuple<bool, List<string>> resultado = pedidosProductos.renovarPedido(hf_numero_operacion.Value);

        if (resultado.Item1)
        {
            materializeCSS.crear_toast(up_estatus_Santander, "Pedido actualizado con éxito", true);

            string script = @"   setTimeout(function () {  location.reload(); }, 1000);";
            ScriptManager.RegisterStartupScript(up_estatus_Santander, typeof(Page), "redirección", script, true);

        }
        else
        {
            materializeCSS.crear_toast(up_estatus_Santander, "Error al actualizar algunos productos: " + resultado.Item2, true);
        }
    }

    protected void chkEnvioEnTienda_CheckedChanged(object sender, EventArgs e)
    {


        string resultado = null;

        if (chkEnvioEnTienda.Checked)
        {
            string metodoEnvio = "En Tienda";
            decimal envio = 0;


            resultado = pedidosDatos.actualizarEnvio(envio, metodoEnvio, lt_numero_operacion.Text);
        }
        else
        {
            resultado = pedidosDatos.actualizarEnvio(0, "Ninguno", lt_numero_operacion.Text);

        }


        if (resultado != null)
        {
            bool resultadoTotales = pedidosProductos.actualizarTotalStatic(lt_numero_operacion.Text);
            materializeCSS.crear_toast(up_estatus_Santander, "Envío actualizado con éxito", true);
            up_estatus_Santander.Update();
        }
        else
        {
            materializeCSS.crear_toast(up_estatus_Santander, "Error al actualizar método de envío", true);
            up_estatus_Santander.Update();
        }




        CargarDatosOperación();
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class usuario_cliente_pago_paypal : System.Web.UI.Page
{
    NumberFormatInfo myNumberFormatInfo = new CultureInfo("es-MX", true).NumberFormat;
    decimal montoMínimoPedidoEnvioGratuito = 1000;
    int diasVigenciaPedidoPagoUSD = 30;
    int diasVigenciaPedidoPagoMXN = 5;
    decimal limiteDiferenciaTipoDeCambio = (decimal)0.5;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Page.RouteData.Values["id_operacion"].ToString() != null)
            {
                CargarDatosPedido();
                link_regresar_metodos.NavigateUrl = GetRouteUrl("cliente-pedido-pago", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",seguridad.Encriptar(hf_id_pedido.Value) }
                    });
            }
        }
    }
    protected void cargarScriptPayPal(string monedaPedido)
    {
        var scriptTag = new HtmlGenericControl { TagName = "script" };
        string client_id = "";
        string host = HttpContext.Current.Request.Url.Host;
        if (host == "localhost") client_id = PayPalClient.client_id_Sandbox; else client_id = PayPalClient.client_id_Productivo;
        scriptTag.Attributes.Add("src", "https://www.paypal.com/sdk/js?client-id=" + client_id + "&currency=" + monedaPedido);
        this.Page.Header.Controls.Add(scriptTag);
    }
    private void CargarDatosPedido()
    {
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

            if (pedidoDatos == null)
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.warning, "No se encontró alguna operación");
                // materializeCSS.crear_toast(this, "No se ha encontrado una operación", false);
                return;
            }

            pedidos_datosNumericos pedidoDatosNumericos = null;

            using (var db = new tiendaEntities())
            {
                pedidoDatosNumericos = db.pedidos_datosNumericos
                     .Where(l => l.id == idSQL)
                     .FirstOrDefault();
            }

            string usuario_cliente = pedidoDatos.usuario_cliente;
            bool permisoVisualizar = privacidadAsesores.validarOperacion(usuario_cliente);

            if (!permisoVisualizar)
            {
                Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority));
            }
            string numero_operacion = pedidoDatos.numero_operacion;
            string moneda = pedidoDatosNumericos.monedaPedido;
            string metodoEnvio = pedidoDatosNumericos.metodoEnvio;
            string envio = Math.Round(pedidoDatosNumericos.envio, MidpointRounding.ToEven).ToString("C2", myNumberFormatInfo) + " " + moneda;
            double descuento = string.IsNullOrEmpty(pedidoDatosNumericos.descuento.ToString()) ? descuento = 0.0 : descuento = (double)pedidoDatosNumericos.descuento;
            string descuento_porcentaje = pedidoDatosNumericos.descuento_porcentaje.ToString();
            decimal d_subtotal = Math.Round(pedidoDatosNumericos.subtotal, MidpointRounding.ToEven);

            hf_numero_operacion.Value = numero_operacion;
            lt_numero_operacion.Text = numero_operacion;

            Page.Title = "Productos de pedido #" + numero_operacion;
            //lt_nombre_pedido.Text = pedidoDatos.nombre_pedido;

            hf_id_pedido.Value = pedidoDatos.id.ToString();
            //lbl_moneda.Text = moneda;


            // if (subtotal == "" || string.IsNullOrEmpty(subtotal)) subtotal = "0";


            // Validamos que haya un descuento aplicado pues si este se encuentra, es necesario mostrarlo, de lo contrario no, pues ocupario mayor espacio en el desglose
            //if (!string.IsNullOrWhiteSpace(descuento) || !string.IsNullOrWhiteSpace(descuento_porcentaje))
            //{
            //    content_descuento.Visible = true;
            //    content_descuento_subtotal.Visible = true;
            //    decimal d_descuento = decimal.Parse(descuento);

            //    lbl_subtotalSinDescuento.Text = Math.Round(d_subtotal + d_descuento, 2).ToString("C2", myNumberFormatInfo) + " " + moneda;



            //    lbl_descuento_porcentaje.Text = Math.Round(decimal.Parse(descuento_porcentaje), 1).ToString();

            //}
            //else
            //{
            //    content_descuento_subtotal.Visible = false;
            //    content_descuento.Visible = false;
            //}
            lbl_productos.Text = d_subtotal.ToString("C2", myNumberFormatInfo) + " " + moneda;
            lbl_descuento.Text = descuento.ToString("C2", myNumberFormatInfo) + " " + moneda;
            lbl_envio.Text = envio;
            //lbl_metodoEnvio.Text = metodoEnvio;
            lbl_subtotal.Text = d_subtotal.ToString("C2", myNumberFormatInfo) + " " + moneda;
            lbl_impuestos.Text = pedidoDatosNumericos.impuestos.ToString("C2", myNumberFormatInfo) + " " + moneda;
            lbl_total.Text = pedidoDatosNumericos.total.ToString("C2", myNumberFormatInfo) + " " + moneda;

            var PedidoProductos = PedidosEF.ObtenerProductos(numero_operacion);

            if (validarBloqueoPago(PedidoProductos, pedidoDatos, pedidoDatosNumericos))
            {
                crearBotonDePago(pedidoDatos, pedidoDatosNumericos, PedidoProductos);
            }
        }
        catch (Exception ex)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Error al generar el formulario de pago");
            //materializeCSS.crear_toast(this, "Error al generar formulario de pago", false);
            Response.Write(ex.ToString());
            devNotificaciones.error("Error al generar formulario de pago Santander, id op:" + Page.RouteData.Values["id_operacion"].ToString(), ex);
            devNotificaciones.ErrorSQL("Error al generar formulario de pago Santander, id op:" + Page.RouteData.Values["id_operacion"].ToString(), ex, "");
            return;
        }
    }

    protected void crearBotonDePago(pedidos_datos Datos, pedidos_datosNumericos DatosNumericos, List<pedidos_productos> Productos)
    {

        string numero_operacion = hf_numero_operacion.Value;


        var DirecciónDeEnvio = PedidosEF.ObtenerDireccionEnvio(numero_operacion);


        // Validamos que haya un descuento aplicado pues si este se encuentra, es necesario mostrarlo, de lo contrario no, pues ocupario mayor espacio en el desglose
        //if (DatosNumericos.descuento != null || DatosNumericos.descuento_porcentaje != null)
        //{
        //    content_descuento.Visible = true;
        //    content_descuento_subtotal.Visible = true;


        //    lbl_subtotalSinDescuento.Text = decimal.Round(DatosNumericos.subtotal + (decimal)DatosNumericos.descuento, 2).ToString("C2", myNumberFormatInfo) + " " + DatosNumericos.monedaPedido;



        //    lbl_descuento_porcentaje.Text = decimal.Round((decimal)DatosNumericos.descuento_porcentaje, 1).ToString();

        //}
        //else
        //{
        //    content_descuento_subtotal.Visible = false;
        //    content_descuento.Visible = false;
        //}




        StringBuilder json = new StringBuilder();

        string scriptIni = @"  paypal.Buttons({
            createOrder: function(data, actions) {
                return actions.order.create({
                    'intent': 'CAPTURE',
                    'application_context' : {
                    'shipping_preference': '{tipo_envio}'
                     },
                    'purchase_units': [{";







        // Si el método de envío requiere dicha dirección, se procesa.
        switch (DatosNumericos.metodoEnvio)
        {

            case "En Tienda":
                json.Append(scriptIni.Replace("{tipo_envio}", "NO_SHIPPING"));
                break;

            case "Gratuito":
                json.Append(scriptIni.Replace("{tipo_envio}", "SET_PROVIDED_ADDRESS"));
                json.Append(procesarDirecciónEnvío(DirecciónDeEnvio.response, Datos.cliente_nombre + " " + Datos.cliente_apellido_paterno));
                break;

            case "Estándar":
                json.Append(scriptIni.Replace("{tipo_envio}", "SET_PROVIDED_ADDRESS"));
                json.Append(procesarDirecciónEnvío(DirecciónDeEnvio.response, Datos.cliente_nombre + " " + Datos.cliente_apellido_paterno));
                break;

            case "Ninguno":
                json.Append(scriptIni.Replace("{tipo_envio}", "NO_SHIPPING"));
                break;


        }
        json.Append(@"

                   'amount' : {
                    'currency_code' : '" + DatosNumericos.monedaPedido + @"',
                            'value': '" + Math.Round((DatosNumericos.subtotal - DatosNumericos.envio) + DatosNumericos.impuestos + DatosNumericos.envio, 2) + @"',
                            'breakdown': {
                   'item_total': {
                   'currency_code' : '" + DatosNumericos.monedaPedido + @"',
                                    'value':  '" + decimal.Round((DatosNumericos.subtotal - DatosNumericos.envio), 2) + @"',
                                },
                    'shipping': {
                    'currency_code': '" + DatosNumericos.monedaPedido + @"',
                                    'value': '" + decimal.Round(DatosNumericos.envio, 2) + @"'
                                },
                                'tax_total': {
                    'currency_code' : '" + DatosNumericos.monedaPedido + @"',
                                    'value': '" + decimal.Round(DatosNumericos.impuestos, 2) + @"'
                                },
                            }

        },");




        json.Append(@" 'items': [ ");

        //   LinkButton linkActualizarUP = up_estatus_paypal.FindControl("linkActualizarUP") as LinkButton;

        foreach (var p in Productos)
        {
            string sku = p.numero_parte;
            string name = p.descripcion;
            name = name.Replace("'", "ft").Replace("\"", "in");

            if (name.Length >= 127) { name = name.Substring(0, 127); }

            string description = name;
            string unit_amount = decimal.Round(p.precio_unitario, 2).ToString();
            string quantity = decimal.Round(p.cantidad, 0).ToString();






            json.Append(@"{     'name' : '" + name + @"',
                                'sku' : '" + sku + @"',
                                'description': '" + description + @"',
                                'unit_amount' : {
                                'currency_code' : '" + DatosNumericos.monedaPedido + @"',
                                    'value' : '" + unit_amount + @"'
                                },
                               'quantity': '" + quantity + @"',
                                'category' : 'PHYSICAL_GOODS'
                         },");

        }

        json.Append(@"     ], ");
        json.Append(@" 'invoice_id' : '" + numero_operacion + @"', ");
        json.Append(@"  'description': '" + Datos.nombre_pedido + @"', ");
        json.Append(@" 'soft_descriptor' : 'PD " + numero_operacion + @"' ");
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

        cargarScriptPayPal(DatosNumericos.monedaPedido);
        ClientScriptManager cs = Page.ClientScript;
        Type cstype = this.GetType();
        cs.RegisterStartupScript(cstype, "PayPalButton", json.ToString(), true);

        paypal_button_container.Visible = true;

        up_estatus_paypal.Update();
    }

    protected void btn_renovarPedido_Click(object sender, EventArgs e)
    {
        Tuple<bool, List<string>> resultado = pedidosProductos.renovarPedido(hf_numero_operacion.Value);

        if (resultado.Item1)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.success, "Pedido renovado");
            //materializeCSS.crear_toast(up_estatus_paypal, "Pedido actualizado con éxito", true);

            string script = @"   setTimeout(function () {  location.reload(); }, 1000);";
            ScriptManager.RegisterStartupScript(up_estatus_paypal, typeof(Page), "redirección", script, true);

        }
        else
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Error al renovar el pedido");
            //materializeCSS.crear_toast(up_estatus_paypal, "Error al actualizar algunos productos: " + resultado.Item2, true);
        }
    }
    protected void resetearTextosInfoPago()
    {
        lbl_paypal_intento.Text = "";
        lbl_paypal_estado.Text = "";
        lbl_paypal_monto.Text = "";
        lbl_paypal_moneda.Text = "";
        lbl_paypal_fecha_primerIntento.Text = "";
        lbl_paypal_fecha_actualización.Text = "";
    }

    protected void mostrarEstadoPayPal(List<pedidos_pagos_paypal> historialPagos)
    {


        paypal_button_container.Visible = false;
        dt_desglose_paypal.Visible = true;

        lbl_paypal_intento.Text = historialPagos[0].intento;
        lbl_paypal_estado.Text = historialPagos[0].estado;
        lbl_paypal_monto.Text = decimal.Parse(historialPagos[0].monto).ToString("C2", myNumberFormatInfo);
        lbl_paypal_moneda.Text = historialPagos[0].moneda;

        lbl_paypal_fecha_primerIntento.Text = String.Format("{0:F}", historialPagos[0].fecha_primerIntento);
        lbl_paypal_fecha_actualización.Text = String.Format("{0:F}", historialPagos[0].fecha_actualización);





        up_estatus_paypal.Update();

    }

    // Documentación: https://developer.paypal.com/docs/api/orders/v2/#definition-shipping_detail.address_portable
    protected string procesarDirecciónEnvío(pedidos_direccionEnvio DirecciónDeEnvio, string fullName)
    {


        string numero_operacion = hf_numero_operacion.Value;


        string address_line_1 = DirecciónDeEnvio.calle + ", " + DirecciónDeEnvio.numero + ", " + DirecciónDeEnvio.colonia;


        if (address_line_1.Length > 300) address_line_1 = address_line_1.Substring(0, 300);

        string address_line_2 = DirecciónDeEnvio.numero;


        string admin_area_2 = DirecciónDeEnvio.delegacion_municipio;
        string admin_area_1 = DirecciónDeEnvio.estado;
        string postal_code = DirecciónDeEnvio.codigo_postal;
        string country_code = paises.obtenerCódigoPais(DirecciónDeEnvio.pais);

        StringBuilder direcciónEnvio = new StringBuilder();


        string envio = (@"'shipping': {
            'name': {
                'full_name': '" + fullName + @"'
                            },
                            'address': {
                                'address_line_1': '" + address_line_1 + @"',
                                'address_line_2': '" + address_line_2 + @"',
                                'admin_area_2': '" + admin_area_2 + @"',
                                'admin_area_1': '" + admin_area_1 + @"',
                                'postal_code': '" + postal_code + @"',
                                'country_code': '" + country_code + @"'
                            },
                        },");


        return envio;

    }


    /// <summary>
    /// Verifica 7 aspectos para mostrar el boton de pago, [true] para mostrarlo, [false] para no mostrarlo
    /// 
    /// </summary>
    protected bool validarBloqueoPago(List<pedidos_productos> Productos, pedidos_datos Datos, pedidos_datosNumericos DatosNumericos)
    {
        bool pedidoAprobadoPorAsesor = false;
        bool pagoYaRealizado = false;
        bool montoSuperiorParaEnvioGratuito = false;
        bool fechaPermitidaPago = false;
        bool precioEnvioEstablecido = false;
        // Este valor cambia a [false] en cuanto en la validación encuentre un producto no disponible.
        bool pedidoElegibleParaEnvioGratuito = true;
        bool direcciónEnvíoCompleta = true;
        // Si el tipo de cambio aumentó más de n monto, el pedido deberá ser recalculado, si hay diferencia del monto, este se vuelve [true] y no permité el pago hasta recalcular
        bool diferenciaTipoDeCambio = false;

        bool permitirPago = false;



        string numero_operacion = hf_numero_operacion.Value;
        int idSQL = int.Parse(hf_id_pedido.Value);

        decimal envio = Math.Round(DatosNumericos.envio, 2);
        decimal tipoDeCambioPedido = DatosNumericos.tipo_cambio;
        string metodoEnvio = DatosNumericos.metodoEnvio;

        string monedaPedido = DatosNumericos.monedaPedido;
        decimal totalPedido = Math.Round(DatosNumericos.total, 2);
        DateTime fechaPedido = Datos.fecha_creacion;

        Decimal tipoCambioActual = operacionesConfiguraciones.obtenerTipoDeCambio();

        // Guarda el número de parte de los productos que no son elegibles para envío gratuito y mostrarlos.
        string ProductosNoDisponiblesParaEnvioGratis = "";

        //lbl_metodoEnvio.Text = metodoEnvio;





        List<pedidos_pagos_respuesta_santander> historialPagosSantander = SantanderResponse.ObtenerTodos(numero_operacion);


        historialPagosSantander = historialPagosSantander.Where(p => p.estatus == "approved").ToList();
        if (historialPagosSantander.Count >= 1)
        {
            motivosNoDisponiblePago.Visible = true; lbl_NoDisponiblePago.Visible = true;
            motivosNoDisponiblePago.InnerHtml += "- Ya se encuentra un pago en Santander";

            return false;

        }




        // INICIO [pagoYaRealizado] verificación de pagos PayPal, si ya hay un pago en proceso, automáticamente detiene las validaciones
        // siguientes y retorna [false]
        {

            List<pedidos_pagos_paypal> historialPagos = PayPalTienda.obtenerPagos(numero_operacion);



            resetearTextosInfoPago();
            if (historialPagos == null || historialPagos.Count < 1)
            {
                pagoYaRealizado = false;
                dt_desglose_paypal.Visible = false;
            }
            else
            {
                dt_desglose_paypal.Visible = true;
                pagoYaRealizado = true;
                mostrarEstadoPayPal(historialPagos);
                // Si ya hay un pago en proceso retornamos falso para que no procesa a mostrar el boton de pago si ha validar las demás opciones
                return false;

            }
            // FIN verificación de pagos PayPal
        }



        // INICIO [montoSuperiorParaEnvioGratuito] verificación de monto, debe ser superior al monto definido en la variable global [montoMínimoPedidoEnvioGratuito] y en moneda nacional ["MXN"]
        {
            // Si la moneda esta nacional, se comprueba directamente, si no, se convierte de USD a MXN
            if (monedaPedido == "MXN")
            {
                if (totalPedido > montoMínimoPedidoEnvioGratuito) montoSuperiorParaEnvioGratuito = true;
            }
            else
            {
                decimal totalEnMXN = totalPedido * tipoCambioActual;
                if (totalEnMXN > montoMínimoPedidoEnvioGratuito) montoSuperiorParaEnvioGratuito = true;

            }


            // FIN verificación de monto
        }






        // INICIO [fechaPermitidaPago]  valida los días transcurridos y la vigencia del pedido de acuerdo a la establecida a cada moneda.
        {
            if (monedaPedido == "USD")

                fechaPermitidaPago = utilidad_fechas.calcularDiferenciaDias(fechaPedido) < diasVigenciaPedidoPagoUSD;
            else fechaPermitidaPago = utilidad_fechas.calcularDiferenciaDias(fechaPedido) < diasVigenciaPedidoPagoMXN;

            if (!fechaPermitidaPago)
                btn_renovarPedido.Visible = true;
        }


        // Inico diferenciaTipoDeCambio

        if (tipoCambioActual > tipoDeCambioPedido)
        {
            decimal DiferenciaTipoDeCambio = tipoCambioActual - tipoDeCambioPedido;

            if (DiferenciaTipoDeCambio > limiteDiferenciaTipoDeCambio)
            {
                btn_renovarPedido.Visible = true;
                motivosNoDisponiblePago.Visible = true; lbl_NoDisponiblePago.Visible = true;
                motivosNoDisponiblePago.InnerHtml += "- El tipo de cambio de tu pedido ha cambiado desde que lo realizaste, puedes renovar tu pedido y actualizarlo para proceder al pago. ";
                return false;
            }


        }


        // INICIO [precioEnvioEstablecido]  valida que el asesor haya establecido un precio de envio y método de envío.
        {
            if (envio > 0)
            {

                precioEnvioEstablecido = true;
            }
        }
        // INICIO [pedidoElegibleParaEnvioGratuito] valida que todos los productos de la operación sean permitidos para el envio gratuito.
        {
            foreach (var p in Productos)
            {

                string numero_parte = p.numero_parte;
                bool resultado = productosTienda.productoDisponibleEnvio(numero_parte);

                // Si encuentra un producto, el envio ya no podrá ser gratuito y no se generara el boton de pago hasta que otra condición lo anule.
                if (resultado == false)
                {
                    ProductosNoDisponiblesParaEnvioGratis += numero_parte + ", ";
                    pedidoElegibleParaEnvioGratuito = false;
                }

            }
            ProductosNoDisponiblesParaEnvioGratis = ProductosNoDisponiblesParaEnvioGratis.TrimEnd(' ');
            ProductosNoDisponiblesParaEnvioGratis = ProductosNoDisponiblesParaEnvioGratis.TrimEnd(',');
            // FIN [pedidoElegibleParaEnvioGratuito] valida que todos los productos de la operación sean permitidos para el envio gratuito.
        }



        // INICIO [direcciónEnvíoCompleta] valida que todos los campos de la dirección de envío esten completos.


        bool validarEnvio = false;

        // Solo validaremos la dirección de envío siempre y cuando este se vaya a utilizar.
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
                validarEnvio = true; break;

            case "Ninguno":
                motivosNoDisponiblePago.Visible = true; lbl_NoDisponiblePago.Visible = true;
                motivosNoDisponiblePago.InnerHtml += "- No se ha establecido ninguna condición de envío. <br>";
                return false;
                break;


        }

        if (validarEnvio)
        {

            model_direccionesEnvio direccionEnvio = pedidosDatos.obtenerPedidoDireccionEnvioStatic(numero_operacion);
            Tuple<bool, string> resultadoValidación = validarCampos.direccionEnvioCompleta(direccionEnvio);

            if (resultadoValidación.Item1)
            {

                permitirPago = true;
                direcciónEnvíoCompleta = true;
            }
            else
            {
                permitirPago = false;
                motivosNoDisponiblePago.Visible = true; lbl_NoDisponiblePago.Visible = true;
                motivosNoDisponiblePago.InnerHtml += "- Tu dirección de envío no esta completa, faltan los siguientes campos: " + resultadoValidación.Item2 + " <br>";

                direcciónEnvíoCompleta = false;
            }

        }






        if (validarEnvio == true)
        {
            // Muestra los resultados que no hayan aplicado
            if (montoSuperiorParaEnvioGratuito == false && precioEnvioEstablecido == false && pedidoElegibleParaEnvioGratuito == false && direcciónEnvíoCompleta)
            {
                motivosNoDisponiblePago.Visible = true; lbl_NoDisponiblePago.Visible = true;
                motivosNoDisponiblePago.InnerHtml += "- El pedido no supera la cantidad de: $" + montoMínimoPedidoEnvioGratuito + " para el envío gratuito. <br>";
                motivosNoDisponiblePago.InnerHtml += "- Tu asesor aún no establece el precio de envío. <br>";
                permitirPago = false;
            }


        }
        // Muestra los resultados que no hayan aplicado
        if (fechaPermitidaPago == false)
        {
            motivosNoDisponiblePago.Visible = true; lbl_NoDisponiblePago.Visible = true;
            motivosNoDisponiblePago.InnerHtml += "- La vigencia de los precios y fecha de pago ha vencido. <br>";
            permitirPago = false;
        }


        if (validarEnvio == true)
        {
            if (pedidoElegibleParaEnvioGratuito == false && montoSuperiorParaEnvioGratuito == true && precioEnvioEstablecido == false && direcciónEnvíoCompleta)
            {
                motivosNoDisponiblePago.Visible = true; lbl_NoDisponiblePago.Visible = true;
                motivosNoDisponiblePago.InnerHtml += "- Contiene los siguientes productos que no aplica el envío gratuito: " + ProductosNoDisponiblesParaEnvioGratis + " <br>";
                motivosNoDisponiblePago.InnerHtml += "- Tu asesor aún no establece el precio de envío. <br>";
                permitirPago = false;
            }
        }




        if (validarEnvio == true)
        {
            if (montoSuperiorParaEnvioGratuito && fechaPermitidaPago && pedidoElegibleParaEnvioGratuito && direcciónEnvíoCompleta)
            {

                permitirPago = true;
                return permitirPago;
            }
        }



        if (validarEnvio == true)
        {
            if (precioEnvioEstablecido && fechaPermitidaPago && direcciónEnvíoCompleta)
            {
                permitirPago = true;
                return permitirPago;
            }
        }
        else
        {
            if (fechaPermitidaPago)
            {

                permitirPago = true;
                return permitirPago;
            }
        }


        return permitirPago;





        ;
    }
    protected void linkActualizarUP_Click(object sender, EventArgs e)
    {

        string numero_operacion = hf_numero_operacion.Value;



        var PedidoDatos = PedidosEF.ObtenerDatos(numero_operacion);
        var PedidoDatosNumericos = PedidosEF.ObtenerNumeros(numero_operacion);
        var PedidoProductos = PedidosEF.ObtenerProductos(numero_operacion);

        if (validarBloqueoPago(PedidoProductos, PedidoDatos, PedidoDatosNumericos))
        {
            crearBotonDePago(PedidoDatos, PedidoDatosNumericos, PedidoProductos);
        }
    }
}
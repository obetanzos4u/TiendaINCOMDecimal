<%@ WebHandler Language="C#" Class="procesar_pago" %>

using System;
using System.Web;
using System.IO;
using System.Threading.Tasks;
using PayPalCheckoutSdk.Orders;
using System.Net;
using System.Web.Script.Serialization;
using System.Data;
using System.Collections.Generic;
using System.Globalization;
using System.Web.SessionState;
using System.Linq;

public class procesar_pago : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        HttpContext _context = context;
        string url = context.Request.Url.GetLeftPart(UriPartial.Authority);

        context.Response.ContentType = "text/plain";

        string strJson = new StreamReader(context.Request.InputStream).ReadToEnd();
        try
        {
            pedidos_pagos_paypal pago = Newtonsoft.Json.JsonConvert.DeserializeObject<pedidos_pagos_paypal>(strJson);


            Task.Run(() =>
            {
                crearPagoAsync(pago, context);
            });

        }
        catch (Exception ex)
        {
            devNotificaciones.notificacionSimple(strJson + "<br>" + ex.ToString());
        }
    }
    static async Task crearPagoAsync(pedidos_pagos_paypal pago, HttpContext _context)
    {
        PayPalTienda guardar = new PayPalTienda();
        int idPagoPaypal = await guardar.guardarPagoAsync(pago);
        HttpContext context = _context;
        System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(PayPalTienda.AcceptAllCertifications);
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        OrdersGetRequest request = new OrdersGetRequest(pago.idTransacciónPayPal);
        //3. Call PayPal to get the transaction
        PayPalHttp.HttpResponse response = await PayPalClient.client().Execute(request);
        //4. Save the transaction in your database. Implement logic to save transaction to your database for future reference.
        Order result = response.Result<Order>();

        AmountWithBreakdown amount = result.PurchaseUnits[0].AmountWithBreakdown;
        // pago.numero_operacion = numero_operacion;
        // pago.idTransacciónPayPal = idTransacciónPayPal;

        using (tiendaEntities db = new tiendaEntities())
        {
            pago = db.pedidos_pagos_paypal.Where(x => x.id == idPagoPaypal).First();

            pago.intento = result.CheckoutPaymentIntent;
            pago.estado = result.Status;
            pago.fecha_primerIntento = utilidad_fechas.obtenerCentral();
            pago.aprobadoAsesor = false;

            pago.moneda = amount.CurrencyCode;
            pago.monto = amount.Value;

            db.SaveChanges();
        }
        enviarEmailComprobantePago(pago.numero_operacion, result, context);
    }
    protected static void enviarEmailComprobantePago(string numero_operacion, Order result, HttpContext _context)
    {
        try
        {
            HttpContext context = _context;

            DataTable dt_PedidoDatos = pedidosDatos.obtenerPedidoDatosStatic(numero_operacion);


            DataTable dt_PedidoProductos = pedidosProductos.obtenerProductos(numero_operacion);

            string usuario_email = dt_PedidoDatos.Rows[0]["usuario_cliente"].ToString();
            string cliente_nombre = dt_PedidoDatos.Rows[0]["cliente_nombre"].ToString();
            string cliente_apellido_paterno = dt_PedidoDatos.Rows[0]["cliente_apellido_paterno"].ToString();
            string nombre_operacion = dt_PedidoDatos.Rows[0]["nombre_pedido"].ToString();
            string total = dt_PedidoDatos.Rows[0]["total"].ToString();
            string monedaPedido = dt_PedidoDatos.Rows[0]["monedaPedido"].ToString();


            AmountWithBreakdown amount = result.PurchaseUnits[0].AmountWithBreakdown;
            decimal montoPayPal = decimal.Parse(amount.Value);
            string monedaPayPal = amount.CurrencyCode;

            string id_operacion_encritado = seguridad.Encriptar(dt_PedidoDatos.Rows[0]["id"].ToString());

            string url_pedido =
                    context.Request.Url.GetLeftPart(UriPartial.Authority) + "/usuario/cliente/mi-cuenta/pedidos/resumen/" + id_operacion_encritado;
            string url_paypal = "https://www.paypal.com/cgi-bin/webscr?cmd=_view-a-trans&id=" + result.Id;

            DateTime fechaSolicitud = utilidad_fechas.obtenerCentral();
            string asunto = "Pago realizado de un pedido Cliente - Nombre Pedido: " + nombre_operacion + " " + cliente_nombre + " ";
            string mensaje = string.Empty;
            string plantillaAsesor = "/email_templates/operaciones/pedidos/asesores_pago_realizado.html";



            string productosEmailHTML = string.Empty;
            foreach (DataRow p in dt_PedidoProductos.Rows)
            {
                productosEmailHTML += "<strong>" + p["numero_parte"].ToString() + "</strong> - " + p["descripcion"].ToString() + "<br><br>";
            }

            Dictionary<string, string> datosDiccRemplazo = new Dictionary<string, string>();

            datosDiccRemplazo.Add("{dominio}", context.Request.Url.GetLeftPart(UriPartial.Authority));

            datosDiccRemplazo.Add("{usuario_email}", usuario_email);
            datosDiccRemplazo.Add("{cliente_nombre}", cliente_nombre + " " + cliente_apellido_paterno);
            datosDiccRemplazo.Add("{numero_operacion}", numero_operacion);
            datosDiccRemplazo.Add("{nombre_operacion}", nombre_operacion);

            datosDiccRemplazo.Add("{totalPayPal}", montoPayPal.ToString("C2", new CultureInfo("es-MX", true).NumberFormat) + " " + monedaPayPal);
            datosDiccRemplazo.Add("{totalOperacion}", decimal.Parse(total).ToString("C2", new CultureInfo("es-MX", true).NumberFormat) + " " + monedaPedido);

            datosDiccRemplazo.Add("{url_operacion}", url_pedido);
            datosDiccRemplazo.Add("{url_paypal}", url_paypal);
            datosDiccRemplazo.Add("{productos}", productosEmailHTML);


            plantillaAsesor = Path.Combine(context.Request.PhysicalApplicationPath, plantillaAsesor);


            using (StreamReader reader = new StreamReader(context.Server.MapPath("~") + plantillaAsesor))
            {
                mensaje = reader.ReadToEnd();
            }

            if (datosDiccRemplazo.Count >= 1)
            {
                foreach (var item in datosDiccRemplazo)
                {
                    mensaje = mensaje.Replace(item.Key, item.Value);
                }
            }

            emailTienda emailAsesor = new emailTienda(asunto, "jaraujo@incom.mx, ralbert@incom.mx, fgarcia@incom.mx", mensaje, "serviciosweb@incom.mx"); //cmiranda@it4u.com.mx, jhernandez@incom.mx, pjuarez@incom.mx ; retail@incom.mx
            emailAsesor.general();

            //Inicio email para el cliente

            string plantillaCliente = "/email_templates/operaciones/pedidos/cliente_pago_realizado.html";

            Dictionary<string, string> datosDiccRemplazoCliente = new Dictionary<string, string>();

            datosDiccRemplazoCliente.Add("{dominio}", context.Request.Url.GetLeftPart(UriPartial.Authority));

            datosDiccRemplazoCliente.Add("{nombre_operacion}", nombre_operacion);
            datosDiccRemplazoCliente.Add("{numero_operacion}", numero_operacion);
            datosDiccRemplazoCliente.Add("{cliente_nombre}", cliente_nombre + " " + cliente_apellido_paterno);

            datosDiccRemplazoCliente.Add("{usuario_email}", usuario_email);
            datosDiccRemplazoCliente.Add("{totalPayPal}", montoPayPal.ToString("C2", new CultureInfo("es-MX", true).NumberFormat) + " " + monedaPayPal);
            datosDiccRemplazoCliente.Add("{totalOperacion}", decimal.Parse(total).ToString("C2", new CultureInfo("es-MX", true).NumberFormat) + " " + monedaPedido);
            datosDiccRemplazoCliente.Add("{estatus}", result.Status);
            datosDiccRemplazoCliente.Add("{url_operacion}", url_pedido);

            datosDiccRemplazoCliente.Add("{productos}", productosEmailHTML);


            plantillaCliente = Path.Combine(context.Request.PhysicalApplicationPath, plantillaCliente);

            mensaje = string.Empty;
            using (StreamReader reader = new StreamReader(context.Server.MapPath("~") + plantillaCliente))
            {
                mensaje = reader.ReadToEnd();
            }

            if (datosDiccRemplazoCliente.Count >= 1)
            {
                foreach (var item in datosDiccRemplazoCliente)
                {
                    mensaje = mensaje.Replace(item.Key, item.Value);
                }
            }

            emailTienda emailCliente = new emailTienda("Pago realizado para el pedido: " + nombre_operacion,
                usuario_email + ", serviciosweb@incom.mx", mensaje, "retail@incom.mx");
            emailCliente.general();

            //FIN email para el cliente
        }
        catch (Exception ex)
        {
            devNotificaciones.notificacionSimple(ex.ToString() + "");
        }
    }
    // Converts the specified JSON string to an object of type T
    public T Deserialize<T>(string context)
    {
        string jsonData = context;
        //cast to specified objectType
        var obj = (T)new JavaScriptSerializer().Deserialize<T>(jsonData);
        return obj;
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;

public class PayPalTokenModel
{
    public string scope { get; set; }
    public string nonce { get; set; }
    public string access_token { get; set; }
    public string token_type { get; set; }
    public string app_id { get; set; }
    public int expires_in { get; set; }
}


/// <summary>
/// Descripción breve de PayPalTienda
/// </summary>
public class PayPalTienda
{
    public bool resultado { get; set; }
    public string msg_resultado { get; set; }


    public PayPalTienda()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //

    }

    public static bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
    //2. Set up your server to receive a call from the client
    /*
      You can use this method to retrieve an order by passing the order ID.
     */
    public async Task<PayPalHttp.HttpResponse> GetOrder(string orderId, bool debug = false)
    {
        OrdersGetRequest request = new OrdersGetRequest(orderId);
        //3. Call PayPal to get the transaction
        PayPalHttp.HttpResponse response = await PayPalClient.client().Execute(request);
        //4. Save the transaction in your database. Implement logic to save transaction to your database for future reference.
        Order result = response.Result<Order>();
        Console.WriteLine("Retrieved Order Status");
        Console.WriteLine("Status: {0}", result.Status);
        Console.WriteLine("Order Id: {0}", result.Id);
        Console.WriteLine("Intent: {0}", result.CheckoutPaymentIntent);
        Console.WriteLine("Links:");
        foreach (LinkDescription link in result.Links)
        {
            Console.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
        }
        AmountWithBreakdown amount = result.PurchaseUnits[0].AmountWithBreakdown;
        Console.WriteLine("Total Amount: {0} {1}", amount.CurrencyCode, amount.Value);

        return response;
    }
    public static pedidos_pagos_paypal obtenerPago(string numero_operacion)
    {

        try
        {
            using (tiendaEntities db = new tiendaEntities())
            {
                var entrada = db.pedidos_pagos_paypal.Where(x => x.numero_operacion == numero_operacion).FirstOrDefault();
                return entrada;
            }
        }
        catch (Exception ex)
        {

            devNotificaciones.error("Obtener historial de pagos", ex);
            return null;
        }

    }
    public static  pedidos_pagos_paypal  obtenerPago(int idPagoPayPal) {

        try { 
        using (tiendaEntities db = new tiendaEntities())
        {
            var entrada = db.pedidos_pagos_paypal.Where(x => x.id == idPagoPayPal).First();
            return entrada;
        }
        } catch(Exception ex)
        {

            devNotificaciones.error("Obtener historial de pagos", ex);
            return null;
        }

    }


    public static List<pedidos_pagos_paypal> obtenerPagos(string numero_operacion)
    {

        try
        {
            using (tiendaEntities db = new tiendaEntities())
            {
                var evento = db.pedidos_pagos_paypal.Where(x => x.numero_operacion == numero_operacion).ToList();
                return evento;
            }
        }
        catch (Exception ex)
        {

            devNotificaciones.error("Obtener historial de pagos", ex);
            return null;
        }

    }


    public async Task<pedidos_pagos_paypal> actualizarPagoAsync(pedidos_pagos_paypal pago) {

        try
        {
            using (tiendaEntities db = new tiendaEntities())
            {
                db.pedidos_pagos_paypal.Add(pago);
                int x = await db.SaveChangesAsync();

                if (x == 1)
                {
                    resultado = true;
                    msg_resultado = "Categoria creada con éxito";
                    return pago;

                }
                else
                {
                    resultado = false;
                    msg_resultado = "No se agrego la categoria";
                    return pago;
                }

            }

        }
        catch (Exception ex)
        {

            resultado = false;
            msg_resultado = "Error al agregar categoria";


            return pago;
        }

    }



    public async Task<int> guardarPagoAsync(pedidos_pagos_paypal pago) {


        try
        {
            using (tiendaEntities db = new tiendaEntities())
            {
                db.pedidos_pagos_paypal.Add(pago);
                int x = await db.SaveChangesAsync();

                if (x == 1)
                {
                    resultado = true;
                    msg_resultado = "Entrada creada con éxito";

                    return pago.id;
                }
                else
                {
                    resultado = false;
                    msg_resultado = "No se agrego la entrada";

                    return 0;
                }

            }

        }
        catch (Exception ex)
        {

            resultado = false;
            msg_resultado = "Error al guardar pago";

            devNotificaciones.notificacionSimple(ex.ToString());

            return 0;
        }


    }

 

    public async Task<PayPalTokenModel> RequestPayPalToken()
    {
        // Discussion about SSL secure channel
        // http://stackoverflow.com/questions/32994464/could-not-create-ssl-tls-secure-channel-despite-setting-servercertificatevalida
        ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

        try
        {
            // ClientId of your Paypal app API
            string APIClientId = "ASb_WuH0ark2vYRZwrVj45RzPxgFwHsL7uASDooPJxNefVNFxk30viAZcCI6bbiMaL76Xaa9REGvnROn";

            // secret key of you Paypal app API
            string APISecret = "EGtv4f5exHgzwacg0LA29wAlAFzkrSdT5NxOle__eV4bJa0LX7yX5VB48nlGyg1yIBZlKfrCh_h-Y1QD";

            using (var client = new System.Net.Http.HttpClient())
            {
                var byteArray = Encoding.UTF8.GetBytes(APIClientId + ":" + APISecret);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                var url = new Uri("https://api.sandbox.paypal.com/v1/oauth2/token", UriKind.Absolute);

                client.DefaultRequestHeaders.IfModifiedSince = DateTime.UtcNow;

                var requestParams = new List<KeyValuePair<string, string>>
                            {
                                new KeyValuePair<string, string>("grant_type", "client_credentials")
                            };

                var content = new FormUrlEncodedContent(requestParams);
                var webresponse = await client.PostAsync(url, content);
                var jsonString = await webresponse.Content.ReadAsStringAsync();

                // response will deserialized using Jsonconver
                var payPalTokenModel = JsonConvert.DeserializeObject<PayPalTokenModel>(jsonString);

                return payPalTokenModel;
            }
        }
        catch (System.Exception ex)
        {
            return null;
        }
    }
}





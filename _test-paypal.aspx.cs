using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using PayPalCheckoutSdk.Orders;
public partial class TestPayPal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
           // Response.TrySkipIisCustomErrors = true;
          
            Page.Title = "Test PayPal";
 
        }
    }
    public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
    protected async  void btn_obtener_ClickAsync(object sender, EventArgs e)
    {
        System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        OrdersGetRequest request = new OrdersGetRequest(txt_order_id.Text);
        //3. Call PayPal to get the transaction
        PayPalHttp.HttpResponse response = await PayPalClient.client().Execute(request);
        //4. Save the transaction in your database. Implement logic to save transaction to your database for future reference.
        Order result = response.Result<Order>();

        div_respuesta.InnerHtml += PayPalClient.ObjectToJSONString(result);
        div_respuesta.InnerHtml += string.Format(" <br> <br>");
        div_respuesta.InnerHtml += string.Format("Retrieved Order Status <br>");
        div_respuesta.InnerHtml += string.Format("<br> Status: {0}", result.Status);
        div_respuesta.InnerHtml += string.Format("<br> Order Id: {0}", result.Id);
        div_respuesta.InnerHtml += string.Format("<br> Intent: {0}", result.CheckoutPaymentIntent);
        div_respuesta.InnerHtml += string.Format("<br> Links:");
        foreach (LinkDescription link in result.Links)
        {
            div_respuesta.InnerHtml += string.Format("<br> {0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
        }
        AmountWithBreakdown amount = result.PurchaseUnits[0].AmountWithBreakdown;
        div_respuesta.InnerHtml += string.Format("<br> Total Amount: {0} {1}", amount.CurrencyCode, amount.Value);
    }


 
 
    
}
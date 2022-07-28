using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _pruebas_Api_Paquete_Express : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

 
    protected void btn_obtener_token_Click(object sender, EventArgs e)
    {
        RestClient client = new RestClient("http://qaglp.paquetexpress.mx:7007/");
 
        var request = new RestRequest(Method.POST);

        request.Resource = "RadRestFul/api/rad/loginv1/login";
        request.RequestFormat = DataFormat.Json;

        request.AddJsonBody(@"{  
  ""header"": {
    ""security"": {
            ""user"": ""WS2071382"",
      ""password"": ""MTIzNA ==""
    }
    }
}");

        var response = client.Execute(request);

        string str_response = response.Content.Substring(response.Content.IndexOf("token")+8, response.Content.IndexOf("objectDTO") - response.Content.IndexOf("token")-12);
        txt_token.Text = str_response;

    }

    protected void btn_send_Click(object sender, EventArgs e)
    {
       string json = txt_json.Text.Replace("{token}", txt_token.Text);


        RestClient client = new RestClient("http://qaglp.paquetexpress.mx:7007/");

        var request = new RestRequest(Method.POST);

        request.Resource = "WsQuotePaquetexpress/api/apiQuoter/v2/getQuotation";
        request.RequestFormat = DataFormat.Json;

        request.AddJsonBody(json);

        var response = client.Execute(request);


        dynamic json_Response = JValue.Parse(response.Content);
 

        var quotations =json_Response.body.response.data.quotations;

        List<PE_API_Quotation> t = quotations.ToObject<List<PE_API_Quotation>>();
        txt_response.Text = response.Content;


    }
}
public class PE_API_Quotation
{
    public string serviceType { get; set; }
    public string id { get; set; }
    public string idRef { get; set; }
    public string serviceName { get; set; }
    public string serviceInfoDescr { get; set; }
    public string serviceInfoDescrLong { get; set; }
    public string cutoffDateTime { get; set; }
    public string cutoffTime { get; set; }
    public string maxRadTime { get; set; }
    public string maxBokTime { get; set; }
    public bool onTime { get; set; }
    public string promiseDate { get; set; }
    public int promiseDateDaysQty { get; set; }
    public int promiseDateHoursQty { get; set; }
    public bool inOffer { get; set; }
    public ShipmentDetail shipmentDetail { get; set; }
    public PE_API_Services services { get; set; }
    public PE_API_Amount amount { get; set; }
}

public class ShipmentDetail
{
    public List<PE_API_Shipment> shipments { get; set; }
}
public class PE_API_Shipment
{
    public int sequence { get; set; }
    public int quantity { get; set; }
    public string shpCode { get; set; }
    public double weight { get; set; }
    public double longShip { get; set; }
    public double widthShip { get; set; }
    public double highShip { get; set; }
    public string slabNo { get; set; }
    public double volume { get; set; }
    public double slabDisc { get; set; }
    public double slabTax { get; set; }
    public double slabTaxRet { get; set; }
    public double slabAmount { get; set; }
    public string cpny { get; set; }
}
public class PE_API_Services
{
    public string dlvyType { get; set; }
    public string ackType { get; set; }
    public double totlDeclVlue { get; set; }
    public string invType { get; set; }
    public string radType { get; set; }
    public double dlvyTypeAmt { get; set; }
    public double dlvyTypeAmtDisc { get; set; }
    public double dlvyTypeAmtTax { get; set; }
    public double dlvyTypeAmtRetTax { get; set; }
    public double radTypeAmt { get; set; }
    public double radTypeAmtDisc { get; set; }
    public double radTypeAmtTax { get; set; }
    public double radTypeAmtRetTax { get; set; }
}

public class PE_API_Amount
{
    public double shpAmnt { get; set; }
    public double discAmnt { get; set; }
    public double srvcAmnt { get; set; }
    public double subTotlAmnt { get; set; }
    public double taxAmnt { get; set; }
    public double taxRetAmnt { get; set; }
    public double totalAmnt { get; set; }
}
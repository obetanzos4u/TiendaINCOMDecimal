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

public partial class TestSap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
           // Response.TrySkipIisCustomErrors = true;
          
            Page.Title = "TestSap";
 
        }
    }

    protected void btn_obtener_Click(object sender, EventArgs e)
    {
        try
        {
 
        this.InvokeService(txt_numero_parte.Text);
        }
        catch (Exception ex)
        {

            materializeCSS.crear_toast(this.Page, "Error: " + ex.Message, false);
        }


    }


 

    public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
    public void InvokeService(string numero_parte)
    {
        //Calling CreateSOAPWebRequest method  
        HttpWebRequest request = CreateSOAPWebRequest();



        System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        XmlDocument SOAPReqBody = new XmlDocument();
        //SOAP Body Request  
        SOAPReqBody.LoadXml(@"<soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:glob=""http://sap.com/xi/SAPGlobal20/Global"" xmlns:a0v=""http://sap.com/xi/AP/CustomerExtension/BYD/A0VKF"">
   <soap:Header/>
   <soap:Body>
      <glob:MaterialByElementsQuery_sync>
         <!--Optional:-->
         <MaterialSelectionByElements>
            <SelectionByInternalID>
               <InclusionExclusionCode>I</InclusionExclusionCode>
               <IntervalBoundaryTypeCode>1</IntervalBoundaryTypeCode>
               <LowerBoundaryInternalID schemeID=""?"" schemeAgencyID =""?"">" + numero_parte + @"</LowerBoundaryInternalID> 
            </SelectionByInternalID>
         </MaterialSelectionByElements>
         <ProcessingConditions>
            <QueryHitsMaximumNumberValue>100</QueryHitsMaximumNumberValue>
            <QueryHitsUnlimitedIndicator>false</QueryHitsUnlimitedIndicator>
         </ProcessingConditions>
      </glob:MaterialByElementsQuery_sync>
    </soap:Body>
</soap:Envelope>");


        using (Stream stream = request.GetRequestStream())
        {
            SOAPReqBody.Save(stream);
        }
        //Geting response from request  
        using (WebResponse Serviceres = request.GetResponse())
        {
            using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
            {

                var ServiceResult = rd.ReadToEnd();




                XDocument doc = XDocument.Parse(ServiceResult);
                Dictionary<string, string> dataDictionary = new Dictionary<string, string>();

                foreach (XElement element in doc.Descendants().Where(p => p.HasElements == false))
                {
                    int keyInt = 0;
                    string keyName = element.Name.LocalName;

                    while (dataDictionary.ContainsKey(keyName))
                    {
                        keyName = element.Name.LocalName + "_" + keyInt++;
                    }

                    dataDictionary.Add(keyName, element.Value);
                }

                div_respuesta.InnerHtml = "";

                Console.WriteLine("\n \n  ***     Respuesta parseada     *** \n \n");
                foreach (KeyValuePair<string, string> entry in dataDictionary)
                {

                    div_respuesta.InnerHtml += "<strong>" + entry.Key + " </strong> : " + entry.Value + " </br>";
                   // Console.WriteLine(entry.Key + " : " + entry.Value);
                }

                //  Console.WriteLine("\n \n  ***     Respuesta en bruto     *** \n \n");

                //  Console.WriteLine(ServiceResult);



              //  Console.ReadLine();
            }
        }
    }

    public HttpWebRequest CreateSOAPWebRequest()
    {
        //Making Web Request  
        HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(@"https://my351458.sapbydesign.com/sap/bc/srt/scs/sap/querymaterialin?sap-vhost=my351458.sapbydesign.com");
        //SOAPAction  
        //   Req.Headers.Add(@"SOAPAction:http://tempuri.org/Addition");
        //Content_type  
        Req.ContentType = "application/soap+xml;charset=\"utf-8\"";
        //    Req.Accept = "text/xml";
        //HTTP method  
        Req.Method = "POST";
        //return HttpWebRequest  

        Req.Credentials = new NetworkCredential("_20200317 TE", "20200317CoronaviruS!");




        // Req.ProtocolVersion = HttpVersion.Version10;
        // Req.ClientCertificates.Add(new X509Certificate2(@"C:\cer\incom.mx.pfx", "309253934_apolo"));

         





        return Req;
    }
}
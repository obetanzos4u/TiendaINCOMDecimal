using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Descripción breve de SAP_conexion
/// </summary>
public class SAP_conexion
{

    private string WebRequestURL { get;set;}


    /// <summary>
    /// Establece la URL para consumir el [HttpWebRequest] al llamar CreateSOAPWebRequest()
    /// </summary>
    public SAP_conexion(string _WebRequestURL)
    {
        WebRequestURL = _WebRequestURL;
    }
    public SAP_conexion() { }

    protected  HttpWebRequest CreateSOAPWebRequest(string WebRequestURL)
    {
        //Making Web Request  
        HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(WebRequestURL);
        //SOAPAction  
        //   Req.Headers.Add(@"SOAPAction:http://tempuri.org/Addition");
        //Content_type  
        Req.ContentType = "application/soap+xml;charset=\"utf-8\"";
        //    Req.Accept = "text/xml";
        //HTTP method  
        Req.Method = "POST";
        //return HttpWebRequest  

        Req.Credentials = new NetworkCredential("_20200523_CS", "28woodFACTORS!egypt93");
        return Req;
    }
}
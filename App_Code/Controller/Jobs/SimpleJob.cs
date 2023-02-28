using Quartz;
using Quartz.Impl;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Xml;

/// <summary>
/// Descripción breve de SimpleJob
/// </summary>
public class SimpleJob : IJob
{
    public SimpleJob()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            DataTable hitsCategorias = BI.ObtenerCategoríasBI();
            var categoryLoadResult = await LoadCategoryHits(hitsCategorias);
            if (categoryLoadResult.response)
            {
                Console.WriteLine("Completado");
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Error al cargar los hits de categoría en SAP", ex);
        }
        throw new NotImplementedException();
    }
    static public async Task<json_respuestas> LoadCategoryHits(DataTable hits)
    {
        HttpWebRequest request = CreateSOAPWebRequest();
        System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
        if (hits.Rows.Count > 0)
        {
            string dataXML = "<Data>ID;CONCEPT;DATE;USER;CONCEPTID;SEARCHES;";
            string date = utilidad_fechas.obtenerFechaSQL();
            int id = 1;
            foreach (DataRow row in hits.Rows)
            {
                string searches = row["Busquedas"].ToString();
                string concept = row["Concepto"].ToString().Replace("\"", "");
                string user = row["Usuario"].ToString();
                string conceptID = row["Id_categoria"].ToString();
                dataXML += $"\r{id};{concept};{date};{user};{conceptID};{searches};";
                id++;
            }
            dataXML = dataXML.TrimEnd(';');
            dataXML += "</Data>";
            if (!string.IsNullOrEmpty(dataXML))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml($@"
                <soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:glob=""http://sap.com/xi/SAPGlobal20/Global"">
                   <soap:Header/>
                   <soap:Body>
                      <glob:AnalyticsDataUploadRequest>
                          <AnalyticsDataUploadRequestContent>
                            <!-- ID de Fuente de datos de nube -->
                            <MDAVName>ZCAT_SEARCH_INCOM_MX</MDAVName>
                            <!-- Método de importación, puede ser MERGE, OVERWRITE o DELETE -->
                            <AnalyticsImportMethod>MERGE</AnalyticsImportMethod>
                            <!-- Datos separados por ; y saltos de linea, no debe haber ningún salto de línea después de <Data> ni antes de </Data> -->
                            {dataXML}
                         </AnalyticsDataUploadRequestContent>
                      </glob:AnalyticsDataUploadRequest>
                   </soap:Body>
                </soap:Envelope>
                ");
                using (Stream stream = request.GetRequestStream())
                {
                    xmlDoc.Save(stream);
                }
                //IAsyncResult asyncResult = request.BeginGetResponse(null, null);
                //asyncResult.AsyncWaitHandle.WaitOne();
                //string soapResult;
                //using (WebResponse response = request.EndGetResponse(asyncResult)) {
                //    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                //    {
                //        soapResult = reader.ReadToEnd();
                //        if (soapResult != null)
                //        {
                //            return new json_respuestas(true, "Cargado con éxito");
                //        }
                //        return new json_respuestas(false, "No cargado");
                //    }
                //}
                using (WebResponse service = await request.GetResponseAsync())
                {
                    using (StreamReader reader = new StreamReader(service.GetResponseStream()))
                    {
                        var serviceResult = reader.ReadToEnd();
                        XmlDocument xmlDocument = new XmlDocument();
                        if (serviceResult != null)
                        {
                            return new json_respuestas(true, "Cargado con éxito");
                        }
                        else
                        {
                            return new json_respuestas(false, "Error al cargar la información");
                        }
                    }
                }
            }
            return new json_respuestas(false, "Sin datos");
        }
        return new json_respuestas(false, "Sin hits");
    }
    static protected HttpWebRequest CreateSOAPWebRequest()
    {
        HttpWebRequest request;
        request = (HttpWebRequest)WebRequest.Create(@"https://my429740.businessbydesign.cloud.sap/sap/bc/srt/scs/sap/analyticsdatauploadin?sap-vhost=my429740.businessbydesign.cloud.sap");
        request.Credentials = new NetworkCredential("ASALINAS", "Pruebas00");
        //request = (HttpWebRequest)WebRequest.Create(@"https://my338095.sapbydesign.com/sap/bc/srt/scs/sap/managesalesorderin5?sap-vhost=my338095.sapbydesign.com");
        //request.Credentials = new NetworkCredential("INCOM.MX", "Sistema$20.22");
        request.ContentType = "text/xml;charset='utf-8'";
        request.Method = "POST";
        return request;
    }
    static protected bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
}
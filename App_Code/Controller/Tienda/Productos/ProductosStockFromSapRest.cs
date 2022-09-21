using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;


public class ProductosStockFromSapRestModel
{
    public string site { get; set; }
    public decimal quantity { get; set; }
    public string unitCode { get; set; }
    public string unitCodeText { get; set; }
    public string material { get; set; }

    public string PlanningAreaName { get; set; } 
}
/// <summary>
/// Descripción breve de ProductosStockFromSapRest
/// </summary>
public class ProductosStockFromSapRest
{
    #region Variables Accesos
    private readonly static bool ForceProductivo = true;
    private readonly static string host = ForceProductivo  ? "incom.mx": HttpContext.Current.Request.Url.Host;

    private readonly static string _user = host == "localhost" || host == "test1.incom.mx" ?
        WebConfigurationManager.AppSettings["UserRestPruebasSAP"] :
        WebConfigurationManager.AppSettings["UserRestProductivoSAP"];

    private readonly static string _pwn = host == "localhost" || host == "test1.incom.mx" ?
        WebConfigurationManager.AppSettings["PassRestPruebasSAP"] :
        WebConfigurationManager.AppSettings["PassRestProductivoSAP"];

    private readonly static string _userTecnico = "_STOCKCONVER";
    private readonly static string _pwnTecnico = "Pericoverde$2021%/r";

  
    private readonly static string UrlSap = host == "localhost" || host == "test1.incom.mx" ?
        WebConfigurationManager.AppSettings["UrlSapPruebas"] :
        WebConfigurationManager.AppSettings["UrlSapProductivo"];
    #endregion
    public ProductosStockFromSapRest()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }


    //public static async void GuardarStockRegistroFaltaDeStock(StockRegistroFaltaDeStock Registro )
    //{
    //    try
    //    {

    //        using (var db = new tiendaEntities())
    //        {
    //            var PedidoDatos = db.StockRegistroFaltaDeStocks.Add(Registro);
    //            db.SaveChanges();
    //        }
          
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex);
    //    }
    //}
  
 

    public static async Task<json_respuestas> ConvertirUnidadProductoAsync(string numero_parteSAP)
    {
        try
        {
            var client = new RestClient("https://my338095.sapbydesign.com/");
            client.Authenticator = new HttpBasicAuthenticator(_userTecnico, _pwnTecnico);
            client.Timeout = -1;

            var request = new RestRequest("sap/bc/srt/scs/sap/yylgqo6riy_managestockconverti?sap-vhost=my338095.sapbydesign.com", Method.POST);
            request.AddHeader("Content-Type", "text/xml");

            request.AddParameter("text/xml",
                @"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:glob=""http://sap.com/xi/SAPGlobal20/Global"">

                     <soapenv:Header/>
                     <soapenv:Body>

                              <glob:StockConvertionUpdateRequest_sync>

                             <BasicMessageHeader>
                             </BasicMessageHeader>
                             <StockConvertion>

                                <SAP_UUID>00163eae-a1ee-1edc-94c0-65356c401720</SAP_UUID>
		                    <material>" + numero_parteSAP + @"</material>
                             </StockConvertion>
                           </glob:StockConvertionUpdateRequest_sync>
                       </soapenv:Body>
                    </soapenv:Envelope>", ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);

            // Si el usuario no tiene acceso o permisos
            if (response.Content.Contains("Logon Error Message"))

                return new json_respuestas(false, "Error al iniciar sesión", false, null);

            // Si la llamada XML contiene ERRORES
            if (response.Content.Contains("Web service processing error"))

                return new json_respuestas(false, "Web service processing error ", false, null);

            // Si la llamada XML es CORRECTA
            if (response.Content.Contains("Update operation was successful"))

                return new json_respuestas(true, "Consulta realiza con éxito: " + response.Content, true, null);




           return new json_respuestas(true, "Resultado inesperado" + response.Content, true, null);
        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Error al realizar la consulta de conversión:" + ex.ToString(), true, null);
        }
    }
 

    public static async Task<json_respuestas> ConsultarStockV2SAPAsync()
    {
        try
        {
   


            var client = new RestClient(UrlSap);
            client.Authenticator = new HttpBasicAuthenticator(_user, _pwn);

            var request =
                new RestRequest($"/sap/byd/odata/cust/v1/stockv2/StockConvertionRootCollection?$filter=id eq '1'&$expand=StockConvertionsite&$format=json", Method.POST);

            var response = await client.ExecuteGetAsync(request);

           dynamic JsonREsult = Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content);

        
            dynamic StockConvertionsite = JsonREsult.d.results[0].StockConvertionsite;

            foreach(dynamic v in StockConvertionsite)
            {
                v.Property("__metadata").Remove();
                v.Property("ObjectID").Remove();
                v.Property("ParentObjectID").Remove();
                v.Property("StockConvertionRoot").Remove();

            }

            List< ProductosStockFromSapRestModel> Productos = JsonConvert.DeserializeObject<List<ProductosStockFromSapRestModel>>(StockConvertionsite.ToString());


            foreach(var p in Productos)
            {
              
                p.PlanningAreaName = SAP_productos.obtenerPlanningArea().Find(x => x.PlanningAreaID == p.site).PlanningAreaName;
            }
            return new json_respuestas(true, "Consulta realizada con éxito.", true, Productos);

        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Error al realizar la consulta de stock:" + ex.ToString(), true, null);
        }
        return new json_respuestas();
    }



    public json_respuestas ActualizarStockLocal()
    {
        return null;
    }
    public json_respuestas ActualizarStockLocalTodos()
    {
        return null;
    }
    public json_respuestas GuardarRegistroConsulta()
    {
        return null;
    }
}
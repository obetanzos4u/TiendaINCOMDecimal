using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Descripción breve de SAP_productos_oData
/// </summary>
/// 
public class SAP_productos_oData_MetaData_Model {
    public string uri { get; set; }
    public string type { get; set; }
}


public class SAP_productos_oData_Model {
 
    public string idProductoSAP { get; set; }
    public string numero_parte { get; set; }

    public string Almacen { get; set; }
    public string AlmacenID { get; set; }
    public string Unidad { get; set; }
    public string CPROJECTED_STOCK_QTY_UOM { get; set; }
    public decimal StockAlmacen { get; set; }
    public decimal DemandaCliente { get; set; }
    public decimal StockDisponible { get; set; }

}
public class SAP_productos_oData {
    private static string _user = "CONSULTAS";
    private static string _pwn = "SFA_NazaA!35";
    public SAP_productos_oData() { }

    public static async Task<List<SAP_productos_oData_Model>> obtenerStockAsync() {

        var client = new RestClient("https://my338095.sapbydesign.com/");
        client.Authenticator = new HttpBasicAuthenticator(_user, _pwn);

        RestRequest request =
            new RestRequest($"sap/byd/odata/crm_woc_salesorders_analytics.svc/RPZ3CFEC590A236E733BD9701QueryResults?$select=CPRODUCT_ID,CSPA_ID,CQUANTITY_UOM,TQUANTITY_UOM,KRZAC52B3549F1E886FD1FA4D,KRZ38A3122568DF31A282B12B,KRZE30C9F39E18D5F860CF537&$top=60000&$format=json");

        var response = await client.ExecuteGetAsync(request);

        /*
            • Id de producto (CPRODUCT_ID) 
            • Sede (CSPA_ID)
            • Stock en almacenes (KRZ38A3122568DF31A282B12B)
            • Demanda de cliente (KRZAC52B3549F1E886FD1FA4D)
            • Stock disponible (KRZE30C9F39E18D5F860CF537).
        */

        var strResult = response.Content
           .Replace("CPRODUCT_ID", "idProductoSAP")
           .Replace("KRZ38A3122568DF31A282B12B", "StockAlmacen")
           .Replace("KRZAC52B3549F1E886FD1FA4D", "DemandaCliente")
           .Replace("KRZE30C9F39E18D5F860CF537", "StockDisponible")

            .Replace("TQUANTITY_UOM", "Unidad")
             .Replace("CSPA_ID", "AlmacenID")



              .Replace("{\"d\":{\"results\":", "")

           ;
        strResult = strResult.Substring(0, strResult.Length - 2);




        var result = JsonConvert.DeserializeObject<List<SAP_productos_oData_Model>>(strResult);
        //result.numero_parte = numero_parte;

        return result;

    }
    public static async Task<List<SAP_productos_oData_Model>> obtenerStockAsync(string numero_parte_SAP, string numero_parte) {

        var client = new RestClient("https://my338095.sapbydesign.com/");
        client.Authenticator = new HttpBasicAuthenticator(_user, _pwn);

        var request =
            new RestRequest($"sap/byd/odata/crm_woc_salesorders_analytics.svc/RPZ3CFEC590A236E733BD9701QueryResults?$select=CPRODUCT_ID,CSPA_ID,CPROJECTED_STOCK_QTY_UOM,TPROJECTED_STOCK_QTY_UOM,KRZAC52B3549F1E886FD1FA4D,KRZ38A3122568DF31A282B12B,KRZE30C9F39E18D5F860CF537&$filter=(CPRODUCT_ID eq'{numero_parte}')&$format=json");

        var response = await client.ExecuteGetAsync(request);

        /*
            • Id de producto (CPRODUCT_ID) 
            • Sede (CSPA_ID)
            • Stock en almacenes (KRZ38A3122568DF31A282B12B)
            • Demanda de cliente (KRZAC52B3549F1E886FD1FA4D)
            • Stock disponible (KRZE30C9F39E18D5F860CF537).
        */

        var strResult = response.Content
           .Replace("CPRODUCT_ID", "idProductoSAP")
           .Replace("KRZ38A3122568DF31A282B12B", "StockAlmacen")
           .Replace("KRZAC52B3549F1E886FD1FA4D", "DemandaCliente")
           .Replace("KRZE30C9F39E18D5F860CF537", "StockDisponible")

            .Replace("TPROJECTED_STOCK_QTY_UOM", "Unidad")
             .Replace("CSPA_ID", "AlmacenID")



              .Replace("{\"d\":{\"results\":", "")

           ;
        strResult = strResult.Substring(0, strResult.Length - 2);




        var result = JsonConvert.DeserializeObject<List<SAP_productos_oData_Model>>(strResult);
        //result.numero_parte = numero_parte;

        return result;

    }

    public static async Task<string> ObtenerStockMensajeAsync(string numero_parte_SAP, string numero_parte) {


        var Stock = await obtenerStockAsync(numero_parte_SAP, numero_parte);

        var StockTotal = Stock.Sum(p => p.StockDisponible);
        if (Stock != null) {


            if (StockTotal >= 0) {
                return $"Disponibilidad inmediata ({StockTotal})"; }
            else if (StockTotal <= 0) {
                return "Disponibilidad bajo pedido";
            }
            else {
                return "Pregunta disponibilidad";

            }
        }
        else {

            return "Pregunta disponibilidad";
        }
    }
    /// <summary>
    /// 20210621  CM - Obtiene todos los productos del servicio oData de SAP y los guarda localmente en la tabla Productos_StockFromSap
    /// </summary>
    /// 
    //public static async Task<json_respuestas> ActualizarStockAsync() {
    //    var StockProductos = new List<SAP_productos_oData_Model>();

    //    #region Obtención de stock 
    //    try {
    //        StockProductos = await obtenerStockAsync();
    //    }
    //    catch (Exception ex) {


    //        return new json_respuestas(false, "Hubo un error al obtener el stock de los productos desde SAP OData.", true, ex);
    //    }
    //    #endregion


    //    string ProductosNoCargados = "";
    //    using (var ctx = new tiendaEntities()) {
    //        ctx.Database.ExecuteSqlCommand("TRUNCATE TABLE [Productos_StockFromSap]");
    //    }
    //    var PlanningArea = SAP_productos.obtenerPlanningArea();
    //    foreach (var p in StockProductos) {

    //        if (string.IsNullOrWhiteSpace(p.idProductoSAP) || string.IsNullOrWhiteSpace(p.AlmacenID)) continue;
    //        var pStock = new Productos_StockFromSap();

    //        try { 
    //        pStock.Almacen = PlanningArea.Find(x => x.PlanningAreaID == p.AlmacenID).PlanningAreaName;
    //        }
    //        catch(Exception ex) {

    //            pStock.Almacen = "N/A";

    //        }

    //        pStock.AlmacenID = p.AlmacenID;
    //        pStock.StockAlmacen = p.StockAlmacen;
    //        pStock.StockDisponible = p.StockDisponible;
    //        pStock.CPROJECTED_STOCK_QTY_UOM = p.CPROJECTED_STOCK_QTY_UOM;
    //        pStock.DemandaCliente = p.DemandaCliente;
    //        pStock.idProductoSAP = p.idProductoSAP;
    //        pStock.Unidad = p.Unidad; 

    //        using (var ctx = new tiendaEntities()) {
    //            using (DbContextTransaction transaction = ctx.Database.BeginTransaction()) {
    //                try {
    //                    ctx.Productos_StockFromSap.Add(pStock);
    //                    ctx.SaveChanges();


    //                    transaction.Commit();
    //                }
    //                catch (Exception ex) {
    //                    transaction.Rollback();

    //                    ProductosNoCargados += $"{ p.idProductoSAP}, ";
    //                }

    //            }
    //        }
    //    }


    //    return new json_respuestas(true, "Proceso realizado con éxito, productos con errores: " + ProductosNoCargados, false, null);
    //} 
}
       
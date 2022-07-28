using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 
public partial class herramientas_cargar_bloqueo_productos_disponibles : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack) {
           
        }
    }






    protected void btn_cargar_listado_productos_Click(object sender, EventArgs e)
    {
        txt_log_result.Text = "";

        string filePath = "";
        using (var db = new tiendaEntities())
        {
            using (DbContextTransaction transaction = db.Database.BeginTransaction())
            {
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE [ProductosBloqueoStock]");
               
                try
                {
                    filePath = HttpContext.Current.Server.MapPath("~") + @"temp\" + Path.GetFileName(FileUpload1.PostedFile.FileName);
                    FileUpload1.SaveAs(filePath);

                    SLDocument sl = new SLDocument(filePath, "ProductosBloqueoStock");



                    int iRow = 2;
                    while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)) || !string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 2)))
                    {
                        string numero_parte = sl.GetCellValueAsString(iRow, 1);
                        
                        Decimal DisponibleMaximo = sl.GetCellValueAsDecimal(iRow, 2);
                        

                        var Registro = new ProductosBloqueoStock();


                        Registro.numero_parte = numero_parte;
                        Registro.Disponible = DisponibleMaximo;
                      
                      

                        db.ProductosBloqueoStocks.Add(Registro);
                        db.SaveChanges();



                        iRow++;
                    }

                    transaction.Commit();
                    txt_log_result.Text = "Registros cargados con éxito";

                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    txt_log_result.Text = "Ocurrio un error al guardar un registro. <br>" + ex.ToString();


                }
                finally
                {

                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }

                     
                }
            }
        }
    }




    protected async void btn_EliminarTodosLosRegistros_Click(object sender, EventArgs e)
    {


        var result = await ProductosBloqueoCantidades.EliminarTodosLosRegistrosAsync();

        txt_log_result.Text = result.message;
    }

    protected async void btn_CargarYLimitarFromSAP_Click(object sender, EventArgs e)
    {

        string[] Productos = txt_ListadoProductosActualizarFromSAP.Text.Replace("\r","").Split('\n');

        Productos = Productos.Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();

       var ListadoProductos = new List<ProductosBloqueoCantidades.ProductoBloqueoCantidadStruct>();

        string LoResultado = "";



        foreach (var  ProductoNumPart in Productos)
        {
            string numero_parte = textTools.lineSimple(ProductoNumPart);


            string numero_parteSAP =await  ProductosTiendaEF.ObtenerNumeroParteSAP(numero_parte);


            if (numero_parteSAP == null) {
                LoResultado += $"El producto: {numero_parte} no tiene número de parte SAP ó ocurrio un error. \r\n";
                continue;
            }

             var ResultObtenerStockFromSAP = 0m;

            #region Obteniendo stock
            try
            {
                var resultConvert = await ProductosStockFromSapRest.ConvertirUnidadProductoAsync(numero_parteSAP);

                if (resultConvert.result == false) {
                    LoResultado += $"El producto: {numero_parte} no se realizo la conversión correcta de su stock \r\n";
                    continue; 
                }

                var resultObtenerStock = await ProductosStockFromSapRest.ConsultarStockV2SAPAsync();

                if (resultObtenerStock.result == false) {
                    LoResultado += $"El producto: {numero_parte} no se pudo obtener  su stock de SAP \r\n";
                    continue;
                }

                ResultObtenerStockFromSAP = ((List<ProductosStockFromSapRestModel>)resultObtenerStock.response).Sum(p => p.quantity);



            }
            catch(Exception ex)
            {
                LoResultado += $"El producto: {numero_parte} no se pudo obtener su stock desde SAP \r\n";
                continue;
            }
            #endregion
            var Element = new ProductosBloqueoCantidades.ProductoBloqueoCantidadStruct();

            Element.numero_parte = numero_parte;
            Element.cantidadMaximaDisponible = ResultObtenerStockFromSAP;

            ListadoProductos.Add(Element);

            // Una vez obtenido el listado, proseguimos a actualizar el bloqueo
            
                var result = await ProductosBloqueoCantidades.CargarListadoProductosBloqueoMax(ListadoProductos);
            LoResultado += result.message + "\r\n";



        }
        txt_log_result.Text =  LoResultado;
    }
}
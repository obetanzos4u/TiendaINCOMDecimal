using ClosedXML.Excel;
using FastMember;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class herramientas_admin_precios : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack) {
            Title = "Admin precios";
        }
    }




    protected async void btn_EliminarTodosLosRegistros_Click(object sender, EventArgs e)
    {


        var result = await ProductosBloqueoCantidades.EliminarTodosLosRegistrosAsync();

        txt_log_result.Text = result.message;
    }

    protected async void btn_CargarYLimitarFromSAP_Click(object sender, EventArgs e)
    {

        string[] Productos = txt_ListadoProductosActualizarPrecio.Text.Replace("\r", "").Split('\n');

        Productos = Productos.Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();

        var ListadoProductos = new List<ProductosBloqueoCantidades.ProductoBloqueoCantidadStruct>();

        string LoResultado = "";



        foreach (var ProductoNumPart in Productos)
        {
            string numero_parte = textTools.lineSimple(ProductoNumPart);


            string numero_parteSAP = await ProductosTiendaEF.ObtenerNumeroParteSAP(numero_parte);


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
            catch (Exception ex)
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
        txt_log_result.Text = LoResultado;
    }

   
    private string[] ObtenerColumnas()
    {

        string[] Columnas = txt_Columns.Text.Split(',');
        Columnas.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        return Columnas;
    }
    private string[] ObtenerProductos()
    {
        string[] Productos = txt_ListadoProductosActualizarPrecio.Text.Replace("\r", "").Split('\n');

        Productos = Productos.Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();
        return Productos;
    }
    private DataTable ObtenerEstructuraTabla()
    {
        var Columnas = ObtenerColumnas();
        var dtProductos = new DataTable();

        foreach (string Column in Columnas)
        {
            dtProductos.Columns.Add(Column, typeof(string));

        }

        return dtProductos;
    }
        protected void btn_ObtenerVistaPrevia_Click(object sender, EventArgs e)
    {

        var Columnas = ObtenerColumnas();
        var dtProductos = ObtenerEstructuraTabla();


        int contador = 0;
        string[] Productos = ObtenerProductos();

        foreach (var p in Productos)
        {

            if (contador > 30) break;
            string[] Producto = p.Split('\t');

            if (Producto.Length < Columnas.Length)
            {
                txt_log_result.Text += "El producto [" + Producto[0]+ "] no contiene las columnas necesarias \n.";
                continue;
            }

 
                DataRow dr = dtProductos.NewRow();

            for (int i = 0; i < Columnas.Length; i++)
            {
              
                dr[Columnas[i]] = Producto[i];
            }
            dtProductos.Rows.Add(dr);
            contador= contador+1;
        }

        dtProductos.AcceptChanges();

        gv_VistaPreview.DataSource = dtProductos;
        gv_VistaPreview.DataBind();
    }


    // Se tuvo que cambiar la lógica de último momento por urgencia de la solicitud
    protected void btn_ObtenerVistaPrevia2_Click(object sender, EventArgs e)
    {

        txt_log_result.Text = "";
        var dtPeview = new DataTable();

      

        string[] Productos = ObtenerProductos();
        if(Productos.Length == 0)
        {
            txt_log_result.Text = "No se ha especificado la fuente de datos o estos no tienen el formato correcto";
            return;
        }
        // Se toma solo la primera linea para obtener la cantidad de columnas.
        var ProductoBase = Productos[0].Split('\t');

        foreach (string Column in ProductoBase)
        {
            dtPeview.Columns.Add(Column, typeof(string));

        }
        dtPeview.Columns.Add(new DataColumn("#", typeof(Int32)));

        Productos = Productos.Skip(1).ToArray();
        int contador = 1;

        foreach (var p in Productos)
        {
            if (contador > 30) break;
            DataRow dr = dtPeview.NewRow();

            var Producto = p.Split('\t');
            for (int i = 0; i < ProductoBase.Length; i++)
            {
                dr["#"] = contador;
                dr[i] = Producto[i];
            }
            dtPeview.Rows.Add(dr);
            contador++;
        }
     

        dtPeview.AcceptChanges();

        gv_VistaPreview.DataSource = dtPeview;
       
        gv_VistaPreview.DataBind();
    }
    protected async void btn_ValidarExistencia_Click(object sender, EventArgs e)
    {
        txt_log_result.Text = "";

    var Productos = ObtenerProductos();


        Productos = Productos.Skip(1).ToArray();

        foreach (var p in Productos)
        {
            string[] columns = p.Split('\t');
            var ObtenerNoParte = await ProductosTiendaEF.ObtenerSoloNumerosParte(columns[0]);

            if(ObtenerNoParte != null)
            {

            
                txt_log_result.Text += "El producto [" + columns[0] + "] SI existe en p.Datos. \n";

                var resultRangos = await ProductosPreciosEF.ObtenerPreciosRangos(ObtenerNoParte.numero_parte);
                if (resultRangos != null) txt_log_result.Text += "El producto [" + columns[0] + "] SI existe en p.Rangos. \n";
                else txt_log_result.Text += "El producto [" + columns[0] + "] NO existe en p.Datos. \n";


                var resultFantasma = await ProductosPreciosEF.ObtenerPreciosFantasma(ObtenerNoParte.numero_parte);
                if (resultFantasma != null) txt_log_result.Text += "El producto [" + columns[0] + "] SI existe en p.Fantasma. \n";
                else txt_log_result.Text += "El producto [" + columns[0] + "] NO existe en p.Fantasma. \n";

                var resultCotizalo = await ProductosPreciosEF.ObtenerProductoCotizalo(ObtenerNoParte.numero_parte);
                if (resultCotizalo != null) txt_log_result.Text += "El producto [" + columns[0] + "] SI existe en p.Cotízalo. \n";
                else txt_log_result.Text += "El producto [" + columns[0] + "] NO existe en p.Cotízalo. \n";

                txt_log_result.Text += "\n **************** \n";
            }
            else
            {
                txt_log_result.Text += "El producto [" + columns[0] + "] NO existe en p.Datos. \n";

                var resultRangos = await ProductosPreciosEF.ObtenerPreciosRangos(columns[0]);
                if (resultRangos != null) txt_log_result.Text += "El producto [" + columns[0] + "] SI existe en p.Rangos. \n";
                else txt_log_result.Text += "El producto [" + columns[0] + "] NO existe en p.Datos. \n";


                var resultFantasma = await ProductosPreciosEF.ObtenerPreciosFantasma(columns[0]);
                if (resultFantasma != null) txt_log_result.Text += "El producto [" + columns[0] + "] SI existe en p.Fantasma. \n";
                else txt_log_result.Text += "El producto [" + columns[0] + "] NO existe en p.Fantasma. \n";

                var resultCotizalo = await ProductosPreciosEF.ObtenerProductoCotizalo(columns[0]);
                if (resultCotizalo != null) txt_log_result.Text += "El producto [" + columns[0] + "] SI existe en p.Cotízalo. \n";
                else txt_log_result.Text += "El producto [" + columns[0] + "] NO existe en p.Cotízalo. \n";

                txt_log_result.Text += "\n **************** \n";
            }
        }
          

    }

    protected async void btn_CargarPreciosFantasma_Click(object sender, EventArgs e)
    {

        try
        {
            txt_log_result.Text = "";

            string[] Productos = ObtenerProductos();


            Productos = Productos.Skip(1).ToArray();

            foreach (var p in Productos)
            {


             

                var Data = p.Split('\t');

                var resGetNumerosParte = await ProductosTiendaEF.ObtenerSoloNumerosParte(Data[0]);
                string numero_parte = "";
                if (resGetNumerosParte != null)
                {

                    numero_parte = resGetNumerosParte.numero_parte;
                }
                else
                {
                    numero_parte = Data[0];
                    txt_log_result.Text = $"\n El producto {Data[0]} no se encontro en Productos Datos o no tiene la columna número de parte SAP o ocurrio un error al obtener números de parte, " +
                        $"se procederá a cargar con el número de parte especificado: {numero_parte}. \n\n";
                }


                decimal PrecioFantasma =  textTools.toDecimal(Data[1]);


                var result = await ProductosPreciosEF.CargarPreciosFantasma(numero_parte, PrecioFantasma);

                txt_log_result.Text += result.message + "\n";


            }
        }
        catch (Exception ex)
        {


            txt_log_result.Text = "Ocurrio un error al procesar, revisa tu información o contacta desarrollo: \n\n" + ex.Message;
        }
    }

    protected async void btn_eliminarPreciosRangos_Click(object sender, EventArgs e)
    {

        try
        {
            txt_log_result.Text = "";

            string[] Productos = ObtenerProductos();


            Productos = Productos.Skip(1).ToArray();

            foreach (var p in Productos)
            {


                var Data = p.Split('\t');

                var resGetNumerosParte = await ProductosTiendaEF.ObtenerSoloNumerosParte(Data[0]);
                string numero_parte = "";
                if (resGetNumerosParte != null)
                {

                    numero_parte = resGetNumerosParte.numero_parte;
                }
                else
                {
                    numero_parte = Data[0];
                    txt_log_result.Text = $"\n El producto especificado {Data[0]} no se encontró en Productos Datos o no tiene la columna número de parte SAP o ocurrió un error al obtener números de parte, " +
                        $"se procederá a eliminar con el número de parte especificado: {numero_parte}. \n\n";
                }


                var result = await ProductosPreciosEF.EliminarPreciosRangos(numero_parte);

                txt_log_result.Text += result.message + "\n";


            }
        }
        catch (Exception ex)
        {


            txt_log_result.Text = "Ocurrio un error al procesar, revisa tu información o contacta desarrollo: \n\n" + ex.Message;
        }
    }

    protected void btn_eliminarPreciosListaFija_Click(object sender, EventArgs e)
    {

    }

    protected async  void btn_eliminarPreciosTodo_Click(object sender, EventArgs e)
    {
        try
        {
            txt_log_result.Text = "";

            string[] Productos = ObtenerProductos();


            Productos = Productos.Skip(1).ToArray();

            foreach (var p in Productos)
            {


                var Data = p.Split('\t');

                var resGetNumerosParte = await ProductosTiendaEF.ObtenerSoloNumerosParte(Data[0]);
                string numero_parte = "";
                if (resGetNumerosParte != null)
                {

                    numero_parte = resGetNumerosParte.numero_parte;
                }
                else
                {
                    numero_parte = Data[0];
                    txt_log_result.Text = $"\n El producto especificado {Data[0]} no se encontró en Productos Datos o no tiene la columna número de parte SAP o ocurrió un error al obtener números de parte, " +
                        $"se procederá a eliminar con el número de parte especificado: {numero_parte}. \n\n";
                }


                var resultPreciosRangos = await ProductosPreciosEF.EliminarPreciosRangos(numero_parte);
                txt_log_result.Text += resultPreciosRangos.message + "\n";

                var resultPreciosFantasma = await ProductosPreciosEF.EliminarPreciosFantasma(numero_parte);
                txt_log_result.Text += resultPreciosFantasma.message + "\n";


                var resultPrecioListaFija = await ProductosPreciosEF.EliminarPreciosListaFija(numero_parte);
                txt_log_result.Text += resultPrecioListaFija.message + "\n";

                var resultCotizalo = await ProductosPreciosEF.EliminarProductoCotizalo(numero_parte);
                txt_log_result.Text += resultCotizalo.message + "\n";


            }
        }
        catch (Exception ex)
        {


            txt_log_result.Text = "Ocurrio un error al procesar, revisa tu información o contacta desarrollo: \n\n" + ex.Message;
        }
    }

    protected async void btn_CargarPrecios1Rango_Click(object sender, EventArgs e)
    {

        try {
            txt_log_result.Text = "";

    string[] Productos = ObtenerProductos();


        Productos = Productos.Skip(1).ToArray();

        foreach (var p in Productos)
        {


            var Producto = new precios_rangos();
             
            var Data = p.Split('\t');

           var resGetNumerosParte = await ProductosTiendaEF.ObtenerSoloNumerosParte(textTools.lineSimple(Data[0]));
                string numero_parte = "";
        if (resGetNumerosParte != null)
                {

                    numero_parte = resGetNumerosParte.numero_parte;
                }
                else
                {
                    numero_parte = Data[0];
                    txt_log_result.Text = $"\n El producto {Data[0]} no se encontro en Productos Datos o no tiene la columna número de parte SAP o ocurrio un error al obtener números de parte, " +
                        $"se procederá a cargar con el número de parte especificado: {numero_parte}. \n\n";
                }

              


            Producto.numero_parte = textTools.lineSimple(numero_parte).ToUpper();
            Producto.moneda_rangos = textTools.lineSimple(Data[1]).ToUpper();
            Producto.precio1 = textTools.toDecimal(Data[2]);
            Producto.max1 = 99999999;

            var result = await ProductosPreciosEF.CargarPreciosRangos(Producto);

            txt_log_result.Text += result.message + "\n";


        }
        } catch(Exception ex)
        {


            txt_log_result.Text = "Ocurrio un error al procesar, revisa tu información o contacta desarrollo: \n\n" +ex.Message;
        }

    }

    protected async void btn_EliminarPreciosFantasma_Click_Click(object sender, EventArgs e)
    {
        try
        {
            txt_log_result.Text = "";

            string[] Productos = ObtenerProductos();


            Productos = Productos.Skip(1).ToArray();

            foreach (var p in Productos)
            {


                var Data = p.Split('\t');

                var resGetNumerosParte = await ProductosTiendaEF.ObtenerSoloNumerosParte(Data[0]);
                string numero_parte = "";
                if (resGetNumerosParte != null)
                {

                    numero_parte = resGetNumerosParte.numero_parte;
                }
                else
                {
                    numero_parte = textTools.lineSimple(Data[0]);
                    txt_log_result.Text = $"\n El producto especificado {Data[0]} no se encontró en Productos Datos o no tiene la columna número de parte SAP o ocurrió un error al obtener números de parte, " +
                        $"se procederá a eliminar con el número de parte especificado: {numero_parte}. \n\n";
                }


                var result = await ProductosPreciosEF.EliminarPreciosFantasma(numero_parte);

                txt_log_result.Text += result.message + "\n";


            }
        }
        catch (Exception ex)
        {


            txt_log_result.Text = "Ocurrio un error al procesar, revisa tu información o contacta desarrollo: \n\n" + ex.Message;
        }
    }
 
 
    protected   void btn_ObtenerListadoPreciosFantasma_Click(object sender, EventArgs e)
    {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        DataTable dtPreciosFantasma= new DataTable();
        cmd.Connection = con;

        using (con)
        {
            cmd.CommandText = @"SELECT * FROM precios_fantasma;";
            cmd.CommandType = CommandType.Text;



            DataSet ds = new DataSet();


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            dtPreciosFantasma = ds.Tables[0];
        }

       
        var workbook = new XLWorkbook();
        workbook.Worksheets.Add(dtPreciosFantasma, "precios_fantasma");  
         

        string FileName = Server.UrlEncode("precios_fantasma_" + utilidad_fechas.obtenerCentral().ToShortDateString() + ".xlsx");

        // Prepare the response
        HttpResponse httpResponse = Response;
        httpResponse.Clear();
        httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        httpResponse.AddHeader("content-disposition", $"attachment;filename={FileName}");

        // Flush the workbook to the Response.OutputStream
        using (MemoryStream memoryStream = new MemoryStream())
        {
            workbook.SaveAs(memoryStream);
            memoryStream.WriteTo(httpResponse.OutputStream);
            memoryStream.Close();
        }

        httpResponse.End();

    }

    protected   void btn_DescargarPreciosRangos_Click(object sender, EventArgs e)
    {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        DataTable dtPreciosRangos = new DataTable();
        cmd.Connection = con;

        using (con)
        {
            cmd.CommandText = @"SELECT * FROM precios_rangos;";
            cmd.CommandType = CommandType.Text;


       
            DataSet ds = new DataSet();
      
               
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(ds);
            dtPreciosRangos = ds.Tables[0];
        }

              
   
        var workbook = new XLWorkbook();
        workbook.Worksheets.Add(dtPreciosRangos, "precios_rangos");


        string FileName = Server.UrlEncode("precios_rangos_" + utilidad_fechas.obtenerCentral().ToShortDateString() + ".xlsx");

        // Prepare the response
        HttpResponse httpResponse = Response;
        httpResponse.Clear();
        httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        httpResponse.AddHeader("content-disposition", $"attachment;filename={FileName}");

        // Flush the workbook to the Response.OutputStream
        using (MemoryStream memoryStream = new MemoryStream())
        {
            workbook.SaveAs(memoryStream);
            memoryStream.WriteTo(httpResponse.OutputStream);
            memoryStream.Close();
        }

        httpResponse.End();
    }

    protected void btn_DescargarProductosCotizalo_Click(object sender, EventArgs e)
    {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        DataTable dtProductosSoloVisualizacion = new DataTable();
        cmd.Connection = con;

        using (con)
        {
            cmd.CommandText = @"SELECT * FROM productos_solo_visualizacion;";
            cmd.CommandType = CommandType.Text;



            DataSet ds = new DataSet();


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            dtProductosSoloVisualizacion = ds.Tables[0];
        }



        var workbook = new XLWorkbook();
        workbook.Worksheets.Add(dtProductosSoloVisualizacion, "productos_solo_visualizacion");


        string FileName = Server.UrlEncode("productos_solo_visualizacion_" + utilidad_fechas.obtenerCentral().ToShortDateString() + ".xlsx");

        // Prepare the response
        HttpResponse httpResponse = Response;
        httpResponse.Clear();
        httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        httpResponse.AddHeader("content-disposition", $"attachment;filename={FileName}");

        // Flush the workbook to the Response.OutputStream
        using (MemoryStream memoryStream = new MemoryStream())
        {
            workbook.SaveAs(memoryStream);
            memoryStream.WriteTo(httpResponse.OutputStream);
            memoryStream.Close();
        }

        httpResponse.End();
    }

    protected async void btn_CargarProductosCotizalo_Click(object sender, EventArgs e)
    {
        try
        {
            txt_log_result.Text = "";

            string[] Productos = ObtenerProductos();


            Productos = Productos.Skip(1).ToArray();

            foreach (var p in Productos)
            {


               

                var Data = p.Split('\t');

                var resGetNumerosParte = await ProductosTiendaEF.ObtenerSoloNumerosParte(textTools.lineSimple(Data[0]));
                string numero_parte = "";
                if (resGetNumerosParte != null)
                {

                    numero_parte = resGetNumerosParte.numero_parte;
                }
                else
                {
                    numero_parte = Data[0];
                    txt_log_result.Text = $"\n El producto {Data[0]} no se encontro en Productos Datos o no tiene la columna número de parte SAP o ocurrio un error al obtener números de parte, " +
                        $"se procederá a cargar con el número de parte especificado: {numero_parte}. \n\n";
                }



                 

                var result = await ProductosPreciosEF.EstablecerProductoEnCotizalo(numero_parte);

                txt_log_result.Text += result.message + "\n";


            }
        }
        catch (Exception ex)
        {


            txt_log_result.Text = "Ocurrio un error al procesar, revisa tu información o contacta desarrollo: \n\n" + ex.Message;
        }

    }

    protected async void btn_EliminarProductosCotizalo_Click(object sender, EventArgs e)
    {
        try {
            txt_log_result.Text = "";

            string[] Productos = ObtenerProductos();


            Productos = Productos.Skip(1).ToArray();

            foreach (var p in Productos) {


                var Data = p.Split('\t');

                var resGetNumerosParte = await ProductosTiendaEF.ObtenerSoloNumerosParte(Data[0]);
                string numero_parte = "";
                if (resGetNumerosParte != null) {

                    numero_parte = resGetNumerosParte.numero_parte;
                }
                else {
                    numero_parte = textTools.lineSimple(Data[0]);
                    txt_log_result.Text = $"\n El producto especificado {Data[0]} no se encontró en Productos Datos o no tiene la columna número de parte SAP o ocurrió un error al obtener números de parte, " +
                        $"se procederá a eliminar con el número de parte especificado: {numero_parte}. \n\n";
                }


                var result = await ProductosPreciosEF.EliminarProductoCotizalo(numero_parte);

                txt_log_result.Text += result.message + "\n";


            }
        }
        catch (Exception ex) {


            txt_log_result.Text = "Ocurrio un error al procesar, revisa tu información o contacta desarrollo: \n\n" + ex.Message;
        }
    }

    protected async void btn_EliminarTodosProductosCotizalo_Click(object sender, EventArgs e) {
        try {

            txt_log_result.Text = "";
            var result = await ProductosPreciosEF.EliminarTodosLosProductosCotizalo();
            txt_log_result.Text += result.message + "\n";

        }
        catch (Exception ex) {

            txt_log_result.Text = "Ocurrio un error al procesar, revisa tu información o contacta desarrollo: \n\n" + ex.Message;
        }
    }

    protected async void btn_ActualizarPreciosFromSAP_Click(object sender, EventArgs e) {
        try {
            string UrlPreciosDeLista = $"/sap/byd/odata/ana_businessanalytics_analytics.svc/RPZ9C7842058B456C57A10C97QueryResults?$select=CAMOUNTUATION7FFFD9EB1ED5ED81,TBASEQUUATION4577A75BCDF673FA,CDESCRIUATIONC4EF176F7D5DDDBB,CPRICESUATION503363A1609841F3,KCAMOUNTUATION5766473FFF195FF7,KCBASEQUUATION001CEA476E31CDC8&$filter=(CAMOUNTUATION7FFFD9EB1ED5ED81%20eq%20%27USD%27)%20and%20(CDESCRIUATIONC4EF176F7D5DDDBB%20eq%20%27PRECIOS_DE_LISTA%27)%20and%20(CPRICESUATION590459A04636B55A%20eq%20%27CND_PRODUCT_ID%27)%20and%20(CPRICESUATION314ADC9936AC87AE%20eq%20%277PR1%27)%20and%20(CSTATUSUATION7A4E5F5269D8C9F7%20eq%20%273%27)%20and%20(CSTATUSUATION25559F8733977D89%20eq%20%273%27)&$format=json&$top=10000&$inlinecount=allpages";
            string UrlPreciosCampañaIncom = $"/sap/byd/odata/ana_businessanalytics_analytics.svc/RPZ9C7842058B456C57A10C97QueryResults?$inlinecount=allpages&$select=CAMOUNTUATION7FFFD9EB1ED5ED81,TBASEQUUATION4577A75BCDF673FA,CDESCRIUATION3BB5DD9FCF4A00E6,CDESCRIUATIONC4EF176F7D5DDDBB,CPRICESUATION503363A1609841F3,KCAMOUNTUATION5766473FFF195FF7,KCBASEQUUATION001CEA476E31CDC8&$filter=(CAMOUNTUATION7FFFD9EB1ED5ED81%20eq%20%27MXN%27)%20and%20(CDESCRIUATIONC4EF176F7D5DDDBB%20eq%20%27INCOM.MX%27)%20and%20(CPRICESUATION590459A04636B55A%20eq%20%27CND_PRODUCT_ID%27)%20and%20(CPRICESUATION314ADC9936AC87AE%20eq%20%277PR1%27)%20and%20(CSTATUSUATION7A4E5F5269D8C9F7%20eq%20%273%27)%20and%20(CSTATUSUATION25559F8733977D89%20eq%20%273%27)&$top=5000&$format=json";
           
            txt_log_result.Text = "";
            var resultPreciosDeLista = await ProductosPreciosEF.ObtenerYActualizarPreciosFromSAP(UrlPreciosDeLista);
            var resultPreciosCampañaIncom = await ProductosPreciosEF.ObtenerYActualizarPreciosFromSAP(UrlPreciosCampañaIncom);
            txt_log_result.Text += resultPreciosDeLista.message + "\n";


        }
        catch (Exception ex) {

            txt_log_result.Text = "Ocurrio un error al procesar, revisa tu información o contacta desarrollo: \n\n" + ex.Message;
        }
    }

    protected async void btn_CargarPrecios5Rangos_Click(object sender, EventArgs e) {

        try {
            txt_log_result.Text = "";

            string[] Productos = ObtenerProductos();


            Productos = Productos.Skip(1).ToArray();

            foreach (var p in Productos) {


                var Producto = new precios_rangos();

                var Data = p.Split('\t');

                var resGetNumerosParte = await ProductosTiendaEF.ObtenerSoloNumerosParte(textTools.lineSimple(Data[0]));
                string numero_parte = "";
                if (resGetNumerosParte != null) {

                    numero_parte = resGetNumerosParte.numero_parte;
                }
                else {
                    numero_parte = Data[0];
                    txt_log_result.Text = $"\n El producto {Data[0]} no se encontro en Productos Datos o no tiene la columna número de parte SAP o ocurrio un error al obtener números de parte, " +
                        $"se procederá a cargar con el número de parte especificado: {numero_parte}. \n\n";
                }




                Producto.numero_parte = textTools.lineSimple(numero_parte).ToUpper();
                Producto.moneda_rangos = textTools.lineSimple(Data[1]).ToUpper();
              
                Producto.precio1 = textTools.toDecimal(Data[2]);
                Producto.max1 = textTools.toDecimal(Data[3]);


                Producto.precio2 = textTools.toDecimal(Data[4]);
                Producto.max2 = textTools.toDecimal(Data[5]);


                Producto.precio3 = textTools.toDecimal(Data[6]);
                Producto.max3 = textTools.toDecimal(Data[7]);


                Producto.precio4 = textTools.toDecimal(Data[8]);
                Producto.max4 = textTools.toDecimal(Data[9]);


                Producto.precio5 = textTools.toDecimal(Data[10]);
                Producto.max5 = textTools.toDecimal(Data[11]);


                var result = await ProductosPreciosEF.CargarPreciosRangos(Producto);

                txt_log_result.Text += result.message + "\n";


            }
        }
        catch (Exception ex) {


            txt_log_result.Text = "Ocurrio un error al procesar, revisa tu información o contacta desarrollo: \n\n" + ex.Message;
        }
    }
}
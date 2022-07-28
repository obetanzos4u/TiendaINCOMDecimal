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

public partial class herramientas_configuraciones_cargar_precios_fantasma : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack) {

            Title = "Carga información de precios fantasma.";
        }
    }



    protected async void btn_cargar_archivos_Click(object sender, EventArgs e)
    {
        string filePath = "";
        string log = string.Empty;
        try {
           

 
            string numero_parte = "";

            filePath = HttpContext.Current.Server.MapPath("~") + @"temp\" + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(filePath);

            SLDocument sl = new SLDocument(filePath, "PreciosFantasma");

            int cantidadProductosCargados = 0;

            int iRow = 2;
            bool errores = false;

            while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)) || !string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 2)))
            {

                numero_parte = textTools.lineSimple( sl.GetCellValueAsString(iRow, 1));
                Decimal? precio = sl.GetCellValueAsDecimal(iRow, 2);
               string porcentajeFantasma = sl.GetCellValueAsString(iRow, 3);


                using (var db = new tiendaEntities())
                {
                    using (DbContextTransaction transaction = db.Database.BeginTransaction()) {

                        if(iRow == 2) {
                            int truncateTable = await db.Database.ExecuteSqlCommandAsync("TRUNCATE TABLE  precios_fantasma;");
                        }
                      
                        try {
                            productos_Datos productoExistente = await db.productos_Datos
                                .Where(t => t.numero_parte == numero_parte)
                                .FirstOrDefaultAsync();

                            if (productoExistente != null) {

                               
                                db.SaveChanges();
                                transaction.Commit();

                                int noOfRowInserted = await db.Database.ExecuteSqlCommandAsync("INSERT INTO  precios_fantasma  " +
                                                                   "(numero_parte, preciosFantasma, porcentajeFantasma) " +
                                                                   $" VALUES ('{numero_parte}', {precio}, {porcentajeFantasma} )");
                                cantidadProductosCargados += 1;
                            }
                            else {
                                

                               log += $"El producto {numero_parte} no existe para su insercción \n\n";
                            }


                        }
                        catch (Exception ex) {
                            transaction.Rollback();
                            errores = true;
                           // log += "Error: " + ex.Message;
                            log +=  $"Ocurrio un error al guardar un producto:  {numero_parte}  \n\n" + ex.Message + "\n\n";
                        }
                      
                    }
                }

                iRow++;
            } // Fin While
            if (errores) {
                Log_Result.Text = "Carga finalizada con errores " + $"Productos cargados: {cantidadProductosCargados} \n\n" + log;
                materializeCSS.crear_toast(this, "Proceso de carga con algunos errores.", false);
            }
            else {
                Log_Result.Text = "Productos cargados con éxito. Log: \n\n" +$"Productos cargados: {cantidadProductosCargados} \n\n" + log;
                materializeCSS.crear_toast(this, "Proceso de carga terminado.", true);
            }

        }  catch(Exception ex)
        {
            Log_Result.Text = ex.ToString();

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


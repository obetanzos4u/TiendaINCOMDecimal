using ClosedXML.Excel;
using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;


public partial class herramientas_configuraciones_cargar_pesos_y_medidas : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack) {

            Title = "Carga información de pesos, medidas y envío de productos";
        }
    }



    protected void btn_cargar_archivos_Click(object sender, EventArgs e)
    {
        string filePath = "";
        string log = string.Empty;
        try {
            string numero_parte = "";

            filePath = HttpContext.Current.Server.MapPath("~") + @"temp\" + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(filePath);

         

            var workbook = new XLWorkbook(filePath);
            var rows = workbook.Worksheet("Master");



            int iRow = 2;
            bool errores = false;

            while (!string.IsNullOrEmpty(rows.Cell(iRow, 1).GetString()) || !string.IsNullOrEmpty(rows.Cell(iRow, 2).GetString()))
            {
               
                numero_parte = textTools.lineSimple(rows.Cell(iRow, 1).GetString());
                Log_Result.Text = numero_parte + "\n";
                Decimal peso = 0;
                Decimal alto = 0;
                Decimal ancho = 0;
                Decimal Largo_Profundidad = 0;
                decimal.TryParse(rows.Cell(iRow, 6).GetValue<string>(), out peso);
                decimal.TryParse(rows.Cell(iRow, 7).GetValue<string>(), out alto);
                decimal.TryParse(rows.Cell(iRow, 8).GetValue<string>(), out ancho);
                decimal.TryParse(rows.Cell(iRow, 9).GetValue<string>(), out Largo_Profundidad);

             

                    string str_RotacionHorizontal = textTools.lineSimple(rows.Cell(iRow, 18).GetValue<string>().ToLower());
                    bool RotacionHorizontal = str_RotacionHorizontal == "true" ? true : false;

                    string str_RotacionVertical = textTools.lineSimple(rows.Cell(iRow, 19).GetValue<string>().ToLower());
                    bool RotacionVertical = str_RotacionVertical == "true" ? true : false;


                    if (string.IsNullOrWhiteSpace(rows.Cell(iRow, 25).GetValue<string>())) {
                        iRow++;
                        continue;
                    }
                    int disponibleEnvio = int.Parse(rows.Cell(iRow, 25).GetValue<string>());

                    using (var db = new tiendaEntities()) {
                        using (DbContextTransaction transaction = db.Database.BeginTransaction()) {

                            try {
                                productos_Datos productoExistente = db.productos_Datos
                                    .Where(t => t.numero_parte == numero_parte)
                                    .FirstOrDefault();

                                if (productoExistente != null) {
                                    productoExistente.peso = (decimal?)peso == 0? null : (decimal?)peso;
                                    productoExistente.alto = (decimal?)alto == 0 ? null : (decimal?)alto;
                                productoExistente.ancho = (decimal?)ancho == 0 ? null : (decimal?)ancho;
                                productoExistente.profundidad = (decimal?)Largo_Profundidad == 0 ? null : (decimal?)Largo_Profundidad;



                                productoExistente.disponibleEnvio = disponibleEnvio;
                                    productoExistente.RotacionHorizontal = RotacionHorizontal;
                                    productoExistente.RotacionVertical = RotacionVertical;

                                    db.SaveChanges();
                                    transaction.Commit();

                                }
                                else {
                                    log += $"El producto {numero_parte} no existe para su actualización de medidas \n\n";
                                }


                            }
                            catch (Exception ex) {
                                transaction.Rollback();
                                errores = true;
                                // log += "Error: " + ex.Message;
                                log += $"Ocurrio un error al guardar un producto:  {numero_parte}  \n\n" + ex.Message + "\n\n";
                            }

                        }
                    }
                

                iRow++;
            } // Fin While
            if (errores) {
                Log_Result.Text = "Carga finalizada con errores " + log;
                materializeCSS.crear_toast(this, "Proceso de carga con algunos errores ", false);
            }
            else {
                Log_Result.Text = "Productos cargados con éxito. Log: \n\n" + log;
                materializeCSS.crear_toast(this, "Proceso de carga terminado exitosamente", true);
            }

        }  catch(Exception ex)
        {
            Log_Result.Text += ex.ToString();

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


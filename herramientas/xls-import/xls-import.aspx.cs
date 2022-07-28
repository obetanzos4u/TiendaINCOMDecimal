using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class herramientas_xls_import_xls_import : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {


        /* foreach (var c in dt.Columns) { 
         Response.Write(c.ToString() + "<br>");
         }
         Response.Write(dt.Rows.Count + "<br>");
         foreach (DataRow c in dt.Rows)
         {

             Response.Write(c[0].ToString() + "<br>");
         }*/
        }
    protected void ImportExcel(object sender, EventArgs e) {

        string tabla = ddl_tablaDestino.SelectedValue;
        //  int numeroHoja = int.Parse(txt_numeroHoja.Text);
        string filePath = HttpContext.Current.Server.MapPath("~") + @"\temp\" + Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.SaveAs(filePath);

        excelImport Import = new excelImport();

        Import.filePath = filePath;


        // Si la tabla destino es el mismo nombre que la hoja (CheckBox en true)
        if (chk_numeroHoja.Checked) Import.nombreHoja = tabla;
        else Import.nombreHoja = txt_numeroHoja.Text;
        DataTable dt = new DataTable();
       
              dt = Import.XlsxToDataTableOLEDB();
            



        // INICIO - Borrando el archivo si ya existe
        if (System.IO.File.Exists(filePath)) {  // Comprobamos su existencia

            try {
                System.IO.File.Delete(filePath);
                }
            catch (System.IO.IOException ex) {

                devNotificaciones.error("Borrar pdf de XLS import: " + filePath,ex );

                }
            }

        // FIN - Borrando el archivo si ya existe
         try {

            bool eliminarTablaDestino = chk_eliminar_tabla_destino.Checked;
            if (eliminarTablaDestino) { excelImport.eliminarTabla(tabla); }

           string resultado = Import.insertar(tabla, dt, ddl_validacion.SelectedValue);
            materializeCSS.crear_toast(this, "Carga realizada, revisar Log", true);
            txt_log.Text = resultado;
        }
        catch (Exception ex) {
            materializeCSS.crear_toast(this, "Error al cargar", false);
            devNotificaciones.error("Error al cargar archivo XLS", ex);
        }
     
           

        }

    protected void UpdateExcel(object sender, EventArgs e) {
        if(!string.IsNullOrWhiteSpace(txt_referencia.Text)) {




            string tabla = ddl_tablaDestino.SelectedValue;
        //  int numeroHoja = int.Parse(txt_numeroHoja.Text);
        string filePath = HttpContext.Current.Server.MapPath("~") + @"\temp\" + Path.GetFileName(FileUpload1.PostedFile.FileName);
        FileUpload1.SaveAs(filePath);

        excelImport Import = new excelImport();

        Import.filePath = filePath;


            // Si la tabla destino es el mismo nombre que la hoja (CheckBox en true)
            if (chk_numeroHoja.Checked) Import.nombreHoja = tabla;
            else Import.nombreHoja = txt_numeroHoja.Text;

            DataTable dt = Import.XlsxToDataTableOLEDB();




        // INICIO - Borrando el archivo si ya existe
        if (System.IO.File.Exists(filePath)) {  // Comprobamos su existencia

            try {
                System.IO.File.Delete(filePath);
            }
            catch (System.IO.IOException ex) {

                devNotificaciones.error("Borrar pdf de XLS import: " + filePath, ex);

            }
        }

        // FIN - Borrando el archivo si ya existe

        string resultado = Import.actualizar(tabla, dt, ddl_validacion.SelectedValue, txt_referencia.Text);

        txt_log.Text = resultado;

        } else {

        }
    }


    protected void chk_numeroHoja_CheckedChanged(object sender, EventArgs e) {
        if (chk_numeroHoja.Checked) {
            content_chk_numeroHoja.Visible = true;
            content_txt_numeroHoja.Visible = false;
        } else {
            chk_numeroHoja.Checked = false;
            content_chk_numeroHoja.Visible = false;
            content_txt_numeroHoja.Visible = true;
        }
      
    }

    protected void Unnamed_Click(object sender, EventArgs e) {

    }

    protected void btn_cancelarNombreHoja_Click(object sender, EventArgs e) {
 
            chk_numeroHoja.Checked = true;
            content_chk_numeroHoja.Visible = true;
            content_txt_numeroHoja.Visible = false;
 
       
    }
}
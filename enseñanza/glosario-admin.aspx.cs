using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class enseñanza_glosario_admin : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {


        if (!IsPostBack)
        {

            if (HttpContext.Current.User.Identity.IsAuthenticated && usuarios.userLogin().rango != 3) Response.Redirect("/enseñanza/glosario.aspx");
            actualizarTotalTérminos();
        }
    }

    protected void actualizarTotalTérminos()
    {
        lbl_registrosActuales.Text = glosarioController.count().ToString();
    }
    protected void btn_subirGlosario_Click(object sender, EventArgs e)
    {
        string tabla = "glosario";
        string filePath = HttpContext.Current.Server.MapPath("~") + @"\temp\" + Path.GetFileName(FileUpload.PostedFile.FileName);
        FileUpload.SaveAs(filePath);

        excelImport Import = new excelImport();

        Import.filePath = filePath;
        Import.nombreHoja = "glosario";

        DataTable dt = new DataTable();

        dt = Import.XlsxToDataTableOLEDB();


        // INICIO - Borrando el archivo si ya existe
        if (System.IO.File.Exists(filePath))
        {  // Comprobamos su existencia

            try
            {
                System.IO.File.Delete(filePath);
            }
            catch (System.IO.IOException ex)
            {


            }
        }

        // FIN - Borrando el archivo si ya existe

        try {
            // Eliminamos lo que tenga actualmente la tabla
           excelImport.eliminarTabla(tabla); 

            string resultado = Import.insertar(tabla, dt, null);

            txt_log.Text = resultado;
        }
        catch (Exception ex)
        {
           
            txt_log.Text = "Error al cargar archivo XLS";
        }

        actualizarTotalTérminos();

    }
}
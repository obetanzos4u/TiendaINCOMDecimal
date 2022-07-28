using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class herramientas_reportes_export : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack) {
            cargarReportesGuardados();
            cargarReportesDisponiblesGenerar();
            if (Request.QueryString["reporteDispSelected"] != null) {
                string queryTab = @"   $(document).ready(function(){
                $('ul.tabs').tabs('select_tab', 'test2');
                });";

                ddl_reportesDisponibles.SelectedValue = Request.QueryString["reporteDispSelected"].ToString();
                ScriptManager.RegisterStartupScript(this, typeof(Control),"Tab]Select", queryTab, true);
                cargarCamposDisponibles();
                generarScriptJs();


                string sortable = @"
          $(document).ready(function () {

           var adjustment;


            $('ol.simple_with_animation').sortable('destroy');
  
                $('ol.simple_with_animation').sortable({
                  group: 'simple_with_animation',
                  pullPlaceholder: false,
                  // animation on drop
                  onDrop: function  ($item, container, _super) {
                    var $clonedItem = $('<li/>').css({height: 0});
                    $item.before($clonedItem);
                    $clonedItem.animate({'height': $item.height()});

                    $item.animate($clonedItem.position(), function  () {
                      $clonedItem.detach();
                      _super($item, container);
                    });
                  },

                  // set $item relative to cursor position
                  onDragStart: function ($item, container, _super) {
                    var offset = $item.offset(),
                        pointer = container.rootGroup.pointer;

                    adjustment = {
                      left: pointer.left - offset.left,
                      top: pointer.top - offset.top
                    };

                    _super($item, container);
                  },
                  onDrag: function ($item, position) {
                    $item.css({
                      left: position.left - adjustment.left,
                      top: position.top - adjustment.top
                    });
                  }
                        });


  $('ol.simple_with_animation').sortable('refresh');
  
        });

                ";

                ScriptManager.RegisterStartupScript(this, typeof(Control), "sortable", sortable, true);
            }

          
        }

    }

    void cargarReportesDisponiblesGenerar() {

      DataTable dtReportesDisponibles = reportes.obtenerReportesDisponibles("cotizaciones");

        
         int noReportes = dtReportesDisponibles.Rows.Count;

        ddl_reportesDisponibles.DataSource = dtReportesDisponibles;
        ddl_reportesDisponibles.DataTextField = "NombreReporte";
        ddl_reportesDisponibles.DataValueField = "aliasNombreReporte";
        ddl_reportesDisponibles.DataBind();

        ddl_reportesDisponibles.Items.Insert(0, new ListItem() { Enabled = true, Text = "Selecciona", Value = "" });
        ddl_reportesDisponibles.Items[0].Attributes.Add("disabled", "");


    }
        protected void ddl_reportesDisponibles_SelectedIndexChanged(object sender, EventArgs e) {

        content_btn.InnerHtml = "";
        content_btnQuitar.InnerHtml = "";
        cargarCamposDisponibles();
        generarScriptJs();
       
       

        

        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
        nameValues.Set("reporteDispSelected", ddl_reportesDisponibles.SelectedValue);
       
        string url = Request.Url.AbsolutePath;

       

        Response.Redirect(url + "?" + nameValues); // ToString() is called implicitly

        up_camposDisponibles.Update();
        up_camposSeleccionados.Update();

    }
   
    protected void cargarCamposDisponibles() {
        List<HtmlGenericControl> elementos = reportes.controlesLI(reportes.recuperarCampos(ddl_reportesDisponibles.SelectedValue));
        foreach (HtmlGenericControl control in elementos) {
            camposDisponibles.Controls.Add(control);
        }
       
 
    }

        protected void generarScriptJs() {

        string nombreReporte = ddl_reportesDisponibles.SelectedValue;
        Dictionary<string, string> tablasAlias = reportes.recuperarTablasAlias(nombreReporte);

        foreach(KeyValuePair<string, string> entry  in tablasAlias) {
           

            string btnText = @"<a style='cursor:pointer;' onclick='btnClick" + entry.Value + "();'>"+ entry.Key +" </a> &nbsp; ";

            content_btn.InnerHtml += btnText;

                   string script = @" function btnClick"+ entry.Value + @"(){
                    var element = document.querySelectorAll('ol#top_contenido_camposDisponibles > li[tablaalias="+ entry.Value + @"]'); 
                            var i;
                            for (i = 0; i < element.length; i++) {   
	                            $(element[i]).appendTo('#top_contenido_camposSeleccionados');
                                 } 
                    }";

            ScriptManager.RegisterStartupScript(this, typeof(Control), entry.Value, script, true);
        }

        // Los que quitan
       
        foreach (KeyValuePair<string, string> entry in tablasAlias) {
 
            string btnText = @"<a   style='cursor:pointer;' onclick='btnClick" + entry.Value + "Quitar();'>" + entry.Key + "</a> &nbsp; ";

            content_btnQuitar.InnerHtml += btnText;

            string script = @" function btnClick" + entry.Value + @"Quitar(){
                    var element = document.querySelectorAll('ol#top_contenido_camposSeleccionados > li[tablaalias=" + entry.Value + @"]'); 
                            var i;
                            for (i = 0; i < element.length; i++) {   
	                            $(element[i]).appendTo('#top_contenido_camposDisponibles');
                                 } 
                    }";

            ScriptManager.RegisterStartupScript(this, typeof(Control), entry.Value + "Quitar", script, true);
        }
       
     
    }

   
    protected void btn_GuardarReporte_Click(object sender, EventArgs e) {


        if (validarCamposGuardar()) { 
        string campos = txt_reporteValor.Text;
            string nombreReporte = txt_nombreReporte.Text;

            string query = " SET LANGUAGE English; SELECT " +  campos.TrimEnd(' ').TrimEnd(',') + " " ;
        query = query + reportes.recuperarJoins(ddl_reportesDisponibles.SelectedValue);

         
        txt_reporteValor.Text=query;
        // true;// 
        bool resultado = reportes.guardarReporte("cotizaciones", ddl_reportesDisponibles.SelectedValue, nombreReporte,  query, __txt_reporteValorHTML.Text);
        if(resultado == true) {
            materializeCSS.crear_toast(up_reportes, "Reporte guardado con éxito", true);
            cargarReportesGuardados();
            cargarCamposDisponibles();
            camposSeleccionados.InnerHtml = "";

        } else {
            materializeCSS.crear_toast(up_reportes, "Error al guardar reporte", false);
        }
        txt_reporteValor.Text = ""; __txt_reporteValorHTML.Text = "";
        }
        up_reportes.Update();
    }
    bool validarCamposGuardar() {
     
        string campos = textTools.lineSimple(txt_reporteValor.Text);
        string nombreReporte = txt_nombreReporte.Text;

        if (reportes.validarExistencia_NombreReporte(nombreReporte) >= 1) {
            materializeCSS.crear_toast(this, "Ya existe ese nombre del reporte", false);
            return false;
        }
        if (string.IsNullOrWhiteSpace(campos)) {
          materializeCSS.crear_toast(this, "No haz seleccionado campos", false);
            return false; }
        if (String.IsNullOrWhiteSpace(nombreReporte)) {
            materializeCSS.crear_toast(this, "No se permiten nombres vacios", false);
            return false; }

        return true;
    }
    void cargarReportesGuardados() {

        DataTable dt_reportes =  reportes.obtenerReportesMin();
        int noReportes = dt_reportes.Rows.Count;

        ddl_reportes.DataSource = dt_reportes;
        ddl_reportes.DataTextField = "nombre";
        ddl_reportes.DataValueField = "id";
        ddl_reportes.DataBind();

        ddl_reportes.Items.Insert(0, new ListItem() { Enabled = true,  Text = "Selecciona", Value = "" });
        ddl_reportes.Items[0].Attributes.Add("disabled", "");
        up_reportes.Update();

    
    }

    protected void btnExport_Click(object sender, EventArgs e) {

        if (ddl_reportes.SelectedValue != "") {
              XLWorkbook wb = new XLWorkbook();


            // var worksheet = wb.Worksheets.Add("Sample Sheet");
            // worksheet.Cell("A1").Value = "Hello World!";
            // worksheet.Cell("A2").FormulaA1 = "=MID(A1, 7, 5)";



            string fecha_desde = " WHERE cotDatos.fecha_creacion BETWEEN '" + utilidad_fechas.obtenerFechaSQL(-6)+ " 00:00' AND ";
        string fecha_hasta = " '" + utilidad_fechas.obtenerFechaSQL() + " 23:59'";

        if (!string.IsNullOrWhiteSpace(txt_fecha_desde.Text)) {

            fecha_desde = " WHERE  cotDatos.fecha_creacion BETWEEN '" +  txt_fecha_desde.Text + " 00:00' AND ";
        }

        if (!string.IsNullOrWhiteSpace(txt_fecha_hasta.Text)) {

            fecha_hasta = "'"+ txt_fecha_hasta.Text + " 23:59' ";
        }

        foreach (ListItem listItem in ddl_reportes.Items) {

            if (listItem.Selected) {

                var id = int.Parse(listItem.Value);
                var nombre = listItem.Text;
                string valorReporte = reportes.obtenerValorReporte(id) + fecha_desde + fecha_hasta;
                DataTable dt_reporte = cotizaciones.obtenerCotizacionesLibre(valorReporte);
              
                wb.Worksheets.Add(dt_reporte, nombre);
            }
        }

        HttpResponse httpResponse = Response;
        httpResponse.Clear();
        httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        httpResponse.AddHeader("content-disposition", "attachment;filename=\"" + utilidad_fechas.AAAMMDD() + " - Reporte tiendaIncom.xlsx\"");

        // Flush the workbook to the Response.OutputStream
        using (MemoryStream memoryStream = new MemoryStream()) {
            wb.SaveAs(memoryStream);
            memoryStream.WriteTo(httpResponse.OutputStream);
            memoryStream.Close();
        }

        httpResponse.End();
        } else {
            materializeCSS.crear_toast(up_reportes, "Selecciona un reporte", false);
        }
    }
    protected void btnEliminarReporte_Click(object sender, EventArgs e) {

         
        foreach (ListItem listItem in ddl_reportes.Items) {
            if (listItem.Selected) {
                var nombre = listItem.Text;
                var id = int.Parse(listItem.Value);
                bool resultado = reportes.eliminarReporte(id);

                if (resultado) materializeCSS.crear_toast(this, "Reporte \"" + nombre + "\" eliminado con éxito",true);
                else materializeCSS.crear_toast(this, "Error al eliminar \"" + nombre + "\"",false);
            }
        }
        cargarReportesGuardados();
        cargarCamposDisponibles();
        generarScriptJs();

    }

   
}
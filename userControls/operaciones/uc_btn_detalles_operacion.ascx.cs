using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class uc_btn_detalles_operacion : System.Web.UI.UserControl
{


 
    public string tipo_operacion {
        get { return this.hf_tipo_operacion.Value; }
        set { this.hf_tipo_operacion.Value = value; }
        }
    public string numero_operacion {
        get { return this.hf_numero_operacion.Value; }
        set { this.hf_numero_operacion.Value = value; }
        }

     public DataTable dt_operacion { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            cargarPlantillas();

        if (hf_tipo_operacion.Value == "pedido") {
  
        } else if (hf_tipo_operacion.Value == "cotizacion"){
                if (dt_operacion != null) cotizacion(); else { Response.Redirect("/usuario/mi-cuenta/cotizaciones/"); }
 
            }
       
        }
        }
    private void cargarPlantillas() {
        ddl_plantillasPersonalizadas.Items.Clear();
        ddl_plantillasPersonalizadas.DataBind();

        DataTable dt_cotizacionesPlantillas = cotizacionesPlantillas.recuperarPlantillasMin();
 

        if (dt_cotizacionesPlantillas.Rows.Count >= 1) {
            ddl_plantillasPersonalizadas.DataSource = dt_cotizacionesPlantillas;
            ddl_plantillasPersonalizadas.DataTextField = "nombre";
            ddl_plantillasPersonalizadas.DataValueField = "id";
            ddl_plantillasPersonalizadas.DataBind();

         
            ddl_plantillasPersonalizadas.Items.Insert(0, new ListItem("Selecciona", ""));
            ddl_plantillasPersonalizadas.Items[0].Attributes.Add("disabled", "");
            ddl_plantillasPersonalizadas.Items[0].Attributes.Add("selected", "");
        }
     else {
            ddl_plantillasPersonalizadas.Items.Insert(0, new ListItem("No has creado plantillas", ""));
            ddl_plantillasPersonalizadas.Items[0].Attributes.Add("disabled", "");
            ddl_plantillasPersonalizadas.Items[0].Attributes.Add("selected", "");
            
        }

    }
    public void cotizacion() {
      

        int vigencia = int.Parse(dt_operacion.Rows[0]["vigencia"].ToString());
        int conversionPedido = int.Parse(dt_operacion.Rows[0]["conversionPedido"].ToString());

        string numero_operacion = dt_operacion.Rows[0]["numero_operacion"].ToString();
        string monedaCotizacion = dt_operacion.Rows[0]["monedaCotizacion"].ToString();
        int id_operacion = int.Parse(dt_operacion.Rows[0]["id"].ToString());

        DateTime fechaOperacion = DateTime.Parse(dt_operacion.Rows[0]["fecha_creacion"].ToString());
        hf_numero_operacion.Value = numero_operacion;
        hf_id_operacion.Value = id_operacion.ToString();
        hf_monedaCotizacion.Value = monedaCotizacion;

        lbl_fecha_operacion.Text = String.Format("{0:f}", fechaOperacion);
        lbl_vigencia.Text = vigencia.ToString();
        if (utilidad_fechas.calcularDiferenciaDias(fechaOperacion) >= vigencia) {
            cont_operacion_vencida.Visible = true;
            } else if (conversionPedido == 1) {
            cont_cotizacionPedido.Visible = true;

            string id =  pedidosDatos.obtenerIdSQLPedido(dt_operacion.Rows[0]["numero_operacion_pedido"].ToString()).ToString();
            link_pedidoFromCotizacion.NavigateUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/usuario/mi-cuenta/pedidos/visualizar/" + seguridad.Encriptar(id);
            }
    }

    protected void btn_renovar_Click(object sender, EventArgs e) {

        var resultado = cotizaciones.renovarCotizacion(numero_operacion);

        bool resultadoOperacion = resultado.Item1;

        if (resultado.Item2.Count >= 1) {

            foreach (string producto in resultado.Item2) {

                resultado_renovar_cotizacion.InnerHtml += producto + "<br>";
                }

            }
        cont_operacion_vencida.Visible = false;

        // Invocamos el método en la parent page que recarge las operaciones

           this.Page.GetType().InvokeMember("cargasDatosOperacion", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null /*, new object[] { txtMessage.Text }*/);
           this.Page.GetType().InvokeMember("cargarProductos", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null /*, new object[] { txtMessage.Text }*/);
        this.Page.GetType().InvokeMember("refreshPage", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null /*, new object[] { txtMessage.Text }*/);


        content_resultado_renovar_cotizacion.Visible = true;
        upEstatusOperacion.Update();
    }

    protected void agregarProductosQuick_Click(object sender, EventArgs e) {

        string numero_operacion = hf_numero_operacion.Value;
        string monedaOperacion = hf_monedaCotizacion.Value;
        string productosQuickAdd = txt_productosQuickAdd.Text;
        string productosFallidos = "";
        Tuple<bool, List<string>> resultado =   cotizacionesProductos.agregarProductosQuick(numero_operacion, monedaOperacion, productosQuickAdd);

        if (resultado.Item1) {
            materializeCSS.crear_toast(up_QuickAdd, "Productos agregados con éxito", true);
        } else {
            materializeCSS.crear_toast(up_QuickAdd, "Error al agregar productos", false);

            foreach(string product in resultado.Item2){

                productosFallidos  = productosFallidos + product + " no pudo ser agregado. ";

            }
            lbl_result_quickAddProducts.Text = productosFallidos;
        }
      

        materializeCSS.crear_toast(up_QuickAdd, "Error al agregar productos", false);
 
         this.Page.GetType().InvokeMember("cargasDatosOperacion", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null /*, new object[] { txtMessage.Text }*/);
          this.Page.GetType().InvokeMember("cargarProductos", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null /*, new object[] { txtMessage.Text }*/);
        this.Page.GetType().InvokeMember("refreshPage", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null /*, new object[] { txtMessage.Text }*/);
        cargarDDL();
    }
    protected void cargarDDL() {
     
 
        materializeCSS.mostrarModal(up_QuickAdd, "#modal_quickAddProducts", 1);
    }

    protected void ddl_plantillasPersonalizadas_SelectedIndexChanged(object sender, EventArgs e) {
    
        if(ddl_plantillasPersonalizadas.SelectedValue != "") {
            int idPlantilla = int.Parse(ddl_plantillasPersonalizadas.SelectedValue);
            cotizacionesPlantillas plantilla = cotizacionesPlantillas.recuperarPlantilla(idPlantilla);
            txt_productosQuickAdd.Text = plantilla.valor;

            ScriptManager.RegisterStartupScript(up_QuickAdd, typeof(Page), "textareaResize", "$('textarea').trigger('autoresize'); ", true);
        } else { txt_productosQuickAdd.Text = ""; }
        cargarDDL();

    }

    protected void btn_eliminarPlantilla_Click(object sender, EventArgs e) {
        if (ddl_plantillasPersonalizadas.SelectedValue != "") {
            int idPlantilla = int.Parse(ddl_plantillasPersonalizadas.SelectedValue);
            bool resultado =  cotizacionesPlantillas.eliminarPlantilla(idPlantilla);
            if (resultado) {
                materializeCSS.crear_toast(up_QuickAdd, "Plantilla eliminada con éxito", true);

            } else {
                materializeCSS.crear_toast(up_QuickAdd, "Error al eliminar platnilla", true);
            }   
        }
        cargarPlantillas();
        materializeCSS.mostrarModal(up_QuickAdd, "#modal_quickAddProducts", 1);
        up_QuickAdd.Update();
    }

    protected void cerrarResultadoRenovarCotizacion_Click(object sender, EventArgs e)
    {
        content_resultado_renovar_cotizacion.Visible = false;
        upEstatusOperacion.Update();


    }
}
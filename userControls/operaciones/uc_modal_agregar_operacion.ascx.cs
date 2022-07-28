using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class uc_modal_agregar_operacion : System.Web.UI.UserControl
{

    public string numero_parte {
        get { return this.hf_numero_parte.Value; }
        set { this.hf_numero_parte.Value = value; }
        }



    public string descripcion_corta { get; set; }

    private string activeOperation = "collection-item active";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (HttpContext.Current.User.Identity.IsAuthenticated) {
            mdl_agregarOperacion.Visible = true;
            }
        
        if (!IsPostBack) {

  
            }
  
        }
    private void modalDOMshow() {
        ScriptManager.RegisterStartupScript(this.Page, typeof(Control), "mdl_agregarOperacion", "$(document).ready(function() {  M.AutoInit();   $('.mdl_agregarOperacion').modal('open');  });", true);
        }
    public void mostrarModal() {
     
      
        lbl_numero_parte.Text = numero_parte;
        lbl_descripcion_corta.Text = descripcion_corta;
        loadCotizaciones();
        modalDOMshow();
        }
   
    protected void loadCotizaciones() {

        cotizaciones obtener = new cotizaciones();
        usuarios datosUsuario = usuarios.modoAsesor();

        
        DataTable dtCotizaciones = obtener.obtenerCotizacionesUsuario_min(datosUsuario.email, 15, " AND datos.activo = 1 AND datos.conversionPedido = 0 ");

        //Agregamos la columna que nos sirve para poner la css class como activa
        dtCotizaciones.Columns.Add("active", typeof(System.String));


        // Estas variables se crean al dar click editar la operacion  para posicionarla en primer lugar la operación deseada a editar.
        if (Session["cotizacion_edit"] != null && Session["cotizacion_edit_idSQL"] != null) {

            hf_tipo_operacion.Value = "cotizacion";

            hf_intIndex_ListViewItemActive.Value = "0";
            // Obteneos el DT con la operación y también agregamos la columna para el CSS class
            DataTable dtCotizacionEdit = obtener.obtenerCotizacionDatos_min(int.Parse(Session["cotizacion_edit_idSQL"].ToString()));
            dtCotizacionEdit.Columns.Add("active", typeof(System.String));

            // Al ser un único valor que traeremos le añadimos el texto con las propiedades del estilo
            dtCotizacionEdit.Rows[0]["active"] = activeOperation;

          

            lbl_numero_operacion.Text = dtCotizacionEdit.Rows[0]["numero_operacion"].ToString() ;
            lbl_nombre_operacion.Text = dtCotizacionEdit.Rows[0]["nombre_cotizacion"].ToString();
            lbl_cliente_nombre.Text = dtCotizacionEdit.Rows[0]["cliente_nombre"].ToString();
            lbl_moneda.Text = dtCotizacionEdit.Rows[0]["monedaCotizacion"].ToString();

            dtCotizacionEdit.Merge(dtCotizaciones);
            dtCotizaciones = dtCotizacionEdit;
            /* PAra ordenar DataRow[] foundRows = dtCotizaciones.Select("Date = '1/31/1979' or OrderID = 2", "CompanyName ASC");
             DataTable dt = foundRows.CopyToDataTable();

             dtCotizaciones = dt;
             dt.Dispose();
                */

            btn_agregarOperacion.Enabled = true;
            btn_agregarOperacion.CssClass = " btn blue";
            } 
        else {
            lbl_nombre_operacion.Text = "Selecciona una cotización";
            lbl_numero_operacion.Text = "---"; 
            lbl_moneda.Text = "---";
            lbl_cliente_nombre.Text = "---";

            btn_agregarOperacion.CssClass = " btn disabled";
            btn_agregarOperacion.Enabled = false;


            }


        lv_cotizaciones.DataSource = dtCotizaciones;
        lv_cotizaciones.DataBind();

        up_operacionesAdd.Update();
     
        }

    protected void lv_cotizaciones_OnItemDataBound(object sender, ListViewItemEventArgs e) {

        System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;
        HtmlGenericControl liItem = (HtmlGenericControl)e.Item.FindControl("liItem");


        // Obtenemos el Index del elemento que será el nuevo activo para aplicarlo el HF

        HiddenField hf_indexItem = (HiddenField)e.Item.FindControl("hf_indexItem");
        HiddenField hf_id_cotizacionSQL = (HiddenField)liItem.FindControl("hf_id_cotizacionSQL");
        HyperLink link_cotizacion = (HyperLink)liItem.FindControl("link_cotizacion");

        if (hf_indexItem.Value == hf_intIndex_ListViewItemActive.Value) {
           liItem.Attributes.Add("class", "collection-item active");
            link_cotizacion.Attributes.Add("class", "white-text");

            if ( lbl_numero_operacion.Text == "Selecciona una operación"){

             
                Label lbl_cotizacion_numero_operacion = (Label)liItem.FindControl("lbl_cotizacion_numero_operacion");
                Label lbl_cotizacion_nombre_operacion = (Label)liItem.FindControl("lbl_cotizacion_nombre_operacion");
                Label lbl_cotizacion_cliente_nombre = (Label)liItem.FindControl("lbl_cotizacion_cliente_nombre");
                Label lbl_cotizacion_moneda = (Label)liItem.FindControl("lbl_cotizacion_moneda");
             


                string numero_operacion = lbl_cotizacion_numero_operacion.Text;
                string nombre_operacion = lbl_cotizacion_nombre_operacion.Text;
                string cliente_nombre = lbl_cotizacion_cliente_nombre.Text;
                string moneda = lbl_cotizacion_moneda.Text;



            

                lbl_numero_operacion.Text = numero_operacion;
                lbl_nombre_operacion.Text = nombre_operacion;
                lbl_cliente_nombre.Text = cliente_nombre;
                lbl_moneda.Text = moneda;

                btn_agregarOperacion.Enabled = true;
                btn_agregarOperacion.CssClass = " btn blue";
                }
            } else liItem.Attributes.Add("class", "collection-item ");

        link_cotizacion.NavigateUrl = GetRouteUrl("usuario-cotizacion-visualizar", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",seguridad.Encriptar(hf_id_cotizacionSQL.Value) }
                    });
        preLoad();

        }

         protected void preLoad() {

     //   if (!string.IsNullOrEmpty(hf_intIndex_ListViewItemActive.Value)) { 
       // int.Parse(hf_intIndex_ListViewItemActive.Value);

            foreach (ListViewItem item in lv_cotizaciones.Items) {

                Label lbl_cotizacion_numero_operacion = (Label)item.FindControl("lbl_cotizacion_numero_operacion");
         

            if (lbl_cotizacion_numero_operacion.Text == lbl_numero_operacion.Text) {

                HyperLink link_cotizacion = (HyperLink)item.FindControl("link_cotizacion");
                link_cotizacion.Attributes.Add("class", "white-text");

                Label lbl_cotizacion_nombre_operacion = (Label)item.FindControl("lbl_cotizacion_nombre_operacion");
                    Label lbl_cotizacion_cliente_nombre = (Label)item.FindControl("lbl_cotizacion_cliente_nombre");
                    Label lbl_cotizacion_moneda = (Label)item.FindControl("lbl_cotizacion_moneda");

                    string numero_operacion = lbl_cotizacion_numero_operacion.Text;
                    string nombre_operacion = lbl_cotizacion_nombre_operacion.Text;
                    string cliente_nombre = lbl_cotizacion_cliente_nombre.Text;
                    string moneda = lbl_cotizacion_moneda.Text;

                    lbl_numero_operacion.Text = numero_operacion;
                    lbl_nombre_operacion.Text = nombre_operacion;
                    lbl_cliente_nombre.Text = cliente_nombre;
                    lbl_moneda.Text = moneda;

                    btn_agregarOperacion.Enabled = true;
                    btn_agregarOperacion.CssClass = " btn blue";
                    }
                }

         //   }
        }

        protected void btn_seleccionarOperacion_Click(object sender, EventArgs e) {
        hf_tipo_operacion.Value = "cotizacion";
        // Obtenemos el elemento que activo el click
        LinkButton btn_seleccionarOperacion = sender as LinkButton;

        // Obtenemos el index del elemento activo
        if (hf_intIndex_ListViewItemActive.Value != "") {
            HtmlGenericControl old_itemActive = (HtmlGenericControl)lv_cotizaciones.Items[int.Parse(hf_intIndex_ListViewItemActive.Value)].FindControl("liItem");
            // Removemos el estilo
            old_itemActive.Attributes.Add("class", "collection-item");

            lbl_nombre_operacion.Text = "Selecciona una operación"; 
           lbl_numero_operacion.Text = "---";
            lbl_moneda.Text = "---";
            lbl_cliente_nombre.Text = "---";

            btn_agregarOperacion.CssClass = " btn disabled";
            btn_agregarOperacion.Enabled = false;
            }
        btn_agregarOperacion.Enabled = true;
        btn_agregarOperacion.CssClass = " btn blue";
        // Obtenemos el contenedor, en este caso no es un ListView Item si no un HTML Generic
        HtmlGenericControl item = (HtmlGenericControl)btn_seleccionarOperacion.Parent;

        // Le aplicamos el estilo
        item.Attributes.Add("class", activeOperation);

        // Añadimos el texto de color blanco para que no se pierda
        HyperLink link_cotizacion = (HyperLink)item.FindControl("link_cotizacion");
        link_cotizacion.Attributes.Add("class", "white-text");

        btn_seleccionarOperacion.Text = "Seleccionada";
        // Obtenemos el Index del elemento que será el nuevo activo para aplicarlo el HF
        HiddenField hf_indexItem = (HiddenField)item.FindControl("hf_indexItem");

        // Cambiamos el viejo valor por el nuemo
        hf_intIndex_ListViewItemActive.Value = hf_indexItem.Value;
        

       

        Label lbl_cotizacion_numero_operacion = (Label)item.FindControl("lbl_cotizacion_numero_operacion");
        Label lbl_cotizacion_nombre_operacion = (Label)item.FindControl("lbl_cotizacion_nombre_operacion");
        Label lbl_cotizacion_cliente_nombre = (Label)item.FindControl("lbl_cotizacion_cliente_nombre");
        Label lbl_cotizacion_moneda = (Label)item.FindControl("lbl_cotizacion_moneda");

        string numero_operacion = lbl_cotizacion_numero_operacion.Text;
        string nombre_operacion = lbl_cotizacion_nombre_operacion.Text;
        string cliente_nombre = lbl_cotizacion_cliente_nombre.Text;
        string moneda = lbl_cotizacion_moneda.Text;
        
        lbl_numero_operacion.Text = numero_operacion;
        lbl_nombre_operacion.Text = nombre_operacion;
        lbl_cliente_nombre.Text = cliente_nombre;
        lbl_moneda.Text = moneda;


        modalDOMshow();

        }
    protected void txt_cantidadCarrito_TextChanged(object sender, EventArgs e)
    {
        TextBox txt_cantidad = sender as TextBox;

        if(txt_cantidad.Text != "" && txt_cantidad.Text != null)
        {
            double cantidad = 1;
            try { 
                cantidad = textTools.soloNumeros(txt_cantidad.Text);
            }
            catch (Exception ex) {
           //     materializeCSS.crear_toast(this.Page, "Debe ingresar valores númericos", false);
                txt_cantidad.Text = "1";
            }
            

            if (cantidad < 1) { txt_cantidad.Text = "1"; }
        }

        modalDOMshow();

        }



    protected async void btn_agregarOperacion_Click(object sender, EventArgs e) {

        string tipo_operacion = hf_tipo_operacion.Value;
        string numero_operacion = lbl_numero_operacion.Text;
        operacionesProductos agregar = new operacionesProductos(tipo_operacion,"", numero_operacion, lbl_numero_parte.Text, txt_cantidadCarrito.Text, lbl_moneda.Text);
        await agregar.agregarProductoAsync();

        materializeCSS.crear_toast(this.Page, agregar.mensaje_ResultadoOperacion, agregar.resultado_operacion);

        // S egregó correctamente a la cotización, realizamos cálculo de flete
        if (agregar.resultado_operacion == true) {

            try{
                ValidarCalculoEnvioOperacion validar = new ValidarCalculoEnvioOperacion(numero_operacion, "cotizacion");
                materializeCSS.crear_toast(this, validar.Message, validar.OperacionValida);


            }
            catch (Exception ex)
            {

                materializeCSS.crear_toast(this, "Ocurrio un error", false);

            }
        }
        modalDOMshow();


        }

    protected async void btn_crearCotizacionEnBlanco_Click(object sender, EventArgs e)
    {

        LinkButton btn = (LinkButton)sender;
        TextBox txt_nombreCotización = lv_cotizaciones.Controls[0].FindControl("txt_nombreCotización") as TextBox;

        string  nombreCotización = textTools.lineSimple( txt_nombreCotización.Text);
        usuarios datosUsuario = usuarios.modoAsesor();
        var result = await CotizacionesEF.CrearCotizacionEnBlanco(datosUsuario, nombreCotización, "MXN");

        materializeCSS.crear_toast(this, result.message, result.result);

        loadCotizaciones();
        modalDOMshow();
    }
}
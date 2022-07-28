using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class userControls_productosTienda : System.Web.UI.UserControl
{



    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) {
            ddl_ordenTipo.SelectedValue = Request.QueryString["ordenTipo"];
            ddl_ordenBy.SelectedValue = Request.QueryString["ordenBy"];
            rd_filtroMarcas.SelectedValue = Request.QueryString["filtroMarcas"];
            cargarProductos(sender, e);
          
            }

       
        }
    protected void orden(object sender, EventArgs e) {

        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
            nameValues.Set("ordenTipo", ddl_ordenTipo.SelectedValue);
            nameValues.Set("ordenBy", ddl_ordenBy.SelectedValue);
            nameValues.Set("filtroMarcas", rd_filtroMarcas.SelectedValue);
        Type type = sender.GetType();
        if(sender.GetType().Name == "RadioButtonList") {

            if ((sender as RadioButtonList).ID == "rd_filtroMarcas") {
                nameValues.Set("PageId", "1");
                }
            }
        string url = Request.Url.AbsolutePath;
        Response.Redirect(url + "?" + nameValues); // ToString() is called implicitly
        }

        protected void cargarProductos(object sender, EventArgs e) {
        string monedaTienda = Session["monedaTienda"].ToString();

        usuarios datosUsuario = usuarios.modoAsesor();


        DataTable productos = new DataTable();
        productosTienda obtener = new productosTienda();
        if (Page.RouteData.Values["identificador"] != null) {
            string categoriaID = Page.RouteData.Values["identificador"].ToString();
            productos = obtener.obtenerProductos_RolyCat(datosUsuario.rol_productos, categoriaID);
        } else if(Request.QueryString["busqueda"] != null) {
            string terminos = Request.QueryString["busqueda"].ToString();
            productos = obtener.obtenerProductos(terminos, terminos);
            content_resultado_busqueda_text.Visible = true;
            lt_termino_busqueda.Text = terminos;
            }
    


        preciosTienda procesar = new preciosTienda();
        procesar.monedaTienda = monedaTienda;

        productos = procesar.procesarProductos(productos);

     

        // INICIO - Motor de búsqueda
        if (!string.IsNullOrEmpty(txt_search.Text)) {
            string find = txt_search.Text;
            string query = "numero_parte Like '%" + find + "%' OR  titulo Like '%" + find + "%'";
            productos = buscador.filtrar(this, productos, query, find);
            }
        // FIN - Motor de búsqueda 

        if (productos.Rows.Count >= 1) {
            cargarMarcas(productos);
            }

        // INICIO - ORDEN de visualización
        DataView dv = productos.DefaultView;
        dv.Sort = ddl_ordenBy.SelectedValue  + " " + ddl_ordenTipo.SelectedValue;
       
        // FIN - ORDEN de visualización

        if(rd_filtroMarcas.SelectedValue != "") dv.RowFilter = "marca = '" + rd_filtroMarcas.SelectedValue + "'";

        productos = dv.ToTable();

        if(productos == null || productos.Rows.Count == 0) {
            cont_filtros.Visible = false;
            cont_ordenar.Visible = false;
            }
        lv_productos.DataSource = productos;
        lv_productos.DataBind();


      
    }
    protected void cargarMarcas(DataTable dtProductos) {
        dtProductos = dtProductos.AsEnumerable()
       .GroupBy(r => new { Col1 = r["marca"] })
       .Select(g => g.OrderBy(r => r["id"]).First())
       .CopyToDataTable();
        
        foreach (DataRow r in dtProductos.Rows) {
            rd_filtroMarcas.Items.Add(new ListItem(r["marca"].ToString(), r["marca"].ToString()));
            }
        rd_filtroMarcas.Items.Add(new ListItem("", "Todas las Marcas"));
        rd_filtroMarcas.SelectedValue = Request.QueryString["filtroMarcas"];

        }

    protected void lv_productos_OnItemDataBound(object sender, ListViewItemEventArgs e)
    {
        

        string monedaTienda = Session["monedaTienda"].ToString();


        usuarios datosUsuario = privacidadAsesores.modoAsesor();

        HyperLink link = (HyperLink)e.Item.FindControl("link_producto");
        HyperLink link_productoIMG = (HyperLink)e.Item.FindControl("link_productoIMG");
        HtmlGenericControl item_producto = (HtmlGenericControl)e.Item.FindControl("item_producto");

       

        System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;


        string[] rol_visibilidad = rowView["rol_visibilidad"].ToString().Replace(" ", "").Split(',');
        string usuario_rol_producto = datosUsuario.rol_productos;


        bool itemVisible = privacidad.validarProducto(rol_visibilidad, usuario_rol_producto);

        item_producto.Visible = itemVisible;

        // Si encontro incidencia seguimos todo los procesos correspondientes
        if (itemVisible) {

            string numero_parte = rowView["numero_parte"].ToString();
            string titulo = rowView["titulo"].ToString();
            string descripcion_corta = rowView["descripcion_corta"].ToString();
            string marca = rowView["marca"].ToString();

            NumberFormatInfo myNumberFormatInfo = new CultureInfo("es-MX", true).NumberFormat;

            // Quitamos el símbolo de pesos (establecido en string vacio)
            myNumberFormatInfo.CurrencySymbol = "";

            decimal? precio = null;

            if (rowView["precio"] != DBNull.Value)
            {

                precio = decimal.Parse(rowView["precio"].ToString());
            } else if (rowView["precio1"] != DBNull.Value)
            {
                precio = decimal.Parse(rowView["precio1"].ToString());
            }

          
            Label lbl_producto_precio  = (Label)e.Item.FindControl("lbl_producto_precio");
            lbl_producto_precio.Text = decimal.Parse(precio.ToString()).ToString("C2", myNumberFormatInfo);


            Label lbl_producto_moneda = (Label)e.Item.FindControl("lbl_producto_moneda");
            lbl_producto_moneda.Text = monedaTienda;

            Literal lt_descripcion_corta = (Literal)e.Item.FindControl("lt_descripcion_corta");

            if(descripcion_corta.Length > 120)
            {

                descripcion_corta = descripcion_corta.Remove(120, descripcion_corta.Length - 120)+"...";
            }

            lt_descripcion_corta.Text = descripcion_corta;

            string[] imagenes = rowView["imagenes"].ToString().Replace(" ","").Split(',');

            Image img_producto = (Image)e.Item.FindControl("img_producto");


            img_producto.ImageUrl = archivosManejador.imagenProducto(rowView["imagenes"].ToString().Split(',')[0]);

            link.NavigateUrl = GetRouteUrl("productos", new System.Web.Routing.RouteValueDictionary {
                        { "numero_parte", textTools.limpiarURL_NumeroParte(numero_parte) },
                        {"marca", marca},
                        { "productoNombre",  textTools.limpiarURL(titulo) }
                    });
            link_productoIMG.NavigateUrl = link.NavigateUrl;
        }
    }


   

}
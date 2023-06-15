using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Adapters;

public partial class userControls_productosTiendaListado : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            NotiflixJS.Loading(this, NotiflixJS.LoadingType.loading);
            cargarProductos(sender, e);
            uc_moneda.ddl_default();
            ProductosVisitados.cantidadCargar = 5;
            ProductosVisitados.slidesToShow = 1;
            ProductosVisitados.verticalMode = true;
            ProductosVisitados.obtenerProductos();
            //    loginForm.urlReturn = HttpContext.Current.Request.Url.AbsoluteUri;
            NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
        }
        NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
    }

    /// <summary>
    /// Contabiliza las categorias y cantidad de productos que tienen.
    /// </summary>
    protected void obtenerCategorias(DataTable dtProductos)
    {
        DataTable dtProductosFiltrado = new DataTable();
        dtProductosFiltrado.Columns.Add(new DataColumn("categoria_identificador", System.Type.GetType("System.String")));

        DataView view = new DataView(dtProductos);
        dtProductos = view.ToTable(false, "categoria_identificador");


        foreach (DataRow r in dtProductos.Rows)
        {


            string categorias = r["categoria_identificador"].ToString();

            // Un producto puede pertenecer a múltples categorias por esta razón se necesita validar si contiene comas y si es así, añadirlo po filas separadas cada valor en un nuevo DT
            if (categorias.Contains(","))
            {
                categorias = textTools.lineSimple(categorias);
                string[] array_categorias = categorias.Split(',');

                foreach (string cat in array_categorias)
                {


                    DataRow cat_row = dtProductosFiltrado.NewRow();
                    cat_row["categoria_identificador"] = cat;
                    dtProductosFiltrado.Rows.InsertAt(cat_row, 0);
                }

            }
            else
            {
                DataRow cat_row = dtProductosFiltrado.NewRow();
                cat_row["categoria_identificador"] = r["categoria_identificador"].ToString();
                dtProductosFiltrado.Rows.InsertAt(cat_row, 0);

            }

        }

        // query with LINQ 
        var query = from row in dtProductosFiltrado.AsEnumerable()
                    group row by row.Field<string>("categoria_identificador") into cat
                    orderby cat.Key
                    select new
                    {
                        identificador = cat.Key,
                        nombre = categorias.obtenerNombreCategoria(cat.Key) + " (" + cat.Count() + ")",
                        total = cat.Count()
                    }
                   into selection
                    orderby selection.total descending
                    select selection;


        ddl_filtroCategorias.DataSource = query;
        ddl_filtroCategorias.DataValueField = "identificador";
        ddl_filtroCategorias.DataTextField = "nombre";

        ddl_filtroCategorias.DataBind();

        ddl_filtroCategorias.Items.Insert(0, new ListItem("Todas las categorias", ""));
        ddl_filtroCategorias.SelectedValue = Request.QueryString["filtroCategorias"];




        //  Console.WriteLine();
    }
    protected void orden(object sender, EventArgs e)
    {
        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
        //   nameValues.Set("ordenTipo", ddl_ordenTipo.SelectedValue);
        // nameValues.Set("ordenBy", ddl_ordenBy.SelectedValue);
        nameValues.Set("filtroMarcas", ddl_filtroMarcas.SelectedValue);
        nameValues.Set("filtroCategorias", ddl_filtroCategorias.SelectedValue);

        Type type = sender.GetType();
        if (sender.GetType().Name == "DropDownList")
        {

            if ((sender as DropDownList).ID == "ddl_filtroCategorias")
            {
                nameValues.Set("filtroMarcas", "");
                nameValues.Set("PageId", "1");
            }
            else if ((sender as DropDownList).ID == "ddl_filtroMarcas")
            {
                nameValues.Set("filtroCategorias", "");
                nameValues.Set("PageId", "1");
            }
        }



        string url = Request.Url.AbsolutePath;
        Response.Redirect(url + "?" + nameValues); // ToString() is called implicitly
    }
    protected void cargarProductos(object sender, EventArgs e)
    {
        string monedaTienda = Session["monedaTienda"].ToString();
        usuarios datosUsuario = usuarios.modoAsesor();
        DataTable productos = new DataTable();
        productosTienda obtener = new productosTienda();
        preciosTienda procesar = new preciosTienda();

        if (Page.RouteData.Values["identificador"] != null)
        {
            string categoriaID = Page.RouteData.Values["identificador"].ToString();
            productos = obtener.obtenerProductos_RolyCat(datosUsuario.rol_productos, categoriaID);
        }
        else if (Request.QueryString["busqueda"] != null)
        {
            string terminos = Request.QueryString["busqueda"].ToString();
            if (terminos.Length > 80) terminos = terminos.Substring(0, 100);
            productos = obtener.obtenerProductosFullTextSearch_Contains(terminos);
            if (productos.Rows.Count > 0)
            {
                content_resultado_busqueda_text.Visible = true;
                linkTerminoBusqueda.Text = terminos;
                linkTerminoBusqueda.NavigateUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/productos/buscar?busqueda=" + terminos;
                HttpRequest request = HttpContext.Current.Request;
                BI_historialBusqueda.guardarBusqueda(terminos, request);
            }
            else
            {
                content_resultado_busqueda_vacio.Visible = true;
                lbl_termino_busqueda.Text = terminos;
            }
        }

        if (productos != null && productos.Rows.Count > 0)
        {
            contentResultados.Visible = true;
            cont_categorias.Visible = true;
            cont_filtros.Visible = true;
            cont_moneda.Visible = true;
            productos.AcceptChanges();
            procesar.monedaTienda = monedaTienda;
            productos = procesar.procesarProductos(productos);
            double cantidadPaginas = (double)productos.Rows.Count / dp_2.PageSize;
            decimal paginaActual = (dp_2.StartRowIndex / dp_2.MaximumRows) + 1;

            lbl_contadorPaginas.Text = paginaActual + " de " + Math.Ceiling(cantidadPaginas);

            cargarMarcas(productos);
            obtenerCategorias(productos);

            if (ddl_filtroMarcas.SelectedValue != "")
            {
                DataView dv = new DataView(productos);
                dv.RowFilter = "marca = '" + ddl_filtroMarcas.SelectedValue + "'";
                productos = dv.ToTable();
            }

            if (ddl_filtroCategorias.SelectedValue != "")
            {
                DataView dv = new DataView(productos);
                dv.RowFilter = "categoria_identificador  LIKE '%" + ddl_filtroCategorias.SelectedValue + "%'";
                productos = dv.ToTable();
            }

            lv_productos.DataSource = productos;
            lv_productos.DataBind();

            string filtroCat = Request.QueryString["filtroCategorias"];

            if (!string.IsNullOrWhiteSpace(filtroCat))
            {
                ListItem itemCat = ddl_filtroCategorias.Items.FindByValue(filtroCat);

                lt_termino_busqueda.Text += " > " + itemCat.Text;
            }
        }
        else if (Page.RouteData.Values["identificador"] != null)
        {
            cont_filtros.Visible = false;
            cont_ordenar.Visible = false;
            no_productos.Visible = false;
        }
        else
        {
            cont_filtros.Visible = false;
            cont_ordenar.Visible = false;
            no_productos.Visible = true;
        }
    }
    protected void cargarMarcas(DataTable dtProductos)
    {
        TextInfo myTI = new CultureInfo("es-MX", false).TextInfo;

        // query with LINQ 
        var query = from row in dtProductos.AsEnumerable()

                    group row by row.Field<string>("marca") into marca

                    select new
                    {

                        nombre = myTI.ToTitleCase(marca.Key.ToLower()) + " (" + marca.Count() + ")",
                        valor = marca.Key,
                        count = marca.Count(),
                    } into selection
                    orderby selection.count descending
                    select selection;


        ddl_filtroMarcas.DataSource = query;
        ddl_filtroMarcas.DataValueField = "valor";
        ddl_filtroMarcas.DataTextField = "nombre";

        ddl_filtroMarcas.DataBind();

        ddl_filtroMarcas.Items.Insert(0, new ListItem("Todas las marcas", ""));

        ddl_filtroMarcas.SelectedValue = Request.QueryString["filtroMarcas"];

        /*
        dtProductos = dtProductos.AsEnumerable()
       .GroupBy(r => new { Col1 = r["marca"] })
       .Select(g => g.OrderBy(r => r["id"]).First())
       .CopyToDataTable();
        
        foreach (DataRow r in dtProductos.Rows) {
            ddl_filtroMarcas.Items.Add(new ListItem(r["marca"].ToString().ToLower(), r["marca"].ToString()));

            }
        ddl_filtroMarcas.Items.Insert(0,new ListItem("Todas las Marcas",""));
        ddl_filtroMarcas.SelectedValue = Request.QueryString["filtroMarcas"];

        ddl_filtroMarcas.DataBind();


        */
    }
    protected void lv_productos_OnItemDataBound(object sender, ListViewItemEventArgs e)
    {
        System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;
        string monedaTienda = Session["monedaTienda"].ToString();
        string disponibleVenta = rowView["disponibleVenta"].ToString();
        usuarios datosUsuario = privacidadAsesores.modoAsesor();
        LinkButton t = new LinkButton();
        HyperLink link = (HyperLink)e.Item.FindControl("link_producto");
        HyperLink link_productoIMG = (HyperLink)e.Item.FindControl("link_productoIMG");
        HtmlGenericControl item_producto = (HtmlGenericControl)e.Item.FindControl("item_producto");
        HtmlAnchor btn_VerDisponibilidad = (HtmlAnchor)e.Item.FindControl("btn_VerDisponibilidad");

        if (disponibleVenta == "0")
        {
            item_producto.Visible = false;
            return;
        }

        string[] rol_visibilidad = rowView["rol_visibilidad"].ToString().Replace(" ", "").Split(',');
        string usuario_rol_producto = datosUsuario.rol_productos;
        bool itemVisible = privacidad.validarProducto(rol_visibilidad, usuario_rol_producto);
        item_producto.Visible = itemVisible;

        // Si encontro incidencia seguimos todo los procesos correspondientes
        if (itemVisible)
        {
            string id = rowView["id"].ToString();
            string numero_parte = rowView["numero_parte"].ToString();
            string noParte_Sap = rowView["noParte_Sap"].ToString();
            string titulo = rowView["titulo"].ToString();
            string descripcion_corta = rowView["descripcion_corta"].ToString();
            string marca = rowView["marca"].ToString();
            string str_solo_para_Visualizar = rowView["solo_para_Visualizar"].ToString();
            bool solo_para_Visualizar = string.IsNullOrEmpty(str_solo_para_Visualizar) ? false : bool.Parse(str_solo_para_Visualizar);
            string avisos = rowView["avisos"].ToString();
            string disponibleEnvio = rowView["disponibleEnvio"].ToString();

            NumberFormatInfo myNumberFormatInfo = new CultureInfo("es-MX", true).NumberFormat;

            // Quitamos el símbolo de pesos (establecido en string vacio)
            myNumberFormatInfo.CurrencySymbol = "";

            Label lbl_disponibilidad_stock = (Label)e.Item.FindControl("lbl_disponibilidad_stock");
            Label lbl_producto_precio = (Label)e.Item.FindControl("lbl_producto_precio");
            Label lbl_producto_moneda = (Label)e.Item.FindControl("lbl_producto_moneda");
            Label lbl_preciosFantasma = (Label)e.Item.FindControl("lbl_preciosFantasma");
            Label lbl_descuento_porcentaje_fantasma = (Label)e.Item.FindControl("lbl_descuento_porcentaje_fantasma");
            Label lbl_envioGratuito = (Label)e.Item.FindControl("lbl_envioGratuito");
            HtmlGenericControl cnt_addOperacion = (HtmlGenericControl)e.Item.FindControl("cnt_addOperacion");

            link.Target = "_self";

            link.NavigateUrl = Request.Url.GetLeftPart(UriPartial.Authority) + GetRouteUrl("productos", new System.Web.Routing.RouteValueDictionary {
                        { "numero_parte", textTools.limpiarURL_NumeroParte(numero_parte) },
                         { "marca", marca },
                        { "productoNombre",  textTools.limpiarURL(titulo) }
                    });
            link_productoIMG.NavigateUrl = link.NavigateUrl;

            if (disponibleEnvio == "1")
            {
                lbl_envioGratuito.Attributes.Add("class", "is-text-xs is-text-white is-font-semibold is-select-none");
                lbl_envioGratuito.Text = "Envio gratis &starf;";
            }
            else
            {
                lbl_envioGratuito.Attributes.Add("style", "height: 22.5px");
            }

            btn_VerDisponibilidad.Attributes.Add("onclick", $"openModalProductoDisponibilidad('{numero_parte}');");

            Label lbl_bandera = (Label)e.Item.FindControl("lbl_bandera");
            string bandera = rowView["bandera"].ToString().ToUpper();

            switch (bandera)
            {
                case "OFERTA":
                    lbl_bandera.Attributes.Add("class", "is-block is-tag is-text-white is-text-xs is-font-bold is-py-1 is-select-none is-bg-offer");
                    break;
                case "LIQUIDACIÓN":
                    lbl_bandera.Attributes.Add("class", "is-block is-tag is-text-white is-text-xs is-font-bold is-py-1 is-select-none is-bg-liquidation");
                    break;
                case "ÚLTIMAS":
                    lbl_bandera.Attributes.Add("class", "is-block is-tag is-text-white is-text-xs is-font-bold is-py-1 is-select-none is-bg-lastItems");
                    bandera = "ÚLTIMAS PIEZAS";
                    break;
                case "PERSONALIZADO":
                    lbl_bandera.Attributes.Add("class", "is-block is-tag is-text-white is-text-xs is-font-bold is-py-1 is-select-none is-bg-customized");
                    break;
                case "EXISTENCIAS":
                    lbl_bandera.Attributes.Add("class", "is-block is-tag is-text-white is-text-xs is-font-bold is-py-1 is-select-none is-bg-customized");
                    bandera = "Precio válido solo en existencias";
                    break;
                case "PEDIDO":
                    lbl_bandera.Attributes.Add("class", "is-block is-tag is-text-white is-text-xs is-font-bold is-py-1 is-select-none is-bg-onRequest");
                    bandera = "SOBRE PEDIDO";
                    break;
                case "RENTA":
                    lbl_bandera.Attributes.Add("class", "is-block is-tag is-text-white is-text-xs is-font-bold is-py-1 is-select-none is-bg-rent");
                    bandera = "VENTA Y RENTA";
                    break;
                case "SOLORENTA":
                    lbl_bandera.Attributes.Add("class", "is-block is-tag is-text-white is-text-xs is-font-bold is-py-1 is-select-none is-bg-onlyrent");
                    bandera = "RENTA";
                    break;
                case "NUEVO":
                    lbl_bandera.Attributes.Add("class", "is-block is-tag is-text-white is-text-xs is-font-bold is-py-1 is-select-none is-bg-new");
                    bandera = "NUEVO";
                    break;
                default:
                    lbl_bandera.Attributes.Add("style", "height: 22px; display: block");
                    break;
            }

            lbl_bandera.Text = bandera;
            if (!solo_para_Visualizar)
            {
                //  var DisponibilidadStockSAP = SAP_productos_oData.ObtenerStockMensaje(noParte_Sap, numero_parte);
                //  lbl_disponibilidad_stock.Text = DisponibilidadStockSAP;

                decimal? precio = null;



                if (rowView["precio"] != DBNull.Value)
                {

                    precio = decimal.Parse(rowView["precio"].ToString());
                }
                else if (rowView["precio1"] != DBNull.Value)
                {
                    precio = decimal.Parse(rowView["precio1"].ToString());
                }
                decimal PrecioConImpuestos = Impuestos.ObterPrecioConImpuestos((decimal)precio);
                lbl_producto_precio.Text = decimal.Parse(PrecioConImpuestos.ToString()).ToString("C2", myNumberFormatInfo);
                #region Precios fantasma
                if (rowView["preciosFantasma"] != DBNull.Value)
                {

                    decimal preciosFantasma = decimal.Parse(rowView["preciosFantasma"].ToString());


                    decimal PrecioFantasmaConImpuestos = Impuestos.ObterPrecioConImpuestos((decimal)preciosFantasma);


                    lbl_preciosFantasma.Visible = true;
                    lbl_preciosFantasma.Text = "$" + decimal.Parse(PrecioFantasmaConImpuestos.ToString()).ToString("C2", myNumberFormatInfo);
                }
                if (rowView["porcentajeFantasma"] != DBNull.Value)
                {

                    int porcentajeFantasma = int.Parse(rowView["porcentajeFantasma"].ToString());
                    lbl_descuento_porcentaje_fantasma.Visible = true;
                    lbl_descuento_porcentaje_fantasma.Text = $"{porcentajeFantasma}% de descuento";
                }
                #endregion
            }
            else
            {
                btn_VerDisponibilidad.Visible = false;
                lbl_producto_moneda.Visible = false;
                lbl_producto_precio.Text = "Cotízalo";

                uc_btn_agregar_carritoListado AddCarrito = (uc_btn_agregar_carritoListado)e.Item.FindControl("AddCarrito");
                uc_producto_btn_SoloVisualizar linkVisualizarProducto = (uc_producto_btn_SoloVisualizar)e.Item.FindControl("linkVisualizarProducto");
                linkVisualizarProducto.setLink(link.NavigateUrl);
                linkVisualizarProducto.Establecer_Numero_Parte(numero_parte);
                AddCarrito.Visible = false;
                linkVisualizarProducto.Visible = true;
            }








            lbl_producto_moneda.Text = monedaTienda;

            Label lbl_marca = (Label)e.Item.FindControl("lbl_marca");
            lbl_marca.Text = marca.Length > 12 ? marca.Substring(0, 10) + "..." : marca;
            lbl_marca.Attributes.Add("data-tooltip", marca);
            Literal lt_descripcion_corta = (Literal)e.Item.FindControl("lt_descripcion_corta");

            /*   if(descripcion_corta.Length > 120)
               {

                   descripcion_corta = descripcion_corta.Remove(120, descripcion_corta.Length - 120)+"...";
               }
               */


            lt_descripcion_corta.Text = descripcion_corta;
            //string imagen = rowView["imagenes"].ToString();
            Panel contentSlider = (Panel)e.Item.FindControl("contentSlider");

            HtmlGenericControl ulSlider = new HtmlGenericControl("ul");
            ulSlider.Attributes.Add("class", "sliderProductos");
            //if (imagen.Contains(","))
            //{
            //    string[] arr_imagenes = rowView["imagenes"].ToString().Replace(" ", "").Split(',');

            //    foreach (string r in arr_imagenes)
            //    {
            //        Image img = new Image();
            //        img.Attributes.Add("loading", "lazy");
            //        img.ImageUrl = archivosManejador.imagenProducto(r);
            //        img.AlternateText = titulo;
            //        img.ToolTip = descripcion_corta;

            //        img.CssClass += " IncomWebpToJpg";

            //        HtmlGenericControl li = new HtmlGenericControl("li");

            //        li.Attributes.Add("onclick", "cargarSliderModalListadoProductos('content_imgProducto_" + id + "','" + link.NavigateUrl + "')");
            //        li.Controls.Add(img);
            //        ulSlider.Controls.Add(li);
            //    }
            //    contentSlider.Controls.Add(ulSlider);

            //}
            //else
            //{
            //    Image img_producto = new Image();
            //    img_producto.EnableViewState = true;
            //    img_producto.Attributes.Add("loading", "lazy");
            //    img_producto.ImageUrl = archivosManejador.imagenProducto(rowView["imagenes"].ToString().Split(',')[0]);
            //    img_producto.AlternateText = titulo;
            //    img_producto.ToolTip = descripcion_corta;
            //    //   img_producto.AlternateText = titulo;
            //    img_producto.CssClass += " IncomWebpToJpg";

            //    HtmlGenericControl li = new HtmlGenericControl("li");

            //    li.Attributes.Add("onclick", "cargarSliderModalListadoProductos('content_imgProducto_" + id + "','" + link.NavigateUrl + "')");
            //    li.Controls.Add(img_producto);
            //    ulSlider.Controls.Add(li);

            //    contentSlider.Controls.Add(ulSlider);



            //}

            Image img_producto = new Image();
            img_producto.EnableViewState = true;
            img_producto.Attributes.Add("loading", "lazy");
            img_producto.ImageUrl = archivosManejador.imagenProducto(rowView["imagenes"].ToString().Split(',')[0]);
            img_producto.AlternateText = titulo;
            img_producto.ToolTip = descripcion_corta;
            //   img_producto.AlternateText = titulo;
            img_producto.CssClass += " IncomWebpToJpg";

            HtmlGenericControl li = new HtmlGenericControl("li");

            li.Attributes.Add("onclick", "cargarSliderModalListadoProductos('content_imgProducto_" + id + "','" + link.NavigateUrl + "')");
            li.Controls.Add(img_producto);
            ulSlider.Controls.Add(li);

            contentSlider.Controls.Add(ulSlider);


            #region Validación usuario logeado (cliente,admin)
            var userLogin = usuarios.userLogin();

            if (userLogin.tipo_de_usuario == "usuario" && userLogin.rango == 3 &&
             rowView.DataView.Table.Columns.Contains("puntajeBusqueda") == true)
            {
                if (rowView["puntajeBusqueda"] != DBNull.Value)
                {
                    Label lbl_puntajeBusqueda = (Label)e.Item.FindControl("lbl_puntajeBusqueda");
                    string puntaje = rowView["puntajeBusqueda"].ToString();
                    lbl_puntajeBusqueda.Visible = false;
                    lbl_puntajeBusqueda.Text = "Puntaje: " + puntaje;
                }
            }
            if (userLogin.rango == 3)
            {
                cnt_addOperacion.Visible = true;
            }
            #endregion
        }
        NotiflixJS.Loading(this, NotiflixJS.LoadingType.remove);
    }
}
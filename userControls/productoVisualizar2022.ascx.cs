
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class userControls_productoVisualizar : System.Web.UI.UserControl {

    protected void Page_Load(object sender, EventArgs e) {
       
    }
    protected async void guardarHit() {
        if (HttpContext.Current.User.Identity.IsAuthenticated && usuarios.userLogin().tipo_de_usuario == "cliente") {

            HttpRequest request = HttpContext.Current.Request;

            model_BI_historialProductos producto = new model_BI_historialProductos();
            producto.idUsuario = usuarios.userLogin().id;
            producto.numero_parte = lt_numero_parte.Text;
            producto.fecha = utilidad_fechas.obtenerCentral();
            producto.direccion_ip = red.GetDireccionIp(request);
            BI_historialProductos.guardarHitProducto(producto); 
        }
    }


    protected void Page_PreRender(object sender, EventArgs e) {

        if (!IsPostBack) {
            cargarProducto();
        }


    }
    protected void Page_Unload(object sender, EventArgs e) {

    }
    protected void cargarProducto() {

        if (Page.RouteData.Values["numero_parte"] != null) {
            string route_numero_parte = Page.RouteData.Values["numero_parte"].ToString();

            productosTienda obtener = new productosTienda();
            DataTable productos = obtener.obtenerProducto(textTools.recuperarURL_NumeroParte(route_numero_parte));

            string[] rol_visibilidadProducto = productos.Rows[0]["rol_visibilidad"].ToString().Split(',');
            usuarios datosUsuario = (usuarios)System.Web.HttpContext.Current.Session["datosUsuario"];


            string str_ProductoAvisos = textTools.lineSimple(productos.Rows[0]["avisos"].ToString());
            if (!string.IsNullOrWhiteSpace(str_ProductoAvisos)) {
                string[] ProductoAvisos = str_ProductoAvisos.Split('~');
                if (ProductoAvisos.Length > 0) Array.ForEach(ProductoAvisos, i => ProductoAvisosListado.InnerHtml += $"<li>{i}</li>");
            
            }



            ProductoAvisosListado.InnerHtml += "<li>La disponibilidad puede cambiar sin previo aviso.</li>";



               bool productoVisible = privacidad.validarProducto(rol_visibilidadProducto, datosUsuario.rol_productos);
            // Validamos la privacidad del producto para proceder, si no mostramos mensaje
            if (productoVisible) {


                // Parent = master > Parent Page
                UserControl uc_moneda = this.FindControl("uc_moneda") as UserControl;
                string monedaTienda = (uc_moneda.FindControl("ddl_moneda") as DropDownList).SelectedValue;

                preciosTienda procesar = new preciosTienda();
                procesar.monedaTienda = monedaTienda;

                productos = procesar.procesarProductos(productos);

                string numero_parte = productos.Rows[0]["numero_parte"].ToString();
                string noParte_Sap = productos.Rows[0]["noParte_Sap"].ToString();


                string marca = productos.Rows[0]["marca"].ToString();
                string descripcion_corta = productos.Rows[0]["descripcion_corta"].ToString();



                string titulo = productos.Rows[0]["titulo"].ToString();
                string especificaciones = productos.Rows[0]["especificaciones"].ToString();
                string unidad_venta = productos.Rows[0]["unidad_venta"].ToString();
                string cantidad = productos.Rows[0]["cantidad"].ToString();
                string productos_relacionados = productos.Rows[0]["productos_relacionados"].ToString();


                string unidad = productos.Rows[0]["unidad"].ToString();
                string imagen_URL = Request.Url.GetLeftPart(UriPartial.Authority) + "/img_catalog/" + productos.Rows[0]["imagenes"].ToString().Split(',')[0];

                string peso = productos.Rows[0]["peso"].ToString();
                string alto = productos.Rows[0]["alto"].ToString();
                string ancho = productos.Rows[0]["ancho"].ToString();
                string profundidad = productos.Rows[0]["profundidad"].ToString();
                string etiquetas = productos.Rows[0]["etiquetas"].ToString();

                string video = productos.Rows[0]["video"].ToString();

                try {
                    if (video.Contains("youtube")) {

                        int posicion = video.IndexOf("?v=");

                        if (video.Contains("&list")) {

                            video = video.Remove(0, posicion + 3);
                            int posicionFinPlay = video.IndexOf("&list");
                            int aEliminar = video.Length - posicionFinPlay;

                            video = video.Remove(posicionFinPlay, aEliminar);
                        } else video = video.Remove(0, posicion + 3);



                        cont_videos.InnerHtml = @"<iframe width='560' height='315' src='https://www.youtube.com/embed/"
                            + video + " ' frameborder=\"0\" allow =\"accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture\" allowfullscreen></iframe>";

                    }

                }
                catch (Exception ex) {
                    devNotificaciones.error("Cortar url videos en producto", ex);
                }
                string url = Request.Url.GetLeftPart(UriPartial.Authority) + GetRouteUrl("productos", new System.Web.Routing.RouteValueDictionary {
                        { "numero_parte", textTools.limpiarURL_NumeroParte(numero_parte) },
                         { "marca", marca },
                        { "productoNombre",  textTools.limpiarURL(titulo) }
                    });


                lt_numero_parte.Text = numero_parte;
                lt_titulo.Text = titulo;
                lbl_descripcion_corta.Text = descripcion_corta;
                lbl_especificaciones.Text = especificaciones;



                string str_solo_para_Visualizar = productos.Rows[0]["solo_para_Visualizar"].ToString();
                bool solo_para_Visualizar = string.IsNullOrEmpty(str_solo_para_Visualizar) ? false : bool.Parse(str_solo_para_Visualizar);



                NumberFormatInfo myNumberFormatInfo = new CultureInfo("es-MX", true).NumberFormat;

                // Quitamos el símbolo de pesos (establecido en string vacio)
                myNumberFormatInfo.CurrencySymbol = "";


                decimal? precio = null;


                if (!solo_para_Visualizar)
                {
                    // La columna [precio] solo se habilita si el cliente tiene asignado un precio fijo, por lo tanto es el precio a mostrar
                    if (productos.Rows[0]["precio"] != DBNull.Value) {
                    precio = decimal.Parse(productos.Rows[0]["precio"].ToString());

                    // Al ser un precio general, debemos mostrar que tiene cierto descuento siempre y cuando el precio para el cliente sea menor que el general
                    decimal? precioGeneral = procesar.obtenerPrecioGeneralProducto(numero_parte);

                    if (precioGeneral != null && precioGeneral > precio) {

                        lbl_precioGeneral.Visible = true;
                        lbl_precioGeneralLeyenda.Visible = true;
                        lbl_precioGeneral.Text = "$ " + float.Parse(precioGeneral.ToString()).ToString("C2", myNumberFormatInfo) + monedaTienda;
                    }
                    // Si es [precio1] esta tomando datos de los tabuladores y precios generales.
                } else if (productos.Rows[0]["precio1"] != DBNull.Value) {
                    precio = decimal.Parse(productos.Rows[0]["precio1"].ToString());


                    // INICIO - Sirve para mostrar si hay precio de lista, solo NO HAY hay un precio fijo para un cliente en especial
                    DataTable dtProducoPrecioLista = preciosTienda.obtenerProductoPrecioLista(numero_parte);
                    if (dtProducoPrecioLista != null) {
                        string moneda = dtProducoPrecioLista.Rows[0]["moneda_fija"].ToString();
                        decimal precioDeLista = decimal.Parse(dtProducoPrecioLista.Rows[0]["precio"].ToString());
                        precioDeLista = procesar.precio_a_MonedaTienda(moneda, precioDeLista);

                        lbl_precioGeneralLeyenda.Visible = true;
                        lbl_precioGeneralLeyenda.Text = "Tu precio usuario registrado ✓";
                        lbl_precioLista.Visible = true;
                        lbl_precioLista.Text = "$ " + decimal.Parse(precioDeLista.ToString()).ToString("C2", myNumberFormatInfo) + monedaTienda;
                    }

                    // Fin - Sirve para mostrar si hay precio de lista, solo si NO HAY un precio fijo  para un cliente en especial
                }
                 
                    lbl_precio.Text = float.Parse(precio.ToString()).ToString("C2", myNumberFormatInfo);
                    lbl_moneda.Text = monedaTienda;


                    detalles_precios.numero_parte = numero_parte;
                    detalles_precios.moneda = monedaTienda;
                    detalles_precios.size = "max";


                    #region Precios fantasma
                    if (productos.Rows[0]["preciosFantasma"] != DBNull.Value) {

                        decimal preciosFantasma = decimal.Parse(productos.Rows[0]["preciosFantasma"].ToString());


                     

                        lbl_preciosFantasma.Visible = true;
                        lbl_preciosFantasma.Text = "$" + decimal.Parse(preciosFantasma.ToString()).ToString("C2", myNumberFormatInfo);
                    }
                    if (productos.Rows[0]["porcentajeFantasma"] != DBNull.Value) {

                        int porcentajeFantasma = int.Parse( productos.Rows[0]["porcentajeFantasma"].ToString());
                        lbl_descuento_porcentaje_fantasma.Visible = true;
                        lbl_descuento_porcentaje_fantasma.Text = $"{porcentajeFantasma}% de descuento";
                            }

                    #endregion
                }
                else
                {

                    lbl_moneda.Visible = false;
                    lbl_precio.Text = "Cotízalo";

                      linkVisualizarProducto.solicitarInforme();
                    AddCarrito.Visible = false;
                    linkVisualizarProducto.Visible = true;
                    linkVisualizarProducto.Establecer_Numero_Parte(numero_parte);
                }



                lbl_marca.Text = marca;
                lbl_numero_parte.Text = numero_parte;

                lt_unidad_venta.Text = unidad_venta;
                lt_cantidad.Text = cantidad;
                lt_unidad.Text = unidad;

                procesarImagenes(productos.Rows[0]["imagenes"].ToString(), titulo, descripcion_corta);

                cargarNavegacion(productos.Rows[0]["categoria_identificador"].ToString());

                procesarCaracteristicas(productos.Rows[0]["atributos"].ToString());
                procesarDocumentacion(productos.Rows[0]["pdf"].ToString());
                tbody_dimensiones_empaque.InnerHtml += @"
            <tr>" +
                    "<td style='white-space: nowrap; text-align:right; padding-right:4px;'><strong>Peso</strong>:</td>" +
                    "<td style='white-space: nowrap; width:100%;'>" + productos.Rows[0]["peso"].ToString() + " kg</td>" +
                "</tr>" +
                 "<tr>" +
                    "<td style='white-space: nowrap; text-align:right; padding-right:4px;'><strong>Alto </strong>:</td>" +
                    "<td style='white-space: nowrap; width:100%;'>" + productos.Rows[0]["alto"].ToString() + " cm</td>" +
                "</tr>" +
                "<tr>" +
                    "<td style='white-space: nowrap; text-align:right; padding-right:4px;'><strong>Ancho</strong>:</td>" +
                    "<td style='white-space: nowrap; width:100%;'>" + productos.Rows[0]["ancho"].ToString() + " cm</td>" +
                "</tr>" +
                "<tr>" +
                    "<td style='white-space: nowrap; text-align:right; padding-right:4px;'><strong>Largo/Profundidad </strong>:</td>" +
                    "<td style='white-space: nowrap; width:100%;'>" + productos.Rows[0]["profundidad"].ToString() + " cm</td>" +
                "</tr>";

             


                // INICIO SEO TAGS - 

                this.Page.Title = lt_titulo.Text + " " + lt_numero_parte.Text + ", Marca " + marca;
                this.Page.MetaDescription = titulo + " " + descripcion_corta;


                HtmlGenericControl urlCanonical = new HtmlGenericControl("link");
                urlCanonical.Attributes.Add("rel", "canonical");
                urlCanonical.Attributes.Add("href", url);
                Page.Header.Controls.Add(urlCanonical);


                HtmlMeta og_site_name = new HtmlMeta();
                HtmlMeta og_title = new HtmlMeta();
                HtmlMeta og_description = new HtmlMeta();
                HtmlMeta og_url = new HtmlMeta();
                HtmlMeta og_image = new HtmlMeta();
                HtmlMeta og_type = new HtmlMeta();

                og_site_name.Attributes.Add("property", "og:site_name");
                og_site_name.Content = "Incom México";

                og_title.Attributes.Add("property", "og:title");
                og_title.Content = titulo;

                og_description.Attributes.Add("property", "og:description");
                og_description.Content = descripcion_corta.Length > 150 ? descripcion_corta.Substring(0, 149) : descripcion_corta;

                og_url.Attributes.Add("property", "og:url");
                og_url.Content = url;

                og_image.Attributes.Add("property", "og:image");
                og_image.Content = imagen_URL;

                og_type.Attributes.Add("property", "og:type");
                og_type.Content = "product";

                Page.Header.Controls.Add(og_site_name);
                Page.Header.Controls.Add(og_title);
                Page.Header.Controls.Add(og_description);
                Page.Header.Controls.Add(og_url);
                Page.Header.Controls.Add(og_image);
                Page.Header.Controls.Add(og_type);


                // INICIO Twitter


                HtmlMeta tw_card = new HtmlMeta();
                HtmlMeta tw_title = new HtmlMeta();
                HtmlMeta tw_description = new HtmlMeta();
                HtmlMeta tw_site = new HtmlMeta();
                HtmlMeta tw_image = new HtmlMeta();


                tw_card.Attributes.Add("name", "twitter:card");
                tw_card.Content = "summary_large_image";

                tw_title.Attributes.Add("name", "twitter:title");
                tw_title.Content = titulo;

                tw_description.Attributes.Add("name", "twitter:description");
                tw_description.Content = descripcion_corta;

                tw_site.Attributes.Add("name", "twitter:site");
                tw_site.Content = "@incom_mx";

                tw_image.Attributes.Add("name", "twitter:image");
                tw_image.Content = imagen_URL;



                Page.Header.Controls.Add(tw_card);
                Page.Header.Controls.Add(tw_title);
                Page.Header.Controls.Add(tw_description);
                Page.Header.Controls.Add(tw_site);
                Page.Header.Controls.Add(tw_image);


                // FIN Twitter
                // FIN SEO TAGS - 

                productoAddOperacion.numero_parte = numero_parte;
                productoAddOperacion.descripcion_corta = descripcion_corta;

                stock_producto.numero_parte = noParte_Sap;
                stock_producto.obtenerStock();




 

                string microDataProducto = @"
                <script type = ""application/ld+json"">
                {
                         ""@context"": ""http://schema.org"",
                         ""@id"": ""#product"",
                         ""@type"": ""IndividualProduct"",
                         ""name"":"" " + titulo.Replace('"', '\'') + @" "",
                         ""brand"": "" " + marca.Replace('"', '\'') + @" "",
                         ""category"": "" " + descripcion_corta.Replace('"', '\'') + @" "",
                         ""depth"": "" " + profundidad.Replace('"', '\'') + @" "",
                         ""height"": "" " + alto.Replace('"', '\'') + @" "",
                         ""weight"": "" " + peso.Replace('"', '\'') + @" "",
                         ""width"": "" " + ancho.Replace('"', '\'') + @" "",
                         ""image"": "" " + imagen_URL.Replace('"', '\'') + @" "",
                         ""manufacturer"": "" " + marca.Replace('"', '\'') + @" "",
                         ""model"": "" " + numero_parte.Replace('"', '\'') + @" "",
                         ""sku"": "" " + numero_parte.Replace('"', '\'') + @" "",
                         ""url"": "" " +url + @" "",
                        ""description"": "" " + descripcion_corta.Replace('"', '\'') + @" "",
                        ""offers"": {
                                    ""@type"": ""Offer"",
                                    ""availability"": ""http://schema.org/InStock"",
                                    ""price"": """+ precio +@""",
                                    ""priceCurrency"": """+monedaTienda+ @""",
                                    ""itemCondition"": "" https://schema.org/NewCondition""
                                     }
                  }
                </script>";

                lt_microdataProducto.Text = microDataProducto;



                guardarHit();



                productosRelacionados.productos = productos_relacionados;
                productosRelacionados.uc_moneda = uc_moneda;
                productosRelacionados.obtenerProductos();

            }
            else {  // Producto no visible
                contenedor_producto.Visible = productoVisible;
                h1ProductoNoDisponible.Visible = true;
            }
        } else {

            contenedor_producto.Visible = false;
            h1ProductoNoDisponible.Visible = true;

        }

    }
    protected void documentacionExterna(string _documentacionPDF) {

        HyperLink link = new HyperLink();
        link.CssClass = "waves -effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5";
        link.Text = @"<i class='material-icons left'>description</i>";
        link.Text += " Ficha Técnica";
        link.NavigateUrl = _documentacionPDF;
        link.Target = "_blank";
        cont_documentacion.Controls.Add(link);

    }

    protected void procesarDocumentacion(string _documentacionPDF) {
        // sí el documento contiene un enlace a ficha externa
        if (_documentacionPDF.Contains("http") && !_documentacionPDF.Contains("_new")) {
            documentacionExterna(_documentacionPDF);
            return;
            }


        if (_documentacionPDF.Contains("_new")) {

            _documentacionPDF = _documentacionPDF.Replace("_new", "");
            JavaScriptSerializer deserializer = new JavaScriptSerializer();

            Dictionary<string, object> D_documentacionPDF = deserializer.Deserialize<Dictionary<string, object>>("{" + _documentacionPDF + "}");

         
            foreach (var atributo in D_documentacionPDF)
            {

               
                    HyperLink link = new HyperLink();
                    link.CssClass = "waves -effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5";
                    link.Text = @"<i class='material-icons left'>description</i>";
                    if (atributo.Key.ToLower() == "ft") { link.Text += " Ficha Técnica"; }
                    else if (link.Text == "") { link.Text += " Más documentación"; }
                    else { link.Text = "<i class='material-icons left'>description</i> " + atributo.Key; }

                if (atributo.Value.ToString().Contains("http")) link.NavigateUrl = atributo.Value.ToString();
                else {
                    if (!archivosManejador.validarExistenciaPDF(atributo.Value.ToString())) {
                        continue;
                        }
                    link.NavigateUrl = Request.Url.GetLeftPart(UriPartial.Authority) + archivosManejador.pdfDirectorioWeb + atributo.Value;
                    } 

                    link.Target = "_blank";
                    cont_documentacion.Controls.Add(link);

                


           
               

            }




        }
        else   //Si contiene múltiples fichas en el formato antiguo (separado por comas
        {


            string[] documentacionPDF = _documentacionPDF.Split(',');


            if (documentacionPDF.Length >= 1)
            {


                for (int i = 0; i < documentacionPDF.Length; i++)
                {

                    if (archivosManejador.validarExistenciaPDF(documentacionPDF[i]))
                    {
                        HyperLink link = new HyperLink();
                        link.CssClass = "waves -effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5";
                        link.Text = @"<i class='material-icons left'>description</i>";
                        if (i == 0) link.Text += " Ficha Técnica"; else link.Text += " Más información";
                        link.NavigateUrl = Request.Url.GetLeftPart(UriPartial.Authority) + archivosManejador.pdfDirectorioWeb + documentacionPDF[i];
                        link.Target = "_blank";
                        cont_documentacion.Controls.Add(link);

                    }


                }

            }

        }




    }
    protected void procesarImagenes(string img, string alt, string title) {

        img = img.Replace(" ", "");
        string[] imagenes = img.Split(',');

        string imgHTML = "";

        if (imagenes.Length > 1) {

            foreach (string i in imagenes) {
                string src = archivosManejador.imagenProductoXL(i);
                imgHTML += "<a  href='" + src + "'><img  class='IncomWebpToJpg' alt='" + alt + "' title='" + title + "' src='" + src + "'></a>";
            }
            img_producto.InnerHtml = imgHTML;
        } else if (imagenes.Length == 1) {
            string src = archivosManejador.imagenProductoXL(imagenes[0]);
            img_producto.InnerHtml = "<a href='" + src + "'><img  class='IncomWebpToJpg' alt='" + alt + "' title='" + title + "' src='" + src + "'></a>";
        }
    }
    protected void procesarCaracteristicas(string caracteristicas) {
        try {
            if (caracteristicas != null && caracteristicas != "") {
                JavaScriptSerializer deserializer = new JavaScriptSerializer();

                Dictionary<string, object> D_caracteristicas = deserializer.Deserialize<Dictionary<string, object>>("{" + caracteristicas + "}");

                string caracHTML = "";
                foreach (var atributo in D_caracteristicas) {
                    caracHTML += @"
            <tr>" +
                        "<td style='white-space: nowrap; text-align:right; padding-right:4px;'><strong>" + atributo.Key + "</strong>:</td>" +
                        "<td style='white-space: nowrap; width:100%;'>" + atributo.Value + "</td>" +
                    "</tr>";

                }

                tbody_caracteristicas.InnerHtml = caracHTML;
            }

        }
        catch (Exception ex) {
          //  devNotificaciones.error("Procesar características JSON", ex);
        }
    }

    protected void cargarNavegacion(string categoriaID) {

        categorias obtener = new categorias();
        model_categorias categoriaActual = obtener.obtener_CatInfo(categoriaID);

        if (categoriaActual == null) return;

        HyperLink navActual = new HyperLink();
        navActual.CssClass = "breadcrumb";
        navActual.Text = categoriaActual.nombre;

        link_todas_categorias.NavigateUrl = GetRouteUrl("categoriasTodas");


        switch (categoriaActual.nivel) {
            case 1:
                navActual.NavigateUrl = GetRouteUrl("categorias", new System.Web.Routing.RouteValueDictionary {
                    { "identificador", categoriaActual.identificador },
                    { "nombre",  textTools.limpiarURL_NumeroParte(categoriaActual.nombre) } });
                break;

            case 2:
                model_categorias obtenerL1 = obtener.obtener_CatPadre(categoriaActual.identificador);

                navActual.NavigateUrl = GetRouteUrl("categoriasL2", new System.Web.Routing.RouteValueDictionary {
                        { "l1",  textTools.limpiarURL_NumeroParte(obtenerL1.nombre)},
                    { "identificador", categoriaActual.identificador },
                    { "nombre",  textTools.limpiarURL_NumeroParte(categoriaActual.nombre) } });




                HyperLink L1 = new HyperLink();
                L1.CssClass = "breadcrumb";
                L1.Text = obtenerL1.nombre.Replace("-", " ");
                L1.NavigateUrl = GetRouteUrl("categorias", new System.Web.Routing.RouteValueDictionary {
                    { "identificador", obtenerL1.identificador },
                    { "nombre",   textTools.limpiarURL_NumeroParte(obtenerL1.nombre)} });

                navegacion.Controls.Add(L1);
                break;

            case 3:

                List<model_categorias> PadresL3 = obtener.obtener_PadresL3(categoriaActual.identificador);

                navActual.NavigateUrl = GetRouteUrl("categoriasL3", new System.Web.Routing.RouteValueDictionary {

                    { "identificador", categoriaActual.identificador },
                    { "nombre",  textTools.limpiarURL_NumeroParte(categoriaActual.nombre) },
                    { "l1",     textTools.limpiarURL_NumeroParte(PadresL3[0].nombre) },
                    { "l2",    textTools.limpiarURL_NumeroParte(PadresL3[1].nombre) }
                });

                HyperLink L2_ = new HyperLink() {
                    CssClass = "breadcrumb",
                    Text = PadresL3[1].nombre.Replace("-", " "),
                    NavigateUrl = GetRouteUrl("categoriasL2", new System.Web.Routing.RouteValueDictionary {
                    { "identificador", PadresL3[1].identificador },
                    { "nombre", PadresL3[1].nombre },
                    { "l1",    textTools.limpiarURL_NumeroParte(PadresL3[0].nombre)}
                })
                };

                HyperLink L1_ = new HyperLink();
                L1_.CssClass = "breadcrumb";
                L1_.Text = PadresL3[0].nombre;
                L1_.NavigateUrl = GetRouteUrl("categorias", new System.Web.Routing.RouteValueDictionary {
                            { "identificador",   textTools.limpiarURL_NumeroParte(PadresL3[0].identificador)},
                            { "nombre",  textTools.limpiarURL_NumeroParte(PadresL3[0].nombre) }

                            });


                navegacion.Controls.Add(L1_);
                navegacion.Controls.Add(L2_);
                break;

        }
        navegacion.Controls.Add(navActual);



    }
}
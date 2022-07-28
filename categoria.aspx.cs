using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.HtmlControls;

public partial class categoria : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) { 
		string categoriaID = Page.RouteData.Values["identificador"].ToString();
		categorias obtener = new categorias();
		model_categorias categoriaActual = obtener.obtener_CatInfo(categoriaID);
 

            if(categoriaActual == null)
            {
                Response.Clear();
                Response.StatusCode = 404;
                Response.End();

            }
            string urlCanonical = Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.Url.AbsolutePath;
        /*GetRouteUrl("categorias", new System.Web.Routing.RouteValueDictionary {
                { "identificador", categoriaActual.identificador },
                { "nombre",  textTools.limpiarURL_NumeroParte(categoriaActual.nombre) } });
                */
        //  SEO TAGS
        Title = categoriaActual.nombre + ", "+ categoriaActual.descripcion;
        Page.MetaDescription = categoriaActual.nombre + ", " + categoriaActual.descripcion;

        titulo_categoria.InnerText = categoriaActual.nombre + ", " + categoriaActual.descripcion;
        descripcion_categoria.InnerText = categoriaActual.descripcion;

       

        // INICIO SEO TAGS
            #region SEO Tags
        HtmlGenericControl urlCanonicalTag = new HtmlGenericControl("link");
        urlCanonicalTag.Attributes.Add("rel", "canonical");
        urlCanonicalTag.Attributes.Add("href", urlCanonical);
        Page.Header.Controls.Add(urlCanonicalTag);


        HtmlMeta og_site_name = new HtmlMeta();
        HtmlMeta og_title = new HtmlMeta();
        HtmlMeta og_description = new HtmlMeta();
        HtmlMeta og_url = new HtmlMeta();
        HtmlMeta og_image = new HtmlMeta();
        HtmlMeta og_type = new HtmlMeta(); 

        og_site_name.Attributes.Add("property", "og:site_name");
        og_site_name.Content = "Incom Retail México";

        og_title.Attributes.Add("property", "og:title");
        og_title.Content = categoriaActual.descripcion.Length > 150 ? categoriaActual.descripcion.Substring(0, 149) : categoriaActual.descripcion;

        og_description.Attributes.Add("property", "og:description");
        og_description.Content = categoriaActual.descripcion.Length > 150 ? categoriaActual.descripcion.Substring(0, 149) : categoriaActual.descripcion;

        og_url.Attributes.Add("property", "og:url");
        og_url.Content = urlCanonical;

        og_image.Attributes.Add("property", "og:image");
        og_image.Content = "/img/webUI/categorias/" + categoriaActual.imagen;

        og_type.Attributes.Add("property", "og:type");
        og_type.Content = "article";

        Page.Header.Controls.Add(og_site_name);
        Page.Header.Controls.Add(og_title);
        Page.Header.Controls.Add(og_description);
        Page.Header.Controls.Add(og_url);
        Page.Header.Controls.Add(og_image);
        Page.Header.Controls.Add(og_type);
            // FIN SEO TAGS
#endregion


            // INICIO Twitter
            #region Twitter
            HtmlMeta tw_card = new HtmlMeta();
        HtmlMeta tw_title = new HtmlMeta();
        HtmlMeta tw_description = new HtmlMeta();
        HtmlMeta tw_site = new HtmlMeta();
        HtmlMeta tw_image = new HtmlMeta();


        tw_card.Attributes.Add("name", "twitter:card");
        tw_card.Content = "summary_large_image";

        tw_title.Attributes.Add("name", "twitter:title");
        tw_title.Content = categoriaActual.descripcion.Length > 150 ? categoriaActual.descripcion.Substring(0, 149) : categoriaActual.descripcion;

        tw_description.Attributes.Add("name", "twitter:description");
        tw_description.Content = categoriaActual.descripcion;

        tw_site.Attributes.Add("name", "twitter:site");
        tw_site.Content = "@incom_mx";

        tw_image.Attributes.Add("name", "twitter:image");
        tw_image.Content = Request.Url.GetLeftPart(UriPartial.Authority)+"/img/webUI/categorias/" + categoriaActual.imagen;



        Page.Header.Controls.Add(tw_card);
        Page.Header.Controls.Add(tw_title);
        Page.Header.Controls.Add(tw_description);
        Page.Header.Controls.Add(tw_site);
        Page.Header.Controls.Add(tw_image);

            #endregion
            // FIN Twitter


          
            HyperLink navActual = new HyperLink();
        navActual.CssClass = "breadcrumb";
        navActual.Text = categoriaActual.nombre;

        link_todas_categorias.NavigateUrl = GetRouteUrl("categoriasTodas");


        switch (categoriaActual.nivel)
        {
            case 1:
                navActual.NavigateUrl = urlCanonical;
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

            HyperLink L2_ = new HyperLink();
            L2_.CssClass = "breadcrumb";
            L2_.Text = PadresL3[1].nombre.Replace("-", " ");
            L2_.NavigateUrl = GetRouteUrl("categoriasL2", new System.Web.Routing.RouteValueDictionary {
                    { "identificador",  textTools.limpiarURL_NumeroParte(PadresL3[1].identificador) },
                    { "nombre",  textTools.limpiarURL_NumeroParte(PadresL3[1].nombre) },
                    { "l1",    textTools.limpiarURL_NumeroParte(PadresL3[0].nombre)}
            });



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

        
        guardarHitCategoria();
            }
        }



    //Manda los valores para que se pueda ejecutar la función "guardarCategoriaHit" 
    protected async void guardarHitCategoria()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated && usuarios.userLogin().tipo_de_usuario == "cliente")
        {
            string Identificador = Page.RouteData.Values["identificador"].ToString();
            string nombreCategoria = Page.RouteData.Values["nombre"].ToString();

            model_BI_HitCategorias categoria = new model_BI_HitCategorias();

            categoria.nombreCategoria = nombreCategoria;
            categoria.identificador = Identificador;
            categoria.idUsuario = usuarios.userLogin().id;
            categoria.fecha = utilidad_fechas.obtenerCentral();
            categoria.direccion_ip = red.GetDireccionIp(request: HttpContext.Current.Request);
            historialCategorias.guardarCategoriaHit(categoria);

        }

    }
}
 
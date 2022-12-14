using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI.HtmlControls;

public partial class inicio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Title = "INCOM ® productos de telecomunicaciones y fibra óptica en México";
            Page.MetaDescription = "Somos distribuidores con más de 18 años en Fibra óptica, cableado estructurado, CATV, identificación, telefonía, redes externas aéreas y subterráneas.";

            //ProductosVisitados.cantidadCargar = 15;
            //ProductosVisitados.slidesToShow = 5;
            //ProductosVisitados.verticalMode = false;
            //ProductosVisitados.obtenerProductos();


            // INICIO SEO TAGS - 
            HtmlGenericControl urlCanonical = new HtmlGenericControl("link");
            urlCanonical.Attributes.Add("rel", "canonical");
            urlCanonical.Attributes.Add("href", "https://www.incom.mx");
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
            og_title.Content = this.Page.Title;

            og_description.Attributes.Add("property", "og:description");
            og_description.Content = this.Page.MetaDescription;

            og_url.Attributes.Add("property", "og:url");
            og_url.Content = Request.Url.GetLeftPart(UriPartial.Authority);

            og_image.Attributes.Add("property", "og:image");
            og_image.Content = Request.Url.GetLeftPart(UriPartial.Authority) + "/img/webUI/incom_retail_logo_header_white.png"; ;

            og_type.Attributes.Add("property", "og:type");
            og_type.Content = "article";

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
            tw_title.Content = this.Page.Title;

            tw_description.Attributes.Add("name", "twitter:description");
            tw_description.Content = this.Page.MetaDescription;

            tw_site.Attributes.Add("name", "twitter:site");
            tw_site.Content = "@incom_mx";

            tw_image.Attributes.Add("name", "twitter:image");
            tw_image.Content = Request.Url.GetLeftPart(UriPartial.Authority) + "/img/webUI/incom_retail_logo_header_white.png";



            Page.Header.Controls.Add(tw_card);
            Page.Header.Controls.Add(tw_title);
            Page.Header.Controls.Add(tw_description);
            Page.Header.Controls.Add(tw_site);
            Page.Header.Controls.Add(tw_image);


            // FIN Twitter
            // FIN SEO TAGS - 
            cargarAnuncio();
            cargarUSP();
        }
    }

    protected void cargarAnuncio()
    {
        DataTable anuncios = configuracion_sliders.obtenerImagenesSlider("sliderAnuncio");

        if (anuncios.Rows.Count > 0)
        {
            foreach (DataRow anuncio in anuncios.Rows)
            {
                string activo = anuncio["activo"].ToString();
                if (activo == "1")
                {
                    string titulo = anuncio["titulo"].ToString();
                    string descripcion = anuncio["descripcion"].ToString();
                    string linkAnuncio = anuncio["link"].ToString();
                    string nombreArchivo = anuncio["nombreArchivo"].ToString();
                    HtmlGenericControl divControl = new HtmlGenericControl("div");
                    HyperLink link = new HyperLink();
                    Image img = new Image();
                    img.ImageUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/img/webUI/sliderHome/" + nombreArchivo;
                    img.AlternateText = titulo;
                    link.NavigateUrl = linkAnuncio;
                    link.Controls.Add(img);
                    divControl.Controls.Add(link);
                    anunciosSlider.Controls.Add(divControl);
                }
            }

        }
    }

    protected void cargarUSP()
    {
        DataTable usps = configuracion_sliders.obtenerImagenesSlider("sliderUSP");

        if (usps.Rows.Count > 0)
        {
            foreach (DataRow usp in usps.Rows)
            {
                string activo = usp["activo"].ToString();
                if (activo == "1")
                {
                    string titulo = usp["titulo"].ToString();
                    string descripcion = usp["descripcion"].ToString();
                    string nombreArchivo = usp["nombreArchivo"].ToString();
                    string posicion = usp["posicion"].ToString();
                    string linkUSP = usp["link"].ToString();
                    string opciones = usp["opciones"].ToString();
                    HtmlGenericControl divControl = new HtmlGenericControl("div");
                    HyperLink link = new HyperLink();
                    Image img = new Image();
                    img.ImageUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/img/webUI/sliderHome/" + nombreArchivo;
                    img.AlternateText = titulo;
                    img.CssClass = "is-w-full";
                    link.NavigateUrl = linkUSP;
                    link.ToolTip = descripcion;
                    link.Controls.Add(img);
                    divControl.Controls.Add(link);
                    switch (posicion)
                    {
                        case "1":
                            sliderUSP1.Controls.Add(divControl);
                            break;
                        case "2":
                            sliderUSP2.Controls.Add(divControl);
                            break;
                        case "3":
                            sliderUSP3.Controls.Add(divControl);
                            break;
                    }
                }
            }
        }
    }
}
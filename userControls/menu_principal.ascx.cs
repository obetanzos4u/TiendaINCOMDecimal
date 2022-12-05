using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

public partial class menuPrincipal : System.Web.UI.UserControl
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarMenu();
        }
    }
    protected void cargarMenu()
    {
        //Control menuMarcas = new Control();
        //menuMarcas = marcas.construirMenu();

        Control menuCat = new Control();
        menusCategorias obtener = new menusCategorias();

        menuCat = obtener.obtenerMenuCategorias("");

        //Control menuCat2 = new Control();
        //menuCat2 = obtener.obtenerMenuCategorias2("");

        //HtmlGenericControl home = new HtmlGenericControl("li");
        //home.InnerHtml = "<a title='página principal' class='hide-on-med-and-down'  href='" + request.url.getleftpart(uripartial.authority) + "'>inicio</a>";

        HtmlGenericControl rebajas = new HtmlGenericControl("li");
        rebajas.ID = "content-menu-incom-outlet";
        rebajas.ClientIDMode = ClientIDMode.Static;
        rebajas.InnerHtml = "<a title='Rebajas'  href='/productos/Rebajas-REBAJAS1'>Rebajas</a>";

        HtmlGenericControl biblioteca = new HtmlGenericControl("li");
        biblioteca.ID = "content-menu-incom-biblioteca";
        biblioteca.ClientIDMode = ClientIDMode.Static;
        biblioteca.InnerHtml = "<a title='Biblioteca'>Biblioteca <img alt='Flecha de desplazamiento para ver menu' id='menu_icon_biblioteca' src='https://www.incom.mx/img/webUI/newdesign/Flecha.svg'></a><ul><li><a href='https://blog.incom.mx/'>Blog</a></li><li><a href='https://www.incom.mx/ense%C3%B1anza/infograf%C3%ADas'>Infografías</a></li><li><a href='https://www.incom.mx/glosario/a'>Enciclopédico</a></li></ul>";

        HtmlGenericControl catalogos = new HtmlGenericControl("li");
        catalogos.ID = "content-menu-incom-catalogos";
        catalogos.ClientIDMode = ClientIDMode.Static;
        catalogos.InnerHtml = "<a title='Catalogos'  href='#'>Catálogos<img alt ='Flecha de desplazamiento para ver menu' id='menu_icon_catalogos' src ='https://www.incom.mx/img/webUI/newdesign/Flecha.svg'></a>" +
                "<ul>" +
                    "<li><a href='/documents/pdf/CATALOGO_INCOM_CANALIZACION.pdf' target='_blank' title='Canalización'>Canalización</a></li>" +
                    "<li><a href='/documents/pdf/CATALOGO_INCOM_ELECTRICO.pdf' target='_blank' title='Soluciones eléctricas'>Eléctrico</a></li>" +
                    "<li><a href='/documents/pdf/CATALOGO_INCOM_ESTRUCTURADO.pdf' target='_blank' title='Cableado estructurado y data center'>Estructurado</a></li>" +
                    "<li><a href='/documents/pdf/CATALOGO_INCOM_FERRETERIA.pdf' target='_blank' title='Ferretería'>Ferretería</a></li>" +
                    "<li><a href='/documents/pdf/CATALOGO_INCOM_FIBRA_OPTICA.pdf' target='_blank' title='Fibra Óptica'>Fibra óptica</a></li>" +
                    "<li><a href='/documents/pdf/ICOPTIKS_CATALOGO_SOLUCIONES_PARA_FIBRA_OPTICA.pdf' target='_blank' title='ICOPTIKS soluciones para fibra óptica'>Icoptiks</a></li>" +
                    "<li><a href='/documents/pdf/CATALOGO_INCOM_IDENTIFICACION.pdf' target='_blank' title='Identificación'>Identificación de redes</a></li>" +
                    "<li><a href='/documents/pdf/CATALOGO_INCOM_AEREO.pdf' target='_blank' title='Aéreo'>Instalación aérea</a></li>" +
                    "<li><a href='/documents/pdf/CATALOGO_INCOM_SUBTERRANEO.pdf' target='_blank' title='Subterráneo'>Instalación subterránea</a></li>" +
                    "<li><a href='/documents/promos/CATALOGO_INCOM_LIQUIDACION.pdf' target='_blank' title='Liquidación'>Liquidación</a></li>" +
                    "<li><a href='/documents/pdf/CATALOGO_INCOM_BLOQUEO.pdf' target='_blank' title='Bloqueo'>Seguridad industrial</a></li>" +
                    "<li><a href='/documents/pdf/INCOM_CATALOGO_SOLUCION_FIBRA_SOPLADA.pdf' target='_blank' title='Solución Fibra Soplada'>Solución fibra soplada</a></li>" +
                    "<li><a href='/documents/pdf/CATALOGO_MAQUINAS_PARA_SOPLADO_FREMCO.pdf' target='_blank' title='Fremco'>Sopladoras Fremco</a></li>" +
                    "<li><a href='/documents/pdf/CATALOGO_INCOM_TELEFONIA_Y_CATV.pdf' target='_blank' title='Telefonía & CATV'>Telefonía y CATV</a></li>" +
                "</ul>";

        //  HtmlGenericControl blog = new HtmlGenericControl("li");
        //  blog.InnerHtml = "<a title='Blog Incom' target='_blank'  href='https://blog.incom.mx'>Blog</a>";

        //contenedorMenu.Controls.Add(home);

        //contenedorMenu.Controls.Add(blog);

        contenedorMenu.Controls.Add(menuCat);
        //contenedorMenu.Controls.Add(menuMarcas);

        contenedorMenu.Controls.Add(rebajas);

        //contenedorMenu.Controls.Add(menuCat2);
        contenedorMenu.Controls.Add(biblioteca);

        contenedorMenu.Controls.Add(catalogos);

        contenedorMenu.DataBind();
    }
}
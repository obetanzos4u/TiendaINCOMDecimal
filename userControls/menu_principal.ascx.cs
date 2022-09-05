using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class menuPrincipal : System.Web.UI.UserControl
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            cargarMenu();
        }
    }
    protected void cargarMenu( )
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

        HtmlGenericControl ofertas = new HtmlGenericControl("li");
        ofertas.ID = "content-menu-incom-outlet";
        ofertas.ClientIDMode = ClientIDMode.Static;
        ofertas.InnerHtml = "<a title='Ofertas'  href='https://www.incom.mx/productos/Outlet-OUTLET1'>Ofertas</a>";

        HtmlGenericControl biblioteca = new HtmlGenericControl("li");
        biblioteca.ID = "content-menu-incom-biblioteca";
        biblioteca.ClientIDMode = ClientIDMode.Static;
        biblioteca.InnerHtml = "<a title='Biblioteca'>Biblioteca <img alt='Flecha de desplazamiento para ver menu' id='menu_icon_biblioteca' src='../img/webUI/newdesign/Flecha.svg'></a><ul><li><a href='https://blog.incom.mx/'>Blog</a></li><li><a href='https://www.incom.mx/ense%C3%B1anza/infograf%C3%ADas'>Infografías</a></li><li><a href='https://www.incom.mx/glosario/a'>Enciclopédico</a></li></ul>";

        HtmlGenericControl catalogos = new HtmlGenericControl("li");
        catalogos.ID = "content-menu-incom-outlet";
        catalogos.ClientIDMode = ClientIDMode.Static;
        catalogos.InnerHtml = "<a title='Catalogos'  href='/'>Catálogos <img alt='Flecha de desplazamiento para ver menu' id='menu_icon_catalogos' src='../img/webUI/newdesign/Flecha.svg'></a>";
        //  HtmlGenericControl blog = new HtmlGenericControl("li");
        //  blog.InnerHtml = "<a title='Blog Incom' target='_blank'  href='https://blog.incom.mx'>Blog</a>";

        //contenedorMenu.Controls.Add(home);

        //contenedorMenu.Controls.Add(blog);

        contenedorMenu.Controls.Add(menuCat);
        //contenedorMenu.Controls.Add(menuMarcas);

        contenedorMenu.Controls.Add(ofertas);

        //contenedorMenu.Controls.Add(menuCat2);
        contenedorMenu.Controls.Add(biblioteca);

        contenedorMenu.Controls.Add(catalogos);

        contenedorMenu.DataBind();
    }
}
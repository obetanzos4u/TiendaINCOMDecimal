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

        Control menuCat2 = new Control();
        menuCat2 = obtener.obtenerMenuCategorias2("");


        //HtmlGenericControl home = new HtmlGenericControl("li");
        //home.InnerHtml = "<a title='página principal' class='hide-on-med-and-down'  href='" + request.url.getleftpart(uripartial.authority) + "'>inicio</a>";

        HtmlGenericControl ofertas = new HtmlGenericControl("li");
        ofertas.ID = "content-menu-incom-outlet";
        ofertas.ClientIDMode = ClientIDMode.Static;
        ofertas.InnerHtml = "<a title='Ofertas'  href='https://www.incom.mx/productos/Outlet-OUTLET1'>Ofertas</a>";

        //  HtmlGenericControl blog = new HtmlGenericControl("li");
        //  blog.InnerHtml = "<a title='Blog Incom' target='_blank'  href='https://blog.incom.mx'>Blog</a>";

        //contenedorMenu.Controls.Add(home);
        //contenedorMenu.Controls.Add(blog);

        contenedorMenu.Controls.Add(menuCat);
        //contenedorMenu.Controls.Add(menuMarcas);

        contenedorMenu.Controls.Add(ofertas);

        contenedorMenu.Controls.Add(menuCat2);

        contenedorMenu.DataBind();
    }

    
 



  
}
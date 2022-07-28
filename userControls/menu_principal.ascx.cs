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
        Control menuMarcas = new Control();

        menuMarcas = marcas.construirMenu();

        Control menuCat = new Control();
        menusCategorias obtener = new menusCategorias();

        menuCat = obtener.obtenerMenuCategorias("");
     

        HtmlGenericControl home = new HtmlGenericControl("li");
        home.InnerHtml = "<a title='Página principal' class='hide-on-med-and-down'  href='" + Request.Url.GetLeftPart(UriPartial.Authority) + "'>Inicio</a>";

        HtmlGenericControl outlet = new HtmlGenericControl("li");
        outlet.ID = "content-menu-incom-outlet";
        outlet.ClientIDMode = ClientIDMode.Static;
        outlet.InnerHtml = "<a title='Outlet'  href='https://www.incom.mx/productos/Outlet-OUTLET1'>Outlet</a>";

      //  HtmlGenericControl blog = new HtmlGenericControl("li");
      //  blog.InnerHtml = "<a title='Blog Incom' target='_blank'  href='https://blog.incom.mx'>Blog</a>";

        contenedorMenu.Controls.Add(home);
      //  contenedorMenu.Controls.Add(blog);

        contenedorMenu.Controls.Add(menuCat);
        contenedorMenu.Controls.Add(menuMarcas);

        contenedorMenu.Controls.Add(outlet);
        contenedorMenu.DataBind();
    }

    
 



  
}
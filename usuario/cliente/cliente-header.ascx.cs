using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usuario_cliente_cliente_header : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //GenerarMenuMarcas();
            //GenerarMenuCategorias();
        }
    }

    //protected void GenerarMenuMarcas()
    //{

    //    var Marcas = MarcasEF.obtenerTodas().OrderBy(c => c);
    //    foreach(string marca in Marcas)
    //    {
    //        Submenu_Marcas.InnerHtml += "<a class='dropdown-item' href='/productos/buscar?busqueda=" + marca + "'>" + marca + "<a>";
    //    }
    //}

    //protected void GenerarMenuCategorias()
    //{

    //    var Categorias = CategoriasEF.obtenerNivel_1().Where(c => c.rol_categoria =="general").ToList();
    //    foreach (var categoria in Categorias)
    //    {
    //        string Url = GetRouteUrl("categorias", new System.Web.Routing.RouteValueDictionary { { "identificador", categoria.identificador }, { "nombre", menusCategorias.limpiarURL(categoria.nombre) } });
    //        Submenu_Productos.InnerHtml += $"<a class='dropdown-item' href='{Url}'>" + categoria.nombre + "<a>";
    //    }
    //}

    //protected void btn_loggout_Click(object sender, EventArgs e)
    //{
    //    Session.Clear();
    //    Session.Abandon();
    //    Session.RemoveAll();
    //    FormsAuthentication.SignOut();
    //    FormsAuthentication.RedirectToLoginPage();
    //}
}
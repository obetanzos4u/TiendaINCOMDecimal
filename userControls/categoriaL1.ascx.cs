using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class userControls_categoriaL1 : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            string categoriaID = Page.RouteData.Values["identificador"].ToString();

            categorias obtener = new categorias();
            DataTable categorias = obtener.obtener_CatHijas(categoriaID);
            lv_categoriasHijas.DataSource = categorias;
            lv_categoriasHijas.DataBind();
        }
           

	}

    protected void lv_categoriasHijas_OnItemDataBound(object sender, ListViewItemEventArgs e)
    {
        usuarios datosUsuario = (usuarios)System.Web.HttpContext.Current.Session["datosUsuario"];

        HyperLink link = (HyperLink)e.Item.FindControl("link_cat");
        HtmlGenericControl item_categoria = (HtmlGenericControl)e.Item.FindControl("item_categoria"); 

        System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;


        string[] rol_categorias = rowView["rol_categoria"].ToString().Split(',');
        string[] usuario_rol_categorias = datosUsuario.rol_categorias;
        link.ToolTip = rowView["descripcion"].ToString();

        bool itemVisible = false;

        // Comenzamos con la validación de roles[] de categoria del usuario logeado contra la visibilidad de la configuración de la categoria
        // Para eso necesitamos recorrer cada uno de los roles de cat. que el usuario tenga asignado contra los roles permitidos de la categoria
        foreach (string rol_user in usuario_rol_categorias)
        {
            foreach (string rol_Cat in rol_categorias)
            {
                // Si el rol de la categoria tiene como general, por default es visible para todo el público
                if (rol_Cat.Replace(" ", "") == "general" || rol_user == rol_Cat.Replace(" ", ""))
                {
                   
                    itemVisible = true;
                } else { item_categoria.Visible = false;}
            }

        }

        // Si encontro incidencia seguimos todo los procesos correspondientes
        if (itemVisible)
        {


            string nivel = rowView["nivel"].ToString();
            

            switch (nivel)
            {
                case "2":
                string L1_nombre = Page.RouteData.Values["nombre"].ToString();
                link.NavigateUrl = GetRouteUrl("categoriasL2", new System.Web.Routing.RouteValueDictionary {
                        { "identificador", rowView["identificador"].ToString() },
                        { "l1", L1_nombre },
                        { "nombre",  textTools.limpiarURL_NumeroParte(rowView["nombre"].ToString()) }
                    });
                break;
                case "3":
                    string L1_nombre_ = Page.RouteData.Values["l1"].ToString();
                    string L2_nombre = Page.RouteData.Values["nombre"].ToString();

                    link.NavigateUrl = GetRouteUrl("categoriasL3", new System.Web.Routing.RouteValueDictionary {
                        { "identificador", rowView["identificador"].ToString() },
                        { "l1", L1_nombre_ },
                         { "l2", L2_nombre },
                        { "nombre",  textTools.limpiarURL_NumeroParte(rowView["nombre"].ToString()) }
                     });
                break;
                default:
              
                break;
            }

            // Inicio del proceso de validador de existencia de archivo de la imagen
            Image img_categoria = (Image)e.Item.FindControl("img_categoria");
            string imagen = rowView["imagen"].ToString();
            string fileImagenPath = archivosManejador.appPath + @"\img\webUI\categorias\" + imagen;
            bool existeImagen = archivosManejador.validarExistencia(fileImagenPath);

            img_categoria.AlternateText = rowView["nombre"].ToString();
            img_categoria.ToolTip = rowView["descripcion"].ToString();
            img_categoria.Attributes.Add("loading", "lazy");

            if (existeImagen) {
                img_categoria.ImageUrl = "/img/webUI/categorias/" + imagen;
                } else {
                img_categoria.ImageUrl = "/img/webUI/categorias/blanco.jpg";
                }
            // Fin del proceso de validador de existencia de archivo de la imagen

            }
        }
}
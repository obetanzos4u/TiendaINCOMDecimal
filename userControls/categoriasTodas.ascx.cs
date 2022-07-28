using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class userControls_categoriaTodas : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            categorias obtener = new categorias();
            DataTable categorias = obtener.obtenerCategorias(1);
            lv_categoriasTodas.DataSource = categorias;
            lv_categoriasTodas.DataBind();
        }

            

	}

    protected void lv_categoriasTodas_OnItemDataBound(object sender, ListViewItemEventArgs e)
    {

        HtmlGenericControl item_categoria = (HtmlGenericControl)e.Item.FindControl("item_categoria"); 

        System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;

        usuarios datosUsuario = (usuarios)System.Web.HttpContext.Current.Session["datosUsuario"];
        string[] rol_categorias = rowView["rol_categoria"].ToString().Split(',');
        string[] usuario_rol_categorias = datosUsuario.rol_categorias;
        bool itemVisible = false;

        // Comenzamos con la validación de roles[] de categoria del usuario logeado contra la visibilidad de la configuración de la categoria
        // Para eso necesitamos recorrer cada uno de los roles de cat. que el usuario tenga asignado contra los roles permitidos de la categoria
        foreach (string rol_user in usuario_rol_categorias) {
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
        if (itemVisible) {

            HyperLink link = (HyperLink)e.Item.FindControl("link_cat");
            link.ToolTip = rowView["descripcion"].ToString();

            string imge_fileName = rowView["imagen"].ToString();

           
                link.NavigateUrl = GetRouteUrl("categorias", new System.Web.Routing.RouteValueDictionary {
                        { "identificador", rowView["identificador"].ToString() },
                        { "nombre",  textTools.limpiarURL_NumeroParte(rowView["nombre"].ToString()) }
                    });

            // Inicio del proceso de validador de existencia de archivo de la imagen
            Image img_categoria = (Image)e.Item.FindControl("img_categoria");
            string imagen = rowView["imagen"].ToString();
            string fileImagenPath = archivosManejador.appPath + @"\img\webUI\categorias\" + imagen;
            bool existeImagen =  archivosManejador.validarExistencia(fileImagenPath);

            img_categoria.AlternateText = rowView["nombre"].ToString();
            img_categoria.ToolTip= rowView["descripcion"].ToString();
            img_categoria.Attributes.Add("loading", "lazy");
            if (existeImagen) {
                img_categoria.ImageUrl = "/img/webUI/categorias/"+ imagen;
                    } else {
                img_categoria.ImageUrl = "/img/webUI/categorias/blanco.jpg";
            }
           // Findel proceso de validador de existencia de archivo de la imagen
            }
        }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Descripción breve de menu
/// </summary>
public class menusCategorias : System.Web.UI.Page
{
    private SqlConnection con { get; set; }
    private SqlCommand cmd { get; set; }
    private SqlDataAdapter da { get; set; }
    private DataSet ds { get; set; }
    private DataTable dt { get; set; }

    protected void dbConexion()
    {
        con = new SqlConnection(conexiones.conexionTienda());
        cmd = new SqlCommand();
        cmd.Connection = con;
    }

    public Control obtenerMenuCategorias(string usuario)
    {
        Panel test = new Panel();

        //Contenedor principal L1
        HtmlGenericControl li = new HtmlGenericControl("li");
        li.Attributes.Add("id", "menuProductos");
        li.Attributes.Add("class", "menu-categorias");

        //Contenedor L2
        HtmlGenericControl L1_Title = new HtmlGenericControl("a");
        L1_Title.Attributes.Add("href", "#");
        L1_Title.Attributes.Add("title", "Productos");
        //L1_Title.InnerHtml = "Productos<i id='menu_ico_productos' class='material-icons right'>arrow_drop_down</i>";
        L1_Title.InnerHtml = "Productos <img id='menu_ico_productos' class='material-icons right' src='https://www.incom.mx/img/webUI/newdesign/Flecha.svg'/>";

        HtmlGenericControl L1_Cont = new HtmlGenericControl("div");
        L1_Cont.Attributes.Add("id", "menuProductosContenido");
        L1_Cont.Attributes.Add("class", "menu-items");
        L1_Cont.Attributes.Add("style", "display: none;");
        L1_Cont = nivel1(L1_Cont);

        li.Controls.Add(L1_Title);
        li.Controls.Add(L1_Cont);

        return li;

    }

    //public Control obtenerMenuCategorias2(string usuario)
    //{

    //    Panel test = new Panel();

    //    //Contenedor principal L2
    //    HtmlGenericControl li = new HtmlGenericControl("li");
    //    li.Attributes.Add("class", "menu-categorias");

    //    //Contenedor L3
    //    HtmlGenericControl L2_Title = new HtmlGenericControl("a");
    //    L2_Title.Attributes.Add("href", "#");
    //    L2_Title.Attributes.Add("title", "Biblioteca");
    //    //L2_Title.InnerHtml = "Biblioteca<i id='menu_ico_productos' class='material-icons right'>arrow_drop_down</i>";
    //    L2_Title.InnerHtml = "Biblioteca <img id='menu_ico_productos 'src='../img/webUI/newdesign/Flecha.svg' style='width: 1rem  '>";

    //    HtmlGenericControl L3_Cont = new HtmlGenericControl("div");
    //    L3_Cont.Attributes.Add("class", "  menu-items  ");
    //    L3_Cont.Attributes.Add("style", "display:none;");
    //    L3_Cont = nivel1(L3_Cont);

    //    li.Controls.Add(L2_Title);
    //    li.Controls.Add(L3_Cont);

    //    return li;

    //}

    private HtmlGenericControl nivel1(HtmlGenericControl li)
    {
        usuarios datosUsuario = usuarios.modoAsesor();
        string[] usuario_rol_categorias = datosUsuario.rol_categorias;


        DataTable nivel1 = obtenerNivel1();

        foreach (DataRow r in nivel1.Rows)
        {

            // INI de asignación de valores básicos
            string identificador = r["identificador"].ToString();
            string nombre = r["nombre"].ToString();
            string[] rol_categorias = r["rol_categoria"].ToString().Split(',');
            // FIN de asignación de valores básicos



            // Si encontro incidencia, proseguimos con el Nivel 1 y también procederemos a permitir el nivel 2
            if (privacidad.validarCategoria(rol_categorias, usuario_rol_categorias))
            {
                HtmlGenericControl itemL1 = new HtmlGenericControl("div");
                itemL1.Attributes.Add("class", "menu-l1"); //col l2 m4 s12
                itemL1.Attributes.Add("id", identificador);

                //HtmlGenericControl itemL2 = new HtmlGenericControl("div");
                //itemL2.Attributes.Add("class", "menu-l1 col l2 m4 s12");

                HyperLink itemL1_Title = new HyperLink();
                itemL1_Title.Text = nombre;
                itemL1_Title.NavigateUrl = GetRouteUrl("categorias", new System.Web.Routing.RouteValueDictionary { { "identificador", identificador }, { "nombre", limpiarURL(nombre) } });
                itemL1_Title.ToolTip = nombre;

                itemL1.Controls.Add(itemL1_Title);

                //HyperLink itemL2_Title = new HyperLink();
                //itemL2_Title.Text = nombre;
                //itemL2_Title.NavigateUrl = GetRouteUrl("categorias", new System.Web.Routing.RouteValueDictionary { { "identificador", identificador }, { "nombre", limpiarURL(nombre) } });
                //itemL2_Title.ToolTip = nombre;

                //itemL2.Controls.Add(itemL2_Title);


                 DataTable nivel2 = ObtenerSubniveles(identificador, 2);

                // Validamos si este nivel tiene nivel 2
                if (nivel2.Rows.Count >= 1)
                {

                    HtmlGenericControl L2_Cont = new HtmlGenericControl("ul");
                    L2_Cont.Attributes.Add("id", "SUB-" + identificador);
                    L2_Cont.Attributes.Add("style", "display: none;");

                    foreach (DataRow r2 in nivel2.Rows)
                    {
                        int idL2 = int.Parse(r2["id"].ToString());
                        string identificadorL2 = r2["identificador"].ToString();
                        string nombreL2 = r2["nombre"].ToString();
                        string[] rol_categoriasL2 = r2["rol_categoria"].ToString().Split(',');

                        // Validamos si dicha categoría L2 esta admitida
                        if (privacidad.validarCategoria(rol_categoriasL2, usuario_rol_categorias))
                        {
                            HtmlGenericControl li_L2 = new HtmlGenericControl("li");
                            HyperLink a_TitleL2 = new HyperLink();


                            a_TitleL2.NavigateUrl = GetRouteUrl("categoriasL2", new System.Web.Routing.RouteValueDictionary { { "identificador", identificadorL2 }, { "l1", limpiarURL(nombre) }, { "nombre", limpiarURL(nombreL2) } });
                            a_TitleL2.Text = nombreL2;
                            a_TitleL2.ToolTip = nombreL2;
                            li_L2.Controls.Add(a_TitleL2);

                            L2_Cont.Controls.Add(li_L2);
                        }
                    }

                    itemL1.Controls.Add(L2_Cont);

                }

                li.Controls.Add(itemL1);
            }
        }

        return li;
    }

    public DataTable obtenerNivel1()
    {
        dbConexion();
        using (con)
        {

            StringBuilder sel = new StringBuilder();
            sel.Append("SELECT ");
            sel.Append(" * ");
            sel.Append(" FROM categorias WHERE nivel = 1 ORDER BY orden asc");

            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;


            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            return ds.Tables[0];
        }


    }
    public DataTable ObtenerSubniveles(string identificador, int nivel)
    {
        dbConexion();
        using (con)
        {

            StringBuilder sel = new StringBuilder();
            sel.Append("SELECT ");
            sel.Append(" * ");
            sel.Append(" FROM categorias WHERE nivel = @nivel AND asociacion = @asociacion ORDER BY orden asc");

            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@nivel", SqlDbType.Int);
            cmd.Parameters["@nivel"].Value = nivel;

            cmd.Parameters.Add("@asociacion", SqlDbType.NVarChar);
            cmd.Parameters["@asociacion"].Value = identificador;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            return ds.Tables[0];
        }
    }

    public static string limpiarURL(string url)
    {
        string urlLimpia = url.Replace(" ", "-").Replace(".", "").Replace(",", "").Replace("/", "-");


        return urlLimpia;
    }
}
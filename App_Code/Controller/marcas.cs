using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

/// <summary>
/// Descripción breve de categorias
/// </summary>
public class marcas
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

    /// <summary>
    /// Obtiene todas las categorias
    /// </summary>
    static public List<menuMarca> ObtenerMarcas()
    {

 
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        SqlCommand cmd = new SqlCommand();
        DataTable dtMenu = new DataTable();
     cmd.Connection = con; 
        using (con)
        {
            string query = @"  SELECT * FROM (
SELECT   marca as nombreMarca
      ,categoria = (SELECT nombre FROM categorias WHERE identificador = VALUE and nivel = 2)  
	  ,nivel = (SELECT nivel FROM categorias WHERE identificador = VALUE )  
	  ,identificadorCategoria = VALUE
FROM productos_Datos
    CROSS APPLY STRING_SPLIT(categoria_identificador, ',') AS BK
	--where marca = '3M' 
	GROUP BY marca , value  
 ) AS t WHERE nivel = 2 order by nombreMarca


  SELECT * FROM (
 SELECT   marca as nombreMarca
      ,categoria = (SELECT nombre FROM categorias WHERE identificador = VALUE and nivel = 3)  
	  ,nivel = (SELECT nivel FROM categorias WHERE identificador = VALUE )  
	  ,identificadorCategoria = VALUE
FROM productos_Datos
    CROSS APPLY STRING_SPLIT(categoria_identificador, ',') AS BK
 
	GROUP BY marca , VALUE  
 ) AS t WHERE nivel = 3 ORDER by nombreMarca";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
              dtMenu =  ds.Tables[0];


            SqlConnection.ClearAllPools();
            dtMenu.Merge(ds.Tables[1]);

        }


        List<menuMarca> menuCompleto = new List<menuMarca>();
        menuCompleto = (from DataRow dr in dtMenu.Rows
                        select new menuMarca()
                        {

                            nombreMarca = dr["nombreMarca"].ToString(),
                            categoria = dr["categoria"].ToString(),
                            nivel = Convert.ToInt32(dr["nivel"]),
                            identificadorCategoria = dr["identificadorCategoria"].ToString()
                        }).ToList();



        SqlConnection.ClearAllPools();
        return menuCompleto;
    }
 

    static  public System.Web.UI.Control construirMenu()
    {
        List<menuMarca> catMarcas =  ObtenerMarcas();


        List<menuMarca> marcas = catMarcas.GroupBy(d => new { d.nombreMarca })
                             .Select(d => d.First())
                             .OrderBy(m => m.nombreMarca)
                             .ToList();

        HtmlGenericControl txt_buscador = new HtmlGenericControl("input");
        txt_buscador.ID = "txt_buscadorMenuMarcas";
        txt_buscador.ClientIDMode = System.Web.UI.ClientIDMode.Static;
        txt_buscador.Attributes.Add("type", "text");
        txt_buscador.Attributes.Add("placeholder", "Busca tu marca");
        txt_buscador.Attributes.Add("class", "browser-default");
 

        HtmlGenericControl liMenuPrincipal = new HtmlGenericControl("li");
        liMenuPrincipal.ID = "content-menu-incom-marcas";
        liMenuPrincipal.ClientIDMode = System.Web.UI.ClientIDMode.Static;
        HtmlGenericControl aMenuPrincipal = new HtmlGenericControl("a");

        //aMenuPrincipal.InnerHtml = "Marcas <i id='menu_ico_marcas' class='material-icons right'>arrow_drop_down</i>";
        aMenuPrincipal.InnerHtml = "Marcas <img src='../img/webUI/newdesign/Flecha.svg'/>";
        aMenuPrincipal.Attributes.Add("href", "#");
        aMenuPrincipal.ID = "menu-incom-marcas-btn";
        aMenuPrincipal.ClientIDMode = System.Web.UI.ClientIDMode.Static;
        liMenuPrincipal.Controls.Add(aMenuPrincipal);

            // Contenedor del sub menu
            HtmlGenericControl menuContenedor = new HtmlGenericControl("div");
        menuContenedor.Attributes.Add("class", "row menu-incom-contenedor");
        menuContenedor.ID = "contenedorSubMenuMarcas";
        menuContenedor.ClientIDMode = System.Web.UI.ClientIDMode.Static;

        HtmlGenericControl marcasContainer = new HtmlGenericControl("div");
        marcasContainer.Attributes.Add("class", "menu-incom-marcas-container");


        HtmlGenericControl categoriasContainer = new HtmlGenericControl("div");
        categoriasContainer.Attributes.Add("class", "menu-incom-marcas-categorias-container");


        HtmlGenericControl marcasList = new HtmlGenericControl("ul");
        marcasList.Attributes.Add("class", "menu-incom-marcas-list");

        marcasContainer.Controls.Add(txt_buscador);

        foreach (menuMarca item in marcas)
        {
            HtmlGenericControl marcaItem = new HtmlGenericControl("li");
            HyperLink link_marcaItem = new HyperLink();

            marcaItem.Attributes.Add("data-active", "content"+item.nombreMarca.Replace(" ",""));


            link_marcaItem.Text = item.nombreMarca;
            link_marcaItem.NavigateUrl = "/productos/buscar?busqueda=" + item.nombreMarca+ "&filtroMarcas=" + item.nombreMarca;

            marcaItem.Controls.Add(link_marcaItem);
            marcasList.Controls.Add(marcaItem);

            List<menuMarca> categoriasMarca = catMarcas.FindAll(x => x.nombreMarca == item.nombreMarca );

            //
            HtmlGenericControl categoriaContent = new HtmlGenericControl("div");
            HtmlGenericControl marcasCategoriasList = new HtmlGenericControl("ul");

            categoriaContent.ID ="content"+item.nombreMarca.Replace(" ","");
            categoriaContent.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            categoriaContent.Attributes.Add("class", "menu-incom-marcas-categorias-list");

            HtmlGenericControl categoriaTitulo = new HtmlGenericControl("p");
            categoriaTitulo.Attributes.Add("class", "menu-incom-categoria-titulo");
            categoriaTitulo.InnerHtml = "<a href='/productos/buscar?busqueda=" + item.nombreMarca + "'>"+ item.nombreMarca+ "<a>";

            categoriaContent.Controls.Add(categoriaTitulo);

            // Agregamos las categorias que tiene cada marca
            foreach (menuMarca catItem in categoriasMarca)
            {




                HtmlGenericControl categoriaItem = new HtmlGenericControl("li");
                HtmlAnchor link_categoriaItem = new HtmlAnchor();
                link_categoriaItem.InnerText = catItem.categoria;
                link_categoriaItem.HRef = "/productos/"+ catItem.categoria + "-"+ catItem.identificadorCategoria+ "?filtroMarcas="+ item.nombreMarca + "&filtroCategorias=&PageId=1";


                categoriaItem.Controls.Add(link_categoriaItem);
 
                marcasCategoriasList.Controls.Add(categoriaItem);

            }
         
            categoriaContent.Controls.Add(marcasCategoriasList);
            categoriasContainer.Controls.Add(categoriaContent);



        }

        marcasContainer.Controls.Add(marcasList);

        menuContenedor.Controls.Add(marcasContainer);
        menuContenedor.Controls.Add(categoriasContainer);

        menuContenedor.Attributes.Add("style", "display:none;");
        liMenuPrincipal.Controls.Add(menuContenedor);
        return liMenuPrincipal;

    }
    /// <summary>
    /// Obtiene todas las categorias de un nivel en especifico
    /// </summary>
    public DataTable obtenerCategorias(int nivel)
    {


        dbConexion();
        using (con)
        {
            string query = "SELECT * FROM categorias WHERE nivel = @nivel";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@nivel", SqlDbType.Int);
            cmd.Parameters["@nivel"].Value = nivel;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            return ds.Tables[0];
        }


    }

     
        

    }
public class menuMarca
{
    public string nombreMarca { get; set; }
    public string categoria { get; set; }
    public int nivel { get; set; }
    public string identificadorCategoria { get; set; }
}

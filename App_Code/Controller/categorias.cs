using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de categorias
/// </summary>
public class categorias
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
    public DataTable obtenerCategorias()
    {


        dbConexion();
        using (con)
        {
            string query = "SELECT * FROM paises";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            return ds.Tables[0];
        }


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


    /// <summary>
    /// Obtiene las categorias hijas especificando el padre (Identificador)
    /// </summary>
    public DataTable obtener_CatHijas(string categoriaPadreID)
    {


        dbConexion();
        using (con)
        {
            string query = "SELECT * FROM categorias WHERE asociacion = @categoriaPadreID";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@categoriaPadreID", SqlDbType.NVarChar, 30);
            cmd.Parameters["@categoriaPadreID"].Value = categoriaPadreID;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            return ds.Tables[0];
        }


    }
    /// <summary>
    /// Obtiene el nombre de la categoria por el campo [identificador]
    /// </summary>
    public static string obtenerNombreCategoria(string identificador) {


        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        try {
            using (con) {
                string query = "SELECT nombre FROM categorias WHERE  identificador = @identificador";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@identificador", SqlDbType.NVarChar, 30);
                cmd.Parameters["@identificador"].Value = identificador;


                con.Open();
                string nombreCategoria = cmd.ExecuteScalar().ToString();
                return nombreCategoria;
            }
        } catch (Exception ex)
        {
            devNotificaciones.ErrorSQL("Obtener nombre categoria", ex, "Identificador: " + identificador);
            return null;
        }
    }


    




    /// <summary>
    /// Obtiene información de la categoria padre. Recibe el identificador
    /// </summary>
    public model_categorias obtener_CatPadre(string categoriaActualID)
    {

        model_categorias cat = new model_categorias();

        dbConexion();
        using (con)
        {
            string query = "DECLARE @catPadreID nvarchar(30); SET @catPadreID = (SELECT asociacion FROM categorias WHERE identificador = @categoriaActualID); " +
                           " SELECT * FROM categorias WHERE identificador = @catPadreID";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@categoriaActualID", SqlDbType.NVarChar, 30);
            cmd.Parameters["@categoriaActualID"].Value = categoriaActualID;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            dt = ds.Tables[0];

            if (dt.Rows.Count >= 1) {

                foreach (DataRow r in dt.Rows)
                {
                    cat.id = int.Parse(r["id"].ToString());
                    cat.nombre = r["nombre"].ToString();
                    cat.descripcion = r["descripcion"].ToString();
                    cat.imagen = r["imagen"].ToString();
                    cat.rol_categoria = r["rol_categoria"].ToString();
                    cat.productos_Destacados = r["productos_Destacados"].ToString();
                    cat.identificador = r["identificador"].ToString();
                    cat.asociacion = r["asociacion"].ToString();
                    cat.nivel = int.Parse(r["id"].ToString());
                    cat.orden = Convert.ToUInt16((r["orden"].ToString()));
                }

            }
            return cat;
        }


    }

    public model_categorias obtener_CatInfo(string identificador)
    {

        model_categorias cat = null;

        dbConexion();
        using (con)
        {
            string query = "SELECT * FROM categorias WHERE identificador = @identificador";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@identificador", SqlDbType.NVarChar, 30);
            cmd.Parameters["@identificador"].Value = identificador;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            dt = ds.Tables[0];

            if (dt.Rows.Count >= 1)
            {
                cat = new model_categorias();
                foreach (DataRow r in dt.Rows)
                {
                    cat.id = int.Parse(r["id"].ToString());
                    cat.nombre = r["nombre"].ToString();
                    cat.descripcion = r["descripcion"].ToString();
                    cat.imagen = r["imagen"].ToString();
                    cat.rol_categoria = r["rol_categoria"].ToString();
                    cat.productos_Destacados = r["productos_Destacados"].ToString();
                    cat.identificador = r["identificador"].ToString();
                    cat.asociacion = r["asociacion"].ToString();
                    cat.nivel = int.Parse(r["nivel"].ToString());
                    cat.orden = Convert.ToUInt16((r["orden"].ToString()));
                }

            }
            return cat;
        }
    }


        /// <summary>
        /// Obtiene las dos categorias padres de un nivel 3 [0] = nivel 1, [1] = nivel 2.
        /// </summary>
        public List<model_categorias> obtener_PadresL3(string identificadorL3)
        {

            List<model_categorias> cat = new List<model_categorias>();

            dbConexion();
            using (con)
            {
                string query = @"DECLARE @L2 nvarchar(30); SET @L2 = (SELECT asociacion FROM categorias WHERE identificador = @L3);
                            DECLARE @L1 nvarchar(30); SET @L1 = (SELECT asociacion FROM categorias WHERE identificador = @L2);
                            SELECT* FROM categorias WHERE identificador = @L1 UNION SELECT *FROM categorias WHERE identificador = @L2;
            ; ";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@L3", SqlDbType.NVarChar, 30);
                cmd.Parameters["@L3"].Value = identificadorL3;

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(ds);
                dt = ds.Tables[0];

                if (dt.Rows.Count >= 1)
                {

                    foreach (DataRow r in dt.Rows)
                    {
                        cat.Add(new model_categorias {
                            id = int.Parse(r["id"].ToString()),
                            nombre = r["nombre"].ToString(),
                            descripcion = r["descripcion"].ToString(),
                            imagen = r["imagen"].ToString(),
                            rol_categoria = r["rol_categoria"].ToString(),
                            productos_Destacados = r["productos_Destacados"].ToString(),
                            identificador = r["identificador"].ToString(),
                            asociacion = r["asociacion"].ToString(),
                            nivel = int.Parse(r["nivel"].ToString()),
                            orden = Convert.ToUInt16((r["orden"].ToString()))
                        });

                    }

                }
                return cat;
            }


        }

    } 
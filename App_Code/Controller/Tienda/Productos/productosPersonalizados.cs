using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de productosPersonalizados
/// </summary>
public class productosPersonalizados {
    public productosPersonalizados() {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
        }
    /// <summary>
    /// Obtiene el listado de marcas de la tabla SQL [productos_marcas] para productos personalizados y productos que no son de de productos base.
    /// </summary>
    public static DataTable obtenerMarcas() {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {
            string query = "SELECT * FROM productos_marcas ORDER BY marca_nombre ASC";
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
    /// Obtiene el listado de unidades de la tabla SQL [productos_unidades] para productos personalizados y productos que no son de de productos base.
    /// </summary>
    public static DataTable obtenerUnidades() {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {
            string query = "SELECT * FROM productos_unidades";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            return ds.Tables[0];
            }
        }
    }
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Descripción breve de usuarios
/// </summary>
 
public partial class productosCotizaciones 
{
    static string NombreTablaSQLCotizaciones = "cotizaciones_productos";
    static string NombreTablaSQLProductos = "cotizaciones_productos";
    ///<summary>
    /// Recuperar table productos
    ///</summary>
    public static DataTable recuperar_productos(int? cantidad) {

        if (cantidad == null) cantidad = 9999999;

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            StringBuilder sel = new StringBuilder();
            sel.Append("SELECT  TOP(@cantidad) * FROM " + NombreTablaSQLProductos + " ");

            cmd.CommandText = sel.ToString();
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@cantidad", SqlDbType.Int);
            cmd.Parameters["@cantidad"].Value = cantidad;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);

            return ds.Tables[0];
        }


    }
    /// <summary>
    /// Obtiene el total de items (SKU'S agrupados) en un periodo, 
    /// </summary>
    public int totaItemsCotizados(DateTime fechaDesde, DateTime fechaHasta) {

        //  Si no se recibe parametro fechaDesde, por default los últimos 30 días
        if (fechaDesde == null) fechaDesde = utilidad_fechas.obtenerCentral().AddDays(-30);

        // Si no se recibe parametro fechaHasta, se estable al día actual
        if (fechaHasta == null) fechaHasta = utilidad_fechas.obtenerCentral(); ;

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            StringBuilder sel = new StringBuilder();
            sel.Append(" SELECT COUNT(*)  FROM (SELECT COUNT(*) AS ItemsCotizados, numero_parte FROM " + NombreTablaSQLProductos);
            sel.Append("GROUP BY numero_parte) BETWEEN fecha_creacion " + fechaDesde);
            sel.Append(" fechaHasta " + fechaHasta);
            sel.Append(" GROUP BY numero_parte)");

            cmd.CommandText = sel.ToString();
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@fechaDesde", SqlDbType.DateTime);
            cmd.Parameters["@fechaDesde"].Value = fechaDesde;

            cmd.Parameters.Add("@fechaHasta", SqlDbType.DateTime);
            cmd.Parameters["@fechaHasta"].Value = fechaHasta;

            con.Open();
            int ItemsCotizados = int.Parse(cmd.ExecuteScalar().ToString());
         
            return ItemsCotizados;
        }

    }
    ///<summary>
    /// Returna  el número de registros con ese usuario en la tabla [usuarios]
    ///</summary>
  
}
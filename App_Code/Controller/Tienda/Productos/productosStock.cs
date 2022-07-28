using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Obtiene el stock de los productos que se cargan manualmente de SAP a una tabla en Incom.mx, inicialmente para saldos.
/// </summary>
public class productosStock
{
    public productosStock()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    /// <summary>
    /// Obtiene un listado de la ubicación (es) que se encuentre disponible.
    /// </summary>
    public static  List<productos_stock> obtenerStock(string numero_parte)
    {
        using (tiendaEntities db = new tiendaEntities())
        {
            var producto_stock = db.productos_stock
                .AsNoTracking()
                .Where(a => a.numero_parte == numero_parte).ToList();
            return producto_stock;
        }
    }


    /// <summary>
    ///  
    /// </summary>
    public static DataTable obtenerStockDT(string numero_parte)
    {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con)
        {
            string query = "SELECT * FROM productos_stock WHERE numero_parte =  @numero_parte";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar);
            cmd.Parameters["@numero_parte"].Value = numero_parte;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            return ds.Tables[0];
        }


    }


}
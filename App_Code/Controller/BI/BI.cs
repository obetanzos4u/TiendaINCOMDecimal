using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de BI
/// </summary>
public class BI
{
    private SqlConnection con { get; set; }
    private SqlCommand cmd { get; set; }
    private SqlDataAdapter da { get; set; }
    private DataSet ds { get; set; }
    private DataTable td { get; set; }
    protected void dbConexion()
    {
        con = new SqlConnection(conexiones.conexionTienda());
        cmd = new SqlCommand();
        cmd.Connection = con;
    }
    public BI()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public static DataTable ObtenerCategoríasBI ()
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;
            using (con)
            {
                string fecha = utilidad_fechas.obtenerFechaSQL();
                StringBuilder query = new StringBuilder();
                query.Append("SET LANGUAGE English; ");
                query.Append(@"SELECT 
                                COUNT (identificador) AS 'Busquedas', 
                                nombreCategoria AS 'Concepto', 
                                (SELECT email FROM [tienda].[dbo].[usuarios] WHERE id = idUsuario) AS 'Usuario', 
                                identificador AS 'Id_categoria' 
                                FROM [tienda].[dbo].[BI_hitsCategorias] 
                                WHERE fecha BETWEEN '2022-11-30 00:00:00.000' AND '2022-11-30 23:59:59.999' 
                                GROUP BY nombreCategoria, identificador, idUsuario;");
                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(ds);

                return ds.Tables[0];
            }
        }
        catch(Exception ex)
        {
            devNotificaciones.error("Obtener BI categorías", ex);
            return null;
        }
    }
}
using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Descripción breve de destacados
/// </summary>
public class destacados
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

    public DataTable obtenerDestacados()
    {
        dbConexion();
        try
        {
            using (con)
            {
                string query = "SELECT * FROM productos_Datos WHERE destacado='1'";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(ds);
                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.ErrorSQL("Obtener destacado", ex, "");
            return null;
        }
    }
}
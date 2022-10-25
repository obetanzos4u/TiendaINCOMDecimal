using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de productos
/// </summary>
public class productos
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
    public productos()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public string obtenerNumeroParteWithSAP(string noParte_Sap)
    {
        StringBuilder query = new StringBuilder();
        query.Append("SELECT numero_parte FROM productos_Datos WHERE noParte_Sap = @noParte_Sap;");
        dbConexion();

        using (con)
        {
            cmd.CommandText = query.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@noParte_Sap", SqlDbType.NVarChar, 50);
            cmd.Parameters["@noParte_Sap"].Value = noParte_Sap;
            con.Open();

            try
            {
                var result = cmd.ExecuteScalar();
                return result.ToString();
            }
            catch (Exception ex)
            {
                devNotificaciones.error("Error al obtener número de parte", ex);
            }
            return "";
        }
    }
}
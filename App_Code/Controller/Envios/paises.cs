using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de paises
/// </summary>
public class paises
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
    public DataTable obtenerPaises()
    {


        dbConexion();
        using (con) {
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

    public static string obtenerCódigoPais(string pais)
    {


        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con)
        {
            string query = "SELECT codigo FROM paises WHERE nombre = @nombre";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@nombre", pais);


            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
           string resultado = cmd.ExecuteScalar().ToString();
            return resultado;
        }
    }
    public DataTable obtenerEstados()
    {


        dbConexion();
        using (con)
        {
            string query = "SELECT * FROM estados_mexico";
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
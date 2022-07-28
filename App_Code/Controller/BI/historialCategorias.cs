using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de historialCategorias
/// </summary>
public class historialCategorias
{
    public historialCategorias()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }






    static public void guardarCategoriaHit(model_BI_HitCategorias categoria)
    {



        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con)
        {


            cmd.Parameters.Add("@idUsuario", SqlDbType.Int);
            cmd.Parameters["@idUsuario"].Value = categoria.idUsuario;

            cmd.Parameters.Add("@nombreCategoria", SqlDbType.NVarChar, 50);
            cmd.Parameters["@nombreCategoria"].Value = categoria.nombreCategoria;

            cmd.Parameters.Add("@identificador", SqlDbType.NVarChar, 50);
            cmd.Parameters["@identificador"].Value = categoria.identificador;

            cmd.Parameters.Add("@fecha", SqlDbType.DateTime);
            cmd.Parameters["@fecha"].Value = categoria.fecha;

            cmd.Parameters.Add("@direccion_ip", SqlDbType.NVarChar, 30);
            cmd.Parameters["@direccion_ip"].Value = categoria.direccion_ip;


            cmd.CommandType = CommandType.Text;

            cmd.CommandText = @"INSERT INTO BI_hitsCategorias(nombreCategoria, identificador, idUsuario, fecha, direccion_ip) 
                               VALUES (@nombreCategoria, @identificador, @idUsuario, @fecha, @direccion_ip)";

            try
            {
                con.Open();

                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {

                devNotificaciones.error("guardar hit categoria", ex);
            }

        }
    }
}




public class model_BI_HitCategorias
{

    public int? id { get; set; }

    public string nombreCategoria { get; set; }

    public string identificador { get; set; }

    public int idUsuario { get; set; }

    public DateTime fecha { get; set; }

    public string direccion_ip { get; set; }




}

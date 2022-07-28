using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
/// <summary>
/// Utilidades necesarias para trabajar la tienda
/// </summary>
public class operacionesConfiguraciones {

 


    /// <summary>
    /// Obtiene el tipo de cambio guardado en la tabla [operacionesConfiguraciones]
    /// </summary>
    /// 
    public static decimal obtenerTipoDeCambio() {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        using (con) {
           
            string query = "SELECT valorM FROM operacionesConfiguraciones WHERE identificador='TipoDeCambio'";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
           
            con.Open();
            decimal TipoDeCambio = Math.Round(decimal.Parse(cmd.ExecuteScalar().ToString()),5);
            
            return TipoDeCambio;
            }
        }

    static public bool  guardarTipoDeCambio(decimal tipoDeCambio) {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            string query = "UPDATE  operacionesConfiguraciones SET valorM=@tipoDeCambio,  fechaUltimaActualizacion=@fechaUltimaActualizacion WHERE identificador='TipoDeCambio'";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@TipoDeCambio", SqlDbType.Money);
            cmd.Parameters["@TipoDeCambio"].Value = tipoDeCambio;

            cmd.Parameters.Add("@fechaUltimaActualizacion", SqlDbType.DateTime);
            cmd.Parameters["@fechaUltimaActualizacion"].Value = utilidad_fechas.obtenerCentral();

            con.Open();
            try {
                cmd.ExecuteNonQuery();

                return true;
                } catch (Exception ex) {

                devNotificaciones.error("Guardar Tipo de cambio en configuraciones", ex);
                return false;
                }
         
            }
        }

    /// <summary>
    /// Obtiene el estatus de los cálculos de fletes mediante el api en la tabla [operacionesConfiguraciones]
    /// </summary>
    /// 
    public static bool obtenerEstatusApiFlete()
    {
        try { 
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        using (con)
        {

            string query = "SELECT valorB FROM operacionesConfiguraciones WHERE identificador='CalculoFletesApi'";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            con.Open();
            bool Activo = bool.Parse(cmd.ExecuteScalar().ToString());
            return Activo;
        }
        }
        catch(Exception ex)
        {

            return false;
        }
    }

    static public bool guardarEstatusApiFlete(bool Activo)
    {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con)
        {

            string query = "UPDATE  operacionesConfiguraciones SET valorB=@valorB,  fechaUltimaActualizacion=@fechaUltimaActualizacion WHERE identificador='CalculoFletesApi'";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@valorB", SqlDbType.Bit);
            cmd.Parameters["@valorB"].Value = Activo;

            cmd.Parameters.Add("@fechaUltimaActualizacion", SqlDbType.DateTime);
            cmd.Parameters["@fechaUltimaActualizacion"].Value = utilidad_fechas.obtenerCentral();

            con.Open();
            try
            {
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {

                devNotificaciones.error("Guardar Estatus ApiFlete", ex);
                return false;
            }

        }
    }
}
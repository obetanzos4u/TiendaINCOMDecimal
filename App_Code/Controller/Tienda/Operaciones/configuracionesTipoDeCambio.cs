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
public class configuracionesTipoDeCambio
{


    /// <summary>
    /// Obtiene el tipo de cambio de la fecha recibida
    /// </summary>
    /// 
    public static decimal obtenerTipoDeCambio(DateTime fecha) {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        using (con) {

            string query = "SELECT tipoDeCambio FROM configuracionesTipoDeCambio WHERE fecha=@fecha";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@fecha", SqlDbType.DateTime);
            cmd.Parameters["@fecha"].Value = fecha; utilidad_fechas.obtenerCentral();

            con.Open();
            decimal TipoDeCambio = Math.Round(decimal.Parse(cmd.ExecuteScalar().ToString()), 5);
            return TipoDeCambio;
        }
    }
    /// <summary>
    /// Obtiene el tipo de cambio de la fecha del día actual
    /// </summary>
    /// 
    public static decimal obtenerTipoDeCambioFechaActual() {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        using (con) {

            string query = "SELECT tipoDeCambio FROM configuracionesTipoDeCambio WHERE fecha=@fecha";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@fecha", SqlDbType.DateTime);
            cmd.Parameters["@fecha"].Value = utilidad_fechas.obtenerCentral();

            con.Open();
            decimal TipoDeCambio = Math.Round(decimal.Parse(cmd.ExecuteScalar().ToString()), 5);
            return TipoDeCambio;
        }
    }
    static public bool guardarTipoDeCambio(decimal tipoDeCambio) {

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
            }
            catch (Exception ex) {

                devNotificaciones.error("Guardar Tipo de cambio en configuraciones", ex);
                return false;
            }

        }
    }
    }
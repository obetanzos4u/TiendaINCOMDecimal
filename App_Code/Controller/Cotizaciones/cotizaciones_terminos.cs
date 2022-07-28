using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
 

/// <summary>
/// Descripción breve de cotizaciones_terminos
/// </summary>
public class cotizaciones_terminos {

    public enum tipoTermino {
        TiempoDeEntrega = 1,
        FormaDePago = 2,
        Entrega = 3,
        Envio = 4,
    }
    public int? idTermino { get; set; }
    public string numero_operacion { get; set; }
    public tipoTermino idTipoTermino { get; set; }
    public string termino { get; set; }
  
   

    public cotizaciones_terminos(int? _idTermino, string _numero_operacion, tipoTermino _idTipoTermino, string _termino) {
        idTermino = _idTermino;
         numero_operacion = _numero_operacion;
         idTipoTermino = _idTipoTermino;
         termino = _termino;
    }

    public bool GuardarTermino() {

        try {

            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;

            using (con) {

                StringBuilder query = new StringBuilder();


                query.Append("INSERT INTO cotizaciones_terminos  ");
                query.Append(" (numero_operacion, idTipoTermino, termino) ");
                query.Append(" VALUES (@numero_operacion, @idTipoTermino, @termino);");



                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@idTipoTermino", SqlDbType.Int);
                cmd.Parameters["@idTipoTermino"].Value = idTipoTermino;


                cmd.Parameters.Add("@termino", SqlDbType.NVarChar);
                cmd.Parameters["@termino"].Value = termino;


                con.Open();
                int resultado = cmd.ExecuteNonQuery();
                if (resultado == 1) return true; else return false;



            }
        }
        catch (Exception ex) {
            devNotificaciones.error("Insertar termino de cotización", ex);
            return false;
        }
        
  
    
    }
    static public bool ActualizarTermino(int idTermino, string termino) {

        try {

            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;

            using (con) {

                StringBuilder query = new StringBuilder();


                query.Append("UPDATE cotizaciones_terminos  ");
                query.Append("SET termino = @termino");
                query.Append(" WHERE idTermino = @idTermino");



                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.Add("@idTermino", SqlDbType.NVarChar);
                cmd.Parameters["@idTermino"].Value = idTermino;


                cmd.Parameters.Add("@termino", SqlDbType.NVarChar);
                cmd.Parameters["@termino"].Value = termino;


                con.Open();
                int resultado = cmd.ExecuteNonQuery();
                if (resultado == 1) return true; else return false;



            }
        }
        catch (Exception ex) {
            devNotificaciones.error("Actualiar término de cotización", ex);
            return false;
        }



    }

    static public  List< cotizaciones_terminos> ObtenerTerminosCotizacion(string numero_operacion) {

        try {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;
            using (con) {

                cmd.CommandText = "SELECT * FROM cotizaciones_terminos WHERE numero_operacion = @numero_operacion";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(ds);

           

                return ProcesarDTTerminosCot(ds.Tables[0]);
            }
        }
        catch (Exception ex) {

            devNotificaciones.error("No: operacion: "+numero_operacion, ex);

            return null;
        }
    }

   static public List<cotizaciones_terminos> ProcesarDTTerminosCot(DataTable dtTerminosCotizacion) {
        if (dtTerminosCotizacion != null && dtTerminosCotizacion.Rows.Count >=1) {

            List<cotizaciones_terminos> cotTerminos = new List<cotizaciones_terminos>();
            foreach (DataRow r in dtTerminosCotizacion.Rows) {

                int? idTermino = int.Parse(r["idTermino"].ToString());
                string numero_operacion = r["numero_operacion"].ToString();
                tipoTermino idTipoTermino = (tipoTermino)Enum.Parse(typeof(tipoTermino), r["idTipoTermino"].ToString());
                string terminoText = r["termino"].ToString();

                cotizaciones_terminos termino = new cotizaciones_terminos(idTermino, numero_operacion, idTipoTermino, terminoText);
                cotTerminos.Add(termino);
            }

            return cotTerminos;
        } return null;
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Guarda las busquedas de productos en la caja de texto, tanto visitantes y usuarios logeados
/// </summary>
public class BI_historialBusqueda {
 
    public BI_historialBusqueda() {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    static public void guardarBusqueda( string terminoBusqueda, HttpRequest Request) {

        int? idUsuario = null;
        string direccionIP = red.GetDireccionIp(Request);
        if (HttpContext.Current.User.Identity.IsAuthenticated) {
            idUsuario = usuarios.userLogin().id;
        }

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        StringBuilder query = new StringBuilder();
        query.Append("SET LANGUAGE English;  INSERT INTO historialBusquedasProductos ");
        query.Append("(idUsuario, fecha, terminoBusqueda, direccionIP) ");
        query.Append("VALUES (@idUsuario, @fecha, @terminoBusqueda, @direccionIP)");
       

        using (con) {

            cmd.CommandText = query.ToString();
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@terminoBusqueda", SqlDbType.NVarChar, 100);
            cmd.Parameters["@terminoBusqueda"].Value = terminoBusqueda;

            cmd.Parameters.Add("@idUsuario", SqlDbType.Int);
            if (idUsuario== null) cmd.Parameters["@idUsuario"].Value = DBNull.Value;
            else cmd.Parameters["@idUsuario"].Value = idUsuario;

            cmd.Parameters.Add("@direccionIP", SqlDbType.NVarChar,25);
            if (direccionIP == null) cmd.Parameters["@direccionIP"].Value = DBNull.Value;
            else cmd.Parameters["@direccionIP"].Value = direccionIP;

            
            cmd.Parameters.Add("@fecha", SqlDbType.DateTime);
            cmd.Parameters["@fecha"].Value = utilidad_fechas.obtenerCentral();


            try {
                con.Open();
                cmd.ExecuteNonQuery();
           
            }
            catch (Exception ex) {
                 devNotificaciones.error("Guardar término de búsqueda", ex);
            }
        }

    }

   

    }




    
      
    
     
    


       

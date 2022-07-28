using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;

/// <summary>
/// Descripción breve de reportes
/// </summary>
public class cotizacionesPlantillas {
    public cotizacionesPlantillas() {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    /// <summary>
    /// Recupera una plantilla de un determinado usuario (por email)
    /// </summary>
    /// <param name="id"> ID SQL de la plantilla a recuperar</param>  
    public static cotizacionesPlantillas recuperarPlantilla(int  id) {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        cotizacionesPlantillas cotizacionPlantilla = new cotizacionesPlantillas();
        using (con) {

            string query = "SELECT  *  FROM  cotizaciones_plantillas  WHERE id= @id";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = id;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);

            foreach (DataRow r in ds.Tables[0].Rows) {

                cotizacionPlantilla.id = int.Parse(r["id"].ToString());
                cotizacionPlantilla.usuario_email = r["usuario_email"].ToString();
                cotizacionPlantilla.fechaCreacion = DateTime.Parse(r["fechaCreacion"].ToString());
                cotizacionPlantilla.nombre = r["nombre"].ToString();
                cotizacionPlantilla.valor = r["valor"].ToString();
                if(r["fechaModificacion"]   !=  DBNull.Value)
                cotizacionPlantilla.fechaModificacion = DateTime.Parse(r["fechaModificacion"].ToString());
                    }

            return cotizacionPlantilla;
        }
    }

    /// <summary>
    /// Recupera las plantillas únicamente con los campos [id],[nombre] de un determinado usuario (por email)
    /// </summary>
    
    public static DataTable recuperarPlantillasMin() {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
       

        using (con) {

            string query = "SELECT id, nombre  FROM  cotizaciones_plantillas  WHERE usuario_email= @usuario_email order by fechaCreacion ASC";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@usuario_email", SqlDbType.NVarChar, 60);
            cmd.Parameters["@usuario_email"].Value = HttpContext.Current.User.Identity.Name; ;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);


            return ds.Tables[0];
        }
    }


    /// <summary>
    /// Recupera las plantillas de un determinado usuario (por email)
    /// </summary>
    public static  DataTable recuperarPlantillas( ) {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
       

        using (con) {

            string query = "SELECT *  FROM  cotizaciones_plantillas WHERE usuario_email= @usuario_email order by fechaCreacion ASC";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@usuario_email", SqlDbType.NVarChar, 60);
            cmd.Parameters["@usuario_email"].Value =  HttpContext.Current.User.Identity.Name;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);

           
            return ds.Tables[0];
        }
    }
   
   
    /// <summary>
    /// Elimina una plantilla con el campo id
    /// </summary>
    /// <param name="id"> id de la plantilla guardada a eliminar, devuelve true si se elimino 1 resultado en el query, de lo contrario regresa false
    public static bool eliminarPlantilla  (int id) {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {
            string query = "DELETE FROM cotizaciones_plantillas WHERE id=@id";

            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = id;
            try {
                con.Open();
                int resultado = int.Parse(cmd.ExecuteNonQuery().ToString());

            if(resultado == 1) {
                    return true;
                } 
            else if(resultado == 0) {
                    return false;
            }
            } catch(Exception ex) {
                devNotificaciones.error("Eliminar plantilla con el id: " + id, ex);
                return false;
            }
            return false;
        }
    }
    /// <summary>
    /// Crea una plantilla a partir de una cotización
    /// </summary>
    public static bool guardarPlantillaDeCotizacion( string numero_operacion) {
        DataTable dtProductos = cotizacionesProductos.obtenerProductosMin(numero_operacion);

        string producto = "";
        foreach (DataRow r in dtProductos.Rows) {
            decimal cantidad = Math.Round(decimal.Parse(r["cantidad"].ToString()), 1);
            producto = producto+r["numero_parte"].ToString() + " " + cantidad.ToString() + "\n";
        }
        producto.TrimEnd('\n');
        cotizacionesPlantillas plantilla = new cotizacionesPlantillas();
        plantilla.usuario_email = HttpContext.Current.User.Identity.Name;
        plantilla.nombre = utilidad_fechas.AAAMMDD() + " Cotizacion #: " + numero_operacion;
        plantilla.valor = producto;
        plantilla.fechaCreacion = utilidad_fechas.obtenerCentral();

        if (cotizacionesPlantillas.guardarPlantilla(plantilla)) { return true; }
        else { return false; }
    }

    /// <summary>
    /// Crea una plantilla a partir de un carrito
    /// </summary>
    public static bool guardarPlantillaDeCarrito() {

        string usuario_email = usuarios.modoAsesor().email;

        DataTable dtProductos = carrito.obtenerCarritoUsuarioMin(usuario_email);

        string producto = "";
        foreach (DataRow r in dtProductos.Rows) {
            decimal cantidad = Math.Round(decimal.Parse(r["cantidad"].ToString()), 1);
            producto = producto+r["numero_parte"].ToString() + " " + cantidad.ToString() + "\n";
        }
        producto.TrimEnd('\n');
        cotizacionesPlantillas plantilla = new cotizacionesPlantillas();
        plantilla.usuario_email = HttpContext.Current.User.Identity.Name;
        plantilla.nombre = utilidad_fechas.AAAMMDD() + " Carrito del usuario  : " + usuario_email;
        plantilla.valor = producto;
        plantilla.fechaCreacion = utilidad_fechas.obtenerCentral();

        if (cotizacionesPlantillas.guardarPlantilla(plantilla)) { return true; } else { return false; }
    }
    public  static bool guardarPlantilla(cotizacionesPlantillas plantilla) {

        try {

            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;

            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; INSERT INTO cotizaciones_plantillas  ");
                query.Append(" (usuario_email, fechaCreacion, nombre, valor) ");
                query.Append(" VALUES (@usuario_email, @fechaCreacion, @nombre, @valor)  ");

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@usuario_email", SqlDbType.NVarChar,60);
                cmd.Parameters["@usuario_email"].Value = plantilla.usuario_email;

                cmd.Parameters.Add("@fechaCreacion", SqlDbType.DateTime);
                cmd.Parameters["@fechaCreacion"].Value = plantilla.fechaCreacion;

                cmd.Parameters.Add("@nombre", SqlDbType.NVarChar,65);
                cmd.Parameters["@nombre"].Value = plantilla.nombre;

            
                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, 800);
                cmd.Parameters["@valor"].Value = textTools.lineMulti(plantilla.valor);

              

                con.Open();

                cmd.ExecuteNonQuery();
                return true;
            }
        }
        catch (Exception ex) {
            devNotificaciones.error("Guardar plantilla personalizada, valor: "+ plantilla.valor, ex);
            return false;
        }
    }

    public static bool actualizarPlantilla(cotizacionesPlantillas plantilla) {

        try {

            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;

            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; UPDATE cotizaciones_plantillas SET ");
                query.Append("  nombre = @nombre, valor = @valor, fechaModificacion =  @fechaModificacion ");
                query.Append(" WHERE id=@id  ");

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = plantilla.id;


           
                cmd.Parameters.Add("@nombre", SqlDbType.NVarChar, 65);
                cmd.Parameters["@nombre"].Value = plantilla.nombre;


                cmd.Parameters.Add("@valor", SqlDbType.NVarChar, 800);
                cmd.Parameters["@valor"].Value = textTools.lineMulti(plantilla.valor);

                cmd.Parameters.Add("@fechaModificacion", SqlDbType.DateTime);
                cmd.Parameters["@fechaModificacion"].Value = plantilla.fechaModificacion;


                con.Open();

                cmd.ExecuteNonQuery();
                return true;
            }
        }
        catch (Exception ex) {
            devNotificaciones.error("Guardar plantilla personalizada, valor: " + plantilla.valor, ex);
            return false;
        }
    }

    public int id { get; set; }
    public string usuario_email { get; set; }
    public DateTime fechaCreacion { get; set; }
    public string nombre { get; set; }
    public string valor { get; set; }
    public DateTime fechaModificacion { get; set; }
}
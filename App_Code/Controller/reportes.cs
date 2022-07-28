using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;

/// <summary>
/// Genera reportes de cotizaciones/pedidos
/// </summary>
public class reportes {
    public reportes() {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    /// <summary>
    /// Recupera todas las tablas y campos disponibles para un reporte
    /// </summary>
    /// <param name="nombreReporte">Nombre del reporte: [cotDatos],[cotPro],[cotDatos],[cotDireccFact],[cotDireccEnv]</param>  

    public static DataTable recuperarCampos(string aliasNombreReporte) {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {
            StringBuilder sel = new StringBuilder();
            sel.Append("SELECT * FROM reportesCampos ");
            sel.Append(" WHERE aliasNombreReporte = @aliasNombreReporte;");

            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@aliasNombreReporte", SqlDbType.NVarChar, 40);
            cmd.Parameters["@aliasNombreReporte"].Value = aliasNombreReporte;

           
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);

            return ds.Tables[0];
        }
    }
    /// <summary>
    /// Recupera los campos [tablaAlias] de manera agrupada
    /// </summary>
    /// <param name="aliasNombreReporte">Nombre del reporte: [cotizaciones],[pedidos],[usuarios]</param>  
    public static Dictionary<string, string> recuperarTablasAlias(string aliasNombreReporte) {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        Dictionary<string, string> tablasAlias = new Dictionary<string, string>();

        using (con) {

            string query = "SELECT  tablaAlias, tablaNombre  FROM  reportesCampos   WHERE aliasNombreReporte = @aliasNombreReporte GROUP BY tablaAlias, tablaNombre;";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@aliasNombreReporte", SqlDbType.NVarChar, 40);
            cmd.Parameters["@aliasNombreReporte"].Value = aliasNombreReporte;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);

            foreach (DataRow r in ds.Tables[0].Rows)
                tablasAlias.Add(r["tablaNombre"].ToString(), r["tablaAlias"].ToString() );

            return tablasAlias;
        }
    }
    /// <summary>
    /// Recupera los nombres de reportes disponibles
    /// </summary>
    /// <param name="NombreReporte">Nombre del reporte al que se desea obtener el Join correcto de la tabla reporteQuerys: [cotizaciones],[pedidos],[usuarios]</param>  
    public static string recuperarJoins(string aliasNombreReporte) {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        DataTable dtTabla = new DataTable();

        using (con) {
            StringBuilder sel = new StringBuilder();
            sel.Append("SELECT   valorReporte  FROM reportesJoins ");
            sel.Append(" WHERE  aliasNombreReporte = @aliasNombreReporte  ");

            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@aliasNombreReporte", SqlDbType.NVarChar, 40);
            cmd.Parameters["@aliasNombreReporte"].Value = aliasNombreReporte;

 

          
            con.Open();

            return cmd.ExecuteScalar().ToString();
        }
      
         
        
    }
   
    /// <summary>
    /// Elimina un reporte con el campo id
    /// </summary>
    /// <param name="id"> id del reporte guardado a eliminar, devuelve true si se elimino 1 resultado en el query, de lo contrario regresa false
    public static bool eliminarReporte(int id) {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {
            string query = "DELETE FROM reportesPreferencias WHERE id=@id";

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
                devNotificaciones.error("Eliminar reporte con el id: " + id, ex);
                return false;
            }
            return false;
        }
    }
    public static List<HtmlGenericControl> controlesLI(DataTable DT_campos) {

        List<HtmlGenericControl> elementos = new List<HtmlGenericControl>();

        foreach (DataRow r in DT_campos.Rows) {

            HtmlGenericControl elemento = new HtmlGenericControl("li");

            string tablaNombre = r["tablaNombre"].ToString();
            string tablaAlias = r["tablaAlias"].ToString();
            string campoValor = r["campoValor"].ToString();
            string campoAlias = r["campoAlias"].ToString();

            // Este IFF es para hacer funcionar el código js  que crea los query
            if (campoValor.Contains("IFF")) campoValor = campoValor.Replace("IFF", "IFF ");

            if (campoValor.Contains("{aliasTabla}")) campoValor = campoValor.Replace("{aliasTabla}", tablaAlias);

            elemento.InnerHtml = "[" + tablaNombre + "] " + "<strong> " + campoAlias + " </strong> ";

            elemento.Attributes.Add("tablaAlias", tablaAlias);
            elemento.Attributes.Add("campoValor", campoValor);
            elemento.Attributes.Add("campoAlias", campoAlias);
            elementos.Add(elemento);
        }

        return elementos;
    }

    public  static bool guardarReporte(string tipoNombreReporteFK, string aliasNombreReporte, string nombre, string valorReporte, string valorHTML) {

        try {

            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;

            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; INSERT INTO reportesPreferencias  ");
                query.Append(" (tipoNombreReporteFK, aliasNombreReporte, usuario_email, nombre, valor, valorHTML) ");
                query.Append(" VALUES (@tipoNombreReporteFK, @aliasNombreReporte, @usuario_email, @nombre, @valor, @valorHTML)  ");

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.Add("@tipoNombreReporteFK", SqlDbType.NVarChar,40);
                cmd.Parameters["@tipoNombreReporteFK"].Value = tipoNombreReporteFK;


                cmd.Parameters.Add("@aliasNombreReporte", SqlDbType.NVarChar, 25);
                cmd.Parameters["@aliasNombreReporte"].Value = aliasNombreReporte;

                cmd.Parameters.Add("@usuario_email", SqlDbType.NVarChar);
                cmd.Parameters["@usuario_email"].Value = System.Web.HttpContext.Current.User.Identity.Name;

                cmd.Parameters.Add("@nombre", SqlDbType.NVarChar, 40);
                cmd.Parameters["@nombre"].Value = textTools.lineSimple(nombre);

                cmd.Parameters.Add("@valor", SqlDbType.NVarChar);
                cmd.Parameters["@valor"].Value = valorReporte.Trim(' ');

                cmd.Parameters.Add("@valorHTML", SqlDbType.NVarChar);
                cmd.Parameters["@valorHTML"].Value = valorHTML.Trim(' ').Trim('|').Trim(' ').Trim('|');

                con.Open();

                cmd.ExecuteNonQuery();
                return true;
            }
        }
        catch (Exception ex) {
            devNotificaciones.error("Guardar reporte", ex);
            return false;
        }
    }
    /// <summary>
    /// Obtiene los reportes guardados por el usuario logeado
    /// </summary>
    public static DataTable obtenerReportesMin() {


        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        using (con) {
            string query = "SELECT  id, nombre FROM reportesPreferencias WHERE usuario_email = @usuario_email ORDER BY nombre ASC;";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@usuario_email", SqlDbType.NVarChar,60);
            cmd.Parameters["@usuario_email"].Value = System.Web.HttpContext.Current.User.Identity.Name;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            return ds.Tables[0];
        }


    }

    /// <summary>
    /// Obtiene los reportes guardados por el usuario logeado
    /// </summary>
    /// <param name="tipo">Tipo del reporte al que se desea obtener los nombres [cotizaciones],[pedidos],[usuarios]</param>  
    public static DataTable obtenerReportesDisponibles(string tipo) {


        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        using (con) {
            string query = "SELECT NombreReporte, aliasNombreReporte  " +
                "FROM reportesCampos WHERE tipo = @tipo GROUP BY NombreReporte, aliasNombreReporte; ";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@tipo", SqlDbType.NVarChar, 40);
            cmd.Parameters["@tipo"].Value = tipo;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            return ds.Tables[0];
        }
    }
    public static string obtenerValorReporte(int id) {


        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            string query = "SELECT valor FROM reportesPreferencias WHERE usuario_email = @usuario_email AND id=@id ; ";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = id;

            cmd.Parameters.Add("@usuario_email", SqlDbType.NVarChar, 60);
            cmd.Parameters["@usuario_email"].Value = System.Web.HttpContext.Current.User.Identity.Name;

          
            con.Open();
            string valor = cmd.ExecuteScalar().ToString();
            return valor;
        }


    }

    ///<summary>
    /// Valida la existencia de un nombre de un reporte
    ///</summary>
    public static  int validarExistencia_NombreReporte(string nombreReporte) {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {
            string query = @"SET LANGUAGE English;  SELECT COUNT(nombre) FROM reportesPreferencias WHERE nombre = @nombre";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@nombre", SqlDbType.NVarChar, 60);
            cmd.Parameters["@nombre"].Value = textTools.lineSimple(nombreReporte);


            con.Open();
            int resultado = Convert.ToInt16(cmd.ExecuteScalar());
            
            return resultado;

        }


    }
    public string tableReferenciaAlias { get; set; }
    public string campoFK { get; set; }
    public string tablaMultiplesRegistros { get; set; }
}
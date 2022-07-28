using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
/// <summary>
/// Utilidades necesarias para trabajar los sliders de la tienda
/// </summary>
public class configuracion_sliders {

    public static int cantidadMaximaSlider = 5;
    /// <summary>
    /// Devuelve la ruta [~/img/webUI/]
    /// </summary>
    public static string directorioSliderRelativo = "~/img/webUI/sliderHome/";
    public static string directorioSliderAbsoluto = HttpContext.Current.Request.PhysicalApplicationPath + "\\img\\webUI\\sliderHome\\";
    private SqlConnection con { get; set; }
    private SqlCommand cmd { get; set; }
    private SqlDataAdapter da { get; set; }
    private DataSet ds { get; set; }
    private DataTable dt { get; set; }


    /// <summary>
    /// Obtiene los nombres de los archivos dentro de la carpeta 
    /// </summary>
    /// 
    public static string[] obtenerListadoDeImagenesSlider(string seccion) {

        DirectoryInfo d = new DirectoryInfo(HttpContext.Current.Request.PhysicalApplicationPath + "\\img\\webUI\\"+ seccion);
        FileInfo[] Files = d.GetFiles();

        // Si contiene archivos hacemos el proceso, si no es inecesario
        if (Files.Length >= 1) {
            string[] sliderFileName = new string[Files.Length];

            for (int i = 0; i < Files.Length; i++) {
                sliderFileName[i] = Files[i].Name;
                }

            return sliderFileName;
            }
        return null;
        }

    /// <summary>
    /// Obtiene los registros que haya en cada sección
    /// </summary>
    /// 
    public static DataTable obtenerImagenesSlider(string seccion) {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            string query = "SELECT * FROM configuracion_sliders WHERE seccion=@seccion ORDER BY posicion ASC";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@seccion", SqlDbType.NVarChar, 50);
            cmd.Parameters["@seccion"].Value = seccion;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);

            return ds.Tables[0];

            }
        }
    /// <summary>
    /// Obtiene los registros que haya en cada sección
    /// </summary>
     public static DataTable obtenerSlider(string seccion, string id) {

    SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            string query = "SELECT * FROM configuracion_sliders WHERE seccion=@seccion AND id=@id";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@seccion", SqlDbType.NVarChar, 50);
            cmd.Parameters["@seccion"].Value = seccion;

            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = int.Parse(id);

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);

            return ds.Tables[0];

            }
        }
    static public bool actualizarSlider(model_configuracion_sliders slider) {

        try {
            string total = @" UPDATE configuracion_sliders
                          SET      seccion = @seccion, titulo = @titulo, descripcion = @descripcion, nombreArchivo = @nombreArchivo,
                                   activo = @activo, posicion = @posicion, link = @link, opciones = @opciones, duracion = @duracion 
                          WHERE id = @id;
        ";


        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            cmd.CommandText = total;
            cmd.CommandType = CommandType.Text;

            if (slider.id == null ) return false;
            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = slider.id;

            cmd.Parameters.Add("@seccion", SqlDbType.NVarChar, 50);
            if (slider.seccion == null) cmd.Parameters["@seccion"].Value = DBNull.Value;
            else cmd.Parameters["@seccion"].Value = slider.seccion;

            cmd.Parameters.Add("@titulo", SqlDbType.NVarChar, 50);
            if (slider.titulo == null) cmd.Parameters["@titulo"].Value = DBNull.Value;
            else cmd.Parameters["@titulo"].Value = slider.titulo;

            cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar, 50);
            if (slider.descripcion == null) cmd.Parameters["@descripcion"].Value = DBNull.Value;
            else cmd.Parameters["@descripcion"].Value = slider.descripcion;

            cmd.Parameters.Add("@nombreArchivo", SqlDbType.NVarChar, 100);
            if (slider.nombreArchivo == null) cmd.Parameters["@nombreArchivo"].Value = DBNull.Value;
            else cmd.Parameters["@nombreArchivo"].Value = slider.nombreArchivo;

            cmd.Parameters.Add("@activo", SqlDbType.Int);
            cmd.Parameters["@activo"].Value = slider.activo;

                cmd.Parameters.Add("@posicion", SqlDbType.Int);
                if (slider.posicion == null)
                cmd.Parameters["@posicion"].Value = 5;
            else
                cmd.Parameters["@posicion"].Value = slider.posicion;

            cmd.Parameters.Add("@link", SqlDbType.NVarChar, 180);
            if (string.IsNullOrWhiteSpace(slider.link)) cmd.Parameters["@link"].Value = DBNull.Value;
            else cmd.Parameters["@link"].Value = slider.link;

            cmd.Parameters.Add("@opciones", SqlDbType.NVarChar, 250);
            if (slider.opciones == null) cmd.Parameters["@opciones"].Value = DBNull.Value;
            else cmd.Parameters["@opciones"].Value = slider.opciones;

            cmd.Parameters.Add("@duracion", SqlDbType.Float);
            if (slider.duracion == float.NaN) cmd.Parameters["@duracion"].Value = 2000;
            else cmd.Parameters["@duracion"].Value = slider.duracion;

       
            con.Open();

         
                cmd.ExecuteNonQuery();
                return true;
            }
        }
        catch (Exception ex) {
                devNotificaciones.error("Error al actualizar slider", ex);
                return false;
                }


        }
    static public bool actualizarSliderActivarDesactivar(string id, int activo) {

        string total = @" UPDATE configuracion_sliders
                          SET      
                                   activo = @activo 
                          WHERE id = @id;
        ";


        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            cmd.CommandText = total;
            cmd.CommandType = CommandType.Text;

            if (id == null) return false;
            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = id;
 
            cmd.Parameters.Add("@activo", SqlDbType.Int);
            cmd.Parameters["@activo"].Value = activo;

 
            con.Open();

            try {
                cmd.ExecuteNonQuery();
                return true;
                }
            catch (Exception ex) {
                devNotificaciones.error("Error al activar/desactivar slider", ex);
                return false;
                }

            }

        }
    static public int  obtenerSliderActivos(string seccion) {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            string query = "SELECT COUNT(seccion) FROM configuracion_sliders WHERE seccion=@seccion AND activo = 1";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@seccion", SqlDbType.NVarChar, 50);
            cmd.Parameters["@seccion"].Value = seccion;

            con.Open();
            try {
               int cantidadSlider = int.Parse(cmd.ExecuteScalar().ToString());

                return cantidadSlider;
                } catch (Exception ex) {

                devNotificaciones.error("Obtener cantidad de slider activos", ex);
                return -1;
                }
         
            }
        }
    static public bool insertarSlider(model_configuracion_sliders slider) {

        try {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;

            using (con) {

                StringBuilder query = new StringBuilder();

             
                query.Append("INSERT INTO configuracion_sliders  ");
                query.Append(" (seccion, titulo, descripcion, nombreArchivo, activo, posicion, link, opciones, duracion ) ");
                query.Append(" VALUES (@seccion, @titulo, @descripcion, @nombreArchivo, @activo, @posicion, @link, @opciones, @duracion );");

             

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;
              

                cmd.Parameters.Add("@seccion", SqlDbType.NVarChar, 50);
                if (slider.seccion == null) cmd.Parameters["@seccion"].Value = DBNull.Value;
                else cmd.Parameters["@seccion"].Value = slider.seccion;

                cmd.Parameters.Add("@titulo", SqlDbType.NVarChar, 50);
                if (slider.titulo == null) cmd.Parameters["@titulo"].Value = DBNull.Value;
                else cmd.Parameters["@titulo"].Value = slider.titulo;

                cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar, 50);
                if (slider.descripcion == null) cmd.Parameters["@descripcion"].Value = DBNull.Value;
                else cmd.Parameters["@descripcion"].Value = slider.descripcion;

                cmd.Parameters.Add("@nombreArchivo", SqlDbType.NVarChar, 100);
                if (slider.nombreArchivo == null) cmd.Parameters["@nombreArchivo"].Value = DBNull.Value;
                else cmd.Parameters["@nombreArchivo"].Value = slider.nombreArchivo;

                cmd.Parameters.Add("@activo", SqlDbType.Int);

                int sliderActivos = obtenerSliderActivos("sliderHome");

                if(sliderActivos > cantidadMaximaSlider) 
                    cmd.Parameters["@activo"].Value = 0;
                else  
                    cmd.Parameters["@activo"].Value = slider.activo;

                cmd.Parameters.Add("@posicion", SqlDbType.Int);
                if (slider.posicion == null)
                    cmd.Parameters["@posicion"].Value = 5;
                else
                    cmd.Parameters["@posicion"].Value = slider.posicion;


                cmd.Parameters.Add("@link", SqlDbType.NVarChar, 180);
                if (slider.link == null) cmd.Parameters["@link"].Value = DBNull.Value;
                else cmd.Parameters["@link"].Value = slider.link;

                cmd.Parameters.Add("@opciones", SqlDbType.NVarChar, 250);
                if (slider.opciones == null) cmd.Parameters["@opciones"].Value = DBNull.Value;
                else cmd.Parameters["@opciones"].Value = slider.opciones;

                cmd.Parameters.Add("@duracion", SqlDbType.Float);
                if (slider.duracion == float.NaN) cmd.Parameters["@duracion"].Value = 2000;
                else cmd.Parameters["@duracion"].Value = slider.duracion;


                con.Open();
                cmd.ExecuteNonQuery();
                return true;
                }
            }
        catch (Exception ex) {
            devNotificaciones.error("Insertar Slider", ex);
            return false;
            }
        }

    static public bool eliminarSlider(int? id) { 
        if (id == null) return false;

     
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {
            cmd.CommandText = "SET LANGUAGE English; DELETE FROM configuracion_sliders   WHERE id=@id;";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = id;

            try {
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
                }
            catch (Exception ex) {
                return false;
                }
            }
 
        }
    static public bool eliminarImagen(string seccion, string fileName) {

        string fileFullName = HttpContext.Current.Request.PhysicalApplicationPath + "\\img\\webUI\\" + seccion+"\\"+ fileName;
        if (File.Exists(fileFullName)) {
            File.Delete(fileFullName);
            return true;
        } else return false;
 
    }
    }
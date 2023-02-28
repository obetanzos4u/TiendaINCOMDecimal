using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Descripción breve de historialProductos
/// </summary>
public class BI_historialProductos { 
    private static string sql_table_name = "BI_hitsProductos";
    public bool  resultado { get; set; }
    public string mensaje { get; set; }
    public BI_historialProductos() {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    /// <summary>
    /// Devuelve en orden Desc los productos más visitados en un determinado periodo. Columnas: [numero_parte][titulo][imagenes][descripcion_corta][total]
    /// </summary>
    static public DataTable obtenerProductosMásVisitados(int idUsuario, int cantidadProductos, DateTime fechaIni, DateTime fechaFin)
    {
        
       
        StringBuilder query = new StringBuilder();
        query.Append(@"SELECT TOP("+cantidadProductos+@") BIProductos.numero_parte, datos.titulo, datos.imagenes, datos.descripcion_corta,  datos.marca
            ,COUNT (BIProductos.numero_parte) as total 
             FROM " + sql_table_name + @"  BIProductos
             LEFT JOIN   productos_Datos  datos
             ON BIProductos.numero_parte = datos.numero_parte
             WHERE idUsuario = @idUsuario AND
             BIProductos.fecha BETWEEN  @fechaIni AND @fechaFin
             GROUP by
            BIProductos.numero_parte
           ,datos.titulo
           , datos.imagenes
           ,datos.descripcion_corta
 ,datos.marca
            ORDER by total DESC");

      

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con)
        {

            cmd.CommandText = query.ToString();
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@idUsuario", SqlDbType.Int);
            cmd.Parameters["@idUsuario"].Value = idUsuario;

            cmd.Parameters.Add("@fechaIni", SqlDbType.DateTime);
            cmd.Parameters["@fechaIni"].Value = fechaIni;


            cmd.Parameters.Add("@fechaFin", SqlDbType.DateTime);
            cmd.Parameters["@fechaFin"].Value = fechaFin;


            try
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(ds);
                return ds.Tables[0];

            }
            catch (Exception ex)
            {
                devNotificaciones.ErrorSQL("Obtener Productos visitados", ex, "id usuario: " + idUsuario);
                return null;
               
            }
        } 

    }
    static public void guardarHitProducto(model_BI_historialProductos producto) {

      

        StringBuilder query = new StringBuilder();

        query.Append("SET LANGUAGE English; INSERT INTO " + sql_table_name + " ");
        query.Append("(numero_parte, idUsuario, fecha, direccion_ip)  ");
        query.Append(" VALUES (@numero_parte, @idUsuario, @fecha, @direccion_ip ) ");

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            cmd.CommandText = query.ToString();
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 50);
            cmd.Parameters["@numero_parte"].Value = producto.numero_parte;

            cmd.Parameters.Add("@idUsuario", SqlDbType.Int);
            cmd.Parameters["@idUsuario"].Value = producto.idUsuario;

            cmd.Parameters.Add("@fecha", SqlDbType.DateTime);
            cmd.Parameters["@fecha"].Value = producto.fecha;

            cmd.Parameters.Add("@direccion_ip", SqlDbType.NVarChar, 30);
            cmd.Parameters["@direccion_ip"].Value = producto.direccion_ip;


            try {
                con.Open();
                cmd.ExecuteNonQuery();
           
            }
            catch (Exception ex) {
                 devNotificaciones.error("guardar hit", ex);
            }
        }

    }

    static public void ObtenerCantidadCategorias (model_BI_historialProductos producto) {

        StringBuilder query = new StringBuilder();
        query.Append("SET LANGUAGE English;  SELECT dp.marca, COUNT(marca) as total FROM   " + sql_table_name + "  p");
        query.Append(" LEFT JOIN productos_Datos dp");
        query.Append(" ON p.numero_parte = dp.numero_parte");
        query.Append(" WHERE p.fecha BETWEEN @fechaIni AND @fechaFin");
        query.Append(" GROUP by dp.marca ORDER by total DESC");

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            cmd.CommandText = query.ToString();
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 50);
            cmd.Parameters["@numero_parte"].Value = producto.numero_parte;

            cmd.Parameters.Add("@idUsuario", SqlDbType.Int);
            cmd.Parameters["@idUsuario"].Value = producto.idUsuario;

            cmd.Parameters.Add("@fecha", SqlDbType.DateTime);
            cmd.Parameters["@fecha"].Value = producto.fecha;

            cmd.Parameters.Add("@direccion_ip", SqlDbType.NVarChar, 30);
            cmd.Parameters["@direccion_ip"].Value = producto.direccion_ip;


            try {
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex) {
                devNotificaciones.error("guardar hit", ex);
            }
        }

    }




    
      
    
     
    


       

    /*
    ObtenerCantidadCategorias
    obtenerCantidadProductos
    ObtenerProductoAgrupados
    obtenerProductosRelacionados
    Procesar
    guardarEfectividadProducto
    configuracionObtenerTop
    configuracionObtenerTopTodos
    configuracionEliminarTop
    configuracionAgregarTop
    configuracionEditarTop
    configuracionSwitchSección

    */
}

public class model_BI_historialProductos {
    public int? id { get; set; }
    public string numero_parte { get; set; }
    public int idUsuario { get; set; }
    public DateTime fecha { get; set; }
    public string direccion_ip { get; set; }
}
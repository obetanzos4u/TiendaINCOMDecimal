using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Descripción breve de cotizaciones
/// </summary>
public class carrito
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

    public carrito()
    {
        // 
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public DataTable obtenerCarritoProducto(string usuario, string producto)
    {
        dbConexion();
        using (con)
        {
            StringBuilder sel = new StringBuilder();
            sel.Append("SELECT ");
            // Campos básicos
            sel.Append(" * ");
            sel.Append(" FROM carrito_productos WHERE usuario = @usuario AND numero_parte = @numero_parte;");

            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 60);
            cmd.Parameters["@usuario"].Value = usuario;
            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 50);
            cmd.Parameters["@numero_parte"].Value = producto;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            return ds.Tables[0];
        }
    }
    /// <summary>
    /// Obtiene el campo [id], [numero_parte], [cantidad] de la tabla carrito de un usuario establecido
    /// </summary>
    public static DataTable obtenerCarritoUsuarioMin(string usuario)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        using (con)
        {
            StringBuilder sel = new StringBuilder();
            sel.Append("SELECT ");
            // Campos básicos
            sel.Append(@"
                        id,
                        numero_parte,
                        cantidad
                        ");
            sel.Append(" FROM carrito_productos   " +
                "WHERE usuario = @usuario ;");

            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 60);
            cmd.Parameters["@usuario"].Value = usuario;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            return ds.Tables[0];
        }
    }
    /// <summary>
    /// Obtiene todos los campos de la tabla carrito de un usuario establecido
    /// </summary>
    public DataTable obtenerCarritoUsuario(string usuario)
    {
        dbConexion();
        using (con)
        {
            StringBuilder sel = new StringBuilder();
            sel.Append("SELECT ");
            // Campos básicos
            sel.Append(@"
                        id,
                        usuario,
                        activo,
                        tipo ,
                        fecha_creacion,
                        numero_parte,
                        (SELECT top(1) titulo from productos_Datos WHERE  numero_parte = carrito_productos.numero_parte) as titulo,
                        descripcion,
                        marca,
                        moneda, 
                        tipo_cambio ,
                        fecha_tipo_cambio,
                        unidad,
                        precio_unitario,
                        cantidad,
                        precio_total,
                        stock1,
                        stock1_fecha,
                        stock2,
                        stock2_fecha,
                        (SELECT top(1) imagenes from productos_Datos WHERE  numero_parte = carrito_productos.numero_parte) as imagenes ");
            sel.Append(" FROM carrito_productos   " +
                "WHERE usuario = @usuario ;");
            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 60);
            cmd.Parameters["@usuario"].Value = usuario;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            con.Close();
            return ds.Tables[0];
        }
    }

    /// <summary>
    /// Obtiene todos los campos de la tabla carrito de un usuario establecido con medidas y datos importantes
    /// </summary>
    public DataTable obtenerCarritoUsuarioWithMedidas(string usuario)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        using (con)
        {
            StringBuilder sel = new StringBuilder();
            sel.Append("SELECT ");
            // Campos básicos
            sel.Append(@"
                        Carrito.id,
                        Carrito.usuario,
                        Carrito.activo,
                        Carrito.tipo ,
                        Carrito.fecha_creacion,
                        Carrito.numero_parte,
                        P_Datos.titulo,
                        Carrito.descripcion,
                        Carrito.marca,
                        Carrito.moneda, 
                        Carrito.tipo_cambio ,
                        Carrito.fecha_tipo_cambio,
                        Carrito.unidad,
                        Carrito.precio_unitario,
                        Carrito.cantidad,
                        Carrito.precio_total,
                        Carrito.stock1,
                        Carrito.stock1_fecha,
                        Carrito.stock2,
                        Carrito.stock2_fecha,
                        P_Datos.imagenes,
                        P_Datos.avisos,
                        P_Datos.disponibleEnvio,
                        P_Datos.RotacionHorizontal,
                        P_Datos.RotacionVertical,
                        P_Datos.alto,
                        P_Datos.ancho,
                        P_Datos.profundidad,
                        P_Datos.peso
                        ");

            sel.Append(" FROM carrito_productos as Carrito" +
                " FULL OUTER JOIN productos_Datos P_Datos ON Carrito.numero_parte = P_Datos.numero_parte " +
                " WHERE usuario = @usuario ;");
            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 60);
            cmd.Parameters["@usuario"].Value = usuario;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            return ds.Tables[0];
        }
    }
    // Función para obtener los productos del carrito del usuario con número de parte SAP
    public DataTable obtenerCarritoUsuarioWithSAP(string usuario)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        using (con)
        {
            StringBuilder sel = new StringBuilder();
            sel.Append("SELECT ");
            sel.Append(@"
                        Carrito.id,
                        Carrito.usuario,
                        Carrito.activo,
                        Carrito.tipo ,
                        Carrito.fecha_creacion,
                        Carrito.numero_parte,
                        P_Datos.numero_parte,
                        P_Datos.titulo,
                        Carrito.descripcion,
                        Carrito.marca,
                        Carrito.moneda, 
                        Carrito.tipo_cambio ,
                        Carrito.fecha_tipo_cambio,
                        Carrito.unidad,
                        Carrito.precio_unitario,
                        Carrito.cantidad,
                        Carrito.precio_total,
                        P_Datos.disponibleEnvio,
                        P_Datos.noParte_Sap
                    ");
            sel.Append(" FROM carrito_productos as Carrito FULL OUTER JOIN productos_Datos P_Datos ON Carrito.numero_parte = P_Datos.numero_parte WHERE usuario = @usuario;");
            string query = sel.ToString();
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 60);
            cmd.Parameters["@usuario"].Value = usuario;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            return ds.Tables[0];
        }
    }

    public static int obtenerCantidadProductos(string usuario)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        using (con)
        {
            StringBuilder sel = new StringBuilder();
            sel.Append(@"
                SELECT COUNT(*) FROM carrito_productos 
	                 WHERE usuario = @usuario
                ");

            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 60);
            cmd.Parameters["@usuario"].Value = usuario;
            con.Open();
            return int.Parse(cmd.ExecuteScalar().ToString());
        }
    }
    public string obtenerTotalMXN(string usuario)
    {
        dbConexion();
        using (con)
        {
            StringBuilder sel = new StringBuilder();
            sel.Append(@"
                SELECT SUM([totalMXN]) FROM
                 (SELECT  IIF( carrito_productos.moneda = 'USD',  
	                carrito_productos.precio_total *  carrito_productos.tipo_cambio, 
	                carrito_productos.precio_total) as  [totalMXN] FROM carrito_productos 
	                 WHERE usuario =  @usuario) as [totalMXN]
                ");
            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 60);
            cmd.Parameters["@usuario"].Value = usuario;
            con.Open();
            return cmd.ExecuteScalar().ToString();
        }
    }
    public string obtenerTotalUSD(string usuario)
    {
        dbConexion();
        using (con)
        {
            StringBuilder sel = new StringBuilder();
            sel.Append(@"
                SELECT SUM([totalUSD]) FROM
                 (SELECT  IIF( carrito_productos.moneda = 'MXN',  
	                carrito_productos.precio_total /  carrito_productos.tipo_cambio, 
	                carrito_productos.precio_total) as  [totalUSD] FROM carrito_productos 
	                 WHERE usuario =  @usuario) as [totalUSD]
                ");

            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 60);
            cmd.Parameters["@usuario"].Value = usuario;
            con.Open();
            return cmd.ExecuteScalar().ToString();
        }
    }
    public bool actualizarStockCarritoProducto (string usuario, string numero_parte, int stock)
    {
        StringBuilder query = new StringBuilder();
        query.Append("SET LANGUAGE English; UPDATE carrito_productos SET stock1 = @stock1, stock1_fecha = @stock1_fecha WHERE usuario = @usuario AND numero_parte = @numero_parte;");
        dbConexion();

        using (con)
        {
            cmd.CommandText = query.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 50);
            cmd.Parameters["@usuario"].Value = usuario;
            cmd.Parameters.Add("@stock1", SqlDbType.NVarChar, 10);
            cmd.Parameters["@stock1"].Value = stock;
            cmd.Parameters.Add("@stock1_fecha", SqlDbType.DateTime);
            cmd.Parameters["@stock1_fecha"].Value = utilidad_fechas.obtenerCentral();
            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 50);
            cmd.Parameters["@numero_parte"].Value = numero_parte;
            con.Open();

            try
            {
                var result = cmd.ExecuteNonQuery();
                if (result != 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                devNotificaciones.error("Actualizar stock carrito", ex);
            }
            return false;
        }
    }
    public void desactivarProductoCarrito (string usuario, string numero_parte)
    {
        StringBuilder query = new StringBuilder();
        query.Append("UPDATE carrito_productos SET activo = 0 WHERE usuario = @usuario AND numero_parte = @numero_parte");
        dbConexion();

        using (con)
        {
            cmd.CommandText = query.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 50);
            cmd.Parameters["@usuario"].Value = usuario;
            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 50);
            cmd.Parameters["@numero_parte"].Value = numero_parte;
            con.Open();

            try
            {
                var result = cmd.ExecuteNonQuery();
                if (result == 0) { return; }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al desactivar producto");
            }
        }
    }
    public void actualizarCantidadCarritoProducto(string usuario, model_cotizacionesProductos producto)
    {
        StringBuilder query = new StringBuilder();
        query.Append("SET LANGUAGE English; UPDATE  carrito_productos  SET ");
        query.Append(" moneda = @moneda, tipo_cambio = @tipo_cambio, fecha_tipo_cambio = @fecha_tipo_cambio,  precio_unitario = @precio_unitario, ");
        query.Append(" cantidad = @cantidad , precio_total = @precio_total ");
        query.Append(" WHERE usuario = @usuario AND numero_parte = @numero_parte; ");
        dbConexion();

        using (con)
        {
            cmd.CommandText = query.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 50);
            cmd.Parameters["@usuario"].Value = producto.usuario;
            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 50);
            cmd.Parameters["@numero_parte"].Value = producto.numero_parte;
            cmd.Parameters.Add("@moneda", SqlDbType.NVarChar, 5);
            cmd.Parameters["@moneda"].Value = producto.moneda;
            cmd.Parameters.Add("@tipo_cambio", SqlDbType.Money);
            cmd.Parameters["@tipo_cambio"].Value = producto.tipo_cambio;
            cmd.Parameters.Add("@fecha_tipo_cambio", SqlDbType.DateTime);
            cmd.Parameters["@fecha_tipo_cambio"].Value = producto.fecha_tipo_cambio;
            cmd.Parameters.Add("@unidad", SqlDbType.NVarChar, 25);
            cmd.Parameters["@unidad"].Value = producto.unidad;
            cmd.Parameters.Add("@precio_unitario", SqlDbType.Money);
            cmd.Parameters["@precio_unitario"].Value = producto.precio_unitario;
            cmd.Parameters.Add("@cantidad", SqlDbType.Money);
            cmd.Parameters["@cantidad"].Value = producto.cantidad;
            cmd.Parameters.Add("@precio_total", SqlDbType.Money);
            cmd.Parameters["@precio_total"].Value = producto.precio_total;
            con.Open();

            try
            {
                int resultado = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                devNotificaciones.error("Actualizar cantidad producto de carrito ", ex);
            }
        }
    }
    public void agregarProductoCarrito(string usuario, model_cotizacionesProductos producto)
    {
        StringBuilder query = new StringBuilder();

        query.Append("SET LANGUAGE English; INSERT INTO carrito_productos  ");
        query.Append(" (usuario, activo, tipo, fecha_creacion, numero_parte, descripcion, marca, moneda, tipo_cambio, fecha_tipo_cambio, unidad, precio_unitario, cantidad, ");
        query.Append(" precio_total, stock1, stock1_fecha) ");
        query.Append(" VALUES (@usuario, @activo, @tipo, @fecha_creacion, @numero_parte, @descripcion, @marca, @moneda, @tipo_cambio, @fecha_tipo_cambio, @unidad, @precio_unitario, @cantidad, ");
        query.Append(" @precio_total, @stock1, @stock1_fecha)  ");
        dbConexion();

        using (con)
        {
            cmd.CommandText = query.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 50);
            cmd.Parameters["@usuario"].Value = producto.usuario;
            cmd.Parameters.Add("@activo", SqlDbType.Int);
            cmd.Parameters["@activo"].Value = producto.activo;
            cmd.Parameters.Add("@tipo", SqlDbType.Int);
            cmd.Parameters["@tipo"].Value = producto.tipo;
            cmd.Parameters.Add("@fecha_creacion", SqlDbType.DateTime);
            cmd.Parameters["@fecha_creacion"].Value = producto.fecha_creacion;
            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 50);
            cmd.Parameters["@numero_parte"].Value = producto.numero_parte;
            cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar, 250);
            cmd.Parameters["@descripcion"].Value = producto.descripcion;
            cmd.Parameters.Add("@marca", SqlDbType.NVarChar, 50);
            cmd.Parameters["@marca"].Value = producto.marca;
            cmd.Parameters.Add("@moneda", SqlDbType.NVarChar, 5);
            cmd.Parameters["@moneda"].Value = producto.moneda;
            cmd.Parameters.Add("@tipo_cambio", SqlDbType.Money);
            cmd.Parameters["@tipo_cambio"].Value = producto.tipo_cambio;
            cmd.Parameters.Add("@fecha_tipo_cambio", SqlDbType.DateTime);
            cmd.Parameters["@fecha_tipo_cambio"].Value = producto.fecha_tipo_cambio;
            cmd.Parameters.Add("@unidad", SqlDbType.NVarChar, 25);
            cmd.Parameters["@unidad"].Value = producto.unidad;
            cmd.Parameters.Add("@precio_unitario", SqlDbType.Money);
            cmd.Parameters["@precio_unitario"].Value = producto.precio_unitario;
            cmd.Parameters.Add("@cantidad", SqlDbType.Money);
            cmd.Parameters["@cantidad"].Value = producto.cantidad;
            cmd.Parameters.Add("@precio_total", SqlDbType.Money);
            cmd.Parameters["@precio_total"].Value = producto.precio_total;
            cmd.Parameters.Add("@stock1", SqlDbType.Money);
            cmd.Parameters["@stock1"].Value = producto.stock1;
            cmd.Parameters.Add("@stock1_fecha", SqlDbType.DateTime);
            cmd.Parameters["@stock1_fecha"].Value = producto.stock1_fecha;
            con.Open();

            cmd.ExecuteNonQuery();
        }
    }

    public void eliminarProductoCarrito(string idProductoCarrito)
    {
        StringBuilder query = new StringBuilder();
        query.Append("SET LANGUAGE English; DELETE FROM carrito_productos WHERE id=@id");
        dbConexion();
        using (con)
        {
            cmd.CommandText = query.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = idProductoCarrito;
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
    public void eliminarCarrito(string usuario)
    {
        StringBuilder query = new StringBuilder();
        query.Append("SET LANGUAGE English; DELETE FROM carrito_productos WHERE usuario=@usuario ");
        dbConexion();
        using (con)
        {
            cmd.CommandText = query.ToString();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 60);
            cmd.Parameters["@usuario"].Value = usuario;
            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                devNotificaciones.error("Eliminar carrito del usuario: " + usuario, ex);
            }
        }
    }
}
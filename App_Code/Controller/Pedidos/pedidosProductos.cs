using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Descripción breve de pedidos productos
/// </summary>
public class pedidosProductos : model_pedidos_productos
{
    private SqlConnection con { get; set; }
    private SqlCommand cmd { get; set; }
    private SqlDataAdapter da { get; set; }
    private DataSet ds { get; set; }
    private DataTable dt { get; set; }
    public string monedaPedido { get; set; }
    public decimal tipoDeCambio { get; set; }
    public DateTime fechaTipoDeCambio { get; set; }

    protected void dbConexion()
    {
        con = new SqlConnection(conexiones.conexionTienda());
        cmd = new SqlCommand();
        cmd.Connection = con;
    }
    public pedidosProductos()
    {
        // 
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    /// <summary>
    /// Retorna los productos de un pedido: id, numero_parte, descripcion, marca, cantidad, unidad, precios e imagenes de la productosBase
    /// </summary>
    /// 
    public static DataTable obtenerProductos(string numero_operacion)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        using (con)
        {

            StringBuilder sel = new StringBuilder();


            // Campos básicos
            sel.Append("SELECT ");
            sel.Append(@"

                        id,
                        numero_operacion,
                        usuario,
                        orden,
                        tipo ,
                        fecha_creacion,
                        numero_parte,
                        descripcion,
                        marca,
                        (SELECT top(1) monedaPedido from pedidos_datosNumericos WHERE  numero_operacion = pedidos_productos.numero_operacion) as  monedaPedido, 
                        unidad,
                        precio_unitario,
                        cantidad,
                        precio_total,
                        stock1,
                        stock1_fecha,
                        stock2,
                        stock2_fecha,
                        (SELECT top(1) imagenes from productos_Datos WHERE  numero_parte = pedidos_productos.numero_parte) as imagenes  ,
                        (SELECT top(1) titulo from productos_Datos WHERE  numero_parte = pedidos_productos.numero_parte) as titulo,

   (SELECT top(1) pdf from productos_Datos WHERE  numero_parte = pedidos_productos.numero_parte) as pdf
");

            sel.Append(" FROM pedidos_productos   " +
                "WHERE numero_operacion = @numero_operacion ;");

            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
            cmd.Parameters["@numero_operacion"].Value = numero_operacion;


            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            return ds.Tables[0];
        }
    }
    /// <summary>
    /// Obtiene la incedencia de productos en un pedido
    /// </summary>
    static public DataTable obtenerProducto(string numero_operacion, string producto)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con)
        {

            StringBuilder sel = new StringBuilder();

            sel.Append("SELECT ");

            // Campos básicos
            sel.Append(" * ");


            sel.Append(" FROM pedidos_productos WHERE numero_operacion = @numero_operacion AND numero_parte = @numero_parte;");

            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
            cmd.Parameters["@numero_operacion"].Value = numero_operacion;

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
    ///Agregar producto a una operación, si se agrega correctamente devuelve true
    /// </summary>
    static public bool agregarProducto(string numero_operacion, model_pedidos_productos producto)
    {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        try
        {
            using (con)
            {

                StringBuilder sel = new StringBuilder();

                sel.Append("INSERT INTO pedidos_productos");

                // Campos básicos
                sel.Append(@"(
                           [numero_operacion]
                          ,[usuario]
                          ,[orden]
                          ,[tipo]
                          ,[fecha_creacion]
                          ,[numero_parte]
                          ,[descripcion]
                          ,[marca]
                          ,[unidad]
                          ,[precio_unitario]
                          ,[cantidad]
                          ,[precio_total]
                          --,[stock1]
                          --,[stock1_fecha]
                          --,[stock2]
                          --,[stock2_fecha]

)  ");

                sel.Append(@"

                    VALUES(
                           @numero_operacion
                          ,@usuario
                          ,@orden
                          ,@tipo
                          ,@fecha_creacion
                          ,@numero_parte
                          ,@descripcion
                          ,@marca
                          ,@unidad
                          ,@precio_unitario
                          ,@cantidad
                          ,@precio_total
                          --,@stock1
                          --,@stock1_fecha
                          --,@stock2
                          --,@stock2_fecha
                            
)");


                string query = sel.ToString(); ;
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 60);
                cmd.Parameters["@usuario"].Value = producto.usuario;

                cmd.Parameters.Add("@orden", SqlDbType.Int);
                cmd.Parameters["@orden"].Value = producto.orden;

                cmd.Parameters.Add("@tipo", SqlDbType.Int);
                cmd.Parameters["@tipo"].Value = producto.tipo;

                cmd.Parameters.Add("@fecha_creacion", SqlDbType.DateTime);
                cmd.Parameters["@fecha_creacion"].Value = producto.fecha_creacion;

                cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 50);
                cmd.Parameters["@numero_parte"].Value = producto.numero_parte;

                cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar, 500);
                cmd.Parameters["@descripcion"].Value = producto.descripcion;

                cmd.Parameters.Add("@marca", SqlDbType.NVarChar, 50);
                cmd.Parameters["@marca"].Value = producto.marca;

                cmd.Parameters.Add("@unidad", SqlDbType.NVarChar, 25);
                cmd.Parameters["@unidad"].Value = producto.unidad;

                cmd.Parameters.Add("@precio_unitario", SqlDbType.Money);
                cmd.Parameters["@precio_unitario"].Value = producto.precio_unitario;

                cmd.Parameters.Add("@cantidad", SqlDbType.Money);
                cmd.Parameters["@cantidad"].Value = producto.cantidad;

                cmd.Parameters.Add("@precio_total", SqlDbType.Money);
                cmd.Parameters["@precio_total"].Value = producto.precio_total;
                /*
                                cmd.Parameters.Add("@stock1", SqlDbType.money);
                                if (producto.stock1 == float.NaN) cmd.Parameters["@stock1"].Value = DBNull.Value;
                                else cmd.Parameters["@stock1"].Value = producto.stock1;

                                cmd.Parameters.Add("@stock1_fecha", SqlDbType.DateTime);
                                if (producto.stock1_fecha == null) cmd.Parameters["@stock1_fecha"].Value = DBNull.Value;
                                else cmd.Parameters["@stock1_fecha"].Value = producto.stock1_fecha;

                                cmd.Parameters.Add("@stock2", SqlDbType.Float);
                                if (producto.stock2 == float.NaN) cmd.Parameters["@stock2"].Value = DBNull.Value;
                                else cmd.Parameters["@stock2"].Value = producto.stock2;

                                cmd.Parameters.Add("@stock2_fecha", SqlDbType.DateTime);
                                cmd.Parameters["@stock2_fecha"].Value = producto.stock2_fecha;
                  */
                con.Open();

                cmd.ExecuteNonQuery();
            }
            return true;
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Agregar producto a pedido de la operación: " + numero_operacion, ex);
            return false;
        }
    }
    /// <summary>
    /// Retorna los productos de un pedido: id, numero_parte, descripcion, marca, cantidad, unidad
    /// </summary>
    /// 
    public DataTable obtenerProductosPEdidosmin(string numero_operacion)
    {
        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT numero_parte, descripcion, marca, cantidad, unidad FROM  pedidos_productos  
                                 WHERE numero_operacion = @numero_operacion");

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;
                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;



                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(ds);

                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            Task.Run(() => devNotificaciones.error("Obtener productos mínimos de un pedido, número de operación: " + numero_operacion, ex));
            return null;
        }
    }
    /// <summary>
    /// Retorna todos campos de la tabla "pedidos_productos"
    /// </summary>
    /// 
    public DataTable obtenerProductosPedido_max(string numero_operacion)
    {
        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");
                query.Append(@"SELECT id, numero_operacion, usuario, orden, activo, tipo, fecha_creacion, numero_parte, descripcion, marca, unidad,  precio_unitario, cantidad, precio_total, stock1, stock1_fecha, stock2, stock2_fecha FROM pedidos_productos  
                                 WHERE numero_operacion = @numero_operacion");

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;
                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;



                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(ds);

                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Obtener productos de un pedido (todas las columnas), número de operación: " + numero_operacion, ex);
            return null;
        }
    }
    /// <summary>
    /// Retorna X cantidad de productos de un pedido: id, numero_parte, descripcion, marca, cantidad, unidad
    /// </summary>
    /// <param name="numeroProductos">Especifica el # de productos a retornar </param>  
    public DataTable obtenerProductosPedido_min(string numero_operacion, int numeroProductos)
    {
        try
        {
            dbConexion();
            using (con)
            {
                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");
                query.Append(@"SELECT TOP(@numeroProductos) numero_parte, descripcion, marca, cantidad, unidad FROM pedidos_productos 
                                 WHERE numero_operacion = @numero_operacion");

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@numeroProductos", SqlDbType.Int);
                cmd.Parameters["@numeroProductos"].Value = numeroProductos;

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;



                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(ds);

                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Obtener productos mínimos de un pedido", ex);
            return null;
        }
    }
    public DataTable obtenerProductosPedidoDatosMin(string numero_operacion, int numeroProductos)
    {
        try
        {
            dbConexion();
            using (con)
            {
                StringBuilder query = new StringBuilder();
                query.Append("SET LANGUAGE English; ");
                query.Append(@"SELECT TOP(@numeroProductos) productos.numero_parte, productos.descripcion, productos.marca, productos.cantidad, productos.unidad, datos.OperacionCancelada FROM pedidos_productos AS productos FULL OUTER JOIN pedidos_datos AS datos ON productos.numero_operacion = datos.numero_operacion WHERE productos.numero_operacion = @numero_operacion");

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@numeroProductos", SqlDbType.Int);
                cmd.Parameters["@numeroProductos"].Value = numeroProductos;

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(ds);

                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Obtener productos mínimos de un producto", ex);
            return null;
        }
    }
    /// <summary>
    /// Retorna el número total de productos de un pedido, retorna 0 si se crea un error
    /// </summary>
    /// 
    public int obtenerCantidadProductosPedido(string numero_operacion)
    {
        try
        {
            dbConexion();

            using (con)
            {

                StringBuilder query = new StringBuilder();
                query.Append("SET LANGUAGE English; ");
                query.Append(@"SELECT COUNT(*)  FROM pedidos_productos 
                                 WHERE numero_operacion = @numero_operacion");

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                con.Open();
                int productosTotales = int.Parse(cmd.ExecuteScalar().ToString());

                return productosTotales;
            }
        }

        catch (Exception ex)
        {
            devNotificaciones.error("Obtener cantidad de productos de un pedido", ex);
            return 0;
        }
    }
    public void eliminarProducto(string idProductoCarrito)
    {
        StringBuilder query = new StringBuilder();

        query.Append("SET LANGUAGE English; DELETE FROM pedidos_productos   WHERE id=@id");

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
    /// <summary>
    /// Suma productos, envío y aplica el iva
    /// </summary>
    public bool actualizarTotal(string numero_operacion)
    {



        string total = @"DECLARE @totalProductos money;
                            -- Obtenemos el total de los productos del producto y que estos sean activos
                            SET @totalProductos = (SELECT sum(precio_total) FROM pedidos_productos WHERE numero_operacion = @numero_operacion);

                            IF (@totalProductos IS NULL) BEGIN 
	                                 SET @totalProductos  = 0;
	                          END

                                 DECLARE @envio money;
                                SET @envio = (SELECT sum(envio) FROM pedidos_datosNumericos WHERE numero_operacion = @numero_operacion);

                                DECLARE @subtotal money;
                                SET @subtotal = IIF(@envio IS NULL, @totalProductos + 0, @totalProductos + @envio);
                                      
                                DECLARE @descuento_porcentaje SMALLMONEY;
                                SET @descuento_porcentaje = (SELECT descuento_porcentaje FROM pedidos_datosNumericos WHERE numero_operacion = @numero_operacion);

                                DECLARE @descuento money;
                                 SET @descuento = IIF(@descuento_porcentaje IS NULL, NULL, ROUND((@subtotal * @descuento_porcentaje)/100, 2) );

                                -- Aplicando el descuento
                                SET @subtotal = IIF(@descuento IS NULL, @subtotal, ROUND(@subtotal - @descuento, 2) );

                                DECLARE @impuestos money;
                                SET @impuestos = @subtotal * 0.16;


                                DECLARE @total money;
                                SET @total = @subtotal + @impuestos;


                                UPDATE pedidos_datosNumericos
                                SET subtotal = ROUND(@subtotal, 2), impuestos = ROUND(@impuestos, 2), total = ROUND(@total, 2)
                                ,descuento = @descuento,  descuento_porcentaje = @descuento_porcentaje
                                WHERE numero_operacion = @numero_operacion;
        ";



        dbConexion();
        using (con)
        {

            cmd.CommandText = total;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
            cmd.Parameters["@numero_operacion"].Value = numero_operacion;

            con.Open();

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                devNotificaciones.error("Error al actualizar totales de pedido:" + numero_operacion, ex);
                return false;
            }

        }

    }
    /// <summary>
    /// Suma productos, envío y aplica el IVA
    /// </summary>
    /// 20191211 Mod. para  actualizar el total contemplando el procentaje 
    public static bool actualizarTotalStatic(string numero_operacion)
    {



        string total = @"DECLARE @totalProductos money;
                            -- Obtenemos el total de los productos del producto y que estos sean activos
                            SET @totalProductos = (SELECT sum(precio_total) FROM pedidos_productos WHERE numero_operacion = @numero_operacion);
     

                            IF (@totalProductos IS NULL) BEGIN 
	                                 SET @totalProductos  = 0;
	                          END

                                DECLARE @envio money;
                                SET @envio = (SELECT sum(envio) FROM pedidos_datosNumericos WHERE numero_operacion = @numero_operacion);

                                DECLARE @subtotal money;
                                SET @subtotal = IIF(@envio IS NULL, @totalProductos + 0, @totalProductos + @envio);
                                      
                                DECLARE @descuento_porcentaje SMALLMONEY;
                                SET @descuento_porcentaje = (SELECT descuento_porcentaje FROM pedidos_datosNumericos WHERE numero_operacion = @numero_operacion);

                                DECLARE @descuento money;
                                 SET @descuento = IIF(@descuento_porcentaje IS NULL, NULL, ROUND((@subtotal * @descuento_porcentaje)/100, 2) );

                                -- Aplicando el descuento
                                SET @subtotal = IIF(@descuento IS NULL, @subtotal, ROUND(@subtotal - @descuento, 2) );

                                DECLARE @impuestos money;
                                SET @impuestos = @subtotal * 0.16;


                                DECLARE @total money;
                                SET @total = @subtotal + @impuestos;


                                UPDATE pedidos_datosNumericos
                                SET subtotal = ROUND(@subtotal, 2), impuestos = ROUND(@impuestos, 2), total = ROUND(@total, 2)
                                ,descuento = @descuento,  descuento_porcentaje = @descuento_porcentaje
                                WHERE numero_operacion = @numero_operacion;
        ";



        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con)
        {

            cmd.CommandText = total;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
            cmd.Parameters["@numero_operacion"].Value = numero_operacion;

            con.Open();

            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                devNotificaciones.error("Error al actualizar totales de pedido:" + numero_operacion, ex);
                return false;
            }

        }

    }

    public bool actualizarCantidadProducto(string numero_operacion, model_pedidos_productos producto)
    {
        StringBuilder query = new StringBuilder();
        query.Append("SET LANGUAGE English; UPDATE  pedidos_productos  SET ");
        query.Append("   precio_unitario = @precio_unitario, ");
        query.Append(" cantidad = @cantidad , precio_total = @precio_total ");

        dbConexion();

        using (con)
        {
            // Si es producto de la DB se actualiza la información
            if (producto.tipo == 1)
            {
                cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar, 500);
                cmd.Parameters["@descripcion"].Value = producto.descripcion;

                cmd.Parameters.Add("@marca", SqlDbType.NVarChar, 50);
                cmd.Parameters["@marca"].Value = producto.marca;

                cmd.Parameters.Add("@unidad", SqlDbType.NVarChar, 25);
                cmd.Parameters["@unidad"].Value = producto.unidad;

                query.Append(" ,descripcion = @descripcion , marca = @marca, unidad = @unidad ");

            }

            cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
            cmd.Parameters["@numero_operacion"].Value = numero_operacion;

            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 50);
            cmd.Parameters["@numero_parte"].Value = producto.numero_parte;

            cmd.Parameters.Add("@precio_unitario", SqlDbType.Money);
            cmd.Parameters["@precio_unitario"].Value = producto.precio_unitario;

            cmd.Parameters.Add("@cantidad", SqlDbType.Money);
            cmd.Parameters["@cantidad"].Value = producto.cantidad;

            cmd.Parameters.Add("@precio_total", SqlDbType.Money);
            cmd.Parameters["@precio_total"].Value = producto.precio_total;

            // Es importante mantener este orden
            query.Append(" WHERE numero_operacion = @numero_operacion AND numero_parte = @numero_parte; ");

            cmd.CommandText = query.ToString();
            cmd.CommandType = CommandType.Text;

            con.Open();
            try
            {
                int resultado = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Task.Run(() => devNotificaciones.error("Actualizar cantidad de producto en un pedido número:" + numero_operacion, ex));
                return false;
            }
        }
    }
    public string crearPedidoDeCotizacion_productos(string numero_operacionPedido, string numero_operacion_cotizacion)
    {
        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();
                query.Append("SET LANGUAGE English;");
                query.Append(@"INSERT INTO pedidos_productos (
	                                  [numero_operacion]
                                      ,[usuario]
                                      ,[orden]
                                      ,[tipo]
                                      ,[fecha_creacion]
                                      ,[numero_parte]
                                      ,[descripcion]
                                      ,[marca]
                                      ,[unidad]
                                      ,[precio_unitario]
                                      ,[cantidad]
                                      ,[precio_total]
                                      ,[stock1]
                                      ,[stock1_fecha]
                                      ,[stock2]
                                      ,[stock2_fecha]  ) 

	                          SELECT 
	                                [numero_operacion]  = @numero_operacionPedido
                                    ,[usuario]
                                    ,[orden]
                                    ,[tipo]
                                    ,[fecha_creacion]
                                    ,[numero_parte]
                                    ,[descripcion]
                                    ,[marca]
                                    ,[unidad]
                                    ,[precio_unitario]
                                    ,[cantidad]
                                    ,[precio_total]
                                    ,[stock1]
                                    ,[stock1_fecha]
                                    ,[stock2]
                                    ,[stock2_fecha]
	                             FROM cotizaciones_productos WHERE numero_operacion = @numero_operacion_cotizacion");


                cmd.Parameters.Add("@numero_operacionPedido", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacionPedido"].Value = numero_operacionPedido;

                cmd.Parameters.Add("@numero_operacion_cotizacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion_cotizacion"].Value = numero_operacion_cotizacion;

                cmd.CommandText = query.ToString();

                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();

                return "";

            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Crear productos de pedido y datos numéricos", ex);
            return null;
        }
    }
    /// <summary>
    /// 3/4 Crea el pedido y de vuelve un string con el número de operación, también vacía el carrito si esta fue creada con éxito.
    /// </summary>
    /// 
    public string crearPedidoDeCarrito_productos(usuarios usuario, string numero_operacion, model_impuestos impuestos, model_envios envio)
    {
        {
            // DECLARE @idUltimo int; SET @idUltimo = (SELECT MAX(id) FROM cotizaciones_datos);
            // numero_operacion: [ca= de carrito],[co=co directa]

            string subtotal = null;
            string carrito = null;

            string subtotalMXN = "SELECT sum( IIF([moneda] = 'USD',  [precio_total] *  [tipo_cambio], [precio_total] )) FROM carrito_productos";
            string subtotalUSD = "SELECT sum( IIF([moneda] = 'MXN',  [precio_total] /  [tipo_cambio], [precio_total] )) FROM carrito_productos";

            // carritotToUSD
            string carritotToUSD = @"
                -- Cotización/Carrito a USD
                -- Convierte todos los productos del carrito a USD para pasarlo a una cotización
                SELECT 
                       numero_operacion = @numero_operacion                      
                      ,[usuario]
                      ,[tipo]
                      ,[fecha_creacion]
                      ,[numero_parte]
                      ,[descripcion]
                      ,[marca]
                      ,[unidad]
                      ,[precio_unitario] = IIF([moneda] = 'MXN',  [precio_unitario] /  [tipo_cambio], [precio_unitario] )
                      ,[cantidad]
                      ,[precio_total] = IIF([moneda] = 'MXN',  [precio_total] /  [tipo_cambio], [precio_total] ) 
                   -- ,[stock1]
                   -- ,[stock1_fecha]
                   -- ,[stock2]
                   -- ,[stock2_fecha]
                  FROM [carrito_productos] WHERE activo = '1' AND stock1 > 0
		
		";
            // carritotToMXN
            string carritotToMXN = @"
                -- Cotización/Carrito a MXN
                -- Convierte todos los productos del carrito a MXN para pasarlo a una cotización
                SELECT 
                       numero_operacion = @numero_operacion
                      ,[usuario]
                      ,[tipo]
                      ,[fecha_creacion]
                      ,[numero_parte]
                      ,[descripcion]
                      ,[marca]
                      ,[unidad]
                      ,[precio_unitario] = IIF([moneda] = 'USD',  [precio_unitario] * [tipo_cambio], [precio_unitario] )
                      ,[cantidad]
                      ,[precio_total] = IIF([moneda] = 'USD',  [precio_total] *  [tipo_cambio], [precio_total] ) 
                   -- ,[stock1]
                   -- ,[stock1_fecha]
                   -- ,[stock2]
                   -- ,[stock2_fecha]
                  FROM [carrito_productos] WHERE activo = '1' AND stock1 > 0
		";

            // Depende la moneda seleccionada son los querys que se usaran para crear la cotización en USD o MXN
            if (monedaPedido == "USD")
            {
                subtotal = subtotalUSD;
                carrito = carritotToUSD;
            }
            else if (monedaPedido == "MXN")
            {
                subtotal = subtotalMXN;
                carrito = carritotToMXN;
            }

            dbConexion();
            using (con)
            {
                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English;");
                query.Append("DECLARE @subtotal money; SET @subtotal =  (" + subtotal + " WHERE usuario = @usuario );");
                query.Append("DECLARE @impuestos money; SET @impuestos =  (@subtotal + @envio) * (@valorImpuestos / 100);");
                query.Append("DECLARE @total money; SET @total =  (@subtotal + @impuestos + (@envio * (@valorImpuestos / 100)));");

                query.Append("INSERT INTO pedidos_datosNumericos  ");
                query.Append(" (numero_operacion, monedaPedido, tipo_cambio, fecha_tipo_cambio, subtotal, envio, ");
                query.Append(" metodoEnvio, monedaEnvio, impuestos, nombreImpuestos, total) ");

                query.Append(" VALUES (@numero_operacion, @monedaPedido, @tipo_cambio, @fecha_tipo_cambio, @subtotal, @envio, ");
                query.Append(" @metodoEnvio, @monedaEnvio, @impuestos, @nombreImpuestos, @total);");

                query.Append(@" INSERT INTO pedidos_productos (
                                   numero_operacion, usuario, tipo, fecha_creacion, numero_parte, descripcion, marca,
                                   unidad, precio_unitario, cantidad, precio_total
                                ) 

                                " + carrito + " AND usuario = @usuario");

                cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 60);
                cmd.Parameters["@usuario"].Value = usuario.email;

                cmd.Parameters.Add("@valorImpuestos", SqlDbType.Money);
                cmd.Parameters["@valorImpuestos"].Value = impuestos.valor;

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@monedaPedido", SqlDbType.NVarChar, 5);
                cmd.Parameters["@monedaPedido"].Value = monedaPedido;

                cmd.Parameters.Add("@tipo_cambio", SqlDbType.Money);
                cmd.Parameters["@tipo_cambio"].Value = tipoDeCambio;

                cmd.Parameters.Add("@fecha_tipo_cambio", SqlDbType.DateTime);
                cmd.Parameters["@fecha_tipo_cambio"].Value = fechaTipoDeCambio;

                cmd.Parameters.Add("@envio", SqlDbType.Money);
                cmd.Parameters["@envio"].Value = envio.costoEnvio;

                cmd.Parameters.Add("@metodoEnvio", SqlDbType.NVarChar, 15);
                cmd.Parameters["@metodoEnvio"].Value = envio.nombre;

                cmd.Parameters.Add("@monedaEnvio", SqlDbType.NVarChar, 5);
                cmd.Parameters["@monedaEnvio"].Value = envio.monedaEnvio;

                cmd.Parameters.Add("@nombreImpuestos", SqlDbType.NVarChar, 20);
                cmd.Parameters["@nombreImpuestos"].Value = impuestos.nombre;

                cmd.CommandText = query.ToString();

                cmd.CommandType = CommandType.Text;

                con.Open();
                try { cmd.ExecuteNonQuery(); return ""; }
                catch (Exception ex)
                {
                    devNotificaciones.error("Error al insertar productos y totales de carrito a pedido ", ex);
                    return null;
                }
            }
        }
    }
    /// <summary>
    /// Actualiza el % de descuento en la tabla [pedidos_datosNumericos] [descuento] en valor decimal, ej: "5" para un 5%.
    /// </summary>
    // Creada 20200103 |  Carlos
    static public bool establecerDescuento(string numero_operacion, decimal descuento_porcentaje)
    {
        string query = @"UPDATE pedidos_datosNumericos SET descuento_porcentaje = @descuento_porcentaje  
                                WHERE numero_operacion = @numero_operacion";

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con)
        {

            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
            cmd.Parameters["@numero_operacion"].Value = numero_operacion;

            cmd.Parameters.Add("@descuento_porcentaje", SqlDbType.Decimal);

            if (descuento_porcentaje == 0) cmd.Parameters["@descuento_porcentaje"].Value = DBNull.Value;
            else cmd.Parameters["@descuento_porcentaje"].Value = descuento_porcentaje;


            con.Open();

            try
            {
                cmd.ExecuteNonQuery();

                actualizarTotalStatic(numero_operacion);
                return true;
            }
            catch (Exception ex)
            {
                devNotificaciones.error("Error al aplicar descuento a pedido:" + numero_operacion, ex);
                return false;
            }
        }
    }
    /// <summary>
    /// Actualiza los precios de los productos y totales, devuelve el resultado de la operación y los productos que no hayan sido posibles actualizar.
    /// </summary>
    /// Creada 20200309 |  Carlos
    static public Tuple<bool, List<string>> renovarPedido(string numero_operacion)
    {
        /* Se requiere actualizar la fecha de creación (sobre-escribe la inicial) y se guarda un registo en [pedidos_modificaciones]
         */
        // Lista que usaremos para almacenar los productos que tengan error al actualizarce.

        List<string> productosNOActualizados = new List<string>();
        bool resultadoMetodo = new bool();

        // Obtenemos datos del pedido

        DataTable dtOperacion = pedidosDatos.obtenerPedidoDatosStatic(numero_operacion);
        DataTable dtProductos = obtenerProductos(numero_operacion);

        // Obtenemos la fecha original de la cotización próxima a actualizar
        DateTime fecha_creacion_anterior = DateTime.Parse(dtOperacion.Rows[0]["fecha_creacion"].ToString());

        DateTime fecha_creacion_nueva = utilidad_fechas.obtenerCentral();

        foreach (DataRow r in dtProductos.Rows)
        {

            string numero_parte = r["numero_parte"].ToString();
            string cantidad = r["cantidad"].ToString();
            string moneda = r["monedaPedido"].ToString();
            string tipo = r["tipo"].ToString();
            if (tipo == "1")
            {
                operacionesProductos actualizar = new operacionesProductos("pedido", "", numero_operacion, numero_parte, cantidad, moneda);
                actualizar.agregarProductoAsync();
                bool resultadoActualizacionProducto = actualizar.resultado_operacion;

                // Si hubo un error en el resultado agregamos dicho producto al listado
                if (resultadoActualizacionProducto == false) { productosNOActualizados.Add(numero_parte); }
            }
        }


        // Comenzamos a agregar los valores de renovación en "pedidos_datos"

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        using (con)
        {

            StringBuilder query = new StringBuilder();

            // Actualizamos la cotización
            query.Append("SET LANGUAGE English; " +
                         "UPDATE pedidos_datos ");
            query.Append(" SET fecha_creacion=@fecha_creacion_nueva  WHERE numero_operacion= @numero_operacion; ");

            query.Append(" UPDATE pedidos_datosNumericos SET fecha_tipo_cambio=@fecha_creacion_nueva, tipo_cambio= @tipo_cambio  WHERE numero_operacion= @numero_operacion");
            // Insertamos el registro en historial de renovaciones
            query.Append(" INSERT INTO pedidos_modificaciones (numero_operacion, descripcion, fecha_modificacion,  modificada_por ) ");
            query.Append("                               VALUES (@numero_operacion, @descripcion, @fecha_modificacion, @modificada_por);");


            cmd.CommandText = query.ToString();
            cmd.CommandType = CommandType.Text;


            cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
            cmd.Parameters["@numero_operacion"].Value = numero_operacion;


            cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar);
            cmd.Parameters["@descripcion"].Value = "Renovación";


            cmd.Parameters.Add("@fecha_modificacion", SqlDbType.DateTime);
            cmd.Parameters["@fecha_modificacion"].Value = fecha_creacion_nueva;

            cmd.Parameters.Add("@fecha_creacion_nueva", SqlDbType.DateTime);
            cmd.Parameters["@fecha_creacion_nueva"].Value = fecha_creacion_nueva;



            cmd.Parameters.Add("@tipo_cambio", SqlDbType.Decimal);
            cmd.Parameters["@tipo_cambio"].Value = operacionesConfiguraciones.obtenerTipoDeCambio();


            cmd.Parameters.Add("@modificada_por", SqlDbType.NVarChar, 60);
            cmd.Parameters["@modificada_por"].Value = HttpContext.Current.User.Identity.Name;

            con.Open();
            try
            {
                cmd.ExecuteNonQuery();
                resultadoMetodo = true;
            }
            catch (Exception ex)
            {
                devNotificaciones.error(string.Format("Renovar pedido: {0} ", numero_operacion), ex);
                resultadoMetodo = false;
            }

        }

        return Tuple.Create(resultadoMetodo, productosNOActualizados);
    }
    /// <summary>
    /// 20201105 Obtiene el número de parte y sus dimensiones y propiedades para el envio.
    /// </summary>
    static public dynamic ObtenerProductosCalculoEnvio(string numero_operacion)
    {
        using (var db = new tiendaEntities())
        {
            var ProductosEnvio = db.pedidos_productos
            .Join(db.productos_Datos,
               pedido_producto => pedido_producto.numero_parte,
               dat_producto => dat_producto.numero_parte,
              (pedido_producto, dat_producto) => new ProductoEnvioCalculoModel
              {
                  Numero_Operacion = pedido_producto.numero_operacion,
                  Numero_Parte = pedido_producto.numero_parte,
                  Tipo = pedido_producto.tipo,
                  Cantidad = pedido_producto.cantidad,
                  PesoKg = dat_producto.peso,
                  Largo = dat_producto.profundidad,
                  Ancho = dat_producto.ancho,
                  Alto = dat_producto.alto,

                  RotacionVertical = dat_producto.RotacionVertical,
                  RotacionHorizontal = dat_producto.RotacionHorizontal,
                  DisponibleParaEnvioGratuito = dat_producto.disponibleEnvio


              })

       .Where(p => p.Numero_Operacion == numero_operacion).OrderByDescending(p => p.PesoKg)
       .ToList();

            // tipo =2 = personalizado
            var ProductosPersonalizado = db.pedidos_productos
                .Where(p => p.tipo == 2 && p.numero_operacion == numero_operacion)
                 .Select(c => new ProductoEnvioCalculoModel()
                 {
                     Numero_Operacion = c.numero_operacion,
                     Numero_Parte = c.numero_parte,
                     Tipo = c.tipo,
                     Cantidad = c.cantidad,
                     PesoKg = null,
                     Largo = null,
                     Ancho = null,
                     Alto = null,
                     RotacionVertical = null,
                     RotacionHorizontal = null,
                     DisponibleParaEnvioGratuito = 0
                 });


            var allProducts = ProductosEnvio.Union(ProductosPersonalizado).ToList();

            return allProducts.ToList();
        }
    }
}
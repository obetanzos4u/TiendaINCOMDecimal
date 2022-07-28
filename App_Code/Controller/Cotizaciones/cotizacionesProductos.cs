using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de cotizaciones
/// </summary>
public class cotizacionesProductos : model_cotizacionesProductos {
    private SqlConnection con { get; set; }
    private SqlCommand cmd { get; set; }
    private SqlDataAdapter da { get; set; }
    private DataSet ds { get; set; }
    private DataTable dt { get; set; }


    public string monedaCotizacion { get; set; }
    public float tipoDeCambio { get; set; }
    public DateTime fechaTipoDeCambio { get; set; }

    protected void dbConexion() {

        con = new SqlConnection(conexiones.conexionTienda());
        cmd = new SqlCommand();
        cmd.Connection = con;

    }
    public cotizacionesProductos() {
        // 
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public static DataTable obtenerProductos(string numero_operacion) {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        using (con) {

            StringBuilder sel = new StringBuilder();

            sel.Append("SELECT ");

            // Campos básicos
            sel.Append(@"

                        id,
                        numero_operacion,
                        usuario,
                        orden,
                        activo,
                        tipo ,
                        alternativo,
                        fecha_creacion,
                        numero_parte,
                        descripcion,
                        marca,
                        (SELECT top(1) monedaCotizacion from cotizaciones_datosNumericos WHERE  numero_operacion = cotizaciones_productos.numero_operacion) as  monedaCotizacion, 
                        unidad,
                        precio_unitario,
                        cantidad,
                        precio_total,
                        stock1,
                        stock1_fecha,
                        stock2,
                        stock2_fecha,
                        (SELECT top(1) titulo from productos_Datos WHERE  numero_parte = cotizaciones_productos.numero_parte) as  titulo,
                        (SELECT top(1) imagenes from productos_Datos WHERE  numero_parte = cotizaciones_productos.numero_parte) as imagenes,
                        (SELECT top(1) pdf from productos_Datos WHERE  numero_parte = cotizaciones_productos.numero_parte) as pdf ");

            sel.Append(" FROM cotizaciones_productos  WHERE numero_operacion = @numero_operacion  ORDER BY orden ASC;");

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

    public static DataTable obtenerProductosWithMedidas(string numero_operacion)
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

                        operacion.id,
                        operacion.numero_operacion,
                        operacion.usuario,
                        operacion.orden,
                        operacion.activo,
                        operacion.tipo ,
                        operacion.alternativo,
                        operacion.fecha_creacion,
                        operacion.numero_parte,
                        operacion.descripcion,
                        operacion.marca,
                        (SELECT top(1) monedaCotizacion from cotizaciones_datosNumericos WHERE  numero_operacion = operacion.numero_operacion) as  monedaCotizacion, 
                        operacion.unidad,
                        operacion.precio_unitario,
                        operacion.cantidad,
                        operacion.precio_total,
                        operacion.stock1,
                        operacion.stock1_fecha,
                        operacion.stock2,
                        operacion.stock2_fecha,

                        P_Datos.imagenes,
                        P_Datos.RotacionVertical,
                        P_Datos.RotacionHorizontal,
                        P_Datos.alto,
                        P_Datos.ancho,
                        P_Datos.profundidad,
                        P_Datos.peso,
                        P_Datos.pdf,
                        P_Datos.titulo 
                         ");



            sel.Append(" FROM cotizaciones_productos as operacion" +
               " FULL OUTER  JOIN productos_Datos P_Datos ON operacion.numero_parte = P_Datos.numero_parte " +

               "WHERE operacion.numero_operacion = @numero_operacion  ORDER BY operacion.orden ASC;");

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
    /// Obtiene los campos [id],[numero_parte],[descripcion],[cantidad] de la tabla [cotizaciones_productos] 
    /// </summary>
    public static DataTable obtenerProductosMin(string numero_operacion) {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        using (con) {

            StringBuilder sel = new StringBuilder();

            sel.Append("SELECT ");

            // Campos básicos
            sel.Append(@"
                        id, 
                        numero_parte,
                        descripcion,
                        cantidad 
                      ");

            sel.Append(" FROM cotizaciones_productos  WHERE numero_operacion = @numero_operacion ORDER BY orden ASC ;");

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
    /// Obtiene la incedencia de productos en la cotización
    /// </summary>
    public static DataTable obtenerProducto(string numero_operacion, string numero_parte) {


        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            StringBuilder sel = new StringBuilder();

            sel.Append("SELECT ");

            // Campos básicos
            sel.Append(" * ");


            sel.Append(" FROM cotizaciones_productos WHERE numero_operacion = @numero_operacion AND numero_parte = @numero_parte;");

            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
            cmd.Parameters["@numero_operacion"].Value = numero_operacion;

            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 50);
            cmd.Parameters["@numero_parte"].Value = numero_parte;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            return ds.Tables[0];
        }


    }
    public static DataTable obtenerProducto(int id) {


        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            StringBuilder sel = new StringBuilder();

            string query = " SELECT * FROM cotizaciones_productos WHERE id = @id;";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = id;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            return ds.Tables[0];
        }


    }
    /// <summary>
    /// Devuelve true si el producto existe en la operación
    /// </summary>
    public static bool verificarExistenciaProducto(string numero_operacion, string numero_parte) {


        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            StringBuilder sel = new StringBuilder();

            string query = " SELECT COUNT(numero_parte) FROM cotizaciones_productos WHERE numero_operacion = @numero_operacion AND numero_parte = @numero_parte;";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
            cmd.Parameters["@numero_operacion"].Value = numero_operacion;

            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 50);
            cmd.Parameters["@numero_parte"].Value = numero_parte;
            con.Open();
            int cantidadProducto = int.Parse(cmd.ExecuteScalar().ToString());

            if (cantidadProducto >= 1) return true;
            else return false;

        }


    }
    public void eliminarProducto(string idProductoCarrito) {
        StringBuilder query = new StringBuilder();

        query.Append("SET LANGUAGE English; DELETE FROM cotizaciones_productos   WHERE id=@id");

        dbConexion();
        using (con) {

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
    public bool actualizarTotal(string numero_operacion) {

        string total = @"DECLARE @totalProductos money;
                            -- Obtenemos el total de los productos de la cotización y que estos sean activos
                            SET @totalProductos =  (SELECT sum(precio_total) FROM cotizaciones_productos WHERE numero_operacion = @numero_operacion AND activo = 1);
                                
                              IF (@totalProductos IS NULL) BEGIN 
	                                 SET @totalProductos  = 0;
	                          END
                                DECLARE @envio money;
                                SET @envio = (SELECT sum(envio) FROM cotizaciones_datosNumericos WHERE numero_operacion = @numero_operacion);

                                DECLARE @subtotal money;
                                SET @subtotal = IIF(@envio IS NULL, @totalProductos + 0, @totalProductos + @envio);

                                

                                DECLARE @descuento_porcentaje money;
                                SET @descuento_porcentaje = (SELECT descuento_porcentaje FROM cotizaciones_datosNumericos WHERE numero_operacion = @numero_operacion);


                                 DECLARE @descuento money;
                                 SET @descuento = IIF(@descuento_porcentaje IS NULL, NULL, ROUND((@subtotal * @descuento_porcentaje)/100, 2) );

                                -- Aplicando el descuento
                                SET @subtotal = IIF(@descuento IS NULL, @subtotal, ROUND(@subtotal - @descuento, 2) );

                                DECLARE @impuestos money;
                                SET @impuestos = @subtotal * 0.16;


                                DECLARE @total money;
                                SET @total = @subtotal + @impuestos;


                                UPDATE cotizaciones_datosNumericos
                                SET subtotal = @subtotal, impuestos = @impuestos, total = @total
                                ,descuento = @descuento,  descuento_porcentaje = @descuento_porcentaje
                                WHERE numero_operacion = @numero_operacion;
        ";



        dbConexion();
        using (con) {

            cmd.CommandText = total;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
            cmd.Parameters["@numero_operacion"].Value = numero_operacion;

            con.Open();

            try {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) {
                devNotificaciones.error("Error al actualizar totales de cotización:" + numero_operacion, ex);
                return false;
            }

        }

    }
    /// <summary>
    /// Suma productos, envío y aplica el iva
    /// </summary>
    static public bool actualizarTotalStatic(string numero_operacion) {

        string total = @"
                            DECLARE @totalProductos money;
                            -- Obtenemos el total de los productos de la cotización y que estos sean activos
                            SET @totalProductos =  (SELECT sum(precio_total) FROM cotizaciones_productos WHERE numero_operacion = @numero_operacion AND activo = 1);
                                
                              IF (@totalProductos IS NULL) BEGIN 
	                                 SET @totalProductos  = 0;
	                          END
                                DECLARE @envio money;
                                SET @envio = (SELECT sum(envio) FROM cotizaciones_datosNumericos WHERE numero_operacion = @numero_operacion);

                                DECLARE @subtotal money;
                                SET @subtotal = IIF(@envio IS NULL, @totalProductos + 0, @totalProductos + @envio);

                            
                                DECLARE @descuento_porcentaje money;
                                SET @descuento_porcentaje = (SELECT descuento_porcentaje FROM cotizaciones_datosNumericos WHERE numero_operacion = @numero_operacion);


                                 DECLARE @descuento money;
                                 SET @descuento = IIF(@descuento_porcentaje IS NULL, NULL, ROUND((@subtotal * @descuento_porcentaje)/100, 2) );

                                -- Aplicando el descuento
                                SET @subtotal = IIF(@descuento IS NULL, @subtotal, ROUND(@subtotal - @descuento, 2) );

                                DECLARE @impuestos money;
                                SET @impuestos = @subtotal * 0.16;


                                DECLARE @total money;
                                SET @total = @subtotal + @impuestos;


                                UPDATE cotizaciones_datosNumericos
                                SET subtotal = ROUND(@subtotal, 2), impuestos = ROUND(@impuestos, 2), total = ROUND(@total, 2)
                                ,descuento = @descuento,  descuento_porcentaje = @descuento_porcentaje
                                WHERE numero_operacion = @numero_operacion;
        ";



        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            cmd.CommandText = total;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
            cmd.Parameters["@numero_operacion"].Value = numero_operacion;

            con.Open();

            try {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) {
                devNotificaciones.error("Error al actualizar totales de cotización:" + numero_operacion, ex);
                return false;
            }

        }

    }
    public bool actualizarCantidadProducto(string numero_operacion, model_cotizacionesProductos producto) {

        StringBuilder query = new StringBuilder();
        query.Append("SET LANGUAGE English; UPDATE  cotizaciones_productos  SET ");
        query.Append(" precio_unitario = @precio_unitario, ");
        query.Append(" cantidad = @cantidad , precio_total = @precio_total ");


        dbConexion();

        using (con) {

            if (producto.tipo == 1) {

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

            // Es importante mantener este orden al final
            query.Append(" WHERE numero_operacion = @numero_operacion AND numero_parte = @numero_parte; ");



            cmd.Parameters.Add("@precio_unitario", SqlDbType.Money);
            cmd.Parameters["@precio_unitario"].Value = producto.precio_unitario;

            cmd.Parameters.Add("@cantidad", SqlDbType.Money);
            cmd.Parameters["@cantidad"].Value = producto.cantidad;

            cmd.Parameters.Add("@precio_total", SqlDbType.Money);
            cmd.Parameters["@precio_total"].Value = producto.precio_total;


            cmd.CommandText = query.ToString();
            cmd.CommandType = CommandType.Text;



            con.Open();
            try {
                int resultado = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) {
                devNotificaciones.error("Actualizar cantidad de producto en cotización en operacion: " + numero_operacion, ex);
                return false;
            }


        }



    }
    /// <summary>
    /// Actualiza el ampo para establecer si es alternativo. [0] NO es alternativo, [1] SI es un producto alternativo o sugerencia. Campo informativo
    /// </summary>
    public static bool actualizarProductoAlternativo(string numero_operacion, int id, bool _alternativo) {
        int alternativo = 0;

        if (_alternativo) { alternativo = 1; }

        StringBuilder query = new StringBuilder();
        query.Append("SET LANGUAGE English; UPDATE  cotizaciones_productos  SET ");
        query.Append("alternativo=@alternativo ");
        query.Append("WHERE numero_operacion = @numero_operacion AND id = @id; ");

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {
            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = id;

            cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 50);
            cmd.Parameters["@numero_operacion"].Value = numero_operacion;


            cmd.Parameters.Add("@alternativo", SqlDbType.Int);
            cmd.Parameters["@alternativo"].Value = alternativo;

            cmd.CommandText = query.ToString();
            cmd.CommandType = CommandType.Text;

            con.Open();
            try {
                int resultado = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) {
                devNotificaciones.error("Actualizar producto cotizacion, valor alternativo: " + numero_operacion, ex);
                return false;
            }


        }



    }
    public static bool actualizarCantidadyDatosProductoStatic(string numero_operacion, model_cotizacionesProductos producto) {

        StringBuilder query = new StringBuilder();
        query.Append("SET LANGUAGE English; UPDATE  cotizaciones_productos  SET ");
        query.Append(" numero_parte=@numero_parte, precio_unitario = @precio_unitario, ");
        query.Append(" cantidad = @cantidad , precio_total = @precio_total ");
        query.Append(" ,descripcion = @descripcion , marca = @marca, unidad = @unidad ");

        query.Append(" WHERE numero_operacion = @numero_operacion AND id = @id; ");

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {


            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = producto.id;

            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 50);
            cmd.Parameters["@numero_parte"].Value = producto.numero_parte;


            cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar, 500);
            cmd.Parameters["@descripcion"].Value = producto.descripcion;

            cmd.Parameters.Add("@marca", SqlDbType.NVarChar, 50);
            cmd.Parameters["@marca"].Value = producto.marca;

            cmd.Parameters.Add("@unidad", SqlDbType.NVarChar, 25);
            cmd.Parameters["@unidad"].Value = producto.unidad;


            cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
            cmd.Parameters["@numero_operacion"].Value = numero_operacion;


            cmd.Parameters.Add("@precio_unitario", SqlDbType.Money);
            cmd.Parameters["@precio_unitario"].Value = producto.precio_unitario;

            cmd.Parameters.Add("@cantidad", SqlDbType.Money);
            cmd.Parameters["@cantidad"].Value = producto.cantidad;

            cmd.Parameters.Add("@precio_total", SqlDbType.Money);
            cmd.Parameters["@precio_total"].Value = producto.precio_total;


            cmd.CommandText = query.ToString();
            cmd.CommandType = CommandType.Text;



            con.Open();
            try {
                int resultado = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) {
                devNotificaciones.error("Actualizar cantidad y datos de producto en cotización en operacion: " + numero_operacion, ex);
                return false;
            }


        }



    }
    public static bool actualizarCantidadProductoStatic(string numero_operacion, model_cotizacionesProductos producto) {

        StringBuilder query = new StringBuilder();
        query.Append("SET LANGUAGE English; UPDATE  cotizaciones_productos  SET ");
        query.Append(" precio_unitario = @precio_unitario, ");
        query.Append(" cantidad = @cantidad , precio_total = @precio_total ");
        query.Append(" ,descripcion = @descripcion , marca = @marca, unidad = @unidad ");
        // Es importante mantener este orden al final
        query.Append(" WHERE numero_operacion = @numero_operacion AND numero_parte = @numero_parte; ");


        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {



            cmd.Parameters.Add("@descripcion", SqlDbType.NVarChar, 500);
            cmd.Parameters["@descripcion"].Value = producto.descripcion;

            cmd.Parameters.Add("@marca", SqlDbType.NVarChar, 50);
            cmd.Parameters["@marca"].Value = producto.marca;

            cmd.Parameters.Add("@unidad", SqlDbType.NVarChar, 25);
            cmd.Parameters["@unidad"].Value = producto.unidad;



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


            cmd.CommandText = query.ToString();
            cmd.CommandType = CommandType.Text;



            con.Open();
            try {
                int resultado = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) {
                devNotificaciones.error("Actualizar cantidad de producto en cotización en operacion: " + numero_operacion, ex);
                return false;
            }


        }



    }
    /// <summary>
    ///Agregar producto a una operación, si se agrega correctamente devuelve true
    /// </summary>
    static public bool agregarProducto(string numero_operacion, model_cotizacionesProductos producto) {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        try {
            using (con) {

                StringBuilder sel = new StringBuilder();

                sel.Append("INSERT INTO cotizaciones_productos");

                // Campos básicos
                sel.Append(@" (
                           [numero_operacion]
                          ,[usuario]
                          ,[orden]
                          ,[activo]
                          ,[tipo]
                          ,[fecha_creacion]
                          ,[numero_parte]
                          ,[descripcion]
                          ,[marca]
                          ,[unidad]
                          ,[precio_unitario]
                          ,[cantidad]
                          ,[precio_total]
                         -- ,[stock1]
                         -- ,[stock1_fecha]
                         -- ,[stock2]
                        --  ,[stock2_fecha]

                        )  ");

                sel.Append(@"
                            VALUES ( 
                           @numero_operacion
                          ,@usuario
                          ,@orden
                          ,@activo
                          ,@tipo
                          ,@fecha_creacion
                          ,@numero_parte
                          ,@descripcion
                          ,@marca
                          ,@unidad
                          ,@precio_unitario
                          ,@cantidad
                          ,@precio_total
                        --  ,@stock1
                        --  ,@stock1_fecha
                        -- ,@stock2
                        -- ,@stock2_fecha

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

                cmd.Parameters.Add("@activo", SqlDbType.Int);
                cmd.Parameters["@activo"].Value = producto.activo;

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

                /*      cmd.Parameters.Add("@stock1", SqlDbType.Float);
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
        catch (Exception ex) {

            devNotificaciones.error("Agregar producto a la cotizacion número: " + numero_operacion, ex);
            return false;
        }
    }

    /// <summary>
    /// Valida que la operación sea en MXN,  Actualiza una operación a USD diviendo los valores entre el tipo de cambío
    /// ES IMPORTANTE ACTUALIZAR LOS TOTALES DESPUÉS DE LLAMAR ESTE MÉTODO
    /// </summary>
    public static bool convertirProductosMonedaToUSD(string numero_operacion, decimal tipoDeCambio) {

        string moneda = cotizaciones.obtenerMonedaOperacion(numero_operacion);
        if (moneda == "MXN") {
            StringBuilder query = new StringBuilder();
            query.Append("SET LANGUAGE English; UPDATE  cotizaciones_productos  SET ");
            query.Append("precio_unitario = ROUND(precio_unitario,2) / @tipoDeCambio, ");
            query.Append("precio_total = ROUND(precio_total,2)  / @tipoDeCambio  ");

            query.Append(" WHERE numero_operacion = @numero_operacion;");


            // 2 SEGUNDO QUERY - Convertir el envio únicamente
            query.Append("SET LANGUAGE English; UPDATE  cotizaciones_datosNumericos  SET ");
            query.Append("tipo_cambio = @tipoDeCambio, fecha_tipo_cambio = @fecha_tipo_cambio,  ");
            query.Append("envio = ROUND(envio,2) / @tipoDeCambio, monedaEnvio = 'USD',  ");
            query.Append(" monedaCotizacion = 'USD' ");

            query.Append(" WHERE numero_operacion = @numero_operacion; ");

            // 3 TERCERO QUERY - Modificar la vigencia
            query.Append("  UPDATE  cotizaciones_datos SET vigencia = 30  ");
            query.Append(" WHERE numero_operacion = @numero_operacion  ");

            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;

            using (con) {

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@tipoDeCambio", SqlDbType.Money);
                cmd.Parameters["@tipoDeCambio"].Value = tipoDeCambio;

                cmd.Parameters.Add("@fecha_tipo_cambio", SqlDbType.DateTime);
                cmd.Parameters["@fecha_tipo_cambio"].Value = utilidad_fechas.obtenerCentral();

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                con.Open();

                try {
                    int resultado = cmd.ExecuteNonQuery();

                    return true;
                }
                catch (Exception ex) {
                    devNotificaciones.error("Re calcular moneda a USD en productos: " + numero_operacion, ex);
                    return false;
                }

            }
        } else {
            return false;
        }


    }

    /// <summary>
    /// Valida que la operación sea en USD,  Actualiza una operación a USD diviendo los valores entre el tipo de cambío
    /// ES IMPORTANTE ACTUALIZAR LOS TOTALES DESPUÉS DE LLAMAR ESTE MÉTODO
    /// </summary>
    public static bool convertirProductosMonedaToMXN(string numero_operacion, decimal tipoDeCambio) {

        string moneda = cotizaciones.obtenerMonedaOperacion(numero_operacion);
        if (moneda == "USD") {
            StringBuilder query = new StringBuilder();
            query.Append("SET LANGUAGE English; UPDATE  cotizaciones_productos  SET ");
            query.Append("precio_unitario = ROUND(precio_unitario, 2) * @tipoDeCambio, ");
            query.Append("precio_total = ROUND(precio_total, 2)  * @tipoDeCambio  ");

            query.Append(" WHERE numero_operacion = @numero_operacion;");


            // 2 SEGUNDO QUERY - Convertir el envio únicamente
            query.Append(" UPDATE  cotizaciones_datosNumericos  SET ");
            query.Append("tipo_cambio = @tipoDeCambio, fecha_tipo_cambio = @fecha_tipo_cambio,  ");
            query.Append("envio = ROUND(envio,2) * @tipoDeCambio, monedaEnvio = 'MXN',  ");
            query.Append(" monedaCotizacion = 'MXN' ");
            query.Append(" WHERE numero_operacion = @numero_operacion;  ");


            // 3 TERCERO QUERY - Modificar la vigencia
            query.Append("  UPDATE  cotizaciones_datos SET vigencia = 1  ");
            query.Append(" WHERE numero_operacion = @numero_operacion  ");

            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;

            using (con) {



                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@tipoDeCambio", SqlDbType.Money);
                cmd.Parameters["@tipoDeCambio"].Value = tipoDeCambio;

                cmd.Parameters.Add("@fecha_tipo_cambio", SqlDbType.DateTime);
                cmd.Parameters["@fecha_tipo_cambio"].Value = utilidad_fechas.obtenerCentral();

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                con.Open();

                try {
                    int resultado = cmd.ExecuteNonQuery();

                    return true;
                }
                catch (Exception ex) {
                    devNotificaciones.error("Re calcular moneda a USD en productos: " + numero_operacion, ex);
                    return false;
                }

            }
        } else {
            return false;
        }


    }

    static public Tuple<bool, List<string>> agregarProductosQuick(string numero_operacion, string monedaOperacion, string productosQuickAdd) {


        List<string> productosNOActualizados = new List<string>();
        bool resultadoMetodo = new bool();


        string[] productoLinea = productosQuickAdd.Split('\n');

        try {
            foreach (string producto in productoLinea) {

                string[] datosProducto = producto.Split(' ');
                string numero_parte = datosProducto[0];
                if (!string.IsNullOrWhiteSpace(numero_parte)) {
                    string cantidad = datosProducto[1];


                    operacionesProductos actualizar = new operacionesProductos("cotizacion", "", numero_operacion, numero_parte, cantidad, monedaOperacion);
                    actualizar.agregarProductoAsync();
                    bool resultadoActualizacionProducto = actualizar.resultado_operacion;

                    // Si hubo un error en el resultado agregamos dicho producto al listado
                    if (resultadoActualizacionProducto == false) { productosNOActualizados.Add(numero_parte); }
                }
            }
            resultadoMetodo = true;
        } catch (Exception ex) {
            devNotificaciones.error("Error al agregar productos Quick", ex);
            resultadoMetodo = false;
        }

        return Tuple.Create(resultadoMetodo, productosNOActualizados);
    }
    /// <summary>
    /// Actualiza el orden del producto alterando los demás
    /// </summary>
    static public bool actualizarOrdenProducto(string numero_operacion, string numero_parte, int posicion) {

        string query = @" 
-- Resetear
-- UPDATE cotizaciones_productos  SET orden=replace(id,' ', '');


                            DECLARE  @ordenAnterior INT;
                              SET @ordenAnterior =  (SELECT TOP (1) orden FROM cotizaciones_productos 
                               WHERE   numero_operacion = @numero_operacion AND numero_parte = @numero_parte);
                               
                                UPDATE cotizaciones_productos
                                 SET orden = @posicion  
                                WHERE numero_operacion = @numero_operacion AND numero_parte=@numero_parte;


                                IF (@posicion > @ordenAnterior) BEGIN
                                 UPDATE cotizaciones_productos SET orden = (orden - 1)
                                WHERE numero_operacion = @numero_operacion AND orden > @posicion; 
                                END

                                IF (@posicion < @ordenAnterior) BEGIN
                                  UPDATE cotizaciones_productos SET orden = (orden + 1)
                                WHERE numero_operacion = @numero_operacion  AND orden < @posicion ; 
                                 END
                           
                              

                               ";

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
            cmd.Parameters["@numero_operacion"].Value = numero_operacion;

            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 50);
            cmd.Parameters["@numero_parte"].Value = numero_parte;

            cmd.Parameters.Add("@posicion", SqlDbType.Int);
            cmd.Parameters["@posicion"].Value = posicion;
            con.Open();

            try {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) {
                devNotificaciones.error("Error al actualizar totales de cotización:" + numero_operacion, ex);
                return false;
            }

        }

    }
    /// <summary>
    /// Actualiza el campo orden del producto dado sin alterar ningún otro
    /// </summary>
    static public bool actualizarOrdenProductoUnicamente(string numero_operacion, string numero_parte, int posicion) {

        string query = @"UPDATE cotizaciones_productos SET orden = @posicion  
                                WHERE numero_operacion = @numero_operacion AND numero_parte=@numero_parte;";

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
            cmd.Parameters["@numero_operacion"].Value = numero_operacion;

            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 50);
            cmd.Parameters["@numero_parte"].Value = numero_parte;

            cmd.Parameters.Add("@posicion", SqlDbType.Int);
            cmd.Parameters["@posicion"].Value = posicion;
            con.Open();

            try {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) {
                devNotificaciones.error("Error al actualizar el orden de un producto:" + numero_operacion, ex);
                return false;
            }

        }

    }


    public static int obtenerCantidadProductos(string numero_operacion) {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        using (con) {

            StringBuilder sel = new StringBuilder();

            sel.Append(@"
                SELECT COUNT(*) FROM cotizaciones_productos 
	                 WHERE numero_operacion = @numero_operacion ");



            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
            cmd.Parameters["@numero_operacion"].Value = numero_operacion;



            con.Open();

            return int.Parse(cmd.ExecuteScalar().ToString());
        }


    }

    /// <summary>
    /// Actualiza el % de descuento en la tabla [cotizaciones_datosNumericos] [descuento] en valor decimal, ej: "5" para un 5%.
    /// </summary>
    // 20191217|  Carlos
    static public bool establecerDescuento(string numero_operacion, decimal descuento_porcentaje) {

        string query = @"UPDATE cotizaciones_datosNumericos SET descuento_porcentaje = @descuento_porcentaje  
                                WHERE numero_operacion = @numero_operacion";

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
            cmd.Parameters["@numero_operacion"].Value = numero_operacion;

            cmd.Parameters.Add("@descuento_porcentaje", SqlDbType.Decimal);

            if (descuento_porcentaje == 0) cmd.Parameters["@descuento_porcentaje"].Value = DBNull.Value;
            else cmd.Parameters["@descuento_porcentaje"].Value = descuento_porcentaje;


            con.Open();

            try {
                cmd.ExecuteNonQuery();

                actualizarTotalStatic(numero_operacion);
                return true;
            }
            catch (Exception ex) {
                devNotificaciones.error("Error al aplicar descuento:" + numero_operacion, ex);
                return false;
            }

        }

    }

    /// <summary>
    /// 20201105 Obtiene el número de parte y sus dimensiones y propiedades para el envio.
    /// </summary>
    static public dynamic ObtenerProductosCalculoEnvio(string numero_operacion) {

        try {
            using (var db = new tiendaEntities()) {
                var ProductosEnvio = db.cotizaciones_productos
                .Join(db.productos_Datos,
                   cot_producto => cot_producto.numero_parte,
                   dat_producto => dat_producto.numero_parte,
                  (cot_producto, dat_producto) => new ProductoEnvioCalculoModel {
                      Numero_Operacion = cot_producto.numero_operacion,
                      Numero_Parte = cot_producto.numero_parte,
                      Tipo = cot_producto.tipo,
                      Cantidad = cot_producto.cantidad,
                      PesoKg = dat_producto.peso,
                      Largo = dat_producto.profundidad,
                      Ancho = dat_producto.ancho,
                      Alto = dat_producto.alto,

                      RotacionVertical = dat_producto.RotacionVertical,
                      RotacionHorizontal = dat_producto.RotacionHorizontal,
                       DisponibleParaEnvioGratuito =  dat_producto.disponibleEnvio 
              })

           .Where(p => p.Numero_Operacion == numero_operacion).OrderByDescending(p => p.PesoKg)
           .ToList();

                // tipo =2 = personalizado
                var ProductosPersonalizado = db.cotizaciones_productos
                    .Where(p => p.tipo == 2 && p.numero_operacion == numero_operacion)
                     .Select(c => new ProductoEnvioCalculoModel() {
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
                         DisponibleParaEnvioGratuito = null
                     });


                var allProducts = ProductosEnvio.Union(ProductosPersonalizado).ToList();

                return allProducts.ToList();
            }
        }
        catch (Exception ex) {
            devNotificaciones.error("Error al obtener productos", ex);
            return null;
        }
    }

}

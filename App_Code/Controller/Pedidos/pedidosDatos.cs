using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Descripción breve de pedidos
/// </summary>
public class pedidosDatos
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
    public pedidosDatos()
    {
        // 
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    /// <summary>
    /// 1/3 Crea el pedido y de vuelve un string con el número de operación, también vacía el carrito si esta fue creada con éxito.
    /// </summary>
    /// 
    public string crearPedidoDeCarrito(usuarios usuario, model_impuestos impuestos, model_pedidos_datos pedidoDatos)
    {
        try
        {
            if (carrito.obtenerCantidadProductos(usuario.email) < 1) return null;

            pedidosProductos crear = new pedidosProductos();
            crear.monedaPedido = monedaPedido;
            crear.fechaTipoDeCambio = fechaTipoDeCambio;
            crear.tipoDeCambio = tipoDeCambio;

            model_envios envio = new model_envios();

            string numero_operacion = crearPedido_datos(usuario, pedidoDatos, "pd");
            var productos = crear.crearPedidoDeCarrito_productos(usuario, numero_operacion, impuestos, envio);

            if (numero_operacion != null && productos != null)
            {

                carrito eliminar = new carrito();

                eliminar.eliminarCarrito(usuario.email);

                return numero_operacion;

            }
            else
            {

                borrarPermanentemente(numero_operacion);

                return null;
            }

        }
        catch (Exception ex)
        {
            devNotificaciones.error("Error al insertar productos y totales de carrito a pedido ", ex);
            return null;
        }


    }
    /// <summary>
    /// Crea el pedido y de vuelve un string con el número de operación, también vacía el carrito si esta fue creada con éxito.
    /// </summary>
    /// 
    public string crearPedidoDeCotizacionAsync(usuarios usuario, int idSQLCotizacion, string nombre_pedido)
    {

        pedidosProductos pedidoProductos = new pedidosProductos();
        pedidoProductos.monedaPedido = monedaPedido;
        pedidoProductos.fechaTipoDeCambio = fechaTipoDeCambio;
        pedidoProductos.tipoDeCambio = tipoDeCambio;

        model_envios envio = new model_envios();


        cotizaciones obtener = new cotizaciones();
        // Obtenemos la cotización que se usara para crear el pedido.
        DataTable dt_datosCotizacion = obtener.obtenerCotizacionDatosMax(idSQLCotizacion);
        string numero_operacion_cotizacion = dt_datosCotizacion.Rows[0]["numero_operacion"].ToString();

        string idOrigen = numero_operacion_cotizacion.Substring(0, 2);

        if (idOrigen.Contains("ca")) idOrigen = "pcc"; else idOrigen = "pc";

        string numero_operacion = crearPedido_datosFromCotizacion(usuario, dt_datosCotizacion, nombre_pedido, idOrigen);

        var productos = pedidoProductos.crearPedidoDeCotizacion_productos(numero_operacion, numero_operacion_cotizacion);
        var datosNumericos = crearPedidoDeCotizacion_datosNumericos(numero_operacion, numero_operacion_cotizacion);

        var direccionFacturacion = clone_direccionEnvioFacturacion_ToPedido(usuario, numero_operacion, numero_operacion_cotizacion);
        var direccionEnvio = clone_direccionEnvioCotizacion_ToPedido(usuario, numero_operacion, numero_operacion_cotizacion);


        if (numero_operacion != null && productos != null && datosNumericos != null)
        {

            carrito carrito = new carrito();

            carrito.eliminarCarrito(usuario.email);

            return numero_operacion;

        }
        else
        {

            borrarPermanentemente(numero_operacion);

            return null;
        }

    }
    /// <summary>
    /// Elimina la información asociada en las tablas: pedidos_productos,  pedidos_datosNumericos, pedidos_datos
    /// </summary>
    /// 
    public void borrarPermanentemente(string numero_operacion)
    {

        try
        {
            dbConexion();

            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English;");
                query.Append("DELETE FROM pedidos_productos WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM pedidos_datosNumericos WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM pedidos_datos WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM pedidos_direccionFacturacion WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM pedidos_direccionEnvio WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM pedidos_modificaciones WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM pedidos_productos_modificaciones WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM pedidos_asesorBase WHERE numero_operacion = @numero_operacion;");


                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;
                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                con.Open();

                cmd.ExecuteNonQuery();


            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Borrar permanentemente un pedido: " + numero_operacion, ex);

        }
    }

    public DataTable obtenerPedidosUsuario_min(string usuario_cliente)
    {
        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT
                                datos.id,
                                datos.nombre_pedido,
                                datos.numero_operacion,
                                (SELECT COUNT(*)  FROM pedidos_productos WHERE  numero_operacion =  datos.numero_operacion) as productosTotales,
                                datos.id_cliente,
                                datos.fecha_creacion,
                                datos.creada_por,
                                datos.OperacionCancelada,
                                datosNum.monedaPedido,
                                datosNum.subtotal,
                                datosNum.envio,
                                datosNum.impuestos,
                                datosNum.total
                                  FROM pedidos_datos as datos
                                  INNER JOIN  pedidos_datosNumericos as datosNum
                                  ON datos.numero_operacion = datosNum.numero_operacion
                                  WHERE datos.usuario_cliente = @usuario_cliente ORDER BY fecha_creacion DESC
                                ");

                cmd.Parameters.Add("@usuario_cliente", SqlDbType.NVarChar, 60);
                cmd.Parameters["@usuario_cliente"].Value = usuario_cliente;
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

            devNotificaciones.error("Obtener pedidos de un usuario: " + usuario_cliente, ex);

            return null;
        }
    }
    static public int obtenerIdSQLPedido(string numero_operacion)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;
            using (con)
            {

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.CommandText = @"SELECT id, nombre_pedido 
                                  FROM pedidos_datos WHERE numero_operacion = @numero_operacion
                                ";
                cmd.CommandType = CommandType.Text;

                con.Open();


                return int.Parse(cmd.ExecuteScalar().ToString());
            }
        }
        catch (Exception ex)
        {

            devNotificaciones.error("Obtener id sql de pedido de un usuario, número operación pedido: " + numero_operacion, ex);

            return new int();
        }
    }
    public DataTable obtenerPedidoDatos(int idSQL)
    {
        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT
                                datos.id,
                                datos.nombre_pedido,
                                datos.numero_operacion,
                                (SELECT COUNT(*)  FROM pedidos_productos WHERE  numero_operacion =  datos.numero_operacion) as productosTotales,
                                datos.id_cliente,
                                datos.fecha_creacion,
                                datos.creada_por,
                                datos.mod_asesor,
                                datos.id_cliente,
                                datos.usuario_cliente,
                                datos.cliente_nombre,
                                datos.cliente_apellido_paterno,
                                datos.cliente_apellido_materno,
                                datos.email,
                                datos.telefono,
                                datos.celular,
                                datos.activo,
                                datos.comentarios,
                                datos.preCotizacion,
                                datos.numero_operacion_cotizacion,
                                datosNum.subtotal,
                                datosNum.envio,
                                datosNum.metodoEnvio,
                                datosNum.impuestos,
                                datosNum.total

                                  FROM pedidos_datos as datos
                                  INNER JOIN pedidos_datosNumericos as datosNum
                                  ON datos.numero_operacion = datosNum.numero_operacion
                                  WHERE datos.id = @idSQL
                                ");

                cmd.Parameters.Add("@idSQL", SqlDbType.Int);
                cmd.Parameters["@idSQL"].Value = idSQL;
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

            devNotificaciones.error("Obtener pedido de un usuario, idSQL: " + idSQL, ex);

            return null;
        }
    }

    public DataTable obtenerPedidoDatos(string numero_operacion)
    {
        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT
                                datos.id,
                                datos.nombre_pedido,
                                datos.numero_operacion,
                                (SELECT COUNT(*)  FROM pedidos_productos WHERE  numero_operacion =  datos.numero_operacion) as productosTotales,
                                datos.id_cliente,
                                datos.fecha_creacion,
                                datos.creada_por,
                                datos.mod_asesor,
                                datos.id_cliente,
                                datos.usuario_cliente,
                                datos.cliente_nombre,
                                datos.cliente_apellido_paterno,
                                datos.cliente_apellido_materno,
                                datos.email,
                                datos.telefono,
                                datos.celular,
                                datos.activo,
                                datos.comentarios,
                                datos.preCotizacion,
                                datos.numero_operacion_cotizacion,
                                datosNum.subtotal,
                                datosNum.monedaPedido,
                                datosNum.envio,
                                datosNum.impuestos,
                                datosNum.total

                                  FROM pedidos_datos as datos
                                  INNER JOIN pedidos_datosNumericos as datosNum
                                  ON datos.numero_operacion = datosNum.numero_operacion
                                  WHERE datos.numero_operacion = @numero_operacion
                                ");

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

            devNotificaciones.error("Obtener pedido de un usuario, número operación: " + numero_operacion, ex);

            return null;
        }
    }



    /// <summary>
    /// Retorna datos del pedido y datosNumericos
    /// </summary>
    /// Act[20200103] Carlos.
    public static DataTable obtenerPedidoDatosStatic(string numero_operacion)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;

            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT
                                datos.id,
                                datos.nombre_pedido,
                                datos.numero_operacion,
                                (SELECT COUNT(*)  FROM pedidos_productos WHERE  numero_operacion =  datos.numero_operacion) as productosTotales,
                                datos.id_cliente,
                                datos.fecha_creacion,
                                datos.creada_por,
                                datos.mod_asesor,
                                datos.id_cliente,
                                datos.usuario_cliente,
                                datos.cliente_nombre,
                                datos.cliente_apellido_paterno,
                                datos.cliente_apellido_materno,
                                datos.email,
                                datos.telefono,
                                datos.celular,
                                datos.activo,
                                datos.comentarios,
                                datos.preCotizacion,
                                datos.numero_operacion_cotizacion,
                                datosNum.subtotal,
                                datosNum.monedaPedido,
                                datosNum.envio,
                                datosNum.impuestos,
                                datosNum.total

                                  FROM pedidos_datos as datos
                                  INNER JOIN pedidos_datosNumericos as datosNum
                                  ON datos.numero_operacion = datosNum.numero_operacion
                                  WHERE datos.numero_operacion = @numero_operacion
                                ");

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

            devNotificaciones.error("Obtener pedido de un usuario, número operación: " + numero_operacion, ex);

            return null;
        }
    }


    /// <summary>
    /// Retorna datos del pedido y datosNumericos
    /// </summary>
    /// Act[20200103] Carlos.
    public DataTable obtenerPedidoDatosMax(int idSQL)
    {
        try
        {
            dbConexion();
            using (con)
            {
                StringBuilder query = new StringBuilder();
                query.Append("SET LANGUAGE English; ");
                query.Append(@"SELECT
                                datos.id,
                                datos.nombre_pedido,
                                datos.numero_operacion,
                                (SELECT COUNT(*) FROM pedidos_productos WHERE  numero_operacion =  datos.numero_operacion) as productosTotales,
                                datos.id_cliente,
                                datos.fecha_creacion,
                                datos.creada_por,
                                datos.mod_asesor,
                                datos.id_cliente,
                                datos.usuario_cliente,
                                datos.cliente_nombre,
                                datos.cliente_apellido_paterno,
                                datos.cliente_apellido_materno,
                                datos.email,
                                datos.telefono,
                                datos.celular,
                                datos.activo,
                                datos.comentarios,
                                datos.preCotizacion,
                                datos.numero_operacion_cotizacion,
                                datosNum.[monedaPedido],
                                datosNum.[tipo_cambio],
                                datosNum.[fecha_tipo_cambio],
                                datosNum.[subtotal],
                                datosNum.[envio],
                                datosNum.[metodoEnvio],
                                datosNum.[monedaEnvio],
                                datosNum.impuestos,
                                datosNum.[nombreImpuestos],
                                datosNum.[total],
                                datosNum.[descuento],
                                datosNum.[descuento_porcentaje]

                                  FROM pedidos_datos as datos
                                  INNER JOIN  pedidos_datosNumericos as datosNum
                                  ON datos.numero_operacion = datosNum.numero_operacion
                                  WHERE datos.id = @idSQL
                                ");

                cmd.Parameters.Add("@idSQL", SqlDbType.Int);
                cmd.Parameters["@idSQL"].Value = idSQL;
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

            devNotificaciones.error("Obtener pedido idSQL: " + idSQL, ex);

            return null;
        }
    }
    public model_direccionesEnvio obtenerPedidoDireccionEnvio(string numero_operacion)
    {


        try
        {

            DataTable dtDireccion = new DataTable();

            dbConexion();

            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT * FROM pedidos_direccionEnvio WHERE numero_operacion= @numero_operacion ");

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;
                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;


                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(ds);
                dtDireccion = ds.Tables[0];

            }

            if (dtDireccion.Rows.Count >= 1)
            {

                model_direccionesEnvio pedidoDireccionEnvio = new model_direccionesEnvio();
                pedidoDireccionEnvio.id = int.Parse(dtDireccion.Rows[0]["id"].ToString());
                pedidoDireccionEnvio.calle = dtDireccion.Rows[0]["calle"].ToString();
                pedidoDireccionEnvio.numero = dtDireccion.Rows[0]["numero"].ToString();
                pedidoDireccionEnvio.colonia = dtDireccion.Rows[0]["colonia"].ToString();
                pedidoDireccionEnvio.delegacion_municipio = dtDireccion.Rows[0]["delegacion_municipio"].ToString();
                pedidoDireccionEnvio.estado = dtDireccion.Rows[0]["estado"].ToString();
                pedidoDireccionEnvio.codigo_postal = dtDireccion.Rows[0]["codigo_postal"].ToString();
                pedidoDireccionEnvio.pais = dtDireccion.Rows[0]["pais"].ToString();
                pedidoDireccionEnvio.referencias = dtDireccion.Rows[0]["referencias"].ToString();

                return pedidoDireccionEnvio;
            }
            return null;
        }
        catch (Exception ex)
        {

            devNotificaciones.error("Obtener dirección de envío del pedido de la operación: " + numero_operacion, ex);

            return null;
        }
    }
    public static model_direccionesEnvio obtenerPedidoDireccionEnvioStatic(string numero_operacion)
    {


        try
        {

            DataTable dtDireccion = new DataTable();


            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;

            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT * FROM pedidos_direccionEnvio WHERE numero_operacion= @numero_operacion ");

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;
                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;


                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(ds);
                dtDireccion = ds.Tables[0];

            }

            if (dtDireccion.Rows.Count >= 1)
            {

                model_direccionesEnvio pedidoDireccionEnvio = new model_direccionesEnvio();
                pedidoDireccionEnvio.id = int.Parse(dtDireccion.Rows[0]["id"].ToString());
                pedidoDireccionEnvio.calle = dtDireccion.Rows[0]["calle"].ToString();
                pedidoDireccionEnvio.numero = dtDireccion.Rows[0]["numero"].ToString();
                pedidoDireccionEnvio.colonia = dtDireccion.Rows[0]["colonia"].ToString();
                pedidoDireccionEnvio.delegacion_municipio = dtDireccion.Rows[0]["delegacion_municipio"].ToString();
                pedidoDireccionEnvio.estado = dtDireccion.Rows[0]["estado"].ToString();
                pedidoDireccionEnvio.codigo_postal = dtDireccion.Rows[0]["codigo_postal"].ToString();
                pedidoDireccionEnvio.pais = dtDireccion.Rows[0]["pais"].ToString();
                pedidoDireccionEnvio.referencias = dtDireccion.Rows[0]["referencias"].ToString();

                return pedidoDireccionEnvio;
            }
            return null;
        }
        catch (Exception ex)
        {

            devNotificaciones.error("Obtener dirección de envío del pedido de la operación: " + numero_operacion, ex);

            return null;
        }
    }
    public model_direccionesFacturacion obtenerPedidoDireccionFacturacion(string numero_operacion)
    {


        try
        {

            DataTable dtDireccion = new DataTable();

            dbConexion();

            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT * FROM pedidos_direccionFacturacion WHERE numero_operacion= @numero_operacion ");

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;
                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;


                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(ds);

                dtDireccion = ds.Tables[0];
            }
            if (dtDireccion.Rows.Count >= 1)
            {
                model_direccionesFacturacion pedidoDireccionFacturacion = new model_direccionesFacturacion();

                pedidoDireccionFacturacion.id = int.Parse(dtDireccion.Rows[0]["id"].ToString());

                pedidoDireccionFacturacion.calle = dtDireccion.Rows[0]["calle"].ToString();
                pedidoDireccionFacturacion.numero = dtDireccion.Rows[0]["numero"].ToString();

                pedidoDireccionFacturacion.razon_social = dtDireccion.Rows[0]["razon_social"].ToString();
                pedidoDireccionFacturacion.rfc = dtDireccion.Rows[0]["rfc"].ToString();

                pedidoDireccionFacturacion.colonia = dtDireccion.Rows[0]["colonia"].ToString();
                pedidoDireccionFacturacion.delegacion_municipio = dtDireccion.Rows[0]["delegacion_municipio"].ToString();
                pedidoDireccionFacturacion.estado = dtDireccion.Rows[0]["estado"].ToString();
                pedidoDireccionFacturacion.codigo_postal = dtDireccion.Rows[0]["codigo_postal"].ToString();
                pedidoDireccionFacturacion.pais = dtDireccion.Rows[0]["pais"].ToString();


                return pedidoDireccionFacturacion;
            }
            return null;
        }
        catch (Exception ex)
        {

            devNotificaciones.error("Obtener dirección de facturación del pedido para la operación número: " + numero_operacion, ex);

            return null;
        }
    }


    /// <summary>
    /// 2/4 Crea los datos del pedido y devuelve el [numero_operacion] si se realizó correctamente, si no devuelve null
    /// </summary>
    /// <param name="idOrigen">|pc|= pedido de cotización, |pd|= pedido directo, |pcc|= pedido de cotización de carrito</param>  
    public string crearPedido_datos(usuarios usuario, model_pedidos_datos pedidoDatos, string idOrigen)
    {
        try
        {
            dbConexion();
            using (con)
            {
                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; DECLARE @idUltimo nvarchar(10); SET @idUltimo = IIF((SELECT MAX(id) FROM pedidos_datos) IS NULL, 1, (SELECT MAX(id) FROM pedidos_datos)); ");
                query.Append("DECLARE @numero_operacion nvarchar(20); SET @numero_operacion =  @idOrigen + @idFecha + @idUltimo;");

                query.Append("INSERT INTO [pedidos_datos]  ");
                query.Append(" (nombre_pedido, numero_operacion, fecha_creacion, creada_por, mod_asesor, id_cliente, usuario_cliente,  ");
                query.Append(" cliente_nombre, cliente_apellido_paterno, cliente_apellido_materno, email, telefono, activo, comentarios, preCotizacion, numero_operacion_cotizacion) ");

                query.Append(" VALUES (@nombre_pedido, @numero_operacion, @fecha_creacion, @creada_por, @mod_asesor, @id_cliente, @usuario_cliente, ");
                query.Append(" @cliente_nombre, @cliente_apellido_paterno, @cliente_apellido_materno, @email, @telefono, @activo, @comentarios, @preCotizacion, @numero_operacion_cotizacion);");

                query.Append("SELECT @numero_operacion ");

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@idOrigen", SqlDbType.NVarChar, 2);
                cmd.Parameters["@idOrigen"].Value = idOrigen;

                cmd.Parameters.Add("@idFecha", SqlDbType.NVarChar, 20);
                cmd.Parameters["@idFecha"].Value = utilidad_fechas.DDMMAA();

                cmd.Parameters.Add("@nombre_pedido", SqlDbType.NVarChar, 60);
                if (pedidoDatos.nombre_pedido == null) cmd.Parameters["@nombre_pedido"].Value = utilidad_fechas.AAAMMDD() + " - Pedido sin nombre";
                else cmd.Parameters["@nombre_pedido"].Value = textTools.lineSimple(pedidoDatos.nombre_pedido);

                cmd.Parameters.Add("@fecha_creacion", SqlDbType.DateTime);
                cmd.Parameters["@fecha_creacion"].Value = pedidoDatos.fecha_creacion;

                cmd.Parameters.Add("@creada_por", SqlDbType.NVarChar, 60);
                cmd.Parameters["@creada_por"].Value = pedidoDatos.creada_por;

                cmd.Parameters.Add("@mod_asesor", SqlDbType.Int);
                cmd.Parameters["@mod_asesor"].Value = pedidoDatos.mod_asesor;

                cmd.Parameters.Add("@id_cliente", SqlDbType.NVarChar);
                if (pedidoDatos.id_cliente == null) cmd.Parameters["@id_cliente"].Value = DBNull.Value;
                else cmd.Parameters["@id_cliente"].Value = pedidoDatos.id_cliente;


                cmd.Parameters.Add("@cliente_nombre", SqlDbType.NVarChar, 20);
                if (pedidoDatos.cliente_nombre == null) cmd.Parameters["@cliente_nombre"].Value = DBNull.Value;
                else cmd.Parameters["@cliente_nombre"].Value = textTools.lineSimple(pedidoDatos.cliente_nombre);

                cmd.Parameters.Add("@cliente_apellido_materno", SqlDbType.NVarChar, 20);
                if (pedidoDatos.cliente_apellido_materno == null) cmd.Parameters["@cliente_apellido_materno"].Value = DBNull.Value;
                else cmd.Parameters["@cliente_apellido_materno"].Value = textTools.lineSimple(pedidoDatos.cliente_apellido_materno);

                cmd.Parameters.Add("@cliente_apellido_paterno", SqlDbType.NVarChar, 20);
                if (pedidoDatos.cliente_apellido_paterno == null) cmd.Parameters["@cliente_apellido_paterno"].Value = DBNull.Value;
                else cmd.Parameters["@cliente_apellido_paterno"].Value = textTools.lineSimple(pedidoDatos.cliente_apellido_paterno);

                cmd.Parameters.Add("@usuario_cliente", SqlDbType.NVarChar, 60);
                cmd.Parameters["@usuario_cliente"].Value = pedidoDatos.usuario_cliente;

                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 60);
                cmd.Parameters["@email"].Value = textTools.lineSimple(pedidoDatos.email);

                cmd.Parameters.Add("@telefono", SqlDbType.NVarChar, 50);
                if (pedidoDatos.telefono == null) cmd.Parameters["@telefono"].Value = DBNull.Value;
                else cmd.Parameters["@telefono"].Value = textTools.lineSimple(pedidoDatos.telefono);

                cmd.Parameters.Add("@celular", SqlDbType.NVarChar, 50);
                if (pedidoDatos.celular == null) cmd.Parameters["@celular"].Value = DBNull.Value;
                else cmd.Parameters["@celular"].Value = textTools.lineSimple(pedidoDatos.celular);

                cmd.Parameters.Add("@activo", SqlDbType.Int);
                cmd.Parameters["@activo"].Value = pedidoDatos.activo;

                cmd.Parameters.Add("@comentarios", SqlDbType.NVarChar, 600);
                if (pedidoDatos.comentarios == null) cmd.Parameters["@comentarios"].Value = DBNull.Value;
                else cmd.Parameters["@comentarios"].Value = textTools.lineMulti(pedidoDatos.comentarios);

                cmd.Parameters.Add("@preCotizacion", SqlDbType.Int);
                cmd.Parameters["@preCotizacion"].Value = pedidoDatos.preCotizacion;

                cmd.Parameters.Add("@numero_operacion_cotizacion", SqlDbType.NVarChar, 20);
                if (string.IsNullOrEmpty(pedidoDatos.numero_operacion_cotizacion)) cmd.Parameters["@numero_operacion_cotizacion"].Value = DBNull.Value;
                else cmd.Parameters["@numero_operacion_cotizacion"].Value = pedidoDatos.numero_operacion_cotizacion;
                con.Open();

                return cmd.ExecuteScalar().ToString();
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Crear datos de pedido", ex);
            return null;
        }
    }

    /// <summary>
    /// Actualiza datos de contacto de pedido: email cliente, nombre, apellido paterno, apellido materno, telefono y celular. 
    /// </summary>
    /// 
    public string crearPedido_datosFromCotizacion(usuarios usuario, DataTable dt_datosCotizacion, string nombre_pedido, string idOrigen)
    {
        model_pedidos_datos pedidoDatos = new model_pedidos_datos();

        pedidoDatos.id = 2;
        pedidoDatos.nombre_pedido = nombre_pedido;
        //pedidoDatos.numero_operacion = ;
        pedidoDatos.mod_asesor = usuarios.modoAsesorActivado();
        pedidoDatos.id_cliente = dt_datosCotizacion.Rows[0]["id_cliente"].ToString();
        pedidoDatos.usuario_cliente = dt_datosCotizacion.Rows[0]["usuario_cliente"].ToString();
        pedidoDatos.cliente_nombre = dt_datosCotizacion.Rows[0]["cliente_nombre"].ToString();
        pedidoDatos.cliente_apellido_paterno = dt_datosCotizacion.Rows[0]["cliente_apellido_paterno"].ToString();
        pedidoDatos.cliente_apellido_materno = dt_datosCotizacion.Rows[0]["cliente_apellido_materno"].ToString();
        pedidoDatos.email = dt_datosCotizacion.Rows[0]["email"].ToString();
        pedidoDatos.telefono = dt_datosCotizacion.Rows[0]["telefono"].ToString();
        pedidoDatos.celular = dt_datosCotizacion.Rows[0]["celular"].ToString();
        pedidoDatos.activo = 1;
        pedidoDatos.comentarios = dt_datosCotizacion.Rows[0]["comentarios"].ToString();
        pedidoDatos.preCotizacion = 1;
        pedidoDatos.numero_operacion_cotizacion = dt_datosCotizacion.Rows[0]["numero_operacion"].ToString();

        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; " +
                            "DECLARE @idUltimo nvarchar(10); SET @idUltimo = IIF((SELECT MAX(id) FROM pedidos_datos) IS NULL, 1, (SELECT MAX(id) FROM pedidos_datos)); ");
                query.Append("DECLARE @numero_operacion nvarchar(20); SET @numero_operacion =  @idOrigen + @idFecha + @idUltimo;");

                query.Append("INSERT INTO [pedidos_datos]  ");
                query.Append(" (nombre_pedido, numero_operacion, fecha_creacion, creada_por, mod_asesor, id_cliente, usuario_cliente,  ");
                query.Append(" cliente_nombre, cliente_apellido_paterno, cliente_apellido_materno, email, telefono, activo, comentarios, preCotizacion, numero_operacion_cotizacion) ");

                query.Append(" VALUES (@nombre_pedido, @numero_operacion, @fecha_creacion, @creada_por, @mod_asesor, @id_cliente, @usuario_cliente, ");
                query.Append(" @cliente_nombre, @cliente_apellido_paterno, @cliente_apellido_materno, @email, @telefono, @activo, @comentarios, @preCotizacion, @numero_operacion_cotizacion);");

                query.Append("SELECT @numero_operacion ");

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@idOrigen", SqlDbType.NVarChar, 2);
                cmd.Parameters["@idOrigen"].Value = idOrigen;

                cmd.Parameters.Add("@idFecha", SqlDbType.NVarChar, 20);
                cmd.Parameters["@idFecha"].Value = utilidad_fechas.AAAMMDD();

                cmd.Parameters.Add("@nombre_pedido", SqlDbType.NVarChar, 60);
                if (pedidoDatos.nombre_pedido == null) cmd.Parameters["@nombre_pedido"].Value = utilidad_fechas.AAAMMDD() + " - Pedido sin nombre";
                else cmd.Parameters["@nombre_pedido"].Value = textTools.lineSimple(pedidoDatos.nombre_pedido);

                cmd.Parameters.Add("@fecha_creacion", SqlDbType.DateTime);
                cmd.Parameters["@fecha_creacion"].Value = pedidoDatos.fecha_creacion;

                cmd.Parameters.Add("@creada_por", SqlDbType.NVarChar, 60);
                cmd.Parameters["@creada_por"].Value = pedidoDatos.creada_por;

                cmd.Parameters.Add("@mod_asesor", SqlDbType.Int);
                cmd.Parameters["@mod_asesor"].Value = pedidoDatos.mod_asesor;

                cmd.Parameters.Add("@id_cliente", SqlDbType.NVarChar);
                if (pedidoDatos.id_cliente == null) cmd.Parameters["@id_cliente"].Value = DBNull.Value;
                else cmd.Parameters["@id_cliente"].Value = pedidoDatos.id_cliente;


                cmd.Parameters.Add("@cliente_nombre", SqlDbType.NVarChar, 20);
                if (pedidoDatos.cliente_nombre == null) cmd.Parameters["@cliente_nombre"].Value = DBNull.Value;
                else cmd.Parameters["@cliente_nombre"].Value = textTools.lineSimple(pedidoDatos.cliente_nombre);

                cmd.Parameters.Add("@cliente_apellido_materno", SqlDbType.NVarChar, 20);
                if (pedidoDatos.cliente_apellido_materno == null) cmd.Parameters["@cliente_apellido_materno"].Value = DBNull.Value;
                else cmd.Parameters["@cliente_apellido_materno"].Value = textTools.lineSimple(pedidoDatos.cliente_apellido_materno);

                cmd.Parameters.Add("@cliente_apellido_paterno", SqlDbType.NVarChar, 20);
                if (pedidoDatos.cliente_apellido_paterno == null) cmd.Parameters["@cliente_apellido_paterno"].Value = DBNull.Value;
                else cmd.Parameters["@cliente_apellido_paterno"].Value = textTools.lineSimple(pedidoDatos.cliente_apellido_paterno);

                cmd.Parameters.Add("@usuario_cliente", SqlDbType.NVarChar, 60);
                cmd.Parameters["@usuario_cliente"].Value = pedidoDatos.usuario_cliente;

                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 60);
                cmd.Parameters["@email"].Value = textTools.lineSimple(pedidoDatos.email);

                cmd.Parameters.Add("@telefono", SqlDbType.NVarChar, 50);
                if (pedidoDatos.telefono == null) cmd.Parameters["@telefono"].Value = DBNull.Value;
                else cmd.Parameters["@telefono"].Value = textTools.lineSimple(pedidoDatos.telefono);

                cmd.Parameters.Add("@celular", SqlDbType.NVarChar, 50);
                if (pedidoDatos.celular == null) cmd.Parameters["@celular"].Value = DBNull.Value;
                else cmd.Parameters["@celular"].Value = textTools.lineSimple(pedidoDatos.celular);

                cmd.Parameters.Add("@activo", SqlDbType.Int);
                cmd.Parameters["@activo"].Value = pedidoDatos.activo;

                cmd.Parameters.Add("@comentarios", SqlDbType.NVarChar, 600);
                if (pedidoDatos.comentarios == null) cmd.Parameters["@comentarios"].Value = DBNull.Value;
                else cmd.Parameters["@comentarios"].Value = textTools.lineMulti(pedidoDatos.comentarios);

                cmd.Parameters.Add("@preCotizacion", SqlDbType.Int);
                cmd.Parameters["@preCotizacion"].Value = pedidoDatos.preCotizacion;

                cmd.Parameters.Add("@numero_operacion_cotizacion", SqlDbType.NVarChar, 20);
                if (string.IsNullOrEmpty(pedidoDatos.numero_operacion_cotizacion)) cmd.Parameters["@numero_operacion_cotizacion"].Value = DBNull.Value;
                else cmd.Parameters["@numero_operacion_cotizacion"].Value = pedidoDatos.numero_operacion_cotizacion;
                con.Open();

                return cmd.ExecuteScalar().ToString();
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Crear datos de pedido a partir de una cotización", ex);
            return null;
        }
    }
    /// <sumary>
    /// Actualiza el campo idPedidoSAP
    /// </sumary>
    public string actualizarPedido_idPedidoSAP (string numero_operacion, string pedido_sap)
    {
        try
        {
            dbConexion();
            using (con)
            {
                StringBuilder query = new StringBuilder();
                query.Append("SET LANGUAGE English; ");
                query.Append("UPDATE pedido_datos SET ");
                query.Append(" idPedidoSAP = @idPedidoSAP WHERE numero_operacion = @numero_operacion ");
                
                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;
                cmd.Parameters.Add("@idPedidoSAP", SqlDbType.NVarChar, 15);
                cmd.Parameters["@idPedidoSAP"].Value = pedido_sap;

                con.Open();
                cmd.ExecuteScalar();
                return "";
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Actualizar idPedidoSAP: " + numero_operacion, ex);
            return null;
        }
    }
    /// <summary>
    /// Actualiza datos de contacto de pedido: email cliente, nombre, apellido paterno, apellido materno, telefono y celular. 
    /// </summary>
    /// 
    public string actualizarPedido_datosContacto(string numero_operacion, model_pedidos_datos pedidoDatos)
    {

        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append("UPDATE pedidos_datos SET ");
                query.Append(" cliente_nombre = @cliente_nombre, cliente_apellido_paterno = @cliente_apellido_paterno, " +
                                "cliente_apellido_materno = @cliente_apellido_materno, email = @email, telefono = @telefono, celular = @celular ");
                query.Append(" WHERE numero_operacion = @numero_operacion ");

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@cliente_nombre", SqlDbType.NVarChar, 20);
                if (pedidoDatos.cliente_nombre == null) cmd.Parameters["@cliente_nombre"].Value = DBNull.Value;
                else cmd.Parameters["@cliente_nombre"].Value = textTools.lineSimple(pedidoDatos.cliente_nombre);

                cmd.Parameters.Add("@cliente_apellido_materno", SqlDbType.NVarChar, 20);
                if (pedidoDatos.cliente_apellido_materno == null) cmd.Parameters["@cliente_apellido_materno"].Value = DBNull.Value;
                else cmd.Parameters["@cliente_apellido_materno"].Value = textTools.lineSimple(pedidoDatos.cliente_apellido_materno);

                cmd.Parameters.Add("@cliente_apellido_paterno", SqlDbType.NVarChar, 20);
                if (pedidoDatos.cliente_apellido_paterno == null) cmd.Parameters["@cliente_apellido_paterno"].Value = DBNull.Value;
                else cmd.Parameters["@cliente_apellido_paterno"].Value = textTools.lineSimple(pedidoDatos.cliente_apellido_paterno);

                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 60);
                cmd.Parameters["@email"].Value = textTools.lineSimple(pedidoDatos.email);

                cmd.Parameters.Add("@telefono", SqlDbType.NVarChar, 50);
                if (pedidoDatos.telefono == null) cmd.Parameters["@telefono"].Value = DBNull.Value;
                else cmd.Parameters["@telefono"].Value = textTools.lineSimple(pedidoDatos.telefono);

                cmd.Parameters.Add("@celular", SqlDbType.NVarChar, 50);
                if (pedidoDatos.celular == null) cmd.Parameters["@celular"].Value = DBNull.Value;
                else cmd.Parameters["@celular"].Value = textTools.lineSimple(pedidoDatos.celular);

                con.Open();
                cmd.ExecuteScalar();

                return "";
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Actualizar datos de contacto del pedido:" + numero_operacion, ex);
            return null;
        }
    }

    /// <summary>
    /// Actualiza todos los datos de dirección de envío de un pedido es importante validar si es nueva o se actualiza, se recomienda usar el método "
    /// </summary>
    /// 
    public string actualizarPedido_direccionEnvio(string numero_operacion, model_direccionesEnvio direccionEnvio)
    {

        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append("UPDATE pedidos_direccionEnvio SET ");
                query.Append(" calle = @calle, numero = @numero, colonia = @colonia, delegacion_municipio = @delegacion_municipio, estado = @estado," +
                                " codigo_postal = @codigo_postal, pais = @pais, referencias = @referencias");

                query.Append(" WHERE numero_operacion = @numero_operacion ");

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@calle", SqlDbType.NVarChar, 35);
                if (direccionEnvio.calle == null) cmd.Parameters["@calle"].Value = DBNull.Value;
                else cmd.Parameters["@calle"].Value = textTools.lineSimple(direccionEnvio.calle);

                cmd.Parameters.Add("@numero", SqlDbType.NVarChar, 30);
                if (direccionEnvio.numero == null) cmd.Parameters["@numero"].Value = DBNull.Value;
                else cmd.Parameters["@numero"].Value = textTools.lineSimple(direccionEnvio.numero);

                cmd.Parameters.Add("@colonia", SqlDbType.NVarChar, 70);
                if (string.IsNullOrWhiteSpace(direccionEnvio.colonia)) cmd.Parameters["@colonia"].Value = DBNull.Value;
                else cmd.Parameters["@colonia"].Value = textTools.lineSimple(direccionEnvio.colonia);

                cmd.Parameters.Add("@delegacion_municipio", SqlDbType.NVarChar, 60);
                cmd.Parameters["@delegacion_municipio"].Value = textTools.lineSimple(direccionEnvio.delegacion_municipio);

                cmd.Parameters.Add("@estado", SqlDbType.NVarChar, 35);
                if (direccionEnvio.estado == null) cmd.Parameters["@estado"].Value = DBNull.Value;
                else cmd.Parameters["@estado"].Value = textTools.lineSimple(direccionEnvio.estado);

                cmd.Parameters.Add("@codigo_postal", SqlDbType.NVarChar, 15);
                if (direccionEnvio.codigo_postal == null) cmd.Parameters["@codigo_postal"].Value = DBNull.Value;
                else cmd.Parameters["@codigo_postal"].Value = textTools.lineSimple(direccionEnvio.codigo_postal);

                cmd.Parameters.Add("@pais", SqlDbType.NVarChar, 35);
                if (direccionEnvio.pais == null) cmd.Parameters["@pais"].Value = DBNull.Value;
                else cmd.Parameters["@pais"].Value = textTools.lineSimple(direccionEnvio.pais);


                cmd.Parameters.Add("@referencias", SqlDbType.NVarChar, 100);
                if (direccionEnvio.referencias == null) cmd.Parameters["@referencias"].Value = DBNull.Value;
                else cmd.Parameters["@referencias"].Value = textTools.lineSimple(direccionEnvio.referencias);

                con.Open();
                cmd.ExecuteScalar();

                return "";
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Modificar dirección de envio de operación: " + numero_operacion, ex);
            return null;
        }
    }

    /// <summary>
    /// Actualiza todos los datos de dirección de facturación de un pedido es importante validar si es nueva o se actualiza, se recomienda usar el método "
    /// </summary>
    /// 
    public string actualizarPedido_direccionFacturacion(string numero_operacion, model_direccionesFacturacion direccionFacturacion)
    {

        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append("UPDATE pedidos_direccionFacturacion  SET ");
                query.Append(" razon_social = @razon_social, rfc = @rfc, calle = @calle, numero = @numero, colonia = @colonia, delegacion_municipio = @delegacion_municipio, estado = @estado," +
                                " codigo_postal = @codigo_postal, pais = @pais");

                query.Append(" WHERE numero_operacion = @numero_operacion ");

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@razon_social", SqlDbType.NVarChar, 150);
                if (direccionFacturacion.razon_social == null) cmd.Parameters["@razon_social"].Value = DBNull.Value;
                else cmd.Parameters["@razon_social"].Value = textTools.lineSimple(direccionFacturacion.razon_social);

                cmd.Parameters.Add("@rfc", SqlDbType.NVarChar, 15);
                if (direccionFacturacion.rfc == null) cmd.Parameters["@rfc"].Value = DBNull.Value;
                else cmd.Parameters["@rfc"].Value = textTools.lineSimple(direccionFacturacion.rfc);

                cmd.Parameters.Add("@calle", SqlDbType.NVarChar, 35);
                if (direccionFacturacion.calle == null) cmd.Parameters["@calle"].Value = DBNull.Value;
                else cmd.Parameters["@calle"].Value = textTools.lineSimple(direccionFacturacion.calle);

                cmd.Parameters.Add("@numero", SqlDbType.NVarChar, 30);
                if (direccionFacturacion.numero == null) cmd.Parameters["@numero"].Value = DBNull.Value;
                else cmd.Parameters["@numero"].Value = textTools.lineSimple(direccionFacturacion.numero);

                cmd.Parameters.Add("@colonia", SqlDbType.NVarChar, 35);
                if (direccionFacturacion.colonia == null) cmd.Parameters["@colonia"].Value = DBNull.Value;
                else cmd.Parameters["@colonia"].Value = textTools.lineSimple(direccionFacturacion.colonia);

                cmd.Parameters.Add("@delegacion_municipio", SqlDbType.NVarChar, 60);
                cmd.Parameters["@delegacion_municipio"].Value = textTools.lineSimple(direccionFacturacion.delegacion_municipio);

                cmd.Parameters.Add("@estado", SqlDbType.NVarChar, 35);
                if (direccionFacturacion.estado == null) cmd.Parameters["@estado"].Value = DBNull.Value;
                else cmd.Parameters["@estado"].Value = textTools.lineSimple(direccionFacturacion.estado);

                cmd.Parameters.Add("@codigo_postal", SqlDbType.NVarChar, 15);
                if (direccionFacturacion.codigo_postal == null) cmd.Parameters["@codigo_postal"].Value = DBNull.Value;
                else cmd.Parameters["@codigo_postal"].Value = textTools.lineSimple(direccionFacturacion.codigo_postal);

                cmd.Parameters.Add("@pais", SqlDbType.NVarChar, 35);
                if (direccionFacturacion.pais == null) cmd.Parameters["@pais"].Value = DBNull.Value;
                else cmd.Parameters["@pais"].Value = textTools.lineSimple(direccionFacturacion.pais);



                con.Open();
                cmd.ExecuteScalar();

                return "";
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Modificar dirección de facturación de operación: " + numero_operacion, ex);
            return null;
        }
    }

    /// <summary>
    /// Verifica que exista o no una dirección de envío para un pedido en la tabla pedidos_direccionEnvio y realiza un UPDATE o un CREATE de acuerdo al caso.
    /// </summary>
    public bool pedidoDireccionEnvio(string numero_operacion, model_direccionesEnvio direccion)
    {


        if (!String.IsNullOrEmpty(numero_operacion) || direccion != null)
        {

            try
            {
                int cantidad = 0;
                string resultado = string.Empty;

                dbConexion();
                using (con)
                {

                    StringBuilder query = new StringBuilder();

                    query.Append("SET LANGUAGE English; ");

                    query.Append("SELECT COUNT(*) FROM pedidos_direccionEnvio  ");
                    query.Append(" WHERE numero_operacion = @numero_operacion ");

                    cmd.CommandText = query.ToString();
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                    cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                    con.Open();
                    cantidad = int.Parse(cmd.ExecuteScalar().ToString());


                }

                // Si es igual a 0 quiere decir que no existe dirección, entonces hará un CREATE
                if (cantidad == 0)
                    resultado = crearPedido_direccionEnvio(numero_operacion, direccion);

                // Si es igual a 1 quiere decir que ya hay direccioón existente, entonces hará un UPDATE
                else if (cantidad == 1)
                    resultado = actualizarPedido_direccionEnvio(numero_operacion, direccion);

                if (resultado != null) return true; else return false;
            }
            catch (Exception ex)
            {
                devNotificaciones.error("Actualizar/crear direccion de envío de pedido: " + numero_operacion, ex);
                return false;
            }
        }
        else
        {

            return false;
        }
    }

    /// <summary>
    /// Verifica que exista o no una dirección de facturación para un pedido en la tabla pedidos_direccionFacturacion y realiza un UPDATE o un CREATE de acuerdo al caso.
    /// </summary>
    public bool pedidoDireccionFacturacion(string numero_operacion, model_direccionesFacturacion direccion)
    {

        string x = "";
        if (!String.IsNullOrEmpty(numero_operacion) || direccion != null)
        {

            try
            {
                int cantidad = 0;
                string resultado = string.Empty;

                dbConexion();
                using (con)
                {

                    StringBuilder query = new StringBuilder();

                    query.Append("SET LANGUAGE English; ");

                    query.Append("SELECT COUNT(*) FROM pedidos_direccionFacturacion ");
                    query.Append(" WHERE numero_operacion = @numero_operacion ");

                    cmd.CommandText = query.ToString();
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                    cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                    con.Open();
                    cantidad = int.Parse(cmd.ExecuteScalar().ToString());


                }

                // Si es igual a 0 quiere decir que no existe dirección, entonces hará un CREATE
                if (cantidad == 0)
                    resultado = crearPedido_direccionFacturacion(numero_operacion, direccion);

                // Si es igual a 1 quiere decir que ya hay direccioón existente, entonces hará un UPDATE
                else if (cantidad == 1)
                    resultado = actualizarPedido_direccionFacturacion(numero_operacion, direccion);

                if (resultado != null) return true; else return false;
            }
            catch (Exception ex)
            {
                devNotificaciones.error("Actualizar/crear direccion de envío de pedido: " + numero_operacion, ex);
                return false;
            }
        }
        else
        {

            return false;
        }
    }
    /// <summary>
    /// Crear(inserta) una dirección de envío, es importante validar si es nueva o se actualiza, se recomienda usar el método "pedidoDireccionFacturacion"
    /// </summary>
    /// 
    public string crearPedido_direccionEnvio(string numero_operacion, model_direccionesEnvio direccionEnvio)
    {

        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append("INSERT INTO pedidos_direccionEnvio ");
                query.Append("(calle, numero, colonia, delegacion_municipio, estado, ciudad, " +
                                " codigo_postal, pais, referencias,  numero_operacion)");

                query.Append("VALUES (@calle, @numero, @colonia, @delegacion_municipio, @estado, @ciudad, " +
                             "  @codigo_postal, @pais,  @referencias, @numero_operacion)");


                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@calle", SqlDbType.NVarChar, 50);
                if (direccionEnvio.calle == null) cmd.Parameters["@calle"].Value = DBNull.Value;
                else cmd.Parameters["@calle"].Value = textTools.lineSimple(direccionEnvio.calle);

                cmd.Parameters.Add("@numero", SqlDbType.NVarChar, 30);
                if (direccionEnvio.numero == null) cmd.Parameters["@numero"].Value = DBNull.Value;
                else cmd.Parameters["@numero"].Value = textTools.lineSimple(direccionEnvio.numero);

                cmd.Parameters.Add("@colonia", SqlDbType.NVarChar, 70);
                if (direccionEnvio.colonia == null) cmd.Parameters["@colonia"].Value = DBNull.Value;
                else cmd.Parameters["@colonia"].Value = textTools.lineSimple(direccionEnvio.colonia);

                cmd.Parameters.Add("@delegacion_municipio", SqlDbType.NVarChar, 60);
                cmd.Parameters["@delegacion_municipio"].Value = textTools.lineSimple(direccionEnvio.delegacion_municipio);

                cmd.Parameters.Add("@ciudad", SqlDbType.NVarChar, 60);
                if (direccionEnvio.ciudad == null) cmd.Parameters["@ciudad"].Value = DBNull.Value;
                else cmd.Parameters["@ciudad"].Value = textTools.lineSimple(direccionEnvio.ciudad);


                cmd.Parameters.Add("@estado", SqlDbType.NVarChar, 35);
                if (direccionEnvio.estado == null) cmd.Parameters["@estado"].Value = DBNull.Value;
                else cmd.Parameters["@estado"].Value = textTools.lineSimple(direccionEnvio.estado);

                cmd.Parameters.Add("@codigo_postal", SqlDbType.NVarChar, 15);
                if (direccionEnvio.codigo_postal == null) cmd.Parameters["@codigo_postal"].Value = DBNull.Value;
                else cmd.Parameters["@codigo_postal"].Value = textTools.lineSimple(direccionEnvio.codigo_postal);

                cmd.Parameters.Add("@pais", SqlDbType.NVarChar, 35);
                if (direccionEnvio.pais == null) cmd.Parameters["@pais"].Value = DBNull.Value;
                else cmd.Parameters["@pais"].Value = textTools.lineSimple(direccionEnvio.codigo_postal);

                cmd.Parameters.Add("@referencias", SqlDbType.NVarChar, 100);
                if (direccionEnvio.referencias == null) cmd.Parameters["@referencias"].Value = DBNull.Value;
                else cmd.Parameters["@referencias"].Value = textTools.lineSimple(direccionEnvio.codigo_postal);

                con.Open();
                cmd.ExecuteScalar();

                return "";
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Crear dirección de envio de operación para el pedido: " + numero_operacion, ex);
            return null;
        }
    }

    /// <summary>
    /// Crear(inserta) una dirección de envío, es importante validar si es nueva o se actualiza, se recomienda usar el método "pedidoDireccionFacturacion"
    /// </summary>
    /// 
    public string crearPedido_direccionFacturacion(string numero_operacion, model_direccionesFacturacion direccionFacturacion)
    {

        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append("INSERT INTO pedidos_direccionFacturacion ");
                query.Append("(razon_social, rfc, calle, numero, colonia, delegacion_municipio, estado, ciudad, " +
                                " codigo_postal, pais,  numero_operacion)");

                query.Append("VALUES (@razon_social, @rfc, @calle, @numero, @colonia, @delegacion_municipio, @estado, @ciudad, " +
                             "  @codigo_postal, @pais, @numero_operacion)");


                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@razon_social", SqlDbType.NVarChar, 150);
                if (direccionFacturacion.razon_social == null) cmd.Parameters["@razon_social"].Value = DBNull.Value;
                else cmd.Parameters["@razon_social"].Value = textTools.lineSimple(direccionFacturacion.razon_social);

                cmd.Parameters.Add("@rfc", SqlDbType.NVarChar, 15);
                if (direccionFacturacion.rfc == null) cmd.Parameters["@rfc"].Value = DBNull.Value;
                else cmd.Parameters["@rfc"].Value = textTools.lineSimple(direccionFacturacion.rfc);


                cmd.Parameters.Add("@calle", SqlDbType.NVarChar, 50);
                if (direccionFacturacion.calle == null) cmd.Parameters["@calle"].Value = DBNull.Value;
                else cmd.Parameters["@calle"].Value = textTools.lineSimple(direccionFacturacion.calle);

                cmd.Parameters.Add("@numero", SqlDbType.NVarChar, 30);
                if (direccionFacturacion.numero == null) cmd.Parameters["@numero"].Value = DBNull.Value;
                else cmd.Parameters["@numero"].Value = textTools.lineSimple(direccionFacturacion.numero);

                cmd.Parameters.Add("@colonia", SqlDbType.NVarChar, 70);
                if (direccionFacturacion.colonia == null) cmd.Parameters["@colonia"].Value = DBNull.Value;
                else cmd.Parameters["@colonia"].Value = textTools.lineSimple(direccionFacturacion.colonia);

                cmd.Parameters.Add("@delegacion_municipio", SqlDbType.NVarChar, 60);
                cmd.Parameters["@delegacion_municipio"].Value = textTools.lineSimple(direccionFacturacion.delegacion_municipio);

                cmd.Parameters.Add("@estado", SqlDbType.NVarChar, 35);
                if (direccionFacturacion.estado == null) cmd.Parameters["@estado"].Value = DBNull.Value;
                else cmd.Parameters["@estado"].Value = textTools.lineSimple(direccionFacturacion.estado);

                cmd.Parameters.Add("@ciudad", SqlDbType.NVarChar, 60);
                if (direccionFacturacion.ciudad == null) cmd.Parameters["@ciudad"].Value = DBNull.Value;
                else cmd.Parameters["@ciudad"].Value = textTools.lineSimple(direccionFacturacion.ciudad);

                cmd.Parameters.Add("@codigo_postal", SqlDbType.NVarChar, 15);
                if (direccionFacturacion.codigo_postal == null) cmd.Parameters["@codigo_postal"].Value = DBNull.Value;
                else cmd.Parameters["@codigo_postal"].Value = textTools.lineSimple(direccionFacturacion.codigo_postal);

                cmd.Parameters.Add("@pais", SqlDbType.NVarChar, 35);
                if (direccionFacturacion.pais == null) cmd.Parameters["@pais"].Value = DBNull.Value;
                else cmd.Parameters["@pais"].Value = textTools.lineSimple(direccionFacturacion.codigo_postal);



                con.Open();
                cmd.ExecuteScalar();

                return "";
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Crear dirección de facturación de operación para el pedido: " + numero_operacion, ex);
            return null;
        }
    }

    public string editarNombrePedido(int idPedido, string nombre_pedido)
    {

        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; " +
                            "UPDATE pedidos_datos ");
                query.Append(" SET nombre_pedido=@nombre_pedido WHERE id= @idPedido");


                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@idPedido", SqlDbType.Int);
                cmd.Parameters["@idPedido"].Value = idPedido;
                cmd.Parameters.Add("@nombre_pedido", SqlDbType.NVarChar, 60);
                if (nombre_pedido == null) cmd.Parameters["@nombre_pedido"].Value = "Pedido sin nombre";
                else cmd.Parameters["@nombre_pedido"].Value = textTools.lineSimple(nombre_pedido);



                con.Open();
                return cmd.ExecuteNonQuery().ToString();
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Editar nombre de pedido", ex);
            return null;
        }
    }

    public async Task<string> clone_direccionEnvioCotizacion_ToPedido(usuarios usuario, string numero_operacionPedido, string numero_operacionCotizacion)
    {

        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English;");
                query.Append(@"INSERT INTO pedidos_direccionEnvio (
	                                  [numero_operacion]
                                      ,[calle]
                                      ,[numero]
                                      ,[colonia]
                                      ,[delegacion_municipio]
                                      ,[ciudad]
                                      ,[estado]
                                      ,[codigo_postal]
                                      ,[pais]
                                      ,[referencias]   ) 

	                                  SELECT 
	                                   [numero_operacion] = @numero_operacionPedido
                                      ,[calle]
                                      ,[numero]
                                      ,[colonia]
                                      ,[delegacion_municipio]
                                      ,[ciudad]
                                      ,[estado]
                                      ,[codigo_postal]
                                      ,[pais]
                                      ,[referencias]
	                             FROM cotizaciones_direccionEnvio WHERE numero_operacion = @numero_operacionCotizacion");


                cmd.Parameters.Add("@numero_operacionPedido", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacionPedido"].Value = numero_operacionPedido;

                cmd.Parameters.Add("@numero_operacionCotizacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacionCotizacion"].Value = numero_operacionCotizacion;

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                con.Open();

                await cmd.ExecuteNonQueryAsync();
                return "";

            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Clonar direccion de envío de cotización a pedido", ex);
            return null;
        }

    }

    public async Task<string> clone_direccionEnvioFacturacion_ToPedido(usuarios usuario, string numero_operacionPedido, string numero_operacionCotizacion)
    {

        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English;");
                query.Append(@"INSERT INTO pedidos_direccionFacturacion (
	                                  [numero_operacion]
                                      ,[razon_social]
                                      ,[rfc]
                                      ,[calle]
                                      ,[numero]
                                      ,[colonia]
                                      ,[delegacion_municipio]
                                      ,[ciudad] 
                                      ,[estado]
                                      ,[codigo_postal]
                                      ,[pais]
                                      ) 

	                                  SELECT 
	                                   [numero_operacion] = @numero_operacionPedido
                                      ,[razon_social]
                                      ,[rfc]
                                      ,[calle]
                                      ,[numero]
                                      ,[colonia]
                                      ,[delegacion_municipio]
                                      ,[ciudad] 
                                      ,[estado]
                                      ,[codigo_postal]
                                      ,[pais]
	                             FROM cotizaciones_direccionFacturacion WHERE numero_operacion = @numero_operacionCotizacion");


                cmd.Parameters.Add("@numero_operacionPedido", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacionPedido"].Value = numero_operacionPedido;

                cmd.Parameters.Add("@numero_operacionCotizacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacionCotizacion"].Value = numero_operacionCotizacion;

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                con.Open();

                await cmd.ExecuteNonQueryAsync();
                return "";

            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Clonar direccion de facturación de cotización a pedido", ex);
            return null;
        }

    }
    static public bool actualizarComentarioPedido(string numero_operacion, string comentario)
    {

        try
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append("UPDATE pedidos_datos SET ");
                query.Append(" comentarios =  @comentario");
                query.Append(" WHERE numero_operacion = @numero_operacion ");


                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@comentario", SqlDbType.NVarChar, 600);
                cmd.Parameters["@comentario"].Value = comentario;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query.ToString();


                con.Open();

                cmd.ExecuteNonQuery();
                return true;
            }
        }
        catch (Exception ex)
        {

            devNotificaciones.error("Actualizar cotización comentarios", ex);
            return false;
        }
    }
    public string crearPedidoDeCotizacion_datosNumericos(string numero_operacionPedido, string numero_operacion_cotizacion)
    {


        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();
                query.Append("SET LANGUAGE English;");
                query.Append(@"INSERT INTO pedidos_datosNumericos (
	                                  [numero_operacion]
                                      ,[monedaPedido]
                                      ,[tipo_cambio]
                                      ,[fecha_tipo_cambio]
                                      ,[descuento]
                                      ,[descuento_porcentaje]
                                      ,[subtotal]
                                      ,[envio]
                                      ,[metodoEnvio]
                                      ,[monedaEnvio]
                                      ,[impuestos]
                                      ,[nombreImpuestos]
                                      ,[total]
                                ) 

	                          SELECT 
	                              [numero_operacion]  = @numero_operacionPedido
                                  ,[monedaCotizacion]
                                  ,[tipo_cambio]
                                  ,[fecha_tipo_cambio]
                                  ,[descuento]
                                  ,[descuento_porcentaje]
                                  ,[subtotal]
                                  ,[envio]
                                  ,[metodoEnvio]
                                  ,[monedaEnvio]
                                  ,[impuestos]
                                  ,[nombreImpuestos]
                                  ,[total]
	                             FROM cotizaciones_datosNumericos WHERE numero_operacion = @numero_operacion_cotizacion");


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
    /// Inserta el asesor base y también grupo base, tenga o no asignado, si no lo tiene insertara valores null
    /// </summary>
    /// 
    static public string insertarAsesoresBase(string numero_operacion, string asesorBaseUsuario, string asesorBaseGrupo)
    {

        try
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");
                query.Append("IF NOT EXISTS(SELECT numero_operacion FROM pedidos_asesorBase WHERE numero_operacion = @numero_operacion) BEGIN ");

                query.Append("INSERT INTO cotizaciones_asesorBase ");
                query.Append("(asesorBaseUsuario, asesorBaseGrupo, numero_operacion)");

                query.Append("VALUES (@asesorBaseUsuario, @asesorBaseGrupo, @numero_operacion); SELECT 'true';");

                query.Append("END ELSE BEGIN SELECT 'false'; END");

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@asesorBaseUsuario", SqlDbType.NVarChar, 60);
                if (string.IsNullOrEmpty(asesorBaseUsuario)) cmd.Parameters["@asesorBaseUsuario"].Value = DBNull.Value;
                else cmd.Parameters["@asesorBaseUsuario"].Value = asesorBaseUsuario;


                cmd.Parameters.Add("@asesorBaseGrupo", SqlDbType.NVarChar, 20);
                if (string.IsNullOrEmpty(asesorBaseGrupo)) cmd.Parameters["@asesorBaseGrupo"].Value = DBNull.Value;
                else cmd.Parameters["@asesorBaseGrupo"].Value = asesorBaseGrupo;


                con.Open();

                string resultado = cmd.ExecuteScalar().ToString();
                if (resultado == "true") return ""; else return null;
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error(string.Format("Insertar asesor base en: pedido: {0}, usuario: {1}, grupoBase:  {2} ", numero_operacion, asesorBaseUsuario, asesorBaseGrupo), ex);
            return null;
        }
    }
    /// <summary>
    /// Busca el numero de operación de una cotización en el campo [numero_operacion_cotizacion] de un pedido, devuelve 1 si encontró un registro y0 si no existe, sirve para validar si dicha cotización ya fue transformada en pedido
    /// </summary>
    /// 
    static public int verificarPreCotizacion(string numero_operacion_cotizacion)
    {

        try
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");
                query.Append(@"SELECT COUNT(numero_operacion_cotizacion)
                               FROM[tienda].[dbo].[pedidos_datos] WHERE numero_operacion_cotizacion = @numero_operacion_cotizacion");



                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@numero_operacion_cotizacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion_cotizacion"].Value = numero_operacion_cotizacion;

                con.Open();

                return int.Parse(cmd.ExecuteScalar().ToString());

            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Validar existencia de pedido a partir de una cotización, no: " + numero_operacion_cotizacion, ex);
            return 0;
        }
    }

    public static string actualizarEnvio(decimal envio, string metodoEnvio, string numero_operacion)
    {

        try
        {

            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;

            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; " + " UPDATE pedidos_datosNumericos ");
                query.Append("SET metodoEnvio=@metodoEnvio, envio=@envio WHERE numero_operacion= @numero_operacion");

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@envio", SqlDbType.Money);
                cmd.Parameters["@envio"].Value = envio;

                cmd.Parameters.Add("@metodoEnvio", SqlDbType.NVarChar, 15);
                cmd.Parameters["@metodoEnvio"].Value = metodoEnvio;

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                con.Open();
                return cmd.ExecuteNonQuery().ToString();
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Actualizar envío de pedido", ex);
            return null;
        }
    }

    public static string actualizarEnvio(decimal envio, string metodoEnvio, string numero_operacion, string EnvioNota)
    {

        try
        {

            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;

            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; " + " UPDATE pedidos_datosNumericos ");
                query.Append("SET metodoEnvio=@metodoEnvio, envio=@envio, EnvioNota=@EnvioNota WHERE numero_operacion= @numero_operacion");

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@envio", SqlDbType.Money);
                cmd.Parameters["@envio"].Value = envio;

                cmd.Parameters.Add("@metodoEnvio", SqlDbType.NVarChar, 15);
                cmd.Parameters["@metodoEnvio"].Value = metodoEnvio;

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@EnvioNota", SqlDbType.NVarChar, 200);
                cmd.Parameters["@EnvioNota"].Value = EnvioNota;


                con.Open();
                return cmd.ExecuteNonQuery().ToString();
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Actualizar envío de pedido", ex);
            return null;
        }
    }
    /// <summary>
    /// 20201105 Obtiene el valor del campo [Calculo_Costo_Envio] de la tabla [pedidos_datos] el cuál determina si la operación permite calcular el costo
    /// del flete de manera automática. Si no se encuentra este valor, devuelve falso
    /// </summary>
    /// 
    public static bool obtenerEstatusCalculo_Costo_Envio(string numero_operacion)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;
            using (con)
            {

                string query = "SELECT Calculo_Costo_Envio FROM pedidos_datos WHERE numero_operacion=@numero_operacion";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;


                con.Open();

                string strActivo = cmd.ExecuteScalar().ToString();
                if (string.IsNullOrEmpty(strActivo)) return false;
                bool Activo = bool.Parse(strActivo);
                return Activo;
            }
        }
        catch (Exception ex)
        {

            return false;
        }
    }

    /// <summary>
    /// 20201105  Establece si la operación realizará el calculo de envio automático por API
    /// </summary>
    /// 
    public static void establecerEstatusCalculo_Costo_Envio(bool Estatus, string numero_operacion)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;
            using (con)
            {
                string query = "UPDATE pedidos_datos SET Calculo_Costo_Envio=@Estatus WHERE numero_operacion=@numero_operacion;";

                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@Estatus", SqlDbType.Bit);
                cmd.Parameters["@Estatus"].Value = Estatus;


                con.Open();

                cmd.ExecuteNonQuery();


            }
        }
        catch (Exception ex)
        {


        }
    }
    /// <summary>
    /// 20201105 Obtiene el valor del campo [metodoEnvio] de la tabla [pedidos_datosNumericos] 
    /// Valores: [Ninguno][Estándar][En Tienda][Gratuito]
    /// </summary>
    /// 
    public static string obtenerMetodoDeEnvio(string numero_operacion)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;
            using (con)
            {

                string query = "SELECT metodoEnvio FROM pedidos_datosNumericos WHERE numero_operacion=@numero_operacion";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;


                con.Open();

                string metodoEnvio = cmd.ExecuteScalar().ToString();

                return metodoEnvio;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
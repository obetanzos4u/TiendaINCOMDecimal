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
public class cotizaciones {
    private SqlConnection con { get; set; }
    private SqlCommand cmd { get; set; }
    private SqlDataAdapter da { get; set; }
    private DataSet ds { get; set; }
    private DataTable dt { get; set; }


    public string monedaCotizacion { get; set; }
    public decimal tipoDeCambio { get; set; }
    public DateTime fechaTipoDeCambio { get; set; }

    protected void dbConexion() {

        con = new SqlConnection(conexiones.conexionTienda());
        cmd = new SqlCommand();
        cmd.Connection = con;

        }
    public cotizaciones() {
        // 
        // TODO: Agregar aquí la lógica del constructor
        //
        }
    
    /// <summary>
    /// Crea la cotización y de vuelve un string con el número de operación, también vacía el carrito si esta fue creada con éxito.
    /// </summary>
    /// 
    public string crearCotizacionDeCarrito(usuarios usuario, model_impuestos impuestos, model_cotizaciones_datos cotizacionDatos) {

        model_envios envio = new model_envios();
        string numero_operacion = crearCotizacion_datos(usuario, cotizacionDatos);
        string productos = crearCotizacionDeCarrito_productos(usuario, numero_operacion, impuestos, envio);
        string asesorBase = insertarAsesoresBase(numero_operacion, usuario.asesor_base, usuario.grupo_asesor);

        if (numero_operacion != null && productos != null ) {

            carrito carrito = new carrito();

            carrito.eliminarCarrito(usuario.email);

            return numero_operacion;

            } else {

            borrarPermanentemente(numero_operacion);

            return null;
            }

        }

    /// <summary>
    /// Crea la cotización y de vuelve un string con el número de operación
    /// </summary>
    /// 
    public string crearCotizacionVacia(usuarios usuario,   model_cotizaciones_datos cotizacionDatos) {

        model_envios envio = new model_envios();
        model_impuestos impuestos = new model_impuestos() { nombre = "MX", valor = 16, id = 1 };

        string numero_operacion = crearCotizacion_datos(usuario, cotizacionDatos);
        string productos = crearCotizacionVacia_productos(usuario, numero_operacion, impuestos, envio);
        string asesorBase = insertarAsesoresBase(numero_operacion, usuario.asesor_base, usuario.grupo_asesor);

        if (numero_operacion != null && productos != null) {

            return numero_operacion;

            } else {

            borrarPermanentemente(numero_operacion);

            return null;
            }

        }
    /// <summary>
    /// Retorna el numero_operacion de la cotización creada
    /// </summary>
    /// 
    public string borrarPermanentemente(string numero_operacion) {
        try {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English;");
                query.Append("DELETE FROM cotizaciones_productos WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM cotizaciones_datosNumericos WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM cotizaciones_datos WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM cotizaciones_direccionFacturacion WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM cotizaciones_direccionEnvio WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM cotizaciones_modificaciones WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM cotizaciones_productos_modificaciones WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM cotizaciones_asesorBase WHERE numero_operacion = @numero_operacion;");

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;
                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                con.Open();

                cmd.ExecuteNonQuery();

                return "";
                }
            }
        catch (Exception ex) {

            devNotificaciones.error("Borrar permanentemente una cotización", ex);

            return null;
            }
        }
    /// <summary>
    /// Retorna el numero_operacion de la cotización creada
    /// </summary>
    /// 
    public static json_respuestas borrarPermanentementeStatic(string numero_operacion)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;

            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English;");
                query.Append("DELETE FROM cotizaciones_productos WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM cotizaciones_datosNumericos WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM cotizaciones_datos WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM cotizaciones_direccionFacturacion WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM cotizaciones_direccionEnvio WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM cotizaciones_modificaciones WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM cotizaciones_productos_modificaciones WHERE numero_operacion = @numero_operacion;");
                query.Append("DELETE FROM cotizaciones_asesorBase WHERE numero_operacion = @numero_operacion;");

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;
                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                con.Open();

               
                int resultInt = cmd.ExecuteNonQuery();


                return new json_respuestas(true, "Cotizacion eliminada con éxito", false);
            }
        }
        catch (Exception ex)
        {



            return new json_respuestas(false, "Error al eliminar", true);
        }
    }
    /// <summary>
    /// Recibe y ejecuta un query, transforma el resultado en un DataTable
    /// </summary>
    /// 
    public static DataTable obtenerCotizacionesLibre(string query) {
        try {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;
            using (con) {

                
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;



                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(ds);

                return ds.Tables[0];
            }
        }
        catch (Exception ex) {

            devNotificaciones.error("Obtener cotizaciones query Libre de un usuario.", ex);


            return null;
        }
    }
    public DataTable obtenerCotizacionesUsuario_min(string usuario_cliente) {
        try {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT
                                datos.id,
                                datos.nombre_cotizacion,
                                datos.numero_operacion,
                                (SELECT COUNT(*)  FROM cotizaciones_productos WHERE  numero_operacion =  datos.numero_operacion) as productosTotales,
                                datos.id_cliente,
                                datos.cliente_nombre,
                                datos.fecha_creacion,
                                datos.creada_por,
                                datos.vigencia,
                                datos.conversionPedido,
                                datos.idEstatus,
                                datos.numero_operacion_pedido,
                                datosNum.monedaCotizacion,
                                datosNum.subtotal,
                                datosNum.envio,
                                datosNum.impuestos,
                                datosNum.total
                                  FROM cotizaciones_datos as datos
                                  INNER JOIN  cotizaciones_datosNumericos as datosNum
                                  ON datos.numero_operacion = datosNum.numero_operacion
                                  WHERE datos.usuario_cliente = @usuario_cliente ORDER BY fecha_creacion DESC
                                ");
             
             cmd.Parameters.Add("@usuario_cliente", SqlDbType.NVarChar, 60);
                cmd.Parameters["@usuario_cliente"].Value = usuario_cliente;
                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 4;


                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(ds);

                return ds.Tables[0];
                }
            }
        catch (Exception ex) {

                 devNotificaciones.error("Obtener cotizaciones de un usuario.", ex);


            return null;
            }
        }
    public DataTable obtenerCotizacionesAsesor_min(string creada_por) {
        try {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT
                                datos.id,
                                datos.nombre_cotizacion,
                                datos.numero_operacion,
                                (SELECT COUNT(*)  FROM cotizaciones_productos WHERE  numero_operacion =  datos.numero_operacion) as productosTotales,
                                datos.id_cliente,
                                 datos.usuario_cliente,
                                datos.cliente_nombre,
                                datos.fecha_creacion,
                                datos.creada_por,
                                datos.vigencia,
                                datos.conversionPedido,
                                datos.numero_operacion_pedido,
                                datos.idEstatus,

                                datosNum.monedaCotizacion,
                                datosNum.subtotal,
                                datosNum.envio,
                                datosNum.impuestos,
                                datosNum.total
                                  FROM cotizaciones_datos as datos
                                  INNER JOIN  cotizaciones_datosNumericos as datosNum
                                  ON datos.numero_operacion = datosNum.numero_operacion
                                  WHERE datos.creada_por = @creada_por ORDER BY fecha_creacion DESC
                                ");

                cmd.Parameters.Add("@creada_por", SqlDbType.NVarChar, 60);
                cmd.Parameters["@creada_por"].Value = creada_por;
                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;



                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(ds);

                return ds.Tables[0];
            }
        }
        catch (Exception ex) {

            devNotificaciones.error("Obtener cotizaciones de un asesor.", ex);


            return null;
        }
    }

    public DataTable obtenerCotizacionesAsesor_min(string creada_por, DateTime fechaDesde, DateTime fechaHasta, string termino)
    {
        if(fechaDesde == null) fechaDesde = utilidad_fechas.obtenerCentral().AddMonths(-6);

        if (fechaHasta == null) fechaHasta = utilidad_fechas.obtenerCentral();



        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT
                                datos.id,
                                datos.nombre_cotizacion,
                                datos.numero_operacion,
                                (SELECT COUNT(*)  FROM cotizaciones_productos WHERE  numero_operacion =  datos.numero_operacion) as productosTotales,
                                datos.id_cliente,
                                 datos.usuario_cliente,
                                datos.cliente_nombre,
                                datos.fecha_creacion,
                                datos.creada_por,
                                datos.vigencia,
                                datos.conversionPedido,
                                datos.numero_operacion_pedido,
                                datos.idEstatus,

                                datosNum.monedaCotizacion,
                                datosNum.subtotal,
                                datosNum.envio,
                                datosNum.impuestos,
                                datosNum.total
                                  FROM cotizaciones_datos as datos
                                  INNER JOIN  cotizaciones_datosNumericos as datosNum
                                  ON datos.numero_operacion = datosNum.numero_operacion
                                  WHERE datos.creada_por = @creada_por
                                    AND datos.fecha_creacion BETWEEN @fechaDesde AND @fechaHasta

                                  ORDER BY fecha_creacion DESC
                                ");

                cmd.Parameters.Add("@creada_por", SqlDbType.NVarChar, 60);
                cmd.Parameters["@creada_por"].Value = creada_por;


                cmd.Parameters.Add("@fechaDesde", SqlDbType.DateTime);
                cmd.Parameters["@fechaDesde"].Value = fechaDesde;


                cmd.Parameters.Add("@fechaHasta", SqlDbType.DateTime);
                cmd.Parameters["@fechaHasta"].Value = fechaHasta;



                if (string.IsNullOrWhiteSpace(termino))
                {


                }

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

            devNotificaciones.error("Obtener cotizaciones de un asesor.", ex);


            return null;
        }
    }
    public DataTable obtenerCotizacionesUsuario_min(string usuario_cliente, int cantidadRegistros) {
        try {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT TOP(@cantidadRegistros)
                                datos.id,
                                datos.nombre_cotizacion,
                                datos.numero_operacion,
                                (SELECT COUNT(*)  FROM cotizaciones_productos WHERE  numero_operacion =  datos.numero_operacion) as productosTotales,
                                datos.id_cliente,
                                datos.cliente_nombre,
                                datos.fecha_creacion,
                                datos.creada_por,
                                datos.vigencia,
                                datos.conversionPedido,
                                datos.numero_operacion_pedido,
                                datosNum.monedaCotizacion,
                                datosNum.subtotal,
                                datosNum.envio,
                                datosNum.impuestos,
                                datosNum.total
                                  FROM cotizaciones_datos as datos
                                  INNER JOIN  cotizaciones_datosNumericos as datosNum
                                  ON datos.numero_operacion = datosNum.numero_operacion
                                  WHERE datos.usuario_cliente = @usuario_cliente ORDER BY fecha_creacion DESC
                                ");

                cmd.Parameters.Add("@usuario_cliente", SqlDbType.NVarChar, 60);
                cmd.Parameters["@usuario_cliente"].Value = usuario_cliente;

                cmd.Parameters.Add("@cantidadRegistros", SqlDbType.Int);
                cmd.Parameters["@cantidadRegistros"].Value = cantidadRegistros;

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;



                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(ds);

                return ds.Tables[0];
                }
            }
        catch (Exception ex) {

            devNotificaciones.error("Obtener cotizaciones de un usuario.", ex);

            return null;
            }
        }
    // <param name="filtrosSQL">Usar los prefijos de [datos.] [datosNum.]  </ param >
    public DataTable obtenerCotizacionesUsuario_min(string usuario_cliente, int cantidadRegistros, string filtrosSQL) {
        try {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT TOP(@cantidadRegistros)
                                datos.id,
                                datos.nombre_cotizacion,
                                datos.numero_operacion,
                                (SELECT COUNT(*)  FROM cotizaciones_productos WHERE  numero_operacion =  datos.numero_operacion) as productosTotales,
                                datos.id_cliente,
                                datos.cliente_nombre,
                                datos.fecha_creacion,
                                datos.creada_por,
                                datos.vigencia,
                                datos.conversionPedido,
                                datos.numero_operacion_pedido,
                                datosNum.monedaCotizacion,
                                datosNum.subtotal,
                                datosNum.envio,
                                datosNum.impuestos,
                                datosNum.total
                                  FROM cotizaciones_datos as datos
                                  INNER JOIN  cotizaciones_datosNumericos as datosNum
                                  ON datos.numero_operacion = datosNum.numero_operacion
                                  WHERE datos.usuario_cliente = @usuario_cliente 
                                ");

                query.Append(filtrosSQL);
                query.Append(" ORDER BY fecha_creacion DESC ");

                cmd.Parameters.Add("@usuario_cliente", SqlDbType.NVarChar, 60);
                cmd.Parameters["@usuario_cliente"].Value = usuario_cliente;

                cmd.Parameters.Add("@cantidadRegistros", SqlDbType.Int);
                cmd.Parameters["@cantidadRegistros"].Value = cantidadRegistros;

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;



                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(ds);

                return ds.Tables[0];
                }
            }
        catch (Exception ex) {

            devNotificaciones.error("Obtener cotizaciones de un usuario.", ex);

            return null;
            }
        }
    public DataTable obtenerCotizacionDatos_min(int idSQL) {
        try {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT
                                 datos.id,
                                datos.nombre_cotizacion,
                                datos.numero_operacion,
                                (SELECT COUNT(*)  FROM cotizaciones_productos WHERE  numero_operacion =  datos.numero_operacion) as productosTotales,
                                datos.id_cliente,
                                datos.cliente_nombre,
                                datos.fecha_creacion,
                                datos.creada_por,
                                datos.vigencia,
                                datos.conversionPedido,
                                datos.numero_operacion_pedido,
                                datosNum.monedaCotizacion,
                                datosNum.subtotal,
                                datosNum.envio,
                                datosNum.impuestos,
                                datosNum.total

                                  FROM cotizaciones_datos as datos
                                  INNER JOIN  cotizaciones_datosNumericos as datosNum
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
        catch (Exception ex) {

            devNotificaciones.error("Obtener cotizaciones de un usuario.", ex);

            return null;
            }
        }

    public DataTable obtenerCotizacionDatos_min(string numero_operacion) {
        try {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT
                                 datos.id,
                                datos.nombre_cotizacion,
                                datos.numero_operacion,
                                (SELECT COUNT(*)  FROM cotizaciones_productos WHERE  numero_operacion =  datos.numero_operacion) as productosTotales,
                                datos.id_cliente,
                                datos.cliente_nombre,
                                datos.fecha_creacion,
                                datos.creada_por,
                                datos.vigencia,
                                datos.conversionPedido,
                                datos.numero_operacion_pedido,
                                datosNum.monedaCotizacion,
                                datosNum.subtotal,
                                datosNum.envio,
                                datosNum.impuestos,
                                datosNum.total

                                  FROM cotizaciones_datos as datos
                                  INNER JOIN  cotizaciones_datosNumericos as datosNum
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
        catch (Exception ex) {

            devNotificaciones.error("Obtener cotizaciones de un usuario mediante numero de operación.", ex);

            return null;
            }
        }


    public DataTable obtenerCotizacionDatos_min(string usuario_cliente, DateTime fechaDesde, DateTime fechaHasta)
    {

        if (fechaDesde == null) fechaDesde = utilidad_fechas.obtenerCentral().AddMonths(-6);

        if (fechaHasta == null) fechaHasta = utilidad_fechas.obtenerCentral();

        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT
                                datos.id,
                                datos.nombre_cotizacion,
                                datos.numero_operacion,
                                (SELECT COUNT(*)  FROM cotizaciones_productos WHERE  numero_operacion =  datos.numero_operacion) as productosTotales,
                                datos.id_cliente,
                                datos.cliente_nombre,
                                datos.fecha_creacion,
                                datos.creada_por,
                                datos.vigencia,
                                datos.conversionPedido,
                                datos.idEstatus,
                                datos.numero_operacion_pedido,
                                datosNum.monedaCotizacion,
                                datosNum.subtotal,
                                datosNum.envio,
                                datosNum.impuestos,
                                datosNum.total
                                  FROM cotizaciones_datos as datos
                                  INNER JOIN  cotizaciones_datosNumericos as datosNum
                                  ON datos.numero_operacion = datosNum.numero_operacion
                                  WHERE datos.usuario_cliente = @usuario_cliente
                            AND datos.fecha_creacion BETWEEN @fechaDesde AND @fechaHasta

ORDER BY fecha_creacion DESC
                                ");

                cmd.Parameters.Add("@usuario_cliente", SqlDbType.NVarChar, 60);
                cmd.Parameters["@usuario_cliente"].Value = usuario_cliente;

                cmd.Parameters.Add("@fechaDesde", SqlDbType.DateTime);
                cmd.Parameters["@fechaDesde"].Value = fechaDesde;


                cmd.Parameters.Add("@fechaHasta", SqlDbType.DateTime);
                cmd.Parameters["@fechaHasta"].Value = fechaHasta;



                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 4;


                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();
                da.Fill(ds);

                return ds.Tables[0];
            }
        }
        catch (Exception ex)
        {

            devNotificaciones.error("Obtener cotizaciones de un usuario.", ex);


            return null;
        }
    }
    /// <summary>
    /// Retorna datos de cotizacion y datosNumericos
    /// </summary>
    /// 
    public DataTable obtenerCotizacionDatosMax(int idSQL) {
        try {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT
                                datos.id,
                                datos.nombre_cotizacion,
                                datos.numero_operacion,
                                (SELECT COUNT(*)  FROM cotizaciones_productos WHERE  numero_operacion =  datos.numero_operacion) as productosTotales,
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
                                datos.vigencia,
                                datos.vecesRenovada,
                                datos.conversionPedido,
                                datos.tipo_cotizacion,
                                datos.idEstatus,
                                datos.numero_operacion_pedido,
                               
                                datosNum.[monedaCotizacion],
                                datosNum.[tipo_cambio],
                                datosNum.[fecha_tipo_cambio],
                                datosNum.[subtotal],
                                datosNum.[envio],
                                datosNum.[metodoEnvio],
                                datosNum.[monedaEnvio],
                                datosNum.[impuestos],
                                datosNum.[nombreImpuestos],
                                datosNum.[total],
                                datosNum.[descuento],
                                datosNum.[descuento_porcentaje]

                                  FROM cotizaciones_datos as datos
                                  INNER JOIN  cotizaciones_datosNumericos as datosNum
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
        catch (Exception ex) {

            devNotificaciones.error("Obtener cotizaciones de un usuario.", ex);

            return null;
            }
        }
    /// <summary>
    /// Obtiene el campo tipo_cotizacion de  cotizaciones_datos
    /// </summary>
    /// 
    public static string obtener_tipo_cotizacion(int id) {
        try {

            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;

            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT  tipo_cotizacion  FROM cotizaciones_datos WHERE id = @id");

                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = id;
                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;
                con.Open();
                string resultado = cmd.ExecuteScalar().ToString();

                return resultado;
            }
        }
        catch (Exception ex) {

 

            return null;
        }
    }
    public model_direccionesEnvio obtenerCotizacionDireccionEnvio(string numero_operacion) {


        try {

            DataTable dtDireccion = new DataTable();

            dbConexion();

            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT * FROM cotizaciones_direccionEnvio WHERE numero_operacion= @numero_operacion ");

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
            if (dtDireccion.Rows.Count >= 1) {
                model_direccionesEnvio cotizacionDireccionEnvio = new model_direccionesEnvio();

                cotizacionDireccionEnvio.id = int.Parse(dtDireccion.Rows[0]["id"].ToString());
                cotizacionDireccionEnvio.calle = dtDireccion.Rows[0]["calle"].ToString();
                cotizacionDireccionEnvio.numero = dtDireccion.Rows[0]["numero"].ToString();
                cotizacionDireccionEnvio.colonia = dtDireccion.Rows[0]["colonia"].ToString();
                cotizacionDireccionEnvio.delegacion_municipio = dtDireccion.Rows[0]["delegacion_municipio"].ToString();
                cotizacionDireccionEnvio.estado = dtDireccion.Rows[0]["estado"].ToString();
                cotizacionDireccionEnvio.codigo_postal = dtDireccion.Rows[0]["codigo_postal"].ToString();
                cotizacionDireccionEnvio.pais = dtDireccion.Rows[0]["pais"].ToString();
                cotizacionDireccionEnvio.referencias = dtDireccion.Rows[0]["referencias"].ToString();

                return cotizacionDireccionEnvio;
                } return null;
            }
        catch (Exception ex) {

            devNotificaciones.error("Obtener dirección de cotizacion de la operación: " + numero_operacion, ex);

            return null;
            }
        }

    public model_direccionesFacturacion obtenerCotizacionDireccionFacturacion(string numero_operacion) {


        try {

            DataTable dtDireccion = new DataTable();

            dbConexion();

            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT * FROM cotizaciones_direccionFacturacion WHERE numero_operacion= @numero_operacion ");

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
            if (dtDireccion.Rows.Count >= 1) {
                model_direccionesFacturacion cotizacionDireccionFacturacion = new model_direccionesFacturacion();

                cotizacionDireccionFacturacion.id = int.Parse(dtDireccion.Rows[0]["id"].ToString());

                cotizacionDireccionFacturacion.calle = dtDireccion.Rows[0]["calle"].ToString();
                cotizacionDireccionFacturacion.numero = dtDireccion.Rows[0]["numero"].ToString();

                cotizacionDireccionFacturacion.razon_social = dtDireccion.Rows[0]["razon_social"].ToString();
                cotizacionDireccionFacturacion.rfc = dtDireccion.Rows[0]["rfc"].ToString();

                cotizacionDireccionFacturacion.colonia = dtDireccion.Rows[0]["colonia"].ToString();
                cotizacionDireccionFacturacion.delegacion_municipio = dtDireccion.Rows[0]["delegacion_municipio"].ToString();
                cotizacionDireccionFacturacion.estado = dtDireccion.Rows[0]["estado"].ToString();
                cotizacionDireccionFacturacion.codigo_postal = dtDireccion.Rows[0]["codigo_postal"].ToString();
                cotizacionDireccionFacturacion.pais = dtDireccion.Rows[0]["pais"].ToString();


                return cotizacionDireccionFacturacion;
                }
            return null;
            }
        catch (Exception ex) {

            devNotificaciones.error("Obtener dirección de facturación de cotizacion para la  operación: " + numero_operacion, ex);

            return null;
            }
        }
    /// <summary>
    /// Retorna los productos de una cotización: id, numero_parte, descripcion, marca, cantidad, unidad
    /// </summary>
    /// 
    public DataTable obtenerProductosCotizacion_min(string numero_operacion) {
        try {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT numero_parte, descripcion, marca, cantidad, unidad FROM cotizaciones_productos  
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
        catch (Exception ex) {

            devNotificaciones.error("Obtener productos mínimos de una cotización", ex);

            return null;
            }
        }

    /// <summary>
    ///  Obtiene el campo moneda del numéro de operación
    /// </summary>
    /// 
    public static string obtenerMonedaOperacion(string numero_operacion) {
        try {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT   monedaCotizacion  FROM cotizaciones_datosNumericos
                                 WHERE numero_operacion = @numero_operacion");

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;
                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;


                con.Open();
                string moneda = cmd.ExecuteScalar().ToString();

                return moneda;
                }
            }
        catch (Exception ex) {

            devNotificaciones.error("Obtener moneda de una cotización: "+  numero_operacion, ex);

            return null;
            }
        }
    /// <summary>
    /// Retorna todos campos de la tabla "cotizaciones_productos"
    /// </summary>
    /// 
    public DataTable obtenerProductosCotizacion_max(string numero_operacion) {
        try {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append(@"SELECT id, numero_operacion, usuario, orden, activo, tipo, fecha_creacion, numero_parte, descripcion, marca, unidad,  precio_unitario, cantidad, precio_total, stock1, stock1_fecha, stock2, stock2_fecha FROM cotizaciones_productos  
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
        catch (Exception ex) {

            devNotificaciones.error("Obtener productos de una cotización (todas las columnas)", ex);

            return null;
            }
        }

    /// <summary>
    /// Retorna X cantidad de productos de una cotización: id, numero_parte, descripcion, marca, cantidad, unidad
    /// </summary>
    /// 
    public DataTable obtenerProductosCotizacion_min(string numero_operacion, int numeroProductos) {
        try {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");
                query.Append(@"SELECT TOP(@numeroProductos) numero_parte, descripcion, marca, cantidad, unidad FROM cotizaciones_productos 
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
        catch (Exception ex) {

            devNotificaciones.error("Obtener productos mínimos de una cotización", ex);

            return null;
            }
        }

    /// <summary>
    /// Retorna el número total de productos de una cotización|
    /// </summary>
    /// 
    public int obtenerCantidadProductosCotizacion(string numero_operacion) {
        try {
            dbConexion();

            using (con) {

                StringBuilder query = new StringBuilder();
                query.Append("SET LANGUAGE English; ");
                query.Append(@"SELECT COUNT(*)  FROM cotizaciones_productos 
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

        catch (Exception ex) {
            devNotificaciones.error("Obtener productos mínimos de una cotización", ex);
            return 0;
            }
        }

    static public bool actualizarComentarioCotizacion(string numero_operacion, string comentario) {

        try {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;
            using (con) {


                StringBuilder query = new StringBuilder();

            


                query.Append("SET LANGUAGE English; ");

                query.Append("UPDATE cotizaciones_datos SET ");
                query.Append(" comentarios =  @comentario");
                query.Append(" WHERE numero_operacion = @numero_operacion ");
 

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@comentario", SqlDbType.NVarChar, 1500);
                cmd.Parameters["@comentario"].Value = comentario;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query.ToString();
             

                con.Open();

                cmd.ExecuteNonQuery();
                return true;
                }
            }
        catch (Exception ex) {
           
            devNotificaciones.error("Actualizar cotización comentarios", ex);
            return false;
            }
    }
    
    /// <summary>
    /// Actualiza los estatus de una cotización|
    /// </summary>
    static public bool actualizarEstatusCotizacion(string numero_operacion, int idEstatus) {

        try {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;
            using (con) {
                StringBuilder query = new StringBuilder();
  
                query.Append("SET LANGUAGE English; ");

                query.Append("UPDATE cotizaciones_datos SET ");
                query.Append(" idEstatus =  @idEstatus");
                query.Append(" WHERE numero_operacion = @numero_operacion ");


                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@idEstatus", SqlDbType.Int);
                cmd.Parameters["@idEstatus"].Value = idEstatus;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query.ToString();


                con.Open();

                cmd.ExecuteNonQuery();
                return true;
            }
        }
        catch (Exception ex) {

            devNotificaciones.error("Actualizar estatus de cotización", ex);
            return false;
        }
    }
    public string crearCotizacion_datos(usuarios usuario, model_cotizaciones_datos cotizacionDatos) {

        try {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; " +
                            "DECLARE @idUltimo nvarchar(10); SET @idUltimo = IIF((SELECT MAX(id) FROM cotizaciones_datos) IS NULL, 1, (SELECT MAX(id) FROM cotizaciones_datos)); ");
                query.Append("DECLARE @numero_operacion nvarchar(20); SET @numero_operacion =  'ca' + @idFecha + @idUltimo;");

                query.Append("INSERT INTO cotizaciones_datos  ");
                query.Append(" (nombre_cotizacion, numero_operacion, fecha_creacion, creada_por, mod_asesor, id_cliente, usuario_cliente,  ");
                query.Append(" cliente_nombre, cliente_apellido_paterno, cliente_apellido_materno, email, telefono, activo, comentarios, vigencia, vecesRenovada, conversionPedido) ");

                query.Append(" VALUES (@nombre_cotizacion, @numero_operacion, @fecha_creacion, @creada_por, @mod_asesor, @id_cliente, @usuario_cliente, ");
                query.Append(" @cliente_nombre, @cliente_apellido_paterno, @cliente_apellido_materno, @email, @telefono, @activo, @comentarios, @vigencia, 0, @conversionPedido);");

                query.Append("SELECT @numero_operacion ");

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.Add("@idFecha", SqlDbType.NVarChar, 20);
                cmd.Parameters["@idFecha"].Value = utilidad_fechas.AAAMMDD();

                cmd.Parameters.Add("@nombre_cotizacion", SqlDbType.NVarChar, 60);
                if (cotizacionDatos.nombre_cotizacion == null) cmd.Parameters["@nombre_cotizacion"].Value = utilidad_fechas.AAAMMDD() + " - Sin nombre";
                else cmd.Parameters["@nombre_cotizacion"].Value = textTools.lineSimple(cotizacionDatos.nombre_cotizacion);

                cmd.Parameters.Add("@fecha_creacion", SqlDbType.DateTime);
                cmd.Parameters["@fecha_creacion"].Value = cotizacionDatos.fecha_creacion;

                cmd.Parameters.Add("@creada_por", SqlDbType.NVarChar, 60);
                cmd.Parameters["@creada_por"].Value = cotizacionDatos.creada_por;

                cmd.Parameters.Add("@mod_asesor", SqlDbType.Int);
                cmd.Parameters["@mod_asesor"].Value = cotizacionDatos.mod_asesor;

                cmd.Parameters.Add("@id_cliente", SqlDbType.NVarChar);
                if (cotizacionDatos.id_cliente == null) cmd.Parameters["@id_cliente"].Value = DBNull.Value;
                else cmd.Parameters["@id_cliente"].Value = cotizacionDatos.id_cliente;


                cmd.Parameters.Add("@cliente_nombre", SqlDbType.NVarChar, 20);
                if (cotizacionDatos.cliente_nombre == null) cmd.Parameters["@cliente_nombre"].Value = DBNull.Value;
                else cmd.Parameters["@cliente_nombre"].Value = textTools.lineSimple(cotizacionDatos.cliente_nombre);

                cmd.Parameters.Add("@cliente_apellido_materno", SqlDbType.NVarChar, 20);
                if (cotizacionDatos.cliente_apellido_materno == null) cmd.Parameters["@cliente_apellido_materno"].Value = DBNull.Value;
                else cmd.Parameters["@cliente_apellido_materno"].Value = textTools.lineSimple(cotizacionDatos.cliente_apellido_materno);

                cmd.Parameters.Add("@cliente_apellido_paterno", SqlDbType.NVarChar, 20);
                if (cotizacionDatos.cliente_apellido_paterno == null) cmd.Parameters["@cliente_apellido_paterno"].Value = DBNull.Value;
                else cmd.Parameters["@cliente_apellido_paterno"].Value = textTools.lineSimple(cotizacionDatos.cliente_apellido_paterno);

                cmd.Parameters.Add("@usuario_cliente", SqlDbType.NVarChar, 60);
                cmd.Parameters["@usuario_cliente"].Value = cotizacionDatos.usuario_cliente;

                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 60);
                cmd.Parameters["@email"].Value = textTools.lineSimple(cotizacionDatos.email);

                cmd.Parameters.Add("@telefono", SqlDbType.NVarChar, 50);
                if (cotizacionDatos.telefono == null) cmd.Parameters["@telefono"].Value = DBNull.Value;
                else cmd.Parameters["@telefono"].Value = textTools.lineSimple(cotizacionDatos.telefono);

                cmd.Parameters.Add("@celular", SqlDbType.NVarChar, 50);
                if (cotizacionDatos.celular == null) cmd.Parameters["@celular"].Value = DBNull.Value;
                else cmd.Parameters["@celular"].Value = textTools.lineSimple(cotizacionDatos.celular);

                cmd.Parameters.Add("@activo", SqlDbType.Int);
                cmd.Parameters["@activo"].Value = cotizacionDatos.activo;

                cmd.Parameters.Add("@comentarios", SqlDbType.NVarChar, 600);
                if (cotizacionDatos.comentarios == null) cmd.Parameters["@comentarios"].Value = DBNull.Value;
                else cmd.Parameters["@comentarios"].Value = textTools.lineMulti(cotizacionDatos.comentarios);

                cmd.Parameters.Add("@vigencia", SqlDbType.Int);
                cmd.Parameters["@vigencia"].Value = cotizacionDatos.vigencia;

                cmd.Parameters.Add("@conversionPedido", SqlDbType.Int);
                cmd.Parameters["@conversionPedido"].Value = 0;

                con.Open();

                return cmd.ExecuteScalar().ToString();
                }
            }
        catch (Exception ex) {
            devNotificaciones.error("Crear datos de cotización", ex);
            return null;
            }
        }
    static public void deshabilitaCotizacion(usuarios usuario, model_cotizaciones_datos cotizacionDatos) {

        try {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;
            using (con) {


                StringBuilder query = new StringBuilder();

             


                query.Append("SET LANGUAGE English; ");

                query.Append("UPDATE cotizaciones_datos SET ");
                query.Append(" activo = 0");
                query.Append(" WHERE numero_operacion = @numero_operacion ");

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

               
                cmd.Parameters.Add("@activo", SqlDbType.Int);
                cmd.Parameters["@activo"].Value = cotizacionDatos.activo;

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                con.Open();

              cmd.ExecuteNonQueryAsync();
                }
            }
        catch (Exception ex) {
            devNotificaciones.error("Deshabilitar cotización", ex);
           
            }
        }
    /// <summary>
    /// Actualiza datos de contacto de cotización: email cliente, nombre, apellido paterno, apellido materno, telefono y celular. 
    /// </summary>
    /// 
    public string actualizarCotizacion_datosContacto(string numero_operacion, model_cotizaciones_datos cotizacionDatos) {

        try {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append("UPDATE cotizaciones_datos SET ");
                query.Append(" cliente_nombre = @cliente_nombre, cliente_apellido_paterno = @cliente_apellido_paterno, " +
                                "cliente_apellido_materno = @cliente_apellido_materno, email = @email, telefono = @telefono, celular = @celular ");
                query.Append(" WHERE numero_operacion = @numero_operacion ");

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@cliente_nombre", SqlDbType.NVarChar, 20);
                if (cotizacionDatos.cliente_nombre == null) cmd.Parameters["@cliente_nombre"].Value = DBNull.Value;
                else cmd.Parameters["@cliente_nombre"].Value = textTools.lineSimple(cotizacionDatos.cliente_nombre);

                cmd.Parameters.Add("@cliente_apellido_materno", SqlDbType.NVarChar, 20);
                if (cotizacionDatos.cliente_apellido_materno == null) cmd.Parameters["@cliente_apellido_materno"].Value = DBNull.Value;
                else cmd.Parameters["@cliente_apellido_materno"].Value = textTools.lineSimple(cotizacionDatos.cliente_apellido_materno);

                cmd.Parameters.Add("@cliente_apellido_paterno", SqlDbType.NVarChar, 20);
                if (cotizacionDatos.cliente_apellido_paterno == null) cmd.Parameters["@cliente_apellido_paterno"].Value = DBNull.Value;
                else cmd.Parameters["@cliente_apellido_paterno"].Value = textTools.lineSimple(cotizacionDatos.cliente_apellido_paterno);

                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 60);
                cmd.Parameters["@email"].Value = textTools.lineSimple(cotizacionDatos.email);

                cmd.Parameters.Add("@telefono", SqlDbType.NVarChar, 50);
                if (cotizacionDatos.telefono == null) cmd.Parameters["@telefono"].Value = DBNull.Value;
                else cmd.Parameters["@telefono"].Value = textTools.lineSimple(cotizacionDatos.telefono);

                cmd.Parameters.Add("@celular", SqlDbType.NVarChar, 50);
                if (cotizacionDatos.celular == null) cmd.Parameters["@celular"].Value = DBNull.Value;
                else cmd.Parameters["@celular"].Value = textTools.lineSimple(cotizacionDatos.celular);

                con.Open();
                cmd.ExecuteScalar();

                return "";
                }
            }
        catch (Exception ex) {
            devNotificaciones.error("Actualizar datos de contacto de la cotización:" + numero_operacion, ex);
            return null;
            }
        }

    /// <summary>
    /// Actualiza todos los datos de dirección de envío de una cotización  es importante validar si es nueva o se actualiza, se recomienda usar el método "
    /// </summary>
    /// 
    public string actualizarCotizacion_direccionEnvio(string numero_operacion, model_direccionesEnvio direccionEnvio) {

        try {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append("UPDATE cotizaciones_direccionEnvio SET ");
                query.Append(" calle = @calle, numero = @numero, colonia = @colonia, delegacion_municipio = @delegacion_municipio, estado = @estado," +
                                " ciudad = @ciudad, codigo_postal = @codigo_postal, pais = @pais, referencias = @referencias");

                query.Append(" WHERE numero_operacion = @numero_operacion ");

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

                cmd.Parameters.Add("@estado", SqlDbType.NVarChar, 35);
                if (direccionEnvio.estado == null) cmd.Parameters["@estado"].Value = DBNull.Value;
                else cmd.Parameters["@estado"].Value = textTools.lineSimple(direccionEnvio.estado);

                cmd.Parameters.Add("@ciudad", SqlDbType.NVarChar, 60);
                if (direccionEnvio.ciudad == null) cmd.Parameters["@ciudad"].Value = DBNull.Value;
                else cmd.Parameters["@ciudad"].Value = textTools.lineSimple(direccionEnvio.ciudad);

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
        catch (Exception ex) {
            devNotificaciones.error("Modificar dirección de envio de operación: " + numero_operacion, ex);
            return null;
            }
        }

    /// <summary>
    /// Actualiza todos los datos de dirección de facturación de una cotización es importante validar si es nueva o se actualiza, se recomienda usar el método "
    /// </summary>
    /// 
    public string actualizarCotizacion_direccionFacturacion(string numero_operacion, model_direccionesFacturacion direccionFacturacion) {

        try {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append("UPDATE cotizaciones_direccionFacturacion  SET ");
                query.Append(" razon_social = @razon_social, rfc = @rfc, calle = @calle, numero = @numero, colonia = @colonia, " +
                    "delegacion_municipio = @delegacion_municipio, estado = @estado," +
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
        catch (Exception ex) {
            devNotificaciones.error("Modificar dirección de facturación de operación: " + numero_operacion, ex);
            return null;
            }
        }

    /// <summary>
    /// Verifica que exista o no una dirección de envío para una cotización en la tabla cotizaciones_direccionEnvio y realiza un UPDATE o un CREATE de acuerdo al caso.
    /// </summary>
    public bool cotizacionDireccionEnvio(string numero_operacion, model_direccionesEnvio direccion) {

  
        if (!String.IsNullOrEmpty(numero_operacion) || direccion != null) {

            try {
                int cantidad = 0;
                string resultado = string.Empty;

                dbConexion();
                using (con) {

                    StringBuilder query = new StringBuilder();

                    query.Append("SET LANGUAGE English; ");

                    query.Append("SELECT COUNT(*) FROM cotizaciones_direccionEnvio  ");
                    query.Append(" WHERE numero_operacion = @numero_operacion ");

                    cmd.CommandText = query.ToString();
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                    cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                    con.Open();
                     cantidad =  int.Parse(cmd.ExecuteScalar().ToString());

                   
                    }

                // Si es igual a 0 quiere decir que no existe dirección, entonces hará un CREATE
                if (cantidad == 0) 
                    resultado = crearCotizacion_direccionEnvio(numero_operacion, direccion);
                    
                // Si es igual a 1 quiere decir que ya hay direccioón existente, entonces hará un UPDATE
                 else if (cantidad == 1)
                    resultado = actualizarCotizacion_direccionEnvio(numero_operacion, direccion);
                
                if (resultado != null) return true; else return false;
                }
            catch (Exception ex) {
                devNotificaciones.error("Actualizar/crear direccion de envío de cotización: " + numero_operacion, ex);
                return false;
                }
            } else {

            return false;
            }
        }

    /// <summary>
    /// Verifica que exista o no una dirección de facturación para una cotización en la tabla cotizaciones_direccionFacturacion y realiza un UPDATE o un CREATE de acuerdo al caso.
    /// </summary>
    public bool cotizacionDireccionFacturacion(string numero_operacion, model_direccionesFacturacion direccion) {


        if (!String.IsNullOrEmpty(numero_operacion) || direccion != null) {

            try {
                int cantidad = 0;
                string resultado = string.Empty;

                dbConexion();
                using (con) {

                    StringBuilder query = new StringBuilder();

                    query.Append("SET LANGUAGE English; ");

                    query.Append("SELECT COUNT(*) FROM cotizaciones_direccionFacturacion ");
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
                    resultado = crearCotizacion_direccionFacturacion(numero_operacion, direccion);

                // Si es igual a 1 quiere decir que ya hay direccioón existente, entonces hará un UPDATE
                else if (cantidad == 1)
                    resultado = actualizarCotizacion_direccionFacturacion(numero_operacion, direccion);

                if (resultado != null) return true; else return false;
                }
            catch (Exception ex) {
                devNotificaciones.error("Actualizar/crear direccion de envío de cotización: " + numero_operacion, ex);
                return false;
                }
            } else {

            return false;
            }
        }
    /// <summary>
    /// Crear(inserta) una dirección de envío, es importante validar si es nueva o se actualiza, se recomienda usar el método "cotizacionDireccionFacturacion"
    /// </summary>
    /// 
    public string crearCotizacion_direccionEnvio(string numero_operacion, model_direccionesEnvio direccionEnvio) {

        try {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append("INSERT INTO cotizaciones_direccionEnvio ");
                query.Append("(calle, numero, colonia, delegacion_municipio, ciudad, estado," +
                                " codigo_postal, pais, referencias,  numero_operacion)");

                query.Append("VALUES (@calle, @numero, @colonia, @delegacion_municipio, @ciudad,  @estado," +
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

                cmd.Parameters.Add("@ciudad", SqlDbType.NVarChar, 60);
                if (direccionEnvio.ciudad == null) cmd.Parameters["@ciudad"].Value = DBNull.Value;
                else cmd.Parameters["@ciudad"].Value = textTools.lineSimple(direccionEnvio.ciudad);


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
                else cmd.Parameters["@referencias"].Value = textTools.lineSimple(direccionEnvio.codigo_postal);

                con.Open();
                cmd.ExecuteScalar();

                return "";
                }
            }
        catch (Exception ex) {
            devNotificaciones.error("Crear dirección de envio de operación: " + numero_operacion, ex);
            return null;
            }
        }

    /// <summary>
    /// Crear(inserta) una dirección de envío, es importante validar si es nueva o se actualiza, se recomienda usar el método "cotizacionDireccionFacturacion"
    /// </summary>
    /// 
    public string crearCotizacion_direccionFacturacion(string numero_operacion, model_direccionesFacturacion direccionFacturacion) {

        try {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");

                query.Append("INSERT INTO cotizaciones_direccionFacturacion ");
                query.Append("(razon_social, rfc, calle, numero, colonia, delegacion_municipio, estado," +
                                " codigo_postal, pais,  numero_operacion)");

                query.Append("VALUES (@razon_social, @rfc, @calle, @numero, @colonia, @delegacion_municipio, @estado, " +
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
        catch (Exception ex) {
            devNotificaciones.error("Crear dirección de facturación de operación: " + numero_operacion, ex);
            return null;
            }
        }
    public string crearCotizacionDeCarrito_productos(usuarios usuario, string numero_operacion, model_impuestos impuestos, model_envios envio)
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
                      ,[activo]
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
                  FROM [carrito_productos]
		
		"; 
        // carritotToMXN
             string carritotToMXN = @"
                -- Cotización/Carrito a MXN
                -- Convierte todos los productos del carrito a MXN para pasarlo a una cotización
                SELECT 
                       numero_operacion = @numero_operacion
                      ,[usuario]
                      ,[activo]
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
                  FROM [carrito_productos]
		"; 

        // Depende la moneda seleccionada son los querys que se usaran para crear la cotización en USD o MXN
        if (monedaCotizacion == "USD") 
            {
            subtotal = subtotalUSD;
            carrito = carritotToUSD;
            } else if (monedaCotizacion == "MXN")
            {
            subtotal = subtotalMXN;
            carrito = carritotToMXN;
            }


        try
            {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English;");
                query.Append("DECLARE @subtotal float; SET @subtotal =  ("+ subtotal + " WHERE usuario = @usuario );");
                query.Append("DECLARE @impuestos float; SET @impuestos =  (@subtotal + @envio) * (@valorImpuestos / 100);");
                query.Append("DECLARE @total float; SET @total =  (@subtotal + @impuestos + (@envio * (@valorImpuestos / 100)));");

                query.Append("INSERT INTO cotizaciones_datosNumericos  ");
                query.Append(" (numero_operacion, monedaCotizacion, tipo_cambio, fecha_tipo_cambio, subtotal, envio, ");
                query.Append(" metodoEnvio, monedaEnvio, impuestos, nombreImpuestos, total) ");

                query.Append(" VALUES (@numero_operacion, @monedaCotizacion, @tipo_cambio, @fecha_tipo_cambio, @subtotal, @envio, ");
                query.Append(" @metodoEnvio, @monedaEnvio, @impuestos, @nombreImpuestos, @total);");

                query.Append(@" INSERT INTO cotizaciones_productos (
                                   numero_operacion, usuario, activo, tipo, fecha_creacion, numero_parte, descripcion, marca,
                                   unidad, precio_unitario, cantidad, precio_total
                                ) 

                                " + carrito+ " WHERE usuario = @usuario");

                cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 60);
                cmd.Parameters["@usuario"].Value = usuario.email;

                cmd.Parameters.Add("@valorImpuestos", SqlDbType.Float);
                cmd.Parameters["@valorImpuestos"].Value = impuestos.valor;

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@monedaCotizacion", SqlDbType.NVarChar, 5);
                cmd.Parameters["@monedaCotizacion"].Value = monedaCotizacion;

                cmd.Parameters.Add("@tipo_cambio", SqlDbType.Float);
                cmd.Parameters["@tipo_cambio"].Value = tipoDeCambio;

                cmd.Parameters.Add("@fecha_tipo_cambio", SqlDbType.DateTime);
                cmd.Parameters["@fecha_tipo_cambio"].Value = fechaTipoDeCambio;

                cmd.Parameters.Add("@envio", SqlDbType.Float);
                cmd.Parameters["@envio"].Value = envio.costoEnvio;

                cmd.Parameters.Add("@metodoEnvio", SqlDbType.NVarChar,15);
                cmd.Parameters["@metodoEnvio"].Value = envio.nombre;

                cmd.Parameters.Add("@monedaEnvio", SqlDbType.NVarChar,5);
                cmd.Parameters["@monedaEnvio"].Value = envio.monedaEnvio;

                cmd.Parameters.Add("@nombreImpuestos", SqlDbType.NVarChar, 20);
                cmd.Parameters["@nombreImpuestos"].Value = impuestos.nombre;

                cmd.CommandText = query.ToString();

                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();

                return "";
                }
            }
        catch (Exception ex) {
            devNotificaciones.error("Crear productos de cotización y datos numéricos", ex);
            return null;
            }
       
        }
    public string crearCotizacionVacia_productos(usuarios usuario, string numero_operacion, model_impuestos impuestos, model_envios envio) {
 
         

        try {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English;");
                

                query.Append("INSERT INTO cotizaciones_datosNumericos  ");
                query.Append(" (numero_operacion, monedaCotizacion, tipo_cambio, fecha_tipo_cambio, subtotal, envio, ");
                query.Append(" metodoEnvio, monedaEnvio, impuestos, nombreImpuestos, total) ");

                query.Append(" VALUES (@numero_operacion, @monedaCotizacion, @tipo_cambio, @fecha_tipo_cambio, 0, @envio, ");
                query.Append(" @metodoEnvio, @monedaEnvio, @valorImpuestos, @nombreImpuestos, 0);");

              

                cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 60);
                cmd.Parameters["@usuario"].Value = usuario.email;

          

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@monedaCotizacion", SqlDbType.NVarChar, 5);
                cmd.Parameters["@monedaCotizacion"].Value = monedaCotizacion;

                cmd.Parameters.Add("@tipo_cambio", SqlDbType.Float);
                cmd.Parameters["@tipo_cambio"].Value = tipoDeCambio;

                cmd.Parameters.Add("@fecha_tipo_cambio", SqlDbType.DateTime);
                cmd.Parameters["@fecha_tipo_cambio"].Value = fechaTipoDeCambio;

                cmd.Parameters.Add("@envio", SqlDbType.Float);
                cmd.Parameters["@envio"].Value = envio.costoEnvio;

                cmd.Parameters.Add("@metodoEnvio", SqlDbType.NVarChar, 15);
                cmd.Parameters["@metodoEnvio"].Value = envio.nombre;

                cmd.Parameters.Add("@monedaEnvio", SqlDbType.NVarChar, 5);
                cmd.Parameters["@monedaEnvio"].Value = envio.monedaEnvio;

                cmd.Parameters.Add("@valorImpuestos", SqlDbType.Money);
                cmd.Parameters["@valorImpuestos"].Value = impuestos.valor;

                cmd.Parameters.Add("@nombreImpuestos", SqlDbType.NVarChar, 20);
                cmd.Parameters["@nombreImpuestos"].Value = impuestos.nombre;

          

                cmd.CommandText = query.ToString();

                cmd.CommandType = CommandType.Text;

                con.Open();
                cmd.ExecuteNonQuery();

                return "";
                }
            }
        catch (Exception ex) {
            devNotificaciones.error("Crear productos de cotización VACÍA y datos numéricos", ex);
            return null;
            }

        }

    public string editarNombrecotizacion(int idcotizacion, string nombreCotizacion) {

        try {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; " +
                            "UPDATE cotizaciones_datos ");
                query.Append(" SET nombre_cotizacion=@nombre_cotizacion WHERE id= @idcotizacion");


                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@idcotizacion", SqlDbType.Int);
                cmd.Parameters["@idcotizacion"].Value = idcotizacion;
                cmd.Parameters.Add("@nombre_cotizacion", SqlDbType.NVarChar, 60);
                if (nombreCotizacion == null || nombreCotizacion.Length < 3) cmd.Parameters["@nombre_cotizacion"].Value ="Sin nombre";
                else cmd.Parameters["@nombre_cotizacion"].Value = textTools.lineSimple(nombreCotizacion);


           
                con.Open();
                return cmd.ExecuteNonQuery().ToString();
                }
            }
        catch (Exception ex) {
            devNotificaciones.error("Editar nombre de cotización", ex);
            return null;
            }
        }

    public static string actualizarEnvio(decimal envio, string metodoEnvio, string numero_operacion) {

        try {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; " +
                            "UPDATE cotizaciones_datosNumericos ");
                query.Append(" SET metodoEnvio=@metodoEnvio, envio=@envio WHERE numero_operacion= @numero_operacion");


                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@envio", SqlDbType.Money);
                cmd.Parameters["@envio"].Value = envio;

                cmd.Parameters.Add("@metodoEnvio", SqlDbType.NVarChar,15);
                cmd.Parameters["@metodoEnvio"].Value = metodoEnvio;

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar,20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                con.Open();
                return cmd.ExecuteNonQuery().ToString();
                }
            }
        catch (Exception ex) {
            devNotificaciones.error("Actualizar envio de cotización", ex);
            return null;
            }
        }

    public static string actualizarEnvio(decimal envio, string metodoEnvio, string numero_operacion, string EnvioNota) {

        try {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; " +
                            "UPDATE cotizaciones_datosNumericos ");
                query.Append(" SET metodoEnvio=@metodoEnvio, envio=@envio,  EnvioNota=@EnvioNota WHERE numero_operacion= @numero_operacion");


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
        catch (Exception ex) {
            devNotificaciones.error("Actualizar envio de cotización", ex);
            return null;
        }
    }
    /// <summary>
    /// Inserta el asesor base y también grupo base, tenga o no asignado, si no lo tiene insertara valores null
    /// </summary>
    /// 
    static public string insertarAsesoresBase(string numero_operacion, string asesorBaseUsuario, string asesorBaseGrupo) {
        return "";
        try {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; ");
                query.Append("IF NOT EXISTS(SELECT numero_operacion FROM cotizaciones_asesorBase WHERE numero_operacion = @numero_operacion) BEGIN ");
               
                query.Append("INSERT INTO cotizaciones_asesorBase ");
                query.Append("(asesorBaseUsuario, asesorBaseGrupo, numero_operacion)");

                query.Append("VALUES (@asesorBaseUsuario, @asesorBaseGrupo, @numero_operacion); SELECT 'true';");

                query.Append("END ELSE BEGIN SELECT 'false'; END");

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@asesorBaseUsuario", SqlDbType.NVarChar, 60);
                if( string.IsNullOrEmpty(asesorBaseUsuario)) cmd.Parameters["@asesorBaseUsuario"].Value = DBNull.Value;
                else cmd.Parameters["@asesorBaseUsuario"].Value = asesorBaseUsuario ;


                cmd.Parameters.Add("@asesorBaseGrupo", SqlDbType.NVarChar, 20);
                if (string.IsNullOrEmpty(asesorBaseGrupo)) cmd.Parameters["@asesorBaseGrupo"].Value = DBNull.Value;
                else cmd.Parameters["@asesorBaseGrupo"].Value = asesorBaseGrupo;


                con.Open();

               string resultado = cmd.ExecuteScalar().ToString();
                if (resultado == "true") return ""; else return null;
                }
            }
        catch (Exception ex) {
            devNotificaciones.error(string.Format("Insertar asesor base en: cotización: {0}, usuario: {1}, grupoBase:  {2} ", numero_operacion, asesorBaseUsuario, asesorBaseGrupo), ex);
            return null;
            }
        }


    public string actualizarVigencia(string numero_operacion, int vigencia) {

        try {
            dbConexion();
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; " +
                            "UPDATE cotizaciones_datos ");
                query.Append(" SET vigencia=@vigencia WHERE numero_operacion= @numero_operacion");


                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@vigencia", SqlDbType.Int);
                cmd.Parameters["@vigencia"].Value = vigencia;

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                 cmd.Parameters["@numero_operacion"].Value = numero_operacion;



                con.Open();
                return cmd.ExecuteNonQuery().ToString();
                }
            }
        catch (Exception ex) {
            devNotificaciones.error("Actualizar vigencia, operación: "+ numero_operacion, ex);
            return null;
            }
        }
    /// <summary>
    /// Inserta el número de pedido en [numero_operacion_pedido] creado a partir de dicha cotización y establece el valor en 1 en el campo  [conversionPedido]
    /// </summary>
    /// 
    public static bool cotizacionUpdateToPedido(string numero_operacion_cotizacion, string numero_operacion_pedido) {

        try {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; " +
                            "UPDATE cotizaciones_datos ");
                query.Append(" SET conversionPedido = 1, numero_operacion_pedido=@numero_operacion_pedido WHERE numero_operacion= @numero_operacion_cotizacion");


                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@numero_operacion_cotizacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion_cotizacion"].Value = numero_operacion_cotizacion;

                cmd.Parameters.Add("@numero_operacion_pedido", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion_pedido"].Value = numero_operacion_pedido;

                con.Open();
                 cmd.ExecuteNonQuery();
                return true;
                }
            }
        catch (Exception ex) {
            devNotificaciones.error("Actualizar cotización convertida en pedido, operación: " + numero_operacion_cotizacion, ex);
            return false;
            }
    }
    public static bool actualizar_tipo_cotizacion(string numero_operacion, string tipo_cotizacion) {

        try {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;
            using (con) {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; " +
                            "UPDATE cotizaciones_datos ");
                query.Append(" SET tipo_cotizacion = @tipo_cotizacion WHERE numero_operacion= @numero_operacion");


                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@tipo_cotizacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@tipo_cotizacion"].Value = tipo_cotizacion;

                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }
        catch (Exception ex) {
            devNotificaciones.error("Actualizar tipo_cotizacion, operación: " + numero_operacion, ex);
            return false;
        }
    }
    static public Tuple<bool, List<string>> renovarCotizacion(string numero_operacion) {

        /* - Se requiere actualizar la fecha de creación (sobre escribe la inicial) y se aumenta el contador en [cotizaciones_datos]
          - Se inserta un registro en la tabla [cotizaciones_renovaciones] con la fecha que tenia inicialmente, la fecha actual (de renovación) y quien la renueva. 
         */
        // Lista que usaremos para almacenar los productos que tengan error al actualizarce.

        List<string> productosNOActualizados = new List<string>();
        bool resultadoMetodo = new bool();
      
            // Obtenemos datos de la cotización
             cotizaciones obtener = new cotizaciones();
            DataTable dtOperacion = obtener.obtenerCotizacionDatos_min(numero_operacion);
            DataTable dtProductos = cotizacionesProductos.obtenerProductos(numero_operacion);

            // Obtenemos la fecha original de la cotización próxima a actualizar
            DateTime fecha_creacion_anterior = DateTime.Parse(dtOperacion.Rows[0]["fecha_creacion"].ToString());

            DateTime fecha_creacion_nueva = utilidad_fechas.obtenerCentral();

            foreach (DataRow r in dtProductos.Rows) {

                string numero_parte = r["numero_parte"].ToString();
                string cantidad = r["cantidad"].ToString();
                string moneda = r["monedaCotizacion"].ToString();
                 string tipo = r["tipo"].ToString();

          

            if(tipo == "1")

            {   // Si el producto solo esta disponible para visualización este ya no se actualiza y se notifica.
                if (!productosTienda.productoVisualización(numero_parte))
                {
                    operacionesProductos actualizar = new operacionesProductos("cotizacion", "", numero_operacion, numero_parte, cantidad, moneda);
                actualizar.agregarProductoAsync();
                bool resultadoActualizacionProducto = actualizar.resultado_operacion;

                // Si hubo un error en el resultado agregamos dicho producto al listado
                if(resultadoActualizacionProducto == false) {
                        productosNOActualizados.Add("<strong>" + numero_parte + "</strong>, error al actualizar producto, intenta más tarde o elimínela."); 
                    }
                }

                else
                {

                    productosNOActualizados.Add("<strong>" + numero_parte + "</strong>, producto ya no disponile para venta.");
                }
            }
           
        }

        // Comenzamos a agregar los valores de renovación en "cotizaciones_datos"

        SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;
            using (con) {

                StringBuilder query = new StringBuilder();

                // Actualizamos la cotización
                query.Append("SET LANGUAGE English; " +
                             "UPDATE cotizaciones_datos ");
                query.Append(" SET fecha_creacion=@fecha_creacion_nueva,  vecesRenovada= vecesRenovada + 1 WHERE numero_operacion= @numero_operacion; ");

            query.Append(" UPDATE cotizaciones_datosNumericos SET fecha_tipo_cambio=@fecha_creacion_nueva, tipo_cambio= @tipo_cambio  WHERE numero_operacion= @numero_operacion");
            // Insertamos el registro en historial de renovaciones
            query.Append(" INSERT INTO cotizaciones_renovaciones (numero_operacion, fecha_renovacion, renovada_por, fecha_creacion_anterior) ");
                query.Append("                               VALUES (@numero_operacion, @fecha_creacion_nueva, @renovada_por, @fecha_creacion_anterior);");


                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@fecha_creacion_nueva", SqlDbType.DateTime);
                cmd.Parameters["@fecha_creacion_nueva"].Value = fecha_creacion_nueva;

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;


                cmd.Parameters.Add("@fecha_creacion_anterior", SqlDbType.DateTime);
                cmd.Parameters["@fecha_creacion_anterior"].Value = fecha_creacion_anterior;

            cmd.Parameters.Add("@tipo_cambio", SqlDbType.Decimal);
            cmd.Parameters["@tipo_cambio"].Value = operacionesConfiguraciones.obtenerTipoDeCambio();

            cmd.Parameters.Add("@renovada_por", SqlDbType.NVarChar, 60);
                cmd.Parameters["@renovada_por"].Value = HttpContext.Current.User.Identity.Name;
                
                con.Open();
            try {
                cmd.ExecuteNonQuery();
                resultadoMetodo = true;
                }
            catch (Exception ex) {
                devNotificaciones.error(string.Format("Renovar cotización: {0} ", numero_operacion), ex);
                resultadoMetodo = false;
                }
            
            }

        return Tuple.Create(resultadoMetodo, productosNOActualizados);
        }

    /// <summary>
    /// 20201105 Obtiene el valor del campo [Calculo_Costo_Envio] de la tabla [cotizaciones_datos] el cuál determina si la operación permite calcular el costo
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

                string query = "SELECT Calculo_Costo_Envio FROM cotizaciones_datos WHERE numero_operacion=@numero_operacion";
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
    /// 20201105 Obtiene el valor del campo [metodoEnvio] de la tabla [cotizaciones_datosNumericos] 
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

                string query = "SELECT metodoEnvio FROM cotizaciones_datosNumericos WHERE numero_operacion=@numero_operacion";
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
                string query = "UPDATE cotizaciones_datos SET Calculo_Costo_Envio=@Estatus WHERE numero_operacion=@numero_operacion;";

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
    /// Crear(inserta) una dirección de envío, es importante validar si es nueva o se actualiza, se recomienda usar el método "cotizacionDireccionFacturacion"
    /// </summary>
    /// 
    public static string AgregarDireccionEnvioACotizacion(string numero_operacion, direcciones_envio direccionEnvio)
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

                query.Append("INSERT INTO cotizaciones_direccionEnvio ");
                query.Append("(calle, numero, colonia, delegacion_municipio, ciudad, estado," +
                                " codigo_postal, pais, referencias,  numero_operacion)");

                query.Append("VALUES (@calle, @numero, @colonia, @delegacion_municipio, @ciudad,  @estado," +
                             "  @codigo_postal, @pais,  @referencias, @numero_operacion)");


                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@numero_operacion"].Value = numero_operacion;

                cmd.Parameters.Add("@calle", SqlDbType.NVarChar );
                if (direccionEnvio.calle == null) cmd.Parameters["@calle"].Value = DBNull.Value;
                else cmd.Parameters["@calle"].Value = textTools.lineSimple(direccionEnvio.calle);

                cmd.Parameters.Add("@numero", SqlDbType.NVarChar );
                if (direccionEnvio.numero == null) cmd.Parameters["@numero"].Value = DBNull.Value;
                else cmd.Parameters["@numero"].Value = textTools.lineSimple(direccionEnvio.numero);

                cmd.Parameters.Add("@colonia", SqlDbType.NVarChar);
                if (direccionEnvio.colonia == null) cmd.Parameters["@colonia"].Value = DBNull.Value;
                else cmd.Parameters["@colonia"].Value = textTools.lineSimple(direccionEnvio.colonia);

                cmd.Parameters.Add("@ciudad", SqlDbType.NVarChar);
                if (direccionEnvio.ciudad == null) cmd.Parameters["@ciudad"].Value = DBNull.Value;
                else cmd.Parameters["@ciudad"].Value = textTools.lineSimple(direccionEnvio.ciudad);


                cmd.Parameters.Add("@delegacion_municipio", SqlDbType.NVarChar);
                cmd.Parameters["@delegacion_municipio"].Value = textTools.lineSimple(direccionEnvio.delegacion_municipio);

                cmd.Parameters.Add("@estado", SqlDbType.NVarChar);
                if (direccionEnvio.estado == null) cmd.Parameters["@estado"].Value = DBNull.Value;
                else cmd.Parameters["@estado"].Value = textTools.lineSimple(direccionEnvio.estado);

                cmd.Parameters.Add("@codigo_postal", SqlDbType.NVarChar);
                if (direccionEnvio.codigo_postal == null) cmd.Parameters["@codigo_postal"].Value = DBNull.Value;
                else cmd.Parameters["@codigo_postal"].Value = textTools.lineSimple(direccionEnvio.codigo_postal);

                cmd.Parameters.Add("@pais", SqlDbType.NVarChar);
                if (direccionEnvio.pais == null) cmd.Parameters["@pais"].Value = DBNull.Value;
                else cmd.Parameters["@pais"].Value = textTools.lineSimple(direccionEnvio.pais);


                cmd.Parameters.Add("@referencias", SqlDbType.NVarChar);
                if (direccionEnvio.referencias == null) cmd.Parameters["@referencias"].Value = DBNull.Value;
                else cmd.Parameters["@referencias"].Value = textTools.lineSimple(direccionEnvio.codigo_postal);

                con.Open();
                cmd.ExecuteScalar();

                return "";
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Crear dirección de envio de operación: " + numero_operacion, ex);
            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
public class CotizacionesProductosDatos {

    public cotizaciones_productos productos { get; set; }
    public productos_Datos datos { get; set; }
}
/// <summary>
/// Descripción breve de CotizacionesEF
/// </summary>
public class CotizacionesEF {
    public CotizacionesEF() {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    /// <summary>
    /// 20210607 CM - Obtiene los productos de una cotización por número de operación y su información
    /// </summary>
    public static List<CotizacionesProductosDatos> ObtenerProductosWithData(string numero_operacion) {


        try {

            using (var db = new tiendaEntities()) {


                var CotizacionProductos = db.cotizaciones_productos
                          .GroupJoin(
                db.productos_Datos,
                pro => pro.numero_parte,
                dat => dat.numero_parte,
                (pro, dat) => new { pro, dat })
                .SelectMany(x => x.dat.DefaultIfEmpty(),
                     (x, y) => new { pro = x.pro, dat = y })
   .Where(x => x.pro.numero_operacion == numero_operacion).GroupBy(x => new {
       x.pro,
       x.dat
   })
            .Select(g => new {
                g.Key.pro,
                g.Key.dat

            })
            .AsNoTracking()
            .ToList();


 
                return CotizacionProductos.Select(a => new CotizacionesProductosDatos() { productos = a.pro, datos = a.dat }).ToList();
            }
        }
        catch (Exception ex) {

            return null;
        }
    }

    /// <summary>
    /// 20210607 CM - Obtiene la información de montos $$ unicamente de los productos de una cotización por su número de operación
    /// </summary>
    public static decimal? ObtenerMontoTotalProductos(string numero_operacion) {


        try {

            using (var db = new tiendaEntities()) {
                var MontoTotalProductos = db.cotizaciones_productos
                     .AsNoTracking()
                    .Where(n => n.numero_operacion == numero_operacion)
                     .Sum(p => p.precio_total);


                return MontoTotalProductos;
            }
        }
        catch (Exception ex) {
            devNotificaciones.error($"Obtener monto total de productos de una cotización: {numero_operacion}", ex);
            return null;
        }
    }


    /// <summary>
    /// 20210608 CM - Obtiene la moneda de una operación por su numero_operacion
    /// </summary>
    public static string ObtenerMonedaOperacion(string numero_operacion) {


        try {

            using (var db = new tiendaEntities()) {
                var moneda = db.cotizaciones_datosNumericos
                     .AsNoTracking()

                     .Where(s => s.numero_operacion == numero_operacion)
                       .Select(u => u.monedaCotizacion)
                       .FirstOrDefault();

                return moneda;
            }
        }
        catch (Exception ex) {

            return null;
        }
    }


    public static async Task<json_respuestas> CrearCotizacionEnBlanco(usuarios usuario, string nombreCotizacion, string Moneda)
    {


        
        if (String.IsNullOrEmpty(nombreCotizacion) || nombreCotizacion.Length < 3)
        {
            nombreCotizacion = utilidad_fechas.AAAMMDD() + " Sin nombre";

        }


        model_cotizaciones_datos cotizacionDatos = new model_cotizaciones_datos();

        decimal tipoCambio = operacionesConfiguraciones.obtenerTipoDeCambio();
        cotizacionDatos.nombre_cotizacion = nombreCotizacion;
        cotizacionDatos.mod_asesor = usuarios.modoAsesorCotizacion();
        cotizacionDatos.id_cliente = usuario.idSAP;
        cotizacionDatos.usuario_cliente = usuario.email;
        cotizacionDatos.cliente_nombre = usuario.nombre;
        cotizacionDatos.cliente_apellido_paterno = usuario.apellido_paterno;
        cotizacionDatos.cliente_apellido_materno = usuario.apellido_materno;
        cotizacionDatos.email = usuario.email;
        cotizacionDatos.activo = 1;
       

        int vigencia = 1;
        // Vigencia en MXN = 1 día, USD = 30 días
        if (Moneda == "USD") vigencia = 30;
        cotizacionDatos.vigencia = vigencia;

        cotizaciones crear = new cotizaciones();
        crear.monedaCotizacion = Moneda;
        crear.fechaTipoDeCambio = utilidad_fechas.obtenerCentral();
        crear.tipoDeCambio = tipoCambio;



        string resultadoNumeroOperacion = crear.crearCotizacionVacia(usuario, cotizacionDatos);


        if (resultadoNumeroOperacion != null)
        {
            return new json_respuestas(true, "Cotización creada con éxito");


        }
        else
        {

            return new json_respuestas(false, "Error al crear la cotización");

        }
    }
}
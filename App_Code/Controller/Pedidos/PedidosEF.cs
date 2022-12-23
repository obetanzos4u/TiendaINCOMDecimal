using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Descripción breve de PedidosEF
/// </summary>

public class PedidosProductosDatos
{

    public pedidos_productos productos { get; set; }
    public productos_Datos datos { get; set; }




}
/// <summary>
/// Descripción breve de PedidosEF
/// </summary>

public class PedidosDTOModel
{

    public List<pedidos_productos> productos { get; set; }
    public pedidos_datos datos { get; set; }
    public pedidos_datosNumericos montos { get; set; }

    public pedidos_direccionEnvio direccionEnvio { get; set; }
    public pedidos_direccionFacturacion direccionFacturacion { get; set; }
}


public class PedidosEF
{
    public PedidosEF()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    /// <summary>
    /// Regresa con json_respuestas donde si es correcto, devuelve el tipo PedidosDTOModel en el atributo Response
    /// </summary>
    static public async Task<json_respuestas> ObtenerPedidoCompleto(string numero_operacion)
    {

        if (string.IsNullOrWhiteSpace(numero_operacion))
            return new json_respuestas(false, "No se ha especificado un numero de operación");

        var PedidoDatos = ObtenerDatos(numero_operacion);

        if (PedidoDatos == null)
            return new json_respuestas(false, "No se ha encontrado una operación con ese # de operación");

        var Pedido = new PedidosDTOModel();


        Pedido.datos = PedidoDatos;

        var resultGetDatosNumericosMontos = ObtenerNumeros(numero_operacion);
        Pedido.montos = resultGetDatosNumericosMontos != null ? resultGetDatosNumericosMontos : null;

        var resultGetDireccionEnvio = ObtenerDireccionEnvio(numero_operacion);
        Pedido.direccionEnvio = resultGetDireccionEnvio.result == true ? resultGetDireccionEnvio.response : null;

        var resultGetDireccionFacturacion = ObtenerDireccionFacturacion(numero_operacion);
        Pedido.direccionFacturacion = resultGetDireccionFacturacion.result == true ? resultGetDireccionFacturacion.response : null;


        Pedido.productos = ObtenerProductos(numero_operacion);


        return new json_respuestas(true, "Obtenido con éxito", false, Pedido);


    }

    /// <summary>
    /// 20210308 CM - Obtiene la información de un pedido por ID
    /// </summary>
    public static pedidos_datos ObtenerDatos(int idPedido)
    {


        try
        {

            using (var db = new tiendaEntities())
            {
                var PedidoDatos = db.pedidos_datos
                     .AsNoTracking()
                     .Where(s => s.id == idPedido).FirstOrDefault();


                return PedidoDatos;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }
    /// <summary>
    /// 20210404 CM - Obtiene la información de un pedido por su número de operación
    /// </summary>
    public static pedidos_datos ObtenerDatos(string numero_operacion)
    {


        try
        {

            using (var db = new tiendaEntities())
            {
                var PedidoDatos = db.pedidos_datos
                     .AsNoTracking()
                     .Where(s => s.numero_operacion == numero_operacion).FirstOrDefault();


                return PedidoDatos;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }    /// <summary>
         /// 20210810 CM - Obtiene la información de los pedidos por un periodo
         /// </summary>
    public static async Task<List<pedidos_datos>> ObtenerDatos(DateTime desde, DateTime hasta, bool OmitirCancelados, bool omitirCotizaciones)
    {
        try
        {
            using (var db = new tiendaEntities())
            {
                var PedidoDatos = await db.pedidos_datos
                     .AsNoTracking()
                     .Where(s => s.fecha_creacion >= desde && s.fecha_creacion <= hasta)
                     .ToListAsync();
                if (OmitirCancelados)
                {
                    PedidoDatos.RemoveAll(p => p.OperacionCancelada == true);
                }
                if (omitirCotizaciones)
                {
                    PedidoDatos.RemoveAll(p => p.numero_operacion.StartsWith("pc"));
                }
                return PedidoDatos;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    /// <summary>
    /// 20210308 CM - Obtiene el número de operación de un pedido por el id
    /// </summary>
    public static string ObtenerNumeroOperacion(int idPedido)
    {


        try
        {

            using (var db = new tiendaEntities())
            {
                var numero_operacion = db.pedidos_datos
                     .AsNoTracking()

                     .Where(s => s.id == idPedido)
                       .Select(u => u.numero_operacion)
                       .FirstOrDefault();

                return numero_operacion;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    /// <summary>
    /// 20210608 CM - Obtiene la moneda de una operación por su numero_operacion
    /// </summary>
    public static string ObtenerMonedaOperacion(string numero_operacion)
    {


        try
        {

            using (var db = new tiendaEntities())
            {
                var moneda = db.pedidos_datosNumericos
                     .AsNoTracking()

                     .Where(s => s.numero_operacion == numero_operacion)
                       .Select(u => u.monedaPedido)
                       .FirstOrDefault();

                return moneda;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }
    /// <summary>
    /// 20210404 CM - Obtiene la información de montos $$ de un pedido por su número de operación
    /// </summary>
    public static pedidos_datosNumericos ObtenerNumeros(string numero_operacion)
    {


        try
        {

            using (var db = new tiendaEntities())
            {
                var PedidoDatosNumericos = db.pedidos_datosNumericos
                     .AsNoTracking()
                     .Where(s => s.numero_operacion == numero_operacion).FirstOrDefault();


                return PedidoDatosNumericos;
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("obtenernumeros", ex);
            return null;
        }
    }
    /// <summary>
    /// 20210404 CM - Obtiene la información de montos $$ de un pedido por su número de operación
    /// </summary>
    public static decimal? ObtenerMontoTotalProductos(string numero_operacion)
    {


        try
        {

            using (var db = new tiendaEntities())
            {
                var MontoTotalProductos = db.pedidos_productos
                     .AsNoTracking()
                     .Where(n => n.numero_operacion == numero_operacion)
                     .Sum(p => p.precio_total);


                return MontoTotalProductos;
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Obtener monto total de productos", ex);
            return null;
        }
    }
    /// <summary>
    /// 20210308 CM - Obtiene los productos de un pedido por numero_operacion y su información
    /// </summary>
    public static List<PedidosProductosDatos> ObtenerProductosWithData(string numero_operacion)
    {
        try
        {
            using (var db = new tiendaEntities())
            {
                var PedidoProductos = db.pedidos_productos
                    .GroupJoin(
                        db.productos_Datos,
                        pro => pro.numero_parte,
                        dat => dat.numero_parte,
                        (pro, dat) => new { pro, dat })
                    .SelectMany(x => x.dat.DefaultIfEmpty(), (x, y) => new { pro = x.pro, dat = y })
                    .Where(x => x.pro.numero_operacion == numero_operacion)
                    .GroupBy(x => new { x.pro, x.dat })
                    .Select(g => new { g.Key.pro, g.Key.dat })
                    .AsNoTracking()
                    .ToList();

                /*  var PedidoProductos =   db.pedidos_productos
                    .Join(db.productos_Datos, // the source table of the inner join
                       producto => producto.numero_parte,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
                       data => data.numero_parte,   // Select the foreign key (the second part of the "on" clause)
                      (producto, data) => new {
                          numero_operacion = producto.numero_operacion,
                          id = producto.id,
                          numero_parte = producto.numero_parte,
                          cantidad = producto.cantidad,
                          precio_unitario = producto.precio_unitario,
                          precio_total = producto.precio_total,
                          descripcion = producto.descripcion,
                          data = data

                      })
               .Where(a => a.numero_operacion == numero_operacion).ToList();

                  */
                return PedidoProductos.Select(a => new PedidosProductosDatos() { productos = a.pro, datos = a.dat }).ToList();
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    /// <summary>
    /// 20210308 CM - Obtiene los productos de un pedido por ID
    /// </summary>
    public static List<pedidos_productos> ObtenerProductos(string numero_operacion)
    {


        try
        {

            using (var db = new tiendaEntities())
            {
                var PedidoProductos = db.pedidos_productos
                    .Where(s => s.numero_operacion == numero_operacion)
                    .AsNoTracking()
                    .ToList();


                return PedidoProductos;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    /// <summary>
    /// 20210308 CM - Obtiene la dirección de envío de un pedido
    /// </summary>
    public static json_respuestas ObtenerDireccionEnvio(string numero_operacion)
    {


        try
        {

            using (var db = new tiendaEntities())
            {
                var PedidoDireccionEnvio = db.pedidos_direccionEnvio
                      .AsNoTracking()
                      .Where(s => s.numero_operacion == numero_operacion).FirstOrDefault();


                if (PedidoDireccionEnvio != null)
                    return new json_respuestas(true, "Dirección obtenida con éxito)", false, PedidoDireccionEnvio);
                else return new json_respuestas(true, "No se encontró una dirección de envío establecida", false, null);
            }
        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Hubo un error al obtener la dirección de envío", true, null);

        }
    }
    /// <summary>
    /// 20210325 CM - Obtiene la dirección de facturación de un pedido
    /// </summary>
    public static json_respuestas ObtenerDireccionFacturacion(string numero_operacion)
    {


        try
        {

            using (var db = new tiendaEntities())
            {
                var PedidoDireccionFacturacion = db.pedidos_direccionFacturacion
                      .AsNoTracking()
                      .Where(s => s.numero_operacion == numero_operacion).FirstOrDefault();


                if (PedidoDireccionFacturacion != null)
                    return new json_respuestas(true, "Dirección de facturación obtenida con éxito)", false, PedidoDireccionFacturacion);
                else return new json_respuestas(true, "No se encontró una dirección de facturación establecida", false, null);
            }
        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Hubo un error al obtener la dirección de facturación", true, null);

        }
    }
    /// <summary>
    /// 20221212 - Obtiene la descripción del Régimen Fiscal con base en el código
    /// </summary>
    public static string obtenerDescripcionRegimenFiscal(string codigo)
    {
        switch (codigo)
        {
            case "601":
                return codigo + " - General de Ley Personas Morales";
            case "603":
                return codigo + " - Personas Morales con Fines no Lucrativos";
            case "605":
                return codigo + " - Sueldos y Salarios e Ingresos Asimilados a Salarios";
            case "606":
                return codigo + " - Arrendamiento";
            case "607":
                return codigo + " - Régimen de Enajenación o Adquisición de Bienes";
            case "608":
                return codigo + " - Demás ingresos";
            case "610":
                return codigo + " - Residentes en el Extranjero sin Establecimiento Permanente en México";
            case "611":
                return codigo + " - Ingresos por Dividendos (socios y accionistas)";
            case "612":
                return codigo + " - Personas Físicas con Actividades Empresariales y Profesionales";
            case "614":
                return codigo + " - Ingresos por intereses";
            case "615":
                return codigo + " - Régimen de los ingresos por obtención de premios";
            case "616":
                return codigo + " - Sin obligaciones fiscales";
            case "620":
                return codigo + " - Sociedades Cooperativas de Producción que optan por diferir sus ingresos";
            case "621":
                return codigo + " - Incorporación Fiscal";
            case "622":
                return codigo + " - Actividades Agrícolas, Ganaderas, Silvícolas y Pesqueras";
            case "623":
                return codigo + " - Opcional para Grupos de Sociedades";
            case "624":
                return codigo + " - Coordinados";
            case "625":
                return codigo + " - Régimen de las Actividades Empresariales con ingresos a través de Plataformas Tecnológicas";
            case "626":
                return codigo + " - Régimen Simplificado de Confianza";
            default:
                return null;
        }
    }
    /// <summary>
    /// 20210308 CM - Guarda/actualiza una dirección de envío, si no exíste: inserta
    /// </summary>
    public static json_respuestas GuardarDireccionEnvio(string numero_operacion, pedidos_direccionEnvio direccion)
    {


        try
        {

            using (var db = new tiendaEntities())
            {
                var PedidoDireccionEnvio = db.pedidos_direccionEnvio
                    .Where(s => s.numero_operacion == numero_operacion).FirstOrDefault();


                // Si no existe
                if (PedidoDireccionEnvio == null)
                {
                    db.pedidos_direccionEnvio.Add(direccion);
                } // si existe, actualizamos
                else
                {

                    PedidoDireccionEnvio.idDireccionEnvio = direccion.idDireccionEnvio;
                    PedidoDireccionEnvio.calle = direccion.calle;
                    PedidoDireccionEnvio.numero = direccion.numero;
                    PedidoDireccionEnvio.numero_interior = direccion.numero_interior;
                    PedidoDireccionEnvio.colonia = direccion.colonia;
                    PedidoDireccionEnvio.delegacion_municipio = direccion.delegacion_municipio;
                    PedidoDireccionEnvio.ciudad = direccion.ciudad;
                    PedidoDireccionEnvio.estado = direccion.estado;
                    PedidoDireccionEnvio.codigo_postal = direccion.codigo_postal;
                    PedidoDireccionEnvio.pais = direccion.pais;
                    PedidoDireccionEnvio.referencias = direccion.referencias;

                }
                db.SaveChanges();

            }
            return new json_respuestas(true, "Dirección de envío guardada con éxito", false);
        }
        catch (Exception ex)
        {

            return new json_respuestas(false, "Ocurrio un error al guardar la dirección", true);
        }
    }


    /// <summary>
    /// 20210325 CM - Guarda/actualiza una dirección de facturación, si no exíste: inserta
    /// </summary>
    public static json_respuestas GuardarDireccionFacturacion(string numero_operacion, pedidos_direccionFacturacion direccion)
    {


        try
        {

            using (var db = new tiendaEntities())
            {
                var PedidoDireccionFacturacion = db.pedidos_direccionFacturacion
                    .Where(s => s.numero_operacion == numero_operacion).FirstOrDefault();


                // Si no existe
                if (PedidoDireccionFacturacion == null)
                {
                    db.pedidos_direccionFacturacion.Add(direccion);
                } // si existe, actualizamos
                else
                {

                    PedidoDireccionFacturacion.idDireccionFacturacion = direccion.idDireccionFacturacion;
                    PedidoDireccionFacturacion.calle = direccion.calle;
                    PedidoDireccionFacturacion.numero = direccion.numero;
                    PedidoDireccionFacturacion.colonia = direccion.colonia;
                    PedidoDireccionFacturacion.delegacion_municipio = direccion.delegacion_municipio;
                    PedidoDireccionFacturacion.ciudad = direccion.ciudad;
                    PedidoDireccionFacturacion.estado = direccion.estado;
                    PedidoDireccionFacturacion.codigo_postal = direccion.codigo_postal;
                    PedidoDireccionFacturacion.pais = direccion.pais;
                    PedidoDireccionFacturacion.razon_social = direccion.razon_social;
                    PedidoDireccionFacturacion.rfc = direccion.rfc;
                    PedidoDireccionFacturacion.RegimenFiscal = direccion.RegimenFiscal;
                }
                db.SaveChanges();

            }
            return new json_respuestas(true, "Dirección de facturación guardada con éxito", false);
        }
        catch (Exception ex)
        {

            return new json_respuestas(false, "Ocurrio un error al guardar la dirección", true);
        }
    }
    /// <summary>
    /// 20210308 CM - Guarda los datos de contacto de un pedido
    /// </summary>
    public static json_respuestas GuardarContacto(int idPedido, contacto contacto)
    {


        try
        {

            using (var db = new tiendaEntities())
            {
                var PedidoDatos = db.pedidos_datos
                    .Where(s => s.id == idPedido).FirstOrDefault();


                PedidoDatos.cliente_nombre = contacto.nombre;
                PedidoDatos.cliente_apellido_paterno = contacto.apellido_paterno;
                PedidoDatos.cliente_apellido_materno = contacto.apellido_materno;
                PedidoDatos.email = contacto.email;
                PedidoDatos.telefono = contacto.telefono;
                PedidoDatos.celular = contacto.celular;

                db.SaveChanges();


            }
            return new json_respuestas(true, "Datos de contacto guardado con éxito", false);
        }
        catch (Exception ex)
        {

            return new json_respuestas(false, "Ocurrio un error al guardar", true);
        }
    }
    /// <summary>
    /// 20210326 CM - Actualiza el campo nota de envío en la tabla pedidos_datosNumericos
    /// </summary>
    public static json_respuestas ActualizarEnvioNota(string numero_operacion, string EnvioNota)
    {
        try
        {

            using (var db = new tiendaEntities())
            {
                var PedidoDatos = db.pedidos_datosNumericos
                    .Where(s => s.numero_operacion == numero_operacion).FirstOrDefault();

                PedidoDatos.EnvioNota = EnvioNota;
                db.SaveChanges();

            }
            return new json_respuestas(true, "Dato actualizado con éxito.", false);
        }
        catch (Exception ex)
        {

            return new json_respuestas(false, "Error al actualizar", true);
        }
    }
    /// <summary>
    /// 20210326 CM - Actualiza el campo "factura" si requiere o no.
    /// </summary>
    public static json_respuestas ActualizarFacturacion(string numero_operacion, bool factura)
    {
        try
        {

            using (var db = new tiendaEntities())
            {
                var PedidoDatos = db.pedidos_datos
                    .Where(s => s.numero_operacion == numero_operacion).FirstOrDefault();

                PedidoDatos.factura = factura;
                db.SaveChanges();

            }
            return new json_respuestas(true, "Requerimiento de factura actualizado con éxito.", false);
        }
        catch (Exception ex)
        {

            return new json_respuestas(false, "Error al actualizar requerimiento de factura", true);
        }
    }
    /// <summary>
    /// 20210326 CM - Actualiza el campo "factura" si requiere o no.
    /// </summary>
    public static async Task<json_respuestas> ActualizarAsesorAsigando(string numero_operacion, int idUsuarioSeguimiento)
    {
        try
        {

            using (var db = new tiendaEntities())
            {
                var PedidoDatos = await db.pedidos_datos
                    .Where(s => s.numero_operacion == numero_operacion).FirstAsync();

                PedidoDatos.idUsuarioSeguimiento = idUsuarioSeguimiento;
                await db.SaveChangesAsync();

            }
            return new json_respuestas(true, "Asignación de asesor realizado con éxito.", false);
        }
        catch (Exception ex)
        {

            return new json_respuestas(false, "Error al asignar asesor, contacta a desarrollo, ex:" + ex.Message, true);
        }
    }


    public static json_respuestas EliminarDireccionDeFacturacion(string numero_operacion)
    {
        try
        {

            using (var db = new tiendaEntities())
            {
                var direccion = db.pedidos_direccionFacturacion
                    .Where(s => s.numero_operacion == numero_operacion).FirstOrDefault();

                db.pedidos_direccionFacturacion.Remove(direccion);
                db.SaveChanges();

            }
            return new json_respuestas(true, "Dirección de facturación eliminada con éxito.", false);
        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Error al eliminar dirección de facturación de el pedido.", true);
        }

    }

    public static json_respuestas EliminarDireccionDeEnvio(string numero_operacion)
    {
        try
        {

            using (var db = new tiendaEntities())
            {
                var direccion = db.pedidos_direccionEnvio
                    .Where(s => s.numero_operacion == numero_operacion).FirstOrDefault();

                db.pedidos_direccionEnvio.Remove(direccion);
                db.SaveChanges();

            }
            return new json_respuestas(true, "Dirección de facturación eliminada con éxito.", false);
        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Error al eliminar dirección de facturación de el pedido.", true);
        }

    }


    public static json_respuestas ObtenerMetodoDeEnvío(string numero_operacion)
    {
        try
        {

            using (var db = new tiendaEntities())
            {
                var MetodoDeEnvio = db.pedidos_datosNumericos
                      .AsNoTracking()
                      .Where(s => s.numero_operacion == numero_operacion)
                      .Select(s => s.metodoEnvio)
                      .FirstOrDefault();


                if (MetodoDeEnvio != null)
                {
                    if (MetodoDeEnvio == "Ninguno") return new json_respuestas(false, "El método de envío no se ha podido establecer o ocurrió un error.", false, MetodoDeEnvio);
                    else
                        return new json_respuestas(true, "Método de envío obtenido correctamente.", false, MetodoDeEnvio);
                }

                else return new json_respuestas(false, "El método de envío no es válido, no se ha establecido o ocurrió un error.", false, null);
            }
        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Hubo un error al obtener el método de envío", true, null);

        }
    }

    /// <summary>
    /// 20210403 CM - Obtiene  y procesa los registros en las tablas de PayPal,  Santander y transferencias
    /// </summary>
    public static async Task<json_respuestas> ObtenerPagoPedido(string numero_operacion)
    {
        try
        {
            var historialPagosPayPal = PayPalTienda.obtenerPago(numero_operacion);

            if (historialPagosPayPal != null && historialPagosPayPal.estado == "COMPLETED")
            {
                dynamic Pago = new ExpandoObject();
                Pago.tipo = "PayPal";
                Pago.pago = historialPagosPayPal;
                return new json_respuestas(true, $"Pagado con PayPal. <br/> Estado: <b>COMPLETADO</b>.", false, Pago);
            }

            List<pedidos_pagos_respuesta_santander> historialPagosSantander = SantanderResponse.ObtenerTodos(numero_operacion);
            historialPagosSantander = historialPagosSantander.Where(p => p.estatus == "approved").ToList();

            if (historialPagosSantander.Count >= 1)
            {
                dynamic Pago = new ExpandoObject();
                Pago.tipo = "Santander";
                Pago.pago = historialPagosSantander;
                return new json_respuestas(true, $"Pagado con Santander.<br/> Estado: <b>APROVADO</b>", false, Pago);
            }
            var ReferenciaTransferencia = await ObtenerReferenciaTransferencia(numero_operacion);

            if (ReferenciaTransferencia.result == true)
            {
                pedidos_pagos_transferencia referencia = ReferenciaTransferencia.response;
                dynamic Pago = new ExpandoObject();
                Pago.tipo = "Transferencia";
                Pago.pago = referencia;
                string textEstado = (bool)referencia.confirmacionAsesor ? "CONFIRMADO" : "SIN CONFIRMAR";

                return new json_respuestas(true, $"Pagado por transfencia.<br/> Estado: <b>" + textEstado + "</b>", false, Pago);
            }

            // Si llega a este punto no se ha encontrado un pago 

            return new json_respuestas(false, $"Pago <b>no encontrado</b>", false, null);
        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Hubo un error al obtener la comprobación de pago.", true, null);

        }
    }
    public static async Task<json_respuestas> obtenerPagoPedidoPagado(string numero_operacion)
    {
        try
        {
            var historialPagoPayPal = PayPalTienda.obtenerPago(numero_operacion);
            if (historialPagoPayPal != null && historialPagoPayPal.estado == "COMPLETED")
            {
                dynamic pago = new ExpandoObject();
                pago.tipo = "PayPal";
                pago.pago = historialPagoPayPal;
                return new json_respuestas(true, historialPagoPayPal.monto);
            }

            List<pedidos_pagos_respuesta_santander> historialPagoSantander = SantanderResponse.ObtenerTodos(numero_operacion);
            historialPagoSantander = historialPagoSantander.Where(p => p.estatus == "approved").ToList();
            if (historialPagoSantander.Count >= 1)
            {
                string monto = ObtenerNumeros(numero_operacion).total.ToString();
                dynamic pago = new ExpandoObject();
                pago.tipo = "Santander";
                pago.pago = historialPagoSantander;
                return new json_respuestas(true, monto);
            }

            var referenciaTransferencia = await ObtenerReferenciaTransferencia(numero_operacion);
            if (referenciaTransferencia.result)
            {
                string monto = ObtenerNumeros(numero_operacion).total.ToString();
                pedidos_pagos_transferencia referencia = referenciaTransferencia.response;
                dynamic pago = new ExpandoObject();
                pago.tipo = "Transferencia";
                pago.pago = referencia;
                return new json_respuestas(true, monto);
            }

            return new json_respuestas(true, "0");
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Error al obtener el monto de pedido pagado", ex);
            return new json_respuestas(false, "Error");
        }
    }
    /// <summary>
    /// 20210408 CM - Actualiza el campo "factura" si requiere o no.
    /// </summary>
    public static json_respuestas CancelarPedido(string numero_operacion, string MotivoCancelacion)
    {
        try
        {
            if (MotivoCancelacion.Length > 300) MotivoCancelacion = MotivoCancelacion.Substring(0, 300);
            using (var db = new tiendaEntities())
            {
                var PedidoDatos = db.pedidos_datos
                    .Where(s => s.numero_operacion == numero_operacion).FirstOrDefault();

                PedidoDatos.OperacionCancelada = true;
                PedidoDatos.motivoCancelacion = MotivoCancelacion;
                db.SaveChanges();

            }
            return new json_respuestas(true, "Solicitud de cancelación enviada con éxito.", false);
        }
        catch (Exception ex)
        {

            return new json_respuestas(false, "Error al cancelar pedido", true);
        }
    }


    /// <summary>
    /// 20211129 CM - Reactiva el pedido y borra el dato "motivo de cancelación"
    /// </summary>
    public static json_respuestas ReactivarPedido(string numero_operacion)
    {
        try
        {
            using (var db = new tiendaEntities())
            {
                var PedidoDatos = db.pedidos_datos
                    .Where(s => s.numero_operacion == numero_operacion).FirstOrDefault();

                PedidoDatos.OperacionCancelada = false;
                PedidoDatos.motivoCancelacion = null;
                db.SaveChanges();

            }
            return new json_respuestas(true, "Pedido reactivado con éxito.", false);
        }
        catch (Exception ex)
        {

            return new json_respuestas(false, "Error al reactivar pedido", true);
        }
    }


    public static async Task<json_respuestas> GenerarReferenciaTransferencia(pedidos_pagos_transferencia pago)
    {
        try
        {


            using (var db = new tiendaEntities())
            {
                var ReferenciaTransferencia = await db.pedidos_pagos_transferencia
                    .AsNoTracking()
                    .CountAsync(s => s.numero_operacion == pago.numero_operacion);


                if (ReferenciaTransferencia >= 1)
                {
                    return new json_respuestas(false, "Ya se encuentra una referencia generada.", false, null);

                }


                db.pedidos_pagos_transferencia.Add(pago);
                await db.SaveChangesAsync();
            }
            return new json_respuestas(true, "Registro generado con éxito, en unos momentos un asesor confirmara tu transferencia.", false, pago);
        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Error interno, contacta a un asesor.", true, ex);
        }
    }
    public static async Task<json_respuestas> ObtenerReferenciaTransferencia(string numero_operacion)
    {
        try
        {
            using (var db = new tiendaEntities())
            {
                var ReferenciaTransferencia = db.pedidos_pagos_transferencia
                      .AsNoTracking()
                       .Where(s => s.numero_operacion == numero_operacion).FirstOrDefault();

                if (ReferenciaTransferencia == null)
                {
                    return new json_respuestas(false, "No se ha encontrada una referencia para este número de operación", false, null);
                }

                return new json_respuestas(true, "Referencia de transferencia obtenida con éxito", false, ReferenciaTransferencia);
            }

        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Error al obtener refrencia de transfrencia.", true, ex);
        }
    }
    public static async Task<json_respuestas> ActualizarReferenciaTransferencia(pedidos_pagos_transferencia referencia)
    {
        try
        {
            using (var db = new tiendaEntities())
            {
                var ReferenciaTransferencia = db.pedidos_pagos_transferencia

                       .Where(s => s.numero_operacion == referencia.numero_operacion).FirstOrDefault();

                if (ReferenciaTransferencia != null)
                {
                    ReferenciaTransferencia.fecha_confirmacion = referencia.fecha_confirmacion;
                    ReferenciaTransferencia.confirmacionAsesor = referencia.confirmacionAsesor;
                    ReferenciaTransferencia.referencia = referencia.referencia;

                    await db.SaveChangesAsync();
                    return new json_respuestas(true, "Referencia de transferencia actualizada con éxito", false, ReferenciaTransferencia);
                }
                else
                {
                    return new json_respuestas(true, "No se encontró una referencia con el número de operación", false, ReferenciaTransferencia);
                }

            }

        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Error al obtener refrencia de transferencia.", true, ex);
        }
    }

}
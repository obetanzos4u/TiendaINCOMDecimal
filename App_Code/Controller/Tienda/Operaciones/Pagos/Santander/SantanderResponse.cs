using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de SantanderResponse
/// </summary>
public class SantanderResponse
{
    public SantanderResponse()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    static public pedidos_pagos_respuesta_santander obtener(string numero_operacion)
    {
        using (var db = new tiendaEntities())
        {
            var response = db.pedidos_pagos_respuesta_santander
                .Where(o => o.numero_operacion == numero_operacion)
                .AsNoTracking()
                .OrderByDescending(o => o.fecha_actualización)
                .FirstOrDefault();

            return response;
        }
    }
    static public List<pedidos_pagos_respuesta_santander> ObtenerTodos(string numero_operacion)
    {
        using (var db = new tiendaEntities())
        {
            var response = db.pedidos_pagos_respuesta_santander
                .Where(o => o.numero_operacion == numero_operacion)
                .AsNoTracking()
                .OrderByDescending(o => o.fecha_actualización)
                .ToList();

            return response;
        }
    }
    //static public List<pedidos_pagos_respuesta_santander> obtenerTodosConMonto(string numero_operacion)
    //{
    //    using (var db = new tiendaEntities())
    //    {
    //        var santanderConMonto = db.pedidos_pagos_respuesta_santander
    //            .Join(db.pedidos_pagos_liga_santander,
    //                pedidos_pagos_respuesta_santander => pedidos_pagos_respuesta_santander.numero_operacion,
    //                pedidos_pagos_liga_santander => pedidos_pagos_liga_santander.numero_operacion,
    //                (pedidos_pagos_respuesta_santander, pedidos_pagos_liga_santander) => new santanderConMonto
    //                {
    //                    numero_operacion = 
    //                }
    //            )
    //        //var response = db.pedidos_pagos_respuesta_santander
    //        //    .GroupJoin(db.pedidos_datosNumericos, pprs => pprs.numero_operacion, pdn => pdn.numero_operacion, (pprs, pdn) => new { pprs, pdn })
    //        //    .GroupBy(x => new { x.pprs, x.pdn })
    //        //    .Select(p => new { p.Key.pprs, p.Key.pdn })
    //        //    .Where(p => p.pprs.numero_operacion == numero_operacion)
    //        //    .AsNoTracking()
    //        //    .ToList();
    //    }
    //}
    public static void enviarEmail(string numero_operacion, string status, string monto)
    {

        DataTable dt_PedidoProductos = pedidosProductos.obtenerProductos(numero_operacion);
        string productosEmailHTML = string.Empty;
        foreach (DataRow p in dt_PedidoProductos.Rows)
        {
            productosEmailHTML += "<strong>" + p["numero_parte"].ToString() + "</strong> - " + p["descripcion"].ToString() + "<br><br>";
        }

        pedidos_datos pedidoDatos = null;
        pedidos_datosNumericos pedidoDatosNumericos = null;
        pedidos_direccionEnvio direccionEnvio = null;
        using (var db = new tiendaEntities())
        {
            pedidoDatos = db.pedidos_datos
                .Where(p => p.numero_operacion == numero_operacion)
                .AsNoTracking()
                .FirstOrDefault();

            pedidoDatosNumericos = db.pedidos_datosNumericos
             .Where(p => p.numero_operacion == numero_operacion)
             .AsNoTracking()
             .FirstOrDefault();

            direccionEnvio = db.pedidos_direccionEnvio
                .Where(p => p.numero_operacion == numero_operacion)
                .AsNoTracking()
                .FirstOrDefault();
        }

        string id_operacion_encritado = seguridad.Encriptar(pedidoDatos.id.ToString());

        string url_pedido =
            HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +

            "/usuario/cliente/mi-cuenta/pedidos/resumen/" + id_operacion_encritado;

        //Inicio email para el cliente

        string plantillaCliente = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "/email_templates/operaciones/pedidos/pago_santander/santander_cliente_pago_realizado.html");


        Dictionary<string, string> datosDiccRemplazoCliente = new Dictionary<string, string>
        {
            { "{fecha}", utilidad_fechas.DDMMAAAA() },
            { "{numero_operacion}", numero_operacion },
            { "{cliente_nombre}", pedidoDatos.cliente_nombre + " " + pedidoDatos.cliente_apellido_paterno },
            { "{totalPagado}", decimal.Parse(monto).ToString("C2", new CultureInfo("es-MX", true).NumberFormat) + " " },
            { "{estatus}", status },
            { "{url_operacion}", url_pedido },
            { "{productos}", productosEmailHTML },
            { "{direccion_envio}", direccionEnvio.calle + " " + direccionEnvio.numero + ", " + direccionEnvio.colonia + ", " + direccionEnvio.delegacion_municipio + ", " + direccionEnvio.estado + ", " + direccionEnvio.codigo_postal + ", " + direccionEnvio.pais }
        };

        plantillaCliente = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, plantillaCliente);

        string mensaje = string.Empty;
        using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~") + plantillaCliente))
        {
            mensaje = reader.ReadToEnd();
        }

        if (datosDiccRemplazoCliente.Count >= 1)
        {
            foreach (var item in datosDiccRemplazoCliente)
            {
                mensaje = mensaje.Replace(item.Key, item.Value);
            }
        }

        emailTienda emailCliente = new emailTienda("Pago realizado vía Santander con 3D Secure para el pedido: " + pedidoDatos.numero_operacion,
            pedidoDatos.email + ", jaraujo@incom.mx, ralbert@incom.mx, fgarcia@incom.mx", mensaje, "retail@incom.mx");
        emailCliente.general();

        //FIN email para el cliente
    }
}
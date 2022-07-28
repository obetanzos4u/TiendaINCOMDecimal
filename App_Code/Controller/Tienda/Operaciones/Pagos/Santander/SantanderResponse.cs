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

        using(var db = new tiendaEntities())
        {
            var response = db.pedidos_pagos_respuesta_santander
                .Where(o => o.numero_operacion == numero_operacion)
                .AsNoTracking()
                .OrderByDescending(o => o.fecha_actualización)
                .FirstOrDefault();

            return response;
        }
    }
    static public  List<pedidos_pagos_respuesta_santander> ObtenerTodos(string numero_operacion)
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
    public static void enviarEmail(string numero_operacion,string status, string monto)
    {

        DataTable dt_PedidoProductos = pedidosProductos.obtenerProductos(numero_operacion);
        string productosEmailHTML = string.Empty;
        foreach (DataRow p in dt_PedidoProductos.Rows)
        {
            productosEmailHTML += "<strong>" + p["numero_parte"].ToString() + "</strong> - " + p["descripcion"].ToString() + "<br><br>";
        }

     



        pedidos_datos pedidoDatos = null;
        pedidos_datosNumericos pedidoDatosNumericos = null;
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
        }

        string id_operacion_encritado = seguridad.Encriptar(pedidoDatos.id.ToString());

        string url_pedido = 
            HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) +

            "/usuario/cliente/mi-cuenta/pedidos/resumen/" + id_operacion_encritado;

        //Inicio email para el cliente

        string plantillaCliente = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "/email_templates/operaciones/pedidos/pago_santander/santander_cliente_pago_realizado.html"); 
 

        Dictionary<string, string> datosDiccRemplazoCliente = new Dictionary<string, string>();

        datosDiccRemplazoCliente.Add("{urlDominio}", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority));

        datosDiccRemplazoCliente.Add("{nombre_operacion}", pedidoDatos.nombre_pedido);
        datosDiccRemplazoCliente.Add("{numero_operacion}", numero_operacion);
        datosDiccRemplazoCliente.Add("{cliente_nombre}", pedidoDatos.cliente_nombre + " " + pedidoDatos.cliente_apellido_paterno);

        datosDiccRemplazoCliente.Add("{usuario_email}", pedidoDatos.usuario_cliente);

        datosDiccRemplazoCliente.Add("{totalPagado}", decimal.Parse(monto).ToString("C2", new CultureInfo("es-MX", true).NumberFormat) + " ");
        datosDiccRemplazoCliente.Add("{totalOperacion}", pedidoDatosNumericos.total.ToString("C2", new CultureInfo("es-MX", true).NumberFormat) + " ");
        datosDiccRemplazoCliente.Add("{estatus}", status);
        datosDiccRemplazoCliente.Add("{url_operacion}", url_pedido);

        datosDiccRemplazoCliente.Add("{productos}", productosEmailHTML);


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

        emailTienda emailCliente = new emailTienda("Pago realizado 3DS para el pedido: " + pedidoDatos.nombre_pedido,
            pedidoDatos.email + ", cmiranda@it4u.com.mx, jhernandez@incom.mx, pjuarez@incom.mx, ralbert@incom.mx, fgarcia@incom.mx", mensaje, "retail@incom.mx");
        emailCliente.general();

        //FIN email para el cliente
    }
}
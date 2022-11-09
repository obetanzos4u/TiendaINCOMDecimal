using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
public class direcciones_facturacion_EF
{


    /// <summary>
    /// 20210325 CM- Obtiene las direcciones de facturación previamente guardadas de un cliente por su id de usuario FK
    /// </summary>
    public static List<direcciones_facturacion> ObtenerTodas(int id_cliente)
    {

 
        try
        {

            using (var db = new tiendaEntities())
            {

                 
                var direcciones = db.direcciones_facturacion
                    .Where(s => s.id_cliente == id_cliente)
                  
                     .AsNoTracking()
                    .ToList();
               
                return direcciones;
            }
        }
        catch (Exception ex)
        {

            throw new Exception(ex.ToString() + " Error al obtener la direcciones de facturación, ID cliente: "+id_cliente, ex);
            
        }
    }

    /// <summary>
    /// 20210325 CM- Obtiene una dirección de facturación por su ID
    /// </summary>
    public static direcciones_facturacion Obtener(int id_direccion)
    {


        try
        {

            using (var db = new tiendaEntities())
            {
                var direcciones = db.direcciones_facturacion
                    .Where(s => s.id == id_direccion).FirstOrDefault();


                return direcciones;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    /// <summary>
    /// 20210325 CM - Elimina una dirección de facturación
    /// </summary>
    public static json_respuestas eliminar(int Id_Direccion)
    {
        try
        {

            using (var context = new tiendaEntities())
            {

                var direccion = context.direcciones_facturacion
                    .Where(s => s.id == Id_Direccion)
                    .SingleOrDefault();

                context.direcciones_facturacion.Remove(direccion);

                context.SaveChanges();
            }

            return new json_respuestas(true, "Dirección de facturación eliminada.", false);
        }
        catch (Exception ex)
        {

            return new json_respuestas(false, "Error al eliminar", true);
        }
    }

    /// <summary>
    /// 20210308 CM - Guarda/actualiza una dirección de facturación, si no exíste: inserta
    /// </summary>
    public static json_respuestas GuardarDireccion(direcciones_facturacion direccion)
    {
        try
        {
            using (var db = new tiendaEntities())
            {
                var  DireccionFacturacion= db.direcciones_facturacion
                    .Where(s => s.id == direccion.id).FirstOrDefault();


                // Si no existe
                if (DireccionFacturacion == null)
                {
                    db.direcciones_facturacion.Add(direccion);
                } // si existe, actualizamos
                else
                {
                    DireccionFacturacion.calle = direccion.calle;
                    DireccionFacturacion.numero = direccion.numero;
                    DireccionFacturacion.colonia = direccion.colonia;
                    DireccionFacturacion.delegacion_municipio = direccion.delegacion_municipio;
                    DireccionFacturacion.ciudad = direccion.ciudad;
                    DireccionFacturacion.codigo_postal = direccion.codigo_postal;
                    DireccionFacturacion.pais = direccion.pais;
                    DireccionFacturacion.razon_social = direccion.razon_social;
                    DireccionFacturacion.rfc = direccion.rfc;
                    DireccionFacturacion.regimen_fiscal = direccion.regimen_fiscal;
                }
                db.SaveChanges();
            }
            return new json_respuestas(true, "Dirección de facturación guardada con éxito", false);
        }
        catch (Exception ex)
        {

            return new json_respuestas(false, "Ocurrio un error al guardar la dirección de facturación", true);
        }
    }
}
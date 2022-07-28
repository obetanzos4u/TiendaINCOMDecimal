using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
public class direcciones_envio_EF
{

    /// <summary>
    /// Remplaza  la clase direccionesEnvio.cs por esta, usando EF.
    /// </summary>


    public direcciones_envio_EF()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    /// <summary>
    /// 20200730 CM- Obtiene las direcciones de envío previamente guardadas de un cliente por su id de usuario FK
    /// </summary>
    public static List<Modeldirecciones_envio> ObtenerTodas(int id_cliente)
    {


        try
        {

            using (var db = new tiendaEntities())
            {


                var direccionesEnvio = db.direcciones_envio
                      .AsNoTracking()
                    .Where(s => s.id_cliente == id_cliente)
                    
                   
                    .ToList();
               

                var mapper = new AutoMapper.Mapper(Mapeos.getDireccionesEnvio);
                List<Modeldirecciones_envio> direcciones = mapper.Map<List<Modeldirecciones_envio>>(direccionesEnvio);

                return direcciones;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error al obtener la direcciones de envío, ID: " + id_cliente, ex);

        }
    }
    /// <summary>
    /// 20200730 CM- Obtiene las direcciones de envío previamente guardadas de un cliente por su id de usuario FK
    /// </summary>
    public static direcciones_envio ObtenerPredeterminada(int id_cliente)
    {


        try
        {

            using (var db = new tiendaEntities())
            {


                var direccionesEnvio = db.direcciones_envio
                    .Where(s => s.id_cliente == id_cliente && s.direccion_predeterminada == true)
                    .AsNoTracking()

                    .FirstOrDefault();

                return direccionesEnvio;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error al obtener la direcciones de envío, ID: " + id_cliente, ex);

        }
    }
    /// <summary>
    /// 20200730 CM- Obtiene una dirección de envío por su ID
    /// </summary>
    public static direcciones_envio Obtener(int id_direccion)
    {


        try
        {

            using (var db = new tiendaEntities())
            {
                var direccionesEnvio = db.direcciones_envio
                    .Where(s => s.id == id_direccion).FirstOrDefault();


                return direccionesEnvio;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    /// <summary>
    /// 20200730 CM - Elimina una dirección de envío
    /// </summary>
    public static json_respuestas eliminar(int Id_Direccion)
    {
        try
        {

            using (var context = new tiendaEntities())
            {

                var direccion = context.direcciones_envio
                    .Where(s => s.id == Id_Direccion)
                    .SingleOrDefault();

                context.direcciones_envio.Remove(direccion);

                context.SaveChanges();
            }

            return new json_respuestas(true, "Dirección de envío eliminada.", false);
        }
        catch (Exception ex)
        {

            return new json_respuestas(false, "Error al eliminar", true);
        }
    }

    /// <summary>
    /// 20210308 CM - Guarda/actualiza una dirección de envío, si no exíste: inserta
    /// </summary>
    public static json_respuestas GuardarDireccionEnvio(direcciones_envio direccion)
    {


        try
        {

            using (var db = new tiendaEntities())
            {
                var DireccionEnvio = db.direcciones_envio
                    .Where(s => s.id == direccion.id).FirstOrDefault();


                // Si no existe
                if (DireccionEnvio == null)
                {
                    db.direcciones_envio.Add(direccion);
                } // si existe, actualizamos
                else
                {
                    direccion.calle = direccion.calle;
                    direccion.numero = direccion.numero;
                    direccion.numero_interior = direccion.numero_interior;
                    direccion.colonia = direccion.colonia;
                    direccion.delegacion_municipio = direccion.delegacion_municipio;
                    direccion.ciudad = direccion.ciudad;
                    direccion.codigo_postal = direccion.codigo_postal;
                    direccion.pais = direccion.pais;
                    direccion.referencias = direccion.referencias;
                  
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
    /// 20210407 CM - Si solo tiene una dirección de envio creada, establece esa como la predeterminada para envíos
    /// </summary>
    public static void EstablecerPredeterminada(int id_cliente)
    {
        try
        {
            using (var db = new tiendaEntities())
            {
                var direccionesEnvio = db.direcciones_envio

                    .Where(s => s.id_cliente == id_cliente)
                    .ToList();

                if(direccionesEnvio.Count() == 1)
                {

                    direccionesEnvio[0].direccion_predeterminada = true;
                    db.SaveChanges();
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
}
        
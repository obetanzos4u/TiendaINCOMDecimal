using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ContactosEF
/// </summary>
public class ContactosEF
{
    public ContactosEF()
    {

    }

    static public List<contacto> ObtenerContactos(int id_cliente)
    {
        try
        {

            using (var db = new tiendaEntities())
            {
                var Contactos = db.contactos
                     .AsNoTracking()
                     .Where(s => s.id_cliente == id_cliente).ToList();


                return Contactos;
            }
        }
        catch (Exception ex)
        {

            return null;

        }
    }
    static public contacto Obtener(int IdContacto)
    {
        try
        {

            using (var db = new tiendaEntities())
            {
                var Contactos = db.contactos
                     .AsNoTracking()
                     .Where(s => s.id == IdContacto).FirstOrDefault();


                return Contactos;
            }
        }
        catch (Exception ex)
        {

            return null;

        }
    }
    /// <summary>
    /// 20200730 CM - Elimina un contacto
    /// </summary>
    public static json_respuestas eliminar(int idContacto)
    {
        try
        {

            using (var context = new tiendaEntities())
            {

                var direccion = context.contactos
                    
                    .Where(s => s.id == idContacto)
                    
                    .FirstOrDefault();

                context.contactos.Remove(direccion);
                context.SaveChanges();
            }

            return new json_respuestas(true, "Contacto eliminado", false);
        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Error al eliminar ", false);
        }
    }

    /// <summary>
    /// 20210308 CM - Guarda/actualiza un contacto, por id, nombre y apellido paterno, si no exíste: inserta
    /// </summary>
    public static json_respuestas Guardar(contacto contacto)
    {


        try
        {

            using (var db = new tiendaEntities())
            {
                var Contacto = db.contactos
                    .Where(s => s.id == contacto.id) 
                    .FirstOrDefault();


                // Si no existe
                if (Contacto == null)
                {
                    db.contactos.Add(contacto);
                } // si existe, actualizamos
                else
                {
                    Contacto.nombre = contacto.nombre;
                    Contacto.apellido_paterno = contacto.apellido_paterno;
                    Contacto.apellido_materno = contacto.apellido_materno;
                    Contacto.celular = contacto.celular;
                    Contacto.email = contacto.email;
                    Contacto.telefono = contacto.telefono;

                
                }
                db.SaveChanges();
            }
            return new json_respuestas(true, "Contacto guardado con éxito", false);
        }
        catch (Exception ex)
        {

            return new json_respuestas(false, "Ocurrio un error al guardar el contacto", true);
        }
    }
    
}
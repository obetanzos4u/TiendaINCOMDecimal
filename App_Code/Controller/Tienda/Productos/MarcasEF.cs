using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de MarcasEF
/// </summary>
public class MarcasEF
{
    public MarcasEF()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }


    /// <summary>
    /// 20210404 CM - Obtiene todas las marcas.
    /// </summary>
    public static List<string> obtenerTodas()
    {
        try
        {

            using (var db = new tiendaEntities())
            {
                var marcas = db.productos_Datos
                   .AsNoTracking()
                   .Select(m => m.marca)
                   .Distinct()
                   .ToList();


                return marcas;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Categorias
/// </summary>
public class CategoriasEF
{
    public CategoriasEF()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    /// <summary>
    /// 20210404 CM - Obtiene todas las categorias.
    /// </summary>
    public static List<categoria> obtenerTodas()
    {
        try
        {

            using (var db = new tiendaEntities())
            {
                var categorias = db.categorias
                    .AsNoTracking()
                    .ToList();


                return categorias;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    /// <summary>
    /// 20210404 CM - Obtiene todas las categorias de nivel 1.
    /// </summary>
    public static List<categoria> obtenerNivel_1()
    {
        try
        {

            using (var db = new tiendaEntities())
            {
                var categorias = db.categorias
                     .AsNoTracking()
                    .Where(c => c.nivel == 1)
                   
                    .ToList();


                return categorias;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    /// <summary>
    /// 20210404 CM - Obtiene todas las categorias de nivel 2.
    /// </summary>
    public static List<categoria> obtenerNivel_2()
    {
        try
        {

            using (var db = new tiendaEntities())
            {
                var categorias = db.categorias
                      .AsNoTracking()
                      .Where(c => c.nivel == 2)
                    .ToList();


                return categorias;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    /// <summary>
    /// 20210404 CM - Obtiene todas las categorias de nivel 3.
    /// </summary>
    public static List<categoria> obtenerNivel_3()
    {
        try
        {

            using (var db = new tiendaEntities())
            {
                var categorias = db.categorias
                      .AsNoTracking()
                      .Where(c => c.nivel == 3)
                    .ToList();


                return categorias;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }
}
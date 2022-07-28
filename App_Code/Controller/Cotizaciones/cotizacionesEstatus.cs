using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de cotizacionesEstatus
/// </summary>
public class CotizacionesEstatusController
{
    public CotizacionesEstatusController()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }


    public static List<base_cotizaciones_estatus> obtenerEstatus()
    {

        using (tiendaEntities db = new tiendaEntities())
        {
            var listadoEstatus = db.base_cotizaciones_estatus.ToList();
            return listadoEstatus;
        }
    }

    public static void GuardarEstatus(string numero_operacion, int idEstatus)
    {

    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Descripción breve de buscador
/// </summary>
public class buscador
{
    public buscador()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    /// <summary>
    /// Recibe un DataTable con un término de búsqueda y un query, devuelve un DT con los DataRows con incidencias y resalta el texto en el HTML;
    /// </summary>
    public static DataTable filtrar(Control t, DataTable dt, string query, string term)
    {
        if (dt != null)
        {
            term = textTools.lineSimple(term);
            DataView DV = new DataView(dt);

            DV.RowFilter = query;

            //  ScriptManager.RegisterStartupScript(t, typeof(Control), "find", "$('body').highlight('" + term + "');", true);

            return DV.ToTable();
        }
        else return dt;

    }
}
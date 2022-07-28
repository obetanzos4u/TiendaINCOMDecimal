using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Impuestos
/// </summary>
public class Impuestos
{

    static readonly decimal MultiplicadorImpuestos = 1.16m;
    public Impuestos()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public static decimal ObterPrecioConImpuestos(decimal precio)
    {
        return precio * MultiplicadorImpuestos;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class PromocionesProductoModel
{

    public  string numero_parte { get; set; }

    public string PromoCode { get; set; }

    public  DateTime FechaActualización { get; set; }

    public PromocionesProductoModel()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
}

/// <summary>
/// Descripción breve de Promociones
/// </summary>
public class Promociones
{
    public Promociones()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
}
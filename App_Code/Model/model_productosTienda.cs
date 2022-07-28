using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de model_productosTienda
/// </summary>
public class model_productosTienda
{
    public model_productosTienda()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public int id { get; set; }
    public string numero_parte { get; set; }
    public string titulo { get; set; }
    public string descripcion_corta { get; set; }
    public string titulo_corto_ingles { get; set; }
    public string especificaciones { get; set; }
    public string marca { get; set; }
    public string[] categoria_identificador { get; set; }
    public string[] imagenes { get; set; }
    public string[] metatags { get; set; }
    public float peso { get; set; }
    public float alto { get; set; }
    public float ancho { get; set; }
    public float profundidad { get; set; }
    public string pdf { get; set; }
    public string video { get; set; }

    public string unidad_venta { get; set; }

    public decimal cantidad { get; set; }
    public string unidad { get; set; }

    public string producto_alternativo { get; set; }
    public string productos_relacionados { get; set; }
    public string atributos { get; set; }

    public string noParte_proveedor { get; set; }

    public string noParte_interno { get; set; }
    public string upc { get; set; }
    public string noParte_Competidor { get; set; }
    public int orden { get; set; }
    public string[] etiquetas { get; set; }
    public int disponibleVenta { get; set; }

    public string[] rol_visibilidad { get; set; }
    public string[] rol_preciosMultiplicador { get; set; }

    public string id_cliente { get; set; }
    public string moneda_fija { get; set; }
    public decimal precio { get; set; }

    public string moneda_rangos { get; set; }

    public decimal precio1 { get; set; }
    public decimal min1 { get; set; }
    public decimal max1 { get; set; }

    public decimal precio2 { get; set; }
    public decimal min2 { get; set; }
    public decimal max2 { get; set; }

    public decimal precio3 { get; set; }
    public decimal min3 { get; set; }
    public decimal max3 { get; set; }

    public decimal precio4 { get; set; }
    public decimal min4 { get; set; }
    public decimal max4 { get; set; }

    public decimal precio5 { get; set; }
    public decimal min5 { get; set; }
    public decimal max5 { get; set; }
}
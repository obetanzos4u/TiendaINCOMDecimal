using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de model_configuracion_sliders
/// </summary>
public class model_configuracion_sliders {
    public int? id { get; set; }
    /// <summary>
    /// [sliderHome],[catalogos],[sidebarProducto],[sidebarCategoria]
    /// </summary>
    public string seccion { get; set; }
    public string titulo { get; set; }
    public string descripcion { get; set; }
    public string nombreArchivo { get; set; }
    public int? activo { get; set; }
    public int? posicion { get; set; }
    public string link { get; set; }
    public string opciones { get; set; }
    public float duracion { get; set; }

    }
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de model_cotizacionesProductos
/// </summary>
public class model_cotizacionesProductos
    {
    public model_cotizacionesProductos()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    public int id { get; set; }
    /// <summary>
    /// Se refiere al usuario que agrega dicho producto
    /// </summary>
    public string usuario { get; set; }
    // -- se refiere al usuario que agrega dicho producto
    public int orden { get; set; }
    /// <summary>
    /// Establecer: |0|=Desactivado, |1|=Activo
    /// </summary>
    public int activo { get; set; }
    /// <summary>
    ///Establecer dependiendo el origen del producto: |1|=Base, |2|=Personalizado, |3|=Servicio
    /// </summary>
    public int tipo { get; set; }
    /// <summary>
    /// Campo para establecer si es alternativo. [0] NO es alternativo, [1] SI es un producto alternativo o sugerencia. Campo informativo
    /// </summary> public int alternativo { get; set; }
    public DateTime fecha_creacion { get; set; }
    public string numero_parte { get; set; }
    public string descripcion { get; set; }
    public string marca { get; set; }
    public string moneda { get; set; }
    public decimal tipo_cambio { get; set; }
    public DateTime fecha_tipo_cambio { get; set; }
    public string unidad { get; set; }
    public decimal precio_unitario { get; set; }
    public decimal cantidad { get; set; }
    public decimal precio_total { get; set; }
    public float stock1 { get; set; }
    public DateTime stock1_fecha { get; set; }
    public float stock2 { get; set; }
    public DateTime stock2_fecha { get; set; }

    
}
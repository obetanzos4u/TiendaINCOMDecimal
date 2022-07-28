using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de model_cotizacionDatos
/// </summary>
public class model_pedidos_datosNumericos
    {
    public int id { get; set; }
    public string numero_operacion { get; set; }
    public string monedaCotizacion { get; set; }
    public float tipo_cambio { get; set; }
    public DateTime fecha_tipo_cambio { get; set; }
    public float subtotal { get; set; }
    public float envio { get; set; }
    public string monedaEnvio { get; set; }
    public float impuestos { get; set; }
    public string nombreImpuestos { get; set; }
    public float total { get; set; }
    }


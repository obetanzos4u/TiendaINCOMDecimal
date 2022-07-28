using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Modelo Direcciones de envío
/// </summary>
public class model_direccionesEnvio
{
    public int id { get; set; }
    public int id_cliente { get; set; }
    
    public string nombre_direccion { get; set; }
    public string calle { get; set; }
    public string numero { get; set; }
    public string numero_interior { get; set; }
    public string colonia { get; set; }
    public string delegacion_municipio { get; set; }

    public string ciudad { get; set; }
    public string estado { get; set; }
    public string codigo_postal { get; set; }
    public string pais { get; set; }
    public string referencias { get; set; }
}
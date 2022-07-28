using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ModelContactosValidador
/// </summary>
public  class Modeldirecciones_envio
{
    public int id { get; set; }
    public string nombre_direccion { get; set; }
    public int id_cliente { get; set; }
    public string calle { get; set; }
    public string numero { get; set; }
    public string colonia { get; set; }
    public string delegacion_municipio { get; set; }
    public string estado { get; set; }
    public string codigo_postal { get; set; }
    public string pais { get; set; }
    public string referencias { get; set; }
    public Nullable<bool> direccion_predeterminada { get; set; }
    public string numero_interior { get; set; }
    public string ciudad { get; set; }

    [JsonIgnore]
    public virtual usuario usuario { get; set; }
}
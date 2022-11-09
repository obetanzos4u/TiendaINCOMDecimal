using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ModelDireccionFacturacionValidador
/// </summary>
public class ModelDireccionFacturacionValidador
{
    public int id { get; set; }
    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "{0} es requerido")]
    [StringLength(20, MinimumLength = 3,
   ErrorMessage = "El nombre debe ser mínimo 3  y máximo 20 caracteres.")]

    public string nombre_direccion { get; set; }

    [Display(Name = "Razón Social")]
    [Required(ErrorMessage = "{0} es requerido")]
    [StringLength(150, MinimumLength = 5,
   ErrorMessage = "La {0} debe ser mínimo 5 y máximo 150 caracteres.")]

    public string razon_social { get; set; }

    [Display(Name = "RFC")]
    [Required(ErrorMessage = "{0} es requerido")]
    [StringLength(15, MinimumLength = 3,
   ErrorMessage = "El RFC debe ser mínimo 3 y máximo 15 caracteres.")]

    public string rfc { get; set; }
    public string regimenFiscal { get; set; }
    public string calle { get; set; }
    public string numero { get; set; }
    public string colonia { get; set; }
    public string delegacion_municipio { get; set; }
    public string estado { get; set; }
    public string codigo_postal { get; set; }
    public string pais { get; set; }
    public string ciudad { get; set; }
}
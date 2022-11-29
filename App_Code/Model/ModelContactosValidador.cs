using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ModelContactosValidador
/// </summary>
public class ModelContactosValidador
{
    public int id { get; set; }
    public int id_cliente { get; set; }
    [Display(Name = "Nombre")]
    [Required(ErrorMessage = "{0} es requerido")]
    [RegularExpression("^[a-zA-ZÀ-ÿ\u00f1\u00d1 ,.'-]+$", ErrorMessage = "Nombre no válido")]
    //[StringLength(20, MinimumLength = 3, ErrorMessage = "El nombre debe ser mínimo 3 y máximo 20 caracteres.")]
    public string nombre { get; set; }
    [Display(Name = "Apellido(s)")]
    [Required(ErrorMessage = "{0} es requerido")]
    [RegularExpression("^[a-zA-ZÀ-ÿ\u00f1\u00d1 ,.'-]+$", ErrorMessage = "Apellido(s) no válido(s)")]
    //[StringLength(20, MinimumLength = 3, ErrorMessage = "El apellido paterno debe ser mínimo 3 y máximo 20 caracteres.")]
    public string apellido_paterno { get; set; }
    //[Display(Name = "Apellido materno")]
    //[Required(ErrorMessage = "{0} es requerido")]
    //[StringLength(20, MinimumLength = 3, ErrorMessage = "El apellido materno debe ser mínimo 3 y máximo 20 caracteres.")]
    public string apellido_materno { get; set; }
    public string email { get; set; }
    [Display(Name = "Teléfono")]
    [Required(ErrorMessage = "{0} es requerido")]
    [RegularExpression("^(\\+\\d{1,2}\\s)?\\(?\\d{2}\\)?[\\s.-]?\\d{4}[\\s.-]?\\d{4}$", ErrorMessage = "El teléfono no es válido")]
    public string celular { get; set; }
    [Display(Name = "Teléfono alternativo")]
    [RegularExpression("^(\\+\\d{1,2}\\s)?\\(?\\d{2}\\)?[\\s.-]?\\d{4}[\\s.-]?\\d{4}$", ErrorMessage = "El teléfono alternativo no es válido")]
    public string telefono { get; set; }
}
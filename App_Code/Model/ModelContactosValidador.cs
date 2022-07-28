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
    [StringLength(20, MinimumLength = 3,
       ErrorMessage = "El nombre debe ser mínimo 3  y máximo 20 caracteres.")]

    public string nombre { get; set; }

   [Display(Name = "Apellido paterno")]
    [Required(ErrorMessage = "{0} es requerido")]
    [StringLength(20, MinimumLength = 3,
       ErrorMessage = "El apellido paterno debe ser mínimo 3  y máximo 20 caracteres.")]
    public string apellido_paterno { get; set; }


    [Display(Name = "Apellido materno")]
    [Required(ErrorMessage = "{0} es requerido")]
    [StringLength(20, MinimumLength = 3,
        ErrorMessage = "El apellido materno debe ser mínimo 3  y máximo 20 caracteres.")]
    public string apellido_materno { get; set; }
    public string email { get; set; }
    public string telefono { get; set; }
    public string celular { get; set; }

   
}
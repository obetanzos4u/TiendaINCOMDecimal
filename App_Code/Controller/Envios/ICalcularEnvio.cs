using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ICalcularEnvio
/// </summary>
public interface ICalcularEnvio
{
    string Tipo { get; set; }
    string Numero_Operacion { get; set; }
    List<object> Productos { get; set; }
    dynamic DireccionEnvio { get; set; }

    decimal? CostoEnvio { get; set; }
    bool IsValidCalculo { get; set; }
    string MessageCalculo { get; set; }

    void AgregarProducto(string Numero_Parte, decimal? PesoKg, decimal? Largo,
        decimal? Ancho, decimal? Alto, decimal? Cantidad, bool? RotacionVertical, bool? RotacionHorizontal);
    void ValidarDireccion();

    void ValidarTipo();
 
    System.Threading.Tasks.Task<json_respuestas> CalcularEnvio();

}
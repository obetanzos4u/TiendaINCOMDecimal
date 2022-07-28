using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de model_usuarios
/// </summary>
public class model_contactos
{
    public int id { get; set; }
    public int id_cliente { get; set; }

    public string nombre { get; set; }
    public string apellido_paterno { get; set; }
    public string apellido_materno { get; set; }
    public string email { get; set; }
    public string telefono { get; set; }
    public string celular { get; set; }

}
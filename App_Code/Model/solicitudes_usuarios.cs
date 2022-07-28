using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// -- Modelo llevara el control de solicitudes de contactos de diferentes medios
/// </summary>
public class model_solicitudes_usuarios {
    public int id { get; set; }
    public DateTime fechaSolicitud { get; set; }

    public string nombre { get; set; }
    public string email { get; set; }
    public string telefono { get; set; }
    /// <summary>
    /// [0] [1] | Si al momento de la solicitud se encontra su email en la tabla usuarios
    /// </summary>
    public int? registrado { get; set; }
    /// <summary>
    /// Número de parte del que se solicita información, no es obligatorio
    /// </summary>
    public string producto { get; set; } /// <summary>
                                       ///  [contacto] [asesoria] [solicitud Ficha] [solicitud Cotizacion] [queja]
                                       /// </summary>
    public string asunto { get; set; }
    public string comentario { get; set; }

    /// <summary>
    /// 0 desactivado, 1 activado, indica si ya se realizó siguimiento
    /// </summary>
    public int? activo { get; set; }
    public DateTime fechaRespuesta { get; set; }
    }
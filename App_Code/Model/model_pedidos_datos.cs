using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de model_cotizacionDatos
/// </summary>
public class model_pedidos_datos
{
    public int id { get; set; }
    public string nombre_pedido { get; set; }
    public string numero_operacion { get; set; }
    public DateTime fecha_creacion { get { return utilidad_fechas.obtenerCentral(); } }
    public string creada_por { get { return HttpContext.Current.User.Identity.Name; } } // -- se refiere al usuario login que crea dicha cotización, vendedor/cliente
    public int mod_asesor { get; set; }
    public string id_cliente { get; set; } // -- se refiere al ID cliente que se le trabaja dicha cotización si fuera modalidad asesores
    public string usuario_cliente { get; set; } // --
    public string cliente_nombre { get; set; } // --
    public string cliente_apellido_paterno { get; set; } // --
    public string cliente_apellido_materno { get; set; } // --
    public string email { get; set; }
    public string telefono { get; set; }
    public string celular { get; set; }
    public int activo { get; set; } // 0 Desactivado, 1 Activo
    public string comentarios { get; set; }
    public int preCotizacion { get; set; } // 0 No, 1 Si, indica si hubo una cotización previa.
    public string numero_operacion_cotizacion { get; set; } // 
}

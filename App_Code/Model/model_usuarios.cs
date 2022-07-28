using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de model_usuarios
/// </summary>
[Serializable()]
public partial class model_usuarios
{

    public int id { get; set; }
    public DateTime fecha_registro { get; set; }
    public string nombre { get; set; }
    public string apellido_paterno { get; set; }
    public string apellido_materno { get; set; }
    public string email { get; set; }
    public string celular { get; set; }
    public string telefono { get; set; }
    public string password { get; set; }
    /// <summary>
    /// [cliente] = clientes en general, [usuario] = asesores, marketing etc.
    /// </summary>
    public string tipo_de_usuario { get; set; } 
    /// <summary>
    /// |1| = usuario, |2| = asesor, |3| = administrador.
    /// </summary>
    public int rango { get; set; }
    /// <summary>
    ///telemarketing, ventas, marketing etc.
    /// </summary>
    public string departamento { get; set; } // telemarketing, ventas, marketing etc.
    /// <summary>
    /// Exclusivo clientes
    /// </summary>
    public string perfil_cliente { get; set; } // Exclusivo clientes
    /// <summary>
    /// Exclusivo clientes
    /// </summary>
    public string idSAP { get; set; } // Exclusivo clientes
    /// <summary>
    /// Exclusivo clientes
    /// </summary>3
    /// -
    public string rol_precios_multiplicador { get; set; } // Exclusivo clientes

    /// <summary>
    /// Exclusivo clientes
    /// </summary>
    public string rol_productos { get; set; } // Exclusivo clientes
    /// <summary>
    /// Exclusivo clientes
    /// </summary>
    public string[] rol_categorias { get; set; } // Exclusivo clientes
    /// <summary>
    /// Exclusivo clientes
    /// </summary>
    public string asesor_base { get; set; } // Exclusivo clientes
    /// <summary>
    /// Exclusivo clientes
    /// </summary>
    public string grupo_asesor { get; set; } // Exclusivo clientes 
    /// <summary>
    /// Exclusivo clientes
    /// </summary>
    public string[] asesor_adicional { get; set; } // Exclusivo clientes
    /// <summary>
    /// Exclusivo clientes
    /// </summary>
    public string[] grupo_asesores_adicional { get; set; } // Exclusivo clientes
    /// <summary>
    /// UPDATE 20180105, funcionalidad para validar permisos de visualización secciones/páginas a usuarios, independiente de la modalidad asesor.
    /// </summary>
    public string grupoPrivacidad { get; set; } // UPDATE 20180105, funcionalidad para validar permisos de visualización secciones/páginas a usuarios, independiente de la modalidad asesor.
    public string grupo_usuario { get; set; } // UPDATE 20180409, funcionalidad para validar permisos de visualización de clientes/grupos de asesores
    public DateTime ultimo_inicio_sesion { get; set; } 
    public DateTime fecha_nacimiento { get; set; }
    /// <summary>
    /// Campo agregado el 20180730,Sirve para registrar que asesor lo registro
    /// </summary>
    public string registrado_por { get; set; }
    public string cuenta_activa { get; set; }
    
}
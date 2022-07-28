using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de model_productosDatos
/// </summary>
public class model_productosDatos
{
	public int id { get; set; }
	public string numero_parte { get; set; }
	public string titulo { get; set; }
	public string descripcion_corta { get; set; }
	public int titulo_corto_ingles { get; set; }
	public int especificaciones { get; set; }
	public string marca { get; set; }
	public string categoria_identificador { get; set; }
	public string[] imagenes { get; set; }
	public string metatags { get; set; }
    public float peso { get; set; }
    public float alto { get; set; }
    public float ancho { get; set; }
    public float profundidad { get; set; }
    public string pdf { get; set; }
    public string video { get; set; }
    public string unidad_venta { get; set; }
    public float cantidad { get; set; }
    public string unidad { get; set; }
    public string producto_alternativo { get; set; }
    public string productos_relacionados { get; set; }
    public string atributos { get; set; }
    public string noParte_proveedor { get; set; }
    public string noParte_interno { get; set; }
    public string upc { get; set; }
    public string noParte_Competidor { get; set; }
    public int orden { get; set; }
    public string etiquetas { get; set; }
    public int disponibleVenta { get; set; }
  

}


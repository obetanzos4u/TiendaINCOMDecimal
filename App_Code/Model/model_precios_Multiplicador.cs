using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de model_categorias
/// </summary>
public class model_precios_Multiplicador {
	public int id { get; set; }
    public int nivel { get; set; }
    public string nombre_multiplicador { get; set; }
	public float multiplicador_valor { get; set; }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de model_categorias
/// </summary>
public class model_precios_ListaFija
{
	public int id { get; set; }
	public string id_cliente { get; set; }
	public string numero_parte { get; set; }
	public float precio { get; set; }
	public string moneda { get; set; }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de model_categorias
/// </summary>
public class model_categorias
{
	

	public int id { get; set; }
	public string nombre { get; set; }
	public string identificador { get; set; }
	public string asociacion { get; set; }
	public int nivel { get; set; }
	public int orden { get; set; }
	public string imagen { get; set; }
	public string rol_categoria { get; set; }
	public string productos_Destacados { get; set; }

	public string descripcion { get; set; }

}
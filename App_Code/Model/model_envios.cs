using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de model_envios
/// </summary>
public class model_envios
{

    public model_envios() {
        peso = 0;
        alto = 0;
        ancho = 0;
        profundidad = 0;
        costoEnvio = 0;
        nombre = "Ninguno";
        
        monedaEnvio = "";
        }
    public int id { get; set; }
	public string nombre { get; set; }
	public string estado { get; set; }
    public string codigo_postal { get; set; }
    public int productosCantidad { get; set; }
    public float peso { get; set; }
    public float alto { get; set; }
    public float ancho { get; set; }
    public float profundidad { get; set; }
    public float costoEnvio { get; set; }
    public string monedaEnvio { get; set; }

    }

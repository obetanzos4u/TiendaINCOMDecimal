using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Grupo de utilidades para validar campos de formulario
/// </summary>
public class validar_camposCotizacionProductos {

      public string mensaje { get; set; }
      public bool resultado { get; set; }
    /// <summary>
    /// Valida y recibe: numero_parte, marca, descripcion, cantidad, unidad, tipo
    /// </summary>
     public void producto(model_cotizacionesProductos producto) {

        if (producto.numero_parte.Length < 2 || producto.numero_parte.Length > 50) { mensaje = "El numero de parte no cumple con la longitud mínima/máxima requerida"; resultado = false; return; }
        if (producto.marca.Length > 50)  { mensaje = "El campo marca no cumple con la longitud mínima/máxima requerida"; resultado = false; return; }
        if (producto.descripcion.Length < 15 || producto.descripcion.Length > 500) { mensaje = "La descripción no cumple con la longitud "; resultado = false; return; }
        if (producto.unidad.Length > 10  ) { mensaje = "La unidad no cumple con la longitud "; resultado = false; return; }
        if (producto.cantidad <= 0) { mensaje = "Ingresa una cantidad válida"; resultado = false; return; }
        if (producto.precio_unitario <= 0) { mensaje = "Ingresa un precio  válido"; resultado = false; return; }

        mensaje = "Producto agregado con éxito";
        resultado =true;
        }
    
    }
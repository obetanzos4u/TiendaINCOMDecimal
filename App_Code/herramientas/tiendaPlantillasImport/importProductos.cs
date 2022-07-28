using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de productos
/// </summary>
public class importProductos {
   
    public importProductos() {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
        }

    public static Dictionary<string, int> validar(string nombreTablaValidar) {

        switch (nombreTablaValidar) {
            case "productos_datos" : return productosDatos();  break;


            }
        return null;
        }

        public static Dictionary<string,int> productosDatos() {
        
        Dictionary<string, int> productos_datos = new Dictionary<string, int>();
        productos_datos.Add("numero_parte", 50);
        productos_datos.Add("titulo", 130);
        productos_datos.Add("descripcion_corta", 500);
        productos_datos.Add("titulo_corto_ingles", 250);
        productos_datos.Add("especificaciones", 4000);
        productos_datos.Add("marca", 50);
        productos_datos.Add("categoria_identificador", 100);
        productos_datos.Add("imagenes", 500);
        productos_datos.Add("metatags", 250);
        productos_datos.Add("pdf", 350);
        productos_datos.Add("video", 250);
        productos_datos.Add("unidad_venta", 25);
        productos_datos.Add("producto_alternativo", 150);
        productos_datos.Add("productos_relacionados", 250);
        productos_datos.Add("atributos",3000);
        productos_datos.Add("noParte_proveedor", 50);
        productos_datos.Add("noParte_interno", 100);
        productos_datos.Add("upc", 50);
        productos_datos.Add("noParte_Competidor", 250);
        productos_datos.Add("etiquetas", 150);
        productos_datos.Add("noParte_Sap", 100);
        productos_datos.Add("avisos", 4000);


        return productos_datos;

        }
    }



  
    
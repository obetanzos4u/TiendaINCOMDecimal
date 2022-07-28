using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de archivosManejador
/// </summary>
public class archivosManejador {

    static public string filePath { get; set; }
   
    static public bool existe { get; set; }
    static public string appPath = HttpContext.Current.Request.PhysicalApplicationPath;
    static public string urlImagenTemporal = "/img/webUI/producto-imagen-temporal.jpg";
    static public string pdfDirectorio= appPath.Replace("\\", "/") + "documents/pdf/";
    static public string pdfDirectorioWeb =  "/documents/pdf/";

    public archivosManejador() {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
        }
    /// <summary>
    /// Regresa una ruta de imagen si el archivo de imagen del producto existe o no.
    /// </summary>
    public static string imagenProducto(string imagen) {


        if (string.IsNullOrEmpty(imagen))
            return urlImagenTemporal;
        else {
            string filePath = Path.Combine(appPath, "img_catalog\\min\\min-" + imagen);

            if (System.IO.File.Exists(filePath)) {  // Comprobamos su existencia
                return "/img_catalog/min/min-" + imagen;
                } else {
                return urlImagenTemporal;
                }

            }
        }
    /// <summary>
    /// Regresa una ruta de imagen si el archivo de imagen del producto existe o no.
    /// </summary>
    public static string imagenProductoXL(string imagen)
    {


        if (string.IsNullOrEmpty(imagen))
            return urlImagenTemporal;
        else
        {
            string filePath = Path.Combine(appPath, "img_catalog\\" + imagen);

            if (System.IO.File.Exists(filePath))
            {  // Comprobamos su existencia
                return "/img_catalog/" + imagen;
            }
            else
            {
                return urlImagenTemporal;
            }

        }
    }

    /// <summary>
    ///  20210504 CM - Regresa una ruta de imagen si el archivo de imagen del producto existe o no.
    /// Los productos personalizados son productos que no son base de la tienda y la información se ingresa manualmente.
    /// Para mostrar la imagen solo hay que subirlo al directorio [mg_catalog/personalizado]con el número de parte.
    /// </summary>
    public static string imagenProductoPersonalizado(string imagen) {

        // Como los archivos no pueden tener diagonales, se suben con guón bajo
          imagen = imagen.Replace("/", "_");
        if (string.IsNullOrEmpty(imagen))
            return urlImagenTemporal;
        else {
            string filePathPersonalizado = Path.Combine(appPath, "img_catalog\\personalizado\\" + imagen);
         

          

            if (System.IO.File.Exists(filePathPersonalizado)) {  // Comprobamos su existencia
                return "/img_catalog/personalizado/" + imagen;
            }
            else   
            {


                return urlImagenTemporal;
            }
            
            
               
           

        }
    }
    /// <summary>
    /// Validamos la ruta de un archivo PDF en  ["~/documents/pdf/"]
    /// </summary>
    public static bool validarExistenciaPDF(string fileNamePDF) {
        string fileFullPath =  appPath + "documents\\pdf\\" + fileNamePDF;
        bool existencia = System.IO.File.Exists(fileFullPath);
        return existencia;
           
        }
    /// <summary>
    /// Validamos la ruta de un archivo PDF en  ["~/documents/pdf/personalizado"]
    /// </summary>
    public static bool validarExistenciaPDFProductoPersonalizado(string fileNamePDF) {
        string fileFullPath = appPath + "documents\\pdf\\personalizado\\" + fileNamePDF;
        bool existencia = System.IO.File.Exists(fileFullPath);
        return existencia;

    }

    static bool validarExistencia() {
        if (System.IO.File.Exists(filePath))
            return true;
        else return false;
        }

    public static bool validarExistencia(string filePath) {
        if (System.IO.File.Exists(filePath))
            return true;
        else return false;

        }

    public static bool eliminarArchivo(string filePath) {
        if (System.IO.File.Exists(filePath)) {
            try {

                System.IO.File.Delete(filePath);
                return true;
                }

            catch (Exception ex) {
                return false;
                }
            } else return false;

        }
    public static string reemplazarEnArchivo( string fileNamePath,  Dictionary<string, string> DictReplace) {

 

        filePath = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, fileNamePath);

        string body = string.Empty;

        using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~") + filePath)) {
            body = reader.ReadToEnd();
            }

            if (DictReplace.Count >= 1) {
                 foreach (var item in DictReplace) {
                    body = body.Replace(item.Key, item.Value);
                    }
                }

        return body;

        }
   
       
            
    }
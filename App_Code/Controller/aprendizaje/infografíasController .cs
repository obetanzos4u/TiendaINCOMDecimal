using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Descripción breve de glosario
/// </summary>
public class infografíasController
{
    public infografíasController() {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }


    public bool result { get; set; }
    public string message { get; set; }



     public bool validarMiniatura(HttpPostedFile file)
    {

        if (file.ContentLength == 0)
        {
            result = false;
            message = "No se ha especificado un archivo de miniatura";
            return false;
        }
        if (file.ContentLength > 180000)
        {
            result = false;
            message = "Se ha sobre pasado el peso de 100 KB para la miniatura, porfavor optimiza tu imagén.";
            return false;
        }


        if (file.ContentType != "image/jpeg")
        {
            result = false;
            message = "No haz seleccionado un archivo de tipo image/jpeg para la miniatura";
            return false;
        }

        bool archivoExistente = File.Exists(Path.Combine(infografíasController.pathMiniaturas, file.FileName));
        if (archivoExistente)
        {
            result = false;
            message = "Una miniatura con el mismo nombre ya se encuentra cargada.";
            return false;
        }


        result = true;
        message = "Archivo de miniatura correcto.";

        return true;

    }

    public bool validarInfografía(HttpPostedFile file)
    {

        if (file.ContentLength == 0)
        {
            result = false;
            message = "No se ha especificado un archivo de infografía";
            return false;
        }

        string fileName = file.FileName;
        string extension = fileName.Substring(fileName.Length - 3, 3);

       

        bool archivoExistente = File.Exists(Path.Combine(infografíasController.path, fileName));
        if (archivoExistente)
        {
            result = false;
            message = "Una infografía con el mismo nombre ya se encuentra cargada.";
            return false;
        }


        result = true;
        message = "Archivo de miniatura correcto.";

        return true;

    }

 
    /// <summary>
    /// Es la ruta donde se guardan las infografías  [img/infografías/], Puede ser un archivo de imagen o una miniatura.
    /// </summary>

    static public string path = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"img\infografías");

    /// <summary>
    /// Es la ruta donde se guardan las miniaturas de infografías [img/infografías/miniatura/]
    /// </summary>

    static  public string pathMiniaturas = Path.Combine(HttpContext.Current.Server.MapPath("~"), @"img\infografías\miniatura");


    /// <summary>
    /// 20200624 - Obtiene todos registros de la tabla [infografías]
    /// </summary>

    public static  List<infografías> obtener() {
        try {

            using (var db = new tiendaEntities()) {
                var infografías = db.infografías.OrderByDescending(i => i.fecha).ToList();


            
                return infografías;
            }
        }
        catch (Exception ex) {

            return null;
        }
    }

    /// <summary>
    /// 20200629 - Obtiene un registro por ID
    /// </summary>

    public static infografías obtener(int idInfografía)
    {
        try
        {

            using (var db = new tiendaEntities())
            {
                var infografía = db.infografías.Where(p => p.id == idInfografía).SingleOrDefault();



                return infografía;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    /// <summary>
    /// 20200624 - Busca en el campo [titulo] el término de búsqueda.
    /// </summary>
    public static List<infografías> obtenerByBusqueda(string texto) {
        try {
            string[] terminos = texto.Split(' ');
            using (var db = new tiendaEntities()) {
                var infografías = db.infografías

                    .Where(a => terminos.All(p => a.titulo.Contains(p))).ToList();



                return infografías;
            }
        }
        catch (Exception ex) {


            return null;
        }
    }



    /// <summary>
    /// 20200624 RC - Obtiene el total de infografías, devuelve -1 cuando hay se genera un Exception
    /// </summary>
    public static int count() {
        try {

            using (var db = new tiendaEntities()) {
                var total = db.infografías.Count();
                return total;
            }
        }
        catch (Exception ex) {

            return -1;
        }
    }



    public async Task<int> guardarAsync(infografías infografía)
    {
        try
        {
            using (var db = new tiendaEntities())
            {
                db.infografías.Add(infografía);
                int x = await db.SaveChangesAsync();
                return infografía.id;
            }
        }
        catch (Exception ex)
        {

            result = false;
            message = "Error al agregar infografía";



            return 0;
        }
    }
    public void guardar(infografías infografía)
    {
        try
        {
            using (var db = new tiendaEntities())
            {
                db.infografías.Add(infografía);
                int x = db.SaveChanges();

            }

            result = true;
            message = "Inografía guardada con éxito";
        }
        catch (Exception ex)
        {

            result = false;
            message = "Error al agregar infografía";

            devNotificaciones.error(message, ex);


        }
    }

    public static void eliminarFileMiniatura(string nombreImagenMiniatura)
    {
        string filePathMiniatura = Path.Combine(pathMiniaturas, nombreImagenMiniatura);
        bool fileExistsMiniatura = File.Exists(filePathMiniatura);
        if (fileExistsMiniatura) File.Delete(filePathMiniatura);
    }

    public static void eliminarFileInfografia(string nombreArchivo)
    {
        string filePath = Path.Combine(path, nombreArchivo);
        bool fileExists = File.Exists(filePath);
        if (fileExists) File.Delete(filePath);

    }
    /// <summary>
    /// 20200624 RC - Elimina el registro de la infografía y también sus respectivos archivos (infografía y miniatura).
    /// </summary>
    public void eliminar(int idInfografía)
    {
        try
        {

            using (var context = new tiendaEntities())
            {

                var infografía = context.infografías.Where(s => s.id == idInfografía).SingleOrDefault();


                // Procedemos a borrar el archivo físicamente
                string filePath = Path.Combine(path, infografía.nombreArchivo);
                bool fileExists = File.Exists(filePath);
                if (fileExists) File.Delete(filePath);


                // Procedemos a borrar su miniatura
                string filePathMiniatura = Path.Combine(pathMiniaturas, infografía.nombreImagenMiniatura);
                bool fileExistsMiniatura = File.Exists(filePathMiniatura);
                if (fileExistsMiniatura) File.Delete(filePathMiniatura);




                context.infografías.Remove(infografía);

                int resultadoQuery = context.SaveChanges();

                if (resultadoQuery >= 1 && fileExists)
                {
                    message = "El registro y archivos han sido eliminados.";
                }

                if (resultadoQuery == 0 && fileExists)
                {
                    message = "El registro no fué encontrado sin embargo los archivos fué eliminado.";
                }

                if (resultadoQuery >= 1 && fileExists == false)
                {
                    message = "El registro  fué eliminado sin embargo los archivos NO fueron eliminados.";
                }

                result = true;
            }
        }
        catch (Exception ex)
        {

            result = false;
            message = "Error al eliminar";



        }
    }

}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Descripción breve de glosario
/// </summary>
public class glosarioController {
    public glosarioController() {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }


    public bool result { get; set; }
    public string message { get; set; }




    /// <summary>
    /// 20200619 - Obtiene todos registro de una determinada letra.l
    /// </summary>
 
    public static  List<glosario> obtenerByLetra(string letra) {
        try {

            using (var db = new tiendaEntities()) {
                var glosario = db.glosario

                    .Where(s => s.término.StartsWith(letra)).ToList();


            
                return glosario;
            }
        }
        catch (Exception ex) {

            return null;
        }
    }


    public static List<glosario> obtenerByBusqueda(string texto) {
        try {
            string[] terminos = texto.Split(' ');
            using (var db = new tiendaEntities()) {
                var glosario = db.glosario

                    .Where(a => terminos.All(p => a.término.Contains(p) || a.términoInglés.Contains(p) || a.simbolo.Contains(p))).ToList();



                return glosario;
            }
        }
        catch (Exception ex) {


            return null;
        }
    }

 
   
    /// <summary>
    /// 20200611 RC - Obtiene el total de archivos adjuntos ligados a una solicitud de tiempo de entrega
    /// </summary>
    public static int count() {
        try {

            using (var db = new tiendaEntities()) {
                var total = db.glosario.Count();
                return total;
            }
        }
        catch (Exception ex) {

            return -1;
        }
    }
   
}
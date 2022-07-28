using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de utilidadURL
/// </summary>
public class utilidadURL {
    public utilidadURL() {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

   public static string TextoAmigable(string texto) {
        string txtAmigable = texto.Replace(" ", "-")
            .Replace("?", "")
            ;

        return txtAmigable;
    } 
}
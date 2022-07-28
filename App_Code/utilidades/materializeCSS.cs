using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Descripción breve de semantic
/// </summary>
public class materializeCSS : System.Web.UI.Page
{
  

    public static void crear_toast(Control t, string mensaje, bool tipo)
    {
        Random rnd = new Random();
        int n = rnd.Next(1, 10);
         
        string estilo = "";
        string icon = string.Empty;
        if (tipo == true) { estilo = "green darken-2"; icon = "done"; }
        if (tipo == false) { estilo = "red"; icon = "clear"; }
        string script = "M.toast({html: '<i class=\"material-icons\">" + icon + "</i>" + mensaje + "', classes : '" + estilo + "'});";

        
        ScriptManager.RegisterStartupScript(t, typeof(Control), "toast_"+n, script, true);
    }
    /// <summary>
    /// Ejecuta js para abrir o ocultar modal
    /// </summary>
    /// <param name="_action"> [1] para abrir, [0] para cerrar</param>  

    public static void mostrarModal(Control t, string id, int _action ) {
        string  action = "";
        if (_action == 1) action = "open"; else if(_action == 0) action = "close";
        string script = "$(document).ready(function() { $('" + id + "').modal('"+ action + "'); });";
        ScriptManager.RegisterStartupScript(t, typeof(Control), "mostrarModal" + id, script, true);
    }
 

}
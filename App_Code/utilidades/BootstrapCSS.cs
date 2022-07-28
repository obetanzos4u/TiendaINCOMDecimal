using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Utilidades para Bootstrap
/// </summary>
public class BootstrapCSS : System.Web.UI.Page
{

    public enum MessageType
    {
        primary,
        secondary,
        success,
        danger,
        warning,
        info,
        light,
        dark
    }
    public static void Message(Control t,string selector, MessageType MessageType, string Title, string Message)

    {
        Random rnd = new Random();
        int n = rnd.Next(1, 20);
        string script = " BootstrapAlert(\"" + selector+ "\", \"" + MessageType+ "\", \"" + Title + "\", \"" + Message + "\");";
        ScriptManager.RegisterStartupScript(t, typeof(Control), "Alert_" + n, script, true);
    }
    /// <summary>
    /// Registra un RegisterStartupScrupt con   window.location.replace, 4500 default
    /// </summary>
    public static void RedirectJs(Control t, string Url) {
        string script = @"   setTimeout(function () { window.location.replace('" + Url + "')}, 4500);";
        ScriptManager.RegisterStartupScript(t, typeof(Page), "redirección", script, true);
    }
    /// <summary>
    /// Registra un RegisterStartupScrupt con   window.location.replace.
    /// </summary>
    public static void RedirectJs(Control t, string Url, int milisegundos)
    {
        string script = @"   setTimeout(function () { window.location.replace('" + Url + "')}, "+ milisegundos+"); ";
        ScriptManager.RegisterStartupScript(t, typeof(Page), "redirección", script, true);
    }

}
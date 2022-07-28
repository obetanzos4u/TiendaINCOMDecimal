using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Descripción breve de semantic
/// </summary>
public class bulmaCSS : System.Web.UI.Page
{

    public enum MessageType
    {
        success,
        danger,
        warning
    }
    public static void Message(Control t,string selector, MessageType MessageType, string Message)

    {
        Random rnd = new Random();
        int n = rnd.Next(1, 20);
        string script = " BulmaMessage('"+selector+"', '"+ MessageType+"', '" + Message +"');";
        ScriptManager.RegisterStartupScript(t, typeof(Control), "Message_" + n, script, true);
    }
 
 

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Descripción breve de NotiflixJS
/// </summary>
public class NotiflixJS : System.Web.UI.Page
{
    public enum MessageType
    {
        success,
        failure,
        warning,
        info
    }
    public enum LoadingType
    {
        loading,
        remove
    }
    public static void Message(Control t, MessageType MessageType, string Message)
    {
        Random random = new Random();
        int n = random.Next(1,20);
        string script = "NotiflixAlert(\"" + MessageType + "\", \"" + Message + "\");";
        ScriptManager.RegisterStartupScript(t, typeof(Control), "NotiflixAlert_" + n, script, true);
    }
    public static void Loading(Control t, LoadingType LoadingType)
    {
        Random random = new Random();
        int n = random.Next(1,20);
        string script = "NotiflixLoading(\"" + LoadingType + "\");";
        ScriptManager.RegisterStartupScript(t, typeof(Control), "NotiflixLoading_" + n, script, true);
    }
}
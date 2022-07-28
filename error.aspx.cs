using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class error : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        Exception exc = Server.GetLastError();

        // Handle specific exception.
        if (exc is HttpUnhandledException) {
            devNotificaciones.notificacionSimple(exc.ToString());
        }
        // Clear the error from the server.
        Server.ClearError();
    }
}
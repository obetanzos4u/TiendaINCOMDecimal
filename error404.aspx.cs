using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class error_404 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
           // Response.TrySkipIisCustomErrors = true;
          
            Page.Title = "Página no encontrada - Error 404 ";
            Page.MetaDescription = "Página no encontrada";
        }
    }
}
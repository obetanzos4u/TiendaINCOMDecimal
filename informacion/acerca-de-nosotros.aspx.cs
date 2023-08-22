using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class acerca_de_nosotros : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack) {
            Page.Title = "Acerca de nosotros";
            Page.MetaDescription = "Descripción sobre INCOM";
        }
    }
}
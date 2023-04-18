using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class aviso_de_privacidad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack) {
            Page.Title = "Términos y condiciones de compra";
            Page.MetaDescription = "Términos y condiciones de compra";
        }
    }
}
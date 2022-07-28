using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class usuario_aviso_confirmar_password : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {    
        if(!IsPostBack)
      materializeCSS.crear_toast(this, "Se ha enviado un email con las instrucciones", true);
        
    }
}
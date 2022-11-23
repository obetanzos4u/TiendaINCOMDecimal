using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usuario_cliente_finalizado : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        btn_seguir_comprando.NavigateUrl = Request.Url.GetLeftPart(UriPartial.Authority);
    }
}
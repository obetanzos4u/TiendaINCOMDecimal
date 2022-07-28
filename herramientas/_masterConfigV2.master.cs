using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class gnCliente : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
       
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack) {
            usuarios loginUser = usuarios.userLogin();

            if(loginUser.tipo_de_usuario != "usuario") {
                Response.Redirect("~/inicio.aspx");
                }
            }


    }

}

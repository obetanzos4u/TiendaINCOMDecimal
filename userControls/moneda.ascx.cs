using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace tienda {

    
    public partial class uc_moneda : System.Web.UI.UserControl
    {
     

        protected void Page_Init(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                if (Session["monedaTienda"] != null)
                {
                    ddl_moneda.SelectedValue = Session["monedaTienda"].ToString();
                } else
                {
                    Session["monedaTienda"] = ddl_moneda.SelectedValue;
                }
            }
        }

        public  void ddl_default()
        {
            ddl_moneda.CssClass = "browser-default  ddlDefault";
        }
        protected void ddl_moneda_SelectedIndexChanged(object sender, EventArgs e) {
      

            string moneda = ddl_moneda.SelectedValue;
            Session["monedaTienda"] = moneda;

            Response.Redirect(Request.Url.AbsoluteUri);
        }


    }
}


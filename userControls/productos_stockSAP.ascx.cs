using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class uc_productos_stockSAP : System.Web.UI.UserControl
{
    public string numero_parte {
        get { return hf_numero_parte.Value; }   // get method
        set { hf_numero_parte.Value = value; }  // set method
    }



    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            bool login = HttpContext.Current.User.Identity.IsAuthenticated;


            if (login)
            {
  
                content_usuario_logeado.Visible = true;
                content_usuario_visitante.Visible = false;

        

            }
            else
            {

                content_usuario_logeado.Visible = false;
                content_usuario_visitante.Visible = true;

            }
        }
         
    }




    protected void lv_producto_stock_OnItemDataBound(object sender, ListViewItemEventArgs e)
    {

        HtmlAnchor link_categoria = (HtmlAnchor)e.Item.FindControl("link_maps");

        dynamic ubicacion = e.Item.DataItem as dynamic;
        string PlanningAreaID = ubicacion.PlanningAreaID;

        string url101 = "https://g.page/Incom_CDMX?share";
        string url102 = "https://goo.gl/maps/oswKxZjooTXnmzE47";


        if(PlanningAreaID == "101")
        {
            link_categoria.Attributes.Add("href", url101);
        }
        else
        {
            link_categoria.Attributes.Add("href", url102);
        }
    


    }

 
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class menuPrincipal : System.Web.UI.UserControl
{


    protected void Page_PreRender(object sender, EventArgs e)
    {

        if (!IsPostBack) {
          
        }

    }
   


    

    protected void btn_buscarProductos_Click(object sender, EventArgs e) {

        string termino = txt_buscadorProducto.Text;
        termino.Replace("'", "").Replace("<", "").Replace(">", "").Replace("SELECT", "").Replace("Script", "").Replace("==", "").Replace("false", "").Replace("null", "").Replace("true", "").Replace("http", "").Replace("www", ""); 
        string depurado = textTools.lineSimple(termino);

      
         
       Response.Redirect(HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)+ "/productos/buscar?busqueda="+ depurado);


    }
}
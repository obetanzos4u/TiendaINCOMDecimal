using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class service_productos_stock : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        obtener();

    }

    /// <summary>
    /// Devuelve [JSON] con la disponibilidad del producto
    /// </summary>
    public void obtener()
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated) {


            if (Request.QueryString["numero_parte"] != null && Request.QueryString["numero_parte"].Length >= 3)
            {

                string numero_parte = Request.QueryString["numero_parte"].ToString();
                string strCantidad = null;
                int? cantidad = null;

                numero_parte = textTools.lineSimple(numero_parte);

                List<productoSAPModel> productoDisponibilidades = new List<productoSAPModel>();

                SAP_productos obtener = new SAP_productos(numero_parte);


                if (Request.QueryString["cantidad"] != null)
            {
                      strCantidad = Request.QueryString["cantidad"].ToString();
                    cantidad = textTools.soloNumerosInt(strCantidad);

                    obtener.cantidadRequeridoUsuario = cantidad;
            }

              

                productoDisponibilidades = obtener.obtenerProductoStock();

                var json = JsonConvert.SerializeObject(productoDisponibilidades);

                Response.Write(json);
            }
            else
            {
                Response.Write("No se ha especificado un producto");
            }

        } else {
            Response.Write("null");
        };



    
        
    }
}
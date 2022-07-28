using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

public partial class inicio : System.Web.UI.Page
{
    // Se refiere a la antigua URL de productos y establecer la redirección 303
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.RouteData.Values["numero_parte"] != null) {
            string route_numero_parte = Page.RouteData.Values["numero_parte"].ToString();

            productosTienda obtener = new productosTienda();
            DataTable productos = obtener.obtenerProducto(textTools.recuperarURL_NumeroParte(route_numero_parte));

            if(productos.Rows.Count > 0) { 
            string link =  GetRouteUrl("productos", new System.Web.Routing.RouteValueDictionary {
                        { "numero_parte", textTools.limpiarURL_NumeroParte(productos.Rows[0]["numero_parte"].ToString()) },
                         { "marca",  productos.Rows[0]["marca"].ToString() },
                        { "productoNombre",  textTools.limpiarURL_NumeroParte(productos.Rows[0]["titulo"].ToString()) }
                    });

                Response.Clear();
                Response.StatusCode = 301;
                Response.Status = "301 Moved Permanently";
                HttpContext.Current.Response.RedirectPermanent(link);
                Response.AddHeader("Location", link);
                Response.End();
       
            } else {
                Response.StatusCode = 404;
                Response.Status = "404 Page Not Found";
                Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority)+"/error400.aspx", false);
                Response.End();
            }
        }







    }
    }
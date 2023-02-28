using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class basicMaster : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        var datosUsuario = (usuarios)Session["datosUsuario"];
        if (HttpContext.Current.User.Identity.IsAuthenticated == true && datosUsuario == null)
        {
            usuarios session = new usuarios();
            session.establecer_DatosUsuario(HttpContext.Current.User.Identity.Name);
        }
        else if (HttpContext.Current.User.Identity.IsAuthenticated == true && datosUsuario.email == null)
        {
            usuarios session = new usuarios();
            session.establecer_DatosUsuario(HttpContext.Current.User.Identity.Name);
        }
        else if (HttpContext.Current.User.Identity.IsAuthenticated == false && Session["datosUsuario"] == null)
        {
            usuarios session = new usuarios();
            session.establecer_DatosUsuarioVisitante();
        }
        if (Session["impuesto"] == null)
        {
            Session["impuesto"] = 1.16;
        }
        if (Session["tipoCambio"] == null)
        {
            Session["tipoCambio"] = operacionesConfiguraciones.obtenerTipoDeCambio();
        }
        if (Session["modoAsesor"] == null)
        {
            Session["modoAsesor"] = false;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        /*
            Type cstype = this.GetType();
            ClientScriptManager cs = Page.ClientScript;
           
            cs.RegisterClientScriptBlock(cstype, "Materialize", "<link type= 'text/css' rel='stylesheet'  href='" + ResolveUrl("~/css/materialize.css") + "' media='screen'/>");
            cs.RegisterClientScriptBlock(cstype, "incomCSS", "<link type= 'text/css' rel='stylesheet'  href='" + ResolveUrl("~/css/incom.css") + "' media='screen'/>");
            cs.RegisterClientScriptBlock(cstype, "smoothproductsCSS", "<link type= 'text/css' rel='stylesheet'  href='" + ResolveUrl("~/css/smoothproducts.css") + "' media='screen'/>");
            cs.RegisterClientScriptBlock(cstype, "font-awesome", "<link type= 'text/css' rel='stylesheet'  href='" + ResolveUrl("~/css/font-awesome/font-awesome.min.css") + "' media='screen'/>");
            cs.RegisterClientScriptBlock(cstype, "jquery.bxsliderCSS", "<link type= 'text/css' rel='stylesheet'  href='" + ResolveUrl("/css/jquery.bxslider.css") + "' media='screen'/>");
            cs.RegisterClientScriptBlock(cstype, "MaterializeJS", "<script async src = '" + ResolveUrl("~/js/materialize.min.js") + "' ></script>");
            cs.RegisterClientScriptBlock(cstype, "smoothproductsJS", "<script async src = '" + ResolveUrl("~/js/smoothproducts.js") + "' ></script>");
            cs.RegisterClientScriptBlock(cstype, "page_load", "<script async src = '" + ResolveUrl("~/js/page_load.js") + "' ></script>");
            cs.RegisterClientScriptBlock(cstype, "highlight", "<script async src = '" + ResolveUrl("~/js/jquery.highlight-4.closure.js") + "' ></script>");
            cs.RegisterClientScriptBlock(cstype, "modMaterialize", "<script async src = '" + ResolveUrl("~/js/modMaterialize.js") + "' ></script>");
            cs.RegisterClientScriptBlock(cstype, "jquery-sortable", "<script async src = '" + ResolveUrl("~/js/jquery-sortable.js") + " '></script>");
            cs.RegisterClientScriptBlock(cstype, "jquery.nestable", "<script async src = '" + ResolveUrl("~/js/jquery.nestable.js") + "' ></script>");
            cs.RegisterClientScriptBlock(cstype, "bxsliderJS", "<script async src = '" + ResolveUrl("~/js/jquery.bxslider.min.js") + "' ></script>");
           */
    }
}
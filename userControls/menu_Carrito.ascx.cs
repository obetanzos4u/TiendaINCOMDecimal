using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class menuCarrito : System.Web.UI.UserControl
{


    protected void Page_PreRender(object sender, EventArgs e)
    {
        /*
        if (!IsPostBack) {
           cargarMenu(sender, e);
            if (HttpContext.Current.User.Identity.IsAuthenticated) {
                carritoDesglose(sender, e);
            }
        }
        */
    }
    protected void cargarMenu(object sender, EventArgs e)
    {
         
        string cantidadCarrito = "";
        if (HttpContext.Current.User.Identity.IsAuthenticated) {
         
            usuarios usuario  = privacidadAsesores.modoAsesor();

            cantidadCarrito = " ("+carrito.obtenerCantidadProductos(usuario.email).ToString()+ ") ";
            }

        btn_toggle_desgloseCarrito.Text = "Carrito " + cantidadCarrito;

        upTotalProductos.Update();
    }


    protected void carritoDesglose(object sender, EventArgs e) {
        desgloseCarrito.InnerHtml = "";
        if(HttpContext.Current.User.Identity.IsAuthenticated){ 
        HtmlGenericControl ListadoProductos = new HtmlGenericControl("ul");

        // Obtener información del carrito
        carrito obtener = new carrito();
        System.Globalization.NumberFormatInfo myNumberFormatInfo = new System.Globalization.CultureInfo("es-MX", true).NumberFormat;
        usuarios usuario = usuarios.modoAsesor();
        DataTable productosCarritos = obtener.obtenerCarritoUsuario(usuario.email);

        string monedaTienda = ddl_moneda.SelectedValue; //Session["monedaTienda"].ToString();

        preciosTienda procesar = new preciosTienda();
        procesar.monedaTienda = monedaTienda;

        string tabla = @"<table>
        <tbody >
           <tr>
             <th>Numero Parte</th>
             <th style='text-align: right;' >Cantidad</th>
             <th style='text-align: right;' >Total</th>
           </tr>";
        string fila = "";
        foreach (DataRow r in productosCarritos.Rows) {

            string numero_parte = r["numero_parte"].ToString();
            decimal cantidad = Math.Round(decimal.Parse(r["cantidad"].ToString()),1);
            string unidad = r["unidad"].ToString();
            decimal tipo_cambio = decimal.Parse(r["tipo_cambio"].ToString());
            decimal precio_total = decimal.Parse(r["precio_total"].ToString());

            precio_total = procesar.precio_a_MonedaTienda(tipo_cambio, r["moneda"].ToString(), precio_total);
            string str_precio_total = decimal.Parse(precio_total.ToString()).ToString("#,#.##", myNumberFormatInfo);

            fila += " <tr><td>"+ numero_parte + "</td><td style='text-align: right;' >" + cantidad  + "</td><td style='text-align: right;' > $" + str_precio_total + "</td></tr>";
        }
         
        tabla += fila + "<tbody></table>";

      

        string subtotal = "";

        if (monedaTienda == "USD") {
            subtotal = obtener.obtenerTotalUSD(usuario.email);
        } else if (monedaTienda == "MXN") {
            subtotal = obtener.obtenerTotalMXN(usuario.email);

        }
        if (subtotal == "" || string.IsNullOrEmpty(subtotal)) subtotal = "0";
      

        decimal impuestos = decimal.Parse(subtotal) * (decimal.Parse(Session["impuesto"].ToString()) - 1);
        decimal total = decimal.Parse(subtotal) * decimal.Parse(Session["impuesto"].ToString());

        string str_impuestos =decimal.Parse(impuestos.ToString()).ToString("C2", myNumberFormatInfo) + " " + monedaTienda;
        string str_total = decimal.Parse(total.ToString()).ToString("C2", myNumberFormatInfo) + " " + monedaTienda;

      
        subtotal = decimal.Parse(subtotal).ToString("C2", myNumberFormatInfo) + " " + monedaTienda;

        

        lbl_subtotal.Text = subtotal;
        lbl_impuestos.Text = str_impuestos;
        lbl_total.Text = str_total;

        desgloseCarrito.InnerHtml += tabla;

        }
        else
        {
            desgloseCarrito.InnerHtml += "<h3 class='center'><strong>Inicia sesión para ver tu carrito</strong></h3>";

        }
        up_desgloseCarrito.Update();
    }
 
    protected void Page_Init(object sender, EventArgs e) {


        if (!IsPostBack) {
            if (Session["monedaTienda"] != null) {
                ddl_moneda.SelectedValue = Session["monedaTienda"].ToString();
            } else {
                Session["monedaTienda"] = ddl_moneda.SelectedValue;
            }
        }
    }


    protected void ddl_moneda_SelectedIndexChanged(object sender, EventArgs e) {


        string moneda = ddl_moneda.SelectedValue;
        Session["monedaTienda"] = moneda;
     
      
        carritoDesglose(sender,e);

    }
}
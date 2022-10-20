using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/* Este botón se muestra en el archivo [productosTienda.ascx.cs] en el listado de productos de la tienda y por default se agrega uno */


public partial class uc_btn_agregar_carritoListado : System.Web.UI.UserControl
{
    public string numero_parte
    {
        get
        {
            return this.hf_numero_parte.Value;
        }
        set
        {
            this.hf_numero_parte.Value = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!HttpContext.Current.User.Identity.IsAuthenticated)
        {
            btn_agregar_productoCarrito.Visible = false;
            agregar_productoCarrito_logoOut.Visible = true;
            if (!String.IsNullOrEmpty(Request.QueryString["busqueda"]))
            {
                agregar_productoCarrito_logoOut.Attributes.Add("href", "~/iniciar-sesion.aspx?ReturnUrl=/productos/buscar?busqueda=" + Request.QueryString["busqueda"]);
            }
            else
            {
                agregar_productoCarrito_logoOut.Attributes.Add("href", "~/iniciar-sesion.aspx?ReturnUrl=" + HttpContext.Current.Request.Url.AbsolutePath);
            }
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        numero_parte = this.numero_parte;

    }
    protected async void btn_agregar_productoCarrito_Click(object sender, EventArgs e)
    {
        string monedaTienda = HttpContext.Current.Session["monedaTienda"].ToString();
        if (numero_parte.Contains(","))
        {
            string[] numeros_parte = numero_parte.Split(',');
            numero_parte = numeros_parte[0];
        }
        operacionesProductos agregar = new operacionesProductos("carrito", null, null, numero_parte, txt_cantidadCarrito.Text, monedaTienda);
        await agregar.agregarProductoAsync();
        btn_agregar_productoCarrito.Attributes.Add("numero_parte", numero_parte);
        bool resultado = agregar.resultado_operacion;
        if (resultado)
        {
            NotiflixJS.Message(this.Page, NotiflixJS.MessageType.success, "Producto agregado");
            //materializeCSS.crear_toast(this.Page, agregar.mensaje_ResultadoOperacion, resultado);
        }
        else
        {
            NotiflixJS.Message(this.Page, NotiflixJS.MessageType.failure, "Error");
        }
        //   System.Web.UI.ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "upContadorCarrito", " $(\"#btnTotalProductosCarrito\").click();", true);
        UP_cantidadCarrito.Update();
    }
}

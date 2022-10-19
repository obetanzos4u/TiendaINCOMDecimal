using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class uc_btn_agregar_carrito : System.Web.UI.UserControl
{
    string numero_parte { get; set; }
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                btn_agregar_productoCarrito.Visible = false;
                agregar_productoCarrito_logoOut.Visible = true;
                if (HttpContext.Current.Request.Url.AbsolutePath == "/productos/buscar")
                {
                    agregar_productoCarrito_logoOut.Attributes.Add("href", "~/iniciar-sesion.aspx?ReturnUrl=/productos");
                }
                else
                {
                    agregar_productoCarrito_logoOut.Attributes.Add("href", "~/iniciar-sesion.aspx?ReturnUrl=" + HttpContext.Current.Request.Url.AbsolutePath);
                }
            }
            else
            {
                Literal lt_numero_parte = Parent.FindControl("lt_numero_parte") as Literal;
                string numero_parte = lt_numero_parte.Text;
                btn_agregar_productoCarrito.Attributes.Add("numero_parte", numero_parte);
            }
        }
    }

    protected async void btn_agregar_productoCarrito_Click(object sender, EventArgs e)
    {
        if (validadTXT())
        {
            Literal lt_numero_parte = Parent.FindControl("lt_numero_parte") as Literal;
            string numero_parte = lt_numero_parte.Text;
            string cantidad = txt_cantidadCarrito.Text;

            decimal tipoCambio = operacionesConfiguraciones.obtenerTipoDeCambio();
            string monedaTienda = HttpContext.Current.Session["monedaTienda"].ToString();

            operacionesProductos agregar = new operacionesProductos("carrito", null, null, numero_parte, cantidad, monedaTienda);
            await agregar.agregarProductoAsync();

            bool resultado = agregar.resultado_operacion;

            NotiflixJS.Message(UP_cantidadCarrito, NotiflixJS.MessageType.success, "Producto agregado al carrito");
            //materializeCSS.crear_toast(UP_cantidadCarrito, agregar.mensaje_ResultadoOperacion, resultado);



            if (usuarios.userLogin().tipo_de_usuario == "cliente")
            {
                // Inicio - Métricas historial carrito
                HttpContext ctx = HttpContext.Current;

                await Task.Run(() =>
                {
                    HttpContext.Current = ctx;

                    historialCarritos producto = new historialCarritos();

                    producto.usuario = usuarios.modoAsesor().email;
                    producto.agregado_por = usuarios.userLogin().email;
                    producto.fecha_creacion = utilidad_fechas.obtenerCentral();
                    producto.numero_parte = numero_parte;
                    producto.moneda = monedaTienda;
                    producto.tipo_cambio = tipoCambio;
                    producto.unidad = agregar.obtenerProducto().unidad;
                    producto.precio_unitario = agregar.obtenerPrecioUnitario();
                    producto.cantidad = agregar.obtenerCantidad();
                    producto.precio_total = decimal.Parse((Math.Round(agregar.obtenerPrecioUnitario(), 2) * agregar.obtenerCantidad()).ToString());
                    // producto.stock1 = usuarios.modoAsesor().email;
                    // producto.stock1_fecha = usuarios.modoAsesor().email;



                }).ConfigureAwait(false);
                // Fin - Métricas historial carrito
            }

        }
    }

    protected bool validadTXT()
    {


        if (txt_cantidadCarrito.Text != "" && txt_cantidadCarrito.Text != null)
        {
            double cantidad = 1;
            try
            {
                cantidad = textTools.soloNumeros(txt_cantidadCarrito.Text);
            }
            catch (Exception ex)
            {
                NotiflixJS.Message(Parent.Parent.Page, NotiflixJS.MessageType.warning, "Debe ingresar valores numéricos");
                //materializeCSS.crear_toast(Parent.Parent.Page, "Debe ingresar valores númericos", false);
                txt_cantidadCarrito.Text = "1";
                return false;
            }


            if (cantidad < 1)
            {
                txt_cantidadCarrito.Text = "1";
                NotiflixJS.Message(Parent.Parent.Page, NotiflixJS.MessageType.warning, "Debe ingresar valores mayores a 1");
                //materializeCSS.crear_toast(Parent.Parent.Page, "Debe ingresar valores mayores a 1", false);
                return false;
            }

            return true;
        }

        txt_cantidadCarrito.Text = "1";
        NotiflixJS.Message(Parent.Parent.Page, NotiflixJS.MessageType.warning, "Debe ingresar un valor");
        //materializeCSS.crear_toast(Parent.Parent.Page, "Debe ingresar un valor", false);

        return false;
    }
}
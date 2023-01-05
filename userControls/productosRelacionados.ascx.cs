using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uc_productosRelacionados : System.Web.UI.UserControl
{
    public string productos { get; set; }
    public UserControl uc_moneda { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {


    }
    public void obtenerProductos()
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrWhiteSpace(productos))
            {
                string[] productosArray = textTools.lineSimple(productos).Split(',');
                DataTable dt_productos = null;
                foreach (string producto in productosArray)
                {
                    DataTable dt_producto = null;
                    productosTienda obtener = new productosTienda();

                    dt_producto = obtener.obtenerProducto(producto);
                    if (dt_productos != null)
                        dt_productos.Merge(dt_producto);
                    else dt_productos = dt_producto;
                }
                // Parent = master > Parent Page

                string monedaTienda = (uc_moneda.FindControl("ddl_moneda") as DropDownList).SelectedValue;

                preciosTienda procesar = new preciosTienda();
                procesar.monedaTienda = monedaTienda;

                dt_productos = procesar.procesarProductos(dt_productos);
                lv_productosRelacionados.DataSource = dt_productos;
                lv_productosRelacionados.DataBind();
            }
            else
            {
                relacionadosSeccion.Visible = false;
            }
        }
    }

    protected void lv_productosRelacionados_OnItemDataBound(object sender, ListViewItemEventArgs e)
    {
        System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;

        Image img_producto = (Image)e.Item.FindControl("img_producto");
        HyperLink link_producto = (HyperLink)e.Item.FindControl("link_producto");

        string imagenes = rowView["imagenes"].ToString();
        string titulo = rowView["titulo"].ToString();
        string descripcion_corta = rowView["descripcion_corta"].ToString();
        string marca = rowView["marca"].ToString();
        string numero_parte = rowView["numero_parte"].ToString();

        link_producto.NavigateUrl = Request.Url.GetLeftPart(UriPartial.Authority) + GetRouteUrl("productos", new System.Web.Routing.RouteValueDictionary {
                        { "numero_parte", textTools.limpiarURL_NumeroParte(numero_parte) },
                         { "marca", marca },
                        { "productoNombre",  textTools.limpiarURL(titulo) }
                    });

        //if (imagenes.Contains(","))
        //{
        //}
        //else
        //{
        //    string[] arr_imagenes = imagenes.Replace(" ", "").Split(',');

        //    img_producto.ImageUrl = archivosManejador.imagenProducto(imagenes);
        //    img_producto.AlternateText = titulo;
        //    img_producto.ToolTip = descripcion_corta;
        //}
        string[] arr_imagenes = imagenes.Replace(" ", "").Split(',');

        img_producto.ImageUrl = archivosManejador.imagenProducto(arr_imagenes[0]);
        img_producto.AlternateText = titulo;
        img_producto.ToolTip = descripcion_corta;
    }
}
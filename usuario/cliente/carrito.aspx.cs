using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usuario_cliente_basic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarCarrto();
        }
    }

    protected void cargarCarrto(){

        usuarios userCarrito = usuarios.modoAsesor();


        lv_carrito.DataSource = carritoEF.obtenerCarritoFull(userCarrito.email);
        lv_carrito.DataBind();
}




    protected void lv_carrito_ItemDataBound(object sender, ListViewItemEventArgs e)
    {


        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            dynamic ProductoEnCarrito = e.Item.DataItem as dynamic;

            

              ProductoEnCarrito =  (dynamic)e.Item.DataItem as dynamic;


            string imagenes = ProductoEnCarrito.GetType().GetProperty("imagenes").GetValue(ProductoEnCarrito).ToString();

            Image img_carrito_producto = (Image)e.Item.FindControl("img_carrito_producto");

            if(imagenes.Contains(",")) img_carrito_producto.ImageUrl = archivosManejador.imagenProducto(imagenes.Split(',')[0]);
            else img_carrito_producto.ImageUrl = archivosManejador.imagenProducto(imagenes);





        }
    }
}
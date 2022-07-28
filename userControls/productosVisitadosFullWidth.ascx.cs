using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uc_productosVisitadosFullWidth : System.Web.UI.UserControl
{
    public int cantidadCargar { get; set; }
    public int? slidesToShow { get; set; }
    public bool? verticalMode { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }


    public void obtenerProductos() {

        if (usuarios.userLogin().tipo_de_usuario == "cliente")
        {
            titulo.Visible = true;
            DataTable dt = BI_historialProductos.obtenerProductosMásVisitados(usuarios.userLogin().id, cantidadCargar, DateTime.Now.AddDays(-90), utilidad_fechas.obtenerCentral());


            if (dt != null)
            {
                lv_productosMasVistos.DataSource = dt;
                lv_productosMasVistos.DataBind();



            }
        }
        




      


    }

    protected void lv_productosMasVistos_OnItemDataBound(object sender, ListViewItemEventArgs e)
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

        if (imagenes.Contains(","))
        {
            string[] arr_imagenes = imagenes.Replace(" ", "").Split(',');


            img_producto.ImageUrl = archivosManejador.imagenProducto(arr_imagenes[0]);
            img_producto.AlternateText = titulo;
            img_producto.ToolTip = descripcion_corta;
        }
        else
        {
            string[] arr_imagenes = imagenes.Replace(" ", "").Split(',');


            img_producto.ImageUrl = archivosManejador.imagenProducto(imagenes);
            img_producto.AlternateText = titulo;
            img_producto.ToolTip = descripcion_corta;
        }


    }
}
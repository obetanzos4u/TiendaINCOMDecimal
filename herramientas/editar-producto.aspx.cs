using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class herramientas_editar_producto : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

        txt_especificaciones.ValidateRequestMode = ValidateRequestMode.Disabled;
        txt_atributos.ValidateRequestMode = ValidateRequestMode.Disabled;

            if (!IsPostBack) {
            
            }

    }


    protected void actualizar_campo_TextChanged(object sender, EventArgs e) {

        TextBox txt_campo = (sender) as TextBox;
        string campo = txt_campo.ID.Replace("txt_", "");
        string valor_campo = textTools.lineSimple((txt_campo).Text);
        bool resultado = productosTienda.actualizarCampoProducto(txt_numero_parte.Text, campo, valor_campo);

        if (resultado) {
            materializeCSS.crear_toast(up_productos_Datos, "Campo actualizado con éxito", true);
        } else {
            materializeCSS.crear_toast(up_productos_Datos, "Error al actualizar campo", false);
        }


    }

    protected void actualizar_precio_rangos_TextChanged(object sender, EventArgs e) {

        TextBox txt_campo = (sender) as TextBox;
        string campo = txt_campo.ID.Replace("txt_", "");
        string valor_campo = textTools.lineSimple((txt_campo).Text);
        bool resultado = productosTienda.actualizarCampoProductoPrecioRango(txt_numero_parte.Text, campo, valor_campo);

        if (resultado) {
            materializeCSS.crear_toast(up_productos_precios_rangos, "Rango de precio actualizado con éxito", true);
        } else {
            materializeCSS.crear_toast(up_productos_precios_rangos, "Error al actualizar Rango de precio", false);
        }


    }
    protected void txt_search_product_TextChanged(object sender, EventArgs e) {

        productosTienda obtener = new productosTienda();
        string txt_campo = textTools.lineSimple(((sender) as TextBox).Text);
        DataTable dtProducto = obtener.obtenerProducto(txt_campo);

        if(dtProducto.Rows.Count >= 1) { 
        txt_numero_parte.Text = dtProducto.Rows[0]["numero_parte"].ToString();
        txt_titulo.Text = dtProducto.Rows[0]["titulo"].ToString();
        txt_descripcion_corta.Text = dtProducto.Rows[0]["descripcion_corta"].ToString();
        txt_titulo_corto_ingles.Text = dtProducto.Rows[0]["titulo_corto_ingles"].ToString();
        txt_especificaciones.Text = dtProducto.Rows[0]["especificaciones"].ToString();
        txt_marca.Text = dtProducto.Rows[0]["marca"].ToString();
        txt_categoria_identificador.Text = dtProducto.Rows[0]["categoria_identificador"].ToString();
        txt_imagenes.Text = dtProducto.Rows[0]["imagenes"].ToString();
        txt_metatags.Text = dtProducto.Rows[0]["metatags"].ToString();
        txt_peso.Text = dtProducto.Rows[0]["peso"].ToString();
        txt_alto.Text = dtProducto.Rows[0]["alto"].ToString();
        txt_ancho.Text = dtProducto.Rows[0]["ancho"].ToString();
        txt_profundidad.Text = dtProducto.Rows[0]["profundidad"].ToString();
        txt_pdf.Text = dtProducto.Rows[0]["pdf"].ToString();
        txt_video.Text = dtProducto.Rows[0]["video"].ToString();
        txt_unidad_venta.Text = dtProducto.Rows[0]["unidad_venta"].ToString();
        txt_cantidad.Text = dtProducto.Rows[0]["cantidad"].ToString();
        txt_unidad.Text = dtProducto.Rows[0]["unidad"].ToString();
        txt_producto_alternativo.Text = dtProducto.Rows[0]["producto_alternativo"].ToString();
        txt_productos_relacionados.Text = dtProducto.Rows[0]["productos_relacionados"].ToString();
        txt_atributos.Text = dtProducto.Rows[0]["atributos"].ToString();
        txt_noParte_proveedor.Text = dtProducto.Rows[0]["noParte_proveedor"].ToString();
        txt_noParte_interno.Text = dtProducto.Rows[0]["noParte_interno"].ToString();
        txt_upc.Text = dtProducto.Rows[0]["upc"].ToString();
        txt_noParte_Competidor.Text = dtProducto.Rows[0]["noParte_Competidor"].ToString();
        txt_orden.Text = dtProducto.Rows[0]["orden"].ToString();
        txt_etiquetas.Text = dtProducto.Rows[0]["etiquetas"].ToString();
        txt_disponibleVenta.Text = dtProducto.Rows[0]["disponibleVenta"].ToString();
            txt_Avisos.Text = dtProducto.Rows[0]["avisos"].ToString();

            txt_moneda_rangos.Text = dtProducto.Rows[0]["moneda_rangos"].ToString();

        txt_precio1.Text = dtProducto.Rows[0]["precio1"].ToString();
        txt_max1.Text = dtProducto.Rows[0]["max1"].ToString();

        txt_precio2.Text = dtProducto.Rows[0]["precio2"].ToString();
        txt_max2.Text = dtProducto.Rows[0]["max2"].ToString();

        txt_precio3.Text = dtProducto.Rows[0]["precio3"].ToString();
        txt_max3.Text = dtProducto.Rows[0]["max3"].ToString();

        txt_precio4.Text = dtProducto.Rows[0]["precio4"].ToString();
        txt_max4.Text = dtProducto.Rows[0]["max4"].ToString();

        txt_precio5.Text = dtProducto.Rows[0]["precio5"].ToString();
        txt_max5.Text = dtProducto.Rows[0]["max5"].ToString();

        } else {
            foreach(Control c in up_productos_Datos.Controls[0].Controls) {

                Type type = c.GetType();
                if (c.GetType().Name == "TextBox") {
                    (c as TextBox).Text = "";
                }
            }
            materializeCSS.crear_toast(up_productos_Datos, "No se encontró ningún producto con el término de búsqueda", false);
        }
        up_productos_Datos.Update();
        up_productos_precios_rangos.Update();
    }

    protected void btn_eliminarProducto_Click(object sender, EventArgs e)
    {
        string numero_parte = txt_numero_parte.Text;

        if (!string.IsNullOrWhiteSpace(numero_parte))
        {

            if (productosTienda.eliminar_producto(numero_parte))
                materializeCSS.crear_toast(up_productos_Datos, "Eliminado con éxito", true);
            else
                materializeCSS.crear_toast(up_productos_Datos, "Error al eliminar", false);

        }
        else
        {
            materializeCSS.crear_toast(up_productos_Datos, "No se ha especificado un número de parte", false);

        }

    }
}
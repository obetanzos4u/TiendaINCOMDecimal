using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace tienda {
    /* Visualiza en el producto si es precio fijo o si tiene rangos de acuerdo al cliente, si tiene rangos los muestra
        ya sea mediante una tabla o pop up
        */
    // Dependencia de actualmente establecido en [basic.master]:  @" $('.tooltippedPreciosEscalonados').tooltip({ delay: 50, html: true})";

    public partial class uc_precio_detalles : System.Web.UI.UserControl {
        public string numero_parte { get; set; }
        public string moneda { get; set; }
        public string size { get; set; }

        protected void Page_PreRender(object sender, EventArgs e) {

            //Validamos que el producto no sea solo para visualización
           if(!productosTienda.productoVisualización(numero_parte))
                hf_numero_parte.Value = numero_parte;
                hf_moneda.Value = moneda;
                obtenerPrecios();
                
            }

        protected void mostrarMin(string htmlTable) {


            tooltippedPreciosEscalonados.Attributes.Add("data-tooltip", htmlTable);
            tooltippedPreciosEscalonados.Visible = true;

            }
        protected void mostrarMax(string htmlTable) {

            tb_precios_escalonados.Visible = true;
            precios_aviso.Visible = true;
            tb_precios_escalonados.InnerHtml = htmlTable;
            }
         

 
        protected void obtenerPrecios() {

            string numero_parte = hf_numero_parte.Value;
            string moneda = hf_moneda.Value;
            string table = @" <table>
                                <thead>
                                    <tr>
                                        <th class='padding-slim'>Rango</th>
                                        <th class='padding-slim'>Precio</th>
                                    </tr>
                                </thead>
                                <tbody>";

            productosTienda obtenerProducto = new productosTienda();
            DataTable dt_producto = obtenerProducto.obtenerProductoPrecios(numero_parte);
          
            if (dt_producto != null && dt_producto.Rows.Count >= 1) {

                string str_solo_para_Visualizar = dt_producto.Rows[0]["solo_para_Visualizar"].ToString();
                bool solo_para_Visualizar = string.IsNullOrEmpty(str_solo_para_Visualizar) ? false : bool.Parse(str_solo_para_Visualizar);
                 
                    if(solo_para_Visualizar == true)  { 
                    return; 
                }

                string precio1 = dt_producto.Rows[0]["precio1"].ToString();
                 

                if (string.IsNullOrWhiteSpace(precio1) ){
                    return;
                }

                preciosTienda procesar = new preciosTienda();
                procesar.monedaTienda = moneda;
                // Solo si el producto esta disponible para venta, obtenemos sus precios, si no omitimos esta parte
                if (dt_producto.Rows[0]["disponibleVenta"].ToString() == "1") { 
                dt_producto = procesar.procesarProductos(dt_producto);

                model_productosTienda producto = obtenerProducto.dtProductoToListPrecio(dt_producto);

                if (producto.precio > 0) {

                    } else if (producto.precio2 > 0) {

                    NumberFormatInfo myNumberFormatInfo = new CultureInfo("es-MX", true).NumberFormat;

                    if ( producto.precio1 > 0) {
                        table += "<tr ><td class=\"padding-slim\" >" + Math.Round((producto.min1 == 0 ? 1 : producto.min1),1) + " - " + Math.Round(producto.max1,1) + "</td><td class=\"padding-slim\">" + float.Parse(producto.precio1.ToString()).ToString("C2", myNumberFormatInfo) + " " + moneda + "</td></tr>";
                        }
                    if ( producto.precio2 > 0) {
                        table += "<tr><td class=\"padding-slim\">" + Math.Round(producto.min2,1) + " - " + Math.Round(producto.max2, 1) + "</td><td class=\"padding-slim\">" + float.Parse(producto.precio2.ToString()).ToString("C2", myNumberFormatInfo) + " " + moneda + "</td></tr>";
                        }
                    if ( producto.precio3 > 0) {
                        table += "<tr><td class=\"padding-slim\">" + Math.Round(producto.min3, 1) + " - " + Math.Round(producto.max3, 1) + "</td><td class=\"padding-slim\">" + float.Parse(producto.precio3.ToString()).ToString("C2", myNumberFormatInfo) + " " + moneda + "</td></tr>";
                        }
                    if ( producto.precio4 > 0) {
                        table += "<tr><td class=\"padding-slim\">" + Math.Round(producto.min4, 1) + " - " + Math.Round(producto.max4,1) + "</td><td class=\"padding-slim\">" + float.Parse(producto.precio4.ToString()).ToString("C2", myNumberFormatInfo) + " " + moneda + "</td></tr>";
                        }
                    if ( producto.precio5 > 0) {
                        table += "<tr><td class=\"padding-slim\">" + Math.Round(producto.min5,1) + " - " + Math.Round(producto.max5,1) + "</td><td class=\"padding-slim\">" + float.Parse(producto.precio5.ToString()).ToString("C2", myNumberFormatInfo) + " " + moneda + "</td></tr>";
                        }
                    table += " </tbody> </table>";

                    if (size == "max") {
                        mostrarMax(table);
                        } else if (size == "min") {
                        mostrarMin(table);
                        }


                    }
                }
                }

            }

        }
}


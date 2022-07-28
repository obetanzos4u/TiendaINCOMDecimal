using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tienda {
    public partial class uc_editModalProductoPersonalizado : System.Web.UI.UserControl {


        protected void Page_Load(object sender, EventArgs e) {
           
            }

            public void cargarDatos(object sender, string idProducto, string numero_operacion) {
         

            hf_idProducto.Value = idProducto;
            hf_numero_operacion.Value = numero_operacion;

            DataTable dtMarcasPersonalizadas = new DataTable();
            dtMarcasPersonalizadas = productosPersonalizados.obtenerMarcas();

            ddl_marca.DataSource = dtMarcasPersonalizadas;
            ddl_marca.DataTextField = "marca_nombre";
            ddl_marca.DataValueField = "marca_nombre";
            ddl_marca.DataBind();


            DataTable dtUnidadesPersonalizadas = new DataTable();
            dtUnidadesPersonalizadas = productosPersonalizados.obtenerUnidades();

            ddl_unidad.DataSource = dtUnidadesPersonalizadas;
            ddl_unidad.DataTextField = "unidad_nombre";
            ddl_unidad.DataValueField = "unidad_nombre";
            ddl_unidad.DataBind();

            DataTable dtProducto = cotizacionesProductos.obtenerProducto(int.Parse(idProducto));
            txt_numero_parte.Text = dtProducto.Rows[0]["numero_parte"].ToString();
            txt_descripcion.Text = dtProducto.Rows[0]["descripcion"].ToString();
            txt_cantidad.Text = dtProducto.Rows[0]["cantidad"].ToString();
            ddl_marca.SelectedValue = dtProducto.Rows[0]["marca"].ToString();
            ddl_unidad.SelectedValue = dtProducto.Rows[0]["unidad"].ToString();
            ddl_tipo.SelectedValue = dtProducto.Rows[0]["tipo"].ToString();
            txt_precio.Text = dtProducto.Rows[0]["precio_unitario"].ToString();

            LinkButton btn = (LinkButton)sender;
            string script = @" $(document).ready(function(){ $('#modal_editar_producto_personalizado').modal('open');    });";
            ScriptManager.RegisterStartupScript(this.Page, typeof(LinkButton), "modal_editar_producto_personalizado", script, true);
            }

        protected void btn_modificar_producto_Click(object sender, EventArgs e) {

            model_cotizacionesProductos producto = new model_cotizacionesProductos();

            string idProducto = hf_idProducto.Value;
            string numero_operacion = hf_numero_operacion.Value;

            producto.id = int.Parse(idProducto);
            producto.numero_parte = textTools.lineSimple(txt_numero_parte.Text);
            producto.marca = ddl_marca.SelectedValue;
            producto.descripcion = textTools.lineSimple(txt_descripcion.Text);
            producto.cantidad = textTools.soloNumerosD(txt_cantidad.Text);
            producto.unidad = ddl_unidad.SelectedValue;
            producto.usuario = HttpContext.Current.User.Identity.Name;
            producto.tipo = int.Parse(ddl_tipo.SelectedValue);
            producto.activo = 1;
            producto.precio_unitario = textTools.soloNumerosD(txt_precio.Text);
            producto.precio_total = producto.cantidad * producto.precio_unitario;
            producto.fecha_creacion = utilidad_fechas.obtenerCentral();
            producto.orden = 99;
            validar_camposCotizacionProductos validar = new validar_camposCotizacionProductos();
            validar.producto(producto);


            if (validar.resultado == true) {
                this.Page.GetType().InvokeMember("cargarProductos", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, new object[] { });
                if (cotizacionesProductos.actualizarCantidadyDatosProductoStatic(numero_operacion, producto) == true &&     cotizacionesProductos.actualizarTotalStatic(numero_operacion) == true) {
                    materializeCSS.crear_toast(up_btn_edit_producto_personalizado, "Producto editado con éxito", true);
                    } else {
                    materializeCSS.crear_toast(up_btn_edit_producto_personalizado, "Error al editado producto", true);

                    }
                } else {
                materializeCSS.crear_toast(up_btn_edit_producto_personalizado, validar.mensaje, validar.resultado);
                }
            this.Page.GetType().InvokeMember("cargasDatosOperacion", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
            this.Page.GetType().InvokeMember("cargarProductos", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
            this.Page.GetType().InvokeMember("refreshPage", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);

            }
        }
    }
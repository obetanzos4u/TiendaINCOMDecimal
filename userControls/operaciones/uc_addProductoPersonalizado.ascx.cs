using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class uc_addProductoPersonalizado : System.Web.UI.UserControl {

    public string idSQL {
        get { return this.hf_idSQL.Value;}
        set { this.hf_idSQL.Value = value; }
        }
    public string tipo_operacion {
        get { return this.hf_tipo_operacion.Value; }
        set { this.hf_tipo_operacion.Value = value; }
        }
    public string numero_operacion {
        get { return this.hf_numero_operacion.Value; }
        set { this.hf_numero_operacion.Value = value; }
        }
    protected void Page_Load(object sender, EventArgs e) {
        if (!IsPostBack) {

            cargarDatos();
            } else {

            }
        }
 
    protected void cargarDatos() {

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


        }

    protected void btn_agregar_producto_Click(object sender, EventArgs e) {

        bool existenciaProducto = cotizacionesProductos.verificarExistenciaProducto(hf_numero_operacion.Value, textTools.lineSimple(txt_numero_parte.Text));

        if(existenciaProducto == false) {

            
        model_cotizacionesProductos producto = new model_cotizacionesProductos();


        string tipo_operacion = hf_tipo_operacion.Value;
        string numero_operacion = hf_numero_operacion.Value;
        

        producto.numero_parte = textTools.lineSimple(txt_numero_parte.Text);
        producto.marca = ddl_marca.SelectedValue;
        producto.descripcion = textTools.lineSimple(txt_descripcion.Text);
        producto.cantidad = textTools.soloNumerosD(txt_cantidad.Text);
        producto.unidad = ddl_unidad.SelectedValue;
        producto.usuario = HttpContext.Current.User.Identity.Name;
        producto.tipo = int.Parse(ddl_tipo.SelectedValue);
        producto.activo = 1;
        producto.precio_unitario = textTools.soloNumerosD(txt_precio.Text);
        producto.precio_total = Math.Round(decimal.Parse((Math.Round(producto.precio_unitario, 2) * producto.cantidad).ToString()), 2);
        producto.fecha_creacion = utilidad_fechas.obtenerCentral();
        producto.orden = 99;
        validar_camposCotizacionProductos validar = new validar_camposCotizacionProductos();
        validar.producto(producto);


        if(validar.resultado == true) {


            if (cotizacionesProductos.agregarProducto(numero_operacion, producto) == true && cotizacionesProductos.actualizarTotalStatic(numero_operacion) == true) {
                materializeCSS.crear_toast(up_btn_agregar_producto, "Producto agregado con éxito",true);
                } else {
                materializeCSS.crear_toast(up_btn_agregar_producto, "Error al agregar producto", true);

                }
            }
        else {
            materializeCSS.crear_toast(up_btn_agregar_producto, validar.mensaje, validar.resultado);
            }
            this.Page.GetType().InvokeMember("cargasDatosOperacion", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
            this.Page.GetType().InvokeMember("cargarProductos", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
            this.Page.GetType().InvokeMember("refreshPage", System.Reflection.BindingFlags.InvokeMethod, null, this.Page, null);
            ScriptManager.RegisterStartupScript(up_btn_agregar_producto, typeof(Control), "modal_agregar_producto_personalizado", " $('#modal_agregar_producto_personalizado').modal('close');", true);
            } else {
            materializeCSS.crear_toast(up_btn_agregar_producto, "El producto ya existe en tu operacion", false);
            }
        }
       
    }
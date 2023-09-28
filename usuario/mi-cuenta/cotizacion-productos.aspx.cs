using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class usuario_cotizacionDatos : System.Web.UI.Page
{
    NumberFormatInfo myNumberFormatInfo = new CultureInfo("es-MX", true).NumberFormat;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            cargasDatosOperacion();
            cargarProductos();
        
                usuarios usuario = (usuarios)HttpContext.Current.Session["datosUsuario"];
                if (usuario.tipo_de_usuario == "usuario") addProdPer.Visible = true;
                 
            } else {
            string __EVENTTARGET = Request["__EVENTTARGET"];
            string __EVENTARGUMENT =  Request["__EVENTARGUMENT"];
            if (__EVENTTARGET == btn_async.ClientID) {
                actualizarOrden(__EVENTARGUMENT);
            }

        }
       
         
        }
    
    protected void actualizarOrden(string argumento) {

        string numero_parte = argumento.Split('|')[0];
        int posicion = int.Parse(argumento.Split('|')[1]);

      bool resultado =   cotizacionesProductos.actualizarOrdenProducto(lt_numero_operacion.Text, numero_parte, posicion);
        if (resultado) materializeCSS.crear_toast(upAsync, "Posición actualizada", true);
        else if (resultado) materializeCSS.crear_toast(upAsync, "Error al actualizar orden", false);

    }
    public void OpenEditProdPersonalizadoModal(object sender, string idProducto) {

        tienda.uc_editModalProductoPersonalizado modalEditProd = (tienda.uc_editModalProductoPersonalizado)editProdPersonalizadoModal;
        modalEditProd.cargarDatos(sender, idProducto,lt_numero_operacion.Text);
      
        }
    public void cargasDatosOperacion() {

        if (Page.RouteData.Values["id_operacion"] != null) {


            string route_id_operacion = Page.RouteData.Values["id_operacion"].ToString();

            route_id_operacion = seguridad.DesEncriptar(Page.RouteData.Values["id_operacion"].ToString());

            int id_operacion = int.Parse(route_id_operacion);

            string tipo_cotizacion = cotizaciones.obtener_tipo_cotizacion(id_operacion);

            if (string.IsNullOrWhiteSpace(tipo_cotizacion)) {

                // Desactivado a petición de TLMKT para mostrar en sección productos 20190430 $('.modal_tipo_cotizacion').modal('open'); 
                 ScriptManager.RegisterStartupScript(this, typeof(Page), "con", "$(document).ready(function () {       $('.modal_tipo_cotizacion').modal('open');    });", true);
            } else {
                ddl_tipo_cotizacion.SelectedValue = tipo_cotizacion;
            }

            cotizaciones obtener = new cotizaciones();

            DataTable dt_CotizacionDatos = obtener.obtenerCotizacionDatosMax(id_operacion);

            if (dt_CotizacionDatos != null && dt_CotizacionDatos.Rows.Count >= 1) {

                detallesOperacion.dt_operacion = dt_CotizacionDatos;

                    string usuario_cliente = dt_CotizacionDatos.Rows[0]["usuario_cliente"].ToString();
                    bool permisoVisualizar = privacidadAsesores.validarOperacion(usuario_cliente);

                if (permisoVisualizar) { } else {
                    Response.Write(usuario_cliente);
               //   Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority));
                    }

                string moneda = dt_CotizacionDatos.Rows[0]["monedaCotizacion"].ToString();
                string subtotal = dt_CotizacionDatos.Rows[0]["subtotal"].ToString();
                string impuestos = dt_CotizacionDatos.Rows[0]["impuestos"].ToString();
                string descuento = dt_CotizacionDatos.Rows[0]["descuento"].ToString();
                string descuento_porcentaje = dt_CotizacionDatos.Rows[0]["descuento_porcentaje"].ToString();

              
                decimal d_subtotal = Math.Round(decimal.Parse(subtotal),2);

                // Validamos que haya un descuento aplicado pues si este se encuentra, es necesario mostrarlo, de lo contrario no, pues ocupario mayor espacio en el desglose
                if (!string.IsNullOrWhiteSpace(descuento) || !string.IsNullOrWhiteSpace(descuento_porcentaje)) {
                    content_descuento.Visible = true;
                    decimal d_descuento = decimal.Parse(descuento);

                    lbl_subtotalSinDescuento.Text = Math.Round(d_subtotal + d_descuento,2).ToString("C2", myNumberFormatInfo) + " " + moneda;

                

                    lbl_descuento_porcentaje.Text = Math.Round(decimal.Parse(descuento_porcentaje), 1).ToString() ;
                    txt_descuento.Text = Math.Round(decimal.Parse(descuento_porcentaje), 1).ToString();
                    } else {

                    content_descuento.Visible = false;
                    }

                decimal total = decimal.Parse(dt_CotizacionDatos.Rows[0]["total"].ToString());

                 id_operacion = int.Parse(dt_CotizacionDatos.Rows[0]["id"].ToString());
                string numero_operacion = dt_CotizacionDatos.Rows[0]["numero_operacion"].ToString();
                int vigencia = int.Parse(dt_CotizacionDatos.Rows[0]["vigencia"].ToString());
                int conversionPedido = int.Parse(dt_CotizacionDatos.Rows[0]["conversionPedido"].ToString());

                Page.Title = "Productos de cotización #" + numero_operacion;


                string metodoEnvio = dt_CotizacionDatos.Rows[0]["metodoEnvio"].ToString();
                string envio = Math.Round(decimal.Parse(dt_CotizacionDatos.Rows[0]["envio"].ToString()), 2).ToString("C2", myNumberFormatInfo) + " " + moneda;

                lt_numero_operacion.Text = numero_operacion;
                lt_nombre_cotizacion.Text = dt_CotizacionDatos.Rows[0]["nombre_cotizacion"].ToString();
                lt_cliente_nombre.Text = dt_CotizacionDatos.Rows[0]["cliente_nombre"].ToString() + " " + dt_CotizacionDatos.Rows[0]["cliente_apellido_paterno"].ToString();

                hf_id_operacion.Value = dt_CotizacionDatos.Rows[0]["id"].ToString();
                lbl_moneda.Text = moneda;

                if (moneda == "USD") btn_ConvertirMoneda.Text = "Convertir a MXN";
                else btn_ConvertirMoneda.Text = "Convertir a USD";

                if (subtotal == "" || string.IsNullOrEmpty(subtotal)) subtotal = "0";
                lbl_subTotal.Text = d_subtotal.ToString("C2", myNumberFormatInfo) + " " + moneda;


                lbl_impuestos.Text = decimal.Parse(impuestos).ToString("C2", myNumberFormatInfo) + " " + moneda;
                lbl_total.Text = decimal.Parse(total.ToString()).ToString("C2", myNumberFormatInfo) + " " + moneda;

                hl_editarDatos.NavigateUrl = GetRouteUrl("usuario-cotizacion-datos", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",seguridad.Encriptar(id_operacion.ToString()) }
                    });
                hl_editarProductos.NavigateUrl = GetRouteUrl("usuario-cotizacion-productos", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",seguridad.Encriptar(id_operacion.ToString()) }
                    });

                btn_editarProductos.tipo_operacion = "cotizacion";
                btn_editarProductos.id_operacion = id_operacion.ToString();


                link_enviar.NavigateUrl = GetRouteUrl("usuario-cotizacion-visualizar", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion", seguridad.Encriptar(hf_id_operacion.Value) }, });

                DateTime fechaOperacion = DateTime.Parse(dt_CotizacionDatos.Rows[0]["fecha_creacion"].ToString());
                addProdPer.numero_operacion = numero_operacion;
                addProdPer.tipo_operacion = "cotizacion";
                addProdPer.idSQL = id_operacion.ToString();
                if (utilidad_fechas.calcularDiferenciaDias(fechaOperacion) >= vigencia || conversionPedido == 1) {
                    hf_operacionActiva.Value = "false";
                    deshabilitarCampos(content_lv_datos);
                    deshabilitarCampos(content_lv_productos);
                    link_enviar.Text = "Enviar por email";
                    }
                else
                {
                    hf_operacionActiva.Value = "true";
                    habilitarCampos(content_lv_datos);
                    habilitarCampos(content_lv_productos);
                    link_enviar.Text = "Enviar por email";

                }

                lbl_envio.Text = envio;
                lbl_metodo_envio.Text = metodoEnvio;

                chk_CalculoAutomáticoEnvio.Checked = cotizaciones.obtenerEstatusCalculo_Costo_Envio(numero_operacion);

                /// INICIO - Método de envío, descuentos, opciones que solo un asesor o rol usuario puede modificar

                usuarios usuarioLogin = (usuarios)System.Web.HttpContext.Current.Session["datosUsuario"];
                if (usuarioLogin.tipo_de_usuario == "usuario") {
                    content_descuento_asesor.Visible = true;
                    content_envioUsuario.Visible = true;
                    txt_envio.Text = envio;
                    ddl_metodo_envio.SelectedValue = metodoEnvio;
               
                    } else {
                 
                    content_envioUsuario.Visible = false;
                    content_descuento_asesor.Visible = false;

                    }



                switch (metodoEnvio)
                {
                    case "Gratuito":
                       
                        txt_envio.Enabled = false;
                        break;

                    case "En Tienda":
 
                        txt_envio.Enabled = false;
                        break;

                    case "Estándar":
                        if(chk_CalculoAutomáticoEnvio.Checked) txt_envio.Enabled = false;
                        else txt_envio.Enabled = true;
                        break;

                    case "Ninguno":
 
                        txt_envio.Enabled = false; break;


                }


                /// FIN - Método de envío
            } else {
                Server.Transfer("/usuario/cotizaciones/datos/", false);
                }
                
            }
        }
    protected void deshabilitarCampos(Control control) {

        foreach (Control c in control.Controls) {

            if (!c.HasControls()) {

                var type = c.GetType().Name;
                switch (type) {
                    case "TextBox":
                    ((TextBox)c).Enabled = false; break;
                    case "LinkButton":
                    ((LinkButton)c).Enabled = false;
                    ((LinkButton)c).CssClass = "btn disabled";
                    break;
                    case "DropDownList":
                    ((DropDownList)c).Enabled = false;
                    ((DropDownList)c).CssClass = "disabled";
                    break;
                    }

                } else {
                deshabilitarCampos(c);
                }


            }
        }

    protected void habilitarCampos(Control control)
    {

        foreach (Control c in control.Controls)
        {

            if (!c.HasControls())
            {

                var type = c.GetType().Name;
                switch (type)
                {
                    case "TextBox":
                        ((TextBox)c).Enabled = true; break;
                    case "LinkButton":
                        ((LinkButton)c).Enabled = true;
                        ((LinkButton)c).CssClass = "btn";
                        break;
                    case "DropDownList":
                        ((DropDownList)c).Enabled = true;
                        ((DropDownList)c).CssClass = "";
                        break;
                }

            }
            else
            {
                habilitarCampos(c);
            }


        }
    }
    public void cargarProductos() {
       

        DataTable productosCotizacion = cotizacionesProductos.obtenerProductosWithMedidas(lt_numero_operacion.Text);

        lv_productosCotizacion.DataSource = productosCotizacion;
        lv_productosCotizacion.DataBind();

        string numero_operacion = lt_numero_operacion.Text;

        int i = 0;
        foreach(DataRow r in productosCotizacion.Rows) {

            string numero_parte = r["numero_parte"].ToString();
            i = i + 1;
            cotizacionesProductos.actualizarOrdenProductoUnicamente(numero_operacion, numero_parte, i);
        }


        }



    protected void lv_productos_OnItemDataBound(object sender, ListViewItemEventArgs e) {


        System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;

        string monedaCotizacion = lbl_moneda.Text;
        string numero_parte = rowView["numero_parte"].ToString();

       
        string titulo = rowView["titulo"].ToString();
        string marca = rowView["marca"].ToString();

        string idProducto =  rowView["id"].ToString();

        decimal precio_unitario = decimal.Parse(rowView["precio_unitario"].ToString());
        decimal precio_total = decimal.Parse(rowView["precio_total"].ToString());
        decimal cantidad = Math.Round(decimal.Parse(rowView["cantidad"].ToString()), 1);
        var imagenes = rowView["imagenes"].ToString().Split(',');
        Label lbl_precio_unitario = (Label)e.Item.FindControl("lbl_precio_unitario");

        Label lbl_moneda_producto = (Label)e.Item.FindControl("lbl_moneda_producto");
        lbl_precio_unitario.Text = precio_unitario.ToString("C2", myNumberFormatInfo);
        lbl_moneda_producto.Text = monedaCotizacion;
        Label lbl_precio_total = (Label)e.Item.FindControl("lbl_precio_total");
        lbl_precio_total.Text = precio_total.ToString("C2", myNumberFormatInfo) + " " + monedaCotizacion;

        Image imgProducto = (Image)e.Item.FindControl("imgProducto");


        imgProducto.ImageUrl = archivosManejador.imagenProducto(rowView["imagenes"].ToString().Split(',')[0]);

        HyperLink link_producto = (HyperLink)e.Item.FindControl("link_producto");
        HyperLink link_imgProducto = (HyperLink)e.Item.FindControl("link_imgProducto");

        imgProducto.ImageUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/" +  archivosManejador.imagenProducto(imagenes[0]);
       

        link_producto.NavigateUrl = GetRouteUrl("productos", new System.Web.Routing.RouteValueDictionary {
                        { "numero_parte", textTools.limpiarURL_NumeroParte(numero_parte) },
                         { "marca", marca },
                        { "productoNombre",  textTools.limpiarURL(titulo) }
                    });
        link_imgProducto.NavigateUrl = link_producto.NavigateUrl;

        Label lt_activo = (Label)e.Item.FindControl("lbl_activo");
        LinkButton btn_eliminarProducto = (LinkButton)e.Item.FindControl("btn_eliminarProducto");

        Panel content_btn_productoAlternativo = (Panel)e.Item.FindControl("content_btn_productoAlternativo");
        CheckBox chk_alternativo = (CheckBox)e.Item.FindControl("chk_alternativo");
        usuarios usuarioLogin = usuarios.userLogin();

        TextBox txt_cantidadCotizacion = (TextBox)e.Item.FindControl("txt_cantidadCotizacion");
        TextBox txt_cantidadCotizacionPersonalizado = (TextBox)e.Item.FindControl("txt_cantidadCotizacionPersonalizado");

        HiddenField hf_tipoProducto = (HiddenField)e.Item.FindControl("hf_tipoProducto");

        if (usuarioLogin.tipo_de_usuario == "usuario") {
            content_btn_productoAlternativo.Visible = true;


            if (rowView["alternativo"].ToString() != "") {

           
            int? alternativo = int.Parse(rowView["alternativo"].ToString() );
            if (alternativo != null) {
                if (alternativo == 1) chk_alternativo.Checked = true;
                else if (alternativo == 0) chk_alternativo.Checked = false;
            }
            }
        }
        

        txt_cantidadCotizacion.Text = cantidad.ToString();
        txt_cantidadCotizacionPersonalizado.Text = cantidad.ToString();


        int activo = int.Parse(rowView["activo"].ToString());
        bool disponibleParaVenta = productosTienda.productoDisponibleVenta(numero_parte);

        if (activo == 0 || disponibleParaVenta == false) {
            lt_activo.Visible = true;
            lt_activo.Text = "[NO DISPONIBLE PARA VENTA]";
            }

       

        if (hf_operacionActiva.Value  == "false") {
            txt_cantidadCotizacion.Enabled = false;
            txt_cantidadCotizacionPersonalizado.Enabled = false;
            btn_eliminarProducto.Enabled = false;
            btn_eliminarProducto.CssClass = "btn disabled";
            }
        // Si el tipo de producto no es base [1] y es personalizado [2] mostramos los campos para editar dicho producto personalizado
        if (hf_tipoProducto.Value != "1") { 
            txt_cantidadCotizacion.Visible = false;
            txt_cantidadCotizacionPersonalizado.Visible = true;

            tienda.uc_editProductoPersonalizado productoPersonalizado = (tienda.uc_editProductoPersonalizado)e.Item.FindControl("editProdPer");
            productoPersonalizado.Visible = true;
            productoPersonalizado.idProducto = idProducto;
            productoPersonalizado.numero_operacion = lt_numero_operacion.Text;

            link_imgProducto.NavigateUrl = "";
            link_producto.NavigateUrl = "";
            link_producto.Visible = false;

            lt_activo.Visible = false;

            #region Imagen  producto personalizado
            // 20210504 CM - Imagenes para productos personalizados

            if (!string.IsNullOrWhiteSpace(imagenes[0]))
            {
                imgProducto.ImageUrl = archivosManejador.imagenProducto(imagenes[0]);
            }
            else
            {
                imgProducto.ImageUrl = archivosManejador.imagenProductoPersonalizado(numero_parte + ".jpg");
            }
            #endregion

        }
        else {
            txt_cantidadCotizacion.Visible = true;
            txt_cantidadCotizacionPersonalizado.Visible = false;
            tienda.uc_editProductoPersonalizado productoPersonalizado = (tienda.uc_editProductoPersonalizado)e.Item.FindControl("editProdPer");
            productoPersonalizado.Visible = false;

            //Si el producto esta disponible para venta, cargamos su listado de precios
            if (disponibleParaVenta) {
                // Si el tipo de producto es  base (tipo =1) procedemos a mostrar a obtener rango de precios si es que los tuviera. Los rangos de precios solo son para productos base.
                tienda.uc_precio_detalles detalles_precios = (tienda.uc_precio_detalles)e.Item.FindControl("detalles_precios");
                detalles_precios.numero_parte = numero_parte;
                detalles_precios.moneda = monedaCotizacion;
                detalles_precios.size = "min";
                }
        
            }
   

        
        




        }
    protected void txt_cantidadCotizacionPersonalizado_TextChanged(object sender, EventArgs e) {
        TextBox txt_cantidad = sender as TextBox;

        decimal cantidad = 1;

        if (txt_cantidad.Text != "" && txt_cantidad.Text != null) {
            try {
                cantidad = textTools.soloNumerosD(txt_cantidad.Text);

                // Obtenemos el contenedor del objeto que creo el evento
                ListViewItem lvItemCotizacion = (ListViewItem)txt_cantidad.NamingContainer;
                Literal lt_numeroParte = (Literal)lvItemCotizacion.FindControl("lt_numeroParte");
                HiddenField hf_tipoProducto = (HiddenField)lvItemCotizacion.FindControl("hf_tipoProducto");
                Label lbl_precio_unitario  = (Label)lvItemCotizacion.FindControl("lbl_precio_unitario");


                string numero_operacion = lt_numero_operacion.Text;
                decimal precio_unitario = decimal.Parse(lbl_precio_unitario.Text.Replace("$",""));
                model_cotizacionesProductos producto = new model_cotizacionesProductos();
                producto.tipo = int.Parse(hf_tipoProducto.Value);
                producto.cantidad = cantidad;
                producto.precio_unitario = precio_unitario;
                producto.precio_total = cantidad * precio_unitario;
                producto.numero_parte = lt_numeroParte.Text;


                cotizacionesProductos actualizarCantidad = new cotizacionesProductos();
             

                if (actualizarCantidad.actualizarCantidadProducto(numero_operacion, producto) == true) {
                    materializeCSS.crear_toast(this, "Producto Actualizado con éxito", true);
                    cotizacionesProductos.actualizarTotalStatic(numero_operacion);

                    if(ddl_metodo_envio.SelectedValue == "Estándar") CalcularEnvio();

                    cargasDatosOperacion();
                    cargarProductos();
                    up_operacion.Update();
                    } else {
                    materializeCSS.crear_toast(this, "Error al actualizar producto", true);
                    }
                }
            catch (Exception ex) {
                devNotificaciones.error("Actualizar total personalizado", ex);
                materializeCSS.crear_toast(this, "Debe ingresar valores númericos", false);
               
                return;
                }

            } else {
            materializeCSS.crear_toast(this, "Debe ingresar valores númericos", false);
            return;
            }

    
        

        }
        protected async  void txt_cantidadCotizacion_TextChanged(object sender, EventArgs e) {
        TextBox txt_cantidad = sender as TextBox;
        float cantidad = 1;

        if (txt_cantidad.Text != "" && txt_cantidad.Text != null) {
            try {
                cantidad = textTools.soloNumerosF(txt_cantidad.Text);
                }
            catch (Exception ex) {
                materializeCSS.crear_toast(Parent.Parent.Page, "Debe ingresar valores númericos", false);
                }

            } else {
            materializeCSS.crear_toast(this, "Debe ingresar valores númericos", false);
            }

        if (cantidad >= 1) {
            // Obtenemos el contenedor del objeto que creo el evento
            ListViewItem lvItemCotizacion = (ListViewItem)txt_cantidad.NamingContainer;

            // Buscamos un objeto dentro del contenedor
            HiddenField hf_idProductoCarrito = (HiddenField)lvItemCotizacion.FindControl("hf_idProductoCarrito");

            // 1 Base, 2 Personalizado, 3 Servicio
            HiddenField tipoProducto = (HiddenField)lvItemCotizacion.FindControl("hf_tipoProducto");

            bool modalidadAsesor = Boolean.Parse(Session["modoAsesor"].ToString());

            Literal lt_numeroParte = lvItemCotizacion.FindControl("lt_numeroParte") as Literal;
            


            // Iniciamos la obtención de precios

            string numero_operacion = lt_numero_operacion.Text;
            string numero_parte = lt_numeroParte.Text; 

            operacionesProductos add = new operacionesProductos("cotizacion", "update", numero_operacion, numero_parte, txt_cantidad.Text, lbl_moneda.Text);
            await add.agregarProductoAsync();
            materializeCSS.crear_toast(Page, add.mensaje_ResultadoOperacion, add.resultado_operacion);

            CalcularEnvio();

            cargasDatosOperacion();
            cargarProductos();
            up_operacion.Update();
            } else {
            materializeCSS.crear_toast(Page, "Debes ingresar una cantidad mayor o igual a 1", false);
            }

      
        }
    public void refreshPage() {
        up_operacion.Update();

        }


    protected void btn_eliminarProducto_Click(object sender, EventArgs e) {
        LinkButton btnEliminar = (LinkButton)sender;
        // Obtenemos el contenedor del objeto que creo el evento
        ListViewItem lvItem = (ListViewItem)btnEliminar.NamingContainer;

        HiddenField hf_idProducto = (HiddenField)lvItem.FindControl("hf_idProducto");

        string idProducto = hf_idProducto.Value;
        cotizacionesProductos eliminar = new cotizacionesProductos();

        try {
            eliminar.eliminarProducto(idProducto);
            eliminar.actualizarTotal(lt_numero_operacion.Text);
            materializeCSS.crear_toast(this, "Producto eliminado con éxito", true);
            CalcularEnvio();
            cargasDatosOperacion();
            cargarProductos();
            up_operacion.Update();
            }
        catch (Exception ex) {
            devNotificaciones.error("Error al eliminar producto de carrito", ex);
            materializeCSS.crear_toast(this, "Error al eliminar producto", false);
            }

       
        }

    protected async void btn_converPedido_Click(object sender, EventArgs e) {

        pedidosDatos comprar = new pedidosDatos();
        int idSQLcotización = int.Parse(hf_id_operacion.Value);
        var resultado =  comprar.crearPedidoDeCotizacionAsync(usuarios.modoAsesor(), idSQLcotización, lt_nombre_cotizacion.Text);

       
        DataTable dtPedido = comprar.obtenerPedidoDatos(resultado);
        if(resultado != null && cotizaciones.cotizacionUpdateToPedido(lt_numero_operacion.Text, resultado) == true) {
            btn_converPedido_disabled();
            content_pedido_creado.Visible = true;
            content_productos.Visible = false;
            materializeCSS.crear_toast(this, "Pedido creado con éxito", true);

            string script = @"   setTimeout(function () { window.location.replace('" + HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/usuario/mi-cuenta/pedidos/visualizar/" + seguridad.Encriptar(dtPedido.Rows[0]["id"].ToString()) + "')}, 3500);";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "redirección", script, true);

            } else {

            materializeCSS.crear_toast(this, "Error al generar pedidos", false);
            }
        }
    protected void btn_converPedido_disabled() {
        btn_converPedido.Enabled = false;
        btn_converPedido.CssClass = "btn_convertir_pedido disabled";
        }

    protected void ddl_metodo_envio_SelectedIndexChanged(object sender, EventArgs e) {
        string metodoEnvio = ddl_metodo_envio.SelectedValue;


        switch (metodoEnvio)
        {
            case "Gratuito":
                txt_envio.Text = "0";
                txt_envio.Enabled = false;
                break;

            case "En Tienda":
                txt_envio.Text = "0";
                txt_envio.Enabled = false;
                break;

            case "Estándar":
                txt_envio.Text = "0";
                txt_envio.Enabled = true; break;

            case "Ninguno":
                txt_envio.Text = "0";
                txt_envio.Enabled = false; break;


        }

    }

    protected void btn_guardarMetodoEnvio_Click(object sender, EventArgs e) {


        string metodoEnvio = ddl_metodo_envio.SelectedValue;
        decimal envio = textTools.soloNumerosD(txt_envio.Text);

        if (envio <= 0 && metodoEnvio == "Estándar" && chk_CalculoAutomáticoEnvio.Checked == false)
        {
            materializeCSS.crear_toast(this, "El envío estándar no puede ser de 0", false);
            materializeCSS.crear_toast(this, "Selecciona envío gratuito.", false);
            return;
        }

        if (envio > 0 && metodoEnvio == "Estándar" && chk_CalculoAutomáticoEnvio.Checked == false) {

           
                string resultado = cotizaciones.actualizarEnvio(envio, metodoEnvio, lt_numero_operacion.Text);
                bool resultadoTotales = cotizacionesProductos.actualizarTotalStatic(lt_numero_operacion.Text);
            if (resultado != null)
            {
 


                cargasDatosOperacion();
                materializeCSS.crear_toast(this, "Envío actualizado con éxito", true);
            }
            else
            {
                materializeCSS.crear_toast(this, "Error al actualizar método de envío", true);
            }
            return;
        } 
        
        if (metodoEnvio == "Estándar" && chk_CalculoAutomáticoEnvio.Checked)
        {
            txt_envio.Enabled = false;
            string resultado = cotizaciones.actualizarEnvio(envio, metodoEnvio, lt_numero_operacion.Text);
            CalcularEnvio();
            return;
        }

        if (metodoEnvio != "Estándar"  )
        {
            string resultado = cotizaciones.actualizarEnvio(envio, metodoEnvio, lt_numero_operacion.Text);
            bool resultadoTotales = cotizacionesProductos.actualizarTotalStatic(lt_numero_operacion.Text);

            if (resultado != null)
            {
                cotizacionesProductos actualizar = new cotizacionesProductos();


                cargasDatosOperacion();
                materializeCSS.crear_toast(this, "Envío actualizado con éxito", true);
            }
            else
            {
                materializeCSS.crear_toast(this, "Error al actualizar método de envío", true);
            }
        }


        }

    protected void btn_ConvertirMoneda_Click(object sender, EventArgs e) {
        string moneda = lbl_moneda.Text;
        string numero_operacion = lt_numero_operacion.Text;
        bool resultadoProductos = false;
        bool resultadoTotales = false;

        if (moneda == "USD") {

           resultadoProductos =   cotizacionesProductos.convertirProductosMonedaToMXN(numero_operacion, operacionesConfiguraciones.obtenerTipoDeCambio());

            if(resultadoProductos) {
             resultadoTotales =     cotizacionesProductos.actualizarTotalStatic(numero_operacion);
                if (resultadoTotales) {
                    materializeCSS.crear_toast(this, "Cambio de moneda correcto", true);
                    }
                } else {
                materializeCSS.crear_toast(this, "ERROR al calcular productos, verifique cantidades", false);
                materializeCSS.crear_toast(this, "ERROR al calcular productos, verifique cantidades", false);
                }


            }
        
        else if(moneda == "MXN") {

            resultadoProductos = cotizacionesProductos.convertirProductosMonedaToUSD(numero_operacion, operacionesConfiguraciones.obtenerTipoDeCambio());

            if (resultadoProductos ) {
                resultadoTotales = cotizacionesProductos.actualizarTotalStatic(numero_operacion);
                if (resultadoTotales) {
                    materializeCSS.crear_toast(this, "Cambio de moneda correcto", true);
                    }
                } else {
                materializeCSS.crear_toast(this, "ERROR al calcular productos, verifique cantidades", false);
                materializeCSS.crear_toast(this, "ERROR al calcular productos, verifique cantidades", false);
                }
            }

        cargasDatosOperacion();
        cargarProductos();
        detallesOperacion.cotizacion();
        up_operacion.Update();
        }

    protected void chk_alternativo_CheckedChanged(object sender, EventArgs e) {

        CheckBox chk_alternativo = (CheckBox)sender;
     
        ListViewItem lvItem = (ListViewItem)chk_alternativo.NamingContainer;
        HiddenField hf_idProducto = (HiddenField)lvItem.FindControl("hf_idProducto");
        UpdatePanel up_opcionesProducto = (UpdatePanel)lvItem.FindControl("up_opcionesProducto");

        int idProducto = int.Parse(hf_idProducto.Value);
       
        try {
            if (cotizacionesProductos.actualizarProductoAlternativo(lt_numero_operacion.Text, idProducto, chk_alternativo.Checked)) {
                materializeCSS.crear_toast(up_opcionesProducto, "Producto actualizado con éxito", true);
            }
        }
        catch (Exception ex) {
             
            materializeCSS.crear_toast(up_opcionesProducto, "Error al actualizar producto", false);
        }

    }
    protected void btn_guardarPlantilla_Click(object sender, EventArgs e) {
        bool resultado = cotizacionesPlantillas.guardarPlantillaDeCotizacion(lt_numero_operacion.Text);

        if (resultado) materializeCSS.crear_toast(this, "Plantilla guardada con éxito", true);
        else if (resultado) materializeCSS.crear_toast(this, "Error al guardar plantilla", true);
    }

    protected void btn_async_Click(object sender, EventArgs e) {
       LinkButton btn = sender as LinkButton;
       string id =  btn.ID;
       devNotificaciones.notificacionSimple(id);
            }

    protected void ddl_tipo_cotizacion_SelectedIndexChanged(object sender, EventArgs e) {

        bool resultado = cotizaciones.actualizar_tipo_cotizacion(lt_numero_operacion.Text, ddl_tipo_cotizacion.SelectedValue);

        if (resultado == true) {
            materializeCSS.crear_toast(this, "Campo actualizado con éxito", true);
        } else {
            materializeCSS.crear_toast(this, "Error al actualizar el tipo de cotización", false);
        }
    }

    protected void txt_descuento_TextChanged(object sender, EventArgs e) {
        string numero_operacion = lt_numero_operacion.Text;
        decimal descuento =   textTools.soloNumerosD(txt_descuento.Text);

    
       bool resultado = cotizacionesProductos.establecerDescuento(numero_operacion, descuento);

        if(resultado) materializeCSS.crear_toast(this, "Descuento aplicado con éxito", true);
        else materializeCSS.crear_toast(this, "Error al aplicar descuento", false);
        cargasDatosOperacion();
        }

    protected void CalcularEnvio()
    {

        string numero_operacion = lt_numero_operacion.Text;
        

            try
            {
            ValidarCalculoEnvioOperacion validar = new ValidarCalculoEnvioOperacion(numero_operacion, "cotizacion");
            materializeCSS.crear_toast(this, validar.Message, validar.OperacionValida);

                 
            }
            catch (Exception ex)
            {

            materializeCSS.crear_toast(this, "Ocurrio un error", false);

            }

              cargasDatosOperacion();

    }



    protected void chk_CalculoAutomáticoEnvio_CheckedChanged(object sender, EventArgs e)
    {
        string numero_operacion = lt_numero_operacion.Text;
        cotizaciones.establecerEstatusCalculo_Costo_Envio(chk_CalculoAutomáticoEnvio.Checked, numero_operacion);

        if (chk_CalculoAutomáticoEnvio.Checked == false)
        {
            txt_envio.Enabled = true;
        }
    }
}
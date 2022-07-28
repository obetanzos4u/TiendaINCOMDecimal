using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class usuario_pedidoDatos : System.Web.UI.Page
{
    NumberFormatInfo myNumberFormatInfo = new CultureInfo("es-MX", true).NumberFormat;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {

            cargasDatosOperacion() ;
            cargarProductos();

         //   if (usuarios.userLogin().tipo_de_usuario == "cliente") link_pago.Visible = false;
        }
       
         
        }



    protected void cargasDatosOperacion() {

        if (Page.RouteData.Values["id_operacion"] != null) {


            string route_id_operacion = Page.RouteData.Values["id_operacion"].ToString();

            route_id_operacion = seguridad.DesEncriptar(Page.RouteData.Values["id_operacion"].ToString());

         


            pedidosDatos obtener = new pedidosDatos();

            DataTable dt_PedidoDatos = obtener.obtenerPedidoDatosMax(int.Parse(route_id_operacion));

            if(dt_PedidoDatos != null && dt_PedidoDatos.Rows.Count >= 1) {

                string usuario_cliente = dt_PedidoDatos.Rows[0]["usuario_cliente"].ToString();
                bool permisoVisualizar = privacidadAsesores.validarOperacion(usuario_cliente);
               

                if (permisoVisualizar) { }
                else
                {
                    Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority));
                }
                string moneda = dt_PedidoDatos.Rows[0]["monedaPedido"].ToString();

                string metodoEnvio = dt_PedidoDatos.Rows[0]["metodoEnvio"].ToString();
                string envio = Math.Round(decimal.Parse(dt_PedidoDatos.Rows[0]["envio"].ToString()), 2).ToString("C2", myNumberFormatInfo) + " " + moneda;

                


                string subtotal = dt_PedidoDatos.Rows[0]["subtotal"].ToString();
                string impuestos = dt_PedidoDatos.Rows[0]["impuestos"].ToString();
                decimal total = decimal.Parse(dt_PedidoDatos.Rows[0]["total"].ToString());
                string descuento = dt_PedidoDatos.Rows[0]["descuento"].ToString();
                string descuento_porcentaje = dt_PedidoDatos.Rows[0]["descuento_porcentaje"].ToString();

                decimal d_subtotal = Math.Round(decimal.Parse(subtotal), 2);

                int id_operacion = int.Parse(dt_PedidoDatos.Rows[0]["id"].ToString());
                string numero_operacion = dt_PedidoDatos.Rows[0]["numero_operacion"].ToString();




                lt_numero_operacion.Text = numero_operacion;

                Page.Title = "Productos de pedido #" + numero_operacion;
                lt_nombre_pedido.Text = dt_PedidoDatos.Rows[0]["nombre_pedido"].ToString();
                lt_cliente_nombre.Text = dt_PedidoDatos.Rows[0]["cliente_nombre"].ToString() + " " + dt_PedidoDatos.Rows[0]["cliente_apellido_paterno"].ToString();
                hf_id_operacion.Value = dt_PedidoDatos.Rows[0]["id"].ToString();
                lbl_moneda.Text = moneda;


                // if (subtotal == "" || string.IsNullOrEmpty(subtotal)) subtotal = "0";


                // Validamos que haya un descuento aplicado pues si este se encuentra, es necesario mostrarlo, de lo contrario no, pues ocupario mayor espacio en el desglose
                if (!string.IsNullOrWhiteSpace(descuento) && !string.IsNullOrWhiteSpace(descuento_porcentaje))
                {
                    content_descuento.Visible = true;
                    decimal d_descuento = decimal.Parse(descuento);

                    lbl_subtotalSinDescuento.Text = Math.Round(d_subtotal + d_descuento, 2).ToString("C2", myNumberFormatInfo) + " " + moneda;



                    lbl_descuento_porcentaje.Text = Math.Round(decimal.Parse(descuento_porcentaje), 1).ToString();
                    txt_descuento.Text = Math.Round(decimal.Parse(descuento_porcentaje), 1).ToString();
                }
                else
                {

                    content_descuento.Visible = false;
                }


                lbl_subTotal.Text = d_subtotal.ToString("C2", myNumberFormatInfo) + " " + moneda;

                lbl_impuestos.Text = decimal.Parse(impuestos).ToString("C2", myNumberFormatInfo) + " " + moneda;

                lbl_total.Text = decimal.Parse(total.ToString()).ToString("C2", myNumberFormatInfo) + " " + moneda;

                hl_editarDatos.NavigateUrl = GetRouteUrl("usuario-pedido-datos", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",seguridad.Encriptar(id_operacion.ToString()) }
                    });
                hl_editarProductos.NavigateUrl = GetRouteUrl("usuario-pedido-productos", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",seguridad.Encriptar(id_operacion.ToString()) }
                    });

                link_enviar.NavigateUrl = GetRouteUrl("usuario-pedido-visualizar", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion", seguridad.Encriptar(hf_id_operacion.Value) },

                     });
                link_pago.NavigateUrl = GetRouteUrl("usuario-pedido-pago-santander", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion", seguridad.Encriptar(hf_id_operacion.Value) },

                     });


                lbl_envio.Text = envio;
                lbl_metodo_envio.Text = metodoEnvio;


                chk_CalculoAutomáticoEnvio.Checked = pedidosDatos.obtenerEstatusCalculo_Costo_Envio(numero_operacion);

                /// INICIO - Método de envío

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
                        if (chk_CalculoAutomáticoEnvio.Checked) txt_envio.Enabled = false;
                        txt_envio.Enabled = true; break;

                    case "Ninguno":

                        txt_envio.Enabled = false; break;


                }

                /// FIN - Método de envío
                /// 
            } else {

                }

            } else {
            Server.Transfer("/usuario/pedidos/datos/", false); }
        
        }


    protected void cargarProductos() {
        pedidosProductos obtener = new pedidosProductos();

        DataTable productosPedidos = pedidosProductos.obtenerProductos(lt_numero_operacion.Text);

        lv_productosCotizacion.DataSource = productosPedidos;
        lv_productosCotizacion.DataBind();

      
        }
    protected void lv_productos_OnItemDataBound(object sender, ListViewItemEventArgs e) {


        System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;

        string monedaPedido = lbl_moneda.Text;
        float precio_unitario = float.Parse(rowView["precio_unitario"].ToString());
        float precio_total = float.Parse(rowView["precio_total"].ToString());

        string numero_parte = rowView["numero_parte"].ToString();
        string titulo = rowView["titulo"].ToString();
        string marca = rowView["marca"].ToString();

        Label lbl_precio_unitario = (Label)e.Item.FindControl("lbl_precio_unitario");
        lbl_precio_unitario.Text = precio_unitario.ToString("C2", myNumberFormatInfo) + " " + monedaPedido;

        Label lbl_precio_total = (Label)e.Item.FindControl("lbl_precio_total");
        lbl_precio_total.Text =  precio_total.ToString("C2", myNumberFormatInfo) + " " + monedaPedido;

        Image imgProducto = (Image)e.Item.FindControl("imgProducto");
        HiddenField hf_tipoProducto = (HiddenField)e.Item.FindControl("hf_tipoProducto");

        HyperLink link_producto = (HyperLink)e.Item.FindControl("link_producto");
        HyperLink link_imgProducto = (HyperLink)e.Item.FindControl("link_imgProducto");

        imgProducto.ImageUrl = archivosManejador.imagenProducto(rowView["imagenes"].ToString().Split(',')[0]);

        link_producto.NavigateUrl = GetRouteUrl("productos", new System.Web.Routing.RouteValueDictionary {
                        { "numero_parte", textTools.limpiarURL_NumeroParte(numero_parte) },
                         { "marca", marca },
                        { "productoNombre",  textTools.limpiarURL(titulo) }
                    });
        link_imgProducto.NavigateUrl = link_producto.NavigateUrl;

        TextBox txt_cantidadPedido = (TextBox)e.Item.FindControl("txt_cantidadPedido");
        LinkButton btn_eliminarProducto = (LinkButton)e.Item.FindControl("btn_eliminarProducto");

        if (usuarios.modoAsesorActivado() == 1) { 
            txt_cantidadPedido.Enabled = true;
            btn_eliminarProducto.Enabled = true;
        }

        // Si el producto no es de los productos 
        if (hf_tipoProducto.Value != "1") {

            // Desactivo de links al ser producto personalizado  o servicio
            link_imgProducto.NavigateUrl = "";
            link_producto.NavigateUrl = "";
            link_producto.Visible = false;
        }
        }
    protected void txt_cantidadPedido_TextChanged(object sender, EventArgs e) {
        TextBox txt_cantidad = sender as TextBox;
        // Obtenemos el contenedor del objeto que creo el evento
        ListViewItem lvItemPedido = (ListViewItem)txt_cantidad.NamingContainer;
        HiddenField hf_cantidadPedido = (HiddenField)lvItemPedido.FindControl("hf_cantidadPedido");
        decimal cantidad = decimal.MinusOne;

        if (txt_cantidad.Text != "" && txt_cantidad.Text != null) {
            try {
                cantidad = textTools.soloNumerosD(txt_cantidad.Text);
                }
            catch (Exception ex) {
                materializeCSS.crear_toast(Parent.Parent.Page, "Debe ingresar valores númericos", false);
                }

            } else {
            materializeCSS.crear_toast(this, "Debe ingresar valores númericos", false);
            }

        if (cantidad >= 1) {


           

            // Buscamos un objeto dentro del contenedor
            HiddenField hf_idProductoCarrito = (HiddenField)lvItemPedido.FindControl("hf_idProductoCarrito");

            // 1 Base, 2 Personalizado, 3 Servicio
            HiddenField tipoProducto = (HiddenField)lvItemPedido.FindControl("hf_tipoProducto");

            bool modalidadAsesor = Boolean.Parse(Session["modoAsesor"].ToString());

            Literal lt_numeroParte = lvItemPedido.FindControl("lt_numeroParte") as Literal;
            
            float tipoCambio = float.Parse(HttpContext.Current.Session["tipoCambio"].ToString());

            // Iniciamos la obtención de precios

            string numero_operacion = lt_numero_operacion.Text;
            string numero_parte = lt_numeroParte.Text;


            //-- INICIO parte DatosUsuario
            usuarios datosUsuario = usuarios.modoAsesor();



            productosTienda obtener = new productosTienda();
            preciosTienda procesar = new preciosTienda();

            DataTable DTproductos = obtener.obtenerProducto(numero_parte);
           // string monedaTienda = (uc_moneda.FindControl("ddl_moneda") as DropDownList).SelectedValue;

            // Realmente es referente a la moneda de la operación
            procesar.monedaTienda = lbl_moneda.Text;

            //  Procesamos los productos para precios
            DTproductos = procesar.procesarProductos(DTproductos);
            // Si ha pasado las reglas de procesamiento(precios, privacidad, etc) continuamos, si no cancelamos
            if (DTproductos.Rows.Count >= 1 && DTproductos != null) {
               

                model_productosTienda producto = obtener.dtProductoToList(DTproductos);
                model_pedidos_productos productoPedido = new model_pedidos_productos();



                decimal precio = decimal.MinusOne;

                // Si el producto no tiene la propiedad precio (no encontró precio fijo) [columna precio = "precio fijo"]
                if (producto.precio <=0) {
                    List<preciosTabulador> preciosTab = new List<preciosTabulador>();

                    preciosTab.Add(new preciosTabulador { min = producto.min1, max = producto.max1, precio = producto.precio1 });
                    preciosTab.Add(new preciosTabulador { min = producto.min2, max = producto.max2, precio = producto.precio2 });
                    preciosTab.Add(new preciosTabulador { min = producto.min3, max = producto.max3, precio = producto.precio3 });
                    preciosTab.Add(new preciosTabulador { min = producto.min4, max = producto.max4, precio = producto.precio4 });
                    preciosTab.Add(new preciosTabulador { min = producto.min5, max = producto.max5, precio = producto.precio5 });

                    // Obtenemos el precio adecuado de acuerdo a la cantidad
                    precio = preciosTienda.precioRango(preciosTab, cantidad);

                    //  Procesamos el precio corrcto a la moneda, en este caso la de la operación y no de la tienda
                    // Recordar que en el momento que se hizo la instancia se establecio la moneda de la operación en la variable "monedaTienda"
                    precio = procesar.precio_a_MonedaTienda(producto.moneda_rangos, precio);

                    } else if (producto.precio > 0) {
                    precio = procesar.precio_a_MonedaTienda(producto.moneda_fija, producto.precio);
                    }



                decimal precioTotal = precio * cantidad;

                DateTime fechaActual = utilidad_fechas.obtenerCentral();
                productoPedido.usuario = HttpContext.Current.User.Identity.Name;
                // productoPedido.activo = 1;
                productoPedido.tipo = int.Parse(tipoProducto.Value);
                // productoPedido.fecha_creacion = utilidad_fechas.obtenerCentral();
                productoPedido.numero_parte = numero_parte;
                productoPedido.descripcion = producto.descripcion_corta;
                productoPedido.marca = producto.marca;
                // productoPedido.moneda = lbl_moneda.Text;
                // productoPedido.tipo_cambio = tipoCambio;
                // productoPedido.fecha_tipo_cambio = fechaActual;
                productoPedido.unidad = producto.unidad;
                productoPedido.precio_unitario = precio;
                productoPedido.cantidad = cantidad;
                productoPedido.precio_total = precioTotal;
                // productoPedido.stock1 = 0;
                // productoPedido.stock1_fecha = utilidad_fechas.obtenerCentral();



                try {

                    pedidosProductos add = new pedidosProductos();


                    if (add.actualizarCantidadProducto(numero_operacion, productoPedido) != false && add.actualizarTotal(numero_operacion) != false) {
                        CalcularEnvio();
                        cargarProductos();
                        cargasDatosOperacion();
                        materializeCSS.crear_toast(Page, "Producto actualizado con éxito", true);
                        } else {
                        materializeCSS.crear_toast(Page, "Error al actualizar producto", false);
                        }
                    }
                catch (Exception ex) {
                    txt_cantidad.Text = hf_cantidadPedido.Value;
                    materializeCSS.crear_toast(Page, "Error al actualizar producto", false);
                    }
                }
            else {
                // Como no paso la validación se cancela todo

                materializeCSS.crear_toast(Page, "El producto no esta disponible temporalmente. ", false);
                }
            } else {
            txt_cantidad.Text = hf_cantidadPedido.Value;
            materializeCSS.crear_toast(Page, "Debes ingresar una cantidad mayor o igual a 1", false);
            }

       
        }

    protected void btn_eliminarProducto_Click(object sender, EventArgs e) {
        LinkButton btnEliminar = (LinkButton)sender;
        // Obtenemos el contenedor del objeto que creo el evento
        ListViewItem lvItem = (ListViewItem)btnEliminar.NamingContainer;

        HiddenField hf_idProducto = (HiddenField)lvItem.FindControl("hf_idProducto");

        string idProducto = hf_idProducto.Value;
        string numero_operacion = lt_numero_operacion.Text;
        pedidosProductos eliminar = new pedidosProductos();

        try {
            eliminar.eliminarProducto(idProducto);
            eliminar.actualizarTotal(numero_operacion);
            materializeCSS.crear_toast(this, "Producto eliminado con éxito", true);
            CalcularEnvio();
            cargarProductos();
            cargasDatosOperacion();
            up_operacion.Update();
            }
        catch (Exception ex) {
            devNotificaciones.error("Error al eliminar producto de carrito", ex);
            materializeCSS.crear_toast(this, "Error al eliminar producto", false);
            }

       
        }

    protected void ddl_metodo_envio_SelectedIndexChanged(object sender, EventArgs e) {
        string metodoEnvio = ddl_metodo_envio.SelectedValue;


        switch(metodoEnvio)
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

        if (envio > 0 && metodoEnvio == "Estándar" && chk_CalculoAutomáticoEnvio.Checked == false)
        {


            string resultado =  pedidosDatos.actualizarEnvio(envio, metodoEnvio, lt_numero_operacion.Text);
            bool resultadoTotales = pedidosProductos.actualizarTotalStatic(lt_numero_operacion.Text);
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
            string resultado = pedidosDatos.actualizarEnvio(envio, metodoEnvio, lt_numero_operacion.Text);
            CalcularEnvio();
            return;
        }

        if (metodoEnvio != "Estándar")
        {
            string resultado = pedidosDatos.actualizarEnvio(envio, metodoEnvio, lt_numero_operacion.Text);
            bool resultadoTotales = pedidosProductos.actualizarTotalStatic(lt_numero_operacion.Text);

            if (resultado != null) {
              


                cargasDatosOperacion();
                materializeCSS.crear_toast(this, "Envío actualizado con éxito", true);
            }
            else
            
                materializeCSS.crear_toast(this, "Error al actualizar método de envío", true);
            
        }

    }

    protected void txt_descuento_TextChanged(object sender, EventArgs e)
    {
        string numero_operacion = lt_numero_operacion.Text;
        decimal descuento = textTools.soloNumerosD(txt_descuento.Text);


        bool resultado = pedidosProductos.establecerDescuento(numero_operacion, descuento);

        if (resultado) materializeCSS.crear_toast(this, "Descuento aplicado con éxito", true);
        else materializeCSS.crear_toast(this, "Error al aplicar descuento", false);
        cargasDatosOperacion();
    }

    protected void CalcularEnvio()
    {

        string numero_operacion = lt_numero_operacion.Text;


        try
        {
            ValidarCalculoEnvioOperacion validar = new ValidarCalculoEnvioOperacion(numero_operacion, "pedido");
            materializeCSS.crear_toast(this, validar.Message, validar.OperacionValida);


        }
        catch (Exception ex)
        {
            devNotificaciones.error("Calcular envio en pedido: " + numero_operacion + " ", ex);
            materializeCSS.crear_toast(this, "Ocurrio un error", false);

        }

        cargasDatosOperacion();

    }
    protected void chk_CalculoAutomáticoEnvio_CheckedChanged(object sender, EventArgs e)
    {
        string numero_operacion = lt_numero_operacion.Text;
        pedidosDatos.establecerEstatusCalculo_Costo_Envio(chk_CalculoAutomáticoEnvio.Checked, numero_operacion);

        if (chk_CalculoAutomáticoEnvio.Checked == false)
        {
            txt_envio.Enabled = true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.Globalization;

using System.IO;
using System.Threading.Tasks;
using System.Dynamic;
using System.Data.Entity;
using System.Net.Http;
using System.Web.UI.HtmlControls;
using DocumentFormat.OpenXml.Drawing;
using RestSharp;
using RestSharp.Authenticators;

public partial class mi_carrito : System.Web.UI.Page
{
    NumberFormatInfo myNumberFormatInfo = new CultureInfo("es-MX", true).NumberFormat;
    decimal envio = 0;
    decimal subtotal = 0;
    protected void mostrarModalContacto()
    {
        string script = @"
                        setTimeout(function () {
                            M.Modal.getInstance(document.querySelector('#modal_NumerosContacto')).open(); 
                                                }, 1500);  
                  ";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalContacto", script, true);
    }

    protected async void ProcesarProductosPromo()
    {
        //if (HttpContext.Current.Session["ListProductsPromos"] != null)
        //{
        //    var TempListProductsPromos = (List<PromocionesProductoModel>)HttpContext.Current.Session["ListProductsPromos"];

        //    var ListProductsPromos = TempListProductsPromos
        //        .GroupBy(p => new { p.numero_parte })
        //      .Select(g => g.First())
        //      .ToList();


        //    foreach (var ProducPromo in ListProductsPromos)
        //    {
        //        string numero_parte = ProducPromo.numero_parte;

        //        continue;

        //    }

        //}
        //else
        //{


        //    string sadsasad;
        //}


    }
    protected void Page_Load(object sender, EventArgs e)
    {

        // El carrito solo es para usuarios con cuenta
        if (!HttpContext.Current.User.Identity.IsAuthenticated) Server.Transfer("~/iniciar-sesion.aspx");

        if (!IsPostBack)
        {
            Page.Title = "Carrito de compras";
            Page.MetaDescription = "Carrito de compras, compra en linea, telecomunicaciones y fibra óptica";
            obtenerStockCarrito();
            obtenerEnvio();
            cargarProductoAsync();
            up_carrito.Update();

            string emailUsuarioLogin = usuarios.userLogin().email;
            string emailClienteAsesor = usuarios.modoAsesor().email;
            var UserLogin = UsuariosEF.Obtener(emailUsuarioLogin);


            hf_UserLogin.Value = emailUsuarioLogin;

            #region Seccion que valida que tenga un teléfono principal
            if (string.IsNullOrEmpty(UserLogin.telefono) && string.IsNullOrEmpty(UserLogin.celular))
            {


                string script = @" 
                    document.addEventListener('DOMContentLoaded', () => { 
                          console.log('sasfa');
                         setTimeout(function () { 
                                                     M.Modal.getInstance(document.querySelector('#modal_NumerosContacto')).open(); 
                                                }, 3500);  
                    });";


                ScriptManager.RegisterStartupScript(this, this.GetType(), "ModalContacto", script, true);
            }
            #endregion


            #region  INICIO - Sección para evitar que se coticene ellos solitos
            if (UserLogin.tipo_de_usuario == "usuario")
            {
                btn_crearCotizacion.Enabled = false;
                btn_crearCotizacion.Visible = false;

                btn_comprar.Enabled = false;
                btn_comprar.Visible = false;

                //link_API_desglose_envio.Visible = true;
            }
            if (usuarios.modoAsesorActivado() == 1 && emailUsuarioLogin != emailClienteAsesor)
            {
                btn_crearCotizacion.Enabled = true;
                btn_crearCotizacion.Visible = true;

                btn_comprar.Enabled = true;
                btn_comprar.Visible = true;
            }
            #endregion FIN - Sección para evitar que se coticene ellos solitos

            //ProcesarProductosPromo
        }
    }

    private async void obtenerEnvio()
    {
        carrito obtener = new carrito();
        usuarios usuario = usuarios.modoAsesor();
        DataTable productosCarritos = obtener.obtenerCarritoUsuarioWithMedidas(usuario.email);

        try // Contemplar orden de llamadas
        {
            var r = await CalcularEnvio(productosCarritos, subtotal);
        }
        catch (Exception ex)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.info, "Error al calcular el costo de envío");
        }

        try
        {
            string strEnvio = lbl_envio.Text.Replace("MXN", "").Replace("$", "").Replace("USD", "").Replace(" ", "");
            envio = decimal.Parse(strEnvio);
        }
        catch (Exception ex)
        {
            lbl_envio.Text = "Será calculado por  asesor";
        }
    }

    protected async void obtenerStockCarrito()
    {
        carrito obtener = new carrito();
        usuarios usuario = usuarios.modoAsesor();
        if (carrito.obtenerCantidadProductos(usuario.email) == 0)
        {
            pantallaCarga.Visible = false;
            up_carrito.Visible = true;
        }
        else
        {
            pantallaCarga.Visible = true;
            up_carrito.Visible = false;
            try
            {
                DataTable productosCarritos = obtener.obtenerCarritoUsuarioWithSAP(usuario.email);
                string urlNumeroParte = "";
                var posicionFinal = productosCarritos.Rows.Count;
                var posicion = 0;
                // Construcción de url para consulta de stock a SAP
                foreach (DataRow productoCarrito in productosCarritos.Rows)
                {
                    posicion++;
                    if (posicion == 1 && productoCarrito["noParte_Sap"].ToString() != "")
                    {
                        urlNumeroParte += "CPRODUCT_ID eq '" + productoCarrito["noParte_Sap"] + "'";
                    }
                    else if (posicion == 1 && productoCarrito["noParte_Sap"].ToString() == "")
                    {
                        obtener.desactivarProductoCarrito(usuario.email, productoCarrito["numero_parte"].ToString());
                        posicion = 0;
                    }
                    else if (posicion < posicionFinal && productoCarrito["noParte_Sap"].ToString() != "")
                    {
                        urlNumeroParte += " or CPRODUCT_ID eq '" + productoCarrito["noParte_Sap"] + "'";
                    }
                    else if (posicion == posicionFinal && productoCarrito["noParte_Sap"].ToString() != "")
                    {
                        urlNumeroParte += " or CPRODUCT_ID eq '" + productoCarrito["noParte_Sap"] + "'";
                    }
                    else if (productoCarrito["noParte_Sap"].ToString() == "")
                    {
                        obtener.desactivarProductoCarrito(usuario.email, productoCarrito["numero_parte"].ToString());
                    }
                }

                if (urlNumeroParte != "")
                {
                    // Consulta de stock a SAP
                    var client = new RestClient("https://my338095.sapbydesign.com");
                    client.Authenticator = new HttpBasicAuthenticator("ARUIZ", "Incom#724!");

                    var request = new RestRequest("/sap/byd/odata/ana_businessanalytics_analytics.svc/RPZ3CFEC590A236E733BD9701QueryResults?$inlinecount=allpages&$select=CPRODUCT_ID,CQUANTITY_UOM,TQUANTITY_UOM,KRZAC52B3549F1E886FD1FA4D,KRZ38A3122568DF31A282B12B&$filter=(" + urlNumeroParte + ")&$format=json", Method.GET);
                    // Ejecución de función asíncrona en el mismo hilo hasta que termine de obtener el stock por medio de oData. 
                    Task<IRestResponse> t = client.ExecuteGetAsync(request);
                    t.Wait();
                    var response = await t;

                    if (response.IsSuccessful)
                    {
                        dynamic jsonResult = Newtonsoft.Json.JsonConvert.DeserializeObject(response.Content);
                        dynamic stockResult = jsonResult.d.results;
                        productos obtenerNumeroParte = new productos();
                        foreach (dynamic producto in stockResult)
                        {
                            string no_ParteSap = producto.CPRODUCT_ID;
                            int stock = producto.KRZ38A3122568DF31A282B12B;
                            string stockSolicitado = producto.KRZAC52B3549F1E886FD1FA4D;
                            string medida = producto.TQUANTITY_UOM;
                            string numero_parte = obtenerNumeroParte.obtenerNumeroParteWithSAP(no_ParteSap);
                            bool actualizacion = obtener.actualizarStockCarritoProducto(usuario.email, numero_parte, stock);
                            if (!actualizacion || stock == 0)
                            {
                                obtener.desactivarProductoCarrito(usuario.email, numero_parte);
                            }
                        }
                    }
                }
                pantallaCarga.Visible = false;
                up_carrito.Visible = true;
                //Response.Redirect("mi-carrito.aspx");
            }
            catch (Exception ex)
            {
                //devNotificaciones.error("Cálculo de stocks", ex);
                NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Error al obtener la disponibilidad de los productos");
            }
        }
        up_carrito.Update();
    }

    protected void cargarProductoAsync()

    {
        pantallaCarga.Visible = true;
        carrito obtener = new carrito();
        usuarios usuario = usuarios.modoAsesor();
        DataTable productosCarritos = obtener.obtenerCarritoUsuarioWithMedidas(usuario.email);

        lv_productosCarritos.DataSource = productosCarritos;
        lv_productosCarritos.DataBind();

        HttpContext ctx = HttpContext.Current;
        string monedaTienda = (uc_moneda.FindControl("ddl_moneda") as DropDownList).SelectedValue;
        string str_subtotal = "";
        //decimal subtotal = 0;


        if (carrito.obtenerCantidadProductos(usuario.email) > 0)
        {
            lbl_shoppingCartTitle.Text = "Carrito de compra";
            ctn_details.Visible = true;

            if (monedaTienda == "USD")
            {
                str_subtotal = obtener.obtenerTotalUSD(usuario.email);
            }
            else if (monedaTienda == "MXN")
            {
                str_subtotal = obtener.obtenerTotalMXN(usuario.email);
            }

            if (string.IsNullOrEmpty(str_subtotal))
            {
                subtotal = 0;
            }
            else
            {
                subtotal = decimal.Parse(str_subtotal);
            }

            if (carrito.obtenerCantidadProductos(usuario.email) >= 3)
            {
                moreArrow.Visible = true;
            }

            subtotal = subtotal + envio;
            lbl_subTotal.Text = subtotal.ToString("C2", myNumberFormatInfo) + " " + monedaTienda;

            decimal impuestos = subtotal * (decimal.Parse(Session["impuesto"].ToString()) - 1);
            decimal total = subtotal * decimal.Parse(Session["impuesto"].ToString());

            lbl_impuestos.Text = decimal.Parse(impuestos.ToString()).ToString("C2", myNumberFormatInfo) + " " + monedaTienda;
            lbl_total.Text = decimal.Parse(total.ToString()).ToString("C2", myNumberFormatInfo) + " " + monedaTienda;
        }
        else
        {
            ctn_details.Visible = false;
            lbl_consideraciones.Visible = false;
            btn_continuarCompra.Visible = false;
            //lbl_consideracionesCopy.Visible = false;
            moreArrow.Visible = false;
            lbl_shoppingCartTitle.Text = "Tu carrito está vacío";
        }
        pantallaCarga.Visible = false;
    }

    protected async Task<json_respuestas> CalcularEnvio(DataTable DTproductosCarritos, decimal subtotal)
    {
        string monedaTienda = (uc_moneda.FindControl("ddl_moneda") as DropDownList).SelectedValue;
        if (monedaTienda == "USD") subtotal = (decimal)(subtotal * operacionesConfiguraciones.obtenerTipoDeCambio());

        var total = subtotal * (decimal)1.16;

        var ListProductosEnvio = new List<ProductoEnvioCalculoModel>();
        foreach (DataRow r in DTproductosCarritos.Rows)
        {
            var p = new ProductoEnvioCalculoModel();
            p.Numero_Parte = r["numero_parte"].ToString();
            p.Tipo = 1;
            p.Cantidad = decimal.Parse(r["cantidad"].ToString());
            p.PesoKg = decimal.Parse(r["peso"].ToString());
            p.Largo = decimal.Parse(r["profundidad"].ToString());
            p.Ancho = decimal.Parse(r["ancho"].ToString());
            p.Alto = decimal.Parse(r["alto"].ToString());
            p.RotacionHorizontal = bool.Parse(r["RotacionHorizontal"].ToString());
            p.RotacionVertical = bool.Parse(r["RotacionVertical"].ToString());
            p.DisponibleParaEnvioGratuito = int.Parse(r["disponibleEnvio"].ToString());
            ListProductosEnvio.Add(p);
        }


        EnviosIncomReglas CalcularEnvioGratis = new EnviosIncomReglas("", "carrito", ListProductosEnvio, total); // numeroDeOperacion, tipoDeOperacion, listaDeProductos, montoTotal
        var ResultRegla = CalcularEnvioGratis.Resultado;

        // Si aplica el envío gratuito, lo mostramos y evitamos el cálculo real.
        if (ResultRegla.result == true)
        {
            lbl_envio.Text = String.Format("{0:C}", 0, "C2") + monedaTienda;
            lbl_envio_nota.Text = " <span class=\"new badge\" data-badge-caption=\"Gratis\" ></span>";
            return new json_respuestas();
        }
        lbl_envio_nota.Text = "";

        usuarios usuarioCarrito = usuarios.modoAsesor();
        try
        {

            if (DTproductosCarritos == null || DTproductosCarritos.Rows.Count == 0)
            {
                lbl_envio.Text = "0.00 " + monedaTienda;
                return new json_respuestas();

            }
            if (operacionesConfiguraciones.obtenerEstatusApiFlete() == false)
            {
                lbl_envio.Text = "Pendiente";
                return new json_respuestas();
            }
            direcciones_envio DireccionEnvioPreferida = new direcciones_envio();
            int idUusuario = usuarioCarrito.id;

            using (var db = new tiendaEntities())
            {
                DireccionEnvioPreferida = db.direcciones_envio
                    .Where(d => d.direccion_predeterminada == true && d.id_cliente == idUusuario)
                      .AsNoTracking()
                      .FirstOrDefault();
            }

            ValidarCalculoEnvioConsulta CostoEnvio = new ValidarCalculoEnvioConsulta(DireccionEnvioPreferida, DTproductosCarritos, usuarioCarrito.email, subtotal);

            CostoEnvio.host = HttpContext.Current.Request.Url.Host;
            await CostoEnvio.validarConsulta();

            if (CostoEnvio.OperacionValida)
            {
                decimal? precio = CostoEnvio.CostoEnvio;


                if (monedaTienda == "USD") precio = precio / tipoDeCambio.obtenerTipoDeCambio();


                lbl_envio.Text = String.Format("{0:C}", precio, "C2") + monedaTienda;

                //link_API_desglose_envio.NavigateUrl = "https://apiweb.incom.mx/fletes/CalculoFlete.aspx?Numero_Operacion=" + usuarioCarrito.email; // Error because usuarioCarrito.email its invalid parameter.
                //link_API_desglose_envio.NavigateUrl = "https://apiweb.incom.mx/fletes/CalculoFlete.aspx"; // Error because usuarioCarrito.email its invalid parameter.
                //link_API_desglose_envio.Visible = false;
            }
            else
            {
                lbl_envio.Text = CostoEnvio.Message;
                NotiflixJS.Message(this, NotiflixJS.MessageType.info, CostoEnvio.Message);
                //materializeCSS.crear_toast(this, CostoEnvio.Message, false);
                //link_API_desglose_envio.Visible = false;
            }
            return new json_respuestas();
        }
        catch (Exception ex)
        {
            lbl_envio.Text = "El envío no ha podido ser calculado o no se ha establecido dirección de entrega.";
            return new json_respuestas();
            //devNotificaciones.error("Calcular envio:", ex);
            //devNotificaciones.ErrorSQL("Calcular flete carrito " + usuarioCarrito.email, ex, ex.Message);
        }
    }
    protected async void lv_productos_OnItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ContentAlertCrearCotizacion.Visible = false;
        //ContentAlertCrearPedido.Visible = false;
        //ContentAlertCrearPedido.InnerHtml = "";
        ContentAlertCrearCotizacion.InnerHtml = "";

        UserControl uc_moneda = e.Item.Parent.Parent.Parent.FindControl("uc_moneda") as UserControl;

        string monedaTienda = (uc_moneda.FindControl("ddl_moneda") as DropDownList).SelectedValue;

        preciosTienda procesar = new preciosTienda();
        procesar.monedaTienda = monedaTienda;

        System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;

        decimal precio_unitario = decimal.Parse(rowView["precio_unitario"].ToString());
        decimal precio_total = decimal.Parse(rowView["precio_total"].ToString());
        decimal tipo_cambio = decimal.Parse(rowView["tipo_cambio"].ToString());
        decimal cantidad = decimal.Parse(rowView["cantidad"].ToString());
        precio_unitario = procesar.precio_a_MonedaTienda(tipo_cambio, rowView["moneda"].ToString(), precio_unitario);
        precio_total = procesar.precio_a_MonedaTienda(tipo_cambio, rowView["moneda"].ToString(), precio_total);
        string numero_parte = rowView["numero_parte"].ToString();
        string titulo = rowView["titulo"].ToString();
        string marca = rowView["marca"].ToString();
        string activo = rowView["activo"].ToString();
        string stock = rowView["stock1"].ToString();

        TextBox txt_cantidadCarrito = (TextBox)e.Item.FindControl("txt_cantidadCarrito");
        HtmlGenericControl warning_envios_medidas = (HtmlGenericControl)e.Item.FindControl("warning_envios_medidas");
        HtmlGenericControl lbl_stock = (HtmlGenericControl)e.Item.FindControl("lbl_stock");

        #region valida que el producto exista en la  productos datos si no, muestra una advertencia
        var resultExistenciaProducto = await ProductosTiendaEF.ObtenerSoloNumerosParte(numero_parte);

        if (resultExistenciaProducto == null)
        {

            warning_envios_medidas.Visible = true;
            warning_envios_medidas.InnerHtml += "<br>Este producto podría ya no estar disponible para su venta o no esta disponible temporalmente.";
            btn_comprar.Enabled = false;
            btn_crearCotizacion.Enabled = false;

            ContentAlertCrearCotizacion.Visible = true;
            //ContentAlertCrearPedido.Visible = true;
            //ContentAlertCrearPedido.InnerHtml = "<strong>Aviso:<strong> Elimina los productos no disponibles para la venta para continuar.";
            ContentAlertCrearCotizacion.InnerHtml = "<strong>Aviso:<strong> Elimina los productos no disponibles para la venta para continuar.";
        }


        #endregion

        if (usuarios.modoAsesor().tipo_de_usuario == "usuario")
        {
            if (string.IsNullOrEmpty(rowView["alto"].ToString())
                || string.IsNullOrEmpty(rowView["ancho"].ToString())
                 || string.IsNullOrEmpty(rowView["peso"].ToString())
                  || string.IsNullOrEmpty(rowView["profundidad"].ToString())
                )
            {
                warning_envios_medidas.Visible = true;
                warning_envios_medidas.InnerText = "Este producto no tiene las dimensiones completas.";
            }
        }
        if (cantidad % 1 == 0)
        {
            txt_cantidadCarrito.Text = cantidad.ToString("#");
        }
        else
        {
            txt_cantidadCarrito.Text = Math.Round(cantidad, MidpointRounding.ToEven).ToString("#");
        }

        Label lbl_precio_unitario = (Label)e.Item.FindControl("lbl_precio_unitario");
        lbl_precio_unitario.Text = precio_unitario.ToString("#,#.##", myNumberFormatInfo) + " " + monedaTienda;

        Label lbl_precio_total = (Label)e.Item.FindControl("lbl_precio_total");
        lbl_precio_total.Text = decimal.Parse(precio_total.ToString()).ToString("#,#.##", myNumberFormatInfo) + " " + monedaTienda;
        Image imgProducto = (Image)e.Item.FindControl("imgProducto");
        HyperLink link_producto = (HyperLink)e.Item.FindControl("link_producto");
        //HyperLink link_imgProducto = (HyperLink)e.Item.FindControl("link_imgProducto");
        imgProducto.ImageUrl = archivosManejador.imagenProducto(rowView["imagenes"].ToString().Split(',')[0]);

        link_producto.NavigateUrl = GetRouteUrl("productos", new System.Web.Routing.RouteValueDictionary {
                        { "numero_parte", textTools.limpiarURL_NumeroParte(numero_parte) },
                         { "marca", marca },
                        { "productoNombre",  textTools.limpiarURL(titulo) }
                    });
        //link_imgProducto.NavigateUrl = link_producto.NavigateUrl;
        up_carrito.Update();
        if (stock == "0")
        {
            lbl_stock.Visible = true;
            lbl_stock.InnerText = "Sin stock";
            txt_cantidadCarrito.Attributes.Add("min", "0");
            txt_cantidadCarrito.Attributes.Add("max", "0");
            NotiflixJS.Message(this, NotiflixJS.MessageType.info, "Hay productos sin stock en tu carrito, no se agregarán al pedido.");
        }
        else
        {
            txt_cantidadCarrito.Attributes.Add("min", "1");
            txt_cantidadCarrito.Attributes.Add("max", stock);
        }

        tienda.uc_precio_detalles detalles_precios = (tienda.uc_precio_detalles)e.Item.FindControl("detalles_precios");

        detalles_precios.numero_parte = numero_parte;
        detalles_precios.moneda = monedaTienda;
        detalles_precios.size = "min";
    }
    protected async void txt_cantidadCarrito_TextChanged(object sender, EventArgs e)
    {
        decimal cantidad = Math.Round(decimal.Parse(((TextBox)sender).Text), MidpointRounding.ToEven);
        // Obtenemos el contenedor del objeto que creo el evento
        ListViewItem lvItemCarrito = (ListViewItem)((TextBox)sender).NamingContainer;
        Literal lt_numeroParte = lvItemCarrito.FindControl("lt_numeroParte") as Literal;
        string numero_parte = lt_numeroParte.Text;
        string monedaTienda = HttpContext.Current.Session["monedaTienda"].ToString();
        operacionesProductos actualizar = new operacionesProductos("carrito", "update", "", numero_parte, cantidad.ToString(), monedaTienda);

        await actualizar.agregarProductoAsync();

        bool resultado = actualizar.resultado_operacion;

        if (resultado == true)
        {
            cargarProductoAsync();
            up_carrito.Update();
            NotiflixJS.Message(this, NotiflixJS.MessageType.success, actualizar.mensaje_ResultadoOperacion);
            //materializeCSS.crear_toast(this, actualizar.mensaje_ResultadoOperacion, resultado);
        }
        else
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.info, actualizar.mensaje_ResultadoOperacion);
            //materializeCSS.crear_toast(this, actualizar.mensaje_ResultadoOperacion, resultado);
        }
    }

    protected void btn_eliminarProducto_Click(object sender, EventArgs e)
    {
        LinkButton btnEliminar = (LinkButton)sender;
        // Obtenemos el contenedor del objeto que creo el evento
        ListViewItem lvItemCarrito = (ListViewItem)btnEliminar.NamingContainer;

        HiddenField hf_idProductoCarrito = (HiddenField)lvItemCarrito.FindControl("hf_idProductoCarrito");

        string idProductoCarrito = hf_idProductoCarrito.Value;
        carrito eliminar = new carrito();

        try
        {
            eliminar.eliminarProductoCarrito(idProductoCarrito);
            obtenerStockCarrito();
            cargarProductoAsync();
            up_carrito.Update();
            NotiflixJS.Message(this, NotiflixJS.MessageType.info, "Producto eliminado con éxito");
            //materializeCSS.crear_toast(this, "Producto eliminado con éxito", true);
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Error al eliminar producto de carrito", ex);
            NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Error al eliminar el producto");
            //materializeCSS.crear_toast(this, "Error al eliminar producto", false);
        }
    }

    protected async void btn_crearCotizacion_Click(object sender, EventArgs e)
    {
        usuarios usuario = usuarios.modoAsesor();
        usuarios usuarioLogin = usuarios.userLogin();

        string InfoDeContacto = $"{usuario.nombre} {usuario.apellido_paterno} {usuario.apellido_materno}, {usuario.email} " +
                 $"Tel: {usuario.telefono}, Cel: {usuario.celular}";

        if (true
            // !string.IsNullOrWhiteSpace(telefono) || telefono.Length > 50
            )
        {
            if (carrito.obtenerCantidadProductos(usuario.email) < 1)
            {
                NotiflixJS.Message(this, NotiflixJS.MessageType.info, "Aún no tienes productos en tu carrito");
                //materializeCSS.crear_toast(this, "Aún no tienes productos en tu carrito", false);
            }
            else
            {
                string nombreCotizacion = txtNombrecotizacion.Text;

                if (String.IsNullOrEmpty(nombreCotizacion) || nombreCotizacion.Length < 3)
                {
                    nombreCotizacion = utilidad_fechas.AAAMMDD() + " Sin nombre";
                }

                model_impuestos impuestos = new model_impuestos() { nombre = "MX", valor = 16, id = 1 };
                model_cotizaciones_datos cotizacionDatos = new model_cotizaciones_datos();
                string monedaTienda = (uc_moneda.FindControl("ddl_moneda") as DropDownList).SelectedValue;
                decimal tipoCambio = operacionesConfiguraciones.obtenerTipoDeCambio();
                cotizacionDatos.nombre_cotizacion = nombreCotizacion;
                cotizacionDatos.mod_asesor = usuarios.modoAsesorCotizacion();
                cotizacionDatos.id_cliente = usuario.idSAP;
                // cotizacionDatos.telefono = telefono;
                cotizacionDatos.usuario_cliente = usuario.email;
                cotizacionDatos.cliente_nombre = usuario.nombre;
                cotizacionDatos.cliente_apellido_paterno = usuario.apellido_paterno;
                cotizacionDatos.cliente_apellido_materno = usuario.apellido_materno;
                cotizacionDatos.email = usuario.email;
                cotizacionDatos.activo = 1;

                int vigencia = 1;
                // Vigencia en MXN = 1 día, USD = 30 días
                if (monedaTienda == "USD") vigencia = 30;
                cotizacionDatos.vigencia = vigencia;

                cotizaciones crear = new cotizaciones();
                crear.monedaCotizacion = monedaTienda;
                crear.fechaTipoDeCambio = utilidad_fechas.obtenerCentral();
                crear.tipoDeCambio = tipoCambio;

                string resultado = crear.crearCotizacionDeCarrito(usuario, impuestos, cotizacionDatos);

                string strDireccionEnvio = "No establecida aún.";
                if (resultado != null)
                {





                    if (chk_cotizacion_sin_envio.Checked)
                    {
                        string metodoEnvio = "En Tienda";
                        decimal envio = 0;

                        cotizaciones.actualizarEnvio(envio, metodoEnvio, resultado);
                    }
                    else
                    {
                        cotizaciones.establecerEstatusCalculo_Costo_Envio(true, resultado);


                        cotizaciones.actualizarEnvio(0, "Estándar", resultado);


                        direcciones_envio direccionEnvio = direcciones_envio_EF.ObtenerPredeterminada(usuario.id);

                        if (direccionEnvio != null)
                        {
                            cotizaciones.AgregarDireccionEnvioACotizacion(resultado, direccionEnvio);

                            strDireccionEnvio = $"Calle: {direccionEnvio.calle}, número: {direccionEnvio.numero}, colonia: {direccionEnvio.colonia}, " +
                   $"{direccionEnvio.delegacion_municipio}, " +
                   $"{direccionEnvio.ciudad}, {direccionEnvio.estado}, C.P.: {direccionEnvio.codigo_postal} ";

                        }
                        var ProductosCotización = CotizacionesEF.ObtenerProductosWithData(resultado);
                        var ListProductosEnvio = new List<ProductoEnvioCalculoModel>();

                        foreach (var ProductoCotizacion in ProductosCotización)
                        {
                            var p = new ProductoEnvioCalculoModel();
                            p.Numero_Parte = ProductoCotizacion.datos.numero_parte;
                            p.Tipo = 1;
                            p.Cantidad = ProductoCotizacion.productos.cantidad;
                            p.PesoKg = ProductoCotizacion.datos.peso;
                            p.Largo = ProductoCotizacion.datos.profundidad;
                            p.Ancho = ProductoCotizacion.datos.ancho;
                            p.Alto = ProductoCotizacion.datos.alto;
                            p.RotacionHorizontal = ProductoCotizacion.datos.RotacionHorizontal;
                            p.RotacionVertical = ProductoCotizacion.datos.RotacionVertical;
                            p.DisponibleParaEnvioGratuito = ProductoCotizacion.datos.disponibleEnvio;
                            ListProductosEnvio.Add(p);
                        }


                        var ValidarRegla = new EnviosIncomReglas(resultado, "cotizacion", ListProductosEnvio);

                        if (ValidarRegla.Resultado.result == false)
                        {

                            ValidarCalculoEnvioOperacion validar = new ValidarCalculoEnvioOperacion(resultado, "cotizacion");

                            if (validar.OperacionValida == false)
                            {

                            }
                        }

                    }

                    cotizaciones obtener = new cotizaciones();

                    DataTable operacion = obtener.obtenerCotizacionDatos_min(resultado);

                    NotiflixJS.Message(this, NotiflixJS.MessageType.success, "Cotización creada con éxito");
                    //materializeCSS.crear_toast(this, "Cotización creada con éxito", true);

                    string id_operacion_encritado = seguridad.Encriptar(operacion.Rows[0]["id"].ToString());
                    string nombre_cotizacion = operacion.Rows[0]["nombre_cotizacion"].ToString();
                    string numero_operacion = operacion.Rows[0]["numero_operacion"].ToString();
                    string url_cotizacion = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/usuario/mi-cuenta/cotizaciones/visualizar/" + id_operacion_encritado;
                    // Necesario para redirección
                    string script = @"   setTimeout(function () { window.location.replace('" + url_cotizacion + "')}, 3500);";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "redirección", script, true);

                    //content_msg_exito_operacion.Visible = true;
                    //lt_tipo_operacion.Text = "Cotización";
                    cargarProductoAsync();

                    // INICIO - Envio de email
                    if (usuarioLogin.tipo_de_usuario == "cliente")
                    {

                        DateTime fechaSolicitud = utilidad_fechas.obtenerCentral();
                        string asunto = "Cotización Cliente - Nombre cotización: " + cotizacionDatos.nombre_cotizacion + " " + cotizacionDatos.usuario_cliente + " ";
                        string mensaje = string.Empty;
                        string filePathHTML = "/email_templates/operaciones/cotizaciones/cotizacion_cliente.html";
                        DataTable operacionProductos = cotizacionesProductos.obtenerProductos(resultado);

                        string productosEmailHTML = string.Empty;
                        foreach (DataRow r in operacionProductos.Rows)
                        {
                            productosEmailHTML += "<strong>" + r["numero_parte"].ToString() + "</strong> - " + r["descripcion"].ToString() + "<br>" +
                                "Cantidad: " + decimal.Round((decimal.Parse(r["cantidad"].ToString())), 2) + " x $" +

                            decimal.Parse(r["precio_unitario"].ToString(), myNumberFormatInfo) + crear.monedaCotizacion + "<br>";
                        }

                        Dictionary<string, string> datosDiccRemplazo = new Dictionary<string, string>();
                        datosDiccRemplazo.Add("{dominio}", Request.Url.GetLeftPart(UriPartial.Authority));
                        datosDiccRemplazo.Add("{tipo_operacion}", "Cotización");
                        datosDiccRemplazo.Add("{FechaCotizacion}", cotizacionDatos.fecha_creacion.ToString("f"));
                        datosDiccRemplazo.Add("{nombre_operacion}", cotizacionDatos.nombre_cotizacion);
                        datosDiccRemplazo.Add("{usuario_email}", usuarioLogin.email);
                        datosDiccRemplazo.Add("{nombre}", cotizacionDatos.cliente_nombre);
                        datosDiccRemplazo.Add("{numero_operacion}", numero_operacion);
                        datosDiccRemplazo.Add("{url_operacion}", url_cotizacion);
                        datosDiccRemplazo.Add("{productos}", productosEmailHTML);

                        datosDiccRemplazo.Add("{InfoDeContacto}", InfoDeContacto);
                        datosDiccRemplazo.Add("{DireccionEnvio}", strDireccionEnvio);
                        mensaje = archivosManejador.reemplazarEnArchivo(filePathHTML, datosDiccRemplazo);




                        //  emailTienda email = new emailTienda(asunto, "cmiranda@incom.mx", mensaje, "retail@incom.mx");

                        emailTienda email = new emailTienda(asunto, $"telemarketing@incom.mx, fgarcia@incom.mx, {usuarioLogin.email}", mensaje, "retail@incom.mx");
                        email.general();
                        NotiflixJS.Message(this, NotiflixJS.MessageType.info, email.resultadoMensaje);
                        //materializeCSS.crear_toast(this, email.resultadoMensaje, email.resultado);
                    }
                    else
                    {


                    }

                }
            }
        }
        else
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.warning, "El campo teléfono es obligatorio / Excede la cantidad de caracteres");
            //materializeCSS.crear_toast(this, "El campo teléfono es obligatorio / Excede la cantidad de caracteres", false);
        }
    }


    protected void btn_comprar_Click(object sender, EventArgs e)
    {
        usuarios usuario = usuarios.modoAsesor();
        usuarios usuarioLogin = usuarios.userLogin();

        if (carrito.obtenerCantidadProductos(usuario.email) < 1)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.info, "Aún no tienes productos en tu carrito");
            //materializeCSS.crear_toast(this, "Aún no tienes productos en tu carrito", false);
            return;
        }
        //string nombrePedido = txtNombrePedido.Text;

        //if (String.IsNullOrEmpty(nombrePedido) || nombrePedido.Length < 3)
        //{
        //    nombrePedido = utilidad_fechas.AAAMMDD();
        //}
        string nombrePedido = utilidad_fechas.DDMMAAff();

        model_impuestos impuestos = new model_impuestos() { nombre = "MX", valor = 16, id = 1 };
        model_pedidos_datos pedidoDatos = new model_pedidos_datos();
        string monedaTienda = (uc_moneda.FindControl("ddl_moneda") as DropDownList).SelectedValue;
        decimal tipoCambio = operacionesConfiguraciones.obtenerTipoDeCambio();
        pedidoDatos.nombre_pedido = nombrePedido;
        pedidoDatos.mod_asesor = usuarios.modoAsesorCotizacion();
        pedidoDatos.id_cliente = usuario.idSAP;
        pedidoDatos.usuario_cliente = usuario.email;
        pedidoDatos.telefono = usuario.telefono;
        pedidoDatos.celular = usuario.celular;
        pedidoDatos.cliente_nombre = usuario.nombre;
        pedidoDatos.cliente_apellido_paterno = usuario.apellido_paterno;
        pedidoDatos.cliente_apellido_materno = usuario.apellido_materno;
        pedidoDatos.email = usuario.email;
        pedidoDatos.activo = 1;
        pedidoDatos.preCotizacion = 0;

        pedidosDatos crear = new pedidosDatos();
        crear.monedaPedido = monedaTienda;
        crear.fechaTipoDeCambio = utilidad_fechas.obtenerCentral();
        crear.tipoDeCambio = tipoCambio;

        var resultado = crear.crearPedidoDeCarrito(usuario, impuestos, pedidoDatos);

        direcciones_envio direccionEnvio = null;

        if (resultado != null)
        {
            pedidosDatos.actualizarEnvio(0, "Estándar", resultado);
            direccionEnvio = direcciones_envio_EF.ObtenerPredeterminada(usuario.id);
            string strDireccionEnvio = "";

            if (direccionEnvio != null)
            {
                pedidos_direccionEnvio pedidoDireccionEnvio = new pedidos_direccionEnvio();
                pedidoDireccionEnvio.numero_operacion = resultado;
                pedidoDireccionEnvio.idDireccionEnvio = direccionEnvio.id;
                pedidoDireccionEnvio.calle = direccionEnvio.calle;
                pedidoDireccionEnvio.numero = direccionEnvio.numero;
                pedidoDireccionEnvio.numero_interior = direccionEnvio.numero_interior;
                pedidoDireccionEnvio.colonia = direccionEnvio.colonia;
                pedidoDireccionEnvio.delegacion_municipio = direccionEnvio.delegacion_municipio;
                pedidoDireccionEnvio.codigo_postal = direccionEnvio.codigo_postal;
                pedidoDireccionEnvio.ciudad = direccionEnvio.ciudad;
                pedidoDireccionEnvio.estado = direccionEnvio.estado;
                pedidoDireccionEnvio.pais = direccionEnvio.pais;
                pedidoDireccionEnvio.referencias = direccionEnvio.referencias;

                var result = PedidosEF.GuardarDireccionEnvio(resultado, pedidoDireccionEnvio);
                pedidosDatos.establecerEstatusCalculo_Costo_Envio(true, resultado);
                var ProductosPedidos = PedidosEF.ObtenerProductosWithData(resultado);
                var ListProductosEnvio = new List<ProductoEnvioCalculoModel>();

                foreach (var ProductoPedido in ProductosPedidos)
                {
                    var p = new ProductoEnvioCalculoModel();
                    p.Numero_Parte = ProductoPedido.datos.numero_parte;
                    p.Tipo = 1;
                    p.Cantidad = ProductoPedido.productos.cantidad;
                    p.PesoKg = ProductoPedido.datos.peso;
                    p.Largo = ProductoPedido.datos.profundidad;
                    p.Ancho = ProductoPedido.datos.ancho;
                    p.Alto = ProductoPedido.datos.alto;
                    p.RotacionHorizontal = ProductoPedido.datos.RotacionHorizontal;
                    p.RotacionVertical = ProductoPedido.datos.RotacionVertical;
                    p.DisponibleParaEnvioGratuito = ProductoPedido.datos.disponibleEnvio;
                    ListProductosEnvio.Add(p);
                }

                var ValidarRegla = new EnviosIncomReglas(resultado, "pedido", ListProductosEnvio);

                if (ValidarRegla.Resultado.result == false)
                {
                    ValidarCalculoEnvioOperacion validar = new ValidarCalculoEnvioOperacion(resultado, "pedido");
                }

                strDireccionEnvio = $"Calle: {direccionEnvio.calle}, número: {direccionEnvio.numero}, colonia: {direccionEnvio.colonia}, " + $"{direccionEnvio.delegacion_municipio}, " + $"{direccionEnvio.ciudad}, {direccionEnvio.estado}, C.P.: {direccionEnvio.codigo_postal} ";
            }
            else
            {
                pedidosDatos.actualizarEnvio(0, "Ninguno", resultado, "No haz establecido un método/dirección de envío");
                strDireccionEnvio = "No establecida aún.";
                bool resultadoTotales = pedidosProductos.actualizarTotalStatic(resultado);
            }

            NotiflixJS.Message(this, NotiflixJS.MessageType.success, "Pedido creado con éxito");
            NotiflixJS.Message(this, NotiflixJS.MessageType.info, "Redireccionando...");
            //materializeCSS.crear_toast(this, "Pedido creado con éxito", true);

            pedidosDatos obtener = new pedidosDatos();
            DataTable operacion = obtener.obtenerPedidoDatos(resultado);

            string id_operacion_encritado = seguridad.Encriptar(operacion.Rows[0]["id"].ToString());
            string nombre_pedido = operacion.Rows[0]["nombre_pedido"].ToString();
            string numero_operacion = operacion.Rows[0]["numero_operacion"].ToString();
            decimal? MontoTotalProductos = PedidosEF.ObtenerMontoTotalProductos(numero_operacion);
            string InfoDeContacto = $"{usuario.nombre} {usuario.apellido_paterno} {usuario.apellido_materno}, {usuario.email} " +
            $"Tel: {usuario.telefono}, Cel: {usuario.celular}";
            string strDireccionFacturacion = "";
            string UrlDireccionEnvio = GetRouteUrl("cliente-pedido-envio", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion", id_operacion_encritado }
                    });
            string UrlDireccionFacturacion = GetRouteUrl("cliente-pedido-facturacion", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion", id_operacion_encritado }
                    });

            string UrlInfoDeContacto = GetRouteUrl("cliente-pedido-datos", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion", id_operacion_encritado }
                    });

            if (string.IsNullOrWhiteSpace(strDireccionFacturacion)) strDireccionFacturacion = "No establecido aún.";

            string redirectUrl = GetRouteUrl("cliente-pedido-resumen", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion", id_operacion_encritado }
                    });

            //content_msg_exito_operacion.Visible = true;
            //lt_tipo_operacion.Text = "Pedido";
            // Necesario para redirección
            string script = @"setTimeout(function () { window.location.replace('" + redirectUrl + "')}, 1000);";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "redirección", script, true);

            //// INICIO - Envio de email
            //if (usuarioLogin.tipo_de_usuario == "cliente")
            //{
            //    DateTime fechaSolicitud = utilidad_fechas.obtenerCentral();
            //    string asunto = "Incom.mx, Gracias por tu compra: " + pedidoDatos.nombre_pedido + " " + pedidoDatos.usuario_cliente + " ";
            //    string mensaje = string.Empty;
            //    string filePathHTML = "/email_templates/operaciones/pedidos/pedido_cliente.html";

            //    DataTable operacionProductos = pedidosProductos.obtenerProductos(resultado);

            //    string productosEmailHTML = "";
            //    foreach (DataRow r in operacionProductos.Rows)
            //    {
            //        var precio_unitario = "$" + decimal.Parse(r["precio_unitario"].ToString()).ToString("#,#.##", myNumberFormatInfo) + " " + monedaTienda;
            //        var cantidad = decimal.Round(decimal.Parse(r["cantidad"].ToString()), 2);
            //        var unidad = r["unidad"].ToString();

            //        productosEmailHTML += "<strong>" + r["numero_parte"].ToString() + "</strong> - " + r["descripcion"].ToString() + "<br>" +
            //           "Cantidad: <strong>" + cantidad + " x " + precio_unitario + "</strong><hr><br>";
            //    }

            //    Dictionary<string, string> datosDiccRemplazo = new Dictionary<string, string>();

            //    string dominio = Request.Url.GetLeftPart(UriPartial.Authority);

            //    datosDiccRemplazo.Add("{dominio}", dominio);
            //    datosDiccRemplazo.Add("{tipo_operacion}", "Pedido");
            //    datosDiccRemplazo.Add("{nombre_cotizacion}", "Pedido");
            //    datosDiccRemplazo.Add("{usuario_email}", usuarioLogin.email);
            //    datosDiccRemplazo.Add("{nombre}", pedidoDatos.cliente_nombre);
            //    datosDiccRemplazo.Add("{numero_operacion}", numero_operacion);
            //    datosDiccRemplazo.Add("{nombre_operacion}", nombre_pedido);
            //    datosDiccRemplazo.Add("{url_operacion}", dominio + redirectUrl);
            //    datosDiccRemplazo.Add("{productos}", productosEmailHTML);
            //    datosDiccRemplazo.Add("{FechaPedido}", pedidoDatos.fecha_creacion.ToString());
            //    datosDiccRemplazo.Add("{DireccionEnvio}", strDireccionEnvio);
            //    datosDiccRemplazo.Add("{DireccionFacturacion}", strDireccionFacturacion);
            //    datosDiccRemplazo.Add("{InfoDeContacto}", InfoDeContacto);
            //    datosDiccRemplazo.Add("{UrlDireccionEnvio}", dominio + UrlDireccionEnvio);
            //    datosDiccRemplazo.Add("{UrlDireccionFacturacion}", dominio + UrlDireccionFacturacion);
            //    datosDiccRemplazo.Add("{UrlInfoDeContacto}", dominio + UrlInfoDeContacto);
            //    datosDiccRemplazo.Add("{MontoTotalProductos}", decimal.Parse(MontoTotalProductos.ToString()).ToString("#,#.##", myNumberFormatInfo));

            //    mensaje = archivosManejador.reemplazarEnArchivo(filePathHTML, datosDiccRemplazo);

            //    //  emailTienda email = new emailTienda(asunto, $"cmiranda@incom.mx, {usuarioLogin.email}", mensaje, "retail@incom.mx");               

            //    emailTienda email = new emailTienda(asunto, $"iamado@2rent.mx, ralbert@incom.mx, tpavia@incom.mx, jhernandez@incom.mx, pjuarez@incom.mx, fgarcia@incom.mx, {usuarioLogin.email}", mensaje, "retail@incom.mx");

            //    email.general();

            //    NotiflixJS.Message(this, NotiflixJS.MessageType.info, email.resultadoMensaje);
            //    //materializeCSS.crear_toast(this, email.resultadoMensaje, email.resultado);

            //    // FIN - Envio de email
            //    cargarProductoAsync();
            //} 
            //else
            //{
            //    NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "No se puede crear el pedido. Intentar más tarde.");
            //    //    materializeCSS.crear_toast(this, "Error al crear pedido ", false);
            //}
        }
    }
    protected void btn_guardarPlantilla_Click(object sender, EventArgs e)
    {
        bool resultado = cotizacionesPlantillas.guardarPlantillaDeCarrito();

        if (resultado)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.success, "Plantilla guardada con éxito");
            //materializeCSS.crear_toast(this, "Plantilla guardada con éxito", true);
        }
        else if (resultado)
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.warning, "Error al guardar la plantilla");
            //materializeCSS.crear_toast(this, "Error al guardar plantilla", true);
        }

        //protected async void txt_telefono_fijo_TextChangedAsync(object sender, EventArgs e)
        //{
        //    var EmailUserLogin = hf_UserLogin.Value;
        //    var Telefono = textTools.lineSimple(txt_telefono_fijo.Text);
        //    var result = await UsuariosDatosEF.GuardarTelefonoFijo(EmailUserLogin, Telefono);

        //    if (result.result)
        //    {
        //        lbl_text_result_saved_tel.ForeColor = System.Drawing.Color.Green;
        //        lbl_text_result_saved_tel.Text = result.message;
        //        usuarios.establecer_DatosClienteStatic(EmailUserLogin);
        //    }
        //    else
        //    {
        //        lbl_text_result_saved_tel.ForeColor = System.Drawing.Color.Red;
        //        lbl_text_result_saved_tel.Text = result.message;
        //    }
        //    mostrarModalContacto();
        //}

        //protected async void txt_celular_TextChangedAsync(object sender, EventArgs e)
        //{
        //    var EmailUserLogin = hf_UserLogin.Value;
        //    var Celular = textTools.lineSimple(txt_celular.Text);
        //    var result = await UsuariosDatosEF.GuardarCelular(EmailUserLogin, Celular);

        //    if (result.result)
        //    {
        //        lbl_text_result_saved_tel.ForeColor = System.Drawing.Color.Green;
        //        lbl_text_result_saved_tel.Text = result.message;
        //        usuarios.establecer_DatosClienteStatic(EmailUserLogin);
        //    }
        //    else
        //    {
        //        lbl_text_result_saved_tel.ForeColor = System.Drawing.Color.Red;
        //        lbl_text_result_saved_tel.Text = result.message;
        //    }
        //    mostrarModalContacto();
        //}
    }
}
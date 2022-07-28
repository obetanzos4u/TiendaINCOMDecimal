using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userControls_pedido_visualizar : System.Web.UI.UserControl {

    public string idSQL {
        get { return this.hf_id.Value; }
        set { this.hf_id.Value = value; }
        }
    public string nombre_pedido{ get { return this.lbl_nombre_pedido.Text; } }
    public string numero_operacion { get { return this.lbl_numero_operacion.Text; } }
    public string cliente_nombre { get { return this.lbl_cliente_nombre.Text; } }
    public string email { get { return this.lbl_email.Text; } }

    public string html {
        get {
            // Inicio de código que extrae la tabla >> de productos renderizada y la guarda en un string
            var outputBuffer = new StringBuilder();
            using (var writer = new HtmlTextWriter(new StringWriter(outputBuffer))) {
                content_operacion_center.RenderControl(writer);
                }
            // Fin de código << que extrae la tabla de productos
            return outputBuffer.ToString();
            }

        }
    public string header {
        get {
            // Inicio de código que extrae la tabla >> de productos renderizada y la guarda en un string
            var outputBuffer = new StringBuilder();
            using (var writer = new HtmlTextWriter(new StringWriter(outputBuffer))) {
                header_infoBasica.RenderControl(writer);
                }
            // Fin de código << que extrae la tabla de productos
            return "<table style='font-family: Helvetica,Arial,sans-serif; background: white; '>" + outputBuffer.ToString() + "</table>";
            }

        }

    NumberFormatInfo myNumberFormatInfo = new CultureInfo("es-MX", true).NumberFormat;

    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack) {
            cargarDatos();
            logo_header.Attributes.Add("src", Request.Url.GetLeftPart(UriPartial.Authority) + "/img/webUI/company_logo_wide.png");
            } else {

            }
        }


    public string obtenerEmail() {

        // Inicio de código que extrae la tabla >> de productos renderizada y la guarda en un string
        var outputBuffer = new StringBuilder();
        using (var writer = new HtmlTextWriter(new StringWriter(outputBuffer))) {
            content_operacion.RenderControl(writer);
            }
        // Fin de código << que extrae la tabla de productos

        return outputBuffer.ToString();
        }

    void cargarDatos() {

        DataTable dtPedidoDatos = new DataTable();

        pedidosDatos obtener = new pedidosDatos();
        dtPedidoDatos = obtener.obtenerPedidoDatosMax(int.Parse(idSQL));
        string numero_operacion = dtPedidoDatos.Rows[0]["numero_operacion"].ToString();
        string creada_por = dtPedidoDatos.Rows[0]["creada_por"].ToString();
        string usuario_cliente = dtPedidoDatos.Rows[0]["usuario_cliente"].ToString();
        int mod_asesor = int.Parse(dtPedidoDatos.Rows[0]["mod_asesor"].ToString());
       
        // INICIO - de validación de privacidad
        bool permisoVisualizar = privacidadAsesores.validarOperacion(usuario_cliente);

        if (permisoVisualizar)
        {
        }
        else
        {
            Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority), true);
        }
        // FIN - de validación de privacidad

        lbl_cliente_nombre.Text = dtPedidoDatos.Rows[0]["cliente_nombre"].ToString();
        lbl_numero_operacion.Text = dtPedidoDatos.Rows[0]["numero_operacion"].ToString();
        lbl_nombre_pedido.Text = dtPedidoDatos.Rows[0]["nombre_pedido"].ToString();
        lbl_fecha_creacion.Text = dtPedidoDatos.Rows[0]["fecha_creacion"].ToString();
        lbl_metodoEnvio.Text = dtPedidoDatos.Rows[0]["metodoEnvio"].ToString();
        lbl_comentarios.Text = dtPedidoDatos.Rows[0]["comentarios"].ToString();


        lbl_nombre.Text = dtPedidoDatos.Rows[0]["cliente_nombre"].ToString();
        lbl_apellido_paterno.Text = dtPedidoDatos.Rows[0]["cliente_apellido_paterno"].ToString();
        lbl_apellido_materno.Text = dtPedidoDatos.Rows[0]["cliente_apellido_materno"].ToString();

        lbl_email.Text = dtPedidoDatos.Rows[0]["email"].ToString();
        lbl_telefono.Text = dtPedidoDatos.Rows[0]["telefono"].ToString();
        lbl_celular.Text = dtPedidoDatos.Rows[0]["celular"].ToString();

        model_direccionesEnvio PedidoEnvio;
        PedidoEnvio = obtener.obtenerPedidoDireccionEnvio(numero_operacion);

        if (PedidoEnvio != null) {
            lbl_envio_calle.Text = PedidoEnvio.calle;
            lbl_envio_numero.Text = PedidoEnvio.numero + ",";
            lbl_envio_colonia.Text = PedidoEnvio.colonia + ",";
            lbl_envio_delegacion_municipio.Text = PedidoEnvio.delegacion_municipio + ",";

            lbl_envio_estado.Text = PedidoEnvio.estado;
            lbl_envio_codigo_postal.Text = PedidoEnvio.codigo_postal + ".";
            lbl_envio_pais.Text = PedidoEnvio.pais;
            }



        model_direccionesFacturacion pedidoFacturacion;
        pedidoFacturacion = obtener.obtenerPedidoDireccionFacturacion(numero_operacion);

        if (pedidoFacturacion != null) {
            lbl_facturacion_razon_social.Text = pedidoFacturacion.razon_social;
            lbl_facturacion_rfc.Text = pedidoFacturacion.rfc;
            lbl_facturacion_calle.Text = pedidoFacturacion.calle;
            llbl_facturacion_numero.Text = pedidoFacturacion.numero + ",";
            lbl_facturacion_colonia.Text = pedidoFacturacion.colonia + ",";
            lbl_facturacion_delegacion_municipio.Text = pedidoFacturacion.delegacion_municipio + ",";
            lbl_facturacion_estado.Text = pedidoFacturacion.estado;
            lbl_facturacion_codigo_postal.Text = pedidoFacturacion.codigo_postal + ".";
            lbl_facturacion_pais.Text = pedidoFacturacion.pais;
            }

        // Inicio datos numéricos



        decimal subtotal = decimal.Parse(dtPedidoDatos.Rows[0]["subtotal"].ToString());
        decimal envio = decimal.Parse(dtPedidoDatos.Rows[0]["envio"].ToString());
        decimal impuestos = decimal.Parse(dtPedidoDatos.Rows[0]["impuestos"].ToString());
        decimal total = decimal.Parse(dtPedidoDatos.Rows[0]["total"].ToString());
        decimal tipo_cambio = Math.Round( decimal.Parse(dtPedidoDatos.Rows[0]["tipo_cambio"].ToString()),5);//~tc
        string monedaOP = dtPedidoDatos.Rows[0]["monedaPedido"].ToString();

        lbl_subtotal.Text = subtotal.ToString("C2", myNumberFormatInfo) + " " + monedaOP;
        lbl_envio.Text = envio.ToString("C2", myNumberFormatInfo) + " " + monedaOP;
        lbl_impuestos.Text = impuestos.ToString("C2", myNumberFormatInfo) + " " + monedaOP;
        lbl_total.Text = total.ToString("C2", myNumberFormatInfo) + " " + monedaOP;
        lbl_tipo_cambio.Text = tipo_cambio.ToString("C5", myNumberFormatInfo);
        // Fin datos numéricos


        // Inicio Datos Productos
        DataTable dtPedidoProductos;

        dtPedidoProductos = pedidosProductos.obtenerProductos(numero_operacion);
        lv_productos.DataSource = dtPedidoProductos;
        lv_productos.DataBind();

        // Fin Datos Productos

        cargarInfoFooter(creada_por, usuario_cliente, mod_asesor);
        }


    protected void cargarInfoFooter(string creada_por, string usuario_cliente, int modoAsesor) {

        usuarios creada_porUsuario = usuarios.recuperar_DatosUsuario(creada_por);
        
        usuarios operacionCliente = usuarios.recuperar_DatosUsuario(usuario_cliente);


        lbl_operacion_creada_por_email.Text = creada_porUsuario.email;
        lbl_operacion_creada_por_nombre.Text = creada_porUsuario.nombre;
        lbl_emitida_por.Text = HttpContext.Current.User.Identity.Name;

        // Si la modalidad de asesor esta disponible mostramos ciertas imagenes, si no no.
        if (modoAsesor == 1) {
            img_asesor.ImageUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/img/asesores/" + creada_porUsuario.email.Replace("@incom.mx", "") + ".jpg"; 
            } else if (modoAsesor == 0) {
            img_asesor.ImageUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/img/asesores/incom.gif";
            content_img_asesor.Visible = false;
            }

        }

    protected void lv_productos_ItemDataBound(object sender, ListViewItemEventArgs e) {

        System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;

       

        string numero_parte = rowView["numero_parte"].ToString();
        string titulo = rowView["titulo"].ToString();
        string descripcion = rowView["descripcion"].ToString();
        string marca = rowView["marca"].ToString();
        string unidad = rowView["unidad"].ToString();
        decimal precio_unitario = decimal.Parse(rowView["precio_unitario"].ToString());
        decimal cantidad = decimal.Parse(rowView["cantidad"].ToString());
        decimal precio_total = decimal.Parse(rowView["precio_total"].ToString());
        string monedaOP = rowView["monedaPedido"].ToString();
        
        string[] imagenes = rowView["imagenes"].ToString().Split(',');
      


        Image img_producto = (Image)e.Item.FindControl("img_producto");
        img_producto.ImageUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/" + 
            archivosManejador.imagenProducto(imagenes[0]);

        Label lbl_lvProductos_numero_parte = (Label)e.Item.FindControl("lbl_lvProductos_numero_parte");
        HyperLink link_lvProductos_linkPDF = (HyperLink)e.Item.FindControl("link_lvProductos_linkPDF");
        Image img_lvProductos_linkPDF = (Image)e.Item.FindControl("img_lvProductos_linkPDF");
        Label lbl_lvProductos_titulo = (Label)e.Item.FindControl("lbl_lvProductos_titulo");
        Label lbl_lvProductos_descripcion = (Label)e.Item.FindControl("lbl_lvProductos_descripcion");
        Label lbl_lvProductos_cantidad = (Label)e.Item.FindControl("lbl_lvProductos_cantidad");
        Label lbl_lvProductos_unidad = (Label)e.Item.FindControl("lbl_lvProductos_unidad");
        Label lbl_lvProductos_precio_unitario = (Label)e.Item.FindControl("lbl_lvProductos_precio_unitario");
        Label lbl_lvProductos_precio_total = (Label)e.Item.FindControl("lbl_lvProductos_precio_total");
        HyperLink link_producto = (HyperLink)e.Item.FindControl("link_producto");


        
        lbl_lvProductos_numero_parte.Text = numero_parte;
        lbl_lvProductos_descripcion.Text = descripcion;
        lbl_lvProductos_cantidad.Text = Math.Round(cantidad, 1).ToString();
        lbl_lvProductos_unidad.Text = unidad;
        lbl_lvProductos_precio_unitario.Text = precio_unitario.ToString("C2", myNumberFormatInfo) + " " + monedaOP; 
        lbl_lvProductos_precio_total.Text = precio_total.ToString("C2", myNumberFormatInfo) + " " + monedaOP;

        // Inicio de mostrar/ocultar el archivo pdf
        string pdfFileName = rowView["pdf"].ToString().Split(',')[0];
        if (!string.IsNullOrWhiteSpace(pdfFileName)) {

           

            if(archivosManejador.validarExistenciaPDF(pdfFileName) == true) {
                // Si   existe el archivo
                img_lvProductos_linkPDF.ImageUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/img/webUI/email/pdf-icon.png";
                link_lvProductos_linkPDF.NavigateUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/documents/pdf/" + pdfFileName;
                link_lvProductos_linkPDF.Target = "_blank";
                } else {
                // Si no existe el archivo
                img_lvProductos_linkPDF.Visible = false;
                link_lvProductos_linkPDF.Visible = false;
                }

            } else {
            img_lvProductos_linkPDF.Visible = false;
            }
 


        link_producto.NavigateUrl = GetRouteUrl("productos", new System.Web.Routing.RouteValueDictionary {
                        { "numero_parte", textTools.limpiarURL_NumeroParte(numero_parte) },
                        {"marca", marca},
                        { "productoNombre",  textTools.limpiarURL(titulo) }
                    });

        link_producto.NavigateUrl = Request.Url.GetLeftPart(UriPartial.Authority) + link_producto.NavigateUrl;
        }
    }

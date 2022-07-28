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

public partial class userControls_cotizacion_visualizar : System.Web.UI.UserControl {

    public string idSQL {
        get { return this.hf_id.Value; }
        set { this.hf_id.Value = value; }
    }
    public string nombre_cotizacion { get { return this.lbl_nombre_cotizacion.Text; } }
    public string numero_operacion { get { return this.lbl_numero_operacion.Text; } }
    public string cliente_nombre { get { return this.lbl_cliente_nombre.Text; } }
    public string email { get { return this.lbl_email.Text; } }
    public string usuario_email { get { return this.hf_usuario_email.Value; } }
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
            Cargar_CotizacionTerminos();
            //   logo_header.Attributes.Add("src", Request.Url.GetLeftPart(UriPartial.Authority) + "/img/webUI/company_logo_wide.jpg");
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

    protected void Cargar_CotizacionTerminos() {


        List<cotizaciones_terminos> terminos = cotizaciones_terminos.ObtenerTerminosCotizacion(lbl_numero_operacion.Text);

        if (terminos != null && terminos.Count >= 1) {

            foreach (cotizaciones_terminos term in terminos) {

                switch (term.idTipoTermino) {
                    case cotizaciones_terminos.tipoTermino.TiempoDeEntrega: cargarTiempoEntrega(term); break;
                    case cotizaciones_terminos.tipoTermino.FormaDePago: cargarFormaDePago(term); break;
                    case cotizaciones_terminos.tipoTermino.Entrega: cargarEntrega(term); break;
                }

            }
            
        } else {
            lbl_TerminoTiempoEntrega.Text = "<strong>Tiempo de entrega:</strong> Aún no establecido";
            lbl_TerminoEntrega.Text = "<strong>Forma de pago:</strong> Aún no establecido";
            lbl_TerminoFormaDePago.Text = "<strong>Entrega:</strong> Aún no establecido";
        }

    }

    protected void cargarTiempoEntrega(cotizaciones_terminos term) {

        if (term.termino.Contains(",")) {
            string[] tiempoValores = term.termino.Split(',');
            int indice = int.Parse(tiempoValores[0]);
            string termino;

            switch (indice) {
                case 0:
                    termino = tiempoValores[1];
                    lbl_TerminoTiempoEntrega.Text = "<strong>Tiempo de entrega:</strong> " + termino;
                    break;
                case 1:
                    string numeroFechaTiempoEntrega = tiempoValores[1];
                    string TipoFecha = tiempoValores[2];

                    termino = numeroFechaTiempoEntrega + " " + TipoFecha;
                    lbl_TerminoTiempoEntrega.Text = "<strong>Tiempo de entrega:</strong> " + termino;

                    break;
                case 2:
                    termino = tiempoValores[1];
                    lbl_TerminoTiempoEntrega.Text = "<strong>Tiempo de entrega:</strong> " + termino;


                    break;
            }




         

           
        } else {
            lbl_TerminoTiempoEntrega.Text = "Aún no establecido";
        }

    }

    protected void cargarFormaDePago(cotizaciones_terminos term) {

        if (term.termino.Contains(",")) {
            string[] terminoValores = term.termino.Split(',');
            int indice = int.Parse(terminoValores[0]);
            string termino = terminoValores[1];
            lbl_TerminoFormaDePago.Text = "<strong>Forma de pago:</strong> " + termino;
        } else {
            lbl_TerminoFormaDePago.Text = "Aún no establecido";
        }


    }

    protected void cargarEntrega(cotizaciones_terminos term) {
        if (term.termino.Contains(",")) {
            string[] terminoValores = term.termino.Split(',');

            int indice = int.Parse(terminoValores[0]);
            string termino = terminoValores[1];
            lbl_TerminoEntrega.Text = "<strong>Entrega:</strong> " + termino;
        } else {
            lbl_TerminoEntrega.Text = "Aún no establecido";
        }

    }

    void cargarDatos() {

        DataTable dtCotizacionDatos = new DataTable();

        cotizaciones obtener = new cotizaciones();
        dtCotizacionDatos = obtener.obtenerCotizacionDatosMax(int.Parse(idSQL));
        string numero_operacion = dtCotizacionDatos.Rows[0]["numero_operacion"].ToString();
        string creada_por = dtCotizacionDatos.Rows[0]["creada_por"].ToString();
        string usuario_cliente = dtCotizacionDatos.Rows[0]["usuario_cliente"].ToString();
        int mod_asesor = int.Parse(dtCotizacionDatos.Rows[0]["mod_asesor"].ToString());

        // INICIO - de validación de privacidad
        bool permisoVisualizar = privacidadAsesores.validarOperacion(usuario_cliente);

        if (permisoVisualizar) {
        } else {
            Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority), true);
        }
        // FIN - de validación de privacidad

        lbl_cliente_nombre.Text = dtCotizacionDatos.Rows[0]["cliente_nombre"].ToString();
        lbl_numero_operacion.Text = dtCotizacionDatos.Rows[0]["numero_operacion"].ToString();
        lbl_nombre_cotizacion.Text = dtCotizacionDatos.Rows[0]["nombre_cotizacion"].ToString();
        lbl_fecha_creacion.Text = dtCotizacionDatos.Rows[0]["fecha_creacion"].ToString();
        lbl_vigencia.Text = dtCotizacionDatos.Rows[0]["vigencia"].ToString();
        hf_usuario_email.Value = usuario_cliente;


        lbl_metodoEnvio.Text = dtCotizacionDatos.Rows[0]["metodoEnvio"].ToString();
        if (lbl_metodoEnvio.Text == "Ninguno") {
            lbl_metodoEnvio.Text = "Ninguno (Acordar con asesor el costo y método de este)";
            lbl_metodoEnvio.ForeColor = System.Drawing.Color.Red;
        }
        lbl_comentarios.Text = dtCotizacionDatos.Rows[0]["comentarios"].ToString().Replace("\n", "<br>");

        lbl_nombre.Text = dtCotizacionDatos.Rows[0]["cliente_nombre"].ToString();
        lbl_apellido_paterno.Text = dtCotizacionDatos.Rows[0]["cliente_apellido_paterno"].ToString();
        lbl_apellido_materno.Text = dtCotizacionDatos.Rows[0]["cliente_apellido_materno"].ToString();

        lbl_email.Text = dtCotizacionDatos.Rows[0]["email"].ToString();
        lbl_telefono.Text = dtCotizacionDatos.Rows[0]["telefono"].ToString();
        lbl_celular.Text = dtCotizacionDatos.Rows[0]["celular"].ToString();
       


        model_direccionesEnvio cotizacionEnvio;
        cotizacionEnvio = obtener.obtenerCotizacionDireccionEnvio(numero_operacion);

        if (cotizacionEnvio != null) {
            lbl_envio_calle.Text = cotizacionEnvio.calle;
            lbl_envio_numero.Text = cotizacionEnvio.numero + ",";
            lbl_envio_colonia.Text = cotizacionEnvio.colonia + ",";
            lbl_envio_delegacion_municipio.Text = cotizacionEnvio.delegacion_municipio + ",";

            lbl_envio_estado.Text = cotizacionEnvio.estado;
            lbl_envio_codigo_postal.Text = cotizacionEnvio.codigo_postal + ".";
            lbl_envio_pais.Text = cotizacionEnvio.pais;
        }



        model_direccionesFacturacion cotizacionFacturacion;
        cotizacionFacturacion = obtener.obtenerCotizacionDireccionFacturacion(numero_operacion);

        if (cotizacionFacturacion != null) {
            lbl_facturacion_razon_social.Text = cotizacionFacturacion.razon_social;
            lbl_facturacion_rfc.Text = cotizacionFacturacion.rfc;
            lbl_facturacion_calle.Text = cotizacionFacturacion.calle;
            llbl_facturacion_numero.Text = cotizacionFacturacion.numero + ",";
            lbl_facturacion_colonia.Text = cotizacionFacturacion.colonia + ",";
            lbl_facturacion_delegacion_municipio.Text = cotizacionFacturacion.delegacion_municipio + ",";
            lbl_facturacion_estado.Text = cotizacionFacturacion.estado;
            lbl_facturacion_codigo_postal.Text = cotizacionFacturacion.codigo_postal + ".";
            lbl_facturacion_pais.Text = cotizacionFacturacion.pais;
        }

        // Inicio datos numéricos



        decimal subtotal = decimal.Parse(dtCotizacionDatos.Rows[0]["subtotal"].ToString());
        decimal envio = decimal.Parse(dtCotizacionDatos.Rows[0]["envio"].ToString());
        decimal impuestos = decimal.Parse(dtCotizacionDatos.Rows[0]["impuestos"].ToString());
        decimal total = decimal.Parse(dtCotizacionDatos.Rows[0]["total"].ToString());
        decimal tipo_cambio = Math.Round(decimal.Parse(dtCotizacionDatos.Rows[0]["tipo_cambio"].ToString()), 5);//~tc

        string monedaOP = dtCotizacionDatos.Rows[0]["monedaCotizacion"].ToString();
        string descuento = dtCotizacionDatos.Rows[0]["descuento"].ToString();
        string descuento_porcentaje = dtCotizacionDatos.Rows[0]["descuento_porcentaje"].ToString();


        

        // Validamos que haya un descuento aplicado pues si este se encuentra, es necesario mostrarlo, de lo contrario no, pues ocupario mayor espacio en el desglose
        if (!string.IsNullOrWhiteSpace(descuento) || !string.IsNullOrWhiteSpace(descuento_porcentaje)) {

            decimal d_descuento = decimal.Parse(descuento);

            lbl_subtotalSinDescuento.Text = Math.Round(subtotal + d_descuento, 2).ToString("C2", myNumberFormatInfo) + " " + monedaOP;


            lbl_descuento_porcentaje.Text = Math.Round(decimal.Parse(descuento_porcentaje), 1).ToString();

            } else {

            content_subtotalSinDescuento.Visible = false;
            content_descuento_porcentaje.Visible = false;

            }



        lbl_subtotal.Text = subtotal.ToString("C2", myNumberFormatInfo) + " " + monedaOP;
        lbl_envio.Text = envio.ToString("C2", myNumberFormatInfo) + " " + monedaOP;
        lbl_impuestos.Text = impuestos.ToString("C2", myNumberFormatInfo) + " " + monedaOP;
        lbl_total.Text = total.ToString("C2", myNumberFormatInfo) + " " + monedaOP;
        lbl_tipo_cambio.Text = operacionesConfiguraciones.obtenerTipoDeCambio().ToString("C5", myNumberFormatInfo);
        // Fin datos numéricos


        // Inicio Datos Productos
        DataTable dtCotizacionProductos;

        dtCotizacionProductos = cotizacionesProductos.obtenerProductos(numero_operacion);
        lv_productos.DataSource = dtCotizacionProductos;
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
        string monedaOP = rowView["monedaCotizacion"].ToString();
        string tipo = rowView["tipo"].ToString();

        string[] imagenes = rowView["imagenes"].ToString().Split(',');
        int activo = int.Parse(rowView["activo"].ToString());



        Image img_producto = (Image)e.Item.FindControl("img_producto");
        Label lbl_lvProductos_numero_parte = (Label)e.Item.FindControl("lbl_lvProductos_numero_parte");
        HyperLink link_lvProductos_linkPDF = (HyperLink)e.Item.FindControl("link_lvProductos_linkPDF");
        Image img_lvProductos_linkPDF = (Image)e.Item.FindControl("img_lvProductos_linkPDF");
        Label lbl_lvProductos_titulo = (Label)e.Item.FindControl("lbl_lvProductos_titulo");
        Label lbl_lvProductos_descripcion = (Label)e.Item.FindControl("lbl_lvProductos_descripcion");
        Label lbl_lvProductos_alternativo = (Label)e.Item.FindControl("lbl_lvProductos_alternativo");
        Label lbl_lvProductos_cantidad = (Label)e.Item.FindControl("lbl_lvProductos_cantidad");
        Label lbl_lvProductos_unidad = (Label)e.Item.FindControl("lbl_lvProductos_unidad");
        Label lbl_lvProductos_precio_unitario = (Label)e.Item.FindControl("lbl_lvProductos_precio_unitario");
        Label lbl_lvProductos_precio_total = (Label)e.Item.FindControl("lbl_lvProductos_precio_total");
        HyperLink link_producto = (HyperLink)e.Item.FindControl("link_producto");

        link_producto.NavigateUrl = GetRouteUrl("productos", new System.Web.Routing.RouteValueDictionary {
                        { "numero_parte", textTools.limpiarURL_NumeroParte(numero_parte) },
                        {"marca", marca},
                        { "productoNombre",  textTools.limpiarURL(titulo) }
                    });

        link_producto.NavigateUrl = Request.Url.GetLeftPart(UriPartial.Authority) + link_producto.NavigateUrl;
        #if true
        if (tipo == "1") {
            img_producto.ImageUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/" + archivosManejador.imagenProducto(imagenes[0]);


            // INICIO de mostrar/ocultar el archivo pdf
            string pdfFileName = rowView["pdf"].ToString().Split(',')[0];
            if (!string.IsNullOrWhiteSpace(pdfFileName)) {

                if (archivosManejador.validarExistenciaPDF(pdfFileName) == true) {
                    // Si   existe el archivo
                    img_lvProductos_linkPDF.ImageUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/img/webUI/email/pdf-icon.png";
                    link_lvProductos_linkPDF.NavigateUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/documents/pdf/" + pdfFileName;
                    link_lvProductos_linkPDF.Target = "_blank";
                }
                else {
                    // Si no existe el archivo
                    img_lvProductos_linkPDF.Visible = false;
                    link_lvProductos_linkPDF.Visible = false;
                }
            }
            else {
                img_lvProductos_linkPDF.Visible = false;
            }
            // FIN de mostrar/ocultar el archivo pdf
        }
#endif
        else {
            if (!string.IsNullOrWhiteSpace(imagenes[0]))
            {
                img_producto.ImageUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/" + archivosManejador.imagenProducto(imagenes[0]);
            }
            else
            {

              
                img_producto.ImageUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/" + archivosManejador.imagenProductoPersonalizado(numero_parte + ".jpg");
            }
          


            if (archivosManejador.validarExistenciaPDFProductoPersonalizado(numero_parte+".pdf") == true) {
                // Si   existe el archivo
                img_lvProductos_linkPDF.ImageUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/img/webUI/email/pdf-icon.png";
                link_lvProductos_linkPDF.NavigateUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/documents/pdf/personalizado/" + numero_parte + ".pdf";
                link_lvProductos_linkPDF.Target = "_blank";

                link_producto.NavigateUrl = link_lvProductos_linkPDF.NavigateUrl;
            }
            else {
                // Si no existe el archivo
                img_lvProductos_linkPDF.Visible = false;
                link_lvProductos_linkPDF.Visible = false;
            }

        }

        // 20210608 CM - Muestra jpg para poder generar correctamente el pdf ya que no soporta el incrustado .webp
        img_producto.ImageUrl = img_producto.ImageUrl.Replace(".webp", ".jpg");

     


        lbl_lvProductos_numero_parte.Text = numero_parte;
        lbl_lvProductos_titulo.Text = titulo;
        lbl_lvProductos_descripcion.Text = descripcion;
        lbl_lvProductos_cantidad.Text = Math.Round(cantidad, 1).ToString();
        lbl_lvProductos_unidad.Text = unidad;
        lbl_lvProductos_precio_unitario.Text = precio_unitario.ToString("C2", myNumberFormatInfo) + " " + monedaOP;
        lbl_lvProductos_precio_total.Text = precio_total.ToString("C2", myNumberFormatInfo) + " " + monedaOP;

            if (rowView["alternativo"].ToString() != "") {


                int? alternativo = int.Parse(rowView["alternativo"].ToString());
                if (alternativo != null) {
                    if (alternativo == 1) {
                        lbl_lvProductos_alternativo.ForeColor = System.Drawing.Color.Red;
                        lbl_lvProductos_alternativo.Text = "* Este producto es alternativo a otro o sugerencia por tu asesor.";
                    } else if (alternativo == 0) lbl_lvProductos_alternativo.Text = "";
                }
            }


        if (activo == 0) {
                lbl_lvProductos_numero_parte.Text = "<span style=\"color:red; \">[NO DISPONIBLE PARA VENTA]</span> " + lbl_lvProductos_numero_parte.Text;
            }


        }

         
    
}


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class usuario_cotizaciones : System.Web.UI.Page
{
  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            usuarios userLogin = usuarios.userLogin();
            if (userLogin.tipo_de_usuario != "usuario") {
                Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority) + "/usuario/mi-cuenta/mi-cuenta.aspx");
            }
            Page.Title = "Cotizaciones";
            ddl_ordenTipo.SelectedValue = Request.QueryString["ordenTipo"];
            ddl_ordenBy.SelectedValue = Request.QueryString["ordenBy"];
            ddl_usuarios.SelectedValue = Request.QueryString["usuario"];
            ddl_periodo.SelectedValue = Request.QueryString["periodo"];
            txt_search.Text = Request.QueryString["search"];
            cargarDDLASesores();
            cargarInfo();
         
        }
      

        // INICIO - Sección para evitar que se coticene ellos solitos
        string tipoUsuarioLogin = usuarios.userLogin().tipo_de_usuario;
        string emailUsuarioLogin = usuarios.userLogin().email;
        string emailClienteAsesor = usuarios.modoAsesor().email;


        if (tipoUsuarioLogin =="usuario") {
            btn_crear_cotizacion_en_blanco.Enabled = false;
            btn_crear_cotizacion_en_blanco.Visible = false;
            }
        if(usuarios.modoAsesorActivado() == 1 && emailUsuarioLogin != emailClienteAsesor) {
            btn_crear_cotizacion_en_blanco.Enabled = true;
            btn_crear_cotizacion_en_blanco.Visible = true;
            }
        // FIN - Sección para evitar que se coticene ellos solitos

        }
    protected void orden(object sender, EventArgs e) {

        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
        nameValues.Set("ordenTipo", ddl_ordenTipo.SelectedValue);
        nameValues.Set("ordenBy", ddl_ordenBy.SelectedValue);
        nameValues.Set("usuario", ddl_usuarios.SelectedValue);
        nameValues.Set("search", txt_search.Text);
        nameValues.Set("periodo", ddl_periodo.Text);
        string url = Request.Url.AbsolutePath;

        Type type = sender.GetType();
        if (sender.GetType().Name == "TextBox") {

            if ((sender as TextBox).ID == "txt_search") {
                nameValues.Set("PageId", "1");
                }
            }

        Response.Redirect(url + "?" + nameValues); // ToString() is called implicitly
        }
    protected void cargarDDLASesores() {
     DataTable usuariosIncom =  usuarios.recuperar_DatosUsuariosMin("usuario");

        DataColumn dcNombre = new DataColumn("nombre_completo", Type.GetType("System.String"));
        dcNombre.Expression = "nombre + ' ' + apellido_paterno";
       
        usuariosIncom.Columns.Add(dcNombre);

        ddl_usuarios.DataSource = usuariosIncom;
        ddl_usuarios.DataTextField = "nombre_completo"; ;
        ddl_usuarios.DataValueField = "email";
        ddl_usuarios.DataBind();
    }
    protected void cargarInfo( )
    {

        
        cotizaciones obtener = new cotizaciones();
        DataTable dtCotizaciones = null;

        
        int year = int.Parse(ddl_periodo.SelectedValue);

        if (year  > 6)
        {
            DateTime desde = new DateTime(year, 1, 1, 0, 0, 0, 0);
            DateTime hasta = new DateTime(year, 12, 31, 23, 59, 59, 999);

            dtCotizaciones = obtener.obtenerCotizacionesAsesor_min(ddl_usuarios.SelectedValue,desde,hasta,null);
        } else
        {
            DateTime desde = utilidad_fechas.obtenerCentral().AddMonths(-6);
            DateTime hasta = utilidad_fechas.obtenerCentral();
            dtCotizaciones = obtener.obtenerCotizacionesAsesor_min(ddl_usuarios.SelectedValue, desde, hasta, null);
        }
 

        // INICIO - Motor de búsqueda
        if (!string.IsNullOrWhiteSpace(txt_search.Text)) {
            string find = txt_search.Text;

            string query = "nombre_cotizacion Like '%" + find + "%' OR  numero_operacion Like '%" + find + "%'";
            dtCotizaciones = buscador.filtrar(this, dtCotizaciones, query, find);
            }
        // FIN - Motor de búsqueda 

        // INICIO - ORDEN de visualización
        DataView dv = dtCotizaciones.DefaultView;
        dv.Sort = ddl_ordenBy.SelectedValue + " " + ddl_ordenTipo.SelectedValue;
        dtCotizaciones = dv.ToTable();
        // FIN - ORDEN de visualización


        lvCotizaciones.DataSource = dtCotizaciones;

        lvCotizaciones.DataBind();

    }

    protected void cambiarNombreCotizacion(object sender, EventArgs e) {


        TextBox txt_nombre_cotizacion = (TextBox)sender;
        ListViewItem lvItem = (ListViewItem)txt_nombre_cotizacion.NamingContainer;


        // Buscamos un objeto dentro del contenedor
        HiddenField hf_id_cotizacionSQL = (HiddenField)lvItem.FindControl("hf_id_cotizacionSQL");

        string nombre_cotizacion = txt_nombre_cotizacion.Text;
        cotizaciones editar = new cotizaciones();

        if (validarCampos.cotizacion_nombreCotizacion(nombre_cotizacion, this)) {
            if (editar.editarNombrecotizacion(int.Parse(hf_id_cotizacionSQL.Value), nombre_cotizacion) != null) {

                materializeCSS.crear_toast(this, "Nombre actualizado con éxito.", true);
                } else {
                materializeCSS.crear_toast(this, "Error al actualizar nombre.", false);
                }
               


            } else {
            materializeCSS.crear_toast(this, "No cumple con la longitud requerida", false);
            }
           

        }

    protected void lvCotizaciones_ItemDataBound(object sender, ListViewItemEventArgs e) {

        userControls_uc_estatus_cotizacion CotizacionEstatus = e.Item.FindControl("CotizacionEstatus") as userControls_uc_estatus_cotizacion;

        Label lbl_numero_operacion = e.Item.FindControl("lbl_numero_operacion") as Label;
        HyperLink btn_editarCotizacion = e.Item.FindControl("btn_editarCotizacion") as HyperLink;
        HyperLink btn_editarProductos = e.Item.FindControl("btn_editarProductos") as HyperLink;
        HyperLink btn_visualizar = e.Item.FindControl("btn_visualizar") as HyperLink;
        HiddenField hf_id_cotizacionSQL = e.Item.FindControl("hf_id_cotizacionSQL") as HiddenField;

        btn_visualizar.NavigateUrl = GetRouteUrl("usuario-cotizacion-visualizar", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion", seguridad.Encriptar(hf_id_cotizacionSQL.Value) },

                     });

        btn_editarCotizacion.NavigateUrl = GetRouteUrl("usuario-cotizacion-datos", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion", seguridad.Encriptar(hf_id_cotizacionSQL.Value) }
                     });

        btn_editarProductos.NavigateUrl = GetRouteUrl("usuario-cotizacion-productos", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",seguridad.Encriptar(hf_id_cotizacionSQL.Value) }
                    });

        ListView lvProductos = e.Item.FindControl("lvProductos") as ListView;
        cotizaciones obtener = new cotizaciones();

        DataTable dt_productosCotizacionMin = obtener.obtenerProductosCotizacion_min(lbl_numero_operacion.Text, 5);

        if (dt_productosCotizacionMin != null) {
            lvProductos.DataSource = dt_productosCotizacionMin;
            lvProductos.DataBind();
        }

        System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;

        if (!IsPostBack) {
            CotizacionEstatus.numero_operacion = lbl_numero_operacion.Text;
            string idEstatus = rowView["idEstatus"].ToString();
            if (usuarios.userLogin().tipo_de_usuario == "usuario") {
                CotizacionEstatus.obtenerEstatusCotizacion();
                CotizacionEstatus.EstablecerIDSeleccionado(idEstatus);

                CotizacionEstatus.ocultarEncabezado();
            }
        }
    }



    protected void lvProductos_ItemDataBound(object sender, ListViewItemEventArgs e) {

        Literal lt_descripcion = e.Item.FindControl("lt_descripcion") as Literal;

        System.Data.DataRowView rowView = e.Item.DataItem as System.Data.DataRowView;

        string descripcion = rowView["descripcion"].ToString();

        if (descripcion.Length > 40) {
            descripcion = descripcion.Substring(0, 40);
            }
        lt_descripcion.Text = descripcion;

        }
    protected void btn_crear_cotizacion_en_blanco_Click(object sender, EventArgs e) {

        usuarios usuario = usuarios.modoAsesor();

        string nombreCotizacion = txtNombreCotizacionEnBlanco.Text;

        if (String.IsNullOrEmpty(nombreCotizacion) || nombreCotizacion.Length < 3) {
            nombreCotizacion = utilidad_fechas.AAAMMDD() + " Sin nombre";

            }


        model_cotizaciones_datos cotizacionDatos = new model_cotizaciones_datos();
        string monedaTienda = ddl_moneda.SelectedValue;
        decimal tipoCambio = operacionesConfiguraciones.obtenerTipoDeCambio();
        cotizacionDatos.nombre_cotizacion = nombreCotizacion;
        cotizacionDatos.mod_asesor = usuarios.modoAsesorCotizacion();
        cotizacionDatos.id_cliente = usuario.idSAP;
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



        string resultadoNumeroOperacion = crear.crearCotizacionVacia(usuario, cotizacionDatos);


        if (resultadoNumeroOperacion != null) {
            cotizaciones obtener = new cotizaciones();
            DataTable operacion = obtener.obtenerCotizacionDatos_min(resultadoNumeroOperacion);

            materializeCSS.crear_toast(this, "Cotización creada con éxito", true);

            cargarInfo();



            } else {

            materializeCSS.crear_toast(this, "Error al crear cotización ", false);

            }


        }





    protected void ddl_usuarios_SelectedIndexChanged(object sender, EventArgs e) {
        orden(sender, e);
    }

    protected void btn_activarModoAsesor_Click(object sender, EventArgs e) {


        LinkButton btn_activarModoAsesor = (LinkButton)sender;
        ListViewItem lv_item = (ListViewItem)btn_activarModoAsesor.NamingContainer;
        Label lbl_usuario_cliente = (Label)lv_item.FindControl("lbl_usuario_cliente");

        usuarios establecer = new usuarios();
        string email = lbl_usuario_cliente.Text;
        establecer.establecer_DatosCliente(email);


        bool modalidadAsesorActivada = privacidadAsesores.modalidadAsesor();
        System.Web.HttpContext.Current.Session["modoAsesor"] = true;
        Session["pedido_edit_idSQL"] = null;
        Session["pedido_edit"] = null;
        Session["cotizacion_edit_idSQL"] = null;
        Session["cotizacion_edit"] = null;
        Response.Redirect(Request.Url.AbsoluteUri);
    }
}
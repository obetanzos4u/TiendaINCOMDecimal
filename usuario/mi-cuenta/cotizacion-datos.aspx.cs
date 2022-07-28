using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class usuario_cotizacionDatos : System.Web.UI.Page
{
    contactos contactos = new contactos();
    direccionesEnvio envios = new direccionesEnvio();
    direccionesFacturacion facturacion = new direccionesFacturacion();
    protected void Page_Load(object sender, EventArgs e) {
      
        if (!IsPostBack) {

          
            cargasDatosOperacion();
            ddlContacto_cargarInfo();
            ddlEnvios_cargarInfo();
            ddlFacturacion_cargarInfo();
            adminContactos.HeaderInfoContacto = false;

            cotizacion_terminos.numero_operacion = lt_numero_operacion.Text;
            CotizacionEstatus.numero_operacion = lt_numero_operacion.Text;
            cotizacion_terminos.cargarDatos();
            if (usuarios.modoAsesorActivadoBool()) {
                cotizacion_terminos.visibleInfo = true; 
            } else {
                cotizacion_terminos.visibleInfo = false;
            }
          
            rellenarCamposCotizacionDireccionEnvio();
            rellenarCamposCotizacionDireccionFacturacion();
           

            } else {

            if (ddl_pais_envio.SelectedText == "México") {
                cont_ddl_estado_envio.Visible = true;
                cont_txt_estado_envio.Visible = false;
                } else {
                cont_ddl_estado_envio.Visible = false;
                cont_txt_estado_envio.Visible = true;
                }

            if (ddl_pais_facturacion.SelectedText == "México") {
                cont_ddl_estado_facturacion.Visible = true;
                cont_txt_estado_facturacion.Visible = false;
                } else {
                cont_ddl_estado_facturacion.Visible = false;
                cont_txt_estado_facturacion.Visible = true;
                }
            }

        adminEnvios.T += new EventHandler(activarModal);

        adminContactos.eventUpdateContact += new EventHandler(activarModal);
        adminContactos.eventEditContact += new EventHandler(activarModal);

        adminFacturacion.eventUpdate += new EventHandler(activarModal);
        adminFacturacion.eventEdit += new EventHandler(activarModal);

        ddl_pais_envio.SelectedIndexChanged += new uc_ddl_paises.ChangedIndex(activarTabEnvios);
         


        }
   
    ///<summary>
    /// Abre la modal para administrar: Contactos || Envíos || Facturación
    ///</summary>
    protected void btn_modalShow(object sender, EventArgs e) {

        LinkButton btn = (LinkButton)sender;

        switch (btn.ID) {

            case "btn_adminContactos":
            adminContactos.Visible = true;
            adminEnvios.Visible = false;
            adminFacturacion.Visible = false;

            break;
            case "btn_adminEnvios":
            adminContactos.Visible = false;
            adminEnvios.Visible = true;
            adminFacturacion.Visible = false;

            break;

            case "btn_adminFacturacion":
            adminContactos.Visible = false;
            adminEnvios.Visible = false;
            adminFacturacion.Visible = true;

            break;


            }

        upModal.Update();
        activarModal(sender, e);
        }

    protected void activarModal(object sender, EventArgs e) {

        ScriptManager.RegisterStartupScript(this, typeof(Page), "con", "$(document).ready(function () {     $('#modalAdmin').modal('open');  });", true);
        ddlContacto_cargarInfo();
        }
    ///<summary>
    /// Al recargar la página activa la tab envíos (#2);
    ///</summary>
  
    protected void cargasDatosOperacion() {
        if (Page.RouteData.Values["id_operacion"]!= null) {

            string route_id_operacion = Page.RouteData.Values["id_operacion"].ToString();
            route_id_operacion = seguridad.DesEncriptar(route_id_operacion);

            cotizaciones obtener = new cotizaciones();
            DataTable dt_CotizacionDatos = obtener.obtenerCotizacionDatosMax(int.Parse(route_id_operacion));

            if (dt_CotizacionDatos != null && dt_CotizacionDatos.Rows.Count >= 1) {
                string numero_operacion = dt_CotizacionDatos.Rows[0]["numero_operacion"].ToString();
                string usuario_cliente = dt_CotizacionDatos.Rows[0]["usuario_cliente"].ToString();
                 txt_comentarios.Text = dt_CotizacionDatos.Rows[0]["comentarios"].ToString();

                Page.Title = "Datos de cotización #" + numero_operacion;

                if (privacidadAsesores.validarOperacion(usuario_cliente) == false) {
                    content_cotizacion_datos.Visible = false; content_mensaje.Visible = true;
                    return;

                    } ;
             
                int id_operacion = int.Parse(dt_CotizacionDatos.Rows[0]["id"].ToString());
           
                int vigencia = int.Parse(dt_CotizacionDatos.Rows[0]["vigencia"].ToString());
                int conversionPedido = int.Parse(dt_CotizacionDatos.Rows[0]["conversionPedido"].ToString());
                string idEstatus =  dt_CotizacionDatos.Rows[0]["idEstatus"].ToString();

                if (usuarios.userLogin().tipo_de_usuario == "usuario") {
                    CotizacionEstatus.obtenerEstatusCotizacion();
                    CotizacionEstatus.EstablecerIDSeleccionado(idEstatus);
                } else {
                    CotizacionEstatus.ocultarEncabezado();
                }
           


                string tipo_cotizacion = null;

                if (!dt_CotizacionDatos.Rows[0].IsNull("tipo_cotizacion")) {
                    tipo_cotizacion = dt_CotizacionDatos.Rows[0]["tipo_cotizacion"].ToString();
                }
                    lt_numero_operacion.Text = numero_operacion;
                lt_nombre_cotizacion.Text = dt_CotizacionDatos.Rows[0]["nombre_cotizacion"].ToString();
                lt_cliente_nombre.Text = dt_CotizacionDatos.Rows[0]["cliente_nombre"].ToString() + " " + dt_CotizacionDatos.Rows[0]["cliente_apellido_paterno"].ToString();

                hf_id_operacion.Value = id_operacion.ToString();
                rellenarCamposContacto(dt_CotizacionDatos);




                string metodoEnvio = dt_CotizacionDatos.Rows[0]["metodoEnvio"].ToString();
                if (metodoEnvio == "En Tienda") chkEnvioEnTienda.Checked = true;
                else chkEnvioEnTienda.Checked = false;



                hl_editarDatos.NavigateUrl = GetRouteUrl("usuario-cotizacion-datos", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",seguridad.Encriptar(id_operacion.ToString()) }
                    });
                hl_editarProductos.NavigateUrl = GetRouteUrl("usuario-cotizacion-productos", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",seguridad.Encriptar(id_operacion.ToString()) }
                    });

                // INICIO - Sección que valida su vigencia y si esta ya tiene un pedido
                DateTime fechaOperacion = DateTime.Parse(dt_CotizacionDatos.Rows[0]["fecha_creacion"].ToString());
                if (utilidad_fechas.calcularDiferenciaDias(fechaOperacion) >= vigencia || conversionPedido == 1) {
                    deshabilitarCampos(contentDatos);
                    }
                // FIN - Sección que valida su vigencia y si esta ya tiene un pedido

                if (string.IsNullOrWhiteSpace(tipo_cotizacion)) {

                // Desactivado a petición de TLMKT para mostrar en sección productos 20190430
                // ScriptManager.RegisterStartupScript(this, typeof(Page), "con", "$(document).ready(function () {   $('.modal_tipo_cotizacion').modal('open');  });", true);
                } else {
                    ddl_tipo_cotizacion.SelectedValue = tipo_cotizacion;
                }
            }


          

            }
        
           

        }
    protected void deshabilitarCampos(Control control) {

        foreach(Control c in control.Controls) {

            if(!c.HasControls()) {

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


    protected void rellenarCamposContacto(DataTable dt_datos) {
        txt_nombre.Text = dt_datos.Rows[0]["cliente_nombre"].ToString();
        txt_apellido_paterno.Text = dt_datos.Rows[0]["cliente_apellido_paterno"].ToString();
        txt_apellido_materno.Text = dt_datos.Rows[0]["cliente_apellido_materno"].ToString();
        txt_email.Text = dt_datos.Rows[0]["email"].ToString();
        txt_telefono.Text = dt_datos.Rows[0]["telefono"].ToString();
        txt_celular.Text = dt_datos.Rows[0]["celular"].ToString();

        }
    protected void ddlContacto_cargarInfo()
    {
        usuarios usuario = usuarios.modoAsesor();

        DataTable  dtContactos = contactos.obtenerContactos(usuario.id);

        dtContactos.Columns.Add("nombreApEmail", typeof(string), "nombre + ' ' + apellido_paterno + ', ' + email");

        ddl_contactosUser.DataSource = dtContactos;
        ddl_contactosUser.DataTextField = "nombreApEmail";
        ddl_contactosUser.DataValueField = "id";
        ddl_contactosUser.DataBind();

        string nombreSelección = "Selecciona";
        if(dtContactos.Rows.Count ==0) {
            nombreSelección = "No tienes contactos aún "; }
        
        ddl_contactosUser.Items.Insert(0, new ListItem(nombreSelección));
        ddl_contactosUser.Items[0].Value = "";
        ddl_contactosUser.Items[0].Attributes.Add("disabled ", "");
        ddl_contactosUser.Items[0].Attributes.Add("selected ", "");
        up_cotizacionDatos.Update();
        }
    protected void btn_guardarDatosContacto_Click(object sender, EventArgs e) {

        // Iniciamos la validación de campos
        bool nombres = validarCampos.nombres(txt_nombre, txt_apellido_paterno, txt_apellido_paterno, this);
        bool email = validarCampos.email(txt_email.Text, this);
        bool telefonos = validarCampos.telefonos(txt_telefono, txt_celular, this);

        usuarios datosUsuario = usuarios.modoAsesor();

        // Si todo es correcto, procedemos a actualizar
        if (nombres && email && telefonos) {
            string numero_operacion = lt_numero_operacion.Text;
        cotizaciones actualizar = new cotizaciones();
        model_cotizaciones_datos datosContacto = new model_cotizaciones_datos();
        datosContacto.cliente_nombre = txt_nombre.Text;
        datosContacto.cliente_apellido_paterno = txt_apellido_paterno.Text;
        datosContacto.cliente_apellido_materno = txt_apellido_materno.Text;
        datosContacto.email = txt_email.Text ;
        datosContacto.telefono = txt_telefono.Text;
        datosContacto.celular = txt_celular.Text;

        string resultado = actualizar.actualizarCotizacion_datosContacto(numero_operacion, datosContacto);

        if(resultado != null) {
            materializeCSS.crear_toast(this, "Datos de contacto guardados", true);
            }
        else {
            materializeCSS.crear_toast(this, "Error al guardar datos de contacto", false);
            }
        
            //  Si activo la opcion de crear contacto
            if (chk_guardarNuevoContacto.Checked) {

                contactos contacto = new contactos {
                    id_cliente = datosUsuario.id,
                    email = txt_email.Text,
                    nombre = txt_nombre.Text,
                    apellido_paterno = txt_apellido_paterno.Text,
                    apellido_materno = txt_apellido_materno.Text,
                    telefono = txt_telefono.Text,
                    celular = txt_celular.Text
                    };

                if (contacto.crearContacto() != null) {
                    materializeCSS.crear_toast(this, "Contacto creado con éxito", true);
                    ddlContacto_cargarInfo();

                    } else {
                    materializeCSS.crear_toast(this, "Error al crear contacto", false);
                    }

                }



            }
    
       

        }
    protected void resetDatosContacto() {
        txt_nombre.Text = "";
        txt_apellido_paterno.Text = "";
        txt_apellido_materno.Text = "";
        txt_email.Text = "";
        txt_telefono.Text = "";
        txt_celular.Text = "";
        }
    protected void ddl_contactosUser_SelectedIndexChanged(object sender, EventArgs e) {

        if (ddl_contactosUser.SelectedValue != "") {
            int idSelection = int.Parse(ddl_contactosUser.SelectedValue);
            
            contactos contacto = new contactos();

            contacto = contacto.obtenerContacto(idSelection);

            txt_nombre.Text = contacto.nombre;
            txt_apellido_paterno.Text = contacto.apellido_paterno;
            txt_apellido_materno.Text = contacto.apellido_materno;
            txt_email.Text = contacto.email;
            txt_telefono.Text = contacto.telefono;
            txt_celular.Text = contacto.celular;
            } else 
            //resetDatosContacto();
            
        up_cotizacionDatos.Update();

        }
    protected void btn_cancelarDatosContacto_Click(object sender, EventArgs e) {

        cotizaciones obtener = new cotizaciones();
        DataTable dt_CotizacionDatos = obtener.obtenerCotizacionDatosMax(int.Parse(hf_id_operacion.Value));
        ddl_contactosUser.SelectedIndex = 0;
        rellenarCamposContacto(dt_CotizacionDatos);

        }

    // Envío
    protected async void ddl_direccionesEnvio_SelectedIndexChanged(object sender, EventArgs e) {

        if (ddl_direccionesEnvio.SelectedValue != "")
        {
            int idSelection = int.Parse(ddl_direccionesEnvio.SelectedValue);

            direccionesEnvio direccion = new direccionesEnvio();

            direccion = direccion.obtenerDireccion(idSelection);

            txt_nombre_direccion_envio.Text = direccion.nombre_direccion;
            txt_calle_envio.Text = direccion.calle;
            txt_numero_envio.Text = direccion.numero;
          
            txt_delegacion_municipio_envio.Text = direccion.delegacion_municipio;
            txt_ciudad_envio.Text = direccion.ciudad;
            txt_estado_envio.Text = direccion.codigo_postal;
            txt_codigo_postal_envio.Text = direccion.codigo_postal;
            ddl_pais_envio.SelectedText = direccion.pais;

            if (direccion.pais == "México")
            {
                cont_ddl_estado_envio.Visible = true;
                cont_txt_estado_envio.Visible = false;

                ddl_estado_envio.SelectedText = direccion.estado;
            }
            else
            {
                cont_ddl_estado_envio.Visible = false;
                cont_txt_estado_envio.Visible = true;
                txt_estado_envio.Text = direccion.estado;
            }

            txt_referencias_envio.Text = direccion.referencias;
         




 
            if (!string.IsNullOrWhiteSpace(direccion.codigo_postal))
            {
                json_respuestas result =   await DireccionesServiceCP.GetCodigoPostalAsync(direccion.codigo_postal);


                if (result.result == true)
                {
                    dynamic Result = JsonConvert.DeserializeObject<dynamic>(result.response);

                    txt_delegacion_municipio_envio.Text = Result[0].Municipio;
                    txt_ciudad_envio.Text = Result[0].Ciudad;

                    ddl_estado_envio.SelectedText = Result[0].Estado;
                    ddl_pais_envio.SelectedText = Result[0].Pais;

                    ddl_colonia_envio.Items.Clear();
                    foreach (var t in Result)
                    {

                        string Asentamiento = t.Asentamiento;
                        ddl_colonia_envio.Items.Add(new ListItem(Asentamiento, Asentamiento));
                    }
                    ddl_colonia_envio.DataBind();
                    ddl_colonia_envio.SelectedValue = direccion.colonia;



                    cont_ddl_estado_envio.Visible = true;
                    cont_txt_estado_envio.Visible = false;

                    EnabledCamposEnvio(false);

                    ddl_colonia_envio.Visible = true;
                    txt_colonia_envio.Visible = false;
                }
                else
                {
                    ddl_colonia_envio.Items.Clear();
                    ddl_colonia_envio.Visible = false;
                    txt_colonia_envio.Visible = true;
                    txt_colonia_envio.Text =  direccion.colonia;
                    EnabledCamposEnvio(true);

                    materializeCSS.crear_toast(this, result.message, false);
                }

            }
            else
            {
                ddl_colonia_envio.Items.Clear();
                ddl_colonia_envio.Visible = false;
                txt_colonia_envio.Visible = true;
                EnabledCamposEnvio(true);
            }


        }
        else { 
            ddl_colonia_envio.Items.Clear();
        ddl_colonia_envio.Visible = false;
        txt_colonia_envio.Visible = true;
        txt_colonia_envio.Text= "";
            txt_codigo_postal_envio.Text = "";
            EnabledCamposEnvio(true);
            up_AdminEnvios.Update();
        }
    }
    protected void activarTabEnvios(object sender, EventArgs e) {

        ScriptManager.RegisterStartupScript(this, typeof(Page), "con", "$(document).ready(function () {    var instance = M.Tabs.getInstance($('ul.tabs')); instance.select('test-swipe-2');   });", true);
       // ddlContacto_cargarInfo();
        }
    protected void btn_guardarDireccionEnvio_Click(object sender, EventArgs e) {

        try {
        string estado;
        string numero_operacion = lt_numero_operacion.Text;

        if (ddl_pais_envio.SelectedText == "México") {
            estado = ddl_estado_envio.SelectedText;
            } else {
            estado = txt_estado_envio.Text;
            }

        string colonia = "";
        if (ddl_colonia_envio.Visible)
        {
            colonia = ddl_colonia_envio.SelectedValue;
        }
        else
        {
            colonia = txt_colonia_envio.Text;
        }
        

       direccionesEnvio direccion = new direccionesEnvio {
            nombre_direccion = txt_nombre_direccion_envio.Text,
            calle = txt_calle_envio.Text,
            numero = txt_numero_envio.Text,
            colonia = colonia,
            delegacion_municipio = txt_delegacion_municipio_envio.Text,
                ciudad = txt_ciudad_envio.Text,
            estado = estado,
            codigo_postal = txt_codigo_postal_envio.Text,
            pais = ddl_pais_envio.SelectedText,
            referencias = txt_referencias_envio.Text
            };
        if (!chk_guardarNuevadireccionFacturacion.Checked)
            direccion.nombre_direccion = "Dirección ";

        if (validarCampos.direccionEnvio(direccion, this)) {

            usuarios datosUsuario = usuarios.modoAsesor();

            cotizaciones actualizarEnvio = new cotizaciones();

            
            if (actualizarEnvio.cotizacionDireccionEnvio(numero_operacion, direccion) == true) {
                materializeCSS.crear_toast(this, "Dirección de envío actualizada con éxito", true);
                } else {
                materializeCSS.crear_toast(this, "Error al actualizar dirección de envío", false);
                }

            if (chk_guardarNuevadireccionEnvio.Checked) {
                if (direccion.crearDireccion(datosUsuario.id) != null) {
                    ddlEnvios_cargarInfo();
                    adminEnvios.cargar_DireccionesEnvio();
                    materializeCSS.crear_toast(this, "Dirección de envío creada con éxito", true);
                    } else {
                    materializeCSS.crear_toast(this, "Error al crear dirección de envío", false);
                    }
                }


            }
            up_AdminEnvios.Update();
        }
        catch(Exception ex)
        {
            devNotificaciones.notificacionSimple(ex.ToString());
        }
        }
    ///<summary>
    /// Rellena los campos de dirección de envío provientes de la cotización.
    ///</summary>
    protected async void rellenarCamposCotizacionDireccionEnvio() {

        string numero_operacion = lt_numero_operacion.Text;

        model_direccionesEnvio direccion = new model_direccionesEnvio();
        cotizaciones obtener = new cotizaciones();

        direccion = obtener.obtenerCotizacionDireccionEnvio(numero_operacion);

        if(direccion != null) {
            txt_nombre_direccion_envio.Text = direccion.nombre_direccion;
            txt_calle_envio.Text = direccion.calle;
            txt_numero_envio.Text = direccion.numero;
            txt_colonia_envio.Text = direccion.colonia;
           
            txt_delegacion_municipio_envio.Text = direccion.delegacion_municipio;
            txt_ciudad_envio.Text = direccion.ciudad;

            txt_codigo_postal_envio.Text = direccion.codigo_postal;
            txt_referencias_envio.Text = direccion.referencias;


            if (direccion.pais == "México") {
                ddl_pais_envio.SelectedText = direccion.pais;
                cont_ddl_estado_envio.Visible = true;
                cont_txt_estado_envio.Visible = false;
                ddl_estado_envio.SelectedText = direccion.estado;
                } else {
                ddl_pais_envio.SelectedText = direccion.pais;
                cont_ddl_estado_envio.Visible = false;
                cont_txt_estado_envio.Visible = true;
                txt_estado_envio.Text = direccion.estado;
                }












            // Sección llenado Código Postal
            string cp = textTools.lineSimple(txt_codigo_postal_envio.Text);
            if (!string.IsNullOrWhiteSpace(cp))
            {
                json_respuestas result = await DireccionesServiceCP.GetCodigoPostalAsync(cp);


                if (result.result == true)
                {
                    dynamic Result = JsonConvert.DeserializeObject<dynamic>(result.response);

                    txt_delegacion_municipio_envio.Text = Result[0].Municipio;
                    txt_ciudad_envio.Text = Result[0].Ciudad;

                    ddl_estado_envio.SelectedText = Result[0].Estado;
                    ddl_pais_envio.SelectedText = Result[0].Pais;

                    ddl_colonia_envio.Items.Clear();
                    foreach (var t in Result)
                    {

                        string Asentamiento = t.Asentamiento;
                        ddl_colonia_envio.Items.Add(new ListItem(Asentamiento, Asentamiento));
                    }
                    ddl_colonia_envio.DataBind();


                    ddl_colonia_envio.SelectedValue = txt_colonia_envio.Text;

                    cont_ddl_estado_envio.Visible = true;
                    cont_txt_estado_envio.Visible = false;


 

                    ddl_colonia_envio.Visible = true;
                    txt_colonia_envio.Visible = false;



                    EnabledCamposEnvio(false);
                }
                else
                {
                    EnabledCamposEnvio( true);

                    ddl_colonia_envio.Visible = true;
                    txt_colonia_envio.Visible = false;
                    materializeCSS.crear_toast(this, result.message, false);
                }

            }
            else
            {
                ddl_colonia_envio.Visible = false;
                txt_colonia_envio.Visible = true;
            }
        }
        

        }
    protected void ddlEnvios_cargarInfo() {

        usuarios usuario = usuarios.modoAsesor();

        DataTable dtDireccionesEnvio = envios.obtenerDirecciones(usuario.id);

        dtDireccionesEnvio.Columns.Add("ddlValue", typeof(string), "nombre_direccion + ' ' + calle + ', ' + numero");
        ddl_direccionesEnvio.DataSource = dtDireccionesEnvio;
        ddl_direccionesEnvio.DataTextField = "ddlValue";
        ddl_direccionesEnvio.DataValueField = "id";
        ddl_direccionesEnvio.DataBind();

        string nombreSelección = "Selecciona";
        if (dtDireccionesEnvio.Rows.Count == 0) {
            nombreSelección = "No tienes direcciones de envío aún ";
            }

        ddl_direccionesEnvio.Items.Insert(0, new ListItem(nombreSelección));

        ddl_direccionesEnvio.Items[0].Value = "";
        ddl_direccionesEnvio.Items[0].Attributes.Add("disabled ", "");
        ddl_direccionesEnvio.Items[0].Attributes.Add("selected ", "");
        up_AdminEnvios.Update();
        }
    protected void btn_cancelarDireccionEnvio_Click_Click(object sender, EventArgs e) {

        rellenarCamposCotizacionDireccionEnvio();
        }

    // Facturación
    protected void ddl_direccionesFacturacion_SelectedIndexChanged(object sender, EventArgs e) {

        if (ddl_direccionesFacturacion.SelectedValue != "") {
            int idSelection = int.Parse(ddl_direccionesFacturacion.SelectedValue);

            direccionesFacturacion direccion = new direccionesFacturacion();

            direccion = direccion.obtenerDireccion(idSelection);

            txt_nombre_direccion_facturacion.Text = direccion.nombre_direccion;

            txt_razon_social_facturacion.Text = direccion.razon_social;
            txt_rfc_facturacion.Text = direccion.rfc;

            txt_calle_facturacion.Text = direccion.calle;
            txt_numero_facturacion.Text = direccion.numero;
            txt_colonia_facturacion.Text = direccion.colonia;
            txt_delegacion_municipio_facturacion.Text = direccion.delegacion_municipio;
            txt_estado_facturacion.Text = direccion.codigo_postal;
            txt_codigo_postal_facturacion.Text = direccion.codigo_postal;
            ddl_pais_facturacion.SelectedText = direccion.pais;

            if (direccion.pais == "México") {
                cont_ddl_estado_facturacion.Visible = true;
                cont_txt_estado_facturacion.Visible = false;

                ddl_estado_facturacion.SelectedText = direccion.estado;
                } else {
                cont_ddl_estado_facturacion.Visible = false;
                cont_txt_estado_facturacion.Visible = true;
                txt_estado_facturacion.Text = direccion.estado;
                }

            } else
            //resetDatosContacto();

            up_AdminFacturacion.Update();

        }
    protected void activarTabFacturacion(object sender, EventArgs e) {

        ScriptManager.RegisterStartupScript(this, typeof(Page), "con", "$(document).ready(function () {    var instance = M.Tabs.getInstance($('ul.tabs')); instance.select('test-swipe-3');  });", true);
        }
    protected void btn_guardarDireccionFacturacion_Click(object sender, EventArgs e) {
        string estado;
        string numero_operacion = lt_numero_operacion.Text;

        if (ddl_pais_facturacion.SelectedText == "México") {
            estado = ddl_estado_facturacion.SelectedText;
            } else {
            estado = txt_estado_facturacion.Text;
            }

        direccionesFacturacion direccion = new direccionesFacturacion {
            nombre_direccion = txt_nombre_direccion_facturacion.Text,

            razon_social = txt_razon_social_facturacion.Text,
            rfc = txt_rfc_facturacion.Text,


            calle = txt_calle_facturacion.Text,
            numero = txt_numero_facturacion.Text,
            colonia = txt_colonia_facturacion.Text,
            delegacion_municipio = txt_delegacion_municipio_facturacion.Text,

            estado = estado,
            codigo_postal = txt_codigo_postal_facturacion.Text,
            pais = ddl_pais_facturacion.SelectedText,
            };

        if (!chk_guardarNuevadireccionFacturacion.Checked)
            direccion.nombre_direccion = "Dirección ";
            
                if (validarCampos.direccionFacturacion(direccion, this)) {

            usuarios datosUsuario = usuarios.modoAsesor();

            cotizaciones actualizarFacturacion= new cotizaciones();
            if (actualizarFacturacion.cotizacionDireccionFacturacion(numero_operacion, direccion) == true) {
                materializeCSS.crear_toast(this, "Dirección de facturación actualizada con éxito", true);
                } else {
                materializeCSS.crear_toast(this, "Error al actualizar dirección de facturación", false);
                }

            if (chk_guardarNuevadireccionFacturacion.Checked) {
                if (direccion.crearDireccion(datosUsuario.id) != null) {
                    ddlFacturacion_cargarInfo();
                    adminFacturacion.cargar_Direcciones();
                    materializeCSS.crear_toast(this, "Dirección de facturación creada con éxito", true);
                    } else {
                    materializeCSS.crear_toast(this, "Error al crear dirección de facturación", false);
                    }
                }


            }





        }
    ///<summary>
    /// Rellena los campos de dirección de facturación provientes de la cotización.
    ///</summary>
    protected void rellenarCamposCotizacionDireccionFacturacion() {

        string numero_operacion = lt_numero_operacion.Text;

        model_direccionesFacturacion direccion = new model_direccionesFacturacion();
        cotizaciones obtener = new cotizaciones();

        direccion = obtener.obtenerCotizacionDireccionFacturacion(numero_operacion);

        if (direccion != null) {



            txt_razon_social_facturacion.Text = direccion.razon_social;
            txt_rfc_facturacion.Text = direccion.rfc;

            txt_calle_facturacion.Text = direccion.calle;
            txt_numero_facturacion.Text = direccion.numero;
            txt_colonia_facturacion.Text = direccion.colonia;
            txt_delegacion_municipio_facturacion.Text = direccion.delegacion_municipio;


            txt_codigo_postal_facturacion.Text = direccion.codigo_postal;
            ddl_pais_facturacion.SelectedText = direccion.pais;

            if (direccion.pais == "México") {
                ddl_pais_facturacion.SelectedText = "México";
                cont_ddl_estado_facturacion.Visible = true;
                cont_txt_estado_facturacion.Visible = false;
                ddl_estado_facturacion.SelectedText = direccion.estado;
                } else {
                ddl_pais_facturacion.SelectedText = direccion.pais;
                cont_ddl_estado_facturacion.Visible = false;
                cont_txt_estado_facturacion.Visible = true;
                txt_estado_facturacion.Text = direccion.estado;
                }
            }

        up_AdminFacturacion.Update();
        }
    protected void ddlFacturacion_cargarInfo() {

        usuarios usuario = usuarios.modoAsesor();

        DataTable dtDireccionesFacturacion = facturacion.obtenerDirecciones(usuario.id);

        dtDireccionesFacturacion.Columns.Add("ddlValue", typeof(string), "nombre_direccion + ' ' + calle + ', ' + numero");
        ddl_direccionesFacturacion.DataSource = dtDireccionesFacturacion;
        ddl_direccionesFacturacion.DataTextField = "ddlValue";
        ddl_direccionesFacturacion.DataValueField = "id";
        ddl_direccionesFacturacion.DataBind();

        string nombreSelección = "Selecciona";
        if (dtDireccionesFacturacion.Rows.Count == 0) {
            nombreSelección = "No tienes direcciones de facturación aún ";
            }

        ddl_direccionesFacturacion.Items.Insert(0, new ListItem(nombreSelección));
        ddl_direccionesFacturacion.Items[0].Value = "";
        ddl_direccionesFacturacion.Items[0].Attributes.Add("disabled ", "");
        ddl_direccionesFacturacion.Items[0].Attributes.Add("selected ", "");
        up_AdminFacturacion.Update();
        }
    protected void btn_cancelarDireccionFacturacion_Click(object sender, EventArgs e) {

        rellenarCamposCotizacionDireccionFacturacion();
        }

    protected void txt_comentarios_TextChanged(object sender, EventArgs e) {

        string comentarios = textTools.lineMulti(txt_comentarios.Text);

        if(comentarios.Length < 1500) {

            bool resultado = cotizaciones.actualizarComentarioCotizacion(lt_numero_operacion.Text, comentarios);

            if(resultado == true)
                materializeCSS.crear_toast(up_content_comentarios, "Comentarios guardados con éxito", true);
            else
                materializeCSS.crear_toast(up_content_comentarios, "Error al guardar comentario", false);
            }

         else {

            materializeCSS.crear_toast(up_content_comentarios, "Supera el límite de caracteres",false);
            }
        
        }

    protected void ddl_tipo_cotizacion_SelectedIndexChanged(object sender, EventArgs e) {

        bool resultado = cotizaciones.actualizar_tipo_cotizacion(lt_numero_operacion.Text, ddl_tipo_cotizacion.SelectedValue);

        if (resultado == true) {
            materializeCSS.crear_toast(this, "Campo actualizado con éxito", true);
        } else {
            materializeCSS.crear_toast(this, "Error al actualizar el tipo de cotización", false);
        }
    }




    protected async void txt_codigo_postal_TextChanged(object sender, EventArgs e)
    {
        string cp = textTools.lineSimple(txt_codigo_postal_envio.Text);
        if (!string.IsNullOrWhiteSpace(cp))
        {
            json_respuestas result = await  DireccionesServiceCP.GetCodigoPostalAsync(cp);


            if (result.result == true)
            {
                ddl_direccionesEnvio.SelectedIndex = 0;
                dynamic Result = JsonConvert.DeserializeObject<dynamic>(result.response);

                txt_delegacion_municipio_envio.Text = Result[0].Municipio;
                txt_ciudad_envio.Text = Result[0].Ciudad;

                ddl_estado_envio.SelectedText = Result[0].Estado;
                ddl_pais_envio.SelectedText = Result[0].Pais;

                ddl_colonia_envio.Items.Clear();
                foreach (var t in Result)
                {

                    string Asentamiento = t.Asentamiento;
                    ddl_colonia_envio.Items.Add(new ListItem(Asentamiento, Asentamiento));
                }
                ddl_colonia_envio.DataBind();




                cont_ddl_estado_envio.Visible = true;
                cont_txt_estado_envio.Visible = false;

                ddl_colonia_envio.Visible = true;
                txt_colonia_envio.Visible = false;

                EnabledCamposEnvio(false);
            }
            else
            {
                ddl_colonia_envio.Items.Clear();
                ddl_colonia_envio.Visible = false;
                txt_colonia_envio.Visible = true;
                EnabledCamposEnvio(true);

                materializeCSS.crear_toast(this, result.message, false);
            }

        }
        else
        {
            ddl_colonia_envio.Items.Clear();
            ddl_colonia_envio.Visible = false;
            txt_colonia_envio.Visible = true;
            EnabledCamposEnvio(true);
        }


        activarTabEnvios(sender,e);
    }
    protected void EnabledCamposEnvio(bool status)
    {
        txt_colonia_envio.Enabled = status;
        txt_delegacion_municipio_envio.Enabled = status;
        txt_ciudad_envio.Enabled = status;
        txt_estado_envio.Enabled = status;
        ddl_pais_envio.Enabled = status;
        ddl_estado_envio.Enabled = status;
    }

    protected void chkEnvioEnTienda_CheckedChanged(object sender, EventArgs e)
    {
        string resultado = null;

        if (chkEnvioEnTienda.Checked)
        {
            string metodoEnvio = "En Tienda";
            decimal envio = 0;


              resultado = cotizaciones.actualizarEnvio(envio, metodoEnvio, lt_numero_operacion.Text);
        }
        else
        {
            resultado = cotizaciones.actualizarEnvio(0, "Ninguno", lt_numero_operacion.Text);

        }
      
      
        if (resultado != null)
        {
            bool resultadoTotales = cotizacionesProductos.actualizarTotalStatic(lt_numero_operacion.Text);
            materializeCSS.crear_toast(up_AdminEnvios, "Envío actualizado con éxito", true);
            up_AdminEnvios.Update();
        }
        else
        {
            materializeCSS.crear_toast(up_AdminEnvios, "Error al actualizar método de envío", true);
            up_AdminEnvios.Update();
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class usuario_pedidoDatos : System.Web.UI.Page
{
    contactos contactos = new contactos();
    direccionesEnvio envios = new direccionesEnvio();
    direccionesFacturacion facturacion = new direccionesFacturacion();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            cargasDatosOperacion();
            ddlContacto_cargarInfo();
            ddlEnvios_cargarInfo();
            ddlFacturacion_cargarInfo();
            adminContactos.HeaderInfoContacto = false;
            

            rellenarCamposPedidoDireccionEnvio();
            rellenarCamposPedidoDireccionFacturacion();

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
        if (Page.RouteData.Values["id_operacion"].ToString() != null) {

            string route_id_operacion = Page.RouteData.Values["id_operacion"].ToString();
            route_id_operacion = seguridad.DesEncriptar(route_id_operacion);

            pedidosDatos obtener = new pedidosDatos();
            DataTable dt_PedidoDatos = obtener.obtenerPedidoDatos(int.Parse(route_id_operacion));

            if (dt_PedidoDatos != null && dt_PedidoDatos.Rows.Count >= 1) {
                string usuario_cliente = dt_PedidoDatos.Rows[0]["usuario_cliente"].ToString();
                string numero_operacion = dt_PedidoDatos.Rows[0]["numero_operacion"].ToString();

                if (privacidadAsesores.validarOperacion(usuario_cliente) == false)
                {
                    content_pedido_datos.Visible = false; content_mensaje.Visible = true;
                    return;

                };

                int id_operacion = int.Parse(dt_PedidoDatos.Rows[0]["id"].ToString());

          
                lt_numero_operacion.Text = numero_operacion;

                Page.Title = "Datos de pedido #" + numero_operacion; 


                lt_nombre_operacion.Text = dt_PedidoDatos.Rows[0]["nombre_pedido"].ToString();
                lt_cliente_nombre.Text = dt_PedidoDatos.Rows[0]["cliente_nombre"].ToString();
                txt_comentarios.Text = dt_PedidoDatos.Rows[0]["comentarios"].ToString();
                hf_id_operacion.Value = id_operacion.ToString();
                rellenarCamposContacto(dt_PedidoDatos);


                hl_editarDatos.NavigateUrl = GetRouteUrl("usuario-pedido-datos", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",seguridad.Encriptar(id_operacion.ToString()) }
                    });
                hl_editarProductos.NavigateUrl = GetRouteUrl("usuario-pedido-productos", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",seguridad.Encriptar(id_operacion.ToString()) }
                    });

                }

            string metodoEnvio = dt_PedidoDatos.Rows[0]["metodoEnvio"].ToString();
            if(metodoEnvio == "En Tienda")  chkEnvioEnTienda.Checked = true; 
            else chkEnvioEnTienda.Checked = false;

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

        ddl_contactosUser.Items.Insert(0, new ListItem("Selecciona"));
        ddl_contactosUser.Items[0].Value = "";
        ddl_contactosUser.Items[0].Attributes.Add("disabled ", "");
        ddl_contactosUser.Items[0].Attributes.Add("selected ", "");
        up_pedidoDatos.Update();
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
        pedidosDatos actualizar = new pedidosDatos();
        model_pedidos_datos datosContacto = new model_pedidos_datos();
        datosContacto.cliente_nombre = txt_nombre.Text;
        datosContacto.cliente_apellido_paterno = txt_apellido_paterno.Text;
        datosContacto.cliente_apellido_materno = txt_apellido_materno.Text;
        datosContacto.email = txt_email.Text ;
        datosContacto.telefono = txt_telefono.Text;
        datosContacto.celular = txt_celular.Text;

        string resultado = actualizar.actualizarPedido_datosContacto(numero_operacion, datosContacto);

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
            
        up_pedidoDatos.Update();

        }
    protected void btn_cancelarDatosContacto_Click(object sender, EventArgs e) {

        pedidosDatos obtener = new pedidosDatos();
        DataTable dt_PedidoDatos = obtener.obtenerPedidoDatos(int.Parse(hf_id_operacion.Value));
        ddl_contactosUser.SelectedIndex = 0;
        rellenarCamposContacto(dt_PedidoDatos);

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
            txt_colonia_envio.Text = direccion.colonia;
            txt_delegacion_municipio_envio.Text = direccion.delegacion_municipio;
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
                json_respuestas result = await DireccionesServiceCP.GetCodigoPostalAsync(direccion.codigo_postal);


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
                    txt_colonia_envio.Text = direccion.colonia;
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
        else
        {
            ddl_colonia_envio.Items.Clear();
            ddl_colonia_envio.Visible = false;
            txt_colonia_envio.Visible = true;
            txt_colonia_envio.Text = "";
            txt_codigo_postal_envio.Text = "";
            EnabledCamposEnvio(true);
            up_AdminEnvios.Update();
        }

          

        }
    protected void activarTabEnvios(object sender, EventArgs e) {

        ScriptManager.RegisterStartupScript(this, typeof(Page), "con", "$(document).ready(function () {    var instance = M.Tabs.getInstance($('ul.tabs')); instance.select('test-swipe-2');   });", true);
    }
    protected void btn_guardarDireccionEnvio_Click(object sender, EventArgs e) {
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

            pedidosDatos actualizarEnvio = new pedidosDatos ();

            
            if (actualizarEnvio.pedidoDireccionEnvio(numero_operacion, direccion) == true) {
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

 
        }
    ///<summary>
    /// Rellena los campos de dirección de envío provientes del pedido.
    ///</summary>
    protected async void rellenarCamposPedidoDireccionEnvio() {

        string numero_operacion = lt_numero_operacion.Text;

        model_direccionesEnvio direccion = new model_direccionesEnvio();
        pedidosDatos obtener = new pedidosDatos();

        direccion = obtener.obtenerPedidoDireccionEnvio(numero_operacion);

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
                EnabledCamposEnvio(true);

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
    protected void ddlEnvios_cargarInfo() {

        usuarios usuario = usuarios.modoAsesor();

        DataTable dtDireccionesEnvio = envios.obtenerDirecciones(usuario.id);

        dtDireccionesEnvio.Columns.Add("ddlValue", typeof(string), "nombre_direccion + ' ' + calle + ', ' + numero");
        ddl_direccionesEnvio.DataSource = dtDireccionesEnvio;
        ddl_direccionesEnvio.DataTextField = "ddlValue";
        ddl_direccionesEnvio.DataValueField = "id";
        ddl_direccionesEnvio.DataBind();

        ddl_direccionesEnvio.Items.Insert(0, new ListItem("Selecciona"));
        ddl_direccionesEnvio.Items[0].Value = "";
        ddl_direccionesEnvio.Items[0].Attributes.Add("disabled ", "");
        ddl_direccionesEnvio.Items[0].Attributes.Add("selected ", "");
        up_AdminEnvios.Update();
        }
    protected void btn_cancelarDireccionEnvio_Click_Click(object sender, EventArgs e) {

        rellenarCamposPedidoDireccionEnvio();
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

        ScriptManager.RegisterStartupScript(this, typeof(Page), "con", "$(document).ready(function () {     $('ul.tabs').tabs('select_tab', 'test-swipe-3');  });", true);
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

            pedidosDatos actualizarFacturacion= new pedidosDatos();

            bool resultado = actualizarFacturacion.pedidoDireccionFacturacion(numero_operacion, direccion);
            if (resultado == true) {
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
    /// Rellena los campos de dirección de facturación provientes del pedido.
    ///</summary>
    protected void rellenarCamposPedidoDireccionFacturacion() {

        string numero_operacion = lt_numero_operacion.Text;

        model_direccionesFacturacion direccion = new model_direccionesFacturacion();
        pedidosDatos obtener = new pedidosDatos();

        direccion = obtener.obtenerPedidoDireccionFacturacion(numero_operacion);

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

        ddl_direccionesFacturacion.Items.Insert(0, new ListItem("Selecciona"));
        ddl_direccionesFacturacion.Items[0].Value = "";
        ddl_direccionesFacturacion.Items[0].Attributes.Add("disabled ", "");
        ddl_direccionesFacturacion.Items[0].Attributes.Add("selected ", "");
        up_AdminFacturacion.Update();
        }
    protected void btn_cancelarDireccionFacturacion_Click(object sender, EventArgs e) {

        rellenarCamposPedidoDireccionFacturacion();
        }

    protected void txt_comentarios_TextChanged(object sender, EventArgs e) {

        string comentarios = textTools.lineMulti(txt_comentarios.Text);

        if (comentarios.Length < 600) {

            bool resultado = pedidosDatos.actualizarComentarioPedido(lt_numero_operacion.Text, comentarios);

            if (resultado == true)
                materializeCSS.crear_toast(up_content_comentarios, "Comentarios guardados con éxito", true);
            else
                materializeCSS.crear_toast(up_content_comentarios, "Error al guardar comentario", false);
            } else {

            materializeCSS.crear_toast(up_content_comentarios, "Supera el límite de caracteres", false);
            }

        }

    protected void chkEnvioEnTienda_CheckedChanged(object sender, EventArgs e)
    {


        string resultado = null;

        if (chkEnvioEnTienda.Checked)
        {
            string metodoEnvio = "En Tienda";
            decimal envio = 0;


            resultado = pedidosDatos.actualizarEnvio(envio, metodoEnvio, lt_numero_operacion.Text);
        }
        else
        {
            resultado = pedidosDatos.actualizarEnvio(0, "Ninguno", lt_numero_operacion.Text);

        }


        if (resultado != null)
        {
            bool resultadoTotales = pedidosProductos.actualizarTotalStatic(lt_numero_operacion.Text);
            materializeCSS.crear_toast(up_AdminEnvios, "Envío actualizado con éxito", true);
            up_AdminEnvios.Update();
        }
        else
        {
            materializeCSS.crear_toast(up_AdminEnvios, "Error al actualizar método de envío", true);
            up_AdminEnvios.Update();
        }





    }

    protected async void txt_codigo_postal_TextChanged(object sender, EventArgs e)
    {
        string cp = textTools.lineSimple(txt_codigo_postal_envio.Text);
        if (!string.IsNullOrWhiteSpace(cp))
        {
            json_respuestas result = await DireccionesServiceCP.GetCodigoPostalAsync(cp);


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


        activarTabEnvios(sender, e);
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

}
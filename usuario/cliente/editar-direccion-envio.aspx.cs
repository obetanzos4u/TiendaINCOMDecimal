using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usuario_cliente_editar_direccion_envio : System.Web.UI.Page
{

 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            if (Page.RouteData.Values["id_direccion"] != null) {

                obtenerDireccion();

               
            }
            else
            {
                Response.Redirect(HttpContext.Current.Request.Url.Authority, true);
            }
        }
        else

        {
            if (ddl_pais.SelectedText == "México")
            {
                cont_ddl_estado.Visible = true;
                cont_txt_estado.Visible = false;
            }
            else
            {
                cont_ddl_estado.Visible = false;
                cont_txt_estado.Visible = true;
            }
        }
     
    }

    protected async void obtenerDireccion()
    {
        int idDirección = int.Parse(Page.RouteData.Values["id_direccion"].ToString());
        direcciones_envio direccion =  direcciones_envio_EF.Obtener(idDirección);

        txt_nombre_direccion.Text = direccion.nombre_direccion;
        txt_calle.Text = direccion.calle;
         txt_numero.Text = direccion.numero;
        txt_colonia.Text = direccion.colonia;
        txt_delegacion_municipio.Text = direccion.delegacion_municipio;
        txt_codigo_postal.Text = direccion.codigo_postal;
        txt_referencias.Text = direccion.referencias;

        if (direccion.pais == "México")
        {
            ddl_pais.SelectedText = direccion.pais;
            cont_txt_estado.Visible = false;
            cont_ddl_estado.Visible = true;
            ddl_estado.SelectedText = direccion.estado;


        }
        else
        {
            cont_ddl_estado.Visible = false;
            cont_txt_estado.Visible = true;
        }





        // Sección llenado Código Postal
        string cp = textTools.lineSimple(txt_codigo_postal.Text);
        if (!string.IsNullOrWhiteSpace(cp))
        {
            json_respuestas result = await DireccionesServiceCP.GetCodigoPostalAsync(cp);


            if (result.result == true)
            {
                dynamic Result = JsonConvert.DeserializeObject<dynamic>(result.response);

                txt_delegacion_municipio.Text = Result[0].Municipio;
                txt_ciudad.Text = Result[0].Ciudad;

                ddl_estado.SelectedText = Result[0].Estado;
                ddl_pais.SelectedText = Result[0].Pais;

                ddl_colonia.Items.Clear();
                foreach (var t in Result)
                {

                    string Asentamiento = t.Asentamiento;
                    ddl_colonia.Items.Add(new ListItem(Asentamiento, Asentamiento));
                }
                ddl_colonia.DataBind();


                ddl_colonia.SelectedValue = txt_colonia.Text;

                cont_ddl_estado.Visible = true;
                cont_txt_estado.Visible = false;

                EnabledCampos( false);
            }
            else
            {
                EnabledCampos( true);

     
            }

        }
    }
    protected void btn_editar_direccion_Click(object sender, EventArgs e)
    {

        int idDireccion = int.Parse(Page.RouteData.Values["id_direccion"].ToString());
        string estado;
        string colonia = "";
        if (ddl_pais.SelectedText == "México") estado = ddl_estado.SelectedText;
        else estado = txt_estado.Text;

        if (ddl_colonia.Visible) colonia = ddl_colonia.SelectedValue;
        else colonia = txt_colonia.Text;

        direccionesEnvio direccion = new direccionesEnvio
        {
            id= idDireccion,
            nombre_direccion = txt_nombre_direccion.Text,
            calle = txt_calle.Text,
            numero = txt_numero.Text,
            colonia = colonia,
            delegacion_municipio = txt_delegacion_municipio.Text,
            ciudad = txt_ciudad.Text,
            estado = estado,
            codigo_postal = txt_codigo_postal.Text,
            pais = ddl_pais.SelectedText,
            referencias = txt_referencias.Text
        };

        json_respuestas resultValidacion = validarCampos.direccionDeEnvio(direccion);

        if (resultValidacion.result)
        {
            

            if (direccion.actualizarDireccion() != null)
            {

                BootstrapCSS.Message(this, "#content_alert", BootstrapCSS.MessageType.success, "Realizado con éxito", "Dirección de envío actualizada con éxito");
                ValidarReferenciaPEdido(direccion);
            }
            else
            {
                BootstrapCSS.Message(this, "#content_alert", BootstrapCSS.MessageType.success, "Realizado con éxito", "Dirección de envío actualizada con éxito");
            }


        }
        else
        {
            BootstrapCSS.Message(this, "#content_alert", BootstrapCSS.MessageType.danger, "Error al crear dirección de envío", resultValidacion.message);

        }
    }

    protected void ValidarReferenciaPEdido(direccionesEnvio direccionEnvio)
    {

       if(Request.QueryString["ref"] != null && Request.QueryString["numero_operacion"] != null)
        {
            string idPedido = Request.QueryString["ref"].ToString();
            string numero_operacion = Request.QueryString["numero_operacion"].ToString();



            pedidos_direccionEnvio pedidoDireccionEnvio = new pedidos_direccionEnvio();

            pedidoDireccionEnvio.numero_operacion = numero_operacion;
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

           
            var result = PedidosEF.GuardarDireccionEnvio(numero_operacion, pedidoDireccionEnvio);

            pedidosDatos.establecerEstatusCalculo_Costo_Envio(true, numero_operacion);
            string resultado = pedidosDatos.actualizarEnvio(0, "Estándar", numero_operacion);
            HttpContext ctx = HttpContext.Current;
           
                try
                {
                    HttpContext.Current = ctx;
                    ValidarCalculoEnvioOperacion validar = new ValidarCalculoEnvioOperacion(numero_operacion, "pedido");


                }
                catch (Exception ex)
                {
                    devNotificaciones.error("Calcular envio en pedido: " + numero_operacion + " ", ex);


                }  
           

            string redirectUrl = GetRouteUrl("cliente-pedido-resumen", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",idPedido }
                    });


 

            BootstrapCSS.RedirectJs(Page, redirectUrl, 6000);
        }
    }
    protected void EnabledCampos(bool status)
    {
        txt_colonia.Enabled = status;
        txt_delegacion_municipio.Enabled = status;
        txt_ciudad.Enabled = status;
        txt_estado.Enabled = status;
        ddl_pais.Enabled = status;
        ddl_estado.Enabled = status;
        if (!status)
        {
            txt_delegacion_municipio.CssClass += "form-select";
        }
    }
    protected async void txt_codigo_postal_TextChanged(object sender, EventArgs e)
    {

        string cp = textTools.lineSimple(txt_codigo_postal.Text);
        if (!string.IsNullOrWhiteSpace(cp))
        {
            json_respuestas result = await DireccionesServiceCP.GetCodigoPostalAsync(cp);


            if (result.result == true)
            {
                dynamic Result = JsonConvert.DeserializeObject<dynamic>(result.response);

                txt_delegacion_municipio.Text = Result[0].Municipio;
                txt_ciudad.Text = Result[0].Ciudad;

                ddl_estado.SelectedText = Result[0].Estado;
                ddl_pais.SelectedText = Result[0].Pais;

                ddl_colonia.Items.Clear();
                foreach (var t in Result)
                {

                    string Asentamiento = t.Asentamiento;
                    ddl_colonia.Items.Add(new ListItem(Asentamiento, Asentamiento));
                }
                ddl_colonia.DataBind();




                cont_ddl_estado.Visible = true;
                cont_txt_estado.Visible = false;

                EnabledCampos(false);

                ddl_colonia.Visible = true;
                txt_colonia.Visible = false;
            }
            else
            {
                ddl_colonia.Items.Clear();
                ddl_colonia.Visible = false;
                txt_colonia.Visible = true;

                EnabledCampos(true);

                materializeCSS.crear_toast(this, result.message, false);
            }

        }
        else
        {
            ddl_colonia.Items.Clear();
            ddl_colonia.Visible = false;
            txt_colonia.Visible = true;
            EnabledCampos(true);
        }

    }
}
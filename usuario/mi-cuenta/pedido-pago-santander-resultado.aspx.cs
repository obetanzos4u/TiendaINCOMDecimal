using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usuario_mi_cuenta_pedido_pago_santander_resultado : System.Web.UI.Page
{
    string url = HttpContext.Current.Request.Url.AbsoluteUri;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                LeerURL();
            }
            catch (Exception ex)
            {
                MostrarError();
                if (usuarios.userLogin().tipo_de_usuario == "usuario")
                {
                    detallesError.InnerHtml += "<br>" + url;
                }
                devNotificaciones.notificacionSimple(ex.ToString());
                devNotificaciones.error("Error al procesar respuesta: " + url, ex);
            }
        }
    }
    protected void LeerURL()
    {
        if (Request.QueryString["nbResponse"] == null) { MostrarError(); return; }

        string nbResponse = Request.QueryString["nbResponse"].ToString().ToLower();

        if (nbResponse == "Aprobado".ToLower() || nbResponse == "approved".ToLower())
        {
            Aprobado();
        }
        else if (nbResponse == "Rechazado".ToLower() || nbResponse == "denied".ToLower())
        {
            Rechazado();
        }
        else
        {
            NotiflixJS.Message(this, NotiflixJS.MessageType.warning, "No se encontró una solicitud válida, intenta de nuevo");
            //msgError.InnerText = "Intenta de nuevo";
            //detallesError.InnerText = "No se encontró una solicitud válida, intenta de nuevo";
            //content_msgError.Visible = true;
        }
    }
    protected void MostrarError()
    {
        NotiflixJS.Message(this, NotiflixJS.MessageType.failure, "Error");
        msgError.InnerText = "Hubo un problema en el pago";
        detallesError.InnerText = "Vuelte a intentar más adelante o contacta a tú asesor";
        content_msgError.Visible = true;
        {
            detallesError.InnerHtml += "<br>" + url;
        }
    }
    protected void Rechazado()
    {
        string cdResponse = Request.QueryString["cdResponse"].ToString();
        string nb_error = Request.QueryString["nb_error"].ToString();

        content_msgError.Visible = true;
        msgError.InnerText = cdResponse;
        detallesError.InnerText = nb_error.Replace("<", "").Replace("/<", "").Replace("script", "").Replace("http", "");
    }
    protected void Aprobado()
    {
        if (Request.QueryString["referencia"] == null) { MostrarError(); return; }
        if (Request.QueryString["importe"] == null) { MostrarError(); return; }
        if (Request.QueryString["email"] == null) { MostrarError(); return; }
        if (Request.QueryString["nuAut"] == null) { MostrarError(); return; }
        if (Request.QueryString["nbMoneda"] == null) { MostrarError(); return; }
        if (string.IsNullOrWhiteSpace(Request.QueryString["nbResponse"].ToString())) { MostrarError(); return; }

        string idLiga = Request.QueryString["idLiga"].ToString();
        string referencia = Request.QueryString["referencia"].ToString();
        string importe = Request.QueryString["importe"].ToString();
        string email = Request.QueryString["email"].ToString();
        string nuAut = Request.QueryString["nuAut"].ToString();
        string nbMoneda = Request.QueryString["nbMoneda"].ToString();

        Content_Confirmacion.Visible = true;
        lbl_importe.Text = "$" + importe + nbMoneda;
        lbl_referencia.Text = referencia;
        string redireccion = GetRouteUrl("cliente-pedido-finalizado", new System.Web.Routing.RouteValueDictionary
        {
            { "id_operacion", seguridad.Encriptar(referencia) }
        });
        BootstrapCSS.RedirectJs(this, redireccion, 3000);
    }
}
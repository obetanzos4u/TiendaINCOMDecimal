using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usuario_mi_cuenta_pedido_visualizar : System.Web.UI.Page {
    protected void Page_Init(object sender, EventArgs e) {

        if (HttpContext.Current.User.Identity.IsAuthenticated == true && Session["datosUsuario"] == null) {
            usuarios session = new usuarios();
            session.establecer_DatosUsuario(HttpContext.Current.User.Identity.Name);

            } else if (HttpContext.Current.User.Identity.IsAuthenticated == false || Session["datosUsuario"] == null)
            Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority));


        if (Session["impuesto"] == null) Session["impuesto"] = 1.16;


        if (Session["tipoCambio"] == null) Session["tipoCambio"] = operacionesConfiguraciones.obtenerTipoDeCambio();


        if (Session["modoAsesor"] == null) Session["modoAsesor"] = false;


        }
    protected void Page_Load(object sender, EventArgs e) {


        if (!IsPostBack) {

            if (Page.RouteData.Values["id_operacion"] != null) {

                string route_id_operacion = Page.RouteData.Values["id_operacion"].ToString();
                route_id_operacion = seguridad.DesEncriptar(route_id_operacion);

                operacion.idSQL = route_id_operacion;

                link_pago.NavigateUrl = GetRouteUrl("cliente-pedido-resumen", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion", seguridad.Encriptar(route_id_operacion) },
                    }); 
                
           
            }
            }

        }
    protected void Page_PreRender(object sender, EventArgs e) {

        txt_destinatarios.Text = operacion.email;
        }

        protected async void  btn_enviarEmail_Click(object sender, EventArgs e) {

        string destinararios = txt_destinatarios.Text;
        string mensaje = operacionMensaje.obtenerMensaje();
        mensaje = mensaje + "<br><br>" + operacion.obtenerEmail();

        pdfTools pdf = new pdfTools(operacion.html, operacion.header, Server.MapPath("~/"));

        string asunto = string.Format("{0} - {1} {2},", operacion.numero_operacion, operacion.nombre_pedido, operacion.cliente_nombre);
        string nombrePDF = string.Format("{0} - {1},", operacion.numero_operacion, operacion.cliente_nombre);

        List<Attachment> attachmentAdjuntos = new List<Attachment>();

        HttpContext ctx = HttpContext.Current;
       
       Attachment attachment = await System.Threading.Tasks.Task.Run(() => attachment = new System.Net.Mail.Attachment(new MemoryStream(pdf.crearPdfBytes(true)), nombrePDF + " " + utilidad_fechas.AAAMMDD() + ".pdf"));
        HttpContext.Current = ctx;
        attachmentAdjuntos.Add(attachment);

        emailPedidos enviar = new emailPedidos(asunto, destinararios, mensaje, null, attachmentAdjuntos);
        await System.Threading.Tasks.Task.Run(() => {
            HttpContext.Current = ctx;  enviar.enviar();
        });

     
         //lbl_mensaje.Text =  enviar.resultado;
        lbl_mensaje.Text =  enviar.resultadoMensaje;
        if (enviar.resultado == false) lbl_mensaje.CssClass = "msgFalse";
        else lbl_mensaje.CssClass = "msgTrue";
        }
    }
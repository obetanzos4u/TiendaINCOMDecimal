using SelectPdf;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class usuario_mi_cuenta_cotizacion_visualizar : System.Web.UI.Page {

    protected void Page_Init(object sender, EventArgs e) {

        if (HttpContext.Current.User.Identity.IsAuthenticated == true && Session["datosUsuario"] == null) {
            usuarios session = new usuarios();
            session.establecer_DatosUsuario(HttpContext.Current.User.Identity.Name);

            } else if(HttpContext.Current.User.Identity.IsAuthenticated == false || Session["datosUsuario"] == null)
            Response.Redirect(Request.Url.GetLeftPart(UriPartial.Authority));
                    

        if (Session["impuesto"] == null)  Session["impuesto"] = 1.16;
            

        if (Session["tipoCambio"] == null) Session["tipoCambio"] = operacionesConfiguraciones.obtenerTipoDeCambio();
            

        if (Session["modoAsesor"] == null)  Session["modoAsesor"] = false;
            
         
        }

    protected void Page_Load(object sender, EventArgs e) {


        if (!IsPostBack) {

            if (Page.RouteData.Values["id_operacion"] != null) {

                string route_id_operacion = Page.RouteData.Values["id_operacion"].ToString();

     

                btn_editarCotizacion.NavigateUrl = GetRouteUrl("usuario-cotizacion-datos", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion",route_id_operacion }
                     });

                btn_editarProductos.NavigateUrl = GetRouteUrl("usuario-cotizacion-productos", new System.Web.Routing.RouteValueDictionary {
                        { "id_operacion", route_id_operacion }
                    });


                route_id_operacion = seguridad.DesEncriptar(route_id_operacion);

                operacion.idSQL = route_id_operacion;
                 
                // Sección que valida si el usuario logeado tiene permitdo ver el módulo de adjuntos
                usuarios userLogin = usuarios.userLogin();
                if(userLogin.tipo_de_usuario == "usuario") {
                    chk_cc_telemarketing.Visible = true;
                    content_adjuntos.Visible = true;
                    }
                }
            }

        }
    protected void Page_PreRender(object sender, EventArgs e) {

        txt_destinatarios.Text = operacion.email;
        }
 
        protected async void btn_enviarEmail_Click(object sender, EventArgs e) {

       

        string destinararios = txt_destinatarios.Text;
        string mensaje = operacionMensaje.obtenerMensaje();
        mensaje = mensaje + "<br><br>" + operacion.obtenerEmail();

        pdfTools pdf = new pdfTools(operacion.html, operacion.header, Server.MapPath("~/"));

        string asunto = string.Format("{0} - {1} {2},", operacion.numero_operacion, operacion.nombre_cotizacion, operacion.cliente_nombre);
        string nombrePDF = string.Format("{0} - {1},", operacion.numero_operacion,  operacion.cliente_nombre);

        HttpContext ctx = HttpContext.Current;
        usuarios userLogin = usuarios.userLogin();
        List<Attachment> attachmentAdjuntos = new List<Attachment>();

         Attachment attachment  = await System.Threading.Tasks.Task.Run(() => attachment = new System.Net.Mail.Attachment(new MemoryStream( pdf.crearPdfBytes(true)), nombrePDF + " "+utilidad_fechas.AAAMMDD()+".pdf") );
        HttpContext.Current = ctx;
        attachmentAdjuntos.Add(attachment);

        // Sección que valida si el usuario logeado tiene permitdo ver el módulo de adjuntos

        if (userLogin.tipo_de_usuario == "usuario")  {
            
            attachmentAdjuntos = procesarAdjuntos(attachmentAdjuntos);

            if (chk_cc_telemarketing.Checked)
            {
                destinararios += ",telemarketing@incom.mx";
            }
             }

         emailCotizaciones enviar = new emailCotizaciones(asunto, destinararios, mensaje, null, attachmentAdjuntos);
       
      
        await System.Threading.Tasks.Task.Run(() => {
            HttpContext.Current = ctx;
            enviar.enviar();
        });

         //lbl_mensaje.Text =  enviar.resultado;
        lbl_mensaje.Text =  enviar.resultadoMensaje;
        if (enviar.resultado == false) lbl_mensaje.CssClass = "msgFalse";
        else  lbl_mensaje.CssClass = "msgTrue";
        }
    private bool validarLongitudAdjuntos() {

        int size_total = 0;

        HttpFileCollection fileCollection = Request.Files;

        for (int i = 0; i < fileCollection.Count; i++) {
            HttpPostedFile uploadfile = fileCollection[i];
            size_total += uploadfile.ContentLength;
            }
        // Si supera el límite de adjunots              
        if (size_total > 15728640) return false; else return true;
        }
   private List<Attachment> procesarAdjuntos(List<Attachment> attachmentAdjuntos) {

        // Sección que valida si el usuario logeado tiene permitdo ver el módulo de adjuntos
        usuarios userLogin = usuarios.userLogin();

        HttpFileCollection fileCollection = Request.Files;


        string pathFoler =  Server.MapPath("~") + @"\temp\" + userLogin.email+@"\";


        // Sección para validar la eliminación de archivos.
        if (Directory.Exists(pathFoler)) {

            // Obtenemos la fecha de creación del folder
          DateTime fechaCreacion =  Directory.GetCreationTimeUtc(pathFoler);

            // La convertimos a hora central
            fechaCreacion = utilidad_fechas.ConvertirFechaToCentral(fechaCreacion);
            // Si tiene una fecha mayor a 1, borramos su contenido.
           if (utilidad_fechas.calcularDiferenciaDias(fechaCreacion) > 1) { }
            } else {
            // Si no simplemente lo creamos
            Directory.CreateDirectory(pathFoler);
            }



        for (int i = 0; i < fileCollection.Count; i++) {
            HttpPostedFile uploadfile = fileCollection[i];
            string fileName = Path.GetFileName(uploadfile.FileName);
            string fileNamePath = pathFoler + fileName;
            if (uploadfile.ContentLength > 0) {
                uploadfile.SaveAs(fileNamePath);
                    /// INICIO - Creando el constructor de la lista generica
                
                /// FIN - Creando el constructor de la lista generica
                Attachment attachment =  new Attachment(new MemoryStream(File.ReadAllBytes(fileNamePath)), fileName);
                attachmentAdjuntos.Add(attachment);

                if (File.Exists(fileNamePath)) {
                    File.Delete(fileNamePath);
                    }
                }
        }
       
        return attachmentAdjuntos;
   }

     
            
}
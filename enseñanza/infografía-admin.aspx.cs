using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class enseñanza_infografía_admin : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack)
        {


            menuInfografias.activeTab("crear");


            if (HttpContext.Current.User.Identity.IsAuthenticated) { 

            if (usuarios.userLogin().rango != 3) Response.Redirect("/enseñanza/infografías.aspx");

            }
            else
            {

              //  Response.Redirect("/enseñanza/infografías.aspx");
            }
        }
    }

    protected void btn_crearInfografía_Click(object sender, EventArgs e)
    {
        infografías infografía = new infografías();

        infografía.titulo = textTools.lineSimple(txt_titulo.Text);
        infografía.descripción = textTools.lineMulti(txt_descripción.Text);

        infografía.fecha = utilidad_fechas.obtenerCentral();


        // INICIO Validación del archivo de la miniatura
        infografíasController validarMiniatura = new infografíasController();
        validarMiniatura.validarMiniatura(file_miniatura.PostedFile);
            
        if (validarMiniatura.result == false)  bulmaCSS.Message(Page, "#contentResultado", bulmaCSS.MessageType.danger, validarMiniatura.message);

        // FIN  Validación del archivo de la miniatura





        // INICIO Validación de la infografía
        infografíasController validarInfografía = new infografíasController();
        validarInfografía.validarInfografía(file_infografía.PostedFile);

        if (validarInfografía.result == false)    bulmaCSS.Message(Page, "#contentResultado", bulmaCSS.MessageType.danger, validarInfografía.message);
        // FIN  Validación de la infografía



        if (validarInfografía.result && validarMiniatura.result)
        {
            infografía.nombreImagenMiniatura = file_miniatura.FileName;
            infografía.nombreArchivo = file_infografía.FileName;


            infografíasController guardarInfografía = new infografíasController();
            guardarInfografía.guardar(infografía);

            if (guardarInfografía.result)
            {
                file_miniatura.SaveAs(Path.Combine(infografíasController.pathMiniaturas, file_miniatura.FileName));          // file path where you want to upload   
                file_infografía.SaveAs(Path.Combine(infografíasController.path, file_infografía.FileName));          // file path where you want to upload   
            }
          
            bulmaCSS.Message(Page, "#contentResultado", guardarInfografía.result ? bulmaCSS.MessageType.success :  bulmaCSS.MessageType.danger, guardarInfografía.message);
        }


    }
}

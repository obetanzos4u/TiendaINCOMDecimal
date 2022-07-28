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


            menuInfografias.activeTab("editar");
            cargarInfografias();
            cargarInfografia();
            if (HttpContext.Current.User.Identity.IsAuthenticated) { 

            if (usuarios.userLogin().rango != 3) Response.Redirect("/enseñanza/infografías.aspx");

            }
            else
            {

                Response.Redirect("/enseñanza/infografías.aspx");
            }
        }
    }
    protected void cargarInfografias()
    {
        ddl_infografias.DataTextField = "titulo";
        ddl_infografias.DataValueField = "id";
        ddl_infografias.DataSource=  infografíasController.obtener();


        ddl_infografias.DataBind();
        ddl_infografias.Items.Insert(0, new ListItem("Selecciona"));
        ddl_infografias.SelectedValue = "";

    }
    
        protected void cargarInfografia()
    {
        if (Request.QueryString["idInfografia"] != null)
        {
            int idInfografia = -1;
            try
            {

                idInfografia = int.Parse(Request.QueryString["idInfografia"].ToString());

                hf_idInfografia.Value = idInfografia.ToString();
                infografías infografia = infografíasController.obtener(idInfografia);

                ddl_infografias.SelectedValue = infografia.id.ToString();
                txt_titulo.Text = infografia.titulo;
                txt_descripción.Text = infografia.descripción;
                img_miniatura.ImageUrl = "/img/infografías/miniatura/" + infografia.nombreImagenMiniatura;

                link_infografia.NavigateUrl = "/img/infografías/miniatura/" + infografia.nombreArchivo;
                link_infografia.Text = infografia.nombreArchivo;
 

            }
            catch (Exception ex)
            {


            }

        }
        else
        {

         
        }


    }


    
    protected void btn_actualizarInfografía_Click(object sender, EventArgs e)
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

    protected void ddl_infografias_SelectedIndexChanged(object sender, EventArgs e)
    {
        var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());
        nameValues.Set("idInfografia",ddl_infografias.SelectedValue);
        string url = Request.Url.AbsolutePath;
        Response.Redirect(url + "?" + nameValues);

    }

    protected void btn_guardar_descripción_Click(object sender, EventArgs e)
    {

        int idInfografía = -1;
        try { 
         idInfografía = int.Parse(hf_idInfografia.Value.ToString());
        } catch(Exception ex)
        {
            bulmaCSS.Message(Page, "#contentResultado",   bulmaCSS.MessageType.danger, "El id no es correcto");
            return;
        }

        try { 
        using (var db = new tiendaEntities())
        {
            var result = db.infografías.SingleOrDefault(b => b.id == idInfografía);
            if (result != null)
            {
                result.descripción = textTools.lineMulti(txt_descripción.Text);
                db.SaveChanges();

                    bulmaCSS.Message(Page, "#contentResultado", bulmaCSS.MessageType.success, "Descripción actualizada con éxito");
                    cargarInfografia();
                }
        }
        } catch(Exception ex)
        {
            bulmaCSS.Message(Page, "#contentResultado", bulmaCSS.MessageType.danger, "Hubo un error al actualizar la descripción");

        }



    }

    protected void btn_cargarMiniatura_Click(object sender, EventArgs e)
    {

        int idInfografía = -1;
        try
        {
            idInfografía = int.Parse(hf_idInfografia.Value.ToString());
        }
        catch (Exception ex)
        {
            bulmaCSS.Message(Page, "#contentResultado", bulmaCSS.MessageType.danger, "El id no es correcto");
            return;
        }

        try
        {

            // INICIO Validación del archivo de la miniatura
            infografíasController validarMiniatura = new infografíasController();
            validarMiniatura.validarMiniatura(file_miniatura.PostedFile);

         
            // FIN  Validación del archivo de la miniatura




            if (validarMiniatura.result)
            {
                using (var db = new tiendaEntities())
                {
                    var result = db.infografías.SingleOrDefault(b => b.id == idInfografía);
                    if (result != null)
                    {

                        infografíasController.eliminarFileMiniatura(result.nombreImagenMiniatura);
                        file_miniatura.SaveAs(Path.Combine(infografíasController.pathMiniaturas, file_miniatura.FileName));          // file path where you want to upload   

                        result.nombreImagenMiniatura = file_miniatura.FileName;
                        db.SaveChanges();


                        bulmaCSS.Message(Page, "#contentResultado", bulmaCSS.MessageType.success, "Miniatura actualizada con éxito");
                        cargarInfografia();
                    }
                }


            }
            else
            {
                bulmaCSS.Message(Page, "#contentResultado", bulmaCSS.MessageType.danger, validarMiniatura.message);


            }


        }
        catch (Exception ex)
        {
            bulmaCSS.Message(Page, "#contentResultado", bulmaCSS.MessageType.danger, "Hubo un error al actualizar la descripción");

        }



    }

    protected void btn_guardar_titulo_Click(object sender, EventArgs e)
    {
        int idInfografía = -1;
        try
        {
            idInfografía = int.Parse(hf_idInfografia.Value.ToString());
        }
        catch (Exception ex)
        {
            bulmaCSS.Message(Page, "#contentResultado", bulmaCSS.MessageType.danger, "El id no es correcto");
            return;
        }

        try
        {
            using (var db = new tiendaEntities())
            {
                var result = db.infografías.SingleOrDefault(b => b.id == idInfografía);
                if (result != null)
                {
                    result.titulo = textTools.lineMulti(txt_titulo.Text);
                    db.SaveChanges();

                    bulmaCSS.Message(Page, "#contentResultado", bulmaCSS.MessageType.success, "Título actualizado con éxito");
                    cargarInfografia();
                }
            }
        }
        catch (Exception ex)
        {
            bulmaCSS.Message(Page, "#contentResultado", bulmaCSS.MessageType.danger, "Hubo un error al actualizar el título");

        }
    }

    protected void btn_cargarInfografia_Click(object sender, EventArgs e)
    {
        int idInfografía = -1;
        try
        {
            idInfografía = int.Parse(hf_idInfografia.Value.ToString());
        }
        catch (Exception ex)
        {
            bulmaCSS.Message(Page, "#contentResultado", bulmaCSS.MessageType.danger, "El id no es correcto");
            return;
        }

        try
        {

            // INICIO Validación del archivo de la miniatura
            infografíasController validarInfografia = new infografíasController();
            validarInfografia.validarInfografía(file_infografía.PostedFile);

            // FIN  Validación del archivo de la miniatura




            if (validarInfografia.result)
            {
                using (var db = new tiendaEntities())
                {
                    var result = db.infografías.SingleOrDefault(b => b.id == idInfografía);
                    if (result != null)
                    {
                        infografíasController.eliminarFileInfografia(result.nombreArchivo);

                        file_infografía.SaveAs(Path.Combine(infografíasController.path, file_infografía.FileName));          // file path where you want to upload   
                        result.nombreArchivo = file_infografía.FileName;

                        db.SaveChanges();


                        bulmaCSS.Message(Page, "#contentResultado", bulmaCSS.MessageType.success, "Infografía actualizada con éxito");
                        cargarInfografia();
                    }
                }


            }
            else
            {
                bulmaCSS.Message(Page, "#contentResultado", bulmaCSS.MessageType.danger, validarInfografia.message);


            }


        }
        catch (Exception ex)
        {
            bulmaCSS.Message(Page, "#contentResultado", bulmaCSS.MessageType.danger, "Hubo un error al actualizar la infografía");

        }

    }
}

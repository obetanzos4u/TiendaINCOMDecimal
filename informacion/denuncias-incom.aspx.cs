using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class informacion_denuncias_incom : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            logueado.Visible = true;
        }
        else
        {
            anonimo.Visible = true;
        }

          

    }



    protected void restablecerCampos()
    {
        
        txtdenuncias.Text = "";
        txt_coment.Text = "";
        txt_felicitaciones.Text = "";
        txt_sugerencias.Text = "";
    }

    

    protected void guardarComentario()
    {
        quejas comentario = new quejas();

        comentario.idUsuario = usuarios.userLogin().id;
        comentario.tipoComentario = ddl_opciones.SelectedValue;
        comentario.direccion_ip = red.GetDireccionIp(Request);

        string opcion_comentario_ddl; ;
        string comentario_txt;


        if (ddl_opciones.SelectedValue == "quejas")
        {
            

            opcion_comentario_ddl = ddl_comentario_queja.SelectedValue;
            comentario_txt = txt_coment.Text;

            if (ddl_comentario_queja.SelectedValue == "0" && txt_coment.Text != "")
            {
                
                comentario.comentario = comentario_txt;
            }
            else if (ddl_comentario_queja.SelectedValue != "0" && txt_coment.Text == "")
            {
                comentario.comentario = opcion_comentario_ddl;
            }

            else
            {
                comentario.comentario = opcion_comentario_ddl + "; " + comentario_txt;
            }


        }

        else if (ddl_opciones.SelectedValue == "sugerencias")
        {
            opcion_comentario_ddl = ddl_comentario_sugerencia.SelectedValue;
            comentario_txt = txt_sugerencias.Text;

            if (ddl_comentario_sugerencia.SelectedValue == "0" && txt_sugerencias.Text != "")
            {
                comentario.comentario = comentario_txt;
            }

            else if (ddl_comentario_sugerencia.SelectedValue != "0" && txt_sugerencias.Text == "")
            {
                comentario.comentario = ddl_comentario_sugerencia.SelectedValue;
            }

            else
            {
                comentario.comentario = opcion_comentario_ddl + "; " + comentario_txt;
            }


        }

        else if (ddl_opciones.SelectedValue == "denuncias")
        {

            opcion_comentario_ddl = ddl_comentario_denuncia.SelectedValue;
            comentario_txt = txtdenuncias.Text;

            if (ddl_comentario_denuncia.SelectedValue == "0" && txtdenuncias.Text != "")
            {
                comentario.comentario = comentario_txt;
            }

            else if (ddl_comentario_denuncia.SelectedValue != "0" && txtdenuncias.Text == "")
            {
                comentario.comentario = ddl_comentario_denuncia.SelectedValue;
            }
            else
            {
                comentario.comentario = opcion_comentario_ddl + "; " + comentario_txt;
            }

        }


        else if (ddl_opciones.SelectedValue == "felicitaciones")
        {

            opcion_comentario_ddl = ddl_comentario_felicitacion.SelectedValue;
            comentario_txt = txt_felicitaciones.Text;

            if (ddl_comentario_felicitacion.SelectedValue == "0" && txt_felicitaciones.Text != "")
            {
                comentario.comentario = comentario_txt;
            }

            else if (ddl_comentario_felicitacion.SelectedValue != "0" && txt_felicitaciones.Text == "")
            {
                comentario.comentario = opcion_comentario_ddl;
            }
            else
            {
                comentario.comentario = opcion_comentario_ddl + "; " + comentario_txt;
            }

        }



        comentario.fecha = utilidad_fechas.obtenerCentral();

        if (HttpContext.Current.User.Identity.IsAuthenticated && usuarios.userLogin().tipo_de_usuario == "cliente" || HttpContext.Current.User.Identity.IsAuthenticated && usuarios.userLogin().tipo_de_usuario == "usuario")
        {

            comentario.modoComentario = 1;
        }


        else
        {
            comentario.tipoComentario = ddl_opciones.SelectedValue;
            comentario.modoComentario = 0;
            comentario.idUsuario = null;

        }


         QuejasSugerencias_Incom.guardarComentario(comentario);
      //  comentario.email_quejasIncom();
          restablecerCampos();
        
    }




    private void modalQuejasClosed()
    {
        ScriptManager.RegisterStartupScript(this.Page, typeof(Control), "modal_quejas", "$(document).ready(function() {  M.AutoInit();   $('.modal_quejas').modal('close');  });", true);


    }

    private void modalSugerenciasClosed()
    {
        ScriptManager.RegisterStartupScript(this.Page, typeof(Control), "modal_sugerencias", "$(document).ready(function() {  M.AutoInit();   $('.modal_sugerencias').modal('close');  });", true);
    }

    private void modalDenunciasClosed()
    {
        ScriptManager.RegisterStartupScript(this.Page, typeof(Control), "modal_denuncias", "$(document).ready(function() {  M.AutoInit();   $('.modal_denuncias').modal('close');  });", true);
    }

    private void modalFelicitacionesClosed()
    {
        ScriptManager.RegisterStartupScript(this.Page, typeof(Control), "modal_felicitaciones", "$(document).ready(function() {  M.AutoInit();   $('.modal_felicitaciones').modal('close');  });", true);

    }



    protected void btn_enviar_queja_Click(object sender, EventArgs e)

    {

        if (ddl_comentario_queja.SelectedValue == "0" && txt_coment.Text == "")
        {
            materializeCSS.crear_toast(this, "Error al guardar, elige una opción de queja o agregala manualmente", false);
            modalQuejasClosed();
            ddl_comentario_queja.SelectedValue = "0";
            ddl_opciones.SelectedValue = "0";
            txt_coment.Text = "";
            lb_comentario.Text = "Debes seleccionar una opción de QUEJA o agregarla manualmente";
            lb_comentario.ForeColor = System.Drawing.Color.Red;

        }



        else
        {
            guardarComentario();
            materializeCSS.crear_toast(this, "Comentario guardado con éxito", true);
            modalQuejasClosed();

            ddl_comentario_queja.SelectedValue = "0";
            ddl_opciones.SelectedValue = "0";
            txt_coment.Text = "";
            lb_comentario.Text = "Comentario enviado con éxito ✓.";
            lb_comentario.ForeColor = System.Drawing.Color.Green;
        }

    }


    protected void btn_enviar_sugerencia_Click(object sender, EventArgs e)
    {
        if (ddl_comentario_sugerencia.SelectedValue == "0" && txt_sugerencias.Text == "")
        {
            materializeCSS.crear_toast(this, "Error al guardar sugerencia, selecciona una opción o agrega manualmente", false);
            modalSugerenciasClosed();
            ddl_comentario_sugerencia.SelectedValue = "0";
            ddl_opciones.SelectedValue = "0";
            txt_sugerencias.Text = "";
            lb_comentario.Text = "Debes seleccionar una opción de SUGERENCIA o agregarla manualmente";
            lb_comentario.ForeColor = System.Drawing.Color.Red;
        }

        else
        {
            guardarComentario();
            materializeCSS.crear_toast(this, "Comentario guardado con éxito", true);
            modalSugerenciasClosed();
            ddl_comentario_sugerencia.SelectedValue = "0";
            ddl_opciones.SelectedValue = "0";
            txt_sugerencias.Text = "";
            lb_comentario.Text = "Comentario enviado con éxito ✓.";
            lb_comentario.ForeColor = System.Drawing.Color.Green;
        }
    }



    protected void btn_enviar_denuncia_Click(object sender, EventArgs e)
    {

        if (ddl_comentario_denuncia.SelectedValue == "0" && txtdenuncias.Text == "")
        {
            materializeCSS.crear_toast(this, "Error al agregar denuncia, elige una opción o agrega manualmente", false);
            modalDenunciasClosed();
            ddl_comentario_denuncia.SelectedValue = "0";
            ddl_opciones.SelectedValue = "0";
            txtdenuncias.Text = "";
            lb_comentario.Text = "Debes seleccionar una opción de DENUNCIA o agregarla manualmente";
            lb_comentario.ForeColor = System.Drawing.Color.Red;


        }
        else
        {


            guardarComentario();
            materializeCSS.crear_toast(this, "Comentario guardado con éxito", true);
            modalDenunciasClosed();
            ddl_comentario_denuncia.SelectedValue = "0";
            ddl_opciones.SelectedValue = "0";
            txtdenuncias.Text = "";
            lb_comentario.Text = "Comentario enviado con éxito ✓.";
            lb_comentario.ForeColor = System.Drawing.Color.Green;
        }

    }


    protected void btn_enviar_felicitacion_Click(object sender, EventArgs e)
    {
        if (ddl_comentario_felicitacion.SelectedValue == "0" && txt_felicitaciones.Text == "")
        {
            materializeCSS.crear_toast(this, "Error al agregar felicitación", false);
            modalFelicitacionesClosed();
            ddl_comentario_felicitacion.SelectedValue = "0";
            ddl_opciones.SelectedValue = "0";
            txt_felicitaciones.Text = "";
            lb_comentario.Text = "Debes seleccionar una opción de FELICITACIÓN o agregarla manualmente";
            lb_comentario.ForeColor = System.Drawing.Color.Red;


        }

        else
        {
            guardarComentario();
            materializeCSS.crear_toast(this, "Comentario guardado con éxito", true);
            modalFelicitacionesClosed();
            ddl_comentario_felicitacion.SelectedValue = "0";
            ddl_opciones.SelectedValue = "0";
            txt_felicitaciones.Text = "";
            lb_comentario.Text = "Comentario enviado con éxito ✓.";
            lb_comentario.ForeColor = System.Drawing.Color.Green;
        }
    }



    protected void ddl_opciones_SelectedIndexChanged(object sender, EventArgs e)
    {
        string opcion = ddl_opciones.SelectedValue;

        switch (opcion)
        {

            case "quejas":

                string script_quejas = @" $(document).ready(function(event){ $('#modal_quejas').modal('open');  event.preventDefault();  });";
                ScriptManager.RegisterStartupScript(this.Page, typeof(Label), "modal_quejas", script_quejas, true);
                break;


            case "sugerencias":

                string script_sugerencias = @" $(document).ready(function(){ $('#modal_sugerencias').modal('open');    });";
                ScriptManager.RegisterStartupScript(this.Page, typeof(Label), "modal_sugerencias", script_sugerencias, true);

                break;

            case "denuncias":

                string script_denuncias = @" $(document).ready(function(){ $('#modal_denuncias').modal('open');    });";
                ScriptManager.RegisterStartupScript(this.Page, typeof(Label), "modal_denuncias", script_denuncias, true);

                break;

            case "felicitaciones":
                string script_fellicitacione = @" $(document).ready(function(){ $('#modal_felicitaciones').modal('open');    });";
                ScriptManager.RegisterStartupScript(this.Page, typeof(Label), "modal_felicitaciones", script_fellicitacione, true);
                break;

        }
    }
}

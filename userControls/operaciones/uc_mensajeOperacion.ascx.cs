using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userControls_operaciones_uc_mensajeOperacion : System.Web.UI.UserControl {

    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack) {

            if( usuarios.modoAsesorActivado() == 1) {
                summernote.Visible = true;
                content_mensajeCliente.Visible = false;

                txt_mensajeAsesor.ValidateRequestMode = ValidateRequestMode.Disabled;
                txt_mensajeAsesor.Visible = true;
                ScriptManager.RegisterStartupScript(this, typeof(Control), "summer" , @"$('.operacio_summernote').summernote({ placeholder: 'Redacta algún mensaje', tabsize: 1, height: 100 });

                                $('.operacio_summernote').on('summernote.keyup', function(we, e) {
                                    document.querySelector('#txt_mensajeAsesor').value = $('.operacio_summernote').summernote('code');
                                });", true);
                } else {

                summernote.Visible = false;
              
                txt_mensajeAsesor.Visible = false;
                content_mensajeCliente.Visible = true;
                }
            }
        }

    public string obtenerMensaje(){
        string mensaje;
        if (usuarios.modoAsesorActivado() == 1) {


            mensaje = txt_mensajeAsesor.Text;
            return mensaje;


            } else {

            mensaje = txt_mensajeCliente.Text;
            return mensaje;
            }
        
        } 
    }
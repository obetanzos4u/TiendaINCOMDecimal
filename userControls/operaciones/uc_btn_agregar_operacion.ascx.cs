using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class uc_btn_agregar_operacion : System.Web.UI.UserControl
{


    public string numero_parte {
        get { return this.hf_numero_parte.Value; }
        set { this.hf_numero_parte.Value = value; }
        }
    public string descripcion_corta {
        get { return this.hf_descripcion_corta.Value; }
        set { this.hf_descripcion_corta.Value = value; }
        }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated) {

            UP_cantidadCarrito.Visible = true;
            }



        }

   /* protected void mostrarModal(_numero_parte) { }
        protected void btn_MostrarModal_Click(object sender, EventArgs e) {

        dynamic uc_controlMDL = (dynamic)LoadControl("~/userControls/operaciones/uc_modal_agregar_operacion.ascx");
        uc_controlMDL.numero_parte = numero_parte;
        uc_controlMDL.mostrarModal(sender,e);
        //uc_controlMDL.operacionesAdd.Update();
        }
        */
    }
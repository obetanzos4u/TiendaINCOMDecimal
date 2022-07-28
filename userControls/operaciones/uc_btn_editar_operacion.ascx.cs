using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class uc_btn_editar_operacion : System.Web.UI.UserControl
{


    public string id_operacion {
        get { return this.hf_id_operacion.Value; }
        set { this.hf_id_operacion.Value = value; }
        }
    public string tipo_operacion {
        get { return this.hf_tipo_operacion.Value; }
        set { this.hf_tipo_operacion.Value = value; }
        }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (hf_tipo_operacion.Value == "pedido") {

            btn_editar_productos_operacion.Visible = false;
          
            }
       
        }



    protected void btn_editar_productos_operacion_Click(object sender, EventArgs e) {


        if(hf_tipo_operacion.Value == "cotizacion") {

            Session["cotizacion_edit_idSQL"] = hf_id_operacion.Value;
            Session["cotizacion_edit"] = true;

            //Establecemos en null los valores para pedido
            Session["pedido_edit_idSQL"] = null;
            Session["pedido_edit"] = null;

            }
        // Si es pedido
        else  {

            Session["pedido_edit_idSQL"] = true;
            Session["pedido_edit"] = hf_id_operacion.Value;


            //Establecemos en null los valores para cotizacion
            Session["cotizacion_edit_idSQL"] = null;
            Session["cotizacion_edit"] = null;
            }

        Response.Redirect("~/usuario/mi-cuenta/agregar-productos-tutorial.aspx");
        }
    }
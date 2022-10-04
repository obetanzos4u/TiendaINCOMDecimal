using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class uc_asesores_modalidad_clientes_bar : System.Web.UI.UserControl
{
    string numero_parte { get; set; }

    protected void Page_Init(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            modalidadAsesor();

        }
        else
        {

            string parameter = Request["__EVENTARGUMENT"];
            string target = Request.Form["__EVENTTARGET"];
            if (target == "ddl_asesores") establecer(parameter);
        }


    }

    protected void establecer(string id)
    {
        int id_cliente = int.Parse(id);
        privacidadAsesores.establecer_DatosCliente(id_cliente);

        modalidadAsesor();
        Response.Redirect(Request.Url.AbsoluteUri);
    }


    ///<summary>
    /// Muestra el cliente con el que se esta trabajando actualmente (modalidad asesores activada)
    ///</summary>
    protected void modalidadAsesor()
    {
        bool modalidadAsesorActivada = privacidadAsesores.modalidadAsesor();
        if (modalidadAsesorActivada)
        {
            if (!IsPostBack)
            {
                chk_modalidad_asesores.Checked = modalidadAsesorActivada;

            }
            ddl_clientes_asesores.Visible = true;
            myBtnCambiarAsesorModal.Visible = true;
            lbl_modalidad_asesores.Visible = true;
            modoAsesorUl.Visible = true;
            if (System.Web.HttpContext.Current.Session["datosCliente"] != null)
            {
                usuarios cliente = (usuarios)System.Web.HttpContext.Current.Session["datosCliente"];
                chk_modalidad_asesores.Text = cliente.email;
                //lbl_cliente.Text = cliente.email;
            }
        }
        else
        {
            lbl_modalidad_asesores.Visible = true;
            link_.Visible = false;
            btn_agregar_usuario.Visible = false;
            ddl_clientes_asesores.Visible = false;
            myBtnCambiarAsesorModal.Visible = false;
            lbl_cliente.Visible = false;
            lbl_cliente.Text = "";
            chk_salir_modalidad_asesores.Visible = false;
            modoAsesorUl.Visible = false;
        }
    }

    protected void chk_modalidad_asesores_CheckedChanged(object sender, EventArgs e)
    {
        bool chk_modalidad = chk_modalidad_asesores.Checked;
        if (chk_salir_modalidad_asesores.Checked)
        {
            chk_modalidad = false;
            modoAsesorUl.Visible = true;
        }
        System.Web.HttpContext.Current.Session["modoAsesor"] = chk_modalidad;
        privacidadAsesores.establecer_DatosCliente(((usuarios)HttpContext.Current.Session["datosUsuario"]).id);
        Session["pedido_edit_idSQL"] = null;
        Session["pedido_edit"] = null;
        Session["cotizacion_edit_idSQL"] = null;
        Session["cotizacion_edit"] = null;
        Response.Redirect(Request.Url.AbsoluteUri);
    }
}
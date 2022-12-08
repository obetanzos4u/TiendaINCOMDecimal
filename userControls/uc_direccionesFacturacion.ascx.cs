using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class uc_direccionesFacturacion : System.Web.UI.UserControl
{

    public event EventHandler eventEdit;
    public event EventHandler eventUpdate;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargar_Direcciones();

        }
        else
        {

        }
        this.eventEdit += new EventHandler(env);
        this.eventUpdate += new EventHandler(env);

    }
    protected void env(object sender, EventArgs e) { }
    protected void cargar_paises(DropDownList ddl)
    {
        paises obtener = new paises();

        ddl.DataSource = obtener.obtenerPaises();
        ddl.DataTextField = "nombre";
        ddl.DataValueField = "nombre";

        ddl.DataBind();
    }
    protected void cargar_estados(DropDownList ddl)
    {
        paises obtener = new paises();

        ddl.DataSource = obtener.obtenerEstados();
        ddl.DataTextField = "nombre";
        ddl.DataValueField = "nombre";

        ddl.DataBind();
    }

    public void cargar_Direcciones()
    {


        // Obtenemos los datos del usuario iniciado
        usuarios usuario = usuarios.modoAsesor();



        direccionesFacturacion obtener = new direccionesFacturacion();

        lv_direcciones.DataSource = obtener.obtenerDirecciones(usuario.id);
        lv_direcciones.DataBind();
        up_lv_Direcciones.Update();
    }
    protected void direccion_ItemEditing(object sender, ListViewEditEventArgs e)
    {
        int row = lv_direcciones.EditIndex = e.NewEditIndex; // Obtiene el # del elemento a editar

        cargar_Direcciones();

        ListViewItem item = this.lv_direcciones.Items[e.NewEditIndex];
        // Al recargarse la página se pierde el estado de el acordión, con esta función volvemos a abrir dicho estado
        ScriptManager.RegisterStartupScript(item, typeof(Page), "edit", "M.AutoInit();    $('.collapsible').collapsible('open', " + row + ");  ", true);

        HtmlGenericControl cont_ddl_estado = (HtmlGenericControl)item.FindControl("cont_ddl_estado");
        HtmlGenericControl cont_txt_estado = (HtmlGenericControl)item.FindControl("cont_txt_estado");

        UpdatePanel up_pais_estado = (UpdatePanel)item.FindControl("up_pais_estado");
        UpdatePanel up_regimen_fiscal = (UpdatePanel)item.FindControl("ddl_up_regimen_fiscal");
        DropDownList ddl_pais = (DropDownList)up_pais_estado.FindControl("ddl_pais");
        DropDownList ddl_estado = (DropDownList)up_pais_estado.FindControl("ddl_estado");
        DropDownList ddl_regimen_fiscal = (DropDownList)up_regimen_fiscal.FindControl("ddl_regimen_fiscal");

        cargar_paises(ddl_pais);
        cargar_estados(ddl_estado);

        Label lbl_pais = (Label)item.FindControl("lbl_pais");
        Label lbl_estado = (Label)item.FindControl("lbl_estado");
        Label lbl_regimen_fiscal = (Label)item.FindControl("lbl_regimen_fiscal");

        ddl_pais.SelectedItem.Text = lbl_pais.Text;
        ddl_regimen_fiscal.SelectedItem.Text = lbl_regimen_fiscal.Text;

        if (lbl_pais.Text == "México")
        {
            cont_txt_estado.Visible = false;
            cont_ddl_estado.Visible = true;
            ddl_estado.SelectedItem.Text = lbl_estado.Text;
        }
        else
        {
            cont_ddl_estado.Visible = false;
            cont_txt_estado.Visible = true;
        }
        eventEdit(sender, e);
    }
    protected void direccion_ItemCanceling(object sender, ListViewCancelEventArgs e)
    {
        // To switch from edit mode to display mode, set EditIndex property to -1.
        int row = e.ItemIndex;

        ListViewItem item = this.lv_direcciones.Items[row];
        // Solo para mostrar el mensaje de materialize
        UpdatePanel UpdatePanel1 = (UpdatePanel)item.FindControl("UpdatePanel1");
        lv_direcciones.EditIndex = -1;
        // Recargar LV
        cargar_Direcciones();

        // Al recargarse la página se pierde el estado de el acordión, con esta función volvemos a abrir dicho estado
        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "cancelEdit", "M.AutoInit(); $('.collapsible').collapsible('open', " + row + ");", true);
    }
    protected void direccion_ItemUpdating(object sender, ListViewUpdateEventArgs e)
    {

        ListViewItem item = this.lv_direcciones.Items[e.ItemIndex];

        // Solo para mostrar el mensaje de materialize
        UpdatePanel UpdatePanel1 = (UpdatePanel)item.FindControl("UpdatePanel1");


        HiddenField hd_id_direccion = (HiddenField)item.FindControl("hd_id_direccion");


        TextBox txt_nombre_direccion = (TextBox)item.FindControl("txt_nombre_direccion");

        TextBox txt_razon_social = (TextBox)item.FindControl("txt_razon_social");
        TextBox txt_rfc = (TextBox)item.FindControl("txt_rfc");

        TextBox txt_calle = (TextBox)item.FindControl("txt_calle");
        TextBox txt_numero = (TextBox)item.FindControl("txt_numero");
        TextBox txt_colonia = (TextBox)item.FindControl("txt_colonia");
        TextBox txt_delegacion_municipio = (TextBox)item.FindControl("txt_delegacion_municipio");
        TextBox txt_estado = (TextBox)item.FindControl("txt_estado");

        TextBox txt_codigo_postal = (TextBox)item.FindControl("txt_codigo_postal");


        DropDownList ddl_pais = (DropDownList)item.FindControl("ddl_pais");
        DropDownList ddl_estado = (DropDownList)item.FindControl("ddl_estado");
        DropDownList ddl_regimen_fiscal = (DropDownList)item.FindControl("ddl_regimen_fiscal");


        Label lbl_pais = (Label)item.FindControl("lbl_pais");


        string estado;
        string regimen_fiscal = ddl_regimen_fiscal.SelectedValue;


        if (ddl_pais.SelectedItem.Text == "México")
        {
            estado = ddl_estado.SelectedItem.Text;
        }
        else
        {
            estado = txt_estado.Text;
        }

        direccionesFacturacion direccion = new direccionesFacturacion
        {
            id = int.Parse(hd_id_direccion.Value),
            nombre_direccion = txt_nombre_direccion.Text,
            razon_social = txt_razon_social.Text,
            rfc = txt_rfc.Text,
            calle = txt_calle.Text,
            numero = txt_numero.Text,
            colonia = txt_colonia.Text,
            delegacion_municipio = txt_delegacion_municipio.Text,
            estado = estado,
            codigo_postal = txt_codigo_postal.Text,
            pais = ddl_pais.SelectedItem.Text,
            regimen_fiscal = regimen_fiscal,
        };

        if (validarCampos.direccionFacturacion(direccion, UpdatePanel1))
        {
            if (direccion.actualizarDireccion() != null)
            {
                NotiflixJS.Message(UpdatePanel1, NotiflixJS.MessageType.success, "Dirección de facturación actualizada");
                //materializeCSS.crear_toast(UpdatePanel1, "Dirección de facturación actualizada con éxito", true);
                BootstrapCSS.RedirectJs(UpdatePanel1, Request.Url.GetLeftPart(UriPartial.Authority) + "/usuario/mi-cuenta/direcciones-de-facturacion.aspx", 2000);
            }
            else
            {
                NotiflixJS.Message(UpdatePanel1, NotiflixJS.MessageType.failure, "Error al actualizar la dirección");
                //materializeCSS.crear_toast(UpdatePanel1, "Error al actualizar dirección de facturación", false);
            }
        }
        eventUpdate(sender, e);
    }
    protected void direccion_ItemDeleted(object sender, ListViewDeleteEventArgs e)
    {

        ListViewItem item = this.lv_direcciones.Items[e.ItemIndex];


        try
        {
            HiddenField hd_id_contacto = (HiddenField)item.FindControl("hd_id_contacto");

            int id = int.Parse(hd_id_contacto.Value);

            direccionesFacturacion eliminar = new direccionesFacturacion();
            if (eliminar.eliminardireccion(id))
            {

                materializeCSS.crear_toast(item, "Dirección eliminada con éxito", true);
            }
        }
        catch (Exception ex)
        {

            materializeCSS.crear_toast(item, "Error al eliminar dirección", false);
        }

        // Bind data to ListView Control.
        cargar_Direcciones();

    }



    protected void ddl_pais_SelectedIndexChanged(object sender, EventArgs e)
    {

        DropDownList ddl_pais = (DropDownList)sender;


        if (ddl_pais.SelectedItem.Text == "México")
        {
            cont_txt_estado.Visible = false;
            cont_ddl_estado.Visible = true;

            DropDownList ddl_estado = (DropDownList)ddl_pais.Parent.FindControl("ddl_estado");
            cargar_estados(ddl_estado);

        }
        else
        {
            cont_ddl_estado.Visible = false;
            cont_txt_estado.Visible = true;
        }

    }


}
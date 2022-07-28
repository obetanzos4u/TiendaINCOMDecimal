using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class uc_direccionesEnvio : System.Web.UI.UserControl {

    public event EventHandler T;

    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack) {
            cargar_DireccionesEnvio();

            } else {


            }

        this.T += new EventHandler(env);

        }
    protected void env(object sender, EventArgs e) { }

        protected void cargar_paises(DropDownList ddl) {
        paises obtener = new paises();

        ddl.DataSource = obtener.obtenerPaises();
        ddl.DataTextField = "nombre";
        ddl.DataValueField = "nombre";

        ddl.DataBind();
        }
    protected void cargar_estados(DropDownList ddl) {
        paises obtener = new paises();

        ddl.DataSource = obtener.obtenerEstados();
        ddl.DataTextField = "nombre";
        ddl.DataValueField = "nombre";

        ddl.DataBind();
        }

    public void cargar_DireccionesEnvio() {
        // Obtenemos el valor boleano si se esta trabajando en modalidad asesor
        bool modoAsesor = (bool)System.Web.HttpContext.Current.Session["modoAsesor"];

        // Obtenemos los datos del usuario iniciado
        usuarios usuario = (usuarios)System.Web.HttpContext.Current.Session["datosUsuario"];


        /* Si el modo asesor esta activo, obtiene los datos del cliente previamente obtenidos
         para calcular los precios
        */
        if (modoAsesor == true) {
            usuario = (usuarios)System.Web.HttpContext.Current.Session["datosCliente"];
            }

        direccionesEnvio obtener = new direccionesEnvio();

        lv_direcciones.DataSource = obtener.obtenerDirecciones(usuario.id);
        lv_direcciones.DataBind();
          
        up_lv_Direcciones.Update();
        }
    protected async void direccion_ItemEditing(object sender, ListViewEditEventArgs e) {

        int row = lv_direcciones.EditIndex = e.NewEditIndex; // Obtiene el # del elemento a editar

        cargar_DireccionesEnvio();

        ListViewItem item = this.lv_direcciones.Items[e.NewEditIndex];

     
        // Al recargarse la página se pierde el estado de el acordión, con esta función volvemos a abrir dicho estado
        ScriptManager.RegisterStartupScript(this.Page, typeof(Page),  Guid.NewGuid().ToString(), "M.AutoInit();    $('.collapsible').collapsible('open', " + row + ");      ", true);

      //  ScriptManager.RegisterClientScriptBlock(this.Page, typeof(Page), "alert", " console.log('hola');", true);


        HtmlGenericControl cont_ddl_estado = (HtmlGenericControl)item.FindControl("cont_ddl_estado");
        HtmlGenericControl cont_txt_estado = (HtmlGenericControl)item.FindControl("cont_txt_estado");

        HiddenField hf_IndexItem = (HiddenField)item.FindControl("hf_IndexItem");
        hf_IndexItem.Value = row.ToString();

        TextBox txt_codigo_postal = (TextBox)item.FindControl("txt_codigo_postal");
        TextBox txt_colonia = (TextBox)item.FindControl("txt_colonia");
        DropDownList ddl_colonia = (DropDownList)item.FindControl("ddl_colonia");

        UpdatePanel up_pais_estado = (UpdatePanel)item.FindControl("up_pais_estado");

        DropDownList ddl_pais = (DropDownList)up_pais_estado.FindControl("ddl_pais");
        DropDownList ddl_estado = (DropDownList)up_pais_estado.FindControl("ddl_estado");

        cargar_paises(ddl_pais);
        cargar_estados(ddl_estado);

        Label lbl_pais = (Label)item.FindControl("lbl_pais");
        Label lbl_estado = (Label)item.FindControl("lbl_estado");

        ddl_pais.SelectedItem.Text = lbl_pais.Text;

        if (lbl_pais.Text == "México") {
            cont_txt_estado.Visible = false;
            cont_ddl_estado.Visible = true;
            ddl_estado.SelectedItem.Text = lbl_estado.Text;


            } else {
            cont_ddl_estado.Visible = false;
            cont_txt_estado.Visible = true;
            }









        // Sección llenado Código Postal
        string cp = textTools.lineSimple(txt_codigo_postal.Text);
        if (!string.IsNullOrWhiteSpace(cp))
        {
            json_respuestas result = await DireccionesServiceCP.GetCodigoPostalAsync(cp);


            if (result.result == true)
            {
                dynamic Result = JsonConvert.DeserializeObject<dynamic>(result.response);

                txt_delegacion_municipio.Text = Result[0].Municipio;
                txt_ciudad.Text = Result[0].Ciudad;

                ddl_estado.SelectedValue = Result[0].Estado;
                ddl_pais.SelectedValue = Result[0].Pais;

                ddl_colonia.Items.Clear();
                foreach (var t in Result)
                {

                    string Asentamiento = t.Asentamiento;
                    ddl_colonia.Items.Add(new ListItem(Asentamiento, Asentamiento));
                }
                ddl_colonia.DataBind();


                ddl_colonia.SelectedValue = txt_colonia.Text;

                cont_ddl_estado.Visible = true;
                cont_txt_estado.Visible = false;

                EnabledCampos(item, false);
            }
            else
            {
                EnabledCampos(item, true);

                materializeCSS.crear_toast(this, result.message, false);
            }

        }























        T(this, e);
       
        }

    protected void direccion_ItemCanceling(object sender, ListViewCancelEventArgs e) {
        // To switch from edit mode to display mode, set EditIndex property to -1.
        int row = e.ItemIndex;
        
        ListViewItem item = this.lv_direcciones.Items[row];
        // Solo para mostrar el mensaje de materialize
        UpdatePanel UpdatePanel1 = (UpdatePanel)item.FindControl("UpdatePanel1");
        lv_direcciones.EditIndex = -1;
        // Recargar LV
        cargar_DireccionesEnvio();

        // Al recargarse la página se pierde el estado de el acordión, con esta función volvemos a abrir dicho estado
        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(Page), "cancelEdit", "M.AutoInit(); $('.collapsible').collapsible('open', " + row + ");", true);
        }
    protected void direccion_ItemUpdating(object sender, ListViewUpdateEventArgs e) {

        ListViewItem item = this.lv_direcciones.Items[e.ItemIndex];
        // Solo para mostrar el mensaje de materialize
        UpdatePanel UpdatePanel1 = (UpdatePanel)item.FindControl("UpdatePanel1");

        HiddenField hd_id_direccion = (HiddenField)item.FindControl("hd_id_direccion");
        TextBox txt_nombre_direccion = (TextBox)item.FindControl("txt_nombre_direccion");
        TextBox txt_calle = (TextBox)item.FindControl("txt_calle");
        TextBox txt_numero = (TextBox)item.FindControl("txt_numero");
        TextBox txt_colonia = (TextBox)item.FindControl("txt_colonia");
        TextBox txt_delegacion_municipio = (TextBox)item.FindControl("txt_delegacion_municipio");
        TextBox txt_estado = (TextBox)item.FindControl("txt_estado");
        TextBox txt_ciudad = (TextBox)item.FindControl("txt_ciudad");

        TextBox txt_codigo_postal = (TextBox)item.FindControl("txt_codigo_postal");
        TextBox txt_referencias = (TextBox)item.FindControl("txt_referencias");


        DropDownList ddl_pais = (DropDownList)item.FindControl("ddl_pais");
        DropDownList ddl_colonia = (DropDownList)item.FindControl("ddl_colonia");
        DropDownList ddl_estado = (DropDownList)item.FindControl("ddl_estado");


        Label lbl_pais = (Label)item.FindControl("lbl_pais");


        string estado;


        if (ddl_pais.SelectedItem.Text == "México") {
            estado = ddl_estado.SelectedItem.Text;
            } else {
            estado = txt_estado.Text;
            }

        direccionesEnvio direccion = new direccionesEnvio {

            id = int.Parse(hd_id_direccion.Value),
            nombre_direccion = txt_nombre_direccion.Text,
            calle = txt_calle.Text,
            numero = txt_numero.Text,
            colonia = ddl_colonia.SelectedValue,
            delegacion_municipio = txt_delegacion_municipio.Text,
            ciudad = txt_ciudad.Text,
            estado = estado,
            codigo_postal = txt_codigo_postal.Text,
            pais = ddl_pais.SelectedItem.Text,
            referencias = txt_referencias.Text
            };

        if (validarCampos.direccionEnvio(direccion, this)) {
 

            if (direccion.actualizarDireccion() != null) {
                materializeCSS.crear_toast(UpdatePanel1, "Dirección de envío actualizada con éxito", true);
                } else {
                materializeCSS.crear_toast(UpdatePanel1, "Error al actualizar dirección de envío", false);
                }

            }



        T(this, e);

        }
    protected void direccion_ItemDeleted(object sender, ListViewDeleteEventArgs e) {

        ListViewItem item = this.lv_direcciones.Items[e.ItemIndex];

        try {
         
            HiddenField hd_id_direccion = (HiddenField)item.FindControl("hd_id_direccion");

            int id = int.Parse(hd_id_direccion.Value);

            direccionesEnvio eliminar = new direccionesEnvio();
            if (eliminar.eliminardireccion(id)) {
                materializeCSS.crear_toast(item, "Dirección de envío eliminado con éxito", true);
                }
            }
        catch (Exception ex) {

            materializeCSS.crear_toast(item, "Error al eliminar Dirección de envío", false);
            }

        // Bind data to ListView Control.
        cargar_DireccionesEnvio();


        }



    protected void ddl_pais_SelectedIndexChanged(object sender, EventArgs e) {

        DropDownList ddl_pais = (DropDownList)sender;


        if (ddl_pais.SelectedItem.Text == "México") {
            cont_txt_estado.Visible = false;
            cont_ddl_estado.Visible = true;

            DropDownList ddl_municipio_estado = (DropDownList)ddl_pais.Parent.FindControl("ddl_estado");
            cargar_estados(ddl_municipio_estado);

            } else {
            cont_ddl_estado.Visible = false;
            cont_txt_estado.Visible = true;
            }

        }

    protected async void txt_codigo_postal_TextChanged(object sender, EventArgs e)
    {
        TextBox txt_codigo_postal = (TextBox)sender;

        ListViewItem item = (ListViewItem)txt_codigo_postal.Parent;
        HiddenField hf_IndexItem = (HiddenField)item.FindControl("hf_IndexItem");


        TextBox txt_colonia = (TextBox)item.FindControl("txt_colonia");
        TextBox txt_delegacion_municipio = (TextBox)item.FindControl("txt_delegacion_municipio");
        TextBox txt_estado = (TextBox)item.FindControl("txt_estado");
        DropDownList ddl_colonia = (DropDownList)item.FindControl("ddl_colonia");

        DropDownList ddl_pais = (DropDownList)item.FindControl("ddl_pais");
        DropDownList ddl_estado = (DropDownList)item.FindControl("ddl_estado");


        string cp = textTools.lineSimple(txt_codigo_postal.Text);
        if (!string.IsNullOrWhiteSpace(cp))
        {
            json_respuestas result = await DireccionesServiceCP.GetCodigoPostalAsync(cp);


            if (result.result == true)
            {
                dynamic Result = JsonConvert.DeserializeObject<dynamic>(result.response);

                txt_delegacion_municipio.Text = Result[0].Municipio;
                txt_ciudad.Text = Result[0].Ciudad;

                ddl_estado.SelectedValue = Result[0].Estado;
                ddl_pais.SelectedValue = Result[0].Pais;

                ddl_colonia.Items.Clear();
                foreach (var t in Result)
                {

                    string Asentamiento = t.Asentamiento;
                    ddl_colonia.Items.Add(new ListItem(Asentamiento, Asentamiento));
                }
                ddl_colonia.DataBind();




                cont_ddl_estado.Visible = true;
                cont_txt_estado.Visible = false;

                EnabledCampos(item, false);
            }
            else
            {
                 EnabledCampos(item,true);

                materializeCSS.crear_toast(this, result.message, false);
            }

        }
        else
        {
              EnabledCampos(item,true);
        }


        // Al recargarse la página se pierde el estado de el acordión, con esta función volvemos a abrir dicho estado
        ScriptManager.RegisterStartupScript(this.Page, typeof(Page), Guid.NewGuid().ToString(), "M.AutoInit();    $('.collapsible').collapsible('open', " + hf_IndexItem.Value + ");      ", true);
        T(this, e);
    }
    protected void EnabledCampos(ListViewItem item, bool status)
    {
        TextBox txt_colonia = (TextBox)item.FindControl("txt_colonia");
        TextBox txt_delegacion_municipio = (TextBox)item.FindControl("txt_delegacion_municipio");
        TextBox txt_estado = (TextBox)item.FindControl("txt_estado");
        DropDownList ddl_colonia = (DropDownList)item.FindControl("ddl_colonia");

        DropDownList ddl_pais = (DropDownList)item.FindControl("ddl_pais");
        DropDownList ddl_estado = (DropDownList)item.FindControl("ddl_estado");

        txt_colonia.Enabled = status;
        txt_delegacion_municipio.Enabled = status;
        txt_ciudad.Enabled = status;
        txt_estado.Enabled = status;
        ddl_pais.Enabled = status;
        ddl_estado.Enabled = status;

    }





}
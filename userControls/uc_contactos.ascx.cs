using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class uc_contactos : System.Web.UI.UserControl {

    public event EventHandler eventEditContact; 
    public event EventHandler eventUpdateContact;
   
    public bool  HeaderInfoContacto {
        get { return HeaderInfoContactos.Visible; }
        set { HeaderInfoContactos.Visible = value; }
        }


    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack) cargarContactos();

        this.eventEditContact += new EventHandler(env);
        this.eventUpdateContact += new EventHandler(env);
        }
    protected void env(object sender, EventArgs e) { }
    protected void cargarContactos() {
        usuarios datosUsuario = usuarios.modoAsesor();
        contactos obtener = new contactos();

        lv_contactos.DataSource = obtener.obtenerContactos(datosUsuario.id);
        lv_contactos.DataBind();
        up_lv_Contactos.Update();
        }
    protected void contacto_ItemEditing(object sender, ListViewEditEventArgs e) {

        int row = lv_contactos.EditIndex = e.NewEditIndex; // Obtiene el # del elemento a editar
        cargarContactos();
        ListViewItem item = lv_contactos.Items[e.NewEditIndex];

        // Al recargarse la página se pierde el estado de el acordión, con esta función volvemos a abrir dicho estado
        ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "edit", "M.AutoInit(); $('.collapsible').collapsible('open', " + row + ");", true);
        eventEditContact(sender, e);
        }

    protected void contacto_ItemCanceling(object sender, ListViewCancelEventArgs e) {
        // To switch from edit mode to display mode, set EditIndex property to -1.
        int row = e.ItemIndex;

        ListViewItem item = lv_contactos.Items[row];
 
        lv_contactos.EditIndex = -1;

        // Al recargarse la página se pierde el estado de el acordión, con esta función volvemos a abrir dicho estado
        ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "cancelEdit", "M.AutoInit(); $('.collapsible').collapsible('open', " + row + ");", true);

        // Recargar LV
        cargarContactos();

       
        }
    protected void contacto_ItemUpdating(object sender, ListViewUpdateEventArgs e) {

       
        ListViewItem item = this.lv_contactos.Items[e.ItemIndex];

        UpdatePanel UpdatePanel1 = (UpdatePanel)item.FindControl("UpdatePanel1");

        HiddenField hd_id_contacto = (HiddenField)item.FindControl("hd_id_contacto");
        TextBox txt_email = (TextBox)item.FindControl("txt_email");
        TextBox txt_nombre = (TextBox)item.FindControl("txt_nombre");
        TextBox txt_apellido_paterno = (TextBox)item.FindControl("txt_apellido_paterno");
        TextBox txt_apellido_materno = (TextBox)item.FindControl("txt_apellido_materno");
        TextBox txt_telefono = (TextBox)item.FindControl("txt_telefono");
        TextBox txt_celular = (TextBox)item.FindControl("txt_celular");

        // Iniciamos la validación de campos
        bool nombres = validarCampos.nombres(txt_nombre, txt_apellido_paterno, txt_apellido_paterno, this);
        bool email = validarCampos.email(txt_email.Text, this);
        bool telefonos = validarCampos.telefonos(txt_telefono, txt_celular, this);

        // Si todo es correcto, procedemos a actualizar
        if (nombres && email && telefonos) {
            contactos actualizar = new contactos();

            actualizar.id = int.Parse(hd_id_contacto.Value);
            actualizar.email = txt_email.Text;
            actualizar.nombre = txt_nombre.Text;
            actualizar.apellido_paterno = txt_apellido_paterno.Text;
            actualizar.apellido_materno = txt_apellido_materno.Text;
            actualizar.telefono = txt_telefono.Text;
            actualizar.celular = txt_celular.Text;



            if (actualizar.actualizarContacto() != null) {
                materializeCSS.crear_toast(UpdatePanel1, "Contacto actualizado con éxito", true);


                } else {
                materializeCSS.crear_toast(UpdatePanel1, "Error al actualizar contacto", false);
                }

            }

      
   
        
        eventUpdateContact(sender, e);
        }
    protected void contacto_ItemDeleted(object sender, ListViewDeleteEventArgs e) {
        ListViewItem item = this.lv_contactos.Items[e.ItemIndex];

        try {
        

            HiddenField hd_id_contacto = (HiddenField)item.FindControl("hd_id_contacto");

            int id = int.Parse(hd_id_contacto.Value);

            contactos eliminar = new contactos();
            if (eliminar.eliminarContacto(id)) {


                materializeCSS.crear_toast(item, "Contacto eliminado con éxito", true);
                }
            }
        catch (Exception ex) {

            materializeCSS.crear_toast(item, "Error al eliminar contacto", false);
            }

        // Bind data to ListView Control.
        cargarContactos();

        }

    protected void btn_crearContacto_Click(object sender, EventArgs e) {

      
      
        // Iniciamos la validación de campos
        bool nombres = validarCampos.nombres(txt_nombre, txt_apellido_paterno, txt_apellido_paterno, up_crearContacto);
        bool email = validarCampos.email(txt_email.Text, up_crearContacto);
        bool telefonos = validarCampos.telefonos(txt_telefono, txt_celular, up_crearContacto);
        usuarios datosUsuario = usuarios.modoAsesor();

        // Si todo es correcto, procedemos a actualizar
        if (nombres && email && telefonos) {
            contactos actualizar = new contactos {
                id_cliente = datosUsuario.id,
                email = txt_email.Text,
                nombre = txt_nombre.Text,
                apellido_paterno = txt_apellido_paterno.Text,
                apellido_materno = txt_apellido_materno.Text,
                telefono = txt_telefono.Text,
                celular = txt_celular.Text
                };
            
            if (actualizar.crearContacto() != null) {
                materializeCSS.crear_toast(this.Page, "Contacto creado con éxito", true);
                cargarContactos();

                } else {
                materializeCSS.crear_toast(this.Page, "Error al crear contacto", false);
                }
          
            } 
     

        }
    }
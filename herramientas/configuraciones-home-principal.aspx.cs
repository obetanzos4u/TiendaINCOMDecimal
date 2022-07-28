using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class herramientas_configuraciones_home_principal : System.Web.UI.Page {

    /// <summary>
    /// Se refiere a la sección del slider a editar en base la base de datos
    /// </summary>
    private string seccionSlider = "sliderHome";


    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack) {
            cargarSliderLV();
            cargarImagenesLV();
            cargarDDL();
        }
    }
    protected void cargarImagenesLV() {
        Dictionary<string, string> img = new Dictionary<string, string>();

        foreach (string v in configuracion_sliders.obtenerListadoDeImagenesSlider("sliderHome"))
            img.Add(v, v);


        lv_galeriaDeImagenes.DataSource = img;
        lv_galeriaDeImagenes.DataBind();
    }
    protected void cargarSliderLV() {
        lv_imagenes.DataSource = configuracion_sliders.obtenerImagenesSlider("sliderHome");
        lv_imagenes.DataBind();
        up_Lv_Slider.Update();
    }

    protected void cargarDDL() {

        ddl_imagen.Items.Clear();
        string[] archivosSlider = configuracion_sliders.obtenerListadoDeImagenesSlider("sliderHome");

        if (archivosSlider != null && archivosSlider.Length >= 1) {
            foreach (string archivo in archivosSlider) {

                ListItem item = new ListItem();
                item.Value = archivo;
                item.Text = archivo;
                ddl_imagen.Items.Add(item);
            }

        } else {
            ListItem item = new ListItem();
            item.Value = "";
            item.Text = "No hay imagenes disponibles";
            item.Selected = true;
            item.Enabled = false;
            ddl_imagen.Items.Add(item);
        }
    }

    protected void btn_cargarImagenSlider_Click(object sender, EventArgs e) {

        if (fu_imagenSlider.HasFile) {
            string filePath = configuracion_sliders.directorioSliderAbsoluto + Path.GetFileNameWithoutExtension(fu_imagenSlider.PostedFile.FileName);

            if (!archivosManejador.validarExistencia(filePath)) {

                //fu_imagenSlider.SaveAs(filePath);


                var imgStream = fu_imagenSlider.PostedFile.InputStream;

              var resultOperation =  imgFormatUtilities.Convert(imgStream, filePath, 50);
                if(resultOperation.result == false) {

                    materializeCSS.crear_toast(up_agregarSlider, "Error al guardar imagen", false);
                    return;
                }

                cargarDDL();
                materializeCSS.crear_toast(up_agregarSlider, "Imagen cargada con éxito", true);
                cargarImagenesLV();


            } else {
                materializeCSS.crear_toast(up_agregarSlider, "La imagen ya existe", false);
            }
        } else materializeCSS.crear_toast(up_agregarSlider, "Debes seleccionar un archivo", false);

    }
    protected void btn_agregarSlider_Click(object sender, EventArgs e) {

        if (ddl_imagen.SelectedValue != "") {
            model_configuracion_sliders sliderAdd = new model_configuracion_sliders();

            sliderAdd.seccion = seccionSlider;
            sliderAdd.titulo = txt_titulo.Text;
            sliderAdd.descripcion = txt_descripcion.Text;
            sliderAdd.nombreArchivo = ddl_imagen.SelectedValue;
            sliderAdd.link = txt_link.Text;
            sliderAdd.duracion = float.Parse(ddl_duracion.SelectedValue);
            sliderAdd.posicion = int.Parse(ddl_posicion.SelectedValue);
            sliderAdd.opciones = txt_opciones.Text;
            sliderAdd.activo = chk_activa.Checked ? 1 : 0;

            if (configuracion_sliders.insertarSlider(sliderAdd)) {
                materializeCSS.crear_toast(up_agregarSlider, "Slider agregado con éxito", true);
                cargarSliderLV();
            } else {

                materializeCSS.crear_toast(up_agregarSlider, "Error al agregar slider", false);
            }
        } else materializeCSS.crear_toast(up_agregarSlider, "Debes seleccionar una imagen", false);

        up_agregarSlider.Update();
    }

    protected void lv_imagenes_ItemDataBound(object sender, ListViewItemEventArgs e) {


        HiddenField idSlider = (HiddenField)e.Item.FindControl("hf_idSlider");
        CheckBox chk_activo = (CheckBox)e.Item.FindControl("chk_activo");

        DataRowView rowView = (DataRowView)e.Item.DataItem;

        bool activo = rowView["activo"].ToString() == "1" ? true : false;

        chk_activo.Checked = activo;


    }


    protected void chk_activo_CheckedChanged(object sender, EventArgs e) {
        CheckBox chk_activo = (CheckBox)sender;
        HiddenField hf_idSlider = (HiddenField)chk_activo.Parent.FindControl("hf_idSlider");

        int activo = chk_activo.Checked == true ? 1 : 0;

        string respuesta = chk_activo.Checked == true ? "activado" : "desactivado";

        if (configuracion_sliders.actualizarSliderActivarDesactivar(hf_idSlider.Value, activo)) {
            materializeCSS.crear_toast(up_agregarSlider, "Slider " + respuesta + " con éxito", true);
        } else {

            materializeCSS.crear_toast(up_agregarSlider, "Error al cambiar", false);
        }

    }

    protected void btn_editSliderModal_Click(object sender, EventArgs e) {

        LinkButton btn_editSlider = (LinkButton)sender;
        HiddenField hf_idSlider = (HiddenField)btn_editSlider.Parent.FindControl("hf_idSlider");



        DataTable dtSlider = configuracion_sliders.obtenerSlider(seccionSlider, hf_idSlider.Value);

        ddl_imagenEdit.DataSource = configuracion_sliders.obtenerListadoDeImagenesSlider("sliderHome");
        ddl_imagenEdit.DataBind();

        hf_idSliderEdit.Value = dtSlider.Rows[0]["id"].ToString();
        txt_tituloEdit.Text = dtSlider.Rows[0]["titulo"].ToString();
        txt_descripcionEdit.Text = dtSlider.Rows[0]["descripcion"].ToString();
        try { 
        ddl_imagenEdit.SelectedValue = dtSlider.Rows[0]["nombreArchivo"].ToString();
        }
        catch (Exception ex) { }
        txt_linkEdit.Text = dtSlider.Rows[0]["link"].ToString();
        try {
            ddl_duracionEdit.SelectedValue = dtSlider.Rows[0]["duracion"].ToString();
        } catch(Exception ex) { }
        ddl_posicionEdit.SelectedValue = dtSlider.Rows[0]["posicion"].ToString();
        txt_opcionesEdit.Text = dtSlider.Rows[0]["opciones"].ToString();
        chk_activoEdit.Checked = dtSlider.Rows[0]["activo"].ToString() == "1" ? true : false;

        string script = "$(document).ready(function(){     $('#modal_editar_slider').modal('open');      });";

        ScriptManager.RegisterStartupScript(this, typeof(Control), "modal_editar_slider", script, true);



    }

    protected void btn_editarSlider_Click(object sender, EventArgs e) {

        if (ddl_imagen.SelectedValue != "") {
            model_configuracion_sliders sliderAdd = new model_configuracion_sliders();

            sliderAdd.id = int.Parse(hf_idSliderEdit.Value);
            sliderAdd.seccion = seccionSlider;
            sliderAdd.titulo = txt_tituloEdit.Text;
            sliderAdd.descripcion = txt_descripcionEdit.Text;
            sliderAdd.nombreArchivo = ddl_imagenEdit.SelectedValue;
            sliderAdd.link = txt_linkEdit.Text;
            sliderAdd.duracion = float.Parse(ddl_duracionEdit.SelectedValue);
            sliderAdd.posicion = int.Parse(ddl_posicionEdit.SelectedValue);
            sliderAdd.opciones = txt_opcionesEdit.Text;
            sliderAdd.activo = chk_activoEdit.Checked ? 1 : 0;

            if (configuracion_sliders.actualizarSlider(sliderAdd)) {
                materializeCSS.crear_toast(up_agregarSlider, "Slider editado con éxito", true);
                cargarSliderLV();
                 
                cargarDDL();

            } else {

                materializeCSS.crear_toast(up_agregarSlider, "Error al editar slider", false);
            }
        } else materializeCSS.crear_toast(up_agregarSlider, "Debes seleccionar una imagen", false);
        up_agregarSlider.Update();
    }

    protected void btn_eliminar_imagenh_Click(object sender, EventArgs e) {
        LinkButton s = sender as LinkButton;

        HiddenField hf_imgFileName = (HiddenField)s.Parent.FindControl("hf_imgFileName");

        if (configuracion_sliders.eliminarImagen("sliderHome", hf_imgFileName.Value))
            materializeCSS.crear_toast(this, "Eliminado con éxito", true);
        else materializeCSS.crear_toast(this, "Error al eliminar", false);

        cargarImagenesLV();
            
    }

    protected void btn_eliminarSlider_Click(object sender, EventArgs e) {
        LinkButton s = sender as LinkButton;
        HiddenField hf_idSlider = (HiddenField)s.Parent.FindControl("hf_idSlider");
        if (configuracion_sliders.eliminarSlider(int.Parse(hf_idSlider.Value))) {
            materializeCSS.crear_toast(this, "Eliminado con éxito", true);
        } else {
            materializeCSS.crear_toast(this, "Error al eliminar", false);
        }
        cargarSliderLV();
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class herramientas_editar_producto : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {

        if (!IsPostBack) {

        }

        }


    protected void actualizar_campo_TextChanged(object sender, EventArgs e) {
        var permiso = privacidadPaginas.validarPermisoSeccion("editar_usuario", usuarios.userLogin().id);

        if (permiso.result == false)
        {
            materializeCSS.crear_toast(this, permiso.message, permiso.result);
            return;
        }
        TextBox txt_campo = (sender) as TextBox;
        string campo = txt_campo.ID.Replace("txt_", "");
        string valor_campo = textTools.lineSimple((txt_campo).Text);

        bool resultado = usuarios.cambiar_campo_usuario(txt_email.Text, campo, valor_campo);

        if (resultado) {
            materializeCSS.crear_toast(up_informacion_personal, "Campo actualizado con éxito", true);
        } else {
            materializeCSS.crear_toast(up_informacion_personal, "Error al actualizar campo", false);
        }

        up_informacion_personal.Update();
        up_roles_privacidad.Update();
    }

 

    protected void ddl_actualizarCampo_SelectedIndexChanged(object sender, EventArgs e) {
        var permiso = privacidadPaginas.validarPermisoSeccion("editar_usuario", usuarios.userLogin().id);

        if (permiso.result == false)
        {
            materializeCSS.crear_toast(this, permiso.message, permiso.result);
            return;
        }


        DropDownList ddl_campo = (sender) as DropDownList;
        string campo = ddl_campo.ID.Replace("ddl_", "");
        string valor_campo = textTools.lineSimple(ddl_campo.SelectedValue);

        if(campo == "rango" && txt_email.Text == "cmiranda@it4u.com.mx") {
            materializeCSS.crear_toast(up_informacion_personal, "No es posible actualizar este usuario de rango", false);
           
        } else { 

        bool resultado = usuarios.cambiar_campo_usuario(txt_email.Text, campo, valor_campo);

        if (resultado) {
            materializeCSS.crear_toast(up_informacion_personal, "Campo actualizado con éxito", true);
        } else {
            materializeCSS.crear_toast(up_informacion_personal, "Error al actualizar campo", false);
        }
        }
        up_informacion_personal.Update();
        up_roles_privacidad.Update();
    }
    protected void txt_search_usuario_TextChanged(object sender, EventArgs e) {

 
        string txt_campo = textTools.lineSimple(((sender) as TextBox).Text);
        usuarios usuario = usuarios.recuperar_DatosUsuario(txt_campo);

        if(usuario != null) {
            txt_id.Text = usuario.id.ToString();
            lbl_registrado_por.Text = usuario.registrado_por;
             lbl_fecha_registro.Text = usuario.fecha_registro.ToString();
            txt_nombre.Text = usuario.nombre;
            txt_apellido_paterno.Text = usuario.apellido_paterno;
            txt_apellido_materno.Text = usuario.apellido_materno;
            txt_email.Text = usuario.email;

            ddl_tipo_de_usuario.SelectedValue = usuario.tipo_de_usuario;
            var cuenta_activa = usuario.cuenta_activa;
            if (string.IsNullOrEmpty(cuenta_activa)) {
                ddl_cuenta_activa.SelectedValue = "";
            }
            else if(cuenta_activa =="True")
            {
                ddl_cuenta_activa.SelectedValue = "1";
            }
            else if (cuenta_activa == "False")
            {
                ddl_cuenta_activa.SelectedValue = "0";
            }
            ddl_rango.SelectedValue =  usuario.rango.ToString();
            ddl_departamento.SelectedValue = usuario.departamento;
            ddl_grupoPrivacidad.SelectedValue = usuario.grupoPrivacidad;
            ddl_perfil_cliente.SelectedValue = usuario.perfil_cliente;
            txt_id_cliente.Text = usuario.idSAP;
            txt_rol_precios_multiplicador.Text = usuario.rol_precios_multiplicador;
            txt_rol_productos.Text = usuario.rol_productos;
            txt_rol_categorias.Text = textTools.arrayToString(usuario.rol_categorias);


            txt_asesor_base.Text = usuario.asesor_base;
            txt_grupo_asesor.Text = usuario.grupo_asesor;
            txt_asesor_adicional.Text = textTools.arrayToString(usuario.asesor_adicional);
            txt_grupo_asesores_adicional.Text = textTools.arrayToString(usuario.grupo_asesores_adicional);
            txt_grupo_usuario.Text = usuario.grupo_usuario;
            txt_ultimo_inicio_sesion.Text = usuario.ultimo_inicio_sesion.ToString();
            txt_fecha_nacimiento.Text = usuario.fecha_nacimiento.ToString();
            lbl_registrado_por.Text = usuario.registrado_por;


            obtener_permisos_app();
        } else {
            foreach(Control c in up_informacion_personal.Controls[0].Controls) {

                Type type = c.GetType();
                if (c.GetType().Name == "TextBox") {
                    (c as TextBox).Text = "";
                }
            }
            ddl_cuenta_activa.SelectedValue = "";
            materializeCSS.crear_toast(up_informacion_personal, "No se encontró ningún usuario con el término de búsqueda", false);
        }
        up_informacion_personal.Update();
        up_roles_privacidad.Update();
        up_permisos_aplicacion.Update();
    }


    protected void obtener_permisos_app()
    {
      

        if (!string.IsNullOrEmpty(txt_id.Text)){

            int idUsuario = int.Parse(txt_id.Text);
            var permisos_app = privacidadPaginas.obtenerPermisos(idUsuario);

            if(permisos_app.result == true) {
            lv_permisos_app.DataSource = permisos_app.response;
                lv_permisos_app.DataBind();
            }
            else
            {
                materializeCSS.crear_toast(up_permisos_aplicacion, permisos_app.message, permisos_app.result);

            }
        }
       

    }
    protected void btn_agregar_permiso_app_Click(object sender, EventArgs e)
    {
        usuarios userLogin = usuarios.userLogin();
        if (userLogin.email != "cmiranda@it4u.com.mx" || userLogin.email != "rpreza@incom.mx")
        {
            var permiso = privacidadPaginas.validarPermisoSeccion("editar_permisos_usuario_aplicacion", usuarios.userLogin().id);

            if (permiso.result == false)
            {
                materializeCSS.crear_toast(this, permiso.message, permiso.result);
                return;
            }
        }
        string seccion = ddl_permisos_app_secciones_pagina.SelectedValue;
       var result = privacidadPaginas.establecerPermisoAppSeccion(seccion, chk_permisos_app_permitir.Checked, int.Parse( txt_id.Text));

        materializeCSS.crear_toast(up_permisos_aplicacion, result.message, result.result);
        obtener_permisos_app();
        up_permisos_aplicacion.Update();
    }

    protected void chk_permiso_app_permitir_CheckedChanged(object sender, EventArgs e)
    {
        usuarios userLogin = usuarios.userLogin();
       if(userLogin.email != "cmiranda@it4u.com.mx") { 
        var permiso = privacidadPaginas.validarPermisoSeccion("editar_permisos_usuario_aplicacion", userLogin.id);

        if (permiso.result == false)
        {
            materializeCSS.crear_toast(this, permiso.message, permiso.result);
            return;
        }
        }

        CheckBox chk_permiso_app_permitir = (CheckBox)sender;
        ListViewItem lvItem = (ListViewItem)chk_permiso_app_permitir.NamingContainer;
        Label lbl_seccion_pagina = (Label)lvItem.FindControl("lbl_seccion_pagina");

        var result = privacidadPaginas.establecerPermisoAppSeccion(lbl_seccion_pagina.Text, chk_permiso_app_permitir.Checked, int.Parse(txt_id.Text));
        materializeCSS.crear_toast(up_permisos_aplicacion, result.message, result.result);



        obtener_permisos_app();
        up_permisos_aplicacion.Update();
    }

    protected void ddl_cuenta_activa_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl_campo = (sender) as DropDownList;
        string campo = ddl_campo.ID.Replace("ddl_", "");
        string valor_campo = textTools.lineSimple(ddl_campo.SelectedValue);

        if (campo == "rango" && txt_email.Text == "cmiranda@it4u.com.mx")
        {
            materializeCSS.crear_toast(up_informacion_personal, "No es posible actualizar este usuario de rango", false);

        }
        else
        {

            bool resultado = usuarios.cambiar_campo_usuario(txt_email.Text, campo, valor_campo);

            if (resultado)
            {
                materializeCSS.crear_toast(up_informacion_personal, "Campo actualizado con éxito", true);
            }
            else
            {
                materializeCSS.crear_toast(up_informacion_personal, "Error al actualizar campo", false);
            }
        }
        up_informacion_personal.Update();
        up_roles_privacidad.Update();
    }
}
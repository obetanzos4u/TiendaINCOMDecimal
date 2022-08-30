using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

public partial class uc_admin_bar_button : System.Web.UI.UserControl
{
    string numero_parte { get; set; }

    protected void Page_Init(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            mostrarBar();
            PermisosMenu();
        }
        else
        {

        }


    }

    protected void PermisosMenu()
    {

        var establecer_tipo_de_cambio = privacidadPaginas.validarPermisoSeccion("establecer_tipo_de_cambio", usuarios.userLogin().id);
        var reportes_operaciones_xls = privacidadPaginas.validarPermisoSeccion("reportes_operaciones_xls", usuarios.userLogin().id);
        var editar_producto_tienda = privacidadPaginas.validarPermisoSeccion("editar_producto_tienda", usuarios.userLogin().id);
        var admin_slider_home = privacidadPaginas.validarPermisoSeccion("admin_slider_home", usuarios.userLogin().id);
        var editar_usuario = privacidadPaginas.validarPermisoSeccion("editar_usuario", usuarios.userLogin().id);
        var agregar_producto_a_pedido = privacidadPaginas.validarPermisoSeccion("agregar_producto_a_pedido", usuarios.userLogin().id);
        var configurar_API_calculo_envios = privacidadPaginas.validarPermisoSeccion("configurar_API_calculo_envios", usuarios.userLogin().id);
        var log_errores_sql = privacidadPaginas.validarPermisoSeccion("log_errores_sql", usuarios.userLogin().id);
        var cargar_pesos_y_medidas = privacidadPaginas.validarPermisoSeccion("cargar_pesos_y_medidas", usuarios.userLogin().id);
        var cargar_precios_fantasma = privacidadPaginas.validarPermisoSeccion("cargar_precios_fantasma", usuarios.userLogin().id);
        var cargar_multimedia = privacidadPaginas.validarPermisoSeccion("cargar_multimedia", usuarios.userLogin().id);
        var cargar_productos_cantidad_maxima_venta = privacidadPaginas.validarPermisoSeccion("cargar_productos_cantidad_maxima_venta", usuarios.userLogin().id);
        var fast_admin_precios = privacidadPaginas.validarPermisoSeccion("fast_admin_precios", usuarios.userLogin().id);
        var admin_pedidos = privacidadPaginas.validarPermisoSeccion("admin_pedidos", usuarios.userLogin().id);
        var cargar_info_productos_tienda = privacidadPaginas.validarPermisoSeccion("cargar_info_productos_tienda", usuarios.userLogin().id);






        if (establecer_tipo_de_cambio.result == false) link_tipo_de_cambio.Visible = false;
        if (reportes_operaciones_xls.result == false) link_XLS_export.Visible = false;
        if (editar_producto_tienda.result == false) link_editar_producto.Visible = false;
        if (admin_slider_home.result == false) link_slider_home.Visible = false;
        if (editar_usuario.result == false) link_editar_usuario.Visible = false;
        if (agregar_producto_a_pedido.result == false) link_agregar_producto_a_pedido.Visible = false;
        if (configurar_API_calculo_envios.result == false) link_api_calculo_envios.Visible = false;
        if (log_errores_sql.result == false) link_log_errores_sql.Visible = false;
        if (cargar_pesos_y_medidas.result == false) link_cargarPesosyMedidas.Visible = false;
        if (cargar_precios_fantasma.result == false) link_precios_fantasma.Visible = false;
        if (cargar_multimedia.result == false) link_cargar_multimedia.Visible = false;
        if (cargar_productos_cantidad_maxima_venta.result == false) link_cargar_productos_cantidad_maxima_venta.Visible = false;
        if (fast_admin_precios.result == false) link_fast_admin_precios.Visible = false;
        if (admin_pedidos.result == false) link_admin_pedidos.Visible = false;

        if (cargar_info_productos_tienda.result == false) link_cargaXLS.Visible = false;
    }
    private void mostrarBar()
    {
        string dominio = Request.Url.GetLeftPart(UriPartial.Authority) + "/";
        usuarios usuarioLogin = usuarios.userLogin();

        if (usuarioLogin != null && usuarioLogin.rango >= 2)
        {
            content_btn_admin.Visible = true;
            adminBar.Visible = true;
            img_usuario.ImageUrl = dominio + "/img/asesores/" + Regex.Replace(usuarioLogin.email, "@.*", "") + ".jpg";

            WebRequest wrGetURL;
            wrGetURL = WebRequest.Create(img_usuario.ImageUrl);

            Stream objStream;
            try
            {
                objStream = wrGetURL.GetResponse().GetResponseStream();
                HttpWebResponse response = (HttpWebResponse)wrGetURL.GetResponse();
                if (response.ContentType != "image/jpeg")
                {
                    usuarios userActivo = usuarios.modoAsesor();
                    img_usuario.ImageUrl = $"https://ui-avatars.com/api/?name={userActivo.nombre}+{userActivo.apellido_paterno}&background=fff&color=000&rounded=true&format=svg";
                }
            }
            catch (Exception ex)
            {
                usuarios userActivo = usuarios.modoAsesor();
                img_usuario.ImageUrl = $"https://ui-avatars.com/api/?name={userActivo.nombre}+{userActivo.apellido_paterno}&background=fff&color=000&rounded=true&format=svg";
                Console.WriteLine(ex.Message);
            }


            lbl_nombre.Text = usuarioLogin.nombre + " " + usuarioLogin.apellido_paterno;
            lbl_usuario_email.Text = usuarioLogin.email;
            link_XLS_export.NavigateUrl = dominio + "herramientas/reportes/reportes-export.aspx";
            link_cargaXLS.NavigateUrl = dominio + "herramientas/xls-import/xls-import.aspx";
            link_tipo_de_cambio.NavigateUrl = dominio + "herramientas/configuraciones-operaciones.aspx";
            link_slider_home.NavigateUrl = dominio + "herramientas/configuraciones-home-principal.aspx";
            link_editar_producto.NavigateUrl = dominio + "herramientas/editar-producto.aspx";
            link_editar_usuario.NavigateUrl = dominio + "herramientas/editar-usuario.aspx";
            link_api_calculo_envios.NavigateUrl = dominio + "herramientas/configuraciones-calculo-envios.aspx";
            link_agregar_producto_a_pedido.NavigateUrl = dominio + "herramientas/agregar-producto-pedido.aspx";
            link_log_errores_sql.NavigateUrl = dominio + "herramientas/logs-sql.aspx";
            link_cargarPesosyMedidas.NavigateUrl = dominio + "herramientas/cargar-pesos-y-medidas.aspx";
            link_precios_fantasma.NavigateUrl = dominio + "herramientas/cargar-precios-fantasma.aspx";
            link_cargar_multimedia.NavigateUrl = dominio + "herramientas/cargar-imagenes.aspx";
            link_cargar_productos_cantidad_maxima_venta.NavigateUrl = dominio + "herramientas/cargar-bloqueo-productos-disponible.aspx";
            link_admin_pedidos.NavigateUrl = dominio + "herramientas/reporte-pedidos.aspx";
            link_fast_admin_precios.NavigateUrl = dominio + "herramientas/admin-precios.aspx";
        }
        else
        {
            content_btn_admin.Visible = false;
            adminBar.Visible = false;
        }
    }
}
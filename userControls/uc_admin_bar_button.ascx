<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_admin_bar_button.ascx.cs" Inherits="uc_admin_bar_button" %>

<ul id="adminBar" visible="false" runat="server" class="adminBar  side-nav sidenav no-autoinit">
    <li>
        <div class="user-view">
            <div class="background">
                <img src="/img/webUI/bg_sideBarAdmin.jpg" />
            </div>
            <asp:Image ID="img_usuario" class="circle" runat="server" />
            <asp:Label ID="lbl_nombre" class="white-text name is-select-none" runat="server"></asp:Label>
            <asp:Label ID="lbl_usuario_email" class="white-text email" runat="server"></asp:Label>
        </div>
    </li>


    <li>
        <asp:HyperLink ID="link_fast_admin_precios" runat="server"><i class="material-icons">arrow_downward</i>Fast Admin precios</asp:HyperLink>
    </li>

    <li>
        <asp:HyperLink ID="link_precios_fantasma" runat="server"><i class="material-icons">attach_money</i>Cargar precios fantasma</asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_cargaXLS" runat="server"><i class="material-icons">description</i>Carga XLS Productos</asp:HyperLink>
    </li>

    <li>
        <asp:HyperLink ID="link_cargarPesosyMedidas" runat="server">
                <i class="material-icons">airport_shuttle</i>Cargar pesos y medidas</asp:HyperLink>

        <div class="divider"></div>
    </li>
    <li>
        <asp:HyperLink ID="link_tipo_de_cambio" runat="server"><i class="material-icons">attach_money</i>Tipo de Cambio</asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_admin_pedidos" runat="server"><i class="material-icons">arrow_downward</i>Admin. Pedidos </asp:HyperLink>
    </li>
    <li>
        <div class="divider"></div>
    </li>
    <li>
        <asp:HyperLink ID="link_cargar_multimedia" runat="server"><i class="material-icons">image</i>Cargar multimedia</asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_slider_home" runat="server"><i class="material-icons">burst_mode</i>Admin. Slider Home</asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_editar_producto" runat="server"><i class="material-icons">edit</i>Editar Producto</asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_editar_usuario" runat="server"><i class="material-icons">perm_identity</i>Editar Usuario</asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_agregar_producto_a_pedido" runat="server"><i class="material-icons">shopping_basket</i>
                Agregar producto a pedido</asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_cargar_productos_cantidad_maxima_venta" runat="server"><i class="material-icons">assignment_turned_in</i>
                 Bloqueo venta productos</asp:HyperLink>
    </li>


    <li>
        <asp:HyperLink ID="link_api_calculo_envios" runat="server"><i class="material-icons">local_shipping</i>API cálculo envios</asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_log_errores_sql" runat="server"><i class="material-icons">error</i>Log SQL</asp:HyperLink>
    </li>


    <li>
        <asp:HyperLink ID="link_XLS_export" runat="server"><i class="material-icons">arrow_downward</i>Reportes XLS Cotizaciones</asp:HyperLink>
    </li>
    <li>
        <div class="divider"></div>
    </li>
    <li><a class="subheader">Opciones</a></li>
    <li><a class="waves-effect" href="<%=Request.Url.GetLeftPart(UriPartial.Authority) %>">Regresar al Home</a></li>
</ul>

<div id="content_btn_admin" runat="server" visible="false" style="padding: 8px 10px; float: left; z-index: 99997; background: white; position: sticky; top: 0px;">
    <a id="btn_admin" href="#!" data-target='<%=adminBar.ClientID %>' style="margin-left: 10px;" class="sidenav-trigger ">
        <i style="line-height: 36px !important;" class="material-icons">settings</i>
    </a>
</div>
<script>

    document.addEventListener('DOMContentLoaded', function () {
        var elems = document.querySelectorAll('.sidenav');
        var instances = M.Sidenav.init(elems, null);
    });

</script>
<style>
    .select2-container--open {
        z-index: 9999999 !important;
    }
</style>

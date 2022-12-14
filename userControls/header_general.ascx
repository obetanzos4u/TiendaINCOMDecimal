<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header_general.ascx.cs" Inherits="menuPrincipal" %>
<%@ Register Src="~/userControls/menu_principal.ascx" TagName="menuHeaderPrincipal" TagPrefix="uc_menu" %>
<%@ Register Src="~/userControls/uc_asesores_modalidad_clientes_bar.ascx" TagName="modAsesor" TagPrefix="uc_bar" %>
<%@ Register Src="~/userControls/uc_admin_bar_button.ascx" TagName="adminBar" TagPrefix="uc_bar" %>
<%@ Register Src="~/userControls/buscador.ascx" TagName="buscador" TagPrefix="uc_buscador" %>
<%@ Register Src="~/userControls/menu_Carrito.ascx" TagName="btnCarrito" TagPrefix="uc_carrito" %>
<%@ Register Src="~/userControls/uc_DireccionEnvioPredeterminada.ascx" TagName="DireccionEnvio" TagPrefix="uc_Envio" %>

<!-- sidenav MÓVIL -->
<ul id='menu_usuario_movil' class="sidenav">
    <li>
        <a class="home_menu" href="/">
            <img id="home-icon" alt="icono de casita" src="https://www.incom.mx/img/webUI/newdesign/home-icon.svg" />
            Inicio
        </a>
    </li>
    <li>
        <div class="divider"></div>
    </li>
    <%--    #region Botones de usuario--%>
    <asp:LoginView ID="LoginView2" runat="server">
        <LoggedInTemplate>
            <li>
                <asp:HyperLink ID="miCuenta" ToolTip="Mi cuenta" class="micuenta_menu" NavigateUrl="~/usuario/mi-cuenta/mi-cuenta.aspx"
                    runat="server">
                    <img id="user-icon" alt="icono cuenta de usuario" src="https://www.incom.mx/img/webUI/newdesign/mi_cuenta.svg"/>
                    Mi cuenta
                </asp:HyperLink>
            </li>
            <li>
                <a id="item-mis_compras" href="/usuario/mi-cuenta/pedidos.aspx" class="grey-text text-darken-3">
                    <img id="shopping-bag" alt="Bolsa de compras" src="https://www.incom.mx/img/webUI/newdesign/Shopping-bag.jpg" />Mis compras</a>
            </li>
            <li>
                <a href="/usuario/mi-cuenta/cotizaciones.aspx" class="grey-text text-darken-3">
                    <img id="ticket-icon" alt="icono de ticket de cotización" src="https://www.incom.mx/img/webUI/newdesign/cotizacion-icon.svg" />
                    Pedidos cotizados
                </a>
            </li>
        </LoggedInTemplate>
        <AnonymousTemplate>
            <li>
                <a title="Crear cuenta" class="crear_cuenta_menu" href='<%= ResolveUrl("~/registro-de-usuario.aspx") %>'>
                    <img id="crear_cuenta-icon" alt="botón de crear cuenta" src="https://www.incom.mx/img/webUI/newdesign/add_user_icon.svg" />
                    <p>Crear cuenta</p>
                </a>
            </li>
        </AnonymousTemplate>
    </asp:LoginView>
    <%--    #endregion--%>

    <li>
        <asp:LoginStatus ID="LoginStatus2" ToolTip="Sesión de usuario" runat="server" LoginText="<img src='https://www.incom.mx/img/webUI/newdesign/mi_cuenta.svg' alt='botón de inicio de sesión' class='inicio_sesion-icon'/>Iniciar sesión" LogoutText=" " OnLoggedOut="LoginStatus1_LoggedOut" />
    </li>
    <li>
        <div class="divider"></div>
    </li>
    <%--    <li><a class="subheader">Tienda</a></li>--%>

    <li class="no-padding">
        <%--        <img src="../img/webUI/newdesign/Flecha.svg" />--%>
        <ul class="collapsible collapsible-accordion">
            <li>
                <a class="collapsible-header">
                    <img id="products_menu_icon" alt="Icono de cesto con herramientas" src="https://www.incom.mx/img/webUI/newdesign/productos_icon.svg" />
                    <p>Productos</p>
                </a>
                <div class="collapsible-body">
                    <ul id="menu_movil_categorias" runat="server">
                    </ul>
                </div>
            </li>
            <li>
                <div class="divider">
                </div>
            </li>
        </ul>
    </li>
    <%--    <li><a class="subheader">Aprende</a></li>--%>
    <li>
        <a class="menu-book" href="/glosario/A">
            <img id="book-icon" alt="Icono de un libro" src="https://www.incom.mx/img/webUI/newdesign/biblioteca-icon.svg" />
            <p>Enciclopédico</p>
        </a>
    </li>
    <li>
        <a class="menu-infography" href="/enseñanza/infografías">
            <img id="infography-icon" alt="icono de infografia" src="https://www.incom.mx/img/webUI/newdesign/infography-icon.svg" />
            <p>Infografías</p>
        </a>

    </li>
    <li>
        <a class="menu-blog" title='Blog Incom' target='_blank' href='https://blog.incom.mx'>
            <img id="blog-icon" alt="icono de RSS o blog" src="https://www.incom.mx/img/webUI/newdesign/blog-icon.svg" />
            <p>Blog</p>
        </a>
    </li>
    <asp:LoginView ID="LoginView3" runat="server">
        <LoggedInTemplate>
            <li>
                <div class="divider"></div>
            </li>
            <li>
                <asp:LoginStatus ID="LoginStatus3" class="cerrar_sesion-menu" runat="server" LoginText="" LogoutText="<i class='material-icons left'>close</i>Cerrar Sesión" OnLoggedOut="LoginStatus1_LoggedOut" />
            </li>
        </LoggedInTemplate>
    </asp:LoginView>

</ul>

<%--<uc_bar:adminBar ID="botonAsesores" runat="server"></uc_bar:adminBar>--%>
<%--<div class="row z-depth-1 header white" style="margin-bottom: 0px;">--%>
<div>
    <section class="is-w-full is-flex is-justify-between is-items-center is-px-2">
        <div>
            <uc_bar:adminBar ID="botonAsesores" runat="server"></uc_bar:adminBar>
        </div>
        <div class="is-flex is-justify-center is-items-center">
            <uc_bar:modAsesor ID="barraAsesores" Visible="false" runat="server"></uc_bar:modAsesor>
        </div>
        <%--        <a class="btn_tuerca">
            <img class="icon_tuerca" src="../img/webUI/newdesign/Tuerca.svg" alt="boton de tuerca o ajustes" />
        </a>--%>


        <%--<span style="color: rgb(190 18 60) !important;">
            <svg xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                style="enable-background: new 0 0 24 24; width: 24px; height: 24px;">
                <symbol id="path">
                <path
                    d="M14.6 2.9c.8.2 1.6.5 2.3 1l2-1.2 2.7 2.7-1.2 2c.4.7.7 1.5 1 2.3l2.3.5v3.9l-2.3.5c-.2.8-.5 1.6-1 2.3l1.2 2-2.7 2.7-2-1.2c-.7.4-1.5.7-2.3 1l-.5 2.3h-3.9l-.5-2.3c-.8-.2-1.6-.5-2.3-1l-2 1.2-2.7-2.7 1.2-2c-.4-.7-.7-1.5-1-2.3L.7 14v-3.9L3 9.6c.2-.8.5-1.6 1-2.3l-1.2-2 2.7-2.7 2 1.2c.7-.4 1.5-.7 2.3-1l.5-2.3h3.9l.4 2.4zm-2.3 5c-2.3 0-4.2 1.9-4.2 4.2 0 2.3 1.9 4.2 4.2 4.2 2.3 0 4.2-1.9 4.2-4.2 0-2.3-1.9-4.2-4.2-4.2z" />
                </symbol>        
    </svg>
        </span>--%>
    </section>
    <section class="pleca is-flex is-bg-envioGratis is-py-0 is-justify-center  is-h-12">
        <span class="text-pleca is-flex is-text-white is-text-center is-items-center is-select-none">¡ENVÍO GRATIS! &nbsp;&nbsp; Válido en compras en línea desde $3,000 mxn</span>
    </section>
    <div id="content_header" class="col s12 m12 l12" style="padding: 5px 0px;">
        <section class="title_container">
            <p class="title_header is-select-none">INCOM&reg; La ferretera de las telecomunicaciones&reg;</p>
        </section>
        <div class="container_mobile_header  ">
            <!--- Movil --->
            <div class="content_menuMovil show-on-medium-and-down hide-on-med-and-up" style="display: inline;">
                <!-- Dropdown Trigger -->
                <a id="btn_menu_usuario_movil" data-target='menu_usuario_movil' href="#" class="sidenav-trigger">
                    <%--<i class="material-icons" style="font-size: 3rem;">menu</i>--%>
                    <img class="icon_menu" src="/img/webUI/newdesign/Menu.png" loading="lazy"/>
                </a>
            </div>
            <a title="Incom Retail" class="content_mobile_logo" href="<%=Request.Url.GetLeftPart(UriPartial.Authority) %>">
                <div class="content_header_logo-img-movil" ></div>
            </a>
            <%--<a title="Incom Retail" class="content_header_logo" href='<%= HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) %>'>
                <img src='<%=ResolveUrl("~/img/webUI/incom_logo_mini.png") %>'
                    alt="Logo de Incom" title="Incom,  La ferretera de las telecomunicaciones" class="responsive-img header_logo_img" />
            </a>--%>
            <%--<a title="Carrito de productos" class="black-text show-on-medium-and-down hide-on-med-and-up" href="/mi-carrito.aspx">--%>
            <a title="Carrito de productos" class="carrito_productos_movil show-on-medium-and-down hide-on-med-and-up" href="/mi-carrito.aspx" onclick="Notiflix.Loading.custom('Estamos alistando tu carrito',{customSvgUrl: 'https://www.incom.mx/img/webUI/newdesign/icono_de_carga.svg'});">
                <img class="btn-mi-carrito" title="Carrito de productos" src="/img/webUI/newdesign/Carrito.png" loading="lazy" />
                <%--<p class="text_carrito_compra">Carrito</p>--%>
            </a>
        </div>
        <div class="menu_right_contenedor">
            <div class="menu_top hide-on-med-and-down ">
                <!--- corregir posicionamiento icono   --->
                <!--- <i class="material-icons left">perm_identity</i> --->
                <!--- Desktop --->
                <%--                <div class="hide-on-med-and-down " style="display: inline;">
                    <asp:LoginView ID="LoginView1" runat="server">
                        <LoggedInTemplate>
                            <asp:HyperLink ID="miCuenta" ToolTip="Mi cuenta" class="login_btn" NavigateUrl="~/usuario/mi-cuenta/mi-cuenta.aspx"
                                runat="server">Mi cuenta</asp:HyperLink>
                            <asp:LoginStatus ID="LoginStatus1" class="login_btn" ToolTip="Sesión de usuario" runat="server" LoginText="Iniciar sesión"
                                LogoutText="Cerrar Sesión" OnLoggedOut="LoginStatus1_LoggedOut" />
                        </LoggedInTemplate>
                        <AnonymousTemplate>
                            <a title="Crear cuenta" class="login_btn " href='<%= ResolveUrl("~/registro-de-usuario.aspx") %>'>Crear cuenta</a>
                            <a class="login_btn" href="#" onclick="LoginAjaxOpenModal();">Iniciar sesión</a>
                        </AnonymousTemplate>
                    </asp:LoginView>
                </div>--%>
            </div>
            <div class="header_toolbar is-py-xl">
                <a title="Incom Retail" class="content_header_logo" href="<%=Request.Url.GetLeftPart(UriPartial.Authority) %>">
                    <div class="content_header_logo-img" ></div> 
                </a>
                <div class="menu_middle">
                    <uc_buscador:buscador ID="buscador" Visible="true" runat="server"></uc_buscador:buscador>
                </div>
                <div class="sesion_nav">
                    <div class="cuenta_container">
                        <asp:LoginView ID="LoginView1" runat="server">
                            <LoggedInTemplate>
                                <div class="user-menu">
                                    <div class="is-flex is-flex-col is-justify-center is-items-center is-px-2">
                                        <%--<img  class="icon_cuenta" src="https://ui-avatars.com/api/?name=Hugo+Carre%C3%B1o&background=000&color=fff&rounded=true&format=svg" />--%>
                                        <asp:HyperLink ID="miCuenta" ToolTip="Mi cuenta" class="text-nombre_cuenta is-text-black is-flex is-flex-col-reverse is-justify-center is-items-center" NavigateUrl="~/usuario/mi-cuenta/mi-cuenta.aspx" runat="server">
                                            <span id="miCuentaNombre" style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; max-width: 7.5rem; text-transform: capitalize;" runat="server"></span>
                                            <%--<asp:Image ID="profile_photo" class="image-cuenta" Style="width: 2rem;" runat="server" />--%>
                                        </asp:HyperLink>
                                    </div>
                                    <ul style="list-style: none;">
                                        <li>
                                            <asp:LoginStatus ID="LoginStatus1" class="text-sesion is-text-black" runat="server" LoginText="Iniciar Sesión" LogoutText="Cerrar sesión" OnLoggedOut="LoginStatus1_LoggedOut" />
                                        </li>
                                    </ul>
                                </div>
                            </LoggedInTemplate>
                            <AnonymousTemplate>
                                <%--<a title="Crear cuenta" class="login_btn " href='<%= ResolveUrl("~/registro-de-usuario.aspx") %>'>Crear cuenta</a>--%>
                                <a class="btn_cuenta is-text-black is-flex is-flex-col is-justify-center is-items-center" href="#" onclick="LoginAjaxOpenModal();">
                                    <!-- <img class="icon_cuenta" src="https://www.incom.mx/img/webUI/newdesign/Cuenta.svg" /> -->
                                    <img class="icon_cuenta" src="/img/webUI/newdesign/Usuario.png" loading="lazy" /> 
                                    <span>Iniciar sesión</span>
                                </a>
                                <%--<a class="login_btn" href="#" onclick="LoginAjaxOpenModal();">Iniciar sesión</a>--%>
                            </AnonymousTemplate>
                        </asp:LoginView>
                    </div>

                    <div>
                        <div id="carrito_de_compra">
                            <a title="Carrito de productos" href="/mi-carrito.aspx" onclick="Notiflix.Loading.custom('Estamos alistando tu carrito',{customSvgUrl: 'https://www.incom.mx/img/webUI/newdesign/icono_de_carga.svg'});" style="display: flex; flex-direction: column;">
                                <!-- <img class="btn-mi-carrito" title="Carrito de productos" src="https://www.incom.mx/img/webUI/newdesign/Carrito.svg" /> -->
                                <img title="Carrito de productos" src="/img/webUI/newdesign/Carrito.png" style="width: 34px; height: 30px" loading="lazy" />
                                <span class="txt_carrito is-text-black">Carrito</span>
                            </a>
                            <span id="lbl_cantidadProductosCarrito" class="carrito_counter is-select-none" runat="server"></span>
                            <%--<div style="display: flex; flex-direction: column;">
                                <uc_carrito:btnCarrito ID="carrito" runat="server"></uc_carrito:btnCarrito>
                                <p class="txt_carrito">Carrito</p>
                            </div>--%>
                        </div>
                    </div>

                    <div class="content_tipoDeCambio">
                        <span class="title_tipoDeCambio">Tipo de cambio</span>
                        <span id="txt_tipoDeCambio"></span>
                        <strong><span class="cantidad_tipoDeCambio"><%= operacionesConfiguraciones.obtenerTipoDeCambio() %> MXN </span></strong>
                    </div>
                </div>
            </div>
            <uc_menu:menuHeaderPrincipal ID="menuCat" runat="server"></uc_menu:menuHeaderPrincipal>
        </div>
    </div>
    <%--    <div style="padding: 6px 5px; overflow: hidden;">
        <div class="hide-on-med-and-down" style="float: left; padding-left: 15px;">
            <a href="/glosario/A" class="incom-sub-button-header">Enciclopédico</a>
            <a href="/enseñanza/infografías" class="incom-sub-button-header">Infografías</a>
            <a title='Blog Incom' target='_blank' class="incom-sub-button-header" href='https://blog.incom.mx'>Blog</a>
        </div>
        <div style="float: right;">
            <uc_Envio:DireccionEnvio ID="DireccionDeEnvio" runat="server"></uc_Envio:DireccionEnvio>
        </div>
    </div>--%>
    <!-- <div id="Content_aviso_header" style="text-align: center; padding: 6px 5px; background: #ffffff; overflow: hidden;">
        <strong>Aviso: </strong>
        <span  class="hide"> La empresa suspenderá labores del 24 de dic al 2 de Enero.</span>
       <span> Debido a la alta demanda nuestro inventario y entregas pueden verse afectados. Gracias por su comprensión. 
        <a target="_blank" href="/documents/INCOM-MEDIDAS-COVID.pdf">Consulta nuestro protocolo 
       COVID</a></span>
    </div> -->
</div>

<script>

    // Menú móvil usuario
    document.addEventListener('DOMContentLoaded', function () {
        var elems = document.querySelectorAll('#menu_usuario_movil');
        var instances = M.Sidenav.init(elems, null);


        /*         INICIO 20210419 RC - Oculta la sección de avisos al bajar el scroll en determinada altura */
        window.addEventListener('scroll', function (e) {
            var scroll = window.scrollY;


            var delayInMilliseconds = 1000; //1 second

            setTimeout(function () {
                //     console.log(scroll);
                if (scroll > 500) {
                    $('#Content_aviso_header').hide();
                }
                else {
                    $('#Content_aviso_header').show();
                }
            }, delayInMilliseconds)

        });
        /*         FIN 20210419 RC - Oculta la sección de avisos al bajar el scroll en determinada altura */


    });

</script>

<style>

    .mobile_logo {
        display: none;
    }

    .logotipo_home {
        height: 4.5rem;
        width: auto;
        margin-top: 0;
    }

    .menuContainer a {
        color: #202831 !important;
    }

    #txt_buscadorProducto {
        margin: 0rem !important;
        display: flex;
        border: none !important;
        height: auto;
        padding: 0px 1rem;
    }

    #txt_buscadorProducto:focus {
        -webkit-box-shadow: none !important;
        box-shadow: none !important;
        border-bottom: 0 !important:
    }

    #form-buscador_bar {
        height: auto;
        display: inline-flex;
        width: 50vw;
        box-shadow: inset 0 0 0 2px #06c;
        border-radius: 6px;
        max-width: 600px;
        min-width: 270px;
    }

    a.incom-sub-button-header {
        font-weight: 600;
        padding: 2px 9px;
        color: black;
    }

        a.incom-sub-button-header:hover {
            border-radius: 5px;
            color: white;
            background: #245c93;
        }

    .menuContainer ul a {
        -webkit-transition: background-color .3s;
        transition: background-color .3s;
        font-size: 1.5rem;
        display: block;
        padding: 0.5rem;
        cursor: pointer;
        color: #0f0f0f;
        background: #ffffff;
    }

    .menuContainer ul {
        justify-content: center;
        display: flex;
        margin: auto;
    }

    .menu-items > ul > ul > ul {
        left: 100%;
    }

    .menuContainer ul li {
        height: 2.25rem;
        text-align: left;
        left: 100%;
        margin-left: 2rem;
    }


    .over-header {
        height: 2rem;
        padding: 0.25rem;
        align-items: center;
    }

    .center {
        height: 2rem;
        width: 4rem;
        margin: 0;
        display: flex;
        align-items: center;
        justify-content: flex-start;
    }

    .btn_tuerca {
        margin-top: 0.5rem;
    }

    .icon_tuerca {
        filter: invert(18%) sepia(89%) saturate(2251%) hue-rotate(186deg) brightness(96%) contrast(99%);
        padding-left: 1.25rem;
        align-items: center;
    }

    .btn_asesores {
        font-size: .8rem;
        width: 4rem;
        margin: 4px;
        border: none;
        border-radius: 9px;
        color: #FFFFFF;
        background-color: #01568D;
    }

    .title_container {
        font-size: 2rem;
        margin: 0;
        background-color: #F9F7F7;
    }

    .title_header {
        font-size: 1.5rem;
        font-weight: 400;
        width: fit-content;
        margin: auto;
        display: flex;
        align-items: center;
        color: #0C3766;
    }

    .header {
        position: sticky;
        top: 0px;
        background: #fff;
        z-index: 99996;
    }

    .header_toolbar {
        height: 6rem;
        margin: auto 0rem 1.25rem 0rem;
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        -webkit-box-shadow: 4px 10px 5px -10px rgba(0,0,0,.3);
        box-shadow: 4px 10px 5px -10px rgba(0,0,0,.3);
    }

    /* #txt_buscadorProducto {
        font-size: 1rem;
        width: 40vw;
        height: 2rem;
        margin-top: 2rem;
        margin-left: 3rem;
        border-radius: 6px 0px 0px 6px;
        border: 2px #01568D solid !important;
        background: #fff;
    } */

        #txt_buscadorProducto::placeholder {
            font-style: italic;
            font-size: 1.5rem;
            padding-left: 0.5rem;
        }

    .btn_buscador {
        height: 100%;
        width: 100%;
        display: inline-block;
    }

    /* .btn_buscador {
        min-height: 2.4rem;
        height: 3rem !important;
        width: 3.5rem;
        margin-top: 2rem;
        border-radius: 0px 6px 6px 0px;
        box-shadow: 0px 2px 4px 0px rgba(0, 0, 0, 0.38039);
        background: #01568D;
    } */

    /*Este es el cuerpo del boton de búsqueda*/

    .button_buscadorProductos {
        width: 3rem;
        height: 2rem;
        background-color: #ff6a00;
    }

/*
    .icon_busqueda {
        width: 24px;
        height: 24px;
        margin: 0 !important;
        min-height: 24px;
        margin: 0.4rem 0.25rem 0.25rem 1rem;
    }
*/

    .icon_busqueda {
        width: 20px;
        height: 20px;
        margin: .5rem 0px 0px 0px !important;
    }

    .content_header_logo {
        margin: 0rem;
        float: left;
    }

    .header_logo_img {
        max-height: 4rem;
        display: flex;
    }

    .home_menu {
        display: flex !important;
    }

    #home-icon {
        width: 26px;
        margin-right: 26px;
    }

    .crear_cuenta_menu {
        display: flex !important;
        align-items: center;
    }

        .crear_cuenta_menu > p {
            margin-left: 30px;
        }

    #crear_cuenta-icon {
        width: 24px;
        display: flex;
        margin: 0;
    }

    .inicio_sesion-icon {
        margin-right: 30px;
    }

    #item-mis_compras {
        display: flex;
        text-align: center;
    }

    #ticket-icon {
        width: 20px;
        margin-right: 30px;
    }

    #shopping-bag {
        width: 1.5rem;
        height: 1.5rem;
        margin: 1rem 32px 0 0;
    }

    .collapsible-header {
        padding: 0 28px !important;
        display: flex !important;
        height: 5rem;
        align-items: center;
    }

        .collapsible-header > p {
            margin-left: 30px;
        }

    #products_menu_icon {
        width: 26px;
        padding-left: 4px;
    }

    .menu-infography {
        display: flex !important;
        align-items: center;
    }

    #infography-icon {
        width: 26px;
        margin-right: 26px;
    }

    .menu-book {
        display: flex !important;
        align-items: center;
        margin-right: 30px;
    }

    #book-icon {
        width: 24px;
        margin-right: 28px;
    }

    .menu-blog {
        display: flex !important;
        align-items: center;
    }

    #blog-icon {
        width: 25px;
        padding-left: 4px;
        margin-right: 28px;
    }

    .cerrar_sesion-menu {
        margin-top: 1rem;
    }

    .sesion_nav {
        height: 3rem;
        margin-top: 1rem;
        display: flex;
        flex-direction: row;
    }

    .cuenta_container {
        width: 8.5rem;
        border-right: 1px solid #B7B7B7;
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .micuenta_menu {
        display: flex !important;
    }

    .icon_cuenta {
        width: 2.1rem;
    }

    #user-icon {
        width: 22px;
        margin-right: 30px;
    }

    .inicio_sesion-icon {
        width: 22px;
    }

    .btn_cuenta {
        margin: auto;
        font-size: 0.75rem;
    }

    #carrito_de_compra {
        width: 7rem;
        display: flex;
        align-items: center;
        justify-content: center;
    }

    .txt_carrito {
        font-size: 0.75rem;
        margin: 0;
    }

    .shop_button {
        width: 80px;
        display: block;
    }

    .menu_top {
        text-align: right;
        margin-top: 0px;
        overflow: hidden;
    }

    .menu_middle {
        height: 100%;
        width: 100%;
        align-items: center;
        display: flex;
        padding: 0.5rem 0rem 0rem 2rem;
        justify-content: start;
    }


    .menu_bottom {
        width: auto;
        margin-top: 2px;
        overflow: hidden;
    }

    .login_btn {
        font-weight: 700;
        padding: 3px 21px;
        margin: 0px 1rem 0px 0px;
        border-radius: 0.75rem;
        color: #242c33;
        background: #ffffff;
    }

    #btn_menu_usuario_movil {
        display: inline;
        overflow: hidden;
        color: #000;
    }

    .title_tipoDeCambio {
        font-size: 0.75rem;
        height: 1rem;
        text-align: start;
        display: block;
    }

    #txt_tipoDeCambio {
        font-size: 1rem;
    }

        #txt_tipoDeCambio::before {
            content: "1 USD = ";
            font-size: 0.75rem;
        }

    <!-- .icon_menu {
        width: 3rem;
        height: auto;
    } -->

    .cantidad_tipoDeCambio {
        font-size: 0.75rem;
    }

    .content_tipoDeCambio {
        width: 12rem;
        padding-top: 10px;
        padding-left: 1rem;
        border-left: 1px solid #B7B7B7;
    }

    .right_position {
        margin-left: auto;
        display: flex;
    }

    .btn-mi-carrito {
        color: black;
        width: 2.75rem;
        height: auto;
        margin: 0.5rem 0rem 0rem 0.5rem;
    }

    .title_categorias {
        justify-content: center;
        display: flex;
        font-weight: 600;
        font-size: 1.5rem;
        margin-bottom: 1.5rem;
        height: 4rem;
        align-items: center;
    }

    .title_productos-destacados {
        justify-content: center;
        display: flex;
        font-weight: 600;
        font-size: 1.5rem;
        height: 4rem;
        align-items: center;
    }

    .main_container {
        width: 100%;
        margin: 5em auto;
    }

    .anuncios-UPS {
        <!-- height: 100%; -->
        grid-area: anuncios-UPS;
    }

    .anuncios-UPS-column {
        display: flex;
        flex-direction: column;
        height: 100%;
        justify-content: space-between;
    }

    .main_container {
        display: grid;
        grid-template-columns: 75% 25%;
        grid-template-rows: 1fr;
        grid-column-gap: 16px;
        grid-row-gap: 0px; 
        grid-template-areas:
            "Banner anuncios-UPS"
    }

    #slider_home_principal {
        grid-area: Banner;
    }

    #slider_home_principal {
        background-color: #FFFFFF;
    }

    /* .anuncios {
        background-color: #787878;
        height: 43%;
    } */

    .anuncios {
        aspect-ratio: 3/2;
        width: 100%;
        object-fit: cover;
    }

    .anuncios img {
        background-color: #787878;
    }

    .USP1, .USP2, .USP3 {
        aspect-ratio: 4/1;
        width: 100%;
        object-fit: cover;
        <!-- height: 18% -->
    }

    .USP1 img {
        background-color: #c3bb9f;
    }

    <!-- .USP2 {
       /* height: 18% */
        aspect-ratio: 4/1;
        width: 100%;
        object-fit: cover;
    } -->

    .USP2 {
        background-color: #89abad;
    }

    <!-- .USP3 {
       /* height: 18%; */
        aspect-ratio: 4/1;
        width: 100%;
        object-fit: cover;
    } -->

    .USP3 img {
        background-color: #be9393;
    }

    /* .anuncios a, .USP1 a, .USP2 a, .USP3 a {
        display: block;
        height: 100%;
    } */

    <!-- .anuncios > div, .USP1 > div, .USP2 > div, .USP3 > div {
        height: 100%;
    } -->


    /* .anuncios a > img, .USP1 a > img, .USP2 a > img, .USP3 a > img {
        display: flex;
        width: -webkit-fill-available;
        width: -moz-available;
        height: 100%;
    } */

    /* .anuncios a, .USP1 a, .USP2 a, .USP3 a {
        aspect-ratio: 2/1;
        width: 100%;
        object-fit: cover;
    } */

    /* .anuncios a, .USP1 a, .USP2 a, .USP3 a {
        aspect-ratio: 2/1;
        width: 100%;
        object-fit: cover;
        display: block;
        height: 100%;
    } */

    .anuncios a > img, .USP1 a > img, .USP2 a > img, .USP3 a > img {
        display: block;
        height: 100%;
    }

    .carrito_counter {
        display: flex;
        font-size: 14px;
        font-weight: 600;
        color: #06C;
        align-items: center;
    }

    @media only screen and (min-width: 1000px) and (max-width: 1200px) {
        .carrito_counter {
            width: 1.5rem;
            height: 1.5rem;
            margin-bottom: 2rem;
        }

        .profile_photo {
            height: 2rem;
        }

        .btn-mi-carrito {
            width: 2rem;
        }

        #carrito_de_compra {
            width: 5rem;
        }

        /*.btn-mi-carrito {
            height: 28px;
            margin: 0;
        }*/

        .txt_carrito {
            font-size: 0.75rem;
        }

        #carrito_de_compra {
            padding-left: 0.5rem;
        }

        .text-nombre_cuenta > span {
            font-size: 0.75rem;
            margin-top: 0.5rem;
        }

        /*#carrito_de_compra > a:nth-child(1) > img:nth-child(1) {
            padding-bottom: 0.25rem;
        }*/
    }

    @media only screen and (min-width: 1200px) {
        .carrito_counter {
            width: 1.5rem;
            height: 1.5rem;
            margin-bottom: 2rem;
        }

        .profile_photo {
            height: 2rem;
        }

        .btn-mi-carrito {
            width: 2.25rem;
        }
    }

    @media only screen and (max-width:2700px) {

/*        #txt_buscadorProducto {
            margin-left: 2rem;
        }*/

        .logotipo_home {
            height: 3.85rem;
            width: auto;
            margin-top: 0;
        }

        .categorias_container {
            display: flex;
            flex-direction: row;
            height: auto;
            margin-bottom: 10vh;
            flex-wrap: wrap;
            justify-content: center;
        }

        /* Separadores en barra de menús*/
        #content-menu-incom-outlet, #menuProductos, #content-menu-incom-biblioteca {
            border-right: 1px solid #B7B7B7;
            padding: 0rem 2rem 0rem 0rem;
        }
    }

    @media only screen and (max-width:1000px) {

        .menu_middle {
            padding-left: 0 !important;
        }

        .container_mobile_header {
            text-align: center;
            height: 5rem;
            display: flex;
            justify-content: space-between;
            /*          align-items: baseline;*/
            padding: 0.5rem 1rem;
        }

        .mobile_logo {
            display: inline;
            width: auto;
            height: 4rem;
        }

        #txt_tipoDeCambio::after {
            content: "TC: 1 USD = ";
        }

        .buscador_container {
            font-style: italic;
            width: 95%;
            margin-top: 0px;
            position: relative;
        }

        #txt_buscadorProducto {
            font-style: italic;
        }

        .menuContainer {
            margin: 0 auto;
            float: initial !important;
        }

        .content_tipoDeCambio {
            margin-right: 30px;
            border-left: 2px solid black;
            float: none;
            display: inline;
            line-height: 42px;
        }

        .content_menuMovil {
            float: left;
            display: inline !important;
        }

        .sesion_nav {
            display: none
        }

        .content_header_logo {
            display: none;
        }

        .buscador_container {
            margin-top: 0px;
        }

        .text_carrito_compra {
            margin: 0;
            color: black;
        }

        .carrito_productos_movil {
            margin-right: 1rem;
        }

        .categorias_container {
            margin-left: 20px;
        }
    }

    @media only screen and (max-width:600px) {

        .menu_middle {
            overflow: initial;
        }

        .menu_top {
            width: 91%;
            position: fixed;
        }

        .buscador_container {
            display: flex;
            margin-top: 0rem !important;
        }

/*        #txt_buscadorProducto {
            height: 2rem;
            padding: 0px 10px;
            margin-left: 0rem !important;
            width: 100vw;
            z-index: 2;
        }*/

        .categorias_container {
            margin-bottom: 2vh;
        }
    }

    @media only screen and (max-height:1000px) {

        #content_header {
            zoom: 0.8;
        }

        .buscador_container {
            display: flex;
            position: relative;
        }

        .content_header_logo {
            padding-top: 0.5rem;
        }

/*        #txt_buscadorProducto {
            font-size: 1rem;
            height: 1.5rem;
            padding: 5px;
            border: 1px solid #00B4CC;
            border-right: none;
            border-radius: 6px 0 0 6px;
            outline: none;
            color: #000000;
            margin-left: 0;
        }*/

        /*        .btn_buscador {
        height: 2.4rem;
        width: 4rem;
        border-radius: 0 6px 6px 0;
        border: none;
        cursor: pointer;
        background-image: url(../img/webUI/newdesign/Flecha.svg);
        background-size: 2rem;
        background-position: center center, center center;
        background-repeat: no-repeat, repeat;
    }*/

/*        #txt_buscadorProducto {
            height: 2rem;
            padding: 0px 10px;
            margin-left: 2rem;
            border: 3px #01568D solid !important;
            width: 450px;
        }*/
    }

    @media only screen and (max-width:1320px) {

        #top_contenido_sliderUSP1 > div {
            height: 100%;
        }

        #top_contenido_sliderUSP1 > div {
            height: 100%;
        }

            #top_contenido_sliderUSP1 > div > a > img {
                width: 100%;
                height: 100%;
            }

        #top_contenido_sliderUSP2 > div {
            height: 100%;
        }

            #top_contenido_sliderUSP2 > div > a > img {
                width: 100%;
                height: 100%;
            }

        #top_contenido_sliderUSP3 > div {
            height: 100%;
        }

            #top_contenido_sliderUSP3 > div > a > img {
                width: 100%;
                height: 100%;
            }
    }

    @media only screen and (max-width:1200px) {

        .title_tipoDeCambio {
            font-size: 0.75rem;
        }

        .content_tipoDeCambio {
            padding-top: 0.5rem;
        }

        #txt_tipoDeCambio {
            font-size: 0.75rem;
        }

        <!-- .icon_cuenta {
            height: 36px;
        } -->

        .btn-mi-carrito {
            height: 36px;
        }

        #carrito_de_compra {
            width: 4.5rem;
        }

        .cuenta_container {
            width: 6rem;
            margin-left: 1rem;
        }

        .logotipo_home {
            height: 3.5rem;
        }

        .image-cuenta {
            width: 2rem;
            padding-top: 5px;
            margin-bottom: 3px;
        }

        /* .btn_buscador {
            height: 2.4rem;
            margin-top: 2rem;
        } */
    }

    @media only screen and (max-width:999px) {

        .buscador_container {
            margin: 0px auto 0px auto;
            justify-content: center;
        }

/*        #txt_buscadorProducto {
            margin-top: 0rem;
        }

        .btn_buscador {
            height: 2.4rem;
            margin-top: 0rem;
        }*/

        .menuContainer ul a {
            font-size: 1.25rem;
        }

        .menuContainer {
            display: none;
        }

        #slider_home_principal {
            height: 50vw;
            width: 100%;
        }

        .main_container {
            height: 50vw;
            width: 100%;
            display: inline-block;
            margin: 0em 0em 5em 0em;
        }

        .anuncios {
            display: none;
        }

        .USP1 {
            display: none;
        }

        .USP2 {
            display: none;
        }

        .USP3 {
            display: none;
        }

        .title_categorias {
            font-size: 1.5rem;
        }
    }


    @media only screen and (max-width:700px) {

        .title_header {
            font-size: 12px;
            height: 2rem;
        }

        .menu_top {
            height: 35px;
            text-align: right;
            margin-top: 0px;
            overflow: hidden;
        }

        .buscador_container {
            margin-top: 0 !important;
            position: relative;
        }

/*        #txt_buscadorProducto {
            margin-top: 0rem !important;
            height: 2rem;
            margin-left: 0rem !important;
            z-index: 1;
        }

        .btn_buscador {
            margin-top: 0rem;
            padding: 0.25rem 3rem 0.25rem 0rem;
            height: 2.45rem !important;
            margin-right: 2rem;
            border: none;
        }*/

     /*   .icon_busqueda {
            width: 22px;
            margin: 2px 4px 4px 12px;
        }
*/
        .icon_busqueda {
            width: 22px;
            margin: 0.5rem 0px !important;
        }

        .btn-mi-carrito {
            color: black;
            height: auto;
        }

        .title_productos-destacados {
            font-size: 1.2rem;
        }

        .title_categorias {
            font-size: 1.2rem;
        }

        .main_container {
            margin: 0em 0em 3em 0em;
        }
    }

    @media only screen and (max-width: 600px) {

        .title_productos-destacados {
            font-size: 0.8rem;
        }

        .title_categorias {
            font-size: 0.8rem;
        }
    }

        @media only screen and (min-width: 2700px) {

        .header_toolbar {
            max-width: 2000px;
            margin: auto auto 1.25rem auto;
        }

        .main_container {
            margin-top: 8rem;
            max-width: 2100px;
        }

        .slider-container {
            max-width: 2000px;
            margin: auto auto 5rem auto;
        }

        .categorias_container {
            display: flex;
            flex-direction: row;
            height: auto;
            flex-wrap: wrap;
            justify-content: center;
            max-width: 2000px;
            margin: auto auto 6vh auto;
        }

        .is-col-footer {
            max-width: 2000px;
            margin: auto;
        }
    }

</style>

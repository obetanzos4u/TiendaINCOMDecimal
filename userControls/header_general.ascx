<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header_general.ascx.cs" Inherits="menuPrincipal" %>
<%@ Register Src="~/userControls/menu_principal.ascx" TagName="menuPrincipal" TagPrefix="uc_menu" %>
<%@ Register Src="~/userControls/uc_asesores_modalidad_clientes_bar.ascx" TagName="modAsesor" TagPrefix="uc_bar" %>
<%@ Register Src="~/userControls/uc_admin_bar_button.ascx" TagName="adminBar" TagPrefix="uc_bar" %>
<%@ Register Src="~/userControls/buscador.ascx" TagName="buscador" TagPrefix="uc_buscador" %>
<%@ Register Src="~/userControls/menu_Carrito.ascx" TagName="btnCarrito" TagPrefix="uc_carrito" %>
<%@ Register Src="~/userControls/uc_DireccionEnvioPredeterminada.ascx" TagName="DireccionEnvio" TagPrefix="uc_Envio" %>

<!-- sidenav MÓVIL -->
<ul id='menu_usuario_movil' class="sidenav">
    <li><a href="/"><i class="material-icons">home</i>Inicio</a></li>
    <li>
        <div class="divider"></div>
    </li>
    #region Botones de usuario
    <asp:LoginView ID="LoginView2" runat="server">
        <LoggedInTemplate>
            <li>
                <asp:HyperLink ID="miCuenta" ToolTip="Mi cuenta" class=" " NavigateUrl="~/usuario/mi-cuenta/mi-cuenta.aspx"
                    runat="server"> <i class="material-icons left">perm_identity</i>Mi cuenta
                </asp:HyperLink>
            </li>
            <li>
                <a href="/usuario/mi-cuenta/pedidos.aspx" class="grey-text text-darken-3">
                    <i class="material-icons left">assignment_turned_in</i>Pedidos</a>
            </li>
            <li>
                <a href="/usuario/mi-cuenta/cotizaciones.aspx" class="grey-text text-darken-3">
                    <i class="material-icons left">assignment</i> Cotizaciones</a>
            </li>
        </LoggedInTemplate>
        <AnonymousTemplate>
            <li><a title="Crear cuenta" class=" " href='<%= ResolveUrl("~/registro-de-usuario.aspx") %>'>Crear cuenta</a>    </li>
        </AnonymousTemplate>
    </asp:LoginView>
    #endregion

    <li>
        <asp:LoginStatus ID="LoginStatus2" ToolTip="Sesión de usuario" runat="server" LoginText="Iniciar Sesión" LogoutText="" OnLoggedOut="LoginStatus1_LoggedOut" />
    </li>
    <li>
        <div class="divider"></div>
    </li>
    <li><a class="subheader">Tienda</a></li>

    <li class="no-padding">
        <%--        <img src="../img/webUI/newdesign/Flecha.svg" />--%>
        <ul class="collapsible collapsible-accordion">
            <li>
                <a class="collapsible-header">Productos</a>

                <div class="collapsible-body">
                    <ul id="menu_movil_categorias" runat="server">
                    </ul>
                </div>
            </li>
        </ul>
    </li>
    <li><a class="subheader">Aprende</a></li>
    <li><a href="/glosario/A">Enciclopédico</a> </li>
    <li><a href="/enseñanza/infografías">Infografías</a> </li>
    <li><a title='Blog Incom' target='_blank' href='https://blog.incom.mx'>Blog</a> </li>
    <asp:LoginView ID="LoginView3" runat="server">
        <LoggedInTemplate>
            <li>
                <div class="divider"></div>
            </li>
            <li>
                <asp:LoginStatus ID="LoginStatus3" class=" " runat="server" LoginText="" LogoutText="<i class='material-icons left'>close</i>Cerrar Sesión" OnLoggedOut="LoginStatus1_LoggedOut" />
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
    <div id="content_header" class="col s12 m12 l12" style="padding: 5px 0px;">
        <section class="title_container">
            <p class="title_header">INCOM&reg; La ferretera de las telecomunicaciones&reg;</p>
        </section>
        <div class="container_mobile_header  ">
            <!--- Movil --->
            <div class="content_menuMovil show-on-medium-and-down hide-on-med-and-up" style="display: inline;">
                <!-- Dropdown Trigger -->
                <a id="btn_menu_usuario_movil" data-target='menu_usuario_movil' href="#" class="sidenav-trigger">
                    <%--<i class="material-icons" style="font-size: 3rem;">menu</i>--%>
                    <img class="icon_menu" src="../img/webUI/newdesign/Menu.svg" />
                </a>
            </div>
            <a title="Incom Retail" class="content_mobile_logo" href="http://localhost:63722/">
                <img src="../img/webUI/newdesign/Incom_nuevo.png" alt="Logotipo INCOM" class="mobile_logo" />
            </a>
            <%--            <a title="Incom Retail" class="content_header_logo" href='<%= HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) %>'>
                <img src='<%=ResolveUrl("~/img/webUI/incom_logo_mini.png") %>'
                    alt="Logo Incom" title="Incom,  La ferretera de las telecomunicaciones" class="responsive-img header_logo_img" />
            </a>--%>
            <a title="Carrito de productos" class="black-text show-on-medium-and-down hide-on-med-and-up" href="/mi-carrito.aspx">
                <img class="btn-mi-carrito" title="Carrito de productos" src="../img/webUI/newdesign/Carrito.svg" />
                <p>Prueba</p>
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
                            <asp:LoginStatus ID="LoginStatus1" class="login_btn" ToolTip="Sesión de usuario" runat="server" LoginText="Iniciar Sesión"
                                LogoutText="Cerrar Sesión" OnLoggedOut="LoginStatus1_LoggedOut" />
                        </LoggedInTemplate>
                        <AnonymousTemplate>
                            <a title="Crear cuenta" class="login_btn " href='<%= ResolveUrl("~/registro-de-usuario.aspx") %>'>Crear cuenta</a>
                            <a class="login_btn" href="#" onclick="LoginAjaxOpenModal();">Iniciar Sesión</a>
                        </AnonymousTemplate>
                    </asp:LoginView>
                </div>--%>
            </div>
            <div class="header_toolbar">
                <a title="Incom Retail" class="content_header_logo" href="http://localhost:63722/">
                    <img src="../img/webUI/newdesign/Incom_nuevo.png" alt="Logotipo INCOM" class="logotipo_home" />
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
                                        <asp:Image ID="profile_photo" Style="width: 2.50rem;" runat="server" />
                                        <asp:HyperLink ID="miCuenta" ToolTip="Mi cuenta" class="is-text-black" Style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; max-width: 7.5rem; text-transform: capitalize;" NavigateUrl="~/usuario/mi-cuenta/mi-cuenta.aspx" runat="server"></asp:HyperLink>
                                    </div>
                                    <ul style="list-style: none;">
                                        <li>
                                            <asp:LoginStatus ID="LoginStatus1" class="is-text-black" runat="server" LoginText="Iniciar Sesión" LogoutText="Cerrar sesión" OnLoggedOut="LoginStatus1_LoggedOut" />
                                        </li>
                                    </ul>
                                </div>
                            </LoggedInTemplate>
                            <AnonymousTemplate>
                                <%--<a title="Crear cuenta" class="login_btn " href='<%= ResolveUrl("~/registro-de-usuario.aspx") %>'>Crear cuenta</a>--%>
                                <a class="btn_cuenta is-text-black is-flex is-flex-col is-justify-center is-items-center" href="#" onclick="LoginAjaxOpenModal();">
                                    <img class="icon_cuenta" src="../img/webUI/newdesign/Cuenta.svg" />
                                    <span>Iniciar sesión</span>
                                </a>
                                <%--<a class="login_btn" href="#" onclick="LoginAjaxOpenModal();">Iniciar Sesión</a>--%>
                            </AnonymousTemplate>
                        </asp:LoginView>
                    </div>

                    <div>
                        <div id="carrito_de_compra">
                            <a title="Carrito de productos" href="/mi-carrito.aspx" style="display: flex; flex-direction: column;">
                                <img class="btn-mi-carrito" title="Carrito de productos" src="../img/webUI/newdesign/Carrito.svg" />
                                <span class="txt_carrito is-text-black">Carrito</span>
                            </a>
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
            <uc_menu:menuPrincipal ID="menuCat" runat="server"></uc_menu:menuPrincipal>
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
    .menuContainer a {
        color: #202831 !important;
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
        margin: auto 1rem;
    }

    .menuContainer ul {
        justify-content: center;
        display: flex;
        margin: auto;
    }

        .menuContainer ul li {
            height: 2.25rem;
            text-align: center;
        }

        .menuContainer ul :nth-child(2n +2) {
            border-left: 3px solid #CCD1D1;
        }

        .menuContainer ul :nth-child(3) {
            border-left: 3px solid #CCD1D1;
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
        padding: .5rem 5rem .5rem 2rem;
        margin: auto 0rem 1.25rem 0rem;
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        box-shadow: rgba(0, 0, 0, 0.15) 0px 2px 2.6px;
    }

    .mobile_logo {
        display: none;
    }

    .logotipo_home {
        height: 4.5rem;
        width: auto;
        margin-top: 0;
    }

    #txt_buscadorProducto {
        font-size: 1rem;
        width: 60vw;
        height: 3rem;
        margin-top: 1rem;
        border-radius: 6px;
        border: 2px #01568D solid !important;
        /*box-shadow: 0px 2px 4px 0px rgba(0, 0, 0, 0.3803921568627451);*/
        background: #fff;
    }

        #txt_buscadorProducto::placeholder {
            font-style: italic;
        }

    .btn_buscador {
        height: 2rem;
        width: 6rem;
        margin-top: 1rem;
        border-radius: 0px 6px 6px 0px;
        /*        box-shadow: 0px 2px 4px 0px rgba(0, 0, 0, 0.38039);*/
        position: relative;
        background-image: url(/img/webUI/search_icon_bg.png);
        background-position: center center, center center;
        background-repeat: no-repeat, repeat;
        background-size: 24px;
        background-color: white;
        background: #01568D;
    }

    .content_header_logo {
        margin: 0rem;
        float: left;
    }

    .header_logo_img {
        max-height: 4rem;
    }

    .sesion_nav {
        height: 3rem;
        margin-top: 1.75rem;
        display: flex;
        flex-direction: row;
    }

    .cuenta_container {
        width: 8.5rem;
        border-right: 2px solid black;
        display: flex;
        flex-direction: column;
        align-items: center;
    }

    .icon_cuenta {
        width: 2.50rem;
    }

    .btn_cuenta {
        font-weight: 600;
        margin: auto;
    }

    #carrito_de_compra {
        width: 7rem;
        display: flex;
        align-items: center;
        justify-content: center;
    }

    .txt_carrito {
        font-weight: 600;
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
        height: fit-content;
        width: 100vw;
        align-items: flex-start;
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
        font-size: 1rem;
        font-weight: 600;
        height: 1.5rem;
        text-align: center;
        display: block;
    }

    #txt_tipoDeCambio {
        font-size: 1rem;
        font-weight: 600;
    }

        #txt_tipoDeCambio::before {
            content: "1 USD = ";
        }

    .icon_menu {
        filter: invert(18%) sepia(89%) saturate(2251%) hue-rotate(186deg) brightness(96%) contrast(99%);
        width: 3rem;
        height: auto;
    }

    .cantidad_tipoDeCambio {
        font-size: 1rem;
        font-weight: 600;
    }

    .content_tipoDeCambio {
        width: 12rem;
        padding-left: 1rem;
        border-left: 2px solid black;
    }

    .right_position {
        margin-left: auto;
        display: flex;
    }

    .btn-mi-carrito {
        color: black;
        width: 2.5rem;
        height: auto;
    }

    /*    @media only screen and (max-width:1200px) {
        .menu_middle {
            margin: auto;
        }
    }*/

    @media only screen and (max-width:700px) {
        .menu_top {
            height: 35px;
            text-align: right;
            margin-top: 0px;
            overflow: hidden;
        }

        .buscador_container {
            margin-top: 0 !important;
        }
    }

    @media only screen and (max-width:1000px) {

        .container_mobile_header {
            text-align: center;
            height: 5rem;
            border: 2px solid red;
            display: flex;
            justify-content: space-between;
            align-items: baseline;
            padding: 0.5rem 1rem;
        }

        .content_header_logo {
            margin: 0px 2rem 0px 0px;
            float: inherit;
            display: none;
        }

        .mobile_logo {
            display: inline;
            width: auto;
            height: 4rem;
        }
        /*        .menu_top {
            height: 35px;
            text-align: right;
            margin-top: 0px;
            overflow: hidden;
        }*/

        #txt_tipoDeCambio::after {
            content: "TC: 1 USD = ";
        }


        .buscador_container {
            font-style: italic;
            width: 95%;
            margin-top: 0px;
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

        .menu_middle {
            margin: auto;
        }
    }

    @media only screen and (max-width:1000px) {
        .sesion_nav {
            display: none
        }

        .content_header_logo {
            display: none;
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

        #txt_buscadorProducto {
            height: 2rem;
            padding: 0px 10px;
            margin-left: 0rem !important;
            width: 100vw;
            z-index: 2;
        }
    }

    @media only screen and (max-height:1000px) {
        #content_header {
            zoom: 0.8;
        }

        .buscador_container {
            display: flex;
            margin-top: 1.5rem;
        }

        .content_header_logo {
            padding-top: 0.5rem;
        }


        #txt_buscadorProducto {
            font-size: 1rem;
            height: 1.5rem;
            padding: 5px;
            border: 1px solid #00B4CC;
            border-right: none;
            border-radius: 6px 0 0 6px;
            outline: none;
            color: #9DBFAF;
        }

        .btn_buscador {
            height: 2.4rem;
            width: 4rem;
            border-radius: 0 6px 6px 0;
            border: none;
            cursor: pointer;
            background-image: url(/img/webUI/search_icon_bg.png);
            background-size: 2rem;
            background-position: center center, center center;
            background-repeat: no-repeat, repeat;
        }

        #txt_buscadorProducto {
            height: 2rem;
            padding: 0px 10px;
            margin-left: 2rem;
            border: 3px #01568D solid !important;
        }

        .header_toolbar {
            padding-right: 1rem;
        }
</style>

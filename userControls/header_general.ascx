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
                    <i class="material-icons left">assignment_turned_in</i> Pedidos</a>
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
        <ul class="collapsible collapsible-accordion">
            <li>
                <a class="collapsible-header">Productos<i class="material-icons">arrow_drop_down</i></a>
                <div class="collapsible-body">
                    <ul id="menu_movil_categorias" runat="server">
                    </ul>
                </div>
            </li>
        </ul>
    </li>
    <%--    <li><a class="subheader">Aprende</a></li>
    <li><a href="/glosario/A">Enciclopédico</a> </li>
    <li><a href="/enseñanza/infografías">Infografías</a> </li>
    <li><a title='Blog Incom' target='_blank' href='https://blog.incom.mx'>Blog</a> </li>--%>
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

<uc_bar:adminBar ID="botonAsesores" runat="server"></uc_bar:adminBar>
<div class="row z-depth-1 header white" style="margin-bottom: 0px;">
    <div style="background: white; overflow: hidden; color: #353635;">
        <uc_bar:modAsesor ID="barraAsesores" Visible="false" runat="server"></uc_bar:modAsesor>
    </div>
    <div id="content_header" class="col s12 m12 l12" style="padding: 5px 25px;">
        <div class="menu_left_contenedor  ">
            <!--- Movil --->
            <div class="content_menuMovil show-on-medium-and-down hide-on-med-and-up" style="display: inline;">
                <!-- Dropdown Trigger -->
                <a id="btn_menu_usuario_movil" data-target='menu_usuario_movil' href="#" class="sidenav-trigger">
                    <i class="material-icons" style="font-size: 3rem;">menu</i>
                </a>


            </div>
            <a title="Incom Retail" class="content_header_logo" href='<%= HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) %>'>
                <img src='<%=ResolveUrl("~/img/webUI/incom_logo_mini.png") %>'
                    alt="Logo Incom" title="Incom,  La ferretera de las telecomunicaciones" class="responsive-img header_logo_img" />
            </a>
            <a title="Carrito de productos"
                class="btn white black-text show-on-medium-and-down hide-on-med-and-up" href="/mi-carrito.aspx">
                <i class="material-icons  ">shopping_cart
                </i>
            </a>
        </div>
        <div class="menu_right_contenedor  ">
            <div class="menu_top hide-on-med-and-down ">
                <!--- corregir posicionamiento icono   --->
                <!--- <i class="material-icons left">perm_identity</i> --->
                <!--- Desktop --->
                <div class="hide-on-med-and-down " style="display: inline;">
                    <asp:LoginView ID="LoginView1" runat="server">
                        <LoggedInTemplate>
                            <asp:HyperLink ID="miCuenta" ToolTip="Mi cuenta" class="login_btn" NavigateUrl="~/usuario/mi-cuenta/mi-cuenta.aspx"
                                runat="server"> Mi cuenta </asp:HyperLink>
                            <asp:LoginStatus ID="LoginStatus1" class="login_btn" ToolTip="Sesión de usuario" runat="server" LoginText="Iniciar Sesión"
                                LogoutText="Cerrar Sesión" OnLoggedOut="LoginStatus1_LoggedOut" />
                        </LoggedInTemplate>
                        <AnonymousTemplate>
                            <a title="Crear cuenta" class="login_btn " href='<%= ResolveUrl("~/registro-de-usuario.aspx") %>'>Crear cuenta</a>
                            <a class="login_btn" href="#" onclick="LoginAjaxOpenModal();">Iniciar Sesión</a>
                        </AnonymousTemplate>
                    </asp:LoginView>
                </div>
            </div>
            <div class="header_toolbar">
                <div class="menu_middle">
                    <uc_buscador:buscador ID="buscador" Visible="true" runat="server"></uc_buscador:buscador>
                </div>
                <div class="sesion_nav">
                    <span class="sesion_btn">Mi cuenta</span>
                    <div>
                        <div id="carrito_de_compra">
                            <div style="display: flex; flex-direction: column;">
                                <uc_carrito:btnCarrito ID="carrito" runat="server"></uc_carrito:btnCarrito>
                                                                <p>Carrito</p>
                                  </div>
                        </div>
                    </div>

                    <div class="hide-on-med-and-down content_tipoDeCambio">
                        <span class="title_tipoDeCambio">Tipo de cambio</span>
                        <span id="txt_tipoDeCambio"></span>
                        <strong><span><%= operacionesConfiguraciones.obtenerTipoDeCambio() %> MXN </span></strong>
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


        /* INICIO 20210419 RC - Oculta la sección de avisos al bajar el scroll en determinada altura */
        /*    window.addEventListener('scroll', function (e) {
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
               
            }); */
        /* FIN 20210419 RC - Oculta la sección de avisos al bajar el scroll en determinada altura */


    });

</script>
<style>
    a.incom-sub-button-header {
        color: black;
        padding: 2px 9px;
        font-weight: 600;
    }

        a.incom-sub-button-header:hover {
            background: #245c93;
            border-radius: 5px;
            color: white;
        }

    .menuContainer a {
        color: #202831 !important;
    }

    .menuContainer ul a {
        -webkit-transition: background-color .3s;
        transition: background-color .3s;
        font-size: 1rem;
        color: #0f0f0f;
        display: block;
        padding: 5px 15px;
        background: #ffffff;
        cursor: pointer;
        border-radius: 13px;
    }

    .header {
        position: sticky;
        top: 0px;
        z-index: 99996;
        background: #fff;
    }


    .content_header_logo {
        float: left;
        margin: 0px 80px 0px 0px;
    }

    .header_logo_img {
        max-height: 4rem;
    }

    .header_toolbar {
        border: 1px solid #00B4CC;
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        height: 4rem;
        padding: .5rem;
    }

    .sesion_nav {
        border: 1px solid #6b6129;
        display: flex;
        flex-direction: row;
        height: 3rem;
    }

    .sesion_btn {
        margin: auto;
        font-weight: 800;
    }

    .shop_button {
        border: 1px #01568D solid;
        width: 80px;
        display: block;
    }

    .menu_top {
        text-align: right;
        margin-top: 0px;
        overflow: hidden;
    }

    .menu_middle {
        /*  overflow: hidden;
        margin: 0px 35%;*/
    }

    .menu_bottom {
        margin-top: 2px;
        width: auto;
        overflow: hidden;
    }


    /*    .btn_buscador {
        position: absolute;
        top: 0px;
        right: -22px;
        width: 66px;
        height: 2rem;
        border-radius: 0px 6px 6px 0px;
        border: none;
        background: #245c93;
        vertical-align: middle;
        background-image: url(/img/webUI/search_icon_bg.png);
        background-position: center center, center center;
        background-size: 29px;
        background-repeat: no-repeat, repeat;
        box-shadow: 0px 2px 4px 0px rgba(0, 0, 0, 0.38039);
    }*/

    .btn_buscador {
        position: relative;
        width: 4rem;
        height: 2rem;
        border-radius: 0px 6px 6px 0px;
        background: #01568D;
        background-image: url(/img/webUI/search_icon_bg.png);
        background-position: center center, center center;
        background-repeat: no-repeat, repeat;
        background-size: 24px;
    }

    #txt_buscadorProducto {
        border: 2px #01568D solid !important;
        /*        box-shadow: 0px 2px 4px 0px rgba(0, 0, 0, 0.3803921568627451);*/
        border-radius: 6px;
        background: #fff;
        font-size: 1.2rem !important;
        height: 2rem;
        width: 60vw;
        padding: 0px 10px;
    }

    /*    .BuscadorContainer {
        border: solid 1px red;
        position: relative;
        float: left;
        width: 500px;
        height: 2rem;
        margin-top: 3px;
    }*/

    .login_btn {
        color: #242c33;
        margin: 0px 15px 0px 0px;
        padding: 3px 21px;
        background: #ffffff;
        border-radius: 13px;
        font-weight: 700;
    }

    #btn_menu_usuario_movil {
        color: #000;
        overflow: hidden;
        display: block;
    }

    .title_tipoDeCambio {
        display: block;
        height: 1.5rem;
    }

    #txt_tipoDeCambio::before {
        content: "1 USD = ";
    }

    .title_tipoDeCambio {
        border: 1px solid green;
    }

    #text_tipoDeCambio {
        display: inline;
    }

    /*    .content_tipoDeCambio {
  line-height: 40px;
    height: 40px;
    }*/

    .right_position {
        display: flex;
        margin-left: auto;
    }

    @media only screen and (max-width:1200px) {
        .menu_middle {
            margin: auto;
        }
    }

    @media only screen and (max-width:995px) {

        .menu_left_contenedor {
            text-align: center;
        }

        .content_header_logo {
            float: inherit;
            margin: 0px 30px 0px 0px;
        }

        .menu_top {
            text-align: right;
            margin-top: 0px;
            overflow: hidden;
            height: 35px;
        }

        #txt_tipoDeCambio::after {
            content: "TC: 1 USD = ";
        }


        .BuscadorContainer {
            margin-top: 0px;
            width: 95%;
        }

        .menuContainer {
            margin: 0 auto;
            float: initial !important;
        }

        .content_tipoDeCambio {
            display: inline;
            float: none;
            line-height: 42px;
            margin-right: 30px;
        }

        .content_menuMovil {
            float: left;
            display: inline !important;
        }

        .menu_middle {
            margin: auto;
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
    }

    @media only screen and (max-height:1000px) {
        #content_header {
            zoom: 0.8;
        }

        .BuscadorContainer {
            position: relative;
            display: flex;
        }

        #txt_buscadorProducto {
            border: 1px solid #00B4CC;
            border-right: none;
            padding: 5px;
            height: 2rem;
            border-radius: 6px 0 0 6px;
            outline: none;
            color: #9DBFAF;
            font-size: 1rem;
        }

        .btn_buscador {
            width: 6rem;
            height: 3rem;
            border-radius: 0 6px 6px 0;
            cursor: pointer;
        }
</style>

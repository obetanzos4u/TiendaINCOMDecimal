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
                <a class="collapsible-header">Productos<img src="../img/webUI/newdesign/Flecha.svg" /></a>
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

<uc_bar:adminBar ID="botonAsesores" runat="server"></uc_bar:adminBar>
<%--<div class="row z-depth-1 header white" style="margin-bottom: 0px;">--%>
<div>
    <div style="background: white; overflow: hidden; color: #353635">
        <uc_bar:modAsesor ID="barraAsesores" Visible="false" runat="server"></uc_bar:modAsesor>
    </div>
    <section class="over-header">
        <div class="center">
        <a class="btn_tuerca">
            <img class="icon_tuerca" src="../img/webUI/newdesign/Tuerca.svg" alt="boton de tuerca o ajustes"/>
        </a>
            <button class="btn_asesores" type="button">Asesores</button>
        </div>
        
        <%--        <span style="color: rgb(190 18 60) !important;">
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
            <p class="title_header">INCOM&reg; La ferretera de las telecomunicaciones®</p>
        </section>
        <div class="menu_left_contenedor  ">
            <!--- Movil --->
            <div class="content_menuMovil show-on-medium-and-down hide-on-med-and-up" style="display: inline;">
                <!-- Dropdown Trigger -->
                <a id="btn_menu_usuario_movil" data-target='menu_usuario_movil' href="#" class="sidenav-trigger">
                    <%--<i class="material-icons" style="font-size: 3rem;">menu</i>--%>
                    <img class="icon_menu" src="../img/webUI/newdesign/Menu.svg"/>Menu
                </a>


            </div>
            <%--            <a title="Incom Retail" class="content_header_logo" href='<%= HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) %>'>
                <img src='<%=ResolveUrl("~/img/webUI/incom_logo_mini.png") %>'
                    alt="Logo Incom" title="Incom,  La ferretera de las telecomunicaciones" class="responsive-img header_logo_img" />
            </a>--%>
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
                <img src="../img/webUI/incom_logo_mini.png" alt="Logotipo INCOM" class="logotipo_home" />
                <div class="menu_middle">
                    <uc_buscador:buscador ID="buscador" Visible="true" runat="server"></uc_buscador:buscador>
                </div>
                <div class="sesion_nav">
                    <div class="cuenta_container">
                     <img class="icon_cuenta" src="../img/webUI/newdesign/Cuenta.svg"  />
                     <span class="btn_cuenta">Mi cuenta</span>
                    </div>

                    <div>
                        <div id="carrito_de_compra">
                            <div style="display: flex; flex-direction: column;">
                                <uc_carrito:btnCarrito ID="carrito" runat="server"></uc_carrito:btnCarrito>
                                <p class="txt_carrito">Carrito</p>
                            </div>
                        </div>
                    </div>

                    <div class="content_tipoDeCambio">
                        <span class="title_tipoDeCambio">Tipo de cambio</span>
                        <span id="txt_tipoDeCambio"></span>
                        <strong><span class="cantidad_tipoDeCambio" ><%= operacionesConfiguraciones.obtenerTipoDeCambio() %> MXN </span></strong>
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
        font-size: 1.5rem;
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

    .over-header {
        padding: 4px;
        align-items: center;
        height:2rem;
    }

    .center {
  height: 32px;
  display: flex;
  width: 4rem;
  align-items: center;
  justify-content:flex-start;
  /*border: 3px solid #dbe4ed;*/ /* Border color is optional */  
  margin: 0;
    }

    .btn_tuerca {
        margin-top: 8px;
    }

    .icon_tuerca {
        filter: invert(18%) sepia(89%) saturate(2251%) hue-rotate(186deg) brightness(96%) contrast(99%);
        padding-left: 20px;
        align-items: center;

    }

    .btn_asesores {
        width: 4rem;
        background-color: #01568D;
        color: #FFFFFF;
        border: none;
        font-size: .8rem;
        border-radius: 9px;
        margin: 4px;
    }

    .title_container {
        background-color: #F9F7F7;
        font-size: 2rem;
        margin: 0;
    }

    .title_header {
        font-size: 1.5rem;
        display: flex;
        margin: auto;
        width: fit-content;
        align-items: center;
        font-weight: 900;
        color: #0C3766;
    }

    .content_header_logo {
        float: left;
        margin: 0px 80px 0px 0px;
    }

    .header_logo_img {
        max-height: 4rem;
    }

    .header_toolbar {
/*        border: 1px solid #00B4CC;*/
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        height: 4rem;
        padding: .5rem 3rem .5rem 2rem;
        padding-bottom: 5rem;
        margin: auto 0rem 1.25rem 0rem;
        box-shadow: rgba(0, 0, 0, 0.15) 0px 2px 2.6px;
    }

    .logotipo_home {
        height: 3rem;
        width: auto;
    }

    .sesion_nav {
/*        border: 1px solid #6b6129;*/
        display: flex;
        flex-direction: row;
        height: 3rem;
    }
    
    .cuenta_container {
        display: flex;
        flex-direction: column;
        width: 7rem;
        align-items: center;
        border-right: 2px solid black;
    }

    .icon_cuenta {
        width: 1.75rem;
    }

    .btn_cuenta {
        margin: auto;
        font-weight: 800;
    }

    #carrito_de_compra {
        display: flex;
        width: 7rem;
        align-items: center;
        justify-content: center;
    }

    .txt_carrito {
        font-weight: 600;
        margin:0;
    }

    .shop_button {
    /*    border: 1px #01568D solid;*/
        width: 80px;
        display: block;
    }

    .menu_top {
        text-align: right;
        margin-top: 0px;
        overflow: hidden;
    }

/*    .menu_middle {
          overflow: hidden;
        margin: 0px 35%;
    }*/

    .menu_middle {
    height: fit-content;
    width: 100vw;
    align-items: flex-start;
    margin-left: 2rem;
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
        width: 6rem;
        height: 2rem;
        margin-top: 1rem;
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
        font-size: 1rem;
        height: 3rem;
        margin-top: 1rem;
        width: 60vw;
    }

    #txt_buscadorProducto::placeholder {
        font-style: italic;
    }

/*        .BuscadorContainer {
        border: solid 1px red;
        position: relative;
        float: left;
        width: 500px;
        height: 2rem;
        margin-top: 3px;
    }*/
/*.buscador_container {
    border: 2px solid brown;
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
        font-weight:600;
        text-align: center;
        font-size: 0.75rem;F
    }

    #txt_tipoDeCambio {
        font-weight:600;
        font-size: 0.75rem;
    }

    #txt_tipoDeCambio::before {
        content: "1 USD = ";
    }

    .icon_menu {
        filter: invert(18%) sepia(89%) saturate(2251%) hue-rotate(186deg) brightness(96%) contrast(99%);
    }
   /* .title_tipoDeCambio {
        border: 1px solid green;
    }*/

    .cantidad_tipoDeCambio {
        font-weight: 600;
        font-size: .75rem;
    }

    /*    .content_tipoDeCambio {
  line-height: 40px;
    height: 40px;
    }*/
   .content_tipoDeCambio{
       border-left: 2px solid black;
       padding-left: 1rem;
       width: 9rem;
   }

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


        .buscador_container {
            margin-top: 0px;
            width: 95%;
            font-style: italic;
        }

        #txt_buscadorProducto {
            font-style: italic;
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
            border-left: 2px solid black;

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

        .buscador_container {
            display: flex;
        }

        #txt_buscadorProducto {
            border: 1px solid #00B4CC;
            border-right: none;
            padding: 5px;
            height: 1.5rem;
            border-radius: 6px 0 0 6px;
            outline: none;
            color: #9DBFAF;
            font-size: 1rem;
        }

        .btn_buscador {
            width: 4rem;
            height: 2.4rem;
            border-radius: 0 6px 6px 0;
            border: none;
            cursor: pointer;
        }

        #txt_buscadorProducto {
        border: 3px #01568D solid !important;
        height: 2rem;
        margin-left: 2rem;
        padding: 0px 10px;
    }

        .header_toolbar {
            padding-right: 1rem;
        }
</style>

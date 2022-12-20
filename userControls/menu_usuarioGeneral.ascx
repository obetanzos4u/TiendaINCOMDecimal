<%@ Control Language="C#" AutoEventWireup="true"  CodeFile="menu_usuarioGeneral.ascx.cs" Inherits="menu_usuarioGeneral" %>



<nav class="nav navIncom is-flex is-bt-5">
    <div class="nav-wrapper is-bg-blue-darky is-flex is-container is-justify-center">
        <a href="#" data-target="mobile-demo" class="sidenav-trigger" ><i class="material-icons">menu</i></a>
        <ul id="nav-mobile" class="left hide-on-med-and-down  ">
            <li>
                <a href="/usuario/mi-cuenta/mi-cuenta.aspx">
                    <div class="is-flex" style="align-items: center;">
                        <img src="/img/webUI/newdesign/user.png" style="width: 24px; margin-right: 1rem; margin-bottom: 0.25rem"/>
                        <span>Mi cuenta</span>
                    </div>
                </a>
            </li>
            <li><a href="/usuario/mi-cuenta/cotizaciones.aspx"><i class="material-icons left">assignment</i> Cotizaciones</a></li>
            <li>
                <a href="/usuario/mi-cuenta/pedidos.aspx">
                    <div class="is-flex" style="align-items: center;">
                        <img src="/img/webUI/newdesign/shopping-bag.png" style="width: 26px; margin-bottom: 0.25rem; margin-right: 1rem; height: 1.5rem;"/>
                        <span>Pedidos</span>
                    </div>
                </a>
            </li>
            <li><a href="/usuario/mi-cuenta/direcciones-de-facturacion.aspx"><i class="material-icons left">playlist_add_check</i> Direcciones de facturación</a></li>
            <li>
                <a href="/usuario/mi-cuenta/direcciones-de-envio.aspx">
                    <div class="container-icon_mi_cuenta-entrega is-flex is-items-center">
                        <img src="/img/webUI/newdesign/entrega.png" class="icon_mi_cuenta-entrega"/>
                        <span>Direcciones de envío</span>
                    </div>
                </a>
            </li>  
            <li><a href="/usuario/mi-cuenta/contactos.aspx"><i class="material-icons left">people</i> Contactos</a></li>
        </ul>
        <ul class="sidenav" id="mobile-demo">
            <li><a href="/usuario/mi-cuenta/mi-cuenta.aspx" class="grey-text text-darken-3"><img class="menu_movil-mi_cuenta" src="/img/webUI/newdesign/user_menu-movil.png">Mi cuenta</a></li>
            <li><a href="/usuario/mi-cuenta/cotizaciones.aspx" class="grey-text text-darken-3"><i class="material-icons left">assignment</i> Cotizaciones</a></li>
            <li><a href="/usuario/mi-cuenta/pedidos.aspx" class="grey-text text-darken-3"><img class="menu_movil-pedidos" src="/img/webUI/newdesign/shopping-bag-gray.png">Pedidos</a></li>
            <li><a href="/usuario/mi-cuenta/direcciones-de-facturacion.aspx" class="grey-text text-darken-3">
                <i class="material-icons left">playlist_add_check</i> Direcc. de facturación</a></li>
            <li><a href="/usuario/mi-cuenta/direcciones-de-envio.aspx" class="grey-text text-darken-3"><img class="menu_movil-direcciones_envio" src="/img/webUI/newdesign/entrega_rapida_gray.png"/>Direcciones de envío</a></li>
            <li><a href="/usuario/mi-cuenta/contactos.aspx" class="grey-text text-darken-3"><i class="material-icons left">people</i> Contactos</a></li>
        </ul>
    </div>
</nav>

<script type="text/javascript">

    document.addEventListener('DOMContentLoaded', function() {
    var elems = document.querySelectorAll('.sidenav');
    var instances = M.Sidenav.init(elems, null);
  });
</script>

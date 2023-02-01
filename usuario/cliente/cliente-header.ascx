<%@ Control Language="C#" AutoEventWireup="true" CodeFile="cliente-header.ascx.cs" Inherits="usuario_cliente_cliente_header" %>

<div class="is-flex is-flex-col is-justify-center is-items-center is-m-0 is-p-2 is-shadow">
    <a href="/">
        <img alt="INCOM La ferretera de las telecomunicaciones" src="https://www.incom.mx/img/webUI/newdesign/Incom_nuevo.png" class="logotipo_pedido" />
    </a>
    <div id="barraProgreso" runat="server"></div>
</div>

<%--<div class="row bg-white">
    <div class="col-12">
        <nav class="is-flex is-justify-center is-items-center">
            <a class="" href="/">
                <img alt="INCOM La ferretera de las telecomunicaciones" src="https://www.incom.mx/img/webUI/newdesign/Incom_nuevo.png" class="logotipo_pedido">
            </a>
            <%--<button class="navbar-toggler" type="button" data-bs-toggle="collapse"
                data-bs-target="#navbarToggleExternalContent" aria-controls="navbarToggleExternalContent" aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon"></span>
            </button>

            <div class="collapse navbar-collapse" id="navbarToggleExternalContent">

                <div class="input-group col me-2">
                    <input type="text" id="txt_buscador_principal" class="form-control"
                        placeholder="Buscar" aria-label="Buscador" aria-describedby="button-addon4">
                    <div class="input-group-append" id="button-addon4">
                        <button id="btn_buscar" class="btn btn-success" type="button"><i class="fas fa-search"></i></button>
                    </div>
                </div>
                <ul class="nav d-md-block d-lg-none">
                    <li class="nav-item"><a href="/productos" class="nav-link">Productos</a>  </li>
                    <li class="nav-item"><a href="/mi-carrito.aspx" class="nav-link">Carrito</a>  </li>
                </ul>
                <ul class="navbar-nav mr-auto">
                    <asp:LoginView ID="LoginView2" runat="server">
                        <LoggedInTemplate>
                            <asp:HyperLink ID="miCuenta" class="btn btn-primary mb-2 " NavigateUrl="~/usuario/mi-cuenta/mi-cuenta.aspx"
                                runat="server"> Mi cuenta </asp:HyperLink>
                            <a href="/usuario/mi-cuenta/pedidos.aspx" class="btn btn-primary mb-2 d-lg-none">Pedidos</a>
                            <a href="/usuario/mi-cuenta/cotizaciones.aspx" class="btn btn-primary mb-2 d-lg-none">Cotizaciones</a>
                            <asp:LinkButton ID="btn_loggout" class="btn btn-outline-secondary mb-2 ms-2" OnClick="btn_loggout_Click" runat="server">Cerrar sesión</asp:LinkButton>
                        </LoggedInTemplate>
                        <AnonymousTemplate>
                            <li class="nav-item">
                                <a class=" btn btn-primary " href="#">Registro</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#">Iniciar&nbsp;Sesión</a>
                            </li>
                        </AnonymousTemplate>
                    </asp:LoginView>

                </ul>
            </div>
        </nav>
    </div>
</div>--%>

<%--<nav class="navbar navbar-expand-lg navbar-light  bg-white  mb-3" style="padding: .1rem .7rem;">
    <div class="collapse navbar-collapse" id="navbarSupportedContent">
<%--        <ul class="navbar-nav mr-auto">

            <li class="nav-item dropdown">
                <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown_Enseñanza" role="button" data-bs-toggle="dropdown" aria-expanded="false">Enseñanza
                </a>
                <div class="dropdown-menu" aria-labelledby="navbarDropdown_Enseñanza">
                    <a class="dropdown-item" href="/glosario/A">Enciclopédio</a>
                    <a class="dropdown-item" href="/enseñanza/infografías.aspx">Infografías</a>
                    <a class="dropdown-item" href="https://blog.incom.mx/">Blog</a>
                </div>
            </li>

            <li class="nav-item dropdown">
            <li class="nav-item dropdown">
                <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown_Productos" role="button" data-bs-toggle="dropdown" aria-expanded="false">Productos
                </a>
                <div id="Submenu_Productos" class="Submenu_Productos dropdown-menu" runat="server" aria-labelledby="navbarDropdown_Productos">
                </div>
            </li>
            <li class="nav-item dropdown">
                <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown_Marcas" role="button" data-bs-toggle="dropdown" aria-expanded="false">Marcas
                </a>
                <div id="Submenu_Marcas" class="Submenu_Marcas dropdown-menu " runat="server" aria-labelledby="navbarDropdown_Marcas">
                    <input id="txt_buscadorMenuMarcas" type="text" placeholder="Busca tu marca" class="form-control ">
                </div>
            </li>




        </ul>
        <ul class="navbar-nav ">
            <li class="nav-item">
                <a class="nav-link" href="/mi-carrito.aspx"><i class="fas fa-shopping-cart"></i>Carrito</a>
            </li>
        </ul>
    </div>
</nav>--%>

<style>
    /*    .Submenu_Marcas, .Submenu_Productos {
        max-height: 400px;
        overflow-y: auto;
    }*/

    .logotipo_pedido {
        height: 3rem;
        width: auto;
        margin-top: 0;
    }
</style>

<%--<script>
    document.addEventListener("DOMContentLoaded", (event) => {
        BootstrapTxtAction("#txt_buscador_principal", "#btn_buscar")
        // INICIO Buscador en listado de marcas

        //var input = document.querySelector('#txt_buscadorMenuMarcas');
        //input.onkeyup = function () {
        //    var filter = input.value.toUpperCase();
        //    var lis = document.querySelectorAll(".Submenu_Marcas > a");
        //    for (var i = 0; i < lis.length; i++) {

        //        var name = lis[i].innerHTML;

        //        if (name.toUpperCase().includes(filter))
        //            lis[i].style.display = 'block';
        //        else lis[i].style.display = 'none';
        //    }
        //}
        // FIN Buscador en listado de marcas


        // INICIO - Boton de buscar principal


        //var btn_buscador = document.querySelector('#btn_buscar');

        //btn_buscador.onclick = function () {
        //    var text = document.querySelector('#txt_buscador_principal').value;

        //    location.replace("/productos/buscar?busqueda=" + text);

        //}

        // FIN - Boton de buscar principal
    });
</script>--%>

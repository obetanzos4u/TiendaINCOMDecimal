<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_Header.ascx.cs" Inherits="bulmaCSS_uc_Header" %>


<nav class="navbar is-warning" role="navigation" aria-label="main navigation">
    <div class="navbar-brand">
        <a class="navbar-item" href='<%= HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) %>'>
            <img alt="Incom logo" src="/img/webUI/incom_retail_logo_header_big.png" />
        </a>
        <a role="button" class="navbar-burger burger" aria-label="menu" aria-expanded="false" data-target="navbarBasicExample">
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
            <span aria-hidden="true"></span>
        </a>
    </div>

    <div id="navbarBasicExample" class="navbar-menu">
        <div class="navbar-start">
            <a class="navbar-item is-primary" href="/productos">
                <span class="icon  ">
                    <i class="fas fa-shopping-cart "></i>
                </span>
                &nbsp; Tienda
            </a>

            <a class="navbar-item" href="/glosario/a">Enciclopédico</a>
            <a class="navbar-item" href="/enseñanza/infografías">Infografías</a>
            <a class="navbar-item" target="_blank" href="https://blog.incom.mx/">Blog</a>
            <div class="navbar-item has-dropdown is-hoverable is-hidden	">
                <a class="navbar-link">Más</a>
                <div class="navbar-dropdown">
                    <a class="navbar-item">Eventos</a>
                    <a class="navbar-item">Manuales</a>
                    <a class="navbar-item">Wallpapers</a>
                    <hr class="navbar-divider">
                    <a class="navbar-item">Contácto</a>
                </div>
            </div>
        </div>

        <div class="navbar-end">
            <div class="navbar-item">
                <div class="buttons">
                    <asp:LoginView ID="LoginView2" runat="server">
                        <LoggedInTemplate>
                            <asp:HyperLink ID="miCuenta" ToolTip="Mi cuenta" class="button is-light" NavigateUrl="~/usuario/mi-cuenta/mi-cuenta.aspx"
                                runat="server"> Mi cuenta </asp:HyperLink>
                        </LoggedInTemplate>
                        <AnonymousTemplate>
                            <a href="/registro-de-usuario.aspx" class="button is-link">
                                <strong>Registro</strong>
                            </a>
                            <a class="button is-light" href="../iniciar-sesion.aspx?ReturnUrl=<%=HttpContext.Current.Request.Url.AbsolutePath %>">Iniciar sesión
                            </a>
                        </AnonymousTemplate>
                    </asp:LoginView>

                </div>
            </div>
        </div>
    </div>
</nav>

<script>
    document.addEventListener('DOMContentLoaded', () => {
        // Get all "navbar-burger" elements
        const $navbarBurgers = Array.prototype.slice.call(document.querySelectorAll('.navbar-burger'), 0);

        // Check if there are any navbar burgers
        if ($navbarBurgers.length > 0) {

            // Add a click event on each of them
            $navbarBurgers.forEach(el => {
                el.addEventListener('click', () => {

                    // Get the target from the "data-target" attribute
                    const target = el.dataset.target;
                    const $target = document.getElementById(target);

                    // Toggle the "is-active" class on both the "navbar-burger" and the "navbar-menu"
                    el.classList.toggle('is-active');
                    $target.classList.toggle('is-active');

                });
            });
        }

    });
</script>

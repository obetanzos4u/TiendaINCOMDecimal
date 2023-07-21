<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_admin_bar_button.ascx.cs" Inherits="uc_admin_bar_button" %>

<ul id="adminBar" visible="false" runat="server" class="adminBar side-nav sidenav no-autoinit">
    <li>
        <div class="user-view is-flex is-flex-col is-justify-center is-items-center">
            <%--<div class="background">
                <img src="/img/webUI/bg_sideBarAdmin.jpg" />
            </div>--%>
            <div class="background is-bg-black is-bg-richBlack"></div>
            <asp:Image ID="img_usuario" class="circle" runat="server" />
            <asp:Label ID="lbl_nombre" class="white-text name is-select-none" runat="server"></asp:Label>
            <asp:Label ID="lbl_usuario_email" class="white-text email is-select-none" runat="server"></asp:Label>
        </div>
    </li>
    <li class="is-flex is-justify-center is-items-center">
        <asp:HyperLink ID="link_fast_admin_precios" class="is-w-full" runat="server">
            <div class="is-flex is-justify-between is-items-center is-w-full">
                <div class="is-w-1 is-bg-blueSapphire">&zwnj;</div>
                <div class="is-flex is-justify-center is-items-center is-px-2">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" width="16" height="16">
                        <path fill-rule="evenodd" d="M2.5 7.775V2.75a.25.25 0 01.25-.25h5.025a.25.25 0 01.177.073l6.25 6.25a.25.25 0 010 .354l-5.025 5.025a.25.25 0 01-.354 0l-6.25-6.25a.25.25 0 01-.073-.177zm-1.5 0V2.75C1 1.784 1.784 1 2.75 1h5.025c.464 0 .91.184 1.238.513l6.25 6.25a1.75 1.75 0 010 2.474l-5.026 5.026a1.75 1.75 0 01-2.474 0l-6.25-6.25A1.75 1.75 0 011 7.775zM6 5a1 1 0 100 2 1 1 0 000-2z">
                        </path>
                    </svg>
                </div>
                <div class="is-w-full is-px-2">
                    <p class="is-m-0">Gestor de precios</p>
                </div>
            </div>
        </asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_cargaXLS" runat="server">
            <div class="is-flex is-justify-between is-items-center is-w-full">
                <div class="is-w-1 is-bg-rubyRed">&zwnj;</div>
                <div class="is-flex is-justify-center is-items-center is-px-2">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" width="16" height="16">
                        <path fill-rule="evenodd" d="M16 2.75A1.75 1.75 0 0014.25 1H1.75A1.75 1.75 0 000 2.75v2.5A1.75 1.75 0 001.75 7h12.5A1.75 1.75 0 0016 5.25v-2.5zm-1.75-.25a.25.25 0 01.25.25v2.5a.25.25 0 01-.25.25H1.75a.25.25 0 01-.25-.25v-2.5a.25.25 0 01.25-.25h12.5zM16 10.75A1.75 1.75 0 0014.25 9H1.75A1.75 1.75 0 000 10.75v2.5A1.75 1.75 0 001.75 15h12.5A1.75 1.75 0 0016 13.25v-2.5zm-1.75-.25a.25.25 0 01.25.25v2.5a.25.25 0 01-.25.25H1.75a.25.25 0 01-.25-.25v-2.5a.25.25 0 01.25-.25h12.5z">
                        </path>
                    </svg>
                </div>
                <div class="is-w-full is-px-2">
                    <p class="is-m-0">Productos XLS</p>
                </div>
            </div>
        </asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_cargarPesosyMedidas" runat="server">
            <div class="is-flex is-justify-between is-items-center is-w-full">
                <div class="is-w-1 is-bg-rubyRed">&zwnj;</div>
                <div class="is-flex is-justify-center is-items-center is-px-2">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" width="16" height="16">
                        <path fill-rule="evenodd" d="M1.75 2.5a.25.25 0 00-.25.25v1.5c0 .138.112.25.25.25h12.5a.25.25 0 00.25-.25v-1.5a.25.25 0 00-.25-.25H1.75zM0 2.75C0 1.784.784 1 1.75 1h12.5c.966 0 1.75.784 1.75 1.75v1.5A1.75 1.75 0 0114.25 6H1.75A1.75 1.75 0 010 4.25v-1.5zM1.75 7a.75.75 0 01.75.75v5.5c0 .138.112.25.25.25h10.5a.25.25 0 00.25-.25v-5.5a.75.75 0 111.5 0v5.5A1.75 1.75 0 0113.25 15H2.75A1.75 1.75 0 011 13.25v-5.5A.75.75 0 011.75 7zm4.5 1a.75.75 0 000 1.5h3.5a.75.75 0 100-1.5h-3.5z">
                        </path>
                    </svg>
                </div>
                <div class="is-w-full is-px-2">
                    <p class="is-m-0">Pesos y medidas</p>
                </div>
            </div>
        </asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_editar_producto" runat="server">
            <div class="is-flex is-justify-between is-items-center is-w-full">
                <div class="is-w-1 is-bg-rubyRed">&zwnj;</div>
                <div class="is-flex is-justify-center is-items-center is-px-2">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" width="16" height="16">
                        <path fill-rule="evenodd" d="M11.013 1.427a1.75 1.75 0 012.474 0l1.086 1.086a1.75 1.75 0 010 2.474l-8.61 8.61c-.21.21-.47.364-.756.445l-3.251.93a.75.75 0 01-.927-.928l.929-3.25a1.75 1.75 0 01.445-.758l8.61-8.61zm1.414 1.06a.25.25 0 00-.354 0L10.811 3.75l1.439 1.44 1.263-1.263a.25.25 0 000-.354l-1.086-1.086zM11.189 6.25L9.75 4.81l-6.286 6.287a.25.25 0 00-.064.108l-.558 1.953 1.953-.558a.249.249 0 00.108-.064l6.286-6.286z">
                        </path>
                    </svg>
                </div>
                <div class="is-w-full is-px-2">
                    <p class="is-m-0">Editar producto</p>
                </div>
            </div>
        </asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_agregar_producto_a_pedido" runat="server">
            <div class="is-flex is-justify-between is-items-center is-w-full">
                <div class="is-w-1 is-bg-rubyRed">&zwnj;</div>
                <div class="is-flex is-justify-center is-items-center is-px-2">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" width="16" height="16">
                        <path fill-rule="evenodd" d="M13.25 2.5H2.75a.25.25 0 00-.25.25v10.5c0 .138.112.25.25.25h10.5a.25.25 0 00.25-.25V2.75a.25.25 0 00-.25-.25zM2.75 1h10.5c.966 0 1.75.784 1.75 1.75v10.5A1.75 1.75 0 0113.25 15H2.75A1.75 1.75 0 011 13.25V2.75C1 1.784 1.784 1 2.75 1zM8 4a.75.75 0 01.75.75v2.5h2.5a.75.75 0 010 1.5h-2.5v2.5a.75.75 0 01-1.5 0v-2.5h-2.5a.75.75 0 010-1.5h2.5v-2.5A.75.75 0 018 4z">
                        </path>
                    </svg>
                </div>
                <div class="is-w-full is-px-2">
                    <p class="is-m-0">Modificar pedido</p>
                </div>
            </div>
        </asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_cargar_productos_cantidad_maxima_venta" runat="server">
            <div class="is-flex is-justify-between is-items-center is-w-full">
                <div class="is-w-1 is-bg-rubyRed">&zwnj;</div>
                <div class="is-flex is-justify-center is-items-center is-px-2">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" width="16" height="16">
                        <path fill-rule="evenodd" d="M0 2.75A2.75 2.75 0 012.75 0h10.5A2.75 2.75 0 0116 2.75v10.5A2.75 2.75 0 0113.25 16H2.75A2.75 2.75 0 010 13.25V2.75zM2.75 1.5c-.69 0-1.25.56-1.25 1.25v10.5c0 .69.56 1.25 1.25 1.25h10.5c.69 0 1.25-.56 1.25-1.25V2.75c0-.69-.56-1.25-1.25-1.25H2.75z">
                        </path>
                        <path d="M8 4a.75.75 0 01.75.75V6.7l1.69-.975a.75.75 0 01.75 1.3L9.5 8l1.69.976a.75.75 0 01-.75 1.298L8.75 9.3v1.951a.75.75 0 01-1.5 0V9.299l-1.69.976a.75.75 0 01-.75-1.3L6.5 8l-1.69-.975a.75.75 0 01.75-1.3l1.69.976V4.75A.75.75 0 018 4z">
                        </path>
                    </svg>
                </div>
                <div class="is-w-full is-px-2">
                    <p class="is-m-0">Bloquear productos</p>
                </div>
            </div>
        </asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_cargar_multimedia" runat="server">
            <div class="is-flex is-justify-between is-items-center is-w-full">
                <div class="is-w-1 is-bg-gamboge">&zwnj;</div>
                <div class="is-flex is-justify-center is-items-center is-px-2">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" width="16" height="16">
                        <path fill-rule="evenodd" d="M1.75 2.5a.25.25 0 00-.25.25v10.5c0 .138.112.25.25.25h.94a.76.76 0 01.03-.03l6.077-6.078a1.75 1.75 0 012.412-.06L14.5 10.31V2.75a.25.25 0 00-.25-.25H1.75zm12.5 11H4.81l5.048-5.047a.25.25 0 01.344-.009l4.298 3.889v.917a.25.25 0 01-.25.25zm1.75-.25V2.75A1.75 1.75 0 0014.25 1H1.75A1.75 1.75 0 000 2.75v10.5C0 14.216.784 15 1.75 15h12.5A1.75 1.75 0 0016 13.25zM5.5 6a.5.5 0 11-1 0 .5.5 0 011 0zM7 6a2 2 0 11-4 0 2 2 0 014 0z">
                        </path>
                    </svg>
                </div>
                <div class="is-w-full is-px-2">
                    <p class="is-m-0">Carga multimedia</p>
                </div>
            </div>
        </asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_slider_home" runat="server">
            <div class="is-flex is-justify-between is-items-center is-w-full">
                <div class="is-w-1 is-bg-gamboge">&zwnj;</div>
                <div class="is-flex is-justify-center is-items-center is-px-2">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" width="16" height="16">
                        <path fill-rule="evenodd" d="M7.75 14A1.75 1.75 0 016 12.25v-8.5C6 2.784 6.784 2 7.75 2h6.5c.966 0 1.75.784 1.75 1.75v8.5A1.75 1.75 0 0114.25 14h-6.5zm-.25-1.75c0 .138.112.25.25.25h6.5a.25.25 0 00.25-.25v-8.5a.25.25 0 00-.25-.25h-6.5a.25.25 0 00-.25.25v8.5zM4.9 3.508a.75.75 0 01-.274 1.025.25.25 0 00-.126.217v6.5a.25.25 0 00.126.217.75.75 0 01-.752 1.298A1.75 1.75 0 013 11.25v-6.5c0-.649.353-1.214.874-1.516a.75.75 0 011.025.274zM1.625 5.533a.75.75 0 10-.752-1.299A1.75 1.75 0 000 5.75v4.5c0 .649.353 1.214.874 1.515a.75.75 0 10.752-1.298.25.25 0 01-.126-.217v-4.5a.25.25 0 01.126-.217z">
                        </path>
                    </svg>
                </div>
                <div class="is-w-full is-px-2">
                    <p class="is-m-0">Avisos</p>
                </div>
            </div>
        </asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_tipo_de_cambio" runat="server">
            <div class="is-flex is-justify-between is-items-center is-w-full">
                <div class="is-w-1 is-bg-blueSapphire">&zwnj;</div>
                <div class="is-flex is-justify-center is-items-center is-px-2">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" width="16" height="16">
                        <path fill-rule="evenodd" d="M8.75.75a.75.75 0 00-1.5 0V2h-.984c-.305 0-.604.08-.869.23l-1.288.737A.25.25 0 013.984 3H1.75a.75.75 0 000 1.5h.428L.066 9.192a.75.75 0 00.154.838l.53-.53-.53.53v.001l.002.002.002.002.006.006.016.015.045.04a3.514 3.514 0 00.686.45A4.492 4.492 0 003 11c.88 0 1.556-.22 2.023-.454a3.515 3.515 0 00.686-.45l.045-.04.016-.015.006-.006.002-.002.001-.002L5.25 9.5l.53.53a.75.75 0 00.154-.838L3.822 4.5h.162c.305 0 .604-.08.869-.23l1.289-.737a.25.25 0 01.124-.033h.984V13h-2.5a.75.75 0 000 1.5h6.5a.75.75 0 000-1.5h-2.5V3.5h.984a.25.25 0 01.124.033l1.29.736c.264.152.563.231.868.231h.162l-2.112 4.692a.75.75 0 00.154.838l.53-.53-.53.53v.001l.002.002.002.002.006.006.016.015.045.04a3.517 3.517 0 00.686.45A4.492 4.492 0 0013 11c.88 0 1.556-.22 2.023-.454a3.512 3.512 0 00.686-.45l.045-.04.01-.01.006-.005.006-.006.002-.002.001-.002-.529-.531.53.53a.75.75 0 00.154-.838L13.823 4.5h.427a.75.75 0 000-1.5h-2.234a.25.25 0 01-.124-.033l-1.29-.736A1.75 1.75 0 009.735 2H8.75V.75zM1.695 9.227c.285.135.718.273 1.305.273s1.02-.138 1.305-.273L3 6.327l-1.305 2.9zm10 0c.285.135.718.273 1.305.273s1.02-.138 1.305-.273L13 6.327l-1.305 2.9z">
                        </path>
                    </svg>
                </div>
                <div class="is-w-full is-px-2">
                    <p class="is-m-0">Tipo de cambio</p>
                </div>
            </div>
        </asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_precios_fantasma" runat="server">
            <div class="is-flex is-justify-between is-items-center is-w-full">
                <div class="is-w-1 is-bg-blueSapphire">&zwnj;</div>
                <div class="is-flex is-justify-center is-items-center is-px-2">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" width="16" height="16">
                        <path fill-rule="evenodd" d="M6.749.097a8.054 8.054 0 012.502 0 .75.75 0 11-.233 1.482 6.554 6.554 0 00-2.036 0A.75.75 0 016.749.097zM4.345 1.693A.75.75 0 014.18 2.74a6.542 6.542 0 00-1.44 1.44.75.75 0 01-1.212-.883 8.042 8.042 0 011.769-1.77.75.75 0 011.048.166zm7.31 0a.75.75 0 011.048-.165 8.04 8.04 0 011.77 1.769.75.75 0 11-1.214.883 6.542 6.542 0 00-1.439-1.44.75.75 0 01-.165-1.047zM.955 6.125a.75.75 0 01.624.857 6.554 6.554 0 000 2.036.75.75 0 01-1.482.233 8.054 8.054 0 010-2.502.75.75 0 01.858-.624zm14.09 0a.75.75 0 01.858.624 8.057 8.057 0 010 2.502.75.75 0 01-1.482-.233 6.55 6.55 0 000-2.036.75.75 0 01.624-.857zm-13.352 5.53a.75.75 0 011.048.165 6.542 6.542 0 001.439 1.44.75.75 0 01-.883 1.212 8.04 8.04 0 01-1.77-1.769.75.75 0 01.166-1.048zm12.614 0a.75.75 0 01.165 1.048 8.038 8.038 0 01-1.769 1.77.75.75 0 11-.883-1.214 6.543 6.543 0 001.44-1.439.75.75 0 011.047-.165zm-8.182 3.39a.75.75 0 01.857-.624 6.55 6.55 0 002.036 0 .75.75 0 01.233 1.482 8.057 8.057 0 01-2.502 0 .75.75 0 01-.624-.858z">
                        </path>
                    </svg>
                </div>
                <div class="is-w-full is-px-2">
                    <p class="is-m-0">Precios fantasma</p>
                </div>
            </div>
        </asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_XLS_export" runat="server">
            <div class="is-flex is-justify-between is-items-center is-w-full">
                <div class="is-w-1 is-bg-middleBlueGreen">&zwnj;</div>
                <div class="is-flex is-justify-center is-items-center is-px-2">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" width="16" height="16">
                        <path fill-rule="evenodd" d="M0 1.75C0 .784.784 0 1.75 0h12.5C15.216 0 16 .784 16 1.75v12.5A1.75 1.75 0 0114.25 16H1.75A1.75 1.75 0 010 14.25V1.75zM1.5 6.5v7.75c0 .138.112.25.25.25H5v-8H1.5zM5 5H1.5V1.75a.25.25 0 01.25-.25H5V5zm1.5 1.5v8h7.75a.25.25 0 00.25-.25V6.5h-8zm8-1.5h-8V1.5h7.75a.25.25 0 01.25.25V5z">
                        </path>
                    </svg>
                </div>
                <div class="is-w-full is-px-2">
                    <p class="is-m-0">Reportes XLS</p>
                </div>
            </div>
        </asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_editar_usuario" runat="server">
            <div class="is-flex is-justify-between is-items-center is-w-full">
                <div class="is-w-1 is-bg-viridianGreen">&zwnj;</div>
                <div class="is-flex is-justify-center is-items-center is-px-2">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" width="16" height="16">
                        <path fill-rule="evenodd" d="M5.5 3.5a2 2 0 100 4 2 2 0 000-4zM2 5.5a3.5 3.5 0 115.898 2.549 5.507 5.507 0 013.034 4.084.75.75 0 11-1.482.235 4.001 4.001 0 00-7.9 0 .75.75 0 01-1.482-.236A5.507 5.507 0 013.102 8.05 3.49 3.49 0 012 5.5zM11 4a.75.75 0 100 1.5 1.5 1.5 0 01.666 2.844.75.75 0 00-.416.672v.352a.75.75 0 00.574.73c1.2.289 2.162 1.2 2.522 2.372a.75.75 0 101.434-.44 5.01 5.01 0 00-2.56-3.012A3 3 0 0011 4z">
                        </path>
                    </svg>
                </div>
                <div class="is-w-full is-px-2">
                    <p class="is-m-0">Gestor de usuarios</p>
                </div>
            </div>
        </asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_admin_pedidos" runat="server">
            <div class="is-flex is-justify-between is-items-center is-w-full">
                <div class="is-w-1 is-bg-viridianGreen">&zwnj;</div>
                <div class="is-flex is-justify-center is-items-center is-px-2">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" width="16" height="16">
                        <path fill-rule="evenodd" d="M1.75 1A1.75 1.75 0 000 2.75v4c0 .372.116.717.314 1a1.742 1.742 0 00-.314 1v4c0 .966.784 1.75 1.75 1.75h12.5A1.75 1.75 0 0016 12.75v-4c0-.372-.116-.717-.314-1 .198-.283.314-.628.314-1v-4A1.75 1.75 0 0014.25 1H1.75zm0 7.5a.25.25 0 00-.25.25v4c0 .138.112.25.25.25h12.5a.25.25 0 00.25-.25v-4a.25.25 0 00-.25-.25H1.75zM1.5 2.75a.25.25 0 01.25-.25h12.5a.25.25 0 01.25.25v4a.25.25 0 01-.25.25H1.75a.25.25 0 01-.25-.25v-4zm5.5 2A.75.75 0 017.75 4h4.5a.75.75 0 010 1.5h-4.5A.75.75 0 017 4.75zM7.75 10a.75.75 0 000 1.5h4.5a.75.75 0 000-1.5h-4.5zM3 4.75A.75.75 0 013.75 4h.5a.75.75 0 010 1.5h-.5A.75.75 0 013 4.75zM3.75 10a.75.75 0 000 1.5h.5a.75.75 0 000-1.5h-.5z">
                        </path>
                    </svg>
                </div>
                <div class="is-w-full is-px-2">
                    <p class="is-m-0">Gestor de pedidos</p>
                </div>
            </div>
        </asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_api_calculo_envios" runat="server">
            <div class="is-flex is-justify-between is-items-center is-w-full">
                <div class="is-w-1 is-bg-rust">&zwnj;</div>
                <div class="is-flex is-justify-center is-items-center is-px-2">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" width="16" height="16">
                        <path d="M7.823 1.677L4.927 4.573A.25.25 0 005.104 5H7.25v3.236a.75.75 0 101.5 0V5h2.146a.25.25 0 00.177-.427L8.177 1.677a.25.25 0 00-.354 0zM13.75 11a.75.75 0 000 1.5h.5a.75.75 0 000-1.5h-.5zm-3.75.75a.75.75 0 01.75-.75h.5a.75.75 0 010 1.5h-.5a.75.75 0 01-.75-.75zM7.75 11a.75.75 0 000 1.5h.5a.75.75 0 000-1.5h-.5zM4 11.75a.75.75 0 01.75-.75h.5a.75.75 0 010 1.5h-.5a.75.75 0 01-.75-.75zM1.75 11a.75.75 0 000 1.5h.5a.75.75 0 000-1.5h-.5z">
                        </path>
                    </svg>
                </div>
                <div class="is-w-full is-px-2">
                    <p class="is-m-0">API envíos</p>
                </div>
            </div>
        </asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="link_log_errores_sql" runat="server">
            <div class="is-flex is-justify-between is-items-center is-w-full">
                <div class="is-w-1 is-bg-rust">&zwnj;</div>
                <div class="is-flex is-justify-center is-items-center is-px-2">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" width="16" height="16">
                        <path fill-rule="evenodd" d="M4.72.22a.75.75 0 011.06 0l1 .999a3.492 3.492 0 012.441 0l.999-1a.75.75 0 111.06 1.061l-.775.776c.616.63.995 1.493.995 2.444v.327c0 .1-.009.197-.025.292.408.14.764.392 1.029.722l1.968-.787a.75.75 0 01.556 1.392L13 7.258V9h2.25a.75.75 0 010 1.5H13v.5c0 .409-.049.806-.141 1.186l2.17.868a.75.75 0 01-.557 1.392l-2.184-.873A4.997 4.997 0 018 16a4.997 4.997 0 01-4.288-2.427l-2.183.873a.75.75 0 01-.558-1.392l2.17-.868A5.013 5.013 0 013 11v-.5H.75a.75.75 0 010-1.5H3V7.258L.971 6.446a.75.75 0 01.558-1.392l1.967.787c.265-.33.62-.583 1.03-.722a1.684 1.684 0 01-.026-.292V4.5c0-.951.38-1.814.995-2.444L4.72 1.28a.75.75 0 010-1.06zM6.173 5h3.654A.173.173 0 0010 4.827V4.5a2 2 0 10-4 0v.327c0 .096.077.173.173.173zM5.25 6.5a.75.75 0 00-.75.75V11a3.5 3.5 0 107 0V7.25a.75.75 0 00-.75-.75h-5.5z">
                        </path>
                    </svg>
                </div>
                <div class="is-w-full is-px-2">
                    <p class="is-m-0">Registro de errores</p>
                </div>
            </div>
        </asp:HyperLink>
    </li>
    <li>
        <div class="divider"></div>
    </li>
    <%--    <li><a class="subheader">Opciones</a></li>--%>
    <li>
        <a class="waves-effect" href="<%=Request.Url.GetLeftPart(UriPartial.Authority) %>">
            <div class="is-flex is-justify-between is-items-center is-w-full">
                <div style="width: 4px; background-color: transparent;">&zwnj;</div>
                <div class="is-flex is-justify-center is-items-center is-px-2">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" width="16" height="16">
                        <path fill-rule="evenodd" d="M8.156 1.835a.25.25 0 00-.312 0l-5.25 4.2a.25.25 0 00-.094.196v7.019c0 .138.112.25.25.25H5.5V8.25a.75.75 0 01.75-.75h3.5a.75.75 0 01.75.75v5.25h2.75a.25.25 0 00.25-.25V6.23a.25.25 0 00-.094-.195l-5.25-4.2zM6.906.664a1.75 1.75 0 012.187 0l5.25 4.2c.415.332.657.835.657 1.367v7.019A1.75 1.75 0 0113.25 15h-3.5a.75.75 0 01-.75-.75V9H7v5.25a.75.75 0 01-.75.75h-3.5A1.75 1.75 0 011 13.25V6.23c0-.531.242-1.034.657-1.366l5.25-4.2h-.001z">
                        </path>
                    </svg>
                </div>
                <div class="is-w-full is-px-2">
                    <p class="is-m-0">Regresar al inicio</p>
                </div>
            </div>
        </a>
    </li>
</ul>

<div id="content_btn_admin" class="content-btn_admin" runat="server" visible="false">
    <a id="btn_admin" href="#!" data-target='<%=adminBar.ClientID %>' class="sidenav-trigger ">
        <span class="is-flex">
            <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" id="body_1" width="24" height="18">
            <g transform="matrix(0.72 0 0 0.72 2.9999998 -0)">
                <path d="M23.77 11.96L22.01 11.7C 21.94 10.55 21.67 9.65 21.27 8.74L21.27 8.74L21.28 8.71L22.710001 7.64C 23.03 7.4 23.09 6.95 22.86 6.63L22.86 6.63L21.02 4.1400003C 20.78 3.8200004 20.34 3.7500005 20.02 3.9900002L20.02 3.9900002L18.470001 5.03C 17.68 4.38 17.28 3.94 15.760001 3.4800003L15.760001 3.4800003L15.760001 3.4700003L16.160002 1.7300003C 16.220001 1.3400003 15.970002 1.0200002 15.580002 0.9600003L15.580002 0.9600003L12.550002 0.5100003C 12.160002 0.4500003 11.990002 0.7100003 11.930002 1.1100004L11.930002 1.1100004L11.680002 2.8700004C 10.55 2.94 9.62 3.2 8.7 3.6L8.7 3.6L8.66 3.59L7.58 2.15C 7.34 1.83 6.88 1.77 6.56 2L6.56 2L4.07 3.85C 3.7500002 4.0899997 3.6800003 4.5299997 3.92 4.85L3.92 4.85L4.9700003 6.34C 4.32 7.13 3.85 7.73 3.43 8.99L3.43 8.99L3.42 8.99L1.66 8.71C 1.27 8.65 1.02 8.84 0.96 9.23L0.96 9.23L0.51 12.289999C 0.45 12.679999 0.63 12.989999 1.02 13.049999L1.02 13.049999L2.78 13.309999C 2.85 14.459999 3.1299999 15.33 3.52 16.24L3.52 16.24L3.51 16.25L2.08 17.32C 1.76 17.56 1.6999999 18.01 1.93 18.32L1.93 18.32L3.77 20.81C 4.0099998 21.13 4.45 21.189999 4.77 20.96L4.77 20.96L6.25 19.91C 7.04 20.56 7.89 21.07 8.89 21.45L8.89 21.45L8.89 21.460001L8.650001 23.220001C 8.59 23.61 8.710001 23.990002 9.110001 24.050001L9.110001 24.050001L12.18 24.51C 12.570001 24.57 12.97 24.27 13.030001 23.880001L13.030001 23.880001L13.120001 22.09C 14.27 22.02 15.240001 21.76 15.85 21.31L15.85 21.31L15.85 21.32L17.08 22.779999C 17.32 23.099998 17.85 23.179998 18.17 22.939999L18.17 22.939999L20.7 21.099998C 21.02 20.859999 21.11 20.419998 20.87 20.099998L20.87 20.099998L19.84 18.559998C 20.49 17.769997 20.93 17.379997 21.4 15.869997L21.4 15.869997L21.41 15.869997L23.15 16.229998C 23.539999 16.289997 23.99 16.099998 24.05 15.709997L24.05 15.709997L24.5 12.669997C 24.55 12.27 24.16 12.02 23.77 11.96zM10.02 16.26C 9.9800005 16.24 9.940001 16.210001 9.900001 16.18C 9.780001 16.1 9.670001 16.01 9.56 15.92C 9.51 15.87 9.450001 15.83 9.400001 15.78C 9.35 15.73 9.3 15.679999 9.240001 15.63C 9.22 15.61 9.190001 15.58 9.170001 15.55C 9.030001 15.400001 8.900001 15.24 8.780001 15.070001C 8.660001 14.900001 8.550001 14.7300005 8.460001 14.540001C 8.420001 14.470001 8.390001 14.390001 8.350001 14.320001C 8.250001 14.090001 8.160002 13.860001 8.100001 13.620001C 7.9500012 13.050001 7.9100013 12.450001 8.000001 11.830001C 8.190001 10.540001 8.920001 9.460001 9.930001 8.780001C 10.020001 8.72 10.110002 8.660001 10.200002 8.610001C 10.230001 8.59 10.260002 8.570001 10.290002 8.56C 10.350002 8.530001 10.420002 8.490001 10.480001 8.46C 10.670001 8.37 10.870002 8.29 11.080002 8.22C 11.150002 8.2 11.220002 8.18 11.290002 8.16C 11.850002 8.0199995 12.450002 7.98 13.060001 8.07C 13.780002 8.179999 14.4400015 8.45 15.000002 8.849999C 15.060002 8.889999 15.120002 8.929999 15.170002 8.98C 15.250002 9.049999 15.340002 9.12 15.420002 9.19C 15.470002 9.24 15.520002 9.29 15.580002 9.339999C 15.600002 9.36 15.630002 9.389999 15.650002 9.419999C 15.700002 9.469999 15.750002 9.5199995 15.790002 9.579999C 15.860002 9.659999 15.930002 9.739999 15.990002 9.829999C 16.030003 9.889999 16.070002 9.949999 16.110003 9.999999C 16.210003 10.149999 16.300003 10.299999 16.380003 10.459999C 16.410004 10.5199995 16.440002 10.589999 16.470003 10.649999C 16.500004 10.719998 16.530003 10.779999 16.550003 10.849998C 16.590004 10.949999 16.630003 11.049998 16.660004 11.159999C 16.850004 11.779999 16.910004 12.449999 16.810003 13.139999C 16.750004 13.5199995 16.650003 13.879999 16.510004 14.219999C 16.480003 14.289999 16.450005 14.349999 16.420004 14.419999C 16.370005 14.5199995 16.320004 14.619999 16.270004 14.709999C 16.230003 14.7699995 16.200005 14.839999 16.160004 14.899999C 16.130003 14.939999 16.110004 14.979999 16.080004 15.019999C 16.030005 15.099998 15.970004 15.179998 15.910004 15.249998C 15.870004 15.3099985 15.8200035 15.359998 15.770003 15.419998C 15.730003 15.459998 15.690003 15.499998 15.660004 15.549998C 15.630004 15.589998 15.590004 15.619998 15.550004 15.659998C 15.530004 15.679998 15.500004 15.709998 15.470004 15.729998C 15.420004 15.779998 15.370004 15.829998 15.310004 15.869998C 15.2800045 15.889998 15.260004 15.919998 15.230004 15.939998C 15.180004 15.989998 15.120005 16.029997 15.060004 16.069998C 15.0300045 16.089998 15.000004 16.109999 14.970004 16.129997C 14.850004 16.209997 14.730004 16.289997 14.610004 16.359997C 14.580005 16.379997 14.550004 16.399998 14.520004 16.409996C 14.460004 16.439997 14.390004 16.479996 14.330005 16.509996C 14.270004 16.539997 14.200005 16.569996 14.130005 16.599997C 14.100005 16.609997 14.060005 16.629997 14.0300045 16.639997C 13.960005 16.669998 13.900004 16.689997 13.830005 16.709997C 13.180005 16.929996 12.470005 16.999998 11.750005 16.899998C 11.170005 16.809998 10.630005 16.619997 10.150004 16.339998C 10.11 16.31 10.07 16.28 10.02 16.26z" stroke="none" fill-rule="nonzero" />
            </g>
            </svg>
        </span>
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

    #body_1 {
        fill: #01568D;
    }

    .content-btn_admin {
        border-radius: 0.5rem;
        padding: 8px 10px;
        float: left;
        z-index: 0;
        background: #01568d;
        position: sticky;
        top: 0px;
    }
</style>

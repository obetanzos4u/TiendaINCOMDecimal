<%@ Control Language="C#" AutoEventWireup="true" CodeFile="productosAlternativos.ascx.cs" Inherits="uc_productosAlternativos" %>

<div id="alternativosSeccion" class="is-container" runat="server">
    <h2 class="is-font-semibold is-text-center is-select-none" style="line-height: 110%; margin: 0 !important; font-size: 1rem !important;">Productos alternativos</h2>
    <asp:ListView ID="lv_productosAlternativos" OnItemDataBound="lv_productosAlternativos_OnItemDataBound" runat="server">
        <LayoutTemplate>
            <section id="alternativos_splide" class="splide">
                <div class="splide__track">
                    <ul class="splide__list">
                        <div runat="server" id="itemPlaceholder"></div>
                    </ul>
                </div>
            </section>
        </LayoutTemplate>
        <ItemTemplate>
            <li class="splide__slide">
                <asp:HyperLink ID="link_producto" runat="server" CssClass="hoverLinkTituloProducto">
                    <asp:Image ID="img_producto" class="responsive-img" runat="server"/>
                    <h2 class="tituloProductoTienda" style="margin: 0px 5px; padding-top: 1rem">
                        <%#Eval("numero_parte") %> - <%#Eval("titulo") %>
                    </h2>
                </asp:HyperLink>
            </li>
        </ItemTemplate>
    </asp:ListView>
</div>

<script>
    document.addEventListener('DOMContentLoaded', () => {
        const splide = new Splide('#alternativos_splide', {
            type: 'loop',
            drag: 'free',
            focus: 'center',
            perPage: 1,
            autoWidth: true,
            autoScroll: {
                speed: -1,
            },
            autoplay: true,
            speed: 700,
            gap: '2rem',
            arrows: false,
            pauseOnHover: false,
            lazyLoad: 'nearby',
            keyboard: false,
            wheel: false,
            trimSpace: true,
            updateOnMove: true
        });
        splide.mount();
    });
</script>
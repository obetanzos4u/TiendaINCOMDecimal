<%@ Control Language="C#" AutoEventWireup="true" CodeFile="productosRelacionados.ascx.cs" Inherits="uc_productosRelacionados" %>
<%@ Register Src="~/userControls/uc_btn_agregar_carritoListado.ascx" TagPrefix="add" TagName="cart" %>

<div id="relacionadosSeccion" runat="server" class="is-container is-bt-2">
    <h2 class="is-font-semibold is-text-center is-select-none" style="line-height: 110%; margin: 0 !important; font-size: 1rem !important;">Productos relacionados</h2>
    <asp:ListView ID="lv_productosRelacionados" OnItemDataBound="lv_productosRelacionados_OnItemDataBound" runat="server">
        <LayoutTemplate>
            <section class="splide" id="relacionados_splide">
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
                    <asp:Image ID="img_producto" class="responsive-img" runat="server" />
                    <h2 class="tituloProductoTienda">
                        <%#Eval("numero_parte") %> - <%#Eval("titulo") %> 
                    </h2>
                    <add:cart ID="AddCart" numero_parte='<%# Eval("numero_parte") %>' runat="server" />
                </asp:HyperLink>
                <!--  <div class="card-content">
                        <asp:LinkButton id="btn_link" runat="server"></asp:LinkButton> 
                  </div> -->
            </li>
        </ItemTemplate>
        <EmptyDataTemplate>
        </EmptyDataTemplate>
    </asp:ListView>
</div>

<script>
    document.addEventListener("DOMContentLoaded", () => {
        const splide = new Splide("#relacionados_splide", {
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

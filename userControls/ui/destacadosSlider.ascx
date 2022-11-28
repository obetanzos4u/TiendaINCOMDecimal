<%@ Control Language="C#" AutoEventWireup="true" CodeFile="destacadosSlider.ascx.cs" Inherits="tienda.destacadosSlider" %>
<%@ Register Src="~/userControls/uc_btn_agregar_carritoListado.ascx" TagName="add" TagPrefix="uc_cart" %>

<section class="splide splide-destacados is-cursor-grab" id="destacados_splide">
    <div class="splide__track splide__track-destacados">
        <ul id="contenedorDestacados" class="splide__list splide__list-destacados" runat="server"></ul>
    </div>
    <div class="splide__progress">
        <%--<div class="splide__progress__bar">
        </div>--%>
    </div>
</section>

<script>
    const destacadosSection = document.querySelector("#destacados_splide");
    document.addEventListener('DOMContentLoaded', () => {
        const splide = new Splide('#destacados_splide', {
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
        splide.on("drag", () => {
            destacadosSection.classList.remove("is-cursor-grab");
            destacadosSection.classList.add("is-cursor-grabbing");
        });
        splide.on("dragged", () => {
            destacadosSection.classList.remove("is-cursor-grabbing");
            destacadosSection.classList.add("is-cursor-grab");
        });
    });
</script>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="destacadosSlider.ascx.cs" Inherits="tienda.destacadosSlider" %>

<section class="splide splide-destacados" id="destacados_splide">
    <div class="splide__track splide__track-destacados">
        <ul id="contenedorDestacados" class="splide__list" runat="server"></ul>
    </div>
    <div class="splide__progress">
<%--        <div class="splide__progress__bar">
        </div>--%>
    </div>
</section>

<script>
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
    });
</script>

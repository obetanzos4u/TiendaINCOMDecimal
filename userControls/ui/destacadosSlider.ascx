<%@ Control Language="C#" AutoEventWireup="true" CodeFile="destacadosSlider.ascx.cs" Inherits="tienda.destacadosSlider" %>

<section class="splide">
    <div class="splide__track">
        <ul id="contenedorDestacados" class="splide__list" runat="server"></ul>
    </div>
    <div class="splide__progress">
        <div class="splide__progress__bar">
        </div>
    </div>
</section>

<script>
    document.addEventListener('DOMContentLoaded', () => {
        const splide = new Splide('.splide', {
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
            gap: '1rem',
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

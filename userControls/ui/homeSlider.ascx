<%@ Control Language="C#" AutoEventWireup="true" CodeFile="homeSlider.ascx.cs" Inherits="tienda.homeSlider" %>

<section class="splide" id="slider_home_principal">
    <div class="splide__track">
        <ul id="bxsliderHome" class="splide__list" runat="server"></ul>
        <ul class="splide__pagination"></ul>
    </div>
</section>

<script>
    document.addEventListener('DOMContentLoaded', () => {
        const splide = new Splide('#slider_home_principal', {
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
            arrows: true,
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

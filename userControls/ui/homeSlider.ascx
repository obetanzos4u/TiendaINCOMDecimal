<%@ Control Language="C#" AutoEventWireup="true" CodeFile="homeSlider.ascx.cs" Inherits="tienda.homeSlider" %>

<section class="splide" id="slider_home_principal">
    <div class="splide__track splide__track-principal">
        <ul id="bxsliderHome" class="splide__list splide__list-principal" runat="server"></ul>
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
        new Splide('.splide', {
            classes: {
                arrows: 'splide__arrows your-class-arrows',
                arrow: 'splide__arrow your-class-arrow',
                prev: 'splide__arrow--prev your-class-prev',
                next: 'splide__arrow--next your-class-next',
            },
        });
        splide.mount();
    });
</script>

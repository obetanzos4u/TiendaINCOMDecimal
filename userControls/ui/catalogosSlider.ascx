<%@ Control Language="C#" AutoEventWireup="true" CodeFile="catalogosSlider.ascx.cs" Inherits="tienda.catalogosSlider" %>

<ul class="bxslider catalogosSlider">
    <li><a href="/documents/pdf/CATALOGO_INCOM_FERRETERIA.pdf" target="_blank" title="Ferretería">
        <img class="IncomWebpToJpg" loading="lazy" src="<%=Request.Url.GetLeftPart(UriPartial.Authority)  %>/img/webUI/sliderCatalogos/ferretería.webp"
            title="Ferretería" alt="Ferretería"></a></li>
    <li><a href="/documents/pdf/CATALOGO_INCOM_SUBTERRANEO.pdf" target="_blank" title="Subterráneo">
        <img class="IncomWebpToJpg" loading="lazy" src="<%=Request.Url.GetLeftPart(UriPartial.Authority)  %>/img/webUI/sliderCatalogos/subterraneo.webp"
            title="Subterráneo" alt="Subterráneo"></a></li>
    <li><a href="/documents/pdf/CATALOGO_INCOM_AEREO.pdf" target="_blank" title="Aéreo">
        <img class="IncomWebpToJpg" loading="lazy" src="<%=Request.Url.GetLeftPart(UriPartial.Authority)  %>/img/webUI/sliderCatalogos/instalación_aérea.webp"
            title="Aéreo" alt="Aéreo"></a></li>
    <li><a href="/documents/pdf/CATALOGO_INCOM_FIBRA_OPTICA.pdf" target="_blank" title="Fibra Óptica">
        <img class="IncomWebpToJpg" loading="lazy" src="<%=Request.Url.GetLeftPart(UriPartial.Authority)  %>/img/webUI/sliderCatalogos/fibra_optica.webp"
            title="Fibra Óptica" alt="Fibra Óptica"></a></li>
    <li><a href="/documents/pdf/CATALOGO_INCOM_TELEFONIA_Y_CATV.pdf" target="_blank" title="Telefonía & CATV">
        <img class="IncomWebpToJpg" loading="lazy" src="<%=Request.Url.GetLeftPart(UriPartial.Authority)  %>/img/webUI/sliderCatalogos/telefonía_catv.webp"
            title="Telefonía" alt="Telefonía"></a></li>
    <li><a href="/documents/pdf/CATALOGO_INCOM_ESTRUCTURADO.pdf" target="_blank" title="Cableado estructurado">
        <img class="IncomWebpToJpg" loading="lazy" src="<%=Request.Url.GetLeftPart(UriPartial.Authority)  %>/img/webUI/sliderCatalogos/cableado_estructurado.webp"
            title="Cableado estructurado" alt="Cableado estructurado"></a></li>
    <li><a href="/documents/pdf/CATALOGO_INCOM_CANALIZACION.pdf" target="_blank" title="Canalización">
        <img class="IncomWebpToJpg" loading="lazy" src="<%=Request.Url.GetLeftPart(UriPartial.Authority)  %>/img/webUI/sliderCatalogos/canalización.webp"
            title="Canalización" alt="Canalización"></a></li>

    <li><a href="/documents/pdf/CATALOGO_INCOM_IDENTIFICACION.pdf" target="_blank" title="Identificación">
        <img class="IncomWebpToJpg" loading="lazy" src="<%=Request.Url.GetLeftPart(UriPartial.Authority)  %>/img/webUI/sliderCatalogos/identificacion_redes.webp"
            title="Identificación" alt="Identificación"></a></li>
    <li><a href="/documents/pdf/CATALOGO_INCOM_BLOQUEO.pdf" target="_blank" title="Bloqueo">
        <img class="IncomWebpToJpg" loading="lazy" src="<%=Request.Url.GetLeftPart(UriPartial.Authority)  %>/img/webUI/sliderCatalogos//bloqueo.webp"
            title="Protección" alt="Protección"></a></li>
    <li><a href="/documents/promos/CATALOGO_INCOM_LIQUIDACION.pdf" target="_blank" title="Liquidación">
        <img class="IncomWebpToJpg" loading="lazy" src="<%=Request.Url.GetLeftPart(UriPartial.Authority)  %>/img/webUI/sliderCatalogos/liquidación.webp"
            title="Liquidación" alt="Liquidación"></a></li>

    <li><a href="/documents/pdf/CATALOGO_2RENT.pdf" target="_blank" title="2Rent Venta y Renta">
        <img class="IncomWebpToJpg" loading="lazy" src="<%=Request.Url.GetLeftPart(UriPartial.Authority)  %>/img/webUI/sliderCatalogos/2rent.webp"
            title="2Rent Venta y Renta" alt="2Rent Venta y Renta"></a></li>

    <li><a href="/documents/pdf/INCOM_CATALOGO_SOLUCION_FIBRA_SOPLADA.pdf" target="_blank" title="Solución Fibra Soplada">
        <img class="IncomWebpToJpg" loading="lazy" src="<%=Request.Url.GetLeftPart(UriPartial.Authority)  %>/img/webUI/sliderCatalogos/solucion_fibra_optica_soplada.webp"
            title="Solución Fibra Soplada" alt="Solución Fibra Soplada"></a></li>


    <li><a href="/documents/pdf/ICOPTIKS_CATALOGO_SOLUCIONES_PARA_FIBRA_OPTICA.pdf" target="_blank" title="SOLUCIONES PARA REDES DE FIBRA ÓPTICA">
        <img class="IncomWebpToJpg" loading="lazy" src="<%=Request.Url.GetLeftPart(UriPartial.Authority)  %>/img/webUI/sliderCatalogos/soluciones_redes_de_fibra_optica.jpg"
            title="SOLUCIONES PARA REDES DE FIBRA ÓPTICA" alt="SOLUCIONES PARA REDES DE FIBRA ÓPTICA"></a></li>

     <li><a href="/documents/pdf/CATALOGO_INCOM_ELECTRICO.pdf" target="_blank" title="Soluciones eléctricas">
        <img class="IncomWebpToJpg" loading="lazy" src="<%=Request.Url.GetLeftPart(UriPartial.Authority)  %>/img/webUI/sliderCatalogos/electrico.jpg"
            title="Soluciones eléctricas" alt="Soluciones eléctricas"></a></li>


    <li><a href="/documents/pdf/CATALOGO_MAQUINAS_PARA_SOPLADO_FREMCO.pdf" target="_blank" title="Fremco">
        <img class="IncomWebpToJpg" loading="lazy" src="<%=Request.Url.GetLeftPart(UriPartial.Authority)  %>/img/webUI/sliderCatalogos/fremco.webp"
            title="Fremco" alt="Fremco"></a></li>

</ul>
​

 
<script>
    $(function () {
        $(document).ready(function () {
            $('.catalogosSlider.bxslider').bxSlider({
                auto: false,
                autoStart: false,
                speed: 1000,
                minSlides: 2,
                maxSlides: 8,
                slideWidth: 244,
                slideMargin: 10,
                touchEnabled: false,
                responsive: true,

            });
        });
    });
</script>

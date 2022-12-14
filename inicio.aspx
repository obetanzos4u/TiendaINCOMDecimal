<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeFile="inicio.aspx.cs" MasterPageFile="~/general.master" Inherits="inicio" %>

<%@ Register Src="~/userControls/ui/homeSlider.ascx" TagName="sliderHome" TagPrefix="uc1" %>
<%@ Register Src="~/userControls/categoriasTodas.ascx" TagName="categoriasTodas" TagPrefix="uc_cat" %>
<%-- <%@ Register Src="~/userControls/ui/catalogosSlider.ascx" TagName="sliderCatalogos" TagPrefix="uc1" %>--%>
<%@ Register Src="~/userControls/ui/destacadosSlider.ascx" TagName="sliderDestacados" TagPrefix="uc1" %>
<%--<%@ Register Src="~/userControls/productosVisitadosFullWidth.ascx" TagName="visitados" TagPrefix="productos" %>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <script type="application/ld+json">
        {
        "@context": "http://schema.org",
        "@type": "Organization",
        "url": "https://www.incom.mx",
        "name": "Incom",
        "address": {
        "@type": "PostalAddress",
        "addressLocality": "Iztacalco",
        "addressRegion": "Ciudad de México",
        "postalCode":"08710",
        "streetAddress": "Plutarco Elias Calles 276"
        },
        "contactPoint": {
        "@type": "ContactPoint",
        "contactType" : "customer service",
        "telephone": "+1 55 5243 6900"},
  
        "sameAs" : [ "http://www.facebook.com/incommexico",
        "http://www.twitter.com/incom_mx"
        ]
        }
    </script>
    <div class="main_container is-px-xl">
        <%--<div class="row center-align margin-b-2x"  >
                <h1 class="center-align margin-b-2x">INCOM® La ferretera de las telecomunicaciones®</h1>
            </div>--%>
        <%--<div class="row margin-b-2x" style="border: 1px solid blue; height: 1rem;">
              <productos:visitados ID="ProductosVisitados"  runat="server"></productos:visitados>
            </div>--%>

        <uc1:sliderHome ID="uc_SliderHome" runat="server"></uc1:sliderHome>
        <div class="anuncios-UPS">
            <div class="anuncios-UPS-column">
                <div class="anuncios" id="anunciosSlider" runat="server"></div>
                <div class="USP1" id="sliderUSP1" runat="server"></div>
                <div class="USP2" id="sliderUSP2" runat="server"></div>
                <div class="USP3" id="sliderUSP3" runat="server"></div>
            </div>
        </div>
    </div>

    <section class="title_productos-destacados">
        <p class="is-select-none">Productos destacados</p>
    </section>

    <div class="center-align slider-container">
        <uc1:sliderDestacados ID="uc_SliderDestacados" runat="server"></uc1:sliderDestacados>
    </div>

    <section class="title_categorias">
        <p>Categorías</p>
    </section>

    <div class="categorias_container is-px-xl">
        <uc_cat:categoriasTodas runat="server"></uc_cat:categoriasTodas>
    </div>

    <section class="aviso-version_movil is-m-auto is-font-medium is-w-full is-text-center is-bt-2 is-bg-gray-light is-p-4">
        <p class="is-text-center" style="margin: auto 2rem; font-size: 12px;">Te sugerimos el uso de tu dispositivo en modo horizontal para disfrutar de una experiencia más completa de tu tienda INCOM. </p>
        <img title="Usa tu dispositivo en modo horizontal" alt="" src="/img/webUI/newdesign/screen-rotate.svg" style="height: 52px; width: auto;"/>
    </section>

    <%--<div class="row ">
        <div class="col s12 l6 x6">
            <h2>Acerca de Incom ®</h2>
            <p>
                Somos una empresa orgullosamente mexicana, operando en la industria de las telecomunicaciones, fibra óptica y cobre desde 1999.
            </p>
            <p>
                Cubrimos las necesidades de las empresas en los ramos de
                Telecomunicaciones, Construcción, Integración de TI, Televisión por cable, Radio, Internet, Telefonía, voz y datos
            </p>
            <p>
                <strong>Contacto telefónico</strong>
                <br />
                Para la CDMX y área metropolitana:  <span class="blue-text"> <strong>(55) 5243-6900</strong></span>
                <br />
                Del interior sin costo:  <span class="blue-text"> <strong>800-INCOM(46266)-00</strong></span>
                <br /></p><p>
                <asp:HyperLink ID="link_contacto" ToolTip="Contácto" CssClass="btn blue waves-effect waves-light" href="/informacion/ubicacion-y-sucursales.aspx#contacto"
                    runat="server">Contáctanos vía web</asp:HyperLink>
            </p>
        </div>
        <div class="col s12 l6 x6 center-align">
            <h2>Síguenos en nuestras redes sociales</h2>
            <a href="https://www.facebook.com/incommexico/" target="_blank">
                <img class="responsive-img imagesWebpPng" style="width: 80px;" loading="lazy" alt="Facebook" src="img/webUI/rs/facebook.webp" /></a>
            <a href="https://www.instagram.com/incom_mx/" target="_blank">
                <img class="responsive-img imagesWebpPng" style="width: 80px;" alt="Instagram" loading="lazy" src="img/webUI/rs/instagram.webp" /></a>
            <a href="https://twitter.com/incom_mx" target="_blank">
                <img class="responsive-img imagesWebpPng" style="width: 80px;" alt="Twitter" loading="lazy" src="img/webUI/rs/twitter.webp" />
            </a>
            <a href=" https://www.youtube.com/user/incommx" target="_blank">
                <img class="responsive-img imagesWebpPng" alt="Youtube" style="width: 80px; box-shadow: 0px 0px 2px 0px #868686; border-radius: 19px;" src="img/webUI/rs/youtube.webp" loading="lazy" /></a>
        </div>
    </div>--%>

    <%--<div class="row">
            <uc1:sliderCatalogos runat="server"></uc1:sliderCatalogos>
        </div>--%>
</asp:Content>

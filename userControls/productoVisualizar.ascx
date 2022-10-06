<%@ Control Language="C#" AutoEventWireup="true" CodeFile="productoVisualizar.ascx.cs" Inherits="userControls_productoVisualizar" %>
<%@ Register Src="~/userControls/moneda.ascx" TagName="moneda" TagPrefix="uc_mon" %>
<%@ Register Src="~/userControls/uc_btn_agregar_carrito.ascx" TagName="add" TagPrefix="uc_addCarrito" %>
<%@ Register Src="~/userControls/operaciones/uc_btn_agregar_operacion.ascx" TagName="btn_addOperacion" TagPrefix="uc1" %>
<%@ Register Src="~/userControls/operaciones/uc_modal_agregar_operacion.ascx" TagName="mdl_addOperacion" TagPrefix="uc1" %>
<%@ Register Src="~/userControls/uc_precio_detalles.ascx" TagName="preciosDetalles" TagPrefix="uc1" %>
<%@ Register Src="~/userControls/carga_metaTags.ascx" TagName="metaTagColaborativo" TagPrefix="uc1" %>
<%@ Register Src="~/userControls/productosRelacionados.ascx" TagName="relacionados" TagPrefix="productos" %>
<%@ Register Src="~/userControls/productosAlternativos.ascx" TagName="alternativos" TagPrefix="productos" %>
<%@ Register Src="~/userControls/uc_producto_btn_SoloVisualizar.ascx" TagName="link" TagPrefix="uc_visualizarProducto" %>
<%@ Register Src="~/userControls/productos_stockSAP.ascx" TagName="productoStock" TagPrefix="SAP" %>
<%@ Register Src="~/userControls/uc_share_btn.ascx" TagName="share_btnGen" TagPrefix="uc_share_btn" %>

<div id="content_ProductoNoDisponible" visible="false" runat="server">
    <h1 id="h1ProductoNoDisponible">Producto no disponible </h1>
    <p>El producto no se encuentra <strong>disponible temporalmente</strong>, no tienes los permiso para visualizarlo o este ha sido eliminado</p>
    <p>Intenta más tarde o contáctanos</p>
</div>
<div id="contenedor_producto" class="is-container" runat="server">
    <asp:Literal ID="lt_microdataProducto" runat="server"></asp:Literal>
    <div class="nav-wrapper">
        <div id="navegacion" runat="server" style="height: 42px;" class="container-breadcrumb is-bg-blue-darky">
            <asp:HyperLink ID="link_todas_categorias" CssClass="breadcrumb" runat="server">Productos</asp:HyperLink>
        </div>
    </div>
    <div class="productcard is-px-xl">
        <div class="is-productGallery">
            <div class="is-productGallery_featured container-iframe is-cursor-crosshair" id="selectedImage" runat="server">
                <img id="productGallery_selected" src="../img/webUI/newdesign/loading.svg" alt="Fotografía de producto" style="position: relative; width: 100%" />
                <iframe id="videoProductGallery_selected" class="responsive-iframe is-hidden" src="https://www.youtube.com/embed/" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="true"></iframe>
            </div>
            <ul id="img_producto" class="is-productGallery_thumbnails" runat="server"></ul>
            <section id="productZoom"></section>
            <span class="txt-gallery">Posiciona el cursor sobre la imagen para obtener una vista ampliada.</span>
        </div>

        <div class="container-descripcion is-relative">
            <div class="is-flex">
                <div class="wrapper-descripcion">
                    <h1 class="title-product_description is-family-ms is-m-0" style="margin-bottom: 2rem;">
                        <asp:Literal ID="lt_numero_parte" Visible="false" runat="server"></asp:Literal>
                        <asp:Literal ID="lt_titulo" runat="server"></asp:Literal>
                    </h1>
                    <div class="is-flex is-justify-start is-items-center is-font-semibold is-m-0">
                        <p class="is-m-0">Número de parte:</p>
                        <strong class="is-select-all">
                            <asp:Label ID="lbl_numero_parte" Style="margin: 0px; display: inline; padding: 2px 16px;" runat="server" Text=""></asp:Label>
                        </strong>
                        <button type="button" class="is-cursor-pointer" style="background-color: transparent; outline: none; border: none" onclick="copiarNumeroParte()">
                            <span class="is-text-gray">
                                <svg class="is-w-4 is-h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 5H6a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2v-1M8 5a2 2 0 002 2h2a2 2 0 002-2M8 5a2 2 0 012-2h2a2 2 0 012 2m0 0h2a2 2 0 012 2v3m2 4H10m0 0l3-3m-3 3l3 3"></path>
                                </svg>
                            </span>
                        </button>
                    </div>
                    <div class="is-flex is-justify-start is-items-center is-font-semibold is-m-0">
                        <p class="is-m-0">Marca:</p>
                        <strong class="is-select-all">
                            <asp:Label ID="lbl_marca" Style="margin: 0px; display: inline; padding: 2px 16px;" runat="server" Text=""></asp:Label>
                        </strong>
                    </div>
                    <div class="is-flex is-justify-start is-items-center is-font-semibold is-m-0">
                        <p class="is-m-0">Unidad de venta: </p>
                        <strong>
                            <asp:Literal ID="lt_unidad_venta" runat="server"></asp:Literal>
                        </strong>
                        (<asp:Literal ID="lt_cantidad" runat="server"></asp:Literal>
                        <asp:Literal ID="lt_unidad" runat="server"></asp:Literal>)
                    </div>
                    <asp:Label ID="lbl_descripcion_corta" CssClass="is-my-4" runat="server" Text="descripcion_corta"></asp:Label>
                    <uc1:metaTagColaborativo ID="metaTagColaborativo" runat="server"></uc1:metaTagColaborativo>
                    <SAP:productoStock ID="sap_producto_disponibilidad" Visible="true" runat="server"></SAP:productoStock>
                    <uc_visualizarProducto:link ID="linkVisualizarProducto" Visible="false" runat="server"></uc_visualizarProducto:link>
                </div>
                <div class="ticket_compra">
                    <div class="borde-ticket_compra">
                        <div class="">
                            <div class="col s12 m5 l6 xl5" style="width: 100%; display: contents;">
                                <section class="price">
                                    <asp:Label ID="lbl_preciosFantasma" Style="text-decoration: line-through; color: red; font-size: 1.5rem; width: 100%; display: inherit;" Visible="false" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_precioLista" Style="display: block; font-size: 1.2rem; text-decoration: line-through;" Visible="false" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_precioGeneral" Style="display: block; font-size: 1.2rem; text-decoration: line-through;" Visible="false" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_precioGeneralLeyenda" Style="display: block; font-weight: 700; color: #0fb30f;" Visible="false" Text="Tu Precio especial ✓" runat="server"> </asp:Label>
                                    <span class="divisa-txt">$</span>
                                    <asp:Label ID="lbl_precio" class="precio-producto" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_moneda" class="moneda-txt" runat="server"></asp:Label>
                                    <br />
                                    <asp:Label ID="lbl_descuento_porcentaje_fantasma" Visible="false"
                                        class="red white-text" Style="padding: 2px 5px" runat="server"></asp:Label>
                                </section>
                                <section class="text-IVA">
                                    <span class="is-text-black nota">IVA <strong>incluido</strong> </span>
                                </section>
                                <br />
                                <uc1:preciosDetalles ID="detalles_precios" runat="server"></uc1:preciosDetalles>
                            </div>
                            <div class="row" style="margin: inherit 0px;">
                                <div class="s12 m12 l12 xl12">
                                    <asp:Label ID="lbl_msg_maximo_compra" Visible="false" runat="server"></asp:Label>
                                    <section class="wrapper-moneda">
                                        <div class="input-field moneda-input" style="z-index: 0 !important;">
                                            Moneda:
                                        <uc_mon:moneda ID="uc_moneda" runat="server"></uc_mon:moneda>
                                        </div>
                                    </section>
                                    <section class="wrapper-cantidad">
                                        <uc_addCarrito:add ID="AddCarrito" runat="server"></uc_addCarrito:add>
                                    </section>
                                </div>
                            </div>
                            <uc1:btn_addOperacion ID="productoAddOperacion" runat="server"></uc1:btn_addOperacion>
                        </div>
                    </div>
                    <uc_share_btn:share_btnGen ID="share_btn" runat="server"></uc_share_btn:share_btnGen>
                </div>
            </div>
            <div id="cont_documentacion" runat="server" visible="false">
                <h2>Documentación</h2>
            </div>
        </div>
    </div>
</div>

<div class="is-px-xl">
    <div class="tabs">

            <input type="radio" id="tab1" name="tab-control" checked>
            <input type="radio" id="tab2" name="tab-control">
            <input type="radio" id="tab3" name="tab-control">  
            <ul>
            <li title="Características"><label for="tab1" role="button"><span>Características</span></label></li>
            <li title="Especificaciones"><label for="tab2" role="button"><span>Especificaciones</span></label></li>
            <li title="Consideraciones"><label for="tab3" role="button"><span>Consideraciones</span></label></li>    
            </ul>
            
            <div class="slider-tab"><div class="indicator-tab"></div></div>
            <div class="content-tab">
            <section>
                <div id="content_caracteristicas">
                    <p class="item-caracteristicas">
                        <asp:Label ID="lbl_especificaciones" class="caracteristicas-list" runat="server"></asp:Label>
                    </p>
                </div>
            </section>
            <section>
                <!-- caracteristicas -->
                <div class="wrapper-especificaciones">
                    <div class="left-especificaciones">
                        <span class="detalles-producto"><strong>Detalles del producto</strong></span>
                        <br>
                        <table class="striped striped-tb" style="width: 100%;">
                            <tbody id="tbody_caracteristicas" runat="server">
                            </tbody>
                        </table>
                    </div>
                    <div class="right-especificaciones">
                        <span class="medidas-empaque"><strong>Medidas del empaque</strong></span>
                        <br>
                        <table class="striped striped-tb" style="width: 100%;">
                            <tbody id="tbody_dimensiones_empaque" runat="server">
                            </tbody>
                        </table>
                    </div>
                </div>
                <p class="bottom-especificaciones">
                    Si requiere información detallada consulte la ficha técnica o solicite más información acerca del producto dando 
                            <a href="/informacion/ubicacion-y-sucursales.aspx?info=Info. técnica y/o adicional: Referencia del producto: <%= lbl_numero_parte.Text %>">clic aquí</a>
                </p>
            </section>
            <section>
                <div id="content_avisos">
                    <ul id="ProductoAvisosListado" class="content_avisos-list" style="padding-left: 5%; !important; display: inline-block;" runat="server">
                    </ul>
                </div>
            </section>
        </div>
    </div>

    <div class="s12 m12 l12 xl12">
        <productos:relacionados ID="productosRelacionados" runat="server"></productos:relacionados>
        <productos:alternativos ID="productosAlternativos" runat="server"></productos:alternativos>
    </div>
    <!-- <div class="col s12 m12 l12 xl8">
        <h2>Video</h2>
        <p id="cont_videos" runat="server">
        </p>
    </div> -->
</div>

<script>
    const thumbs = document.querySelectorAll(".is-productGallery_thumb");
    let selectedImg = document.getElementById("productGallery_selected");
    let selectedVid = document.getElementById("videoProductGallery_selected");
    const drift = new Drift(selectedImg, {
        paneContainer: document.querySelector("#productZoom"),
        inlinePane: false,
        zoomFactor: 2.5,
        hoverDelay: 0
    });
    for (let i = 0; i < thumbs.length; i++) {
        thumbs[i].addEventListener("mouseenter", () => {
            let firstChild = thumbs[i].firstChild;
            if (thumbs[i].id === "vid_productoContainer") {
                let src = firstChild.getAttribute("data-video");
                selectedImg.classList.add("is-hidden");
                selectedVid.classList.remove("is-hidden");
                document.getElementById("videoProductGallery_selected").src += src;
            } else {
                let src = firstChild.getAttribute("src");
                selectedVid.classList.add("is-hidden");
                selectedImg.classList.remove("is-hidden");
                document.getElementById("productGallery_selected").src = src;
                drift.setZoomImageURL(src);
                selectedImg.setAttribute("data-zoom", src);
            }
        });
    }
    document.addEventListener("DOMContentLoaded", () => {
        let firstSrc = thumbs[0].firstChild.getAttribute("src");
        document.getElementById("productGallery_selected").src = firstSrc;
        selectedImg.setAttribute("data-zoom", firstSrc);
    });
    const copiarNumeroParte = () => {
        const numeroParte = document.getElementById("top_contenido_ctl00_lbl_numero_parte").innerText;
        navigator.clipboard.writeText(numeroParte);
    }
</script>

<uc1:mdl_addOperacion ID="mdl_addOperacion" runat="server"></uc1:mdl_addOperacion>

<style>
    @import "https://fonts.googleapis.com/css?family=Montserrat:400,700|Raleway:300,400";

    .content-tab {
        justify-content: center;
        display: flex;
    }

    .tabs {
        position: relative;
        background: white;
        padding: 50px;
        padding-bottom: 80px;
        width: 100%;
        height: auto;
        margin: 4rem auto;
        border-radius: 0px 0px 8px 8px;
        min-width: 180px;
        box-shadow: 0 3px 1px 2px #c7c7c73a;
        border-top: 1px solid rgba(0, 0, 0, 0.16);
    }

        .tabs input[name=tab-control] {
            display: none;
        }

        .tabs .content-tab section h2,
        .tabs ul li label {
            font-family: "Montserrat";
            font-weight: bold;
            font-size: 15px;
            color: #01568D;
        }

        .tabs ul {
            list-style-type: none;
            padding-left: 0;
            display: flex;
            flex-direction: row;
            margin-bottom: 10px;
            justify-content: space-between;
            align-items: flex-end;
            flex-wrap: wrap;
        }

            .tabs ul li {
                box-sizing: border-box;
                flex: 1;
                width: 33.3333333333%;
                padding: 0 10px;
                text-align: center;
            }

                .tabs ul li label {
                    transition: all 0.3s ease-in-out;
                    color: #929daf;
                    padding: 5px auto;
                    overflow: hidden;
                    text-overflow: ellipsis;
                    display: block;
                    cursor: pointer;
                    transition: all 0.2s ease-in-out;
                    white-space: nowrap;
                    -webkit-touch-callout: none;
                    -webkit-user-select: none;
                    -moz-user-select: none;
                    -ms-user-select: none;
                    user-select: none;
                }

                    .tabs ul li label br {
                        display: none;
                    }

                    .tabs ul li label svg {
                        fill: #929daf;
                        height: 1.2em;
                        vertical-align: bottom;
                        margin-right: 0.2em;
                        transition: all 0.2s ease-in-out;
                    }

                    .tabs ul li label:hover, .tabs ul li label:focus, .tabs ul li label:active {
                        outline: 0;
                        color: #bec5cf;
                    }

                        .tabs ul li label:hover svg, .tabs ul li label:focus svg, .tabs ul li label:active svg {
                            fill: #bec5cf;
                        }

        .tabs .slider-tab {
            position: relative;
            width: 33.3333333333%;
            transition: all 0.33s cubic-bezier(0.38, 0.8, 0.32, 1.07);
        }

            .tabs .slider-tab .indicator-tab {
                position: relative;
                width: 100%;
                max-width: 100%;
                margin: 0 auto;
                height: 3px;
                background: #01568D;
                border-radius: 1px;
            }

        .tabs .content-tab {
            margin-top: 30px;
        }

            .tabs .content-tab section {
                display: none;
                -webkit-animation-name: content-tab;
                animation-name: content-tab;
                -webkit-animation-direction: normal;
                animation-direction: normal;
                -webkit-animation-duration: 0.3s;
                animation-duration: 0.3s;
                -webkit-animation-timing-function: ease-in-out;
                animation-timing-function: ease-in-out;
                -webkit-animation-iteration-count: 1;
                animation-iteration-count: 1;
                line-height: 1;
            }

                .tabs .content-tab section h2 {
                    color: #01568D;
                    display: none;
                }

                    .tabs .content-tab section h2::after {
                        content: "";
                        position: relative;
                        display: block;
                        width: 30px;
                        height: 3px;
                        background: #01568D;
                        margin-top: 5px;
                        left: 1px;
                    }

        .tabs input[name=tab-control]:nth-of-type(1):checked ~ ul > li:nth-child(1) > label {
            cursor: default;
            color: #01568D;
        }

            .tabs input[name=tab-control]:nth-of-type(1):checked ~ ul > li:nth-child(1) > label svg {
                fill: #01568D;
            }

    .wrapper-especificaciones {
        display: flex;
        justify-content: center;
    }

    .right-especificaciones {
        display: inline-block;
        width: 45%;
        margin-left: 5%;
    }

    .txt-gallery {
        font-size: 12px;
        margin-bottom: 2rem;
    }

    .bottom-especificaciones {
        margin-top: 2em;
    }

    .detalles-producto {
        color: #01568D;
    }

    .medidas-empaque {
        color: #01568D;
    }

    .striped-tb {
        margin-top: 1rem;
    }

    .content_avisos-list li {
        list-style-type: square !important;
        line-height: 2rem;
    }

    .caracteristicas-list li {
        list-style-type: square !important;
    }

    #content_caracteristicas ul li {
        line-height: 2rem;
    }

    #content_caracteristicas > p {
        white-space: normal !important;
        line-height: normal;
    }

    #share-facebook > svg:nth-child(1) {
        width: 16px;
        height: 16px;
    }

    #share-twitter > svg:nth-child(1) {
        width: 16px;
        height: 16px;
    }

    #share-whatsapp > svg:nth-child(1) {
        width: 16px;
        height: 16px;
    }

    #share-mail > svg:nth-child(1) {
        width: 16px;
        height: 16px;
    }

    @media (max-width: 450px) {
        .tabs input[name=tab-control]:nth-of-type(1):checked ~ ul > li:nth-child(1) > label {
            background: rgba(0, 0, 0, 0.08);
        }
    }

    .tabs input[name=tab-control]:nth-of-type(1):checked ~ .slider-tab {
        transform: translateX(0%);
    }

    .tabs input[name=tab-control]:nth-of-type(1):checked ~ .content-tab > section:nth-child(1) {
        display: block;
    }

    .tabs input[name=tab-control]:nth-of-type(2):checked ~ ul > li:nth-child(2) > label {
        cursor: default;
        color: #01568D;
    }

        .tabs input[name=tab-control]:nth-of-type(2):checked ~ ul > li:nth-child(2) > label svg {
            fill: #01568D;
        }


    @media (max-width: 450px) {
        .tabs input[name=tab-control]:nth-of-type(2):checked ~ ul > li:nth-child(2) > label {
            background: rgba(0, 0, 0, 0.08);
        }
    }

    .tabs input[name=tab-control]:nth-of-type(2):checked ~ .slider-tab {
        transform: translateX(100%);
    }

    .tabs input[name=tab-control]:nth-of-type(2):checked ~ .content-tab > section:nth-child(2) {
        display: block;
    }

    .tabs input[name=tab-control]:nth-of-type(3):checked ~ ul > li:nth-child(3) > label {
        cursor: default;
        color: #01568D;
    }

        .tabs input[name=tab-control]:nth-of-type(3):checked ~ ul > li:nth-child(3) > label svg {
            fill: #01568D;
        }

    @media (max-width: 450px) {
        .tabs input[name=tab-control]:nth-of-type(3):checked ~ ul > li:nth-child(3) > label {
            background: rgba(0, 0, 0, 0.08);
        }
    }

    .tabs input[name=tab-control]:nth-of-type(3):checked ~ .slider-tab {
        transform: translateX(200%);
    }

    .tabs input[name=tab-control]:nth-of-type(3):checked ~ .content-tab > section:nth-child(3) {
        display: block;
    }

    @-webkit-keyframes content-tab {
        from {
            opacity: 0;
            transform: translateY(5%);
        }

        to {
            opacity: 1;
            transform: translateY(0%);
        }
    }

    @keyframes content-tab {
        from {
            opacity: 0;
            transform: translateY(5%);
        }

        to {
            opacity: 1;
            transform: translateY(0%);
        }
    }

    @media (max-width: 750px) {
        .tabs ul li label {
            white-space: initial;
        }

            .tabs ul li label br {
                display: initial;
            }

            .tabs ul li label svg {
                height: 1.5em;
            }
    }

    @media (max-width: 450px) {
        .tabs ul li label {
            padding: 5px;
            border-radius: 5px;
        }

            .tabs ul li label span {
                display: none;
            }

        .tabs .slider-tab {
            display: none;
        }

        .tabs .content-tab {
            margin-top: 20px;
        }

            .tabs .content-tab section h2 {
                display: block;
            }
    }

    @media (min-width: 1200px) and (max-width: 1600px) {
        
        .tabs {
            width: auto;
            margin-left: 2rem;
            margin-right: 2rem;
        }
    }

    @media (max-width: 1200px) {

        .tabs {
            width: auto;
            margin-left: 0.5rem;
            margin-right: 0.5rem;
            padding: 20px
        }

        .tabs .content-tab section h2,
        .tabs ul li label {
            font-size: 12px;
        } 
    }

    @media only screen and (max-width: 992px) {
        .tabs {
            display: block !important;
            }
    }

    @media (min-width: 350px) {
    
        .content-tab {
            font-size: 8px;
        }
    }

    @media (min-width: 700px) {
    
        .content-tab {
            font-size: 12px;
        }
    }

    @media (min-width: 1600px) {

        .content-tab {
            font-size: 16px;
        }
    }
</style>
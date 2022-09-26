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
        <div id="navegacion" runat="server" style="height: 42px; padding-top: 0.25rem;" class="container-breadcrumb is-bg-blue-darky">
            <asp:HyperLink ID="link_todas_categorias" CssClass="breadcrumb" runat="server">Productos</asp:HyperLink>
        </div>
    </div>
    <div class="is-w-full is-flex is-px-xl">
        <div class="is-productGallery">
            <div class="is-productGallery_featured container-iframe" id="selectedImage" runat="server">
                <img id="productGallery_selected" src="../img/webUI/newdesign/loading.svg" alt="Fotografía de producto" style="position: relative; width: 100%" />
                <iframe id="videoProductGallery_selected" class="responsive-iframe is-hidden" src="https://www.youtube.com/embed/" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen="true"></iframe>
            </div>
            <ul id="img_producto" class="is-productGallery_thumbnails" runat="server"></ul>
        </div>
        <div class="is-relative" style="width: 60%;">
            <div class="is-flex">
                <div class="">
                    <div id="productZoom"></div>
                    <h1 class="title-product_description is-family-ms is-m-0">
                        <asp:Literal ID="lt_numero_parte" Visible="false" runat="server"></asp:Literal>
                        <asp:Literal ID="lt_titulo" runat="server"></asp:Literal>
                    </h1>
                    <span class="is-block">Número de parte:  <strong>
                        <asp:Label ID="lbl_numero_parte" Style="margin: 0px; display: inline; padding: 2px 16px;" runat="server" Text=""></asp:Label></strong>
                    </span>
                    <span class="is-block">Marca <strong>
                        <asp:Label ID="lbl_marca" Style="margin: 0px; display: inline; padding: 2px 16px;" runat="server" Text=""></asp:Label></strong>
                    </span>
                    <strong>Unidad de venta: </strong>
                    <asp:Literal ID="lt_unidad_venta" runat="server"></asp:Literal>
                    &nbsp;
                    (<asp:Literal ID="lt_cantidad" runat="server"></asp:Literal>
                    &nbsp;
                    <asp:Literal ID="lt_unidad" runat="server"></asp:Literal>)
                    <p>
                        <asp:Label ID="lbl_descripcion_corta" runat="server" Text="descripcion_corta"></asp:Label>
                        <uc1:metaTagColaborativo ID="metaTagColaborativo" runat="server"></uc1:metaTagColaborativo>
                    </p>
                    <SAP:productoStock ID="sap_producto_disponibilidad" Visible="true" runat="server"></SAP:productoStock>
                    <uc_visualizarProducto:link ID="linkVisualizarProducto" Visible="false" runat="server"></uc_visualizarProducto:link>
                </div>
                <div>
                    <div class="">
                        <div class="col s12 m5 l6 xl5">
                            <asp:Label ID="lbl_preciosFantasma" Style="text-decoration: line-through; color: red; font-size: 1.5rem;" Visible="false" runat="server"></asp:Label>
                            <br />
                            <asp:Label ID="lbl_precioLista" Style="display: block; font-size: 1.2rem; text-decoration: line-through;" Visible="false" runat="server"></asp:Label>
                            <asp:Label ID="lbl_precioGeneral" Style="display: block; font-size: 1.2rem; text-decoration: line-through;" Visible="false" runat="server"></asp:Label>
                            <asp:Label ID="lbl_precioGeneralLeyenda" Style="display: block; font-weight: 700; color: #0fb30f;" Visible="false" Text="Tu Precio especial ✓" runat="server"> </asp:Label>
                            <span style="font-size: 2rem; color: #287aee;">$</span>
                            <asp:Label ID="lbl_precio" Style="font-size: 2rem; color: #287aee;" runat="server"></asp:Label>

                            <asp:Label ID="lbl_moneda" Style="font-size: 2rem; color: #287aee;" runat="server"></asp:Label>
                            <br />
                            <asp:Label ID="lbl_descuento_porcentaje_fantasma" Visible="false"
                                class="red white-text" Style="padding: 2px 5px" runat="server"></asp:Label>
                            <br />


                            <span class=" white-text green darken-1  nota">Incluye <strong>IVA ✓</strong> </span>
                            <br />

                            <uc1:preciosDetalles ID="detalles_precios" runat="server"></uc1:preciosDetalles>
                        </div>
                        <div class="row" style="margin: inherit 0px;">
                            <div class="s12 m12 l12 xl12">
                                <asp:Label ID="lbl_msg_maximo_compra" Visible="false" runat="server"></asp:Label>
                                <div class="input-field  fixInput" style="z-index: 0 !important;">
                                    Moneda:
                                <uc_mon:moneda ID="uc_moneda" runat="server"></uc_mon:moneda>
                                </div>
                                <uc_addCarrito:add ID="AddCarrito" runat="server"></uc_addCarrito:add>
                            </div>
                        </div>
                        <uc1:btn_addOperacion ID="productoAddOperacion" runat="server"></uc1:btn_addOperacion>
                    </div>
                    <div class="addthis_inline_share_toolbox is-test"></div>
                    <uc_share_btn:share_btnGen ID="share_btn" runat="server"></uc_share_btn:share_btnGen>
                </div>
            </div>
            <div class="">
                <h2>Documentación</h2>
                <p id="cont_documentacion" runat="server">
                </p>
            </div>
        </div>
    </div>
</div>

<div class="is-px-xl">
    <div id="content_caracteristicas">
        <h2>Características</h2>
        <table class="striped" style="width: 100%;">
            <tbody id="tbody_caracteristicas" runat="server">
            </tbody>
        </table>
        <p>
            <asp:Label ID="lbl_especificaciones" runat="server"></asp:Label>
        </p>
    </div>

    <div id="content_avisos" class="s12 m12 l12 xl12">
        <h2>Avisos:</h2>
        <ul id="ProductoAvisosListado" class="incom-ul-default" style="padding-left: 1rem !important;" runat="server">
        </ul>
    </div>
    <div class="s12 m12 l12 xl12">
        <!-- caracteristicas -->
        <h2>Especificaciones</h2>
        <p>
            Si requiere información detallada consulte la ficha técnica o solicite más información acerca del producto dando 
                        <a href="/informacion/ubicacion-y-sucursales.aspx?info=Info. técnica y/o adicional: Referencia del producto: <%= lbl_numero_parte.Text %>">clic aquí</a>
        </p>
        <%--<span class="green-text"><strong>Detalles del producto</strong></span>--%>

        <span class="green-text"><strong>Las siguientes medidas son de empaque</strong></span>
        <table class="striped" style="width: 100%;">
            <tbody id="tbody_dimensiones_empaque" runat="server">
            </tbody>
        </table>

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
    let drift = new Drift(selectedImg, {
        paneContainer: document.querySelector("#productZoom"),
        inlinePane: false,
        zoomFactor: 2,
        hoverDelay: 0
    });
    console.log(drift);
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
</script>

<uc1:mdl_addOperacion ID="mdl_addOperacion" runat="server"></uc1:mdl_addOperacion>

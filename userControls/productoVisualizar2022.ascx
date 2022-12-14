<%@ Control Language="C#" AutoEventWireup="true"  CodeFile="productoVisualizar2022.ascx.cs" Inherits="userControls_productoVisualizar" %>
<%@ Register src="~/userControls/moneda.ascx" TagName="moneda" TagPrefix="uc_mon" %>
<%@ Register Src="~/userControls/uc_btn_agregar_carrito.ascx" TagName="add" TagPrefix="uc_addCarrito" %>
<%@ Register Src="~/userControls/operaciones/uc_btn_agregar_operacion.ascx" TagName="btn_addOperacion" TagPrefix="uc1" %>
<%@ Register Src="~/userControls/operaciones/uc_modal_agregar_operacion.ascx" TagName="mdl_addOperacion" TagPrefix="uc1" %>
<%@ Register Src="~/userControls/uc_precio_detalles.ascx" TagName="preciosDetalles" TagPrefix="uc1" %>
<%@ Register Src="~/userControls/carga_metaTags.ascx" TagName="metaTagColaborativo" TagPrefix="uc1" %>
<%@ Register Src="~/userControls/productosRelacionados.ascx" TagName="relacionados" TagPrefix="productos" %>
<%@ Register Src="~/userControls/productos_stock.ascx" TagName="stock" TagPrefix="productos" %>

<%@ Register Src="~/userControls/uc_producto_btn_SoloVisualizar.ascx" TagName="link" TagPrefix="uc_visualizarProducto" %>

<h1 id="h1ProductoNoDisponible" visible="false" runat="server">Producto no disponible para tu usuario</h1>

<div id="contenedor_producto" runat="server">
    <asp:Literal ID="lt_microdataProducto" runat="server"></asp:Literal>
    <div>
        <div class="nav-wrapper">
            <div id="navegacion" runat="server" style="height: 42px; margin: 3em 0em; padding-top: 0.25rem;" class="col s12 m12 l12 xl12 is-bg-blue-darky">
                <asp:HyperLink ID="link_todas_categorias" CssClass="breadcrumb" runat="server">Productos</asp:HyperLink>
            </div>
        </div>
    </div>
    <div class="row margin-t-6x">
        <div class="col s12 m6 l5 xl3">
            <div class="sp-wrap" id="img_producto" runat="server"></div>
            <div class="addthis_inline_share_toolbox is-test"></div>

          
        </div>
        <div class="col s12 m12 l7 xl9">
            <h1>
                <asp:Literal ID="lt_numero_parte" Visible="false" runat="server"></asp:Literal>
                <asp:Literal ID="lt_titulo" runat="server"></asp:Literal>
            </h1>
            <p>
                <asp:Label ID="lbl_descripcion_corta" runat="server" Text="descripcion_corta"></asp:Label>
                   <uc1:metaTagColaborativo ID="metaTagColaborativo" runat="server"></uc1:metaTagColaborativo>
            </p>
            <div class="row">
                <span>Marca <strong>
                    <asp:Label ID="lbl_marca" Style="margin: 0px; background: #eeeeee; display: inline; padding: 2px 16px;"
                        runat="server" Text=""></asp:Label></strong></span> 
                    <b>Número de parte:  </b><strong>
                    <asp:Label ID="lbl_numero_parte" Style="margin: 0px; background: #eeeeee; display: inline; padding: 2px 16px;" runat="server" Text=""></asp:Label></strong>
            </div>
            <div class="row">
                 <div class="input-field  fixInput ">
                    Moneda:
        <uc_mon:moneda ID="uc_moneda" runat="server"></uc_mon:moneda>

                </div>
                <div class="col s12 m5 l6 xl5">
                    <asp:Label ID="lbl_preciosFantasma" style="text-decoration:line-through;color: red; font-size: 1.5rem;" Visible="false"  runat="server"></asp:Label>
                      <br />
                    <asp:Label ID="lbl_precioLista" Style="display: block; font-size: 1.2rem; text-decoration: line-through;" Visible="false" runat="server"></asp:Label>
                    <asp:Label ID="lbl_precioGeneral" Style="display: block; font-size: 1.2rem; text-decoration: line-through;" Visible="false" runat="server"></asp:Label>
                    <asp:Label ID="lbl_precioGeneralLeyenda" Style="display: block; font-weight: 700; color: #0fb30f;" Visible="false" Text="Tu Precio especial ✓" runat="server"> </asp:Label>
                    <span style="font-size: 2rem; color: #287aee;">$</span>
                    <asp:Label ID="lbl_precio" Style="font-size: 2rem; color: #287aee;" runat="server"></asp:Label>

                    <asp:Label ID="lbl_moneda" Style="font-size: 2rem; color: #287aee;" runat="server"></asp:Label>
                      <br />
                              <asp:Label ID="lbl_descuento_porcentaje_fantasma"   Visible="false"
                                        class="red white-text" style="padding: 2px 5px" runat="server" ></asp:Label>
                    <br />


                    <span class=" blue-grey lighten-5 nota">Impuestos <strong>No incluidos</strong> </span>
                    <br />

                    <strong>Unidad de venta: </strong>
                    <asp:Literal ID="lt_unidad_venta" runat="server"></asp:Literal> &nbsp;
                (<asp:Literal ID="lt_cantidad" runat="server"></asp:Literal> &nbsp;
                <asp:Literal ID="lt_unidad" runat="server"></asp:Literal>)
            
                     <uc1:preciosDetalles ID="detalles_precios" runat="server"></uc1:preciosDetalles>
                </div>
                              

            </div>

            <div class="row" style="margin: inherit 0px;">
                <div class="col s12 m12 l12 xl12">
                    <p>
                        Antes de realizar su <span style="color: red;">compra</span> por favor  <strong>consultar la disponibilidad </strong>a los teléfonos:
                 <br />
                        Para la CDMX y área metropolitana: (55) 5243-6900

                  <br />
                        Del interior sin costo: 800-INCOM(46266)-00
                <br />
                        O llenando el siguiente formulario: <a href="https://www.incom.mx/informacion/ubicacion-y-sucursales.aspx#contacto">Click Aquí</a>
                    </p>
                    <uc_addCarrito:add ID="AddCarrito" runat="server"></uc_addCarrito:add>
                    <uc_visualizarProducto:link ID="linkVisualizarProducto" Visible="false" runat="server"></uc_visualizarProducto:link>

                </div>

            </div>

            <div class="row">
                <div class="col s12 m12 l12 xl12">


                    <productos:stock ID="stock_producto" Visible="false" runat="server"></productos:stock>
                </div>
            </div>
            <div class="row">
                <div class="col s12 m12 l6 xl4">
                    <uc1:btn_addOperacion ID="productoAddOperacion" runat="server"></uc1:btn_addOperacion>
                </div>
            </div>
            <div class="row">



                <div id="content_caracteristicas" class="col s12 m12 l12 xl12">
                    <h2>Características</h2>

                    <p>
                        <asp:Label ID="lbl_especificaciones" runat="server"></asp:Label>
                    </p>
                </div>
                <div id="content_avisos" class="col s12 m12 l12 xl12">
                    <h2>Avisos:</h2>
                    <ul id="ProductoAvisosListado" class="incom-ul-default" runat="server">
                       
                    </ul>

                </div>
                <div class="col s12 m12 l12 xl12">
                    <!-- caracteristicas -->
                    <h2></h2>
                    <p>
                      Si requiere información detallada consulte la ficha técnica o solicite más información acerca del producto dando 
                        <a href="/informacion/ubicacion-y-sucursales.aspx?info=Info. técnica y/o adicional: Referencia del producto: <%= lbl_numero_parte.Text %>">clic aquí</a>
                    </p>
                     <span class="green-text"><strong>Detalles del producto</strong></span>
                    <table class="striped" style="width: 100%;">
                        <tbody id="tbody_caracteristicas" runat="server">
                        </tbody>
                    </table>
                     <span class="green-text"><strong>Las siguientes medidas son de empaque</strong></span>
                      <table class="striped" style="width: 100%;">
                        <tbody id="tbody_dimensiones_empaque" runat="server">
                        </tbody>
                    </table>

                </div>
                <div class="col s12 m12 l12 xl8">
                    <h2>Documentación</h2>
                    <p id="cont_documentacion" runat="server">
                    </p>
                </div>
                <!-- <div class="col s12 m12 l12 xl8">
                    <h2>Video</h2>
                    <p id="cont_videos" runat="server">
                    </p>
                </div> -->
            </div>
        </div>
       <div class="col s12 m12 l7 xl9">
             <productos:relacionados ID="productosRelacionados"  runat="server"></productos:relacionados>
       </div>
    </div>

</div>
<script>
    document.addEventListener("DOMContentLoaded", function (event) {
        $('.sp-wrap').smoothproducts();

       

    });
</script>
 

<uc1:mdl_addOperacion ID="mdl_addOperacion" runat="server"></uc1:mdl_addOperacion>
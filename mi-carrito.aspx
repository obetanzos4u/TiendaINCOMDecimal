<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeFile="mi-carrito.aspx.cs" MasterPageFile="~/general.master" Inherits="mi_carrito" %>

<%@ Register Src="~/userControls/moneda.ascx" TagName="moneda" TagPrefix="uc_mon" %>
<%@ Register Src="~/userControls/uc_precio_detalles.ascx" TagName="preciosDetalles" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <asp:HiddenField ID="hf_UserLogin" runat="server" />
    <div id="pantallaCarga" visible="false" runat="server">
        <div class="is-flex is-w-full is-h-full">
            <img class="is-m-auto" src="/img/webUI/newdesign/Icono_de_carga.svg" alt="Cargando la página" style="width: 140px;" />
        </div>
    </div>
    <asp:UpdatePanel ID="up_carrito" UpdateMode="Conditional" class="is-container" Visible="false" runat="server">
        <ContentTemplate>
            <div class="mi-carrito-contain">
                <div class="col l8 xl8 mi-carrito-list">
                    <div class="head-carrito_list">
                        <asp:Label ID="lbl_shoppingCartTitle" class="title-product_list is-w-full is-font-semibold is-select-none" runat="server"></asp:Label>
                        <%--<asp:LinkButton ID="btn_guardarPlantilla" data-tooltip="Guarda este listado de productos para cotizaciones" OnClick="btn_guardarPlantilla_Click" CssClass="tooltipped" runat="server">
                            <i class="material-icons">save</i>
                        </asp:LinkButton>--%>
                    </div>
                    <div class="is-w-full wrapp-mi_carrito">
                        <asp:ListView ID="lv_productosCarritos" OnItemDataBound="lv_productos_OnItemDataBound" runat="server">
                            <LayoutTemplate>
                                <!-- <div class="is-w-full overflow-productos" style="overflow-y: auto;"> -->
                                <div class="is-w-full overflow-productos" style="overflow-y: auto;">
                                    <table class="product-table">
                                        <tbody>
                                            <div runat="server" id="itemPlaceholder"></div>
                                        </tbody>
                                    </table>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr class="table-product_description">
                                    <td>
                                        <asp:Image ID="imgProducto" CssClass="responsive-img" runat="server" />
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hf_idProductoCarrito" Value='<%#Eval("id") %>' runat="server" />
                                        <div class="is-select-all">
                                            <asp:Literal ID="lt_numeroParte" Text='<%#Eval("numero_parte") %>' runat="server"></asp:Literal>
                                            <span>-</span>
                                            <asp:Literal ID="lt_titulo" Text='<%#Eval("titulo") %>' runat="server"></asp:Literal>
                                        </div>
                                        <div class="is-py-4">
                                            <%#Eval("descripcion") %>
                                        </div>
                                        <div id="warning_envios_medidas" class="is-text-xs is-text-red" runat="server" visible="false"></div>
                                    </td>
                                    <td class="product-list-precio_cantidad">
                                        <div class="is-flex is-justify-between pricenet">
                                            <p>Precio sin IVA: </p>
                                            <asp:Label ID="lbl_precio_sin_impuesto" runat="server"></asp:Label>
                                        </div>
                                        <div class="is-flex is-justify-between priceiva">
                                            <p>Precio con IVA: </p>
                                            <asp:Label ID="lbl_precio_unitario" runat="server"></asp:Label>
                                        </div>
                                        <div class="is-flex is-justify-between countproducts">
                                            <p>Cantidad: </p>
                                            <asp:UpdatePanel UpdateMode="Always" runat="server">
                                                <ContentTemplate>
                                                    <div class="btn-products_counter">
                                                        <%--<input id="txt_cantidadCarrito" type="number" min="1" max="12" onchange="txtLoading(this);" AutoPostBack="true" value='<%#Eval("cantidad") %>' runat="server" />--%>
                                                        <asp:TextBox ID="txt_cantidadCarrito" class="products_counter" TextMode="Number" Type="Integer" AutoPostBack="true" Text='<%#Eval("cantidad") %>' OnTextChanged="txt_cantidadCarrito_TextChanged" Style="border-radius: 10px; text-align: center" runat="server"></asp:TextBox>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txt_cantidadCarrito" EventName="TextChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:Label ID="lbl_precio_total" class="text-subtotal_producto" runat="server"></asp:Label>
                                        </div>
                                        <uc1:preciosDetalles ID="detalles_precios" runat="server"></uc1:preciosDetalles>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" class="options-product_list">
                                        <div class="is-flex is-justify-between is-items-center">
                                            <div class="mi-carrito-product_options is-flex">
                                                <div>
                                                    <asp:HyperLink ID="link_producto" Target="_blank" runat="server">Ver producto</asp:HyperLink>
                                                </div>
                                                <p class="is-px-4 is-text-blue">|</p>
                                                <asp:UpdatePanel UpdateMode="Always" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton OnClick="btn_eliminarProducto_Click" ID="btn_eliminarProducto" CssClass="" OnClientClick="btnLoading(this);" ClientIDMode="Static" runat="server">Eliminar</asp:LinkButton>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <asp:UpdatePanel ID="up_stock" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <div id="lbl_stock" visible="false" runat="server"></div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <div class="center-carrito_vacio">
                                    <h2>Aún no tienes artículos en tu carrito.</h2>
                                    <h3>¡Navega entre más de 2,000 productos!</h3>
                                    <img class="icon-carrito_vacio" alt="Carrito de compras vacio" title="Carrito de compras vacio" src="https://www.incom.mx/img/webUI/newdesign/carrito-vacio.svg">
                                    <div class="center-btn-carrito_vacio">
                                        <a class="is-btn-blue btn-carrito_vacio is-m-auto" href="/productos">Descubrir ofertas</a>
                                    </div>

                                </div>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>
                    <%--<div style="display: flex; flex-direction: column; justify-content: center; align-items: center; margin: 0;">
                        <p style="margin: 0; padding: 0.25rem 0">¿Qué deseas hacer con tu carrito?</p>
                        <div style="display: flex; justify-content: center; align-items: center">
                            <a class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 right-align modal-trigger" style="margin: 0 0.5rem" href="#modal1">Cotizar
                            <i class="material-icons right">content_paste</i>
                            </a>
                            <a class="waves-effect waves-light  btn  green  blue-grey-text text-lighten-5 modal-trigger" href="#modalPedido" style="margin: 0 0.5rem" runat="server">Comprar
                            <i class="material-icons right">shopping_basket</i>
                            </a>
                        </div>
                    </div>--%>
                    <%--<div style="padding: 0 1rem">
                        <strong>Aviso: </strong>
                        <span>Debido a la alta demanda nuestro inventario y entregas pueden verse afectados. Gracias por su comprensión.
                            <a target="_blank" href="/documents/INCOM-MEDIDAS-COVID.pdf">Consulta nuestro protocolo COVID</a>
                        </span>
                        <p style="padding: 0.25rem 0; margin: 0">Los tiempos de entrega pueden cambiar sin previo aviso.</p>
                        <i>Un asesor se comunicará contigo al realizar tu operación para confirmar la disponibilidad.</i>
                    </div>--%>
                </div>
                <div id="moreArrow" visible="false" runat="server">
                    <div class="arrow-doble-down is-text-blue is-flex" style="width: fit-content;">
                        <svg xmlns="http://www.w3.org/2000/svg" id="svg-arrow-down" width="288" height="288" viewBox="0 0 24 24">
                            <path class="arrow" fill="#2d6cdf" d="M12 12a1 1 0 0 1-.71-.29l-4-4A1 1 0 0 1 8.71 6.29L12 9.59l3.29-3.29a1 1 0 0 1 1.41 1.41l-4 4A1 1 0 0 1 12 12zM12 18a1 1 0 0 1-.71-.29l-4-4a1 1 0 0 1 1.41-1.41L12 15.59l3.29-3.29a1 1 0 0 1 1.41 1.41l-4 4A1 1 0 0 1 12 18z" class="color000 svgShape" />
                        </svg>
                    </div>
                </div>

                <!-- <div id="cover"></div> -->
                <div class="scrollable-container">
                    <div class="sticky-container">
                <div id="ctn_details" class="col s12 l4 xl4 right-align mi-carrito-ticket" runat="server">
                    <div class="is-flex moneda-ticket">
                        <p class="is-select-none">Moneda:</p>
                        <uc_mon:moneda ID="uc_moneda" runat="server"></uc_mon:moneda>
                    </div>
                    <div style="text-align: left; padding: 1rem;" class="is-border-soft is-border-collapse is-rounded-lg">
                        <div class="is-rounded-t-lg section-ticket-1">
                            <strong class="title-total_pedido is-w-full is-font-semibold is-select-none">Total del pedido</strong>
                        </div>
                        <div class="section-ticket-2">
                            <div class="is-flex is-justify-between is-items-center">
                                <!-- <span>Envío:</span>
                                <div>
                                    <asp:Label ID="lbl_envio" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_envio_nota" runat="server"></asp:Label>
                                    <%--<asp:HyperLink ID="link_API_desglose_envio" Target="_blank" Visible="false" runat="server" CssClass="sendLogo"><i class="material-icons">launch</i></asp:HyperLink>--%>
                                </div> -->
                            </div>
                            <div class="is-flex is-justify-between is-items-center">
                                <span>Subtotal:</span>
                                <asp:Label ID="lbl_subTotal" runat="server"></asp:Label>
                            </div>
                            <div class="is-flex is-justify-between is-items-center">
                                <span>Impuestos:</span>
                                <asp:Label ID="lbl_impuestos" runat="server"></asp:Label>
                            </div>
                            <div class="is-flex is-justify-between is-items-center">
                                <span>Total:</span>
                                <div>
                                    <asp:Label ID="lbl_total" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_moneda" CssClass="orange-text" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="is-text-center container-ticket_envio">
                            <span class="text-ticket_envio is-select-none">El costo de envío se calcula con tu dirección</span>
                        </div>
                    </div>
                </div>
                <%--
                <div id="lbl_consideracionesCopy" class="mi-carrito-consideraciones-lg is-top-4" runat="server">
                    <span>Consideraciones:</span>
                    <!-- <div class="is-text-justify">
                        <p>Debido a la alta demanda nuestro inventario y entregas pueden verse afectados. Gracias por su comprensión. <a target="_blank" href="/documents/INCOM-MEDIDAS-COVID.pdf">Consulta nuestro protocolo COVID</a></p>
                        <p>Los tiempos de entrega pueden cambiar sin previo aviso. Un asesor se comunicará contigo al realizar tu operación para confirmar la disponibilidad.</p>
                    </div> -->
                    <div class="is-text-justify">
                        <p>Debido a la alta demanda nuestro inventario y entregas pueden verse afectados. Gracias por su comprensión.</p>
                        <p>Los tiempos de entrega pueden cambiar sin previo aviso. Un asesor se comunicará contigo al realizar tu operación para confirmar la disponibilidad.</p>
                    </div>
                </div>--%>
                <div id="btn_continuarCompra" class="mi-carrito-boton-compra is-flex is-flex-col is-justify-center is-items-center" runat="server">
                    <asp:LinkButton ID="btn_comprar" ClientIDMode="Static" OnClick="btn_comprar_Click" CssClass="is-text-white is-btn-green" runat="server">
                        Continuar con la compra
                    </asp:LinkButton>
                    <!-- <a class="modal-trigger" href="#modalPedido" runat="server">Continuar con la compra</a> -->
                    <!-- <a href="#">Descargar cotización</a> -->
                </div>
                <div class="email-carrito">
                    <a href="mailto:telemarketing@incom.mx?cc=serviciosweb@incom.mx&subject=Consulta%20sobre%20productos%20sin%20stock" id="mail_telemarketing" visible="false" runat="server"><p>Comunicación a través de telemarketing@incom.mx</a>
                </div>
                <div id="lbl_consideraciones" visible="true" class="mi-carrito-consideraciones-lg" runat="server">
                    <div class="right-mi_carrito_consideraciones">
                        <span class="is-font-semibold">Consideraciones:</span>
                        <div class="is-text-justify">
                            <p>Debido a la alta demanda nuestro inventario y entregas pueden verse afectados. Gracias por su comprensión.</p>
                            <p>Los tiempos de entrega pueden cambiar sin previo aviso. Un asesor se comunicará contigo al realizar tu operación para confirmar la disponibilidad.</p>
                        </div>
                    </div>
                </div>
                </div>
            </div>
            </div>
            <%--<div id="content_msg_exito_operacion" visible="false" class="row center-align" runat="server">
                <h1 class="blue-text" id="">Tu 
                    <asp:Literal ID="lt_tipo_operacion" Text="Operación " runat="server"></asp:Literal>
                    se ha realizado con éxito</h1>
                <h2>En 3 segundos serás redireccionado a tu operación</h2>
                <i class="material-icons large">check</i>
            </div>--%>
        </ContentTemplate>
    </asp:UpdatePanel>


    <!--- INICIO Modal cotización --->
    <div id="modal1" class="modal">
        <div class="modal-content">
            <p id="ContentAlertCrearCotizacion" runat="server">
            </p>
            <h3>Nombre de tu cotización</h3>
            <p>
                <strong>Ingresa un nombre</strong> de referencia para que <strong>identifiques tu cotización fácilmente.</strong>, ejemplos:
                <br />
                <i>Material para instalación tienda, Proyecto Aeropuerto, Material para la casa.</i>
            </p>
            <p>
                <asp:TextBox ID="txtNombrecotizacion" ClientIDMode="Static" placeholder="Ingresa un nombre de cotización" runat="server"></asp:TextBox>

            </p>
            <p>
                <label>
                    <asp:CheckBox ID="chk_cotizacion_sin_envio" runat="server" />
                    <span>No requiero envio</span>
                </label>
            </p>
            <p style="display: none;">
                <label>Teléfono *</label>
                <asp:TextBox Visible="true" ID="txtTelefonoCotizacion" placeholder="Teléfono (Obligatorio)" runat="server"></asp:TextBox>
            </p>
            <p>
                <asp:LinkButton ID="btn_crearCotizacion" OnClick="btn_crearCotizacion_Click" OnClientClick="btnLoading(this);" ClientIDMode="Static"
                    CssClass="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5" runat="server">
                Crear Cotizacion
                <i class="material-icons right">send</i>
                </asp:LinkButton>
            </p>
            <script>
                /* Script que ayuda a crear la operación al teclear la tecla "enter"  */
                var input = document.getElementById("txtNombrecotizacion");
                input.addEventListener("keyup", function (event) {
                    event.preventDefault();
                    if (event.keyCode === 13) {
                        document.getElementById("btn_crearCotizacion").click();
                    }
                });

            </script>
        </div>
        <div class="modal-footer">
            <a href="#!" class="modal-action modal-close waves-effect waves-light btn-flat">Cancelar</a>
        </div>
    </div>

    <!--- FIN Modal Cotización --->


    <!--- INICIO Modal Pedido --->
    <%--    <div id="modalPedido" class="modal">
        <div class="modal-content">
            <h3>Nombre de tu pedido</h3>
            <p id="ContentAlertCrearPedido" runat="server">
            </p>
            <p>
                <strong>Ingresa un nombre</strong> de referencia para que <strong>identifiques tu pedido fácilmente.</strong>, ejemplos:
                <br />
                <i>Material para instalación tienda, Proyecto Aeropuerto, Material para la casa.</i>
            </p>
            <p>
                <asp:TextBox ID="txtNombrePedido" ClientIDMode="Static" placeholder="Ingresa un nombre de pedido" runat="server"></asp:TextBox>
            </p>
            <p style="display: block;">
                <label>Teléfono *</label>
                <asp:TextBox Visible="true" ID="txtTelefonoPedido" placeholder="Teléfono (Obligatorio)" runat="server"></asp:TextBox>
            </p>
            <p>
                <p>
                    <label>
                        <asp:CheckBox ID="chk_pedido_sin_envio" runat="server" />
                        <span>No requiero envio</span>
                    </label>
                </p>
                <asp:LinkButton ID="btn_comprar" ClientIDMode="Static" OnClick="btn_comprar_Click" OnClientClick="btnLoading(this);"
                    CssClass="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5" runat="server">
                Crear Pedido
                <i class="material-icons right">send</i>
                </asp:LinkButton>
            </p>
        </div>
        <div class="modal-footer">
            <a href="#!" class="modal-action modal-close waves-effect waves-light btn-flat">Cancelar</a>
        </div>
    </div>--%>

    <!--- FIN Modal Pedido --->
    <script> document.addEventListener("DOMContentLoaded", function (event) {
            $('.modal').modal({
                ready: function (modal, trigger) { // Callback for Modal open. Modal and trigger parameters available.
                    $("#txtNombrecotizacion").focus();
                }
            }
            );

            $('#modalPedido').modal({
                ready: function (modal, trigger) { // Callback for Modal open. Modal and trigger parameters available.
                    $("#txtNombrePedido").focus();
                }
            }
            );

        });
    </script>
    <script>
        /* Script que ayuda a crear la operación al teclear la tecla "enter"  */
        var input = document.getElementById("txtNombrePedido");
        input.addEventListener("keyup", function (event) {
            event.preventDefault();
            if (event.keyCode === 13) {
                document.getElementById("btn_comprar").click();
            }
        });
    </script>
    <script>
        $(window).scroll(function (e) {
            frames = 17;
            step = ($("div.arrow-doble-down").height() - $(window).height()) / frames;
            scrollStep = parseInt($(window).scrollTop() / step);
            maskPosition = 100 / frames * scrollStep;
            // $("#cover").css({
            //     "mask-position": maskPosition + "% 90%",
            //     "-webkit-mask-position": maskPosition + "% 90%"
            // });
        });
    </script>
    <style>
        /* div.arrow-doble-down {
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    } */

        div.arrow-doble-down #svg-arrow-down .arrow {
            -webkit-animation: scroll 1.25s ease-in alternate infinite;
            animation: scroll 1.25s ease-in alternate infinite;
        }

        @-webkit-keyframes scroll {
            0% {
                transform: translateY(0);
            }

            100% {
                transform: translateY(10px);
            }
        }

        @keyframes scroll {
            0% {
                transform: translateY(0);
            }

            100% {
                transform: translateY(10px);
            }
        }

        @keyframes scroll {
            0% {
                transform: translateY(0);
            }

            100% {
                transform: translateY(10px);
            }
        }
    </style>
</asp:Content>

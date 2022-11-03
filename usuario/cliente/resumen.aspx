<%@ Page Language="C#" AutoEventWireup="true" Async="true" MaintainScrollPositionOnPostback="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="resumen.aspx.cs" Inherits="usuario_cliente_resumen" %>

<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="hdr" TagName="menu" %>
<%@ Register TagPrefix="uc" TagName="ddlAsignarUsuarioAsesor" Src="~/userControls/operaciones/PedidosUsuarioSeguimientoUC.ascx" %>
<%@ Register TagPrefix="uc" TagName="EdicionDetallesDeEnvioPedido" Src="~/userControls/operaciones/EditarCostoDeEnvioPedido.ascx" %>
<%@ Register TagPrefix="uc" TagName="progreso" Src="~/userControls/uc_progresoCompra.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <!-- Event snippet for Inclusión en el carrito conversion page -->
    <script> gtag('event', 'conversion', { 'send_to': 'AW-10903694501/B-zjCMjt3MEDEKXZpM8o' }); </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="body">
    <hdr:menu ID="menuGeneral" runat="server" />
    <asp:HiddenField ID="hf_id_pedido" runat="server" />
    <asp:HiddenField ID="hf_pedido_tipo_envio" runat="server" />
    <asp:HiddenField ID="hf_id_pedido_direccion_envio" runat="server" />
    <uc:progreso runat="server"></uc:progreso>
    <div class="is-container is-px-4">
        <div class="is-py-2">
            <h2 class="is-text-xl is-font-bold is-select-none">Resumen de pedido<asp:Literal ID="lt_nombre_operacion" runat="server" Visible="false"></asp:Literal></h2>
        </div>
        <div class="row">
            <div class="col  col-12 col-xs-12 col-sm-12 col-md-5 col-xl-6">
                <div id="Content_AsesorSeguimiento" runat="server" visible="false" class="is-rounded-lg is-shadow is-my-2">
                    <div class="is-flex is-flex-col">
                        <h4 class="is-text-lg is-font-semibold is-bg-gray-300 is-px-8 is-rounded-t-lg is-select-none">Seguimiento</h4>
                        <div class="is-px-8 is-py-2">
                            <h6 id="H1" runat="server" class="card-subtitle text-muted"></h6>
                            <p id="P1" runat="server" class="card-text"></p>
                            <p id="P2" runat="server">
                                <uc:ddlAsignarUsuarioAsesor ID="AsignarUsuarioAsesor" runat="server" />
                            </p>
                        </div>
                    </div>
                </div>
                <div class="is-rounded-lg is-shadow is-my-2">
                    <div class="is-flex is-flex-col">
                        <section class="is-bg-gray-300 is-px-8 is-rounded-t-lg is-select-none">
                            <h4 class="is-text-lg is-font-semibold" style="margin: 0 auto;">Datos de quien recibe</h4>
                        </section>
                        <div class="is-px-8 is-py-2">
                            <h6 id="contacto_title" runat="server"></h6>
                            <p id="contacto_desc" runat="server"></p>
                             <asp:HyperLink runat="server" ID="link_cambiar_contacto" style="text-decoration: none;">
                               Editar quien recibe
                            </asp:HyperLink>
                        </div>
                    </div>
                </div>
                <div class="is-rounded-lg is-shadow is-my-2">
                    <div class="is-flex is-flex-col">
                        <section class="is-bg-gray-300 is-px-8 is-rounded-t-lg is-select-none">
                            <h4 class="is-text-lg is-font-semibold" style="margin: 0 auto;">Envío</h4>
                        </section>
                        <div class="is-px-8 is-py-2">
                            <h6 id="metodo_envio_title" class="card-subtitle" runat="server"></h6>
                            <div class="is-flex is-justify-between is-items-start">
                                <p id="metodo_envio_desc" class="is-select-all" runat="server"></p>
                                <a id="localizacionTienda" visible="false" href="https://g.page/Incom_CDMX?share" target="_blank" rel="noreferrer noopener" class="is-decoration-none is-text-black" runat="server">
                                    <span>
                                        <svg class="is-w-6 is-h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M17.657 16.657L13.414 20.9a1.998 1.998 0 01-2.827 0l-4.244-4.243a8 8 0 1111.314 0z"></path>
                                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 11a3 3 0 11-6 0 3 3 0 016 0z"></path>
                                        </svg>
                                    </span>
                                </a>
                            </div>
                            <div id="msg_alert_envio" visible="false" class="alert alert-warning" role="alert" runat="server"></div>
                            <%--<asp:LinkButton ID="btn_Borrar_msg_alert_envio" CssClass="mb-2" Visible="false" OnClick="btn_Borrar_msg_alert_envio_Click" runat="server">
                            Borrar mensaje de error envío
                            </asp:LinkButton>--%>
                            <div class="is-flex is-justify-between is-items-center">
                                <%--<div class="Conteng_msg_envioNota"></div>--%>
                                <div class="dropdown">
                                    <asp:HyperLink runat="server" ID="btn_cambiar_metodo_envio" style="text-decoration: none;">
                                Cambiar método de envío
                                    </asp:HyperLink>
                                </div>
                                <uc:EdicionDetallesDeEnvioPedido ID="uc_EdicionDetallesDeEnvioPedido" runat="server" />
                            </div>
                        </div>

                    </div>
                </div>
                <div class="is-rounded-lg is-shadow is-my-2">
                    <div class="is-flex is-flex-col">
                        <section class="is-bg-gray-300 is-px-8 is-rounded-t-lg is-select-none">
                            <h4 class="is-text-lg is-font-semibold" style="margin: 0 auto;">Facturación</h4>
                        </section>
                        <div class="is-px-8 is-py-2">
                            <h6 id="facturacion_title" runat="server" class="card-subtitle"></h6>
                            <p id="facturacion_desc" runat="server" class="card-text"></p>
                            <p id="ContentFacturacionUsoCFDI" runat="server">
                                <strong>
                                    <label for="ddl_UsoCFDI" class="form-label">Uso de CFDI</label>
                                    <span class="text-danger">*</span>
                                </strong>
                                <asp:DropDownList ID="ddl_UsoCFDI" class="form-select"
                                    OnSelectedIndexChanged="ddl_UsoCFDI_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                </asp:DropDownList>
                            </p>
                            <div class="is-flex is-justify-between is-items-center">
                                <!-- <asp:LinkButton runat="server" ID="btn_sin_factura" OnClick="btn_sin_factura_Click"
                                    class="">
                             Sin factura </asp:LinkButton> -->
                                <asp:HyperLink runat="server" ID="link_cambiar_direcc_facturacion" style="text-decoration: none;">
                                Agregar datos de facturación
                                </asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- <h1 class="h3">#<asp:Literal ID="lt_numero_pedido" runat="server"></asp:Literal></h1> -->
                <!-- <p>
                    Creado el <strong>
                        <asp:Label ID="lbl_fecha_creacion" runat="server"></asp:Label></strong>
                    Usuario: <strong>
                        <asp:Literal ID="lt_usuario_cliente" runat="server"></asp:Literal></strong>
                </p> -->

                <div id="content_pedido_cancelado" visible="false" runat="server">
                    <div class="alert alert-danger" role="alert">
                        <h4 class="alert-heading">Pedido cancelado</h4>
                        <p>Se encontró una solicitud de cancelación de pedido o bien, tu pedido ya se encuentra cancelado</p>
                        <strong>Mensaje:</strong>
                        <asp:Label ID="lbl_motivoCancelacion" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="d-grid gap-2 mt-3">
                    <h3>Aviso</h3>
                    <ul class="is-p-0" style="text-decoration: none; list-style-type: none;">
                        <li>Los <strong>costos de envío</strong> podrían ser recalculados y resultar mayores o menores a lo mostrado.</li>
                        <li>Por seguridad, algunos productos requieren un <strong>seguro de envío</strong>  con costo adicional. De ser el caso, su asesor de ventas le informará.</li>
                        <li>Una vez registrado el pago, solo se podrá actualizar el método de envío con ayuda de un asesor. Los datos de facturación o contacto se pueden actualizar normalmente.</li>
                    </ul>
                    <div class="content_msg_confirmacion_pedido" runat="server"></div>
                </div>
                <div class="row">
                    <div class="is-w-auto">
                        <div id="content_msg_cancelar_pedido"></div>
                            <a id="link_modal_cancelar_pedido" runat="server" data-bs-toggle="modal" data-bs-target="#modal_cancelar_pedido"
                        class="is-text-red is-decoration-none is-text-center">Cancelar pedido</a>
                    </div>
                </div>
                <asp:Panel ID="Pago_Pendiente" Visible="false" CssClass="mt-4 col col-12 col-xs-12 col-sm-12 col-md-12 col-xl-12" runat="server">
                <div class="is-flex">
                    <figure>
                        <figcaption style="float: left; width: fit-content;">Paga de manera segura por alguno de nuestros medios.</figcaption>
                        <br>
                        <img class="icono-formas_pago" alt="Formas de pago; Visa, Mastercard, American Express, Paypal, Transferencia Bancaria y Mercado Pago" title="Formas de pago" src="/img/webUI/newdesign/Formas_de_pago.png"/>
                    </figure>
                </div>
                <div style="float: left; width: fit-content;">
                    <p>Elige el método de pago:</p> 
                    <asp:HyperLink ID="link_pago_santander" runat="server"><div class="is-btn-gray">Tarjeta crédito/débito</div></asp:HyperLink>
                    <asp:HyperLink ID="link_pago_paypal" class="is-text-white is-decoration-none" runat="server"><div class="is-btn-gray"><p id="text-paypal" style="color: white;">PayPal</p></div></asp:HyperLink>
                    <a data-bs-toggle="modal" data-bs-target="#modal_deposito_trans"><div class="is-btn-gray">Transferencia o depósito</div></a>          
                    <div class="alert alert-warning mt-4" role="alert">
                        <strong>Aviso</strong>  No sé ha confirmado un pago aún.
                    </div>
                </div>
                </asp:Panel>
                <asp:Panel ID="Pago_Confirmado" Visible="false" CssClass="mt-4 col col-12 col-xs-12 col-sm-12 col-md-12 col-xl-12" runat="server">
                    <p class="h5">
                        Pago ya realizado vía
                        <asp:Label ID="Tipo_pago" runat="server"></asp:Label>
                    </p>
                </asp:Panel>
            </div>
            <div class="col col-12  col-xs-12 col-sm-12 col-md-7 col-xl-6">
                <div class="is-bg-gray-300 is-rounded-t wrapp-product_list">
                    <p class="h6" style="font-weight: 600; margin: 0;">Productos</p>
                </div>
                <asp:ListView ID="lv_productos" OnItemDataBound="lv_productos_ItemDataBound" Visible="true" runat="server">
                    <LayoutTemplate>
                        <div>
                            <ul class="list-group-resumen list-group mb-3 is-rounded-b">
                                <div id="itemPlaceholder" runat="server"></div>
                            </ul>
                        </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li class="list-group-item d-flex lh-sm">
                            <asp:Image ID="img_producto" class="img-fluid" Style="width: 100px;" runat="server" />
                            <div style="justify-content: space-between; display: flex; width: 100%; margin: 1rem">
                                <div>
                                    <h6 class="my-0"><%#Eval("productos.numero_parte") %></h6>
                                    <small class="text-muted"><%#Eval("productos.descripcion") %>
                                        <br />
                                        <asp:Literal ID="lt_cantidad" Text='<%#Eval("productos.cantidad") %>' runat="server"> </asp:Literal>
                                        pza
                                                x
                                            <asp:Literal ID="lt_precio_unitario" Text=' <%#Eval("productos.precio_unitario") %>' runat="server"></asp:Literal>
                                    </small>
                                </div>
                                <span class="text-muted" style="float: right;">
                                    <strong>
                                        <asp:Literal ID="lt_precio_total" Text='<%#Eval("productos.precio_total") %>' runat="server"></asp:Literal>
                                    </strong>
                                </span>
                            </div>
                        </li>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <div class="is-flex is-justify-center is-items-center">
                            <p>No hay productos</p>
                        </div>
                    </EmptyDataTemplate>
                </asp:ListView>
                <div style="padding: 1rem; border: 1px solid #2333; border-radius: 8px;">
                    <table class="table table-sm">
                        <thead>
                            <tr>
                                <th scope="col">Total del pedido</th>
                                <!-- <th class="text-end" scope="col">Total</th> -->
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Productos</td>
                                <td class="text-end">
                                    <asp:Label ID="lbl_total_productos" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Envío (estándar):</td>
                                <td class="text-end">
                                    <asp:Label ID="lbl_envio" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Subtotal:</td>
                                <td class="text-end">
                                    <asp:Label ID="lbl_subtotal" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Impuestos:</td>
                                <td class="text-end">
                                    <asp:Label ID="lbl_impuestos" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="table-active">
                                <td>Total:</td>
                                <td class="text-end">
                                    <asp:Label ID="lbl_total" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>

                <div class="row is-top-2">
                    <div class="is-m-auto is-w-auto">
                        <div id="content_msg_cancelar_pedido"></div>
                            <a id="link_modal_cancelar_pedido" runat="server" data-bs-toggle="modal" data-bs-target="#modal_cancelar_pedido"
                        class="is-text-red is-decoration-none is-text-center">Cancelar pedido</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal cancelar pedido -->
    <div class="modal  " id="modal_cancelar_pedido" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog  modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">
                        <strong>
                            Cancelar pedido:
                        </strong>
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <p>
                        Una vez que solicites la cancelación de pedido no podras realizar cambios en este.
                    </p>
                    <div class="mb-3">
                        <label for="txt_motivo_cancelacion" class="form-label">Cuéntanos el motivo de cancelación:</label>
                        <asp:TextBox ID="txt_motivo_cancelacion" ClientIDMode="Static" class="form-control" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                    </div>
                    <asp:LinkButton OnClick="btn_cancelar_pedido_Click" OnClientClick="BootstrapClickLoading(this);"
                        ID="btn_cancelar_pedido" runat="server">
                        <div class="btn-cancelar_pedido">
                            <p>
                               Cancelar pedido
                            </p>
                        </div>
                    </asp:LinkButton>
                </div>
                <div class="modal-footer">
                    <!-- <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar ventana</button> -->
                </div>
            </div>
        </div>
    </div>
    <!-- FIN Modal cancelar pedido -->

    <!-- Modal cuentas bancarias -->
    <div class="modal  " id="modal_deposito_trans" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog  modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Transferencia o deposito</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <img src="/img/informacion/logo-banamex.svg" style="width: 200px;" class="img-thumbnail" />
                    <h5 class="card-title mt-2">Datos de la cuenta</h5>
                    <p>
                        Número de operación
            <strong>Razón social:</strong> Insumos Comerciales de Occidente S.A. de C.V.
                            <br>
                        <strong>RFC: </strong>ICO990224H93<br>
                        <strong>Dirección:</strong> Plutarco Elías Calles 276, Colonia Tlazintla, Ciudad de México, México. C.P. 08710, Delegación: Iztacalco<br>
                        <strong>Banco:</strong> Banamex
                    </p>
                    <div class="table-responsive ">
                        <table class="table table-striped">
                            <tbody>
                            </tbody>
                            <thead>
                                <tr>
                                    <th>Moneda </th>
                                    <th>Sucursal</th>
                                    <th>Cuenta</th>
                                    <th>Clabe</th>
                                    <th>Plaza</th>
                                    <th>Swift</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Moneda Nacional</td>
                                    <td>0269</td>
                                    <td>7782861</td>
                                    <td>002180026977828615</td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Dólares (USD)</td>
                                    <td>414</td>
                                    <td>9412714</td>
                                    <td>002180041494127146</td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Dólares (USD) desde el extranjero</td>
                                    <td>414</td>
                                    <td>9412714</td>
                                    <td>002180041494127146</td>
                                    <td>001</td>
                                    <td>BNMXMXMM</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <p>
                        <span class="h4">Importante</span><br />

                        Con el fin de agilizar la identificación de su pago, le agradeceremos indicar en el campo de referencia alfanumérica del depósito
                           el número de pedido y/o nombre del pedido.
             <br />
                        <br />
                        <strong>Número de pedido: </strong>
                        <span class="text-success"><%= lt_numero_pedido.Text %></span>
                    </p>


                    <asp:UpdatePanel ID="up_ConfirmarDeposito" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="ContentGenerarReferenciaTransferencia" runat="server">
                                Si ya realizaste la transferencia da click en el siguiente botón:
                      <br />
                                <asp:LinkButton ID="btn_pago_transferencia" OnClick="btn_pago_transferencia_Click"
                                    class="btn btn-success"
                                    runat="server">Ya realicé el pago</asp:LinkButton>
                            </asp:Panel>
                            <asp:Panel ID="ContentReferenciaTransferencia" Visible="false" runat="server">
                                <p class="h4">Gracias por tu pago</p>
                                <p>En unos momentos se confirmara tu transferencia y un asesor se comunicará contigo.</p>
                                <asp:Label ID="lbl_Referenca_Transferencia" runat="server" Text=""></asp:Label>

                                <asp:Panel ID="Content_Pago_Datos_Transferencia_Asesor" Visible="false" runat="server">
                                    <div class="mb-3">

                                        <label for="txt_TranfenciaReferenciaAsesor" class="form-label">Asesor: Ingresa algún texto referente a esta transferencia, solo lo verás tú.</label>
                                        <asp:TextBox ID="txt_TranfenciaReferenciaAsesor" ClientIDMode="Static" TextMode="Multiline" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="mb-3">

                                        <div class="form-check">
                                            <asp:CheckBox ID="chk_TranfenciaConfirmadaAsesor" CssClass="form-check-input" ClientIDMode="Static" runat="server" />
                                            <label class="form-check-label" for="chk_TranfenciaConfirmada">
                                                Pago confirmado
                                            </label>
                                        </div>
                                    </div>
                                    <asp:LinkButton ID="btn_guardarTransferenciaDatosAsesor" OnClick="btn_guardarTransferenciaDatosAsesor_Click"
                                        CssClass="btn btn-primary" runat="server">Guardar</asp:LinkButton>
                                </asp:Panel>
                            </asp:Panel>
                            <div id="content_msg_transfrencia"></div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_pago_transferencia" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btn_guardarTransferenciaDatosAsesor" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- FIN Modal cuentas bancarias -->


    <style>

    .wrapp-product_list {
        margin-top: 0.5rem;
        height: 32px;
        padding-left: 2rem;
        display: flex;
        align-items: center;
    }

    .btn-cancelar_pedido {
        border: 1px solid red;
        width: 150px;
        height: 38px;
        text-align: center;
        display: flex;
        justify-content: center;
        align-content: center;
        align-items: center;
        border-radius: 8px;
        position: relative;
        float: left;
    }

    .btn-cancelar_pedido > p {
        text-decoration: none;
        color: red;
        margin: auto;
    }

    @media only screen and (min-width: 1600px) {
        .icono-formas_pago {
            width: 450px;
        }
    }

    </style>
</asp:Content>

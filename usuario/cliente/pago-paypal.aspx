<%@ Page Language="C#" AutoEventWireup="true" Async="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="pago-paypal.aspx.cs"
    Inherits="usuario_cliente_pago_paypal" %>

<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="header" TagName="menuGeneral" %>
<%@ Register Src="~/userControls/uc_progresoCompra.ascx" TagPrefix="uc" TagName="progreso" %>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <header:menuGeneral ID="menuGeneral" runat="server" />
    <uc:progreso ID="barraProgreso" runat="server" />
    <asp:HiddenField ID="hf_id_pedido" runat="server" />
    <asp:HiddenField ID="hf_numero_operacion" runat="server" />
    <asp:HiddenField ID="hf_moneda" runat="server" />

    <div class="container">
        <div class="is-flex is-justify-start is-items-start">
            <h1 class="h5">Método de envío del pedido:<asp:Label ID="lt_numero_operacion" class="is-px-2 is-select-all" runat="server"></asp:Label></h1>
            <button type="button" class="is-cursor-pointer" style="background-color: transparent; outline: none; border: none;" onclick="copiarNumeroParte('body_lt_numero_operacion', 'Número de pedido')">
                <span class="is-text-gray">
                    <svg class="is-w-4 is-h-4" aria-labelledby="Clipcopy" title="Copiar elemento" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 5H6a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2v-1M8 5a2 2 0 002 2h2a2 2 0 002-2M8 5a2 2 0 012-2h2a2 2 0 012 2m0 0h2a2 2 0 012 2v3m2 4H10m0 0l3-3m-3 3l3 3"></path>
                        <title id="Clipcopy">Copiar elemento</title>
                    </svg>
                </span>
            </button>
        </div>
        <%--<div class="row">
            <div class="col">
                <h1 class="">Método de pago PayPal del pedido:
                    <asp:Literal ID="lt_numero_operacion" runat="server"></asp:Literal></h1>
            </div>
        </div>--%>
        <div class="is-flex is-justify-between is-items-center">
            <div class="is-w-1_2 is-py-4">
                <asp:UpdatePanel ID="up_estatus_paypal" UpdateMode="Conditional" RenderMode="Block" runat="server" class="col col-12">
                    <ContentTemplate>
                        <div id="paypal_button_container" class="paypal_button_container" runat="server"></div>
                        <asp:Panel ID="lbl_NoDisponiblePago" class="is-select-none is-border is-border-black is-rounded is-p-4" Visible="false" runat="server">
                            <strong>El pago no esta disponible por los siguientes motivos:</strong>
                            <p id="motivosNoDisponiblePago" visible="false" runat="server"></p>
                        </asp:Panel>
                        <div class="is-flex is-justify-center is-items-center">
                            <asp:LinkButton ID="btn_renovarPedido" CssClass="is-decoration-none" Visible="false" OnClientClick="btnLoading(this);" OnClick="btn_renovarPedido_Click" runat="server">Renovar pedido</asp:LinkButton>
                        </div>
                        <div id="texto_cargando_informacion" class=" d-none is-text-sm is-py-2">Cargando información de pago...</div>
                        <div id="content_msg_bootstrap"></div>
                        <table id="dt_desglose_paypal" class="table" runat="server">
                            <tr>
                                <td>Tipo de intento</td>
                                <td>
                                    <asp:Label ID="lbl_paypal_intento" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Estado del pago</td>
                                <td>
                                    <asp:Label ID="lbl_paypal_estado" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Monto Pagado en PayPal</td>
                                <td>
                                    <asp:Label ID="lbl_paypal_monto" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_paypal_moneda" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Fecha del primer intento</td>
                                <td>
                                    <asp:Label ID="lbl_paypal_fecha_primerIntento" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Última actualización</td>
                                <td>
                                    <asp:Label ID="lbl_paypal_fecha_actualización" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:LinkButton ID="linkActualizarUP" CssClass="d-none" runat="server" OnClick="linkActualizarUP_Click">Actualizar</asp:LinkButton>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="linkActualizarUP" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div>
                <div class="is-flex is-justify-center is-items-center">
                    <asp:HyperLink ID="link_regresar_metodos" class="is-decoration-none" runat="server">Regresar a métodos de pago</asp:HyperLink>
                </div>
                <div style="border: 1px solid #b7b7b7; border-radius: 8px; width: 420px; height: fit-content;">
                    <table style="width: 100%;">
                        <thead style="border-bottom: 1px solid #b7b7b7;">
                            <tr>
                                <td colspan="2" style="padding: 0.75rem 0.75rem 0.75rem 1.5rem;"><strong>Desglose</strong></td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td style="padding: 0.75rem 0rem 0.25rem 1.5rem;">Productos:</td>
                                <td style="padding: 0.75rem 1.5rem 0.5rem 0; text-align: end;">
                                    <asp:Label ID="lbl_productos" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 0.25rem 1.5rem;">Descuentos:</td>
                                <td style="padding: 0.25rem 1.5rem; text-align: end;">
                                    <asp:Label ID="lbl_descuento" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 0.25rem 1.5rem;">Envío (estándar):</td>
                                <td style="padding: 0.25rem 1.5rem; text-align: end;">
                                    <asp:Label ID="lbl_envio" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 0.25rem 1.5rem;">Subtotal:</td>
                                <td style="padding: 0.25rem 1.5rem; text-align: end;">
                                    <asp:Label ID="lbl_subtotal" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 0.25rem 0rem 0.75rem 1.5rem;">Impuestos:</td>
                                <td style="padding: 0.25rem 1.5rem 0.75rem 0rem; text-align: end;">
                                    <asp:Label ID="lbl_impuestos" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                        <tfoot style="border-top: 1px solid #b7b7b7;">
                            <tr>
                                <td style="padding: 0.75rem 0rem 0.75rem 1.5rem;">Total:</td>
                                <td style="padding: 0.75rem 1.5rem 0.75rem 0rem; text-align: end;">
                                    <asp:Label ID="lbl_total" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
                <%--<table class="">
                    <thead>
                        <tr>
                            <th scope="col">Concepto</th>
                            <th class="text-end" scope="col">Total
                                <asp:Label ID="lbl_moneda" runat="server"></asp:Label>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Productos</td>
                            <td class="text-end"><strong>
                                <asp:Label ID="lbl_total_productos" runat="server"></asp:Label></strong></td>
                        </tr>
                        <tr>
                            <td>Envío (<asp:Label ID="lbl_metodoEnvio" runat="server"></asp:Label>):</td>
                            <td class="text-end"><strong>
                                <asp:Label ID="lbl_envio" runat="server"></asp:Label></strong></td>
                        </tr>
                        <tr id="content_descuento_subtotal" runat="server" visible="false">
                            <td>Sub Total</td>
                            <td class="text-end"><strong>
                                <asp:Label ID="lbl_subtotalSinDescuento" runat="server"></asp:Label></strong></td>
                        </tr>
                        <tr id="content_descuento" runat="server" visible="false">
                            <td>Descuento aplicado: </td>
                            <td class="text-end"><strong>
                                <asp:Label ID="lbl_descuento_porcentaje" runat="server"></asp:Label>%</strong></td>
                        </tr>
                        <tr>
                            <td>Subtotal</td>
                            <td class="text-end"><strong>
                                <asp:Label ID="lbl_subtotal" runat="server"></asp:Label></strong></td>
                        </tr>
                        <tr>
                            <td>Impuestos</td>
                            <td class="text-end"><strong>
                                <asp:Label ID="lbl_impuestos" runat="server"></asp:Label></strong></td>
                        </tr>
                        <tr class="table-active">
                            <td>Total</td>
                            <td class="text-end"><strong>
                                <asp:Label ID="lbl_total" runat="server"></asp:Label></strong></td>
                        </tr>
                    </tbody>
                </table>--%>
            </div>
        </div>
    </div>
</asp:Content>

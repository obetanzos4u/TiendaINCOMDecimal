<%@ Page Language="C#" AutoEventWireup="true" Async="true" MaintainScrollPositionOnPostback="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="pago.aspx.cs" Inherits="usuario_cliente_pago" %>

<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="hdr" TagName="menu" %>
<%@ Register TagPrefix="uc" TagName="progreso" Src="~/userControls/uc_progresoCompra.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <asp:HiddenField ID="hf_id_operacion" runat="server" />
    <asp:HiddenField ID="hf_moneda" runat="server" />
    <hdr:menu runat="server" />
    <uc:progreso runat="server" />
    <div class="container-md is-top-3">
        <div class="is-flex is-flex-col is-justify-center is-items-center">
            <div class="is-w-full is-flex is-justify-between is-items-center">
                <div class="is-flex is-justify-center is-items-center">
                    <h4>Método de pago:
                    <asp:Label ID="lbl_numero_pedido" class="is-select-all" runat="server"></asp:Label></h4>
                    <button type="button" class="is-cursor-pointer" style="background-color: transparent; outline: none; border: none;" onclick="copiarNumeroParte('body_lt_numero_pedido', 'Pedido')">
                        <span class="is-text-gray">
                            <svg class="is-w-4 is-h-4" aria-labelledby="Clipcopy" title="Copiar elemento" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 5H6a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2v-1M8 5a2 2 0 002 2h2a2 2 0 002-2M8 5a2 2 0 012-2h2a2 2 0 012 2m0 0h2a2 2 0 012 2v3m2 4H10m0 0l3-3m-3 3l3 3"></path>
                                <title id="Clipcopy">Copiar elemento</title>
                            </svg>
                        </span>
                    </button>
                </div>
                <div class="is-flex is-justify-center is-items-center">
                    <p>Aceptamos: </p>
                    <img src="https://via.placeholder.com/400x50" alt="Métodos de pago bancario" />
                </div>
            </div>
            <div class="is-container">
                <div class="is-flex is-justify-between is-items-center">
                    <div>
                        <p>Elige el método de pago: </p>
                        <asp:UpdatePanel ID="up_pasarelaPago" UpdateMode="Conditional" RenderMode="Block" runat="server">
                            <ContentTemplate>
                                <div class="is-flex is-justify-evenly is-items-start">
                                    <asp:Button ID="btn_tarjeta" Text="Tarjeta de crédito/débito" OnClick="btn_tarjeta_Click" UseSubmitBehavior="false" runat="server" />
                                    <asp:Button ID="btn_paypal" Text="PayPal" OnClick="btn_paypal_Click" UseSubmitBehavior="false" runat="server" />
                                    <asp:Button ID="btn_transferencia" OnClick="btn_transferencia_Click" Text="Transferencia o deposito" UseSubmitBehavior="false" runat="server" />
                                </div>
                                <asp:Panel ID="pnl_tarjeta" Visible="false" runat="server">
                                    <p>Información de pago</p>
                                    <iframe id="frm_pagoTarjeta" visible="true" style="width: 100%; height: 680px; border: 0;" runat="server"></iframe>
                                </asp:Panel>
                                <asp:Panel ID="pnl_paypal" Visible="false" runat="server">
                                    <p>Pago con PayPal</p>
                                </asp:Panel>
                                <asp:Panel ID="pnl_transferencia" Visible="false" runat="server">
                                    <p>Pago con transferencia</p>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_tarjeta" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btn_paypal" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btn_transferencia" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div>
                        <table>
                            <thead>
                                <tr>
                                    <td>Desglose</td>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Productos:</td>
                                    <td>
                                        <asp:Label ID="lbl_productos" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Descuentos:</td>
                                    <td>
                                        <asp:Label ID="lbl_descuento" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Envío (estándar):</td>
                                    <td>
                                        <asp:Label ID="lbl_envio" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Subtotal:</td>
                                    <td>
                                        <asp:Label ID="lbl_subtotal" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Impuestos:</td>
                                    <td>
                                        <asp:Label ID="lbl_impuestos" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td>Total:</td>
                                    <td>
                                        <asp:Label ID="lbl_total" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

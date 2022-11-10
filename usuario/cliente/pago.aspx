<%@ Page Language="C#" AutoEventWireup="true" Async="true" MaintainScrollPositionOnPostback="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="pago.aspx.cs" Inherits="usuario_cliente_pago" %>

<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="hdr" TagName="menu" %>
<%@ Register TagPrefix="uc" TagName="progreso" Src="~/userControls/uc_progresoCompra.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <asp:HiddenField ID="hf_id_operacion" runat="server" />
    <asp:HiddenField ID="hf_moneda" runat="server" />
    <asp:HiddenField ID="hf_numero_operacion" runat="server" />
    <hdr:menu runat="server" />
    <uc:progreso runat="server" />
    <div class="container-pago container-md is-top-3">
        <div class="is-flex is-flex-col is-justify-center is-items-center">
            <div class="is-w-full is-flex is-justify-between is-items-center">
                <div class="is-flex is-justify-center is-items-center">
                    <h1 class="h5 text-metodo_pago"><strong>Método de pago del pedido:
                    <asp:Label ID="lbl_numero_pedido" class="is-select-all" runat="server"></asp:Label></strong></h5>
                    <button type="button" class="is-cursor-pointer" style="background-color: transparent; outline: none; border: none;" onclick="copiarNumeroParte('body_lt_numero_pedido', 'Pedido')">
                        <span class="is-text-gray">
                            <svg class="is-w-4 is-h-4" aria-labelledby="Clipcopy" title="Copiar elemento" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 5H6a2 2 0 00-2 2v12a2 2 0 002 2h10a2 2 0 002-2v-1M8 5a2 2 0 002 2h2a2 2 0 002-2M8 5a2 2 0 012-2h2a2 2 0 012 2m0 0h2a2 2 0 012 2v3m2 4H10m0 0l3-3m-3 3l3 3"></path>
                                <title id="Clipcopy">Copiar elemento</title>
                            </svg>
                        </span>
                    </button>
                </div>
                <div class="is-flex container-formas_pago">
                    <p class="text-aceptamos_formas_pago"><strong>Aceptamos:</strong></p>
                    <img title="Formas de pago" class="icono-formas_pago" src="/img/webUI/newdesign/formaspago.jpg" alt="Métodos de pago bancario" />
                </div>
            </div>
            <div class="is-container container-metodo_pago">
                <div class="is-flex is-justify-between">
                    <div>
                        <p><strong>Elige el método de pago:  </strong></p>
                        <asp:UpdatePanel ID="up_pasarelaPago" UpdateMode="Conditional" RenderMode="Block" runat="server">
                            <ContentTemplate>
                                <div class="is-flex is-justify-evenly is-items-start">
                                    <asp:Button ID="btn_tarjeta" class="is-btn-gray is-space-x-9" Text="Tarjeta de crédito/débito" OnClick="btn_tarjeta_Click" UseSubmitBehavior="false" runat="server" />
                                    <asp:Button ID="btn_paypal" class="is-btn-gray is-space-x-9" Text="PayPal" OnClick="btn_paypal_Click" UseSubmitBehavior="false" runat="server" />
                                    <asp:Button ID="btn_transferencia" class="is-btn-gray" OnClick="btn_transferencia_Click" Text="Transferencia o deposito" UseSubmitBehavior="false" runat="server" />
                                </div>
                                <asp:Panel ID="pnl_tarjeta" Visible="false" runat="server">
                                    <iframe id="frm_pagoTarjeta" visible="true" style="width: 100%; height: 680px; border: 0;" runat="server"></iframe>
                                </asp:Panel>
                                <asp:Panel ID="pnl_paypal" Visible="false" runat="server">
                                    <p>Pago con PayPal</p>
                                    <asp:UpdatePanel ID="up_paypal" UpdateMode="Conditional" RenderMode="Block" runat="server">
                                        <ContentTemplate>
                                            <div id="btn_paypal_container" runat="server"></div>
                                            <asp:Panel ID="pnl_noDisponiblePago" Visible="false" runat="server">
                                                <strong>El pago no está disponible por los siguientes motivos: </strong>
                                                <p id="motivosNoDisponiblePago" visible="false" runat="server"></p>
                                            </asp:Panel>
                                            <asp:LinkButton ID="btn_renovarPedidoPayPal" Visible="false" OnClick="btn_renovarPedidoPayPal_Click" runat="server"></asp:LinkButton>
                                            <div id="txt_cargando">Cargando información de pago en PayPal...</div>
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
                                                    <td>Monto pagado en PayPal</td>
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
                                                        <asp:Label ID="lbl_paypal_fecha_actualizacion" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:LinkButton ID="linkActualizarUP" runat="server" OnClick="linkActualizarUP_Click">Actualizar</asp:LinkButton>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
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
                    <div style="border: 1px solid #b7b7b7; border-radius: 8px; width: 420px;">
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
                                    </td style="padding: 1rem;">
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
                </div>
            </div>
        </div>
    </div>

    <style>

    input .is-btn-gray:focus {
        background-color: #06C;
    }

    @media only screen and (min-width: 1200px) {
    .icono-formas_pago {
        width: 450px;
        height: 50px;
        }

    .container-metodo_pago {
        margin-top: 5rem;
        }

    .text-aceptamos_formas_pago {
        margin-right: 1rem;
        margin: 1rem;
    }

    }

    @media only screen and (max-width: 1200px) {

        .container-pago {
            margin-left: 2rem;
        }

        .icono-formas_pago {
            width: 360px;
        }

        .text-metodo_pago {
            font-size: 1rem;
        }

        .text-aceptamos_formas_pago > strong:nth-child(1) {
            font-size: 1rem;
        }

        .icono-formas_pago {
            width: 350px;
            height: 40px;
        }

        .container-metodo_pago {
        margin-top: 0rem;
        }

        .text-aceptamos_formas_pago {
        margin: 0.25rem;
        }

        #body_up_pasarelaPago .is-btn-gray  {
        margin-right: 1rem;
        }

        .container-metodo_pago > div:nth-child(1) > div:nth-child(2) {
        width: 360px;
        margin: 2rem auto 2rem 0;
        }

        .container-pago > div > div {
            flex-direction: column;
            align-items: first baseline;
        }

        .container-metodo_pago > div:nth-child(1) {
        display: flex;
        flex-direction: column-reverse;
        justify-content: center;
        }

        .container-metodo_pago > div:nth-child(1) > div:nth-child(1) {
        margin: auto auto auto 0;
        }

        .container-pago > div > div > div:nth-child(1) {
        margin-bottom: 2rem;
        }
    }

    @media only screen and (min-width:500px) and (max-width: 700px) {

        .container-metodo_pago > div:nth-child(1) > div:nth-child(2) {
            width: 380px;
        }

        .text-resumen {
            font-size: 0.8rem;
        }

        .container-metodo_pago table {
            font-size: 12px;
        }

        .text-aceptamos_formas_pago > strong:nth-child(1) {
            font-size: 0.75rem;
        }

        .text-aceptamos_formas_pago {
            margin: 0.5rem 0.5rem 0.5rem 0rem;
        }

        .is-w-full > div:nth-child(1) {
            margin-bottom: 1rem;
        }

        .container-pago {
            margin-left: 1rem;
        }

        .container-metodo_pago p:nth-child(1) > strong:nth-child(1) {
            font-size: 0.75rem;
        }

        #body_up_pasarelaPago .is-btn-gray {
            height: 26px;
            line-height: 26px;
            font-size: 10px;
        }
    }

    @media only screen and (max-width: 500px) {

        .container-metodo_pago > div:nth-child(1) > div:nth-child(2) {
            width: 300px !important;
        }

        .text-resumen {
            font-size: 0.5rem;
        }

        .icono-formas_pago {
            width: 320px;
            height: 36px;
        }

        .text-metodo_pago {
            font-size: 0.75rem;
        }

        .container-metodo_pago table {
            font-size: 10px;
        }

        .container-formas_pago {
            flex-direction: column;
        }

        .text-aceptamos_formas_pago > strong:nth-child(1) {
            font-size: 10px;
        }

        .container-metodo_pago p:nth-child(1) > strong:nth-child(1) {
            font-size: 10px;
        }

        .text-aceptamos_formas_pago {
            margin: 0.5rem 0.5rem 0.5rem 0rem;
        }

        .container-pago > div > div > div:nth-child(1) {
            margin-bottom: 1rem;
        }

        .container-pago {
            margin-left: 0.5rem;
        }

        #body_up_pasarelaPago .is-btn-gray {
            font-size: 8px;
            height: 26px;
            line-height: 26px;
            padding: 0px 12px !important;
            margin-right: 0.5rem !important;
        }
    }
    </style>
</asp:Content>
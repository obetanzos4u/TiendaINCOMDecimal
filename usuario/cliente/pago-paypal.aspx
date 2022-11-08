<%@ Page Language="C#" AutoEventWireup="true" Async="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="pago-paypal.aspx.cs" 
    Inherits="usuario_cliente_pago_paypal" %>
<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="header" TagName="menuGeneral" %>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <header:menuGeneral ID="menuGeneral" runat="server" />
        <asp:HiddenField ID="hf_id_pedido"   runat="server" />
        <asp:HiddenField ID="hf_numero_operacion" runat="server" />
 
    <div class="container ">
        <div class="row">
            <div class="col">
                <h1 class="h2">Pago vía PayPal</h1>
               <p  class="h4">"<asp:Literal ID="lt_nombre_pedido" runat="server"></asp:Literal>" 
                #<asp:Literal ID="lt_numero_operacion" runat="server"></asp:Literal>
                   </p>
            </div>
        </div>
        <div class="row">
            <div class="col col-12 col-xs-12 col-sm-12 col-md-5 col-xl-5">
                <p class="h3 ">
                 Desglose
                </p>
                <table class="table table-sm">
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
                </table>
                  <div class="d-grid gap-2 mt-3">
                      <asp:HyperLink ID="link_regresar_resumen"
                          class="btn btn-success" runat="server">Regresar al pedido </asp:HyperLink>
                  </div>
            </div>
            <div class="col col-12 col-xs-12 col-sm-12 col-md-7 col-xl-7">
                <asp:UpdatePanel ID="up_estatus_paypal" UpdateMode="Conditional" RenderMode="Block" runat="server" class="col col-12">
                    <ContentTemplate>

                        <h2>Información de pago</h2>
                        <div id="paypal_button_container" class="paypal_button_container" runat="server"></div>
                        <asp:Panel ID="lbl_NoDisponiblePago" class="alert alert-warning" Visible="false" runat="server">
                            <strong>El pago no esta disponible por los siguientes motivos:
                            </strong>
                            <p id="motivosNoDisponiblePago" visible="false" runat="server"></p>
                        </asp:Panel>
                        <asp:LinkButton ID="btn_renovarPedido" CssClass="btn btn-primary " Visible="false" OnClientClick="btnLoading(this);" OnClick="btn_renovarPedido_Click" runat="server">Renovar pedido</asp:LinkButton>


                        <div id="texto_cargando_informacion" class=" d-none"><strong>Cargando información de pago...</div>

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
        </div>
    </div>
</asp:Content>



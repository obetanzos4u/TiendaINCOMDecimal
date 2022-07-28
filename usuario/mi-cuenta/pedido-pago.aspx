<%@ Page Language="C#" AutoEventWireup="true"   Async="true" MaintainScrollPositionOnPostback="true" CodeFile="pedido-pago.aspx.cs"
    MasterPageFile="~/usuario/masterPages/clienteCotizacion.master" Inherits="usuario_pedidoPago" %>
<%@ Import Namespace="System.Globalization" %>


<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">

    <asp:HiddenField ID="hf_id_operacion" runat="server" />
    <asp:HiddenField ID="hf_numero_operacion" runat="server" />
 
    
     

    <div class="row">
        <div class="col s12 m6">
            <h1 class="margin-b-2x">Pago de 
                   "<asp:Literal ID="lt_nombre_pedido" runat="server"></asp:Literal>"
            </h1>

            <label>Número de operacion</label>
            <h2 class="margin-t-2x margin-b-2x">
                <asp:Literal ID="lt_numero_operacion" runat="server"></asp:Literal>

            </h2>
            <asp:Label ID="lbl_moneda" CssClass="orange-text" runat="server"></asp:Label>
            <br />
            <asp:HyperLink ID="hl_editarDatos" CssClass="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 "
                runat="server"><i class="material-icons left">edit</i>Regresar a Contacto, envío y facturación</asp:HyperLink>

            &nbsp;
            <asp:HyperLink ID="hl_editarProductos" CssClass="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 " 
                      runat="server"><i class="material-icons left">edit</i>Editar productos</asp:HyperLink>
        </div>
        <div class="col s12 m6">

            <br />
            <asp:HyperLink ID="link_enviar" CssClass="waves-effect waves-light btn-large blue" runat="server">
                         Enviar operación a mi correo.<i class="material-icons left">email</i>
            </asp:HyperLink>

        </div>
    </div>
    <div class="row">

        <div class="col s12 m5">
            <h2>Resumen</h2>
            <p>Desglose del pedido realizo en Incom.mx</p>
            <table>
                <tr>
                    <td>Envío (<asp:Label ID="lbl_metodoEnvio" runat="server"></asp:Label>):</td>
                    <td><strong>
                        <asp:Label ID="lbl_envio" runat="server"></asp:Label></strong></td>
                </tr>
                <tr id="content_descuento_subtotal"  runat="server" visible="false">
                    <td>Sub Total</td>
                    <td><strong>
                        <asp:Label ID="lbl_subtotalSinDescuento" runat="server"></asp:Label></strong></td>
                </tr>
                <tr id="content_descuento"  runat="server" visible="false">
                    <td>Descuento aplicado: </td>
                    <td><strong>
                        <asp:Label ID="lbl_descuento_porcentaje" runat="server"></asp:Label>%</strong></td>
                </tr>
                <tr>
                    <td>Sub Total antes de IVA: </td>
                    <td><strong>
                        <asp:Label ID="lbl_subTotal" runat="server"></asp:Label></strong>

                    </td>
                </tr>
                <tr>
                    <td>Impuestos</td>
                    <td>
                        <asp:Label ID="lbl_impuestos" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>Total</td>
                    <td>
                        <asp:Label ID="lbl_total" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
            </table>

        </div>
        <asp:UpdatePanel ID="up_estatus_paypal" UpdateMode="Conditional" RenderMode="Block" runat="server" class="col s12 m6">
        <ContentTemplate>
            <img src="/img/webUI/logo_paypal_min.jpg" />
            <h2>Información de pago PayPal</h2>

                <div id="paypal_button_container"  class="paypal_button_container" runat="server"  ></div>
            <asp:Label ID="lbl_NoDisponiblePago"  CssClass="red-text text-darken-1" visible="false"  runat="server">
                <strong>El pago no esta disponible por los siguientes motivos:
                    </strong></asp:Label>
            <p id="motivosNoDisponiblePago" class="margin-t-2x" visible="false" runat="server"></p>

            <asp:LinkButton ID="btn_renovarPedido" CssClass="btn green" Visible="false" OnClientClick="btnLoading(this);"  OnClick="btn_renovarPedido_Click" runat="server">Renovar pedido</asp:LinkButton>

            <p>Desglose del pago en PayPal. <br /><asp:HyperLink ID="link_paypal_estado" runat="server">Consultar directamente en PayPal.com</asp:HyperLink></p>
            <div id="texto_cargando_informacion" class="light-blue-text text-darken-3 hide"><strong>Cargando información de pago...</div>
        <div id="barra_cargando" class="progress light-blue lighten-4 hide">
                <div class="indeterminate  light-blue darken-3"></div>
            </div>

            <table id="dt_desglose_paypal" runat="server">
                
                <tr>
                    <td>Tipo de intento</td>
                    <td><strong>
                        <asp:Label ID="lbl_paypal_intento" runat="server"></asp:Label>

                    </strong>

                    </td>
                </tr>

                   <tr>
                    <td>Estado del pago</td>
                    <td><strong>
                        <asp:Label ID="lbl_paypal_estado" runat="server"></asp:Label>

                    </strong>

                    </td>
                   </tr>
                <tr>
                    <td>Monto Pagado en PayPal</td>
                    <td><strong>
                        <asp:Label ID="lbl_paypal_monto" runat="server"></asp:Label>
                        <asp:Label ID="lbl_paypal_moneda" runat="server"></asp:Label>
                    </strong>

                    </td>
                </tr>
                 <tr>
                    <td>Fecha del primer intento</td>
                    <td><strong>
                        <asp:Label ID="lbl_paypal_fecha_primerIntento" runat="server"></asp:Label>

                    </strong>

                    </td>
                   </tr>

                 <tr>
                     <td>Última actualización</td>
                     <td><strong>
                         <asp:Label ID="lbl_paypal_fecha_actualización" runat="server"></asp:Label>

                     </strong>

                     </td>
                 </tr>
            </table>

            <asp:LinkButton ID="linkActualizarUP" CssClass="hide" runat="server" OnClick="linkActualizarUP_Click">Actualizar</asp:LinkButton>
            
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="linkActualizarUP" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>

         <div class="col s12 m12 center-align">
        <h2>O si lo prefieres paga con (recomendado): </h2>
        <asp:HyperLink ID="link_pago_santander" runat="server"> 
            <img src="/img/webUI/3d-secure-logo.jpg" /></asp:HyperLink></div>
    </div>

</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pedido-pago-santander.aspx.cs" Inherits="usuario_mi_cuenta_pedido_pago_santander"
        MasterPageFile="~/usuario/masterPages/clienteCotizacion.master"  %>
  <asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <asp:HiddenField ID="hf_id_operacion" runat="server" />
    <asp:HiddenField ID="hf_numero_operacion" runat="server" />

    <div class="row">
        <div class="col s12 m6">
            <h1 class="margin-b-2x">Pago del pedido
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
                <asp:UpdatePanel ID="up_estatus_Santander" UpdateMode="Conditional" RenderMode="Block" runat="server" class="col s12 m6">
            <ContentTemplate>
               
                <h2>Información de pago</h2>

                  <asp:Label ID="lbl_NoDisponiblePago"  CssClass="red-text text-darken-1" visible="false"  runat="server">
                <strong>El pago no esta disponible por los siguientes motivos:
                    </strong></asp:Label>
            <p id="motivosNoDisponiblePago" class="margin-t-2x" visible="false" runat="server"></p>

            <asp:LinkButton ID="btn_renovarPedido" CssClass="btn green" Visible="false" OnClientClick="btnLoading(this);"  OnClick="btn_renovarPedido_Click" runat="server">Renovar pedido</asp:LinkButton>

                 <div class="col s12 margin-b-4x">
                            <label>
                                <asp:CheckBox ID="chkEnvioEnTienda" OnCheckedChanged="chkEnvioEnTienda_CheckedChanged" AutoPostBack="true"  ClientIDMode="Static" runat="server" />
                                <span>Prefiero recoger el producto en Tienda.</span>

                            </label>
                        </div>
                <iframe  id="FramePago" visible="false" style="    width: 100%;
    height: 680px;
    border: 0;"  runat="server"></iframe>

            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="col s12 m4">
            <h2>Resumen</h2>
            <p>Desglose del pedido realizo en Incom.mx</p>
            <table>
                <tr>
                    <td>Envío (<asp:Label ID="lbl_metodoEnvio" runat="server"></asp:Label>):</td>
                    <td><strong>
                        <asp:Label ID="lbl_envio" runat="server"></asp:Label></strong></td>
                </tr>
                <tr id="content_descuento_subtotal" runat="server" visible="false">
                    <td>Sub Total</td>
                    <td><strong>
                        <asp:Label ID="lbl_subtotalSinDescuento" runat="server"></asp:Label></strong></td>
                </tr>
                <tr id="content_descuento" runat="server" visible="false">
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

        <div class="col s12 m12 center-align">
        <h2>O si lo prefieres paga con: </h2>
        <asp:HyperLink ID="link_pago_paypal" runat="server"> <img src="/img/webUI/logo_paypal_min.jpg" /></asp:HyperLink></div>
    </div>
      </asp:Content>
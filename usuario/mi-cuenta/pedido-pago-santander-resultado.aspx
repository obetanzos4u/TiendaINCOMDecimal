<%@ Page Title="" Language="C#" MasterPageFile="~/basic.master" AutoEventWireup="true" CodeFile="pedido-pago-santander-resultado.aspx.cs" Inherits="usuario_mi_cuenta_pedido_pago_santander_resultado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="top" runat="Server">
    <asp:Panel ID="content_msgError" class="container" Visible="false" runat="server">
        <h2 id="msgError" class="center-align" runat="server"></h2>
        <div style="height: 96px; overflow-y: scroll">
            <p id="detallesError" runat="server"></p>
        </div>
    </asp:Panel>
    <div class="container">
        <div id="Content_Confirmacion" class="row" visible="false" runat="server">
            <h1 class="center-align">¡Gracias por comprar en INCOM!</h1>
            <h2 class="center-align center-align margin-b-2x">El cobro se ha efectuado correctamente y se te mandará un correo de confirmación</h2>
            <div>
                <table>
                    <tr>
                        <td><strong>Referencia:</strong></td>
                        <td><strong>Importe: </strong></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_referencia" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_importe" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <h3 class="center-align">En breve serás redirigido</h3>
        </div>
    </div>
    <%--<script>
        document.addEventListener("DOMContentLoaded", function (event) {
            // window.history.replaceState(null, null, window.location.pathname);
        });
    </script>--%>
</asp:Content>

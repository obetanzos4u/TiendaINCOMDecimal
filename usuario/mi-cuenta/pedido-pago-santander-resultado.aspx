<%@ Page Title="" Language="C#" MasterPageFile="~/basic.master" AutoEventWireup="true" CodeFile="pedido-pago-santander-resultado.aspx.cs" Inherits="usuario_mi_cuenta_pedido_pago_santander_resultado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="top" Runat="Server">
     <asp:Panel ID="content_msgError" class="container" Visible="false" runat="server">

        <h2 id="msgError" class="center-align" runat="server"> </h2>
         <p id="detallesError" runat="server"></p>
    </asp:Panel>
      <div class="container">
    <div id="Content_Confirmacion" class="row" visible="false" runat="server">
        <h1 class="center-align">¡Gracias por tu compra!</h1>

          <h1 class="center-align center-align margin-b-2x">En breve será procesado tu pedido y un asesor se contactará contigo.</h1>
        <h2 class="center-align center-align margin-t-2x">Se ha enviado un email con tu comprobante de pago.</h2>
        <div class="col ">
            <table>
             <tr>
            <td><strong>Referencia:</strong> <asp:Label ID="lbl_referencia" runat="server" ></asp:Label></td>
            <td><strong>Importe: </strong><asp:Label ID="lbl_importe" runat="server" ></asp:Label></td>
           
            </table>
        </div>
    </div>
</div>
   <script>
  document.addEventListener("DOMContentLoaded", function(event) {
     // window.history.replaceState(null, null, window.location.pathname);
  });
   </script>
</asp:Content>


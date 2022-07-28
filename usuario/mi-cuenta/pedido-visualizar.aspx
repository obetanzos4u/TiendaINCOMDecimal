<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pedido-visualizar.aspx.cs" Async="true" Inherits="usuario_mi_cuenta_pedido_visualizar" %>
<%@ Register TagPrefix="uc_1" TagName="operacionMensaje" Src="~/userControls/operaciones/uc_mensajeOperacion.ascx" %>
<%@ Register TagPrefix="uc_1" TagName="operacion" Src="~/userControls/operaciones/uc_pedido_visualizar.ascx" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Pedido</title>
     <link type="text/css" rel="stylesheet" href="<%= ResolveUrl("/css/incomDev.css") %>" media="screen,projection" />
     <style>
        /* INICIO - animación de cargando */
        .lds-ellipsis {
  display: inline-block;
  position: relative;
  width: 64px;
  height: 38px;
}
.lds-ellipsis div {
  position: absolute;
  top: 27px;
  width: 11px;
  height: 11px;
  border-radius: 50%;
  background: #cef;
  animation-timing-function: cubic-bezier(0, 1, 1, 0);
}
.lds-ellipsis div:nth-child(1) {
  left: 6px;
  animation: lds-ellipsis1 0.6s infinite;
}
.lds-ellipsis div:nth-child(2) {
  left: 6px;
  animation: lds-ellipsis2 0.6s infinite;
}
.lds-ellipsis div:nth-child(3) {
  left: 26px;
  animation: lds-ellipsis2 0.6s infinite;
}
.lds-ellipsis div:nth-child(4) {
  left: 45px;
  animation: lds-ellipsis3 0.6s infinite;
}
@keyframes lds-ellipsis1 {
  0% {
    transform: scale(0);
  }
  100% {
    transform: scale(1);
  }
}
@keyframes lds-ellipsis3 {
  0% {
    transform: scale(1);
  }
  100% {
    transform: scale(0);
  }
}
@keyframes lds-ellipsis2 {
  0% {
    transform: translate(0, 0);
  }
  100% {
    transform: translate(19px, 0);
  }
}
        /* FIN - animación de cargando */
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="content_enviarEmail" class="container" style="position: fixed; top: 0px; width: 98%">
                 <asp:Label ID="lbl_mensaje" runat="server"></asp:Label>
               <label> Email Destinatario(s):</label>
         <asp:TextBox ID="txt_destinatarios" CssClass="txt" placeholder="Multiples destinatarios separados por coma" Style="width: 50%" runat="server"></asp:TextBox>
                <asp:Button CssClass="btn" ID="btn_enviarEmail"  OnClientClick="btnLoading(this);" OnClick="btn_enviarEmail_Click" runat="server" Text="Enviar" />
              &nbsp; 
              <asp:HyperLink ID="link_pago" 
                         CssClass="btn" runat="server">
                          Pagar
                      </asp:HyperLink>
                  &nbsp; <a class="btn bg-grey" href="/usuario/mi-cuenta/pedidos">Regresar a Pedidos</a><br />

                               

              <uc_1:operacionMensaje ID="operacionMensaje" runat="server" />
            </div>
           

        <div id="content_operacion">
            <uc_1:operacion ID="operacion" runat="server" />
        </div>


        <script>

            $(document).ready(function () {
                var alto = $("#content_enviarEmail").height()

                $("#content_operacion").css({ 'margin-top': alto + 'px' });
            });

              // Función que inserta la barra de carga en el mismo nivel del objeto que llama ocultando este
            function btnLoading(btn) {

                btn.style.display = "none";
                var padre = btn.parentNode;
                var progress = document.createElement("div");
                progress.classList.add("lds-ellipsis");
                progress.innerHTML = `<div></div><div></div><div></div><div></div>`;
                padre.insertBefore(progress, btn);
            }
        </script>


    </form>
    
    <style type="text/css">
      
    </style>
</body>

</html>

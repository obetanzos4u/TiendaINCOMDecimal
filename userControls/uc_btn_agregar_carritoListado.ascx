<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_btn_agregar_carritoListado.ascx.cs" Inherits="uc_btn_agregar_carritoListado" %>

<asp:UpdatePanel ID="UP_cantidadCarrito" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hf_numero_parte" runat="server" />
        <!-- <div class="btn_carrito_left">
            <a href="#" onclick="event.preventDefault(); calculoTxtCarrito('resta', '<%=txt_cantidadCarrito.ClientID %>');  " class="btn-cantidad-carrito btn-small btn-small-s  blue-grey darken-1"><i class="material-icons">remove</i></a>
        </div>
        <div class="txt_center">
            <asp:TextBox ID="txt_cantidadCarrito" Style="text-align: center; height: 1.5rem; width: -webkit-fill-available; padding: 3px;" class="inline" Text="1"
                runat="server">
            </asp:TextBox>
        </div>
        <div class="btn_carrito_right">
            <a href="#" onclick="event.preventDefault(); calculoTxtCarrito('suma', '<%=txt_cantidadCarrito.ClientID %>'); " class="btn-cantidad-carrito  btn-small  btn-small-s blue-grey darken-1"><i class="material-icons">add</i></a>
        </div> -->
        <%-- OnClientClick="btnLoading(this);" --%>
        <asp:LinkButton ID="btn_agregar_productoCarrito" runat="server"
            class="waves-effect waves-light is-btn-green btn-full-text is-m-auto" OnClick="btn_agregar_productoCarrito_Click" ToolTip="Agregar al carrito">
            <img class="carrito-white_btn" alt="Botón para añadir a carrito" src="https://www.incom.mx/img/webUI/newdesign/Carrito-white.svg">
            Agregar al carrito
        </asp:LinkButton>
        <asp:LinkButton ID="agregar_productoCarrito_logoOut" runat="server" Visible="false" style="color: #ffffff!important;" ToolTip="Inicia sesión para agregar al carrito" class="waves-effect waves-light is-btn-green btn-full-text is-m-auto">
            <img class="carrito-white_btn" alt="Botón para añadir a carrito" src="https://www.incom.mx/img/webUI/newdesign/Carrito-white.svg">
            Agregar al carrito
        </asp:LinkButton>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btn_agregar_productoCarrito" />
        <%--<asp:AsyncPostBackTrigger ControlID="btn_agregar_productoCarrito" EventName="Click" />--%>
    </Triggers>
</asp:UpdatePanel>

<script>
    // Script para manejar las cajas de texto se ejecute un comando al presionar enter
    var input = document.querySelector("#<%= txt_cantidadCarrito.ClientID %>");
    var btn = document.querySelector("#<%= btn_agregar_productoCarrito.ClientID %>");
    establecerEnter(input, btn);

    function resizeInput() {
        $(this).attr('size', $(this).val().length);
    }

    $('input[type="text"]')
        .keyup(resizeInput)
        .each(resizeInput);
</script>
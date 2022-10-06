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
        <asp:LinkButton ID="btn_agregar_productoCarrito" OnClientClick="btnLoading(this);" runat="server"
            class="  waves-effect waves-light btn-1  btn-full-text  btn-full-text" OnClick="btn_agregar_productoCarrito_Click">
            <img class="carrito-white_btn" alt="Botón para añadir a carrito" src="../../img/webUI/newdesign/Carrito-white.svg"> 
            Agregar al carrito
        </asp:LinkButton>
        <a id="agregar_productoCarrito_logoOut" runat="server" visible="false" class="waves-effect-1 waves-light btn-1">
            <img class="carrito-white_btn" alt="Botón para añadir a carrito" src="../../img/webUI/newdesign/Carrito-white.svg"> 
            Agregar al carrito
        </a>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_agregar_productoCarrito" EventName="Click" />
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


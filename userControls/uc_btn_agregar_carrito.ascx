<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_btn_agregar_carrito.ascx.cs" Inherits="uc_btn_agregar_carrito" %>

 <span style="font-size: 18px; font-weight: 500; color: #4caf50;">Cantidad</span>
<asp:UpdatePanel ID="UP_cantidadCarrito" style="overflow: hidden;"  runat="server">
    <ContentTemplate>
        <div style="width: 250px;">
<%--            <div class="btn_carrito_left">
                <a href="#" onclick="event.preventDefault(); calculoTxtCarrito('resta', '<%=txt_cantidadCarrito.ClientID %>');  "
                    class="btn-cantidad-carrito btn-small btn-small-s blue-grey darken-1"><i class="material-icons">remove</i></a>
            </div>--%>
            <div class="txt_center">

                <asp:TextBox ID="txt_cantidadCarrito" Style="text-align: center; height: 1.5rem; width: -webkit-fill-available; padding: 3px; border-radius: 12px;" class="inline txt_cantidadCarrito" Text="1"
                    runat="server"></asp:TextBox>

            </div>
<%--            <div class="btn_carrito_right">
                <a href="#" onclick="event.preventDefault(); calculoTxtCarrito('suma', '<%=txt_cantidadCarrito.ClientID %>'); "
                    class="btn-cantidad-carrito  btn-small btn-small-s blue-grey darken-1"><i class="material-icons">add</i></a>
            </div>--%>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="up_btn_add_carrito" style="display: inline;" runat="server">
    <ContentTemplate>

        <asp:LinkButton ID="btn_agregar_productoCarrito" ClientIDMode="Static" OnClientClick="btnLoading(this);" runat="server"
            class="waves-effect waves-light btn  blue" OnClick="btn_agregar_productoCarrito_Click">
            <i class="material-icons left">add_shopping_cart</i>Agregar a carrito
        </asp:LinkButton>
        <a id="agregar_productoCarrito_logoOut" runat="server" visible="false" class="waves-effect waves-light btn  blue">
            <i class="material-icons left">add_shopping_cart</i>Agregar a carrito
        </a>
    </ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="btn_agregar_productoCarrito" EventName="Click" /></Triggers>
</asp:UpdatePanel>

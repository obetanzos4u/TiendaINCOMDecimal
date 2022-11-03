<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_btn_agregar_carrito.ascx.cs" Inherits="uc_btn_agregar_carrito" %>

 <span class="cantidad-label" style="font-weight: 400; color: #000000;">Cantidad:</span>
<asp:UpdatePanel ID="UP_cantidadCarrito" style="overflow: hidden;"  runat="server">
    <ContentTemplate>
        <div class="counter_carrito">
<%--            <div class="btn_carrito_left">
                <a href="#" onclick="event.preventDefault(); calculoTxtCarrito('resta', '<%=txt_cantidadCarrito.ClientID %>');  "
                    class="btn-cantidad-carrito btn-small btn-small-s blue-grey darken-1"><i class="material-icons">remove</i></a>
            </div>--%>
            <div class="txt_center">

                <asp:TextBox ID="txt_cantidadCarrito" Style="text-align: center; font-size: 14px; height:1.25rem; line-height: 1.25rem; width: -webkit-fill-available; padding: 3px; border-radius: 12px; margin-bottom: 1rem;" Text="1"
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
        <asp:LinkButton ID="btn_agregar_productoCarrito" ClientIDMode="Static" runat="server"
            class="agregar_carrito waves-effect-1 waves-light btn" OnClick="btn_agregar_productoCarrito_Click" style="background: #108A00 !important; border-radius: 6px; text-transform: none; font-weight: 600;">
            Agregar al carrito
        </asp:LinkButton>
        <a id="agregar_productoCarrito_logoOut" runat="server" visible="false" class="agregar_carrito agregar_carrito-out waves-effect-1 waves-light btn-2" style="background: #108A00 !important; border-radius: 6px; text-transform: none; font-weight: 600;">
            Agregar al carrito
        </a>
    </ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="btn_agregar_productoCarrito" EventName="Click" /></Triggers>
</asp:UpdatePanel>

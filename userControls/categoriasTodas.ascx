<%@ Control Language="C#" AutoEventWireup="true" CodeFile="categoriasTodas.ascx.cs" Inherits="userControls_categoriaTodas" %>

<asp:ListView ID="lv_categoriasTodas" OnItemDataBound="lv_categoriasTodas_OnItemDataBound" runat="server">
    <LayoutTemplate>
        <div runat="server" id="itemPlaceholder"></div>
    </LayoutTemplate>
    <ItemTemplate>
        <%--<div id="item_categoria" runat="server" class="col s12 m6 l3 xl2 center animated fadeIn">--%>
        <div id="item_categoria" runat="server" class="this_categoria">
            <asp:HyperLink ID="link_cat" runat="server">
                <%--<asp:Image ID="img_categoria" class="responsive-img IncomWebpToJpg hoverable"  loading=lazy runat="server" />--%>
                <asp:Image ID="img_categoria" class="img1-item_categoria IncomWebpToJpg hoverable" loading="lazy" runat="server" />
                <%-- <asp:Image ID="Image1" class="responsive-img IncomWebpToJpg hoverable"  loading=lazy runat="server" />--%>
                <asp:Image ID="Image1" class="img2-item_categoria IncomWebpToJpg hoverable" loading="lazy" runat="server" />
                <h2 class="text-this_categria" style="text-align: center !important;"><%# Eval("nombre") %></h2>
            </asp:HyperLink>
        </div>
    </ItemTemplate>
</asp:ListView>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="productosRelacionados.ascx.cs" Inherits="uc_productosRelacionados" %>

<div class=" ">
    <h2 id="encabezado" runat="server" class="margin-t-8x">Productos relacionados</h2>
</div>
<asp:ListView ID="lv_productosRelacionados" OnItemDataBound="lv_productosRelacionados_OnItemDataBound" runat="server">
    <LayoutTemplate>
        <div>
            <div runat="server" id="itemPlaceholder"></div>
        </div>
    </LayoutTemplate>
    <ItemTemplate>
        <div class="col s12 m6 l4 xl2">
            <div class="card">
                <div class="card-image">
                    <asp:HyperLink ID="link_producto" runat="server" CssClass="hoverLinkTituloProducto">
                        <asp:Image ID="img_producto" class="responsive-img" runat="server" />
                        <h2 class="tituloProductoTienda " style="margin: 0px 5px;">
                            <%#Eval("numero_parte") %> - <%#Eval("titulo") %> 
                        </h2>
                    </asp:HyperLink>
                </div>
                <!--  <div class="card-content">
                        <asp:LinkButton id="btn_link" runat="server"></asp:LinkButton> 
                  </div> -->
            </div>
        </div>
    </ItemTemplate>

    <EmptyDataTemplate>
    </EmptyDataTemplate>

</asp:ListView>

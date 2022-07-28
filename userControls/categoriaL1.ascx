<%@ Control Language="C#" AutoEventWireup="true"  CodeFile="categoriaL1.ascx.cs" Inherits="userControls_categoriaL1" %>


<asp:ListView ID="lv_categoriasHijas" OnItemDataBound="lv_categoriasHijas_OnItemDataBound" runat="server">
    <LayoutTemplate>
        <div runat="server" id="itemPlaceholder"></div>
    </LayoutTemplate>
    <ItemTemplate>
        <div id="item_categoria" runat="server" class="col s12 m6 l3 xl3 center animated fadeIn">
            <asp:HyperLink ID="link_cat" runat="server"> 
             <asp:Image ID="img_categoria" class="responsive-img IncomWebpToJpg hoverable tooltipped" data-position="bottom" data-tooltip='<%# Eval("descripcion") %>'
                 loading=lazy runat="server" />
                
               <h2 class="truncate tooltipped " data-position="bottom" data-tooltip='<%# Eval("nombre") %>'
                   style="text-align:center !important; color: rgba(0, 0, 0, 0.87); 
    font-size:1.3rem; margin: 0.77rem 0 1.724rem 0;"><%# Eval("nombre") %></h2>
            </asp:HyperLink>

        </div>

    </ItemTemplate>
</asp:ListView>

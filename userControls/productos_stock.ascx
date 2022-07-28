<%@ Control Language="C#" AutoEventWireup="true" CodeFile="productos_stock.ascx.cs" Inherits="uc_productos_stock" %>


<h2 id="titulo" visible="false" class="margin-t-8x" runat="server">Disponibilidad</h2>

<asp:ListView ID="lv_producto_stock" OnItemDataBound="lv_producto_stock_OnItemDataBound" runat="server">
    <LayoutTemplate>
        <table>
            <th>Cantidad</th>
            <th>Ubicación</th>
            <th>Fecha de actualización</th>
            <div runat="server" id="itemPlaceholder"></div>
        </table>
    </LayoutTemplate>
    <ItemTemplate>
        <tr>
            <td> <%#Eval("cantidad") %> <%#Eval("unidad") %> </td>
            <td> <span class="tooltipped" data-position="top" data-delay="50" data-tooltip="<%#Eval("ubicación_pre") %>"><%#Eval("ubicación") %></span> </td>
            <td>
                <asp:Label ID="lbl_fecha_actualización" runat="server"></asp:Label>
            </td>
        </tr>
    </ItemTemplate>

    <EmptyDataTemplate>
    </EmptyDataTemplate>

</asp:ListView>

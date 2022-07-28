<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/bootstrap/basic.master" CodeFile="carrito.aspx.cs" Inherits="usuario_cliente_basic" %>
<%@ Register Src="~/usuario/cliente/cliente-header.ascx" TagPrefix="header" TagName="menuGeneral" %>

<asp:Content runat="server" ContentPlaceHolderID="body">
    <header:menuGeneral ID="menuGeneral"  runat="server"/>


    <div class="container-fluid">
        <div class="row">
            <div class="col">
                <h1 class="h2">Carrito de productos</h1>
            </div>

        </div>
                <div class="row">
            <div class="col">
                <asp:LinkButton class="btn btn-secondary" runat="server">Cotizar</asp:LinkButton>
                <asp:LinkButton   class="btn btn-primary" runat="server">Comprar</asp:LinkButton>
            </div></div>
        <asp:ListView ID="lv_carrito" OnItemDataBound="lv_carrito_ItemDataBound" runat="server">

            <LayoutTemplate><div class="table-responsive-md">
                <table class="table">
                    <thead>
                        <tr>
                            <th colspan="2">Producto</th>

                            <th>Precio&nbsp;Unitario</th>
                            <th>Cantidad</th>
                            <th colspan="2">Total</th>
                        </tr>
                    </thead>
                    <tbody>

                        <div id="itemPlaceholder" runat="server"></div>
                    </tbody>
                </table></div>
            </LayoutTemplate>

            <ItemTemplate>
                <tr>
                    <td style="width: 250px">
                        <asp:Image ID="img_carrito_producto" class="img-fluid img-thumbnail" runat="server" />
                    </td>
                    <td>
                        <strong><%# Eval("numero_parte") %></strong>  -  <%# Eval("descripcion") %>
                        <br />
                        <asp:LinkButton ID="btn_eliminar" class="btn btn-danger" runat="server">Eliminar</asp:LinkButton>
                    </td>
                    <td><%# Eval("precio_unitario") %> </div>

                    </td>
                    <td>
                        <asp:TextBox ID="txt_cantidad" class="form-control" Style="width: 100px;" Text='<%# Eval("cantidad") %>' runat="server"></asp:TextBox>

                    </td>
                    <td>
                        <%# Eval("precio_total") %>

                    </td>
                </tr>
            </ItemTemplate>
            <EmptyDataTemplate></EmptyDataTemplate>
        </asp:ListView>
    </div>
</asp:Content>


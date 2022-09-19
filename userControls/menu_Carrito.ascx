<%@ Control Language="C#" AutoEventWireup="true" CodeFile="menu_Carrito.ascx.cs" Inherits="menuCarrito" %>



<asp:UpdatePanel ID="upTotalProductos" RenderMode="Inline" UpdateMode="Conditional" style="line-height: initial;" runat="server">
    <ContentTemplate>
        <asp:HyperLink ID="btn_toggle_desgloseCarrito" NavigateUrl="/mi-carrito.aspx" ClientIDMode="Static" title='Carrito de productos'
            class="hide-on-med-and-down btn_toggle_desgloseCarrito btn_desgloseCarrito_style" runat="server">
            <img class="icon_carrito" src="../img/webUI/newdesign/Carrito.svg"/>
        </asp:HyperLink>
        <asp:Button ID="buttonDesglose" ClientIDMode="Static" runat="server" CssClass="hide"
            OnClick="carritoDesglose" />
        <asp:Button ID="btnTotalProductosCarrito" ClientIDMode="Static" runat="server" CssClass="hide" Text="2"
            OnClick="cargarMenu" />
    </ContentTemplate>
    <Triggers>

        <asp:AsyncPostBackTrigger ControlID="btnTotalProductosCarrito" EventName="Click" />

    </Triggers>
</asp:UpdatePanel>



<div id="content_desgloseCarrito" class="desgloseCarrito" style="display: none;" runat="server">
    <div style="text-align: right; height: 45px;">
        <a href="/mi-carrito.aspx" style="position: absolute; left: 19px; width: 250px;" class="waves-effect waves-light  btn btn-s green  blue-grey-text text-lighten-5">Ir a Carrito</a>
        <a id="btn_cerrar_desgloseCarrito" class="btn_toggle_desgloseCarrito btn btn-s grey darken-4" style="position: absolute; right: 19px;" href="#">Cerrar</a>
    </div>
    <asp:UpdatePanel ID="up_desgloseCarrito" RenderMode="Block" UpdateMode="Conditional" style="line-height: initial;" runat="server">
        <ContentTemplate>
            <asp:DropDownList ID="ddl_moneda" class="browser-default" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddl_moneda_SelectedIndexChanged" runat="server">
                <asp:ListItem Selected="True" Value="MXN" Text="MXN"></asp:ListItem>
                <asp:ListItem Value="USD" Text="USD"></asp:ListItem>
            </asp:DropDownList>

            <div id="desgloseCarrito" runat="server"></div>
            <div style="text-align: right; line-height: 34px;">
                Subtotal: 
                        <asp:Label ID="lbl_subtotal" class="desgloseCantidades" runat="server" Text=""></asp:Label>
            </div>
            <div style="text-align: right; line-height: 34px;">
                Impuestos: 
                        <asp:Label ID="lbl_impuestos" class="desgloseCantidades" runat="server" Text=""></asp:Label>
            </div>
            <div style="text-align: right; line-height: 34px;">
                Total: 
                        <asp:Label ID="lbl_total" class="desgloseCantidades" runat="server" Text=""></asp:Label>
            </div>
            <div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddl_moneda" EventName="SelectedIndexChanged" />

            <asp:AsyncPostBackTrigger ControlID="buttonDesglose" EventName="Click" />

        </Triggers>
    </asp:UpdatePanel>
</div>



<style>
    .icon_shop {
        display: flex;
        flex-direction: column;
        /*border: 1px solid green;*/
    }

    .btn_desgloseCarrito_style {
        font-size: 1rem;
        color: #0f0f0f;
        display: block;
        padding: 3px 15px;
        background: #ffffff;
        cursor: pointer;
        border-radius: 13px;
        font-weight: 700;
        display: inline-flex;
        flex-direction: row;
        justify-content: flex-end;
    }

    .desgloseCantidades {
        color: #000000;
        font-weight: 600;
    }

    .desgloseCarrito {
        z-index: 99999;
        position: fixed;
        right: 65px;
        width: 500px;
        top: 415px;
        background: #fff;
        color: black;
        line-height: initial;
        top: 56px;
        box-shadow: rgba(0, 0, 0, 0.29) 0px 0px 4px 0px;
        padding: 15px 12px;
    }

        .desgloseCarrito td, th {
            line-height: 22px;
            display: table-cell;
            text-align: left;
            vertical-align: middle;
            border-radius: 2px;
        }

        .icon_carrito {
            width: 1.75rem;
        }
</style>

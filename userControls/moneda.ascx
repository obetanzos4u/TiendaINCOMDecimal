<%@ Control Language="C#" AutoEventWireup="true"   CodeFile="moneda.ascx.cs" Inherits="tienda.uc_moneda" %>

<asp:DropDownList ID="ddl_moneda" AutoPostBack="true"  OnSelectedIndexChanged="ddl_moneda_SelectedIndexChanged" runat="server">
    <asp:ListItem Selected="True" Value="MXN" Text="MXN"></asp:ListItem>
    <asp:ListItem Value="USD" Text="USD"></asp:ListItem>
</asp:DropDownList>
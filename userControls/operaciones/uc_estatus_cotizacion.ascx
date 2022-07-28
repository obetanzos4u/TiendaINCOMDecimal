<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_estatus_cotizacion.ascx.cs" Inherits="userControls_uc_estatus_cotizacion" %>


<asp:UpdatePanel ID="up_estatus" runat="server">
    <ContentTemplate>
          <h2 id="encabezado" runat="server" class="margin-b-4x">Cotización Estatus</h2>
        <asp:HiddenField ID="hf_numero_operacion" runat="server" />
        <asp:DropDownList ID="ddl_estatusCotizacion" OnSelectedIndexChanged="ddl_estatusCotizacion_SelectedIndexChanged" AutoPostBack="true" runat="server">
            <asp:ListItem></asp:ListItem>
            <asp:ListItem></asp:ListItem>
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
    </ContentTemplate>
    <Triggers></Triggers>
</asp:UpdatePanel>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PedidosUsuarioSeguimientoUC.ascx.cs" Inherits="userControls_operaciones_PedidosUsuarioSeguimiento" %>

<asp:UpdatePanel ID="up_seguimientoUsuarioPedido" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="is-flex is-justify-between is-items-center">
            <asp:HiddenField ID="hf_numero_operacion" runat="server" />
            <label for="<%=ddl_UsuarioSeguimiento.UniqueID %>">Asesor: </label>
            <asp:DropDownList ID="ddl_UsuarioSeguimiento" AppendDataBoundItems="true" class="form-select is-w-3_4" runat="server"></asp:DropDownList>
            <asp:LinkButton ID="btn_asignarAsesor" CssClass="" OnClick="btn_asignarAsesor_Click" runat="server">Asignar</asp:LinkButton>
        </div>
        <div id="Content_msgUsuarioSeguimiento" class="is-py-2"></div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_asignarAsesor" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>

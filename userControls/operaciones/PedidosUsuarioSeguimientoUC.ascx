<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PedidosUsuarioSeguimientoUC.ascx.cs" Inherits="userControls_operaciones_PedidosUsuarioSeguimiento" %>
<asp:UpdatePanel ID="up_seguimientoUsuarioPedido" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:HiddenField ID="hf_numero_operacion" runat="server" />
        <label for="<%=ddl_UsuarioSeguimiento.UniqueID %>">Asesor seguimiento</label>

        <asp:DropDownList ID="ddl_UsuarioSeguimiento" AppendDataBoundItems="true" class="form-select mb-3"
            runat="server">
        </asp:DropDownList>
        <div id="Content_msgUsuarioSeguimiento"></div>
        <asp:LinkButton ID="btn_asignarAsesor" CssClass="btn  btn-primary " OnClick="btn_asignarAsesor_Click" runat="server">Asignar</asp:LinkButton>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_asignarAsesor" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>

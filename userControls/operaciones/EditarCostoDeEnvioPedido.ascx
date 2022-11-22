<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditarCostoDeEnvioPedido.ascx.cs" Inherits="userControls_operaciones_EditarCostoDeEnvio" %>

<asp:HiddenField ID="hf_numero_operacion" runat="server" />
<asp:UpdatePanel ID="up_ContentDetallesEnvioPedido" UpdateMode="Conditional" Visible="false" runat="server">
    <ContentTemplate>
        <div id="ContentDetallesEnvioPedido" class="is-w-full is-px-8" visible="false" runat="server" style="border: 2px solid red">
            <div class="is-flex is-flex-col is-justify-between is-items-center">
                <label for="txt_MontoCostoEnvio" class="form-label">Nuevo costo de envío:</label>
                <asp:TextBox ID="txt_MontoCostoEnvio" placeholder="Monto (sin impuestos)" class="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
            </div>
            <div class="is-flex is-justify-between is-items-center is-py-2">
                <asp:LinkButton ID="btn_CerrarModificarDetallesDeEnvio" OnClick="btn_CerrarModificarDetallesDeEnvio_Click" Visible="true" runat="server">Cerrar </asp:LinkButton>
                <div class="">
                    <div id="Conteng_msg_envio"></div>
                    <asp:LinkButton ID="btn_guardarCostoDeEnvio" OnClick="btn_guardarCostoDeEnvio_Click" class="btn btn-primary" runat="server">Actualizar</asp:LinkButton>
                </div>
            </div>
        </div>
        <div id="Content_btn_ModificarDetallesEnvio" class="row" runat="server">
            <asp:LinkButton ID="btn_ModificarDetallesEnvio" style="text-decoration: none" OnClick="btn_ModificarDetallesEnvio_Click" runat="server">Cambiar costo</asp:LinkButton>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_ModificarDetallesEnvio" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btn_guardarCostoDeEnvio" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btn_CerrarModificarDetallesDeEnvio" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>

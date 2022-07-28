<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditarCostoDeEnvioPedido.ascx.cs" Inherits="userControls_operaciones_EditarCostoDeEnvio" %>


<asp:HiddenField ID="hf_numero_operacion" runat="server" />
<asp:UpdatePanel ID="up_ContentDetallesEnvioPedido" UpdateMode="Conditional" Visible="false" runat="server">
    <ContentTemplate>
        <div id="ContentDetallesEnvioPedido" class="row mb-3"  Visible="false" runat="server">


            <div class="col-12 mb-3">
                <label for="txt_MontoCostoEnvio" class="form-label">Establece costo de envío </label>


                <asp:TextBox ID="txt_MontoCostoEnvio" placeholder="Monto sin impuestos" class="form-control" ClientIDMode="Static" runat="server"></asp:TextBox>
            </div>
            <div class="col-auto">
                 <div id="Conteng_msg_envio"></div>
                <asp:LinkButton ID="btn_guardarCostoDeEnvio" OnClick="btn_guardarCostoDeEnvio_Click" class="btn btn-primary mb-3" runat="server">Actualizar costo</asp:LinkButton>

            </div>
            <div class="col-12">
               
                <asp:LinkButton ID="btn_CerrarModificarDetallesDeEnvio" OnClick="btn_CerrarModificarDetallesDeEnvio_Click" Visible="true"
                    runat="server">Cerrar </asp:LinkButton>
            </div>
        </div>
        <div id="Content_btn_ModificarDetallesEnvio" class="row mb-3"    runat="server">
            <asp:LinkButton ID="btn_ModificarDetallesEnvio" 
                OnClick="btn_ModificarDetallesEnvio_Click" runat="server">Admin. envío</asp:LinkButton>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_ModificarDetallesEnvio" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="btn_guardarCostoDeEnvio" EventName="Click" />
        
         <asp:AsyncPostBackTrigger ControlID="btn_CerrarModificarDetallesDeEnvio" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>

<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_btn_agregar_operacion.ascx.cs" Inherits="uc_btn_agregar_operacion" %>
<%@ register src="~/usercontrols/operaciones/uc_modal_agregar_operacion.ascx" tagname="mdl_addoperacion" tagprefix="uc1" %>
<%@ reference virtualpath="~/usercontrols/operaciones/uc_modal_agregar_operacion.ascx"%>

<script runat="server">


    // Handle the Button1 Click Event
    protected void btn_MostrarModal_Click(object sender, EventArgs e) {
        // Access the TestUserControl2 control from the page.               ListVIew Item  <   ListView   <  Container
        uc_modal_agregar_operacion tuc2 = (uc_modal_agregar_operacion)this.NamingContainer.NamingContainer.NamingContainer.FindControl("mdl_addOperacion");

        if(tuc2 == null) {
               tuc2 = (uc_modal_agregar_operacion)this.NamingContainer.FindControl("mdl_addOperacion");
            }
        // Set the TextBox Property of TestUserControl2 by accessing the publix property of TestUsercontrol2
        if (tuc2 != null)
            tuc2.numero_parte = hf_numero_parte.Value;
        tuc2.descripcion_corta = hf_descripcion_corta.Value;
        tuc2.mostrarModal();
        }

</script>

<asp:UpdatePanel ID="UP_cantidadCarrito" Visible="false" RenderMode="Inline" style="margin-top: -40px; position: absolute; right: 8px;" runat="server">
    <ContentTemplate>
        <asp:HiddenField ID="hf_numero_parte" runat="server" />

        <asp:HiddenField ID="hf_descripcion_corta" runat="server" />

<%--            <asp:LinkButton ID="btn_MostrarModal"   OnClick="btn_MostrarModal_Click" runat="server" class="btn hover-add-operacion waves-effect waves-light btn blue">
            <i class="material-icons  left">add</i> 
            </asp:LinkButton>--%>
        
    </ContentTemplate>
<%--    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btn_MostrarModal" EventName="Click" />
    </Triggers>--%>
</asp:UpdatePanel>





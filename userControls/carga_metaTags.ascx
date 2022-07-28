<%@ Control Language="C#" AutoEventWireup="true" CodeFile="carga_metaTags.ascx.cs" Inherits="tienda.carga_metatags" %>

<div id="content_carga_metatags" visible="false" runat="server">
    <asp:UpdatePanel ID="up_carga_metatags"  UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <asp:TextBox ID="txt_carga_metatags" placeholder="Guardar Metatag" style="width:200px;" runat="server"></asp:TextBox>
            <asp:LinkButton ID="btn_guardar_metatag" OnClientClick="btnLoading(this);" OnClick="btn_guardar_metatag_Click" runat="server">Guardar</asp:LinkButton>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_guardar_metatag" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</div>
 
<script>
    // Script para manejar las cajas de texto se ejecute un comando al presionar enter
    var input = document.querySelector("#<%= txt_carga_metatags.ClientID %>");
    var btn = document.querySelector("#<%= btn_guardar_metatag.ClientID %>");
            establecerEnter(input, btn);

        function resizeInput() {
    $(this).attr('size', $(this).val().length);
}

$('input[type="text"]')
    .keyup(resizeInput)
    .each(resizeInput);
</script>
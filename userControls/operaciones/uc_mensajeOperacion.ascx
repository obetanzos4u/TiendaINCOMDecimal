<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_mensajeOperacion.ascx.cs" Inherits="userControls_operaciones_uc_mensajeOperacion" %>


<asp:UpdatePanel><ContentTemplate>
<script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
<link href="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.9/summernote-lite.css" rel="stylesheet">
<script src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.9/summernote-lite.js"></script>


<div id="summernote" class="operacio_summernote"  visible="false" runat="server">
</div>

 <asp:TextBox ID="txt_mensajeAsesor" ClientIDMode="Static" Visible="false" style="display:none;" TextMode="MultiLine"  runat="server"></asp:TextBox>

<div id="content_mensajeCliente" visible="true" runat="server">
     <label>Mensaje</label>
    <asp:TextBox ID="txt_mensajeCliente" TextMode="MultiLine" Style="width: 100%" runat="server"></asp:TextBox>
</div>
    </ContentTemplate>
    <Triggers> <asp:AsyncPostBackTrigger ControlID="" EventName="Click" /></Triggers>
</asp:UpdatePanel>
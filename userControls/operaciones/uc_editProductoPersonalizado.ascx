<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_editProductoPersonalizado.ascx.cs" Inherits="tienda.uc_editProductoPersonalizado" %>


<asp:HiddenField ID="hf_idProducto" runat="server" />
<asp:HiddenField ID="hf_numero_operacion" runat="server" />

 
<asp:LinkButton ID="btn_editarProductoPersonalizado" OnClick="btn_editarProductoPersonalizado_Click" runat="server">Editar</asp:LinkButton>

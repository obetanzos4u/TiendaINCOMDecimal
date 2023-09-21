<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_btn_editar_operacion.ascx.cs" Inherits="uc_btn_editar_operacion" %>

<asp:HiddenField ID="hf_id_operacion" runat="server" />
<asp:HiddenField ID="hf_tipo_operacion" runat="server" />

 
        <asp:LinkButton ID="btn_editar_productos_operacion" OnClick="btn_editar_productos_operacion_Click" runat="server" class="">
            Agregar más productos
        </asp:LinkButton>

<%@ Page Language="C#" AutoEventWireup="true"   Async="true" CodeFile="contactos.aspx.cs" MasterPageFile="~/gnCliente.master" Inherits="usuario_mi_cuenta" %>
<%@ Register TagPrefix="uc" TagName="contactos"  Src="~/userControls/uc_contactos.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">

    <div class="is-bt-5 is-mx-6 is-border-soft is-rounded-xl is-p-8">
        <uc:contactos ID="adminContactos" runat="server" />
    </div>

</asp:Content>

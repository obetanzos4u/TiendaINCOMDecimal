<%@ Page Language="C#" AutoEventWireup="true"   Async="true" CodeFile="contactos.aspx.cs" MasterPageFile="~/gnCliente.master" Inherits="usuario_mi_cuenta" %>
<%@ Register TagPrefix="uc" TagName="contactos"  Src="~/userControls/uc_contactos.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">

    <div class="container z-depth-3">


        <uc:contactos ID="adminContactos" runat="server" />
    </div>

</asp:Content>

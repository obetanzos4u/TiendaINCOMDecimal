<%@ Page Language="C#" AutoEventWireup="true"   Async="true" CodeFile="crear-direccion-de-envio.aspx.cs" MasterPageFile="~/gnCliente.master" Inherits="crear_direccion_envio" %>
<%@ Register TagPrefix="uc" TagName="cEnvios"  Src="~/userControls/uc_crearDireccionesEnvio.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">

      <uc:cEnvios id="ucCrearDireccEnvios"  runat="server" />
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/herramientas/_masterConfiguraciones.master" Async="true" CodeFile="configuraciones-operaciones.aspx.cs" Inherits="herramientas_configuraciones_operaciones" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="is-container is-flex is-flex-col is-justify-center is-items-center">
        <h1 class="is-text-xl is-font-semibold is-select-none">Tipo de cambio</h1>
        <div class="is-flex is-flex-col is-justify-center is-items-center">
            <p>Módulo para actualizar el tipo de cambio del día conforme al Diario Oficial de la Federación</p>
            <asp:TextBox ID="txt_tipoDeCambio" Style="border-radius: 0.5rem; background-color: #f1f5f9; padding-left: 0.5rem" Enabled="false" runat="server"></asp:TextBox>
            <div class="input-field col s12 l12">
                <%--<label for="<%= txt_tipoDeCambio.UniqueID %>">Tipo de cambio</label>--%>
            </div>
            <div class="is-w-full is-flex is-justify-between is-items-center">
                <asp:LinkButton ID="btn_guardarTipodeCambio" OnClick="btn_guardarTipodeCambio_Click" CssClass="is-btn-gray is-select-none" Enabled="false" runat="server">Establecer</asp:LinkButton>
                <asp:LinkButton ID="btn_obtenerAutomáticamente" OnClick="btn_obtenerAutomáticamente_Click" CssClass="is-btn-blue is-select-none" runat="server">Obtener</asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>

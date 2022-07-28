<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/herramientas/_masterConfiguraciones.master" Async="true" 
    CodeFile="configuraciones-operaciones.aspx.cs"
  Inherits="herramientas_configuraciones_operaciones" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <h1 class="center-align">Configuración de operaciones</h1>
    <h3 class="center-align">Powered by Marketing development</h3>
    <div class="container">
        <div class="row">
            <h2 class="c">Tipo de cambio</h2>
            <div class="input-field col s12 l12">
                <asp:TextBox ID="txt_tipoDeCambio" runat="server"></asp:TextBox>
                <label for="<%= txt_tipoDeCambio.UniqueID %>">Tipo de cambio</label>
            </div>
            <div class="input-field col s12 l12">
                <asp:LinkButton ID="btn_guardarTipodeCambio" OnClick="btn_guardarTipodeCambio_Click" 
                    CssClass="waves-effect waves-light btn btn-s blue" runat="server">Guardar</asp:LinkButton>

                 <asp:LinkButton ID="btn_obtenerAutomáticamente" OnClick="btn_obtenerAutomáticamente_Click"
                    CssClass="waves-effect waves-light btn btn-s blue" runat="server">Obtener automáticamente</asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>




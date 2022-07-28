<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/herramientas/_masterConfiguraciones.master" Async="true"
    CodeFile="reportes-ejecutivos.aspx.cs"
    Inherits="herramientas_reportes_ejecutivos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="row blue gradient-blue-1">
        <div class="input-field col s6 l6 white-text">
            <h1 class="margin-b-2x">Reportes ejecutivos</h1>
            <h3 class="margin-t-2x">Powered by Marketing development</h3>
        </div>
        <div class="input-field col s6 l6 white-text">
            <h2>Selecciona un tipo de reporte</h2>
            <asp:DropDownList ID="ddl_reportes_ejecutivos" runat="server">
                <asp:ListItem>Selecciona</asp:ListItem>
                <asp:ListItem Value="productos">Productos</asp:ListItem>
            </asp:DropDownList>
        </div>

    </div>
    <div class="container">
        <div class="card blue gradient-blue-1 white-text">
            <div class="row" style="padding: 20px;">
                <div class="col s6 l6">
                    <i class="material-icons background-round mt-5">perm_identity</i>
                    <p>Clients</p>
                </div>
                <div class="col s6 l6 ">
                    <h5>1885</h5>
                    <p>New</p>
                    <p>1,12,900</p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeFile="testSantanderGenerarLigar.aspx.cs"
    MasterPageFile="~/general.master" Inherits="testSantanderGenerarLigar" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col s12">
                <h1>Test Pago Santander</h1>
                <asp:LinkButton id="btn_GenerarLigaPago" class="btn blue" OnClick="btn_GenerarLigaPago_Click" runat="server">Generar Liga</asp:LinkButton>
            </div>
        </div>
        <div class="row">
            <div class="col s12">
                <asp:TextBox id="Log" TextMode="Multiline" runat="server" ></asp:TextBox>
            </div>
        </div>
    </div>
</asp:Content>

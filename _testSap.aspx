<%@ Page Language="C#" AutoEventWireup="true" Async="true" MasterPageFile="~/general.master" CodeFile="_testSap.aspx.cs" Inherits="TestSap" %>
<%@ Register Src="~/userControls/PayPal_button.ascx" TagName="pago" TagPrefix="PayPal" %>



<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="container">
        <div class="row">
            <h1 class="center-align">Test Sap</h1>
            <h2 class="center-align">Conexión en tiempo real</h2>


        </div>
        <div>
               <PayPal:pago runat="server"></PayPal:pago>
        </div>
        <div class="row">
            <h1 class="center-align">Ingresa el número de parte</h1>
        </div>
        <div class="row">
            <div class="col s12 m3 input-field">
                <asp:TextBox ID="txt_numero_parte" Text="2170" runat="server"></asp:TextBox>
            </div>
                <div class="col s12 m6 input-field">
                    <asp:LinkButton runat="server" ID="btn_obtener" CssClass="btn blue" OnClick="btn_obtener_Click" >Obtener</asp:LinkButton>
                    </div>
        </div>
        <div class="row">
           <div ID="div_respuesta" runat="server"></div>
        </div>

    </div>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" Async="true" MasterPageFile="~/general.master" CodeFile="_test-paypal.aspx.cs" Inherits="TestPayPal" %>



<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="container">
        <div class="row">
            <h1 class="center-align">Test Pay Pal GetOrder</h1>


        </div>
        <div>
                
        </div>
        <div class="row">
            <h1 class="center-align">Ingresa el número de de orden</h1>
        </div>
        <div class="row">
            <div class="col s12 m3 input-field">
                <asp:TextBox ID="txt_order_id" Text="2170" runat="server"></asp:TextBox>
            </div>
                <div class="col s12 m6 input-field">
                    <asp:LinkButton runat="server" ID="btn_obtener" CssClass="btn blue" OnClick="btn_obtener_ClickAsync" >Obtener</asp:LinkButton>
                    </div>
        </div>
        <div class="row">
           <div ID="div_respuesta" runat="server"></div>
        </div>

    </div>
</asp:Content>

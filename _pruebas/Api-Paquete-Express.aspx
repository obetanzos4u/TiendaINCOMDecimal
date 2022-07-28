<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/basic.master" CodeFile="Api-Paquete-Express.aspx.cs" Inherits="_pruebas_Api_Paquete_Express" %>

<asp:Content ID="Content2"  ContentPlaceHolderID="top" runat="Server">
    <div class="row"><div class="col col s12"> <h1>Api Paquete Express test</h1></div></div>
    <div class="row">
        <div class="col col s12">
            Token
            <asp:TextBox ID="txt_token" runat="server"></asp:TextBox>

        </div>
        <div class="col col s12 margin-b-6x">
            <asp:Button Text="Obtener Token" id="btn_obtener_token"  OnClick="btn_obtener_token_Click" CssClass="btn blue" runat="server" />

        </div>
         
        <div class="col col s12 margin-b-6x">
            Input json
            <asp:TextBox ID="txt_json" TextMode="MultiLine" CssClass="materialize-textarea" runat="server"></asp:TextBox>
              <asp:Button  ID="btn_send" Text="Send" OnClick="btn_send_Click" CssClass="green btn" runat="server" />
        </div>

        <div class="col col s12">
            Response
            <asp:TextBox ID="txt_response" TextMode="MultiLine"   CssClass="materialize-textarea"  runat="server"></asp:TextBox>

        </div>
    </div>
</asp:Content>

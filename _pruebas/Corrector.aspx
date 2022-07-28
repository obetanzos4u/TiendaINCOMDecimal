<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/basic.master" CodeFile="Corrector.aspx.cs" Inherits="_pruebas_Corrector" %>

<asp:Content ID="Content2" ContentPlaceHolderID="top" runat="Server">
    <div class="row">
      
        <div class="col col s12">  <h1>Api corrector ortográfico</h1>
            <asp:TextBox ID="txt_busqueda" runat="server"></asp:TextBox>
      <asp:Button Text="Corregir" id="btn_corregir"  OnClick="btn_corregir_Click" CssClass="btn blue" runat="server" />
            <br />
             <asp:TextBox ID="txt_response" TextMode="MultiLine"   CssClass="materialize-textarea"  runat="server"></asp:TextBox>
            <br />
              <h2 class="margin-b-2x">Sugerencia</h2>
            <asp:Label ID="lbl_corregido" style="font-size:2rem;" CssClass="blue-text text-lighten-1" runat="server"  ></asp:Label>
        </div>
    </div>
</asp:Content>

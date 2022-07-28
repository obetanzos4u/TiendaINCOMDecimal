<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeFile="async.aspx.cs" MasterPageFile="~/general.master" Inherits="inicio" %>
 
<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="container">
        <div class="row">
                <div class="input-field col s6">
                    Data
                <asp:TextBox ID="txt_data" runat="server">

                </asp:TextBox>
            </div>
        </div>
        <div class="row">


            <p>
                <asp:LinkButton ID="btn_SendAsyn" CssClass="btn" OnClick="btn_SendAsyn_Click" runat="server">Send Async</asp:LinkButton>
            </p>
            </div>
             <div class="row">
            <p>
               
 

                 <asp:LinkButton ID="btn_GetStockRestSAP" OnClick="btn_GetStockRestSAP_Click" CssClass="btn" runat="server">
                    Get Stock SAP
                </asp:LinkButton>

             
                 <asp:LinkButton  ID="CrearPedidoEnSapPruebas" OnClick="CrearPedidoEnSapPruebas_Click" CssClass="btn blue" runat="server">
                   Crear pedido en SAP pruebas
                </asp:LinkButton>


                  <asp:LinkButton  ID="btn_ObtenerFecha"  OnClick="btn_ObtenerFecha_Click" CssClass="btn blue" runat="server">
                  Fecha Utc
                </asp:LinkButton>
            </p>
        </div>
    </div>
    <div class="row">
        <div class="input-field col s12">
            <div style="float: right;">
                <a href="#" onclick="event.preventDefault(); document.querySelector('#txt_result').value = ''">Borrar caja</a>

            </div>
            <asp:TextBox ID="txt_result" ClientIDMode="Static" class="materialize-textarea" TextMode="MultiLine" runat="server">

            </asp:TextBox>
        </div>
    </div>
</asp:Content>

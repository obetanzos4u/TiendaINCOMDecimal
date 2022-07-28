<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_DireccionEnvioPredeterminada.ascx.cs" Inherits="userControls_uc_DireccionEnvioPredeterminada" %>


<a id="OpenModalSelectorDireccionEnvio" href="#">
    <i class="material-icons tiny">location_on</i>
    Envio:
    <asp:Label ID="Info" runat="server"></asp:Label></a>

<style>
    #OpenModalSelectorDireccionEnvio {
        color: black;
        border-bottom: solid 2px #ffffff;
        padding: 2px 5px;
    }

     #OpenModalSelectorDireccionEnvio:hover{
background: #ffcc3b;
    border: none;
  
     }
</style>
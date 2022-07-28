<%@ Control Language="C#" AutoEventWireup="true"   CodeFile="uc_precio_detalles.ascx.cs" Inherits="tienda.uc_precio_detalles" %>

<asp:HiddenField ID="hf_numero_parte" runat="server" />

<asp:HiddenField ID="hf_moneda" runat="server" />
 <div id="precios_aviso" class="white-text green" style="padding: 5px 10px; display:table;" visible="false" runat="server">Precios escalonados disponibles</div>
<div id="tb_precios_escalonados" visible="false" runat="server">
</div>
 
<span id="tooltippedPreciosEscalonados"  class="tooltippedPreciosEscalonados green-text" style="cursor:pointer;" visible="false" runat="server">
 <br />   <strong>Mostrar más precios</strong>

</span>


<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/herramientas/_masterConfiguraciones.master" Async="true" 
    CodeFile="configuraciones-calculo-envios.aspx.cs"
  Inherits="herramientas_configuraciones_calculo_envios" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <h1 class="center-align">Configuración de cálculo de envios-fletes por Api</h1>
    <h3 class="center-align">Powered by IT4U development</h3>
    <div class="container">
        <div class="row">
            <h2 class="c">Api envios-fletes</h2>
            <p>Los precios de fletes/envios  se calculan mediante una api en un dominio externo de Incom.mx, active/desactive esta opción
                si los calculos se calcularan de esta forma o de forma manual (Un asesor tiene que establecer el precio del envío manualmente).
            </p>  <h2 class="margin-b-2x">Estatus: <asp:Label ID="lbl_estatus" runat="server"></asp:Label></h2>
            <div class="margin-t-2x input-field col s12 l12">
              
                <label>
                <asp:CheckBox id="chk_Estatus_ApiFlete" AutoPostBack="true"
                            OnCheckedChanged="chk_Estatus_ApiFlete_CheckedChanged" 
                    runat="server"></asp:CheckBox>
                    <span>Activar/Desactivar Cálculo de fletes</span>
                    </label>
            </div>
 
        </div>
    </div>
</asp:Content>




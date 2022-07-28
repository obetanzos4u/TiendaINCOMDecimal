<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/herramientas/_masterConfiguraciones.master" Async="true" 
    CodeFile="cargar-precios-fantasma.aspx.cs"
  Inherits="herramientas_configuraciones_cargar_precios_fantasma" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <h1 class="center-align">Carga información de precios fantasma</h1>
    <h3 class="center-align">Powered by IT4U development</h3>
    <div class="container">
        <div class="row">
            <h2 class="c">Cargar precios fantasma</h2>
            <p>
                Solo se mostrará a los productos que aquí se establezcan, la moneda lo toma de precios rangos.
                <br />
                Nombre de hoja: <strong>[PreciosFantasma]</strong>, Insertar info en las columnas:  <strong>[1] = número de parte</strong>,  <strong>[2] = precio fantasma.</strong>,<strong>[3]  porcentaje Fantasma</strong>
            </p>
        </div>
        <div class="row">
           <div class="col s12 l12">  <h2  >Carga:</h2></div>
            <div class="col s12 l12">
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </div>
            <div class="col s12 l12"><p>
                <asp:LinkButton ID="btn_cargar_archivos" OnClick="btn_cargar_archivos_Click" CssClass="btn "
                    runat="server">Cargar ahora</asp:LinkButton></p>
            </div>
              <div class="col s12 l12">

                  </div>
            <asp:TextBox ID="Log_Result" TextMode="MultiLine" runat="server"></asp:TextBox>
        </div>
    </div>
</asp:Content>




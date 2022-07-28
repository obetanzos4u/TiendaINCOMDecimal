<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/herramientas/_masterConfiguraciones.master" Async="true"
    CodeFile="reportes-ventas-diario.aspx.cs"
    Inherits="herramientas_reportes_ventas_diario" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">

<asp:Timer ID="Timer1" runat="server"  Interval="300000"  OnTick="Timer1_Tick" /> 

    <div class="row blue gradient-black-2">

        <div class="input-field col s4 l4 white-text">
            <h1 class="margin-b-2x">Reporte ventas</h1>
            <h3 class="margin-t-2x">Powered by MKT Development</h3>
        </div>
                  <div class="input-field col s2 l2 white-text">
            <h2>Mes Actual</h2>
                <asp:TextBox ID="txt_fechaActual" CssClass="fecha" runat="server"></asp:TextBox>
        </div>



          <div class="input-field col s2 l2 white-text hide">
            <h2>Periodo Desde</h2>
                <asp:TextBox ID="txt_fecha_desde" CssClass="fecha" runat="server"></asp:TextBox>
        </div>
        <div class="input-field col s2 l2 white-text hide">
            <h2>Periodo Hasta</h2>
            <asp:TextBox ID="txt_fecha_hasta" CssClass="fecha" runat="server"></asp:TextBox>
        </div>
         <div class="input-field col s2 l2 white-text">
          <asp:LinkButton ID="btn_FechaActual" CssClass="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5"
                 OnClick="btn_mostrarFechaActual_Click" runat="server">Actual</asp:LinkButton> <br /><br />
            
              <asp:LinkButton ID="btn_enviarReporte" 
                  CssClass="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5"
               OnClick="btn_enviarReporte_Click" runat="server">Enviar</asp:LinkButton>

             <asp:LinkButton ID="btn_solo_porcentajes" CssClass="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5"
                 OnClick="btn_solo_porcentajes_Click" runat="server">Solo Porcentajes</asp:LinkButton>
        </div>
    </div>
     <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional"  class="container" RenderMode="Block" runat="server">  <ContentTemplate>
        <div id="cont_barraFacturado" class="row" runat="server">
            <div style="height: 38px; background: #b9b9b9;" runat="Server">
               
                    <asp:Panel  id="barraAzulFacturadoMes" runat="server"></asp:Panel>
                   
                
            </div>
        </div>
           <div id="content_datos_reporte_porcentajes" class="row" runat="server" visible="false">
               <div class="col s12 l12">
                <h1 id="h1_titulo_facturado2" class="margin-b-4x" runat="server"></h1>
                <h2 class="margin-t-4x margin-b-2x">En el día</h2>
             
                     Dias laborales transcurridos: <strong><asp:Label ID="lbl_numeroDiaLaboralTranscurridos2" CssClass="blue-text" runat="server"></asp:Label></strong><br />
                   Dias laborales restantes: <strong><asp:Label ID="lbl_numeroDiaLaboralRestantes2" CssClass="blue-text" runat="server"></asp:Label><br /></strong>
                  Porcentaje expectativa del día:
                <strong><asp:Label ID="lbl_porcentajeExpectativaDía2" CssClass="blue-text" runat="server"></asp:Label><br /></strong>

                       <h2 class="margin-t-4x margin-b-2x">En el mes</h2> 
                 <!--    Porcentaje de meta mensual:-->
                 <strong><asp:Label ID="lbl_porcentajeMensual2" Visible="false" CssClass="blue-text" runat="server"></asp:Label></strong> 
                 Porcentaje expectativa meta mensual: <strong><asp:Label ID="lbl_porcentajeExpectativaMensual2" CssClass="blue-text" runat="server"></asp:Label></strong><br />
               
                   </div>
           </div>
        <div id="content_datos_reporte" class="row" runat="server">
            <div class="col s6 l4">
                <h1 id="h1_titulo_facturado" class="margin-b-4x" runat="server"></h1>
                <h2 class="margin-t-4x margin-b-2x">En el día</h2>
                Objetivo diario: <strong>
                    <asp:Label ID="lbl_objetivo_diario" CssClass="blue-text" runat="server"></asp:Label></strong><br />
                Facturado en el día: <strong><asp:Label ID="lbl_facturadoElDia" CssClass="blue-text" runat="server"></asp:Label></strong><br />
                     Dias laborales transcurridos: <strong><asp:Label ID="lbl_numeroDiaLaboralTranscurridos" CssClass="blue-text" runat="server"></asp:Label></strong><br />
                   Dias laborales restantes: <strong><asp:Label ID="lbl_numeroDiaLaboralRestantes" CssClass="blue-text" runat="server"></asp:Label><br /></strong>
                  Porcentaje expectativa del día:
                <strong><asp:Label ID="lbl_porcentajeExpectativaDía" CssClass="blue-text" runat="server"></asp:Label><br /></strong>

                       <h2 class="margin-t-4x margin-b-2x">En el mes</h2>
                   Meta total del mes: <strong><asp:Label ID="lbl_facturadoMetaMes" CssClass="blue-text" runat="server"></asp:Label></strong><br />
                Expectativa al día <asp:Literal ID="lt_diaReporteSeleccionado" runat="server"></asp:Literal>
                en el mes: <strong><asp:Label ID="lbl_expectativaActualMes" CssClass="blue-text" runat="server"></asp:Label></strong><br />
                  Facturado actual en el mes: <strong><asp:Label ID="lbl_facturadoMes" CssClass="blue-text" runat="server"></asp:Label></strong><br />
                <!--   Diferencia: <strong><asp:Label ID="lbl_montoFacturadoDiferenciaActualVSMetaMes" CssClass="blue-text" runat="server"></asp:Label></strong><br />-->

               
                 <!--  Diferencia actual vs meta total:  -->
                <strong><asp:Label ID="lbl_facturadoDiferenciaActualesMes" Visible="false"  CssClass="blue-text" runat="server"></asp:Label></strong> 
                 <!--    Porcentaje de meta mensual:-->
                 <strong><asp:Label ID="lbl_porcentajeMensual" Visible="false" CssClass="blue-text" runat="server"></asp:Label></strong> 
                 Porcentaje expectativa meta mensual: <strong><asp:Label ID="lbl_porcentajeExpectativaMensual" CssClass="blue-text" runat="server"></asp:Label></strong><br />
                <h2 class="margin-t-4x margin-b-2x">En el año</h2>
                    Acumulado anual: <strong><asp:Label ID="lbl_acomuladoAnual" CssClass="blue-text" runat="server"></asp:Label></strong><br />
                   </div>
                <div  class="col s6 l4" >
                 <h2 ID="h2_periodoVendedorFacturado" runat="server"></h2> <br />
               <!---             Monto facturado por Telemarketing: <br /> --->
                      <strong>  <asp:Label ID="lbl_montoTotalFacturadoMesTelemarketing" CssClass="blue-text" Visible="false" runat="server"></asp:Label></strong>
                        <span runat="server" id="vendedoresFacturado"></span> 

                </div>
                    <div  class="col s6 l4" >
                          <h2 ID="h2_periodoVendedorPedidos" class="margin-b-2x" runat="server"></h2>  
                   <!---    Monto total pedidos ingresados  <br />(todos los vendedores): <br /> -->
                      <strong>  <asp:Label ID="lbl_montoTotalPedidosIngresadosMes" CssClass="blue-text" Visible="false" runat="server"></asp:Label></strong>
                         <span runat="server" id="vendedoresPedidos"></span> 
                        <div id="ultimosProductosRegistrados" visible="false" runat="server"></div>
                    </div>
        </div>

         <div></div>

           <div id="content_soloPorcentajes" runat="server" visible="false" class="row center-align" style="background: url('https://i.gifer.com/sis.gif');">
             <div class="col l12">
                 <h1>Apágame</h1>
                 <h1>Apágame</h1>
                 <h1>Apágame</h1>
             </div>
              </div>
         <div id="content_msg_apagado" runat="server" visible="false" class="row center-align" style="background: url('https://i.gifer.com/sis.gif');">
             <div class="col l12">
                 <h1>Apágame</h1>
                 <h1>Apágame</h1>
                 <h1>Apágame</h1>
             </div>
              </div>
         <div class="row">
             <div class="col l12">
                 <asp:Label ID="lbl_fechas_timer" runat="server"></asp:Label>
             </div>
         </div>
     </ContentTemplate>
         <Triggers>
             <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick"></asp:AsyncPostBackTrigger>
         </Triggers>
     </asp:UpdatePanel>
    <script>
                  
                    document.addEventListener('DOMContentLoaded', function () {

                        var inter_es = {
                            yearRange: 5,
                            months: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Deciembre'],
                            monthsShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                            weekdays: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
                            weekdaysShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sab'],
                            weekdaysAbbrev:	['D','L','M','X','J','V','S'],
                            cancel: 'Cancelar',
                            clear: 'Borrar',
                            done: 'Ok'
                        };
                        var opciones = { i18n: inter_es ,     format: 'yyyy-mm-dd'};

                        var elems = document.querySelectorAll('.fecha');
                        var instances = M.Datepicker.init(elems, opciones );
     
           
                    });
                </script>
</asp:Content>

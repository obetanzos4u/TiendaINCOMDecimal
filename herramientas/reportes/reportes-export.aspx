<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    MasterPageFile="~/herramientas/_masterConfiguraciones.master" Async="true" CodeFile="reportes-export.aspx.cs"
    Inherits="herramientas_reportes_export" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <style>
        body.dragging, body.dragging * {
            cursor: move !important;
        }

        .dragged {
            position: absolute;
            opacity: 0.5;
            z-index: 2000;
        }

        ol.simple_with_animation {
            margin: 0px;
            padding: 5px 5px;
            border: solid 1px #f44336;
            height: 400px;
            overflow: overlay;
        }

            ol.simple_with_animation > li {
                list-style: none;
                margin: 5px 5px 5px 0px;
                cursor: move;
                border: solid 1px rgba(0, 0, 0, 0.15);
            }

            ol.simple_with_animation li.placeholder {
                position: relative;
                /** More li styles **/
            }

                ol.simple_with_animation li.placeholder:before {
                    position: absolute;
                    /** Define arrowhead **/
                }
    </style>
    <h1 class="center-align">XLS REPORT EXPORT</h1>
    <h3 class="center-align">Powered by Marketing development</h3>

    <div class="row">
        <div class="col s12">
            <ul class="tabs">
                <li class="tab col s3"><a class="active" href="#test1">Mis reportes</a></li>
                <li class="tab col s3"><a href="#test2">Cotizaciones </a></li>

            </ul>
        </div>
        <div id="test1" class="col s12">
            <div class="row">
                <div class="col s12 m6 l6">
                    <h2>Reportes guardados</h2>
                    <p>En esta sección podrás descargar el archivo de excel de tus reportes configurados previamente en la pestaña "Cotizaciones". </p>
                    <asp:UpdatePanel ID="up_reportes" UpdateMode="Conditional" class="container" runat="server">
                        <ContentTemplate>
                            <label>Selecciona reporte</label>
                            <asp:DropDownList ID="ddl_reportes" SelectionMode="multiple" multiple runat="server"></asp:DropDownList>
                            <asp:LinkButton ID="btnEliminarReporte" OnClientClick="if ( ! eliminarReporte()) return false;"
                                OnClick="btnEliminarReporte_Click" CssClass="red-text" runat="server">Eliminar reporte</asp:LinkButton>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col s12 m6 l6">
                    <div class="row">
                        <h2>Fecha</h2>
                        <p>Aplica un filtro de fecha para tus reportes. El valor predeterminado son los últimos 6 días a la fecha actual del campo fecha de creación de la cotización</p>
                        <div class="col s12 m6 l6">
                            <label>Desde</label>
                            <asp:TextBox ID="txt_fecha_desde" CssClass="fecha" runat="server"></asp:TextBox>
                        </div>
                        <div class="row">
                            <div class="col s12 m6 l6">
                                <label>Hasta</label>
                                <asp:TextBox ID="txt_fecha_hasta" CssClass="fecha" runat="server"></asp:TextBox>

                            </div>
                        </div>

                    </div>

                </div>
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
            </div>
            <div class="row center">
                <asp:LinkButton ID="btnExport" OnClick="btnExport_Click" runat="server" CssClass="waves-effect waves-light btn blue">
                    <i class="left  material-icons">arrow_downward</i>Exportar XLS</asp:LinkButton>




            </div>
            <div class="row">
                <h4>Log de resultado</h4>
                <asp:TextBox TextMode="MultiLine" ID="txt_log" CssClass="materialize-textarea" runat="server">

                </asp:TextBox>
            </div>
        </div>
        <div id="test2" class="col s12">
            <div class="row">
                <h2>Fuente de datos</h2>
                <p>Selecciona una fuente de datos para mostrar campos y guardar un reporte.</p>
                <asp:UpdatePanel ID="up_ddl_reportesDisponibles" class="col s12 m6 l6" RenderMode="Block" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>

                        <asp:DropDownList ID="ddl_reportesDisponibles" AutoPostBack="true"
                            OnSelectedIndexChanged="ddl_reportesDisponibles_SelectedIndexChanged" runat="server">
                        </asp:DropDownList>
                        <label>Selecciona tipo de reporte</label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_reportesDisponibles" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="row">
                <div class="col s12 m6 l6">
                    <h2>Campos disponibles</h2>
                    <p>Da clic para añadir todos los campos en una sola vez</p>
                    <asp:UpdatePanel ID="up_camposDisponibles" RenderMode="Inline" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div id="content_btn" runat="server"></div>

                            <ol id="camposDisponibles" class="simple_with_animation vertical" runat="server">
                            </ol>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="col s12 m6 l6">
                    <h2>Campos seleccionados</h2>
                    <p>Da clic para quitar todos los campos en una sola vez</p>
                    <asp:UpdatePanel ID="up_camposSeleccionados" RenderMode="Inline" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div id="content_btnQuitar" runat="server"></div>
                            <ol id="camposSeleccionados" runat="server" class="simple_with_animation vertical camposSeleccionados">
                            </ol>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:UpdatePanel ID="up_GuardarReporte" ValidateRequestMode="Disabled" UpdateMode="Conditional" class="container" runat="server">
                        <ContentTemplate>
                            <label for="<%= txt_nombreReporte.ClientID%>">Nombre de este reporte</label>
                            <asp:TextBox ID="txt_nombreReporte" placeholder="Nombre de este reporte" ClientIDMode="Static" runat="server"></asp:TextBox>
                            <asp:LinkButton ID="GuardarReporte" OnClick="btn_GuardarReporte_Click" OnClientClick="generarQueryToTxt();" class="waves-effect waves-light btn blue" runat="server">Guardar reporte</asp:LinkButton>
                            <!-- OnClick="btn_GuardarReporte_Click"  -->


                            <asp:TextBox TextMode="MultiLine" ID="txt_reporteValor" CssClass="hide  materialize-textarea" ClientIDMode="Static" runat="server"></asp:TextBox>
                            <asp:TextBox TextMode="MultiLine" ID="__txt_reporteValorHTML" CssClass=" hide materialize-textarea" ClientIDMode="Static" runat="server"></asp:TextBox>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="GuardarReporte" EventName="Click" />

                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

    </div>




    <script>




        function generarQueryToTxt() {

            var query = " ";
            var valorJson = " ";
            var elementsLI = document.querySelectorAll("ol.camposSeleccionados > li");


            for (var i = 0; i < elementsLI.length; i++) {
                var tablaalias = elementsLI[i].getAttribute("tablaalias");

                var campovalor = elementsLI[i].getAttribute("campovalor");
                var campoAlias = elementsLI[i].getAttribute("campoAlias");


                if (campovalor.indexOf('IIF') !== -1) {
                    query = query + campovalor + " as [" + campoAlias + " (" + tablaalias + ")] , ";
                } else {
                    query = query + tablaalias + "." + campovalor + " as [" + campoAlias + " (" + tablaalias + ")] , ";
                }
                valorJson = valorJson + "[\"" + tablaalias + "\",\"" + campovalor + "\", \"" + campoAlias + "\"] | ";


            }
            document.querySelector("#txt_reporteValor").value += query;
            document.querySelector("#__txt_reporteValorHTML").value = valorJson;
        }

        function eliminarReporte() {
            return confirm("Confirma que deseas ELIMINAR los reportes seleccionados");
        }
        function Actualización() {
            return confirm("Confirma que deseas ACTUALIZAR registros.");
        }
        function Insertar() {
            return confirm("Confirma que deseas INSERTAR registros.");
        }




    </script>
</asp:Content>




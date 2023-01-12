<%@ Page Language="C#" AutoEventWireup="true" CodeFile="logs-sql.aspx.cs" MasterPageFile="~/herramientas/_masterConfiguraciones.master" Async="true" Inherits="herramientas_logs_sql" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="is-container is-flex is-flex-col is-justify-center is-items-center">
        <h1 class="is-text-xl is-font-semibold is-select-none">Registro de errores</h1>
        <div class="is-w-full is-flex is-flex-col is-justify-center is-items-center">
            <div class="is-w-4_5 is-flex is-justify-evenly is-items-center borderTest">
                <div class="">
                    <label>Desde</label>
                    <%--<a href="#" onclick="document.querySelector('#txt_fecha_desde').value = '';">borrar</a>--%>
                    <asp:TextBox ID="txt_fecha_desde" ClientIDMode="Static" TextMode="Date" runat="server"></asp:TextBox>
                </div>
                <div class="">
                    <label>Hasta</label>
                    <%--<a href="#" onclick="document.querySelector('#txt_fecha_hasta').value = '';">borrar</a>--%>
                    <asp:TextBox ID="txt_fecha_hasta" ClientIDMode="Static" TextMode="Date" runat="server"></asp:TextBox>
                </div>
                <asp:LinkButton ID="btn_CargarLog" OnClick="btn_CargarLog_Click" CssClass="is-btn-blue" runat="server">Mostrar</asp:LinkButton>
            </div>
            <div class="row">
                <asp:ListView ID="lv_log_sql" ItemPlaceholderID="item" runat="server">
                    <LayoutTemplate>
                        <ul class="collapsible">
                            <div id="item" runat="server"></div>
                        </ul>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <li>
                            <div class="collapsible-header">
                                <i class="material-icons">info_outline</i><span class="light-blue-text text-darken-1"><%# Eval("usuario") %></span>&nbsp;<%# Eval("valores") %>
                            </div>
                            <div class="collapsible-body"><span><%# Eval("error") %></span></div>
                        </li>
                    </ItemTemplate>
                </asp:ListView>
                <asp:GridView ID="gv_log_sql" class=" " HeaderStyle-CssClass="thead-dark"
                    AutoGenerateColumns="true" runat="server">
                </asp:GridView>
            </div>
        </div>
    </div>

    <script>
        document.addEventListener('DOMContentLoaded', function () {
            var elems = document.querySelectorAll('.datepicker');
            var instances = M.Datepicker.init(elems, options);
        });

        function fechaFormat(t) {
            console.log(t);
            if (t.value.length == 4) { console.log("4"); t.value = t.value + "-"; }
            if (t.value.length == 7) { console.log("7"); t.value = t.value + "-"; }

        }
    </script>
</asp:Content>

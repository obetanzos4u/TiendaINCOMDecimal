<%@ Page Language="C#" AutoEventWireup="true" CodeFile="logs-sql.aspx.cs"
    MasterPageFile="~/herramientas/_masterConfiguraciones.master" Async="true" Inherits="herramientas_logs_sql" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <h1 class="center-align">Log SQL</h1>
    <h3 class="center-align">Powered by Development IT4U</h3>
    <div class="container">

        <div class="row">
            <div class="col s12 m5 l3 xl1"><label>Fecha desde AAAAMMDD</label> <a href="#" onclick="document.querySelector('#txt_fecha_desde').value = '';">borrar</a>
                <asp:TextBox ID="txt_fecha_desde" ClientIDMode="Static" onkeyup="fechaFormat(this)"   runat="server"></asp:TextBox>
            </div>
            <div class="col s12 m5 l3 xl1"><label>Fecha hasta AAAAMMDD</label>  <a href="#" onclick="document.querySelector('#txt_fecha_hasta').value = '';">borrar</a>
                <asp:TextBox ID="txt_fecha_hasta"  ClientIDMode="Static" onkeydown="fechaFormat(this)"   runat="server"></asp:TextBox>
            </div>
            <div class="col s12 m2 l2 xl1"><br />
                <asp:LinkButton ID="btn_CargarLog" OnClick="btn_CargarLog_Click" CssClass="btn blue" runat="server">Cargar Log</asp:LinkButton></div>
               </div>   <div class="row">
            <asp:ListView ID="lv_log_sql" ItemPlaceholderID="item" runat="server">
                <LayoutTemplate>
                    <ul class="collapsible">
                      <div id="item" runat="server"></div>
                    </ul>
                </LayoutTemplate>
                <ItemTemplate>

                      <li>
                           <div class="collapsible-header">
                               <i class="material-icons">info_outline</i><span class="light-blue-text text-darken-1"><%# Eval("usuario") %></span>&nbsp;<%# Eval("valores") %> </div>
      <div class="collapsible-body"><span><%# Eval("error") %></span></div>
    
                      </li>
                </ItemTemplate>
            </asp:ListView>
            <asp:GridView ID="gv_log_sql" class=" " HeaderStyle-CssClass="thead-dark"
                AutoGenerateColumns="true" runat="server">
            </asp:GridView>
  </div>    </div>

    <script>

        document.addEventListener('DOMContentLoaded', function () {
            var elems = document.querySelectorAll('.datepicker');
            var instances = M.Datepicker.init(elems, options);
        });


        function fechaFormat(t) {
            console.log(t);
            if (t.value.length == 4) { console.log("4"); t.value = t.value + "-";  }
            if (t.value.length == 7) { console.log("7");t.value = t.value + "-";   }
        
        }
    </script>
</asp:Content>

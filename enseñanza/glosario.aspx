<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/bulmaCSS/basic.master" CodeFile="glosario.aspx.cs" Inherits="enseñanza_glosario" %>
<%@ Register Src="~/enseñanza/uc_Header.ascx" TagName="menuPrincipal" TagPrefix="UI" %>


<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <UI:menuPrincipal ID="UI_MenuPrincipal" runat="server" />
    <div class="section">
        <div class="container">
            <div class="columns">
                <div class="column is-four-fifths">
                    <h1 class="title is-1">Glosario </h1>
                    <p>
                    </p>
                </div>
                <div id="AdminOptions" visible="false" runat="server" class="column">
                    <asp:HyperLink class="button" NavigateUrl="~/enseñanza/glosario-admin.aspx" runat="server"><span>Administrar </span>
                     <span class="icon is-small"> <i class="fas fa-cog"></i></span>
                    </asp:HyperLink>
                </div>
            </div>
            <div class="columns is-multiline">
                <div class="column is-full ">
                    <div class="control has-icons-left has-icons-right">

                        <asp:TextBox ID="txt_buscador" class="input is-small" OnTextChanged="txt_buscador_TextChanged" AutoPostBack="true" placeholder="buscador" runat="server"></asp:TextBox>
                        <span class="icon is-small is-left">
                            <i class="fas fa-search"></i>

                        </span><span class="icon is-small is-right">
                            <asp:LinkButton ID="btn_restablecer_búsqueda" OnClick="btn_restablecer_búsqueda_Click" class="delete  is-small" runat="server"></asp:LinkButton>

                        </span>
                    </div>
                </div>
            </div>
            <div class="column">
                <nav class="pagination is-small   is-centered" role="navigation" aria-label="pagination">

                    <ul id="menu_abecedario" runat="server" class="pagination-list">
                    </ul>
                </nav>
            </div>

            <div class="columns is-multiline ">
                              <div class="column is-full ">


                <asp:ListView ID="lv_términos" OnItemDataBound="lv_términos_DataBound" runat="server">
                    <LayoutTemplate>
                        <div class="table-container">
                            <table id="table-1" class="table is-striped" >
                                <thead class="has-background-grey	">
                                    <tr>

                                        <th class="has-text-white-ter">Término</th>
                                        <th class="has-text-white-ter">Descripción</th>
                                        <th class="has-text-white-ter">Inglés</th>
                                        <th class="has-text-white-ter">Símbolo</th>
                                    </tr>
                                    
                                </thead>
                                <div id="itemPlaceholder" runat="server">

                            </table>



                        </div>
                    </LayoutTemplate>

                    <ItemTemplate>
                        <tr>
                            <td>
                                <strong><%# Eval("término") %></strong>
                            </td>
                            <td>
                                <%# Eval("descripción") %>
                            </td>
                            <td>

                                <%# Eval("términoInglés") %>
                            </td>
                            <td>
                                <%# Eval("simbolo") %>
                            </td>
                        </tr>
                    </ItemTemplate>

                </asp:ListView>

            </div>
        </div></div>
    </div>
 
<script src="https://unpkg.com/sticky-table-headers"></script>
    <script>
        $('table').stickyTableHeaders();

    </script>
</asp:Content>


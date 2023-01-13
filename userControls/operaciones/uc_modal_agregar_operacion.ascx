<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_modal_agregar_operacion.ascx.cs" Inherits="uc_modal_agregar_operacion" %>

<style>
    .modal {
        width: 90% !important;
    }
</style>

<div id="mdl_agregarOperacion" runat="server" visible="false" class="modal mdl_agregarOperacion no-autoinit">
    <div class="modal-content">
        <asp:UpdatePanel ID="up_operacionesAdd" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hf_numero_parte" runat="server" />
                <asp:HiddenField ID="hf_intIndex_ListViewItemActive" runat="server" />
                <asp:HiddenField ID="hf_operacion_moneda" runat="server" />
                <asp:HiddenField ID="hf_tipo_operacion" runat="server" />
                <div class="row" style="border-left: solid 4px #2196f3;">
                    <div class="col s6 m9 l9 ">
                        <h3 class="blue-text margin-b-1x margin-t-2x">
                            <asp:Label ID="lbl_nombre_operacion" runat="server"></asp:Label>
                        </h3>
                        <p class="is-m-0">
                            <asp:Label ID="lbl_numero_operacion" runat="server"></asp:Label>
                        </p>
                        <p class="is-m-0">
                            Moneda: <strong>
                                <asp:Label ID="lbl_moneda" CssClass="blue-text" runat="server"></asp:Label>
                            </strong>
                        </p>
                        <p class="is-m-0">
                            Nombre: <strong>
                                <asp:Label ID="lbl_cliente_nombre" CssClass="blue-text" runat="server"></asp:Label></strong>
                        </p>
                    </div>
                    <div class="col s6 m3 l3 right-align">
                        <asp:LinkButton ID="btn_agregarOperacion" OnClientClick=" $('.mdl_agregarOperacion').modal('close');" OnClick="btn_agregarOperacion_Click" runat="server">Agregar</asp:LinkButton>
                    </div>
                </div>
                <div class="row">
                    <table>
                        <thead>
                            <tr>
                                <th class="is-text-center">No. Parte</th>
                                <th class="is-text-left">Descripción </th>
                                <th class="is-text-center">Cantidad</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td class="is-text-center">
                                    <asp:Label ID="lbl_numero_parte" runat="server"></asp:Label>
                                </td>
                                <td class="is-text-left">
                                    <asp:Label ID="lbl_descripcion_corta" runat="server"></asp:Label>
                                </td>
                                <td class="is-text-center is-w-1_4">
                                    <asp:TextBox ID="txt_cantidadCarrito" onchange=" $('.mdl_agregarOperacion').modal('close');" OnTextChanged="txt_cantidadCarrito_TextChanged" AutoPostBack="true" Text="1" TextMode="Number" Type="Integer" min="1" class="is-w-1_2" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="row">
                    <h2 class="blue-text margin-b-1x margin-t-2x">Cotizaciones</h2>
                    <p>Últimas 15 cotizaciones creadas:</p>
                    <asp:ListView ID="lv_cotizaciones" ClientIDMode="AutoID" OnItemDataBound="lv_cotizaciones_OnItemDataBound" runat="server">
                        <LayoutTemplate>
                            <div class="row">
                                <ul class="collection with-header">
                                    <div runat="server" id="itemPlaceholder"></div>
                                </ul>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <asp:HiddenField ID="hf_indexItem" Value='<%#  Container.DisplayIndex %>' runat="server" />
                            <li id="liItem" class="collection-item" runat="server">
                                <asp:HiddenField ID="hf_id_cotizacionSQL" Value='<%# Eval("id") %>' runat="server" />
                                <div class="is-flex is-justify-between is-items-center">
                                    <div class="is-flex is-justify-start is-items-center">
                                        <p class="is-px-4 is-select-all">
                                            <asp:Label ID="lbl_cotizacion_numero_operacion" Text='<%# Eval("numero_operacion") %>' runat="server"></asp:Label>
                                        </p>
                                        <p class="is-px-2">
                                            <asp:Label ID="lbl_cotizacion_nombre_operacion" Text='<%# Eval("nombre_cotizacion") %>' runat="server"></asp:Label>
                                        </p>
                                        <p class="is-px-2">[<asp:Label ID="lbl_cotizacion_moneda" Text='<%# Eval("monedaCotizacion") %>' runat="server"></asp:Label>]</p>
                                        <p class="is-px-4">
                                            <asp:Label ID="lbl_cotizacion_cliente_nombre" Text='<%# Eval("cliente_nombre") %>' runat="server"></asp:Label>
                                        </p>
                                        <p class="is-px-4">
                                            <asp:HyperLink ID="link_cotizacion" Target="_blank" runat="server">Ver cotización</asp:HyperLink>
                                        </p>
                                    </div>
                                    <asp:LinkButton ID="btn_seleccionarOperacion" OnClick="btn_seleccionarOperacion_Click" class="btn btn-s waves-effect waves-light blue-grey-text text-darken-2 blue-grey lighten-5 secondary-content" runat="server">Seleccionar</asp:LinkButton>
                                </div>
                            </li>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <div class="row center-align">
                                <h2>No tienes cotizaciones activas disponibles.</h2>
                                <h3>Crea una cotización para agregar productos a ella.</h3>
                                <label>Nombre de tu cotizacion</label>
                                <asp:TextBox ID="txt_nombreCotización" runat="server"></asp:TextBox>
                                <asp:LinkButton ID="btn_crearCotizacionEnBlanco" OnClick="btn_crearCotizacionEnBlanco_Click"
                                    class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5 right-align tooltipped"
                                    data-tooltip="Crear cotización" runat="server">
                                     <i class="material-icons right">assignment</i> Crear cotización</a>
                                </asp:LinkButton>
                            </div>
                        </EmptyDataTemplate>
                    </asp:ListView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="modal-footer">
        <a href="#!" class="modal-action modal-close waves-effect waves-green btn-flat">Cerrar</a>
    </div>
</div>

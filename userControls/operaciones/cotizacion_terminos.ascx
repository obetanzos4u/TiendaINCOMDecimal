<%@ Control Language="C#" AutoEventWireup="true" CodeFile="cotizacion_terminos.ascx.cs" Inherits="uc_cotizacion_terminos" %>


<asp:HiddenField ID="hf_numero_operacion" runat="server" />
<asp:HiddenField ID="hf_visibleInfo" runat="server" />

<asp:UpdatePanel ID="up_visibleInfo" UpdateMode="Conditional" runat="server">
    <ContentTemplate>
        <ol>
            <li>
                <asp:Label ID="lbl_TerminoTiempoEntrega" runat="server"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lbl_TerminoFormaDePago" runat="server"></asp:Label>
            </li>
            <li>
                <asp:Label ID="lbl_TerminoEntrega" runat="server"></asp:Label>
            </li>
        </ol>
        <a id="btn_cotizacionTerminos" runat="server"   class="waves-effect waves-light btn-small blue-grey-text text-darken-2 blue-grey lighten-5  modal-trigger"
          data-target="modal_editar_cotizacion_terminos"  >Editar condiciones
      <i class="material-icons right">edit</i>
        </a>
    </ContentTemplate>
</asp:UpdatePanel>
<!-- Modal Structure -->
<div id="modal_editar_cotizacion_terminos" class="modal">
    <div class="modal-content">
        <asp:UpdatePanel ID="up_cotizacion_terminos" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <h2 class="left-align">Condiciones de cotización</h2>
                <div class="row">
                    <h3>Tiempo de entrega</h3>
                    <asp:HiddenField ID="hf_TiempoDeEntrega" runat="server" />
                    <div class="col s12 m12 l12 xl12 input-field">
                        <asp:RadioButtonList ID="chk_FechaTiempoEntrega" OnSelectedIndexChanged="chk_FechaTiempoEntrega_SelectedIndexChanged" AutoPostBack="true" class="resetRadio"
                            RepeatLayout="Flow"
                            RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Value="0" Text="Inmediato en almacén"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Por confirmar"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Otro (establecer)"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col s12 m12 l7 xl4 input-field">
                        <asp:TextBox ID="txt_numeroFechaTiempoEntrega" placeholder="Ejemplo/Número: 1,2,3" Text="1" Visible="false" runat="server"></asp:TextBox>
                        <asp:DropDownList ID="ddlTipoFecha" Visible="false" runat="server">
                            <asp:ListItem Value="días" Text="Días"></asp:ListItem>
                            <asp:ListItem Value="semana(s)" Text="semana(s)">Semana(s)</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <h3>Forma de pago</h3>
                    <asp:HiddenField ID="hf_FormaDePago" runat="server" />
                    <div class="col s12 m12 l7 xl4 input-field">
                        <asp:DropDownList ID="ddl_FormaDePago" runat="server">
                            <asp:ListItem Value="0" Text="Anticipo al 100%"></asp:ListItem>
                            <asp:ListItem Value="1" Text="50% anticipo para generar pedido y el otro 50% al aviso de entrega"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Crédito a 15 días"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Crédito a 20 días"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Crédito a 30 días"></asp:ListItem>
                            <asp:ListItem Value="5" Text="Crédito a 90 días"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <h3>Entrega</h3>
                    <asp:HiddenField ID="hf_Entrega" runat="server" />
                    <div class="col s12 m12 l6 xl12 input-field">
                        <asp:DropDownList ID="ddl_Entrega" runat="server">
                            <asp:ListItem Value="1" Text="En una sola exhibición"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Parcialidades"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col s12 m12 l12 xl12 input-field">
                        <asp:LinkButton ID="btn_GuardarTerminosCotizacion" OnClientClick="btnLoading(this);$('#modal_editar_cotizacion_terminos').modal('close');"
                            CssClass="waves-effect waves-light btn blue" OnClick="btn_GuardarTerminosCotizacion_Click" runat="server"
                            Text="Guardar"></asp:LinkButton>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_GuardarTerminosCotizacion"
                    EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div class="modal-footer">
        <a href="#!" class="is-text-red is-p-4">Cerrar</a>
    </div>
</div>

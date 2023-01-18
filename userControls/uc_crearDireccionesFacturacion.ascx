<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_crearDireccionesFacturacion.ascx.cs" Inherits="uc_crearDireccionesFacturacion" %>
<%@ Register TagPrefix="uc" TagName="ddlPaises" Src="~/userControls/ddl_paises.ascx" %>
<%@ Register TagPrefix="uc" TagName="ddlEstados" Src="~/userControls/ddl_estados.ascx" %>

<div class="is-bt-5 is-mx-6 is-border-soft is-rounded-xl is-p-8">
    <div class="row">
        <div class="col l12">
            <h2 class="is-text-center is-m-0 is-bt-1 is-font-bold is-text-black-soft">Nueva dirección de facturación</h2>
        </div>
    </div>
    <div class="row">
        <div class="col s12 m12 l12">
            <h4>Crear una dirección de facturación:</h4>
        </div>
    </div>
    <div class="row">
        <div class="col s12 m12 l12">
            <div class="input-field col s12 m6 l6 crear_facturacion-nombre_direccion">
                <asp:TextBox ID="txt_nombre_direccion" ClientIDMode="Static" CssClass="validate" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                <label for="txt_nombre_direccion">Asigna un alías como referencia a esta dirección </label>
                <i class="is-text-sm">Ejemplo: Almacén, casa, oficina, etc.</i>
            </div>
            <div class="input-field col s12 m6 l6 crear_facturacion-razon_social">
                <asp:TextBox ID="txt_razon_social" ClientIDMode="Static" CssClass="validate" data-length="150" MaxLength="150" runat="server"></asp:TextBox>
                <label for="txt_razon_social">Razón social</label>
                <i class="is-text-sm">Ejemplo: Insumos Comerciales de Occidente</i>
            </div>
        </div>
        <div class="col s12 m12 l12">
            <div class="input-field col s12 m6 l6 crear_facturacion-rfc">
                <asp:TextBox ID="txt_rfc" ClientIDMode="Static" CssClass="validate" data-length="15" MaxLength="15" runat="server"></asp:TextBox>
                <label for="txt_numero">RFC</label>
            </div>
            <div class="col s12 m6 l6 crear_facturacion-regimen_fiscal">
                <label for="ddl_regimen_fiscal">Régimen fiscal:</label>
                <asp:DropDownList ID="ddl_regimen_fiscal" AutoPostBack="true" ClientIDMode="Static" runat="server">
                    <asp:ListItem Selected="True" Value="">Seleccionar</asp:ListItem>
                    <asp:ListItem Value="601">601 - General de Ley Personas Morales</asp:ListItem>
                    <asp:ListItem Value="603">603 - Personas Morales con Fines no Lucrativos</asp:ListItem>
                    <asp:ListItem Value="605">605 - Sueldos y Salarios e Ingresos Asimilados a Salarios</asp:ListItem>
                    <asp:ListItem Value="606">606 - Arrendamiento</asp:ListItem>
                    <asp:ListItem Value="607">607 - Régimen de Enajenación o Adquisición de Bienes</asp:ListItem>
                    <asp:ListItem Value="608">608 - Demás ingresos</asp:ListItem>
                    <asp:ListItem Value="610">610 - Residentes en el Extranjero sin Establecimiento Permanente en México</asp:ListItem>
                    <asp:ListItem Value="611">611 - Ingresos por Dividendos (socios y accionistas)</asp:ListItem>
                    <asp:ListItem Value="612">612 - Personas Físicas con Actividades Empresariales y Profesionales</asp:ListItem>
                    <asp:ListItem Value="614">614 - Ingresos por intereses</asp:ListItem>
                    <asp:ListItem Value="615">615 - Régimen de los ingresos por obtención de premios</asp:ListItem>
                    <asp:ListItem Value="616">616 - Sin obligaciones fiscales</asp:ListItem>
                    <asp:ListItem Value="620">620 - Sociedades Cooperativas de Producción que optan por diferir sus ingresos</asp:ListItem>
                    <asp:ListItem Value="621">621 - Incorporación Fiscal</asp:ListItem>
                    <asp:ListItem Value="622">622 - Actividades Agrícolas, Ganaderas, Silvícolas y Pesqueras</asp:ListItem>
                    <asp:ListItem Value="623">623 - Opcional para Grupos de Sociedades</asp:ListItem>
                    <asp:ListItem Value="624">624 - Coordinados</asp:ListItem>
                    <asp:ListItem Value="625">625 - Régimen de las Actividades Empresariales con ingresos a través de Plataformas Tecnológicas</asp:ListItem>
                    <asp:ListItem Value="626">626 - Régimen Simplificado de Confianza</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col s12 m12 l12">
            <div class="input-field col s12 m12 l5 crear_facturacion-calle">
                <asp:TextBox ID="txt_calle" ClientIDMode="Static" CssClass="validate" data-length="50" MaxLength="50" runat="server"></asp:TextBox>
                <label for="txt_calle">Calle</label>
            </div>
            <div class="input-field col s12 m12 l2 crear_facturacion-numero">
                <asp:TextBox ID="txt_numero" ClientIDMode="Static" CssClass="validate" data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                <label for="txt_numero">Número</label>
            </div>

            <div class="input-field col s12 m12 l5 crear_facturacion-colonia">
                <asp:TextBox ID="txt_colonia" ClientIDMode="Static" CssClass="validate" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                <label for="txt_colonia">Colonia</label>
            </div>
        </div>
        <div class="col s12 m12 l12">
            <div class="input-field col s12 m12 l4 crear_facturacion-delegacion_municipio">
                <asp:TextBox ID="txt_delegacion_municipio" ClientIDMode="Static" CssClass="validate" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                <label for="txt_delegacion_municipio">Delegación/Municipio</label>
            </div>
            <div class="input-field col s12 m12 l2 crear_facturacion-pais">
                <label for="txt_pais" style="display: contents">Pais:</label>
                <uc:ddlPaises ID="ddl_pais" runat="server" />
            </div>
            <div id="cont_txt_estado" class="input-field col s12 m12 l4 crear_facturacion-estado" runat="server">
                <asp:TextBox ID="txt_estado" ClientIDMode="Static" CssClass="validate" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                <label for="txt_estado">Estado</label>
            </div>
            <div id="cont_ddl_estado" class="input-field col s12 m12 l2" visible="false" runat="server">
                <uc:ddlEstados ID="ddl_estado" runat="server" />
                <label for="ddl_estado">Estado</label>
            </div>
            <div class="input-field col s12 m12 l2 crear_facturacion-codigo-postal">
                <asp:TextBox ID="txt_codigo_postal" ClientIDMode="Static" CssClass="validate" runat="server"></asp:TextBox>
                <label for="txt_codigo_postal">Código Postal</label>
            </div>
        </div>
    </div>
    <div class="is-flex is-justify-around is-items-center">
        <a href="<%= ResolveUrl("~/usuario/mi-cuenta/direcciones-de-facturacion.aspx") %>"
            class="is-text-white is-btn-gray">Volver a direcciones de facturaccion
        </a>
        <asp:LinkButton ID="btn_crear_direccion" class="is-text-white is-btn-blue"
            Style="text-transform: none;"
            OnClick="btn_crear_direccion_Click" runat="server">
                        Guardar
        </asp:LinkButton>
    </div>
</div>
<script>
    $(document).ready(function () {
        $('select').material_select();
    });
</script>

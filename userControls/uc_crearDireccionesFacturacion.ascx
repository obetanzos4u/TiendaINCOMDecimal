<%@ Control Language="C#" AutoEventWireup="true"  CodeFile="uc_crearDireccionesFacturacion.ascx.cs" Inherits="uc_crearDireccionesFacturacion" %>
<%@ Register TagPrefix="uc" TagName="ddlPaises"  Src="~/userControls/ddl_paises.ascx" %>
<%@ Register TagPrefix="uc" TagName="ddlEstados"  Src="~/userControls/ddl_estados.ascx" %>

    <div class="is-bt-5 is-mx-6 is-border-soft is-rounded-xl is-p-8">
        <div class="row">
            <div class="col l12">
                <h2 class="is-text-center is-m-0">Facturación</h2> 
            </div>
        </div>
        <div class="row">
            <div class="col s12 m12 l12">
                <h4>Crear una dirección de facturación</h4>
            </div>
          
            
                  
        </div>
        <div class="row ">
             <div class="col s12 m12 l12 ">
                <div class="input-field col s12 m12 l6">
                      <asp:TextBox ID="txt_nombre_direccion" ClientIDMode="Static" CssClass="validate"  data-length="20" MaxLength="20" runat="server"></asp:TextBox>
                    <label for="txt_nombre_direccion">Asigna un nombre corto como referencia a esta dirección </label>
                    <i>Ejemplo: Incom</i>
                </div></div>


                        <div class="col s12 m12 l12">
                <div class="input-field col s12 m12 l8">
                    <asp:TextBox ID="txt_razon_social" ClientIDMode="Static" CssClass="validate"  data-length="150" MaxLength="150" runat="server"></asp:TextBox>
                       <label for="txt_razon_social">Razón social</label>
                      <i>Ejemplo: Insumos Comerciales de Occidente S.A. de C.V.</i>
                </div>
                <div class="input-field col s12 m12 l4">
                    <asp:TextBox ID="txt_rfc" ClientIDMode="Static" CssClass="validate" data-length="15" MaxLength="15"  runat="server"></asp:TextBox>
                    <label for="txt_numero">RFC</label>
                </div>

               
            </div>
             </div>
          <div class="row">
            <div class="col s12 m12 l12">
                <div class="input-field col s12 m12 l4">
                    <asp:TextBox ID="txt_calle" ClientIDMode="Static" CssClass="validate"  data-length="50" MaxLength="50" runat="server"></asp:TextBox>
                    <label for="txt_calle">Calle</label>
                </div>
                <div class="input-field col s12 m12 l3">
                    <asp:TextBox ID="txt_numero" ClientIDMode="Static" CssClass="validate" data-length="20" MaxLength="20"  runat="server"></asp:TextBox>
                    <label for="txt_numero">Número</label>
                </div>

                <div class="input-field col s12 m12 l5">
                    <asp:TextBox ID="txt_colonia" ClientIDMode="Static" CssClass="validate" data-length="35" MaxLength="35"  runat="server"></asp:TextBox>
                    <label for="txt_colonia">Colonia</label>
                </div>
            </div>
            <div class="col s12 m12 l12">
                <div class="input-field col s12 m12 l5">
                    <asp:TextBox ID="txt_delegacion_municipio" ClientIDMode="Static" CssClass="validate" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                    <label for="txt_delegacion_municipio">Delegación/Municipio</label>
                </div>

                                       <div class="input-field col s12 m12 l4">
                    <uc:ddlPaises ID="ddl_pais" runat="server" />
                    <label for="txt_pais">Pais</label>
                </div>
                <div id="cont_txt_estado" class="input-field col s12 m12 l3" runat="server">
                    <asp:TextBox ID="txt_estado" ClientIDMode="Static" CssClass="validate" data-length="35" MaxLength="35" runat="server"></asp:TextBox>
                    <label for="txt_estado">Estado</label>
                </div>



                <div id="cont_ddl_estado" class="input-field col s12 m12 l3" visible="false" runat="server">
                    <uc:ddlEstados ID="ddl_estado" runat="server" />
                    <label for="ddl_estado">Estado</label>

                </div>

            </div>
            <div class="col s12 m12 l12">
                <div class="input-field col s12 m12 l5">
                    <asp:TextBox ID="txt_codigo_postal" ClientIDMode="Static" CssClass="validate" runat="server"></asp:TextBox>
                    <label for="txt_codigo_postal">Código Postal</label>

                </div>
            </div>
            
        </div>
        <div class="row">
            <div class="col s12 m12 l12">
                <div class="input-field col s12 m12 l12">
                    <a href="<%= ResolveUrl("~/usuario/mi-cuenta/direcciones-de-facturacion.aspx") %>" 
                        class="waves-effect waves-light btn blue-grey darken-1">Regresar</a>
                    <asp:LinkButton ID="btn_crear_direccion" class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5" 
                  OnClick="btn_crear_direccion_Click"
                        runat="server">
                                    <i class="material-icons right">add</i> Crear dirección de facturación</asp:LinkButton>
                    
                </div>
            </div>
        </div>
         <div class="row"></div>
    </div>
    <script>
        $(document).ready(function () {
            $('select').material_select();
        });
    </script>
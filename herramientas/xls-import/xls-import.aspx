<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/herramientas/_masterConfiguraciones.master" Async="true" CodeFile="xls-import.aspx.cs"
    Inherits="herramientas_xls_import_xls_import" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <h1 class="center-align">Productos XLS</h1>
    <div ID="up_xlsImportt"  class="container" ">
            <div class="row">
                <div class="col s12 m6 l4">
                    <label>Nombre de la tabla destino SQL </label>
                    <asp:TextBox ID="txt_tabla" Visible="false" runat="server"></asp:TextBox>
                    <asp:DropDownList ID="ddl_tablaDestino" runat="server">
                        <asp:ListItem Value="productos_Datos" Text="Datos de productos"></asp:ListItem>
                        <asp:ListItem Value="productos_roles" Text="Roles de productos"></asp:ListItem>
                        <asp:ListItem Value="precios_rangos" Text="Rangos de precios"></asp:ListItem>
                        <asp:ListItem Value="categorias" Text="Categorias/Menú"></asp:ListItem>
                        <asp:ListItem Value="productos_marcas" Text="Marcas (productos personalizados)"></asp:ListItem>
                        <asp:ListItem Value="productos_unidades" Text="Unidades (productos personalizados)"></asp:ListItem>
                        <asp:ListItem Value="precios_ListaFija" Text="Lista fija precios"></asp:ListItem>
                        <asp:ListItem Value="precios_multiplicador" Text="Multiplicadores de acuerdo al rango"></asp:ListItem>
                          <asp:ListItem Value="reportesCampos" Text="Campos SQL de los reportes (Solo administrador)"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div id="content_chk_numeroHoja" runat="server" class="col s12 m6 l4">
                    ¿El nombre de la hoja es igual que la tabla destino? 
                     <div class="switch">
                         <label>
                             Off
      <asp:CheckBox ID="chk_numeroHoja" OnCheckedChanged="chk_numeroHoja_CheckedChanged"
          Checked="true" ClientIDMode="Static" AutoPostBack="true" runat="server" />
                             <span class="lever"></span>
                             On
                         </label>
                     </div>



                </div>
                <div id="content_txt_numeroHoja" runat="server" visible="false" class="col s12 m6 l4">
                    <label>Nombre de la hoja en el archivo XLS a importar</label> 
                    <asp:LinkButton ID="btn_cancelarNombreHoja" OnClick="btn_cancelarNombreHoja_Click" runat="server" >Cancelar</asp:LinkButton>
                    <asp:TextBox ID="txt_numeroHoja" runat="server"></asp:TextBox>
                </div>
                <div class="col s12 m6 l4">
                    <label>Aplicar regla y validación</label>
                    <asp:DropDownList ID="ddl_validacion" runat="server">
                        <asp:ListItem Value="" Text="Ninguno"></asp:ListItem>
                        <asp:ListItem Value="productos_Datos" Text="Datos de productos"></asp:ListItem>

                    </asp:DropDownList>
                </div>
                <div class="col s12 m6 l4">
                    <label>Referencia actualización (Solo en actualización)</label>
                    <asp:TextBox ID="txt_referencia" runat="server"></asp:TextBox>
                </div>
            </div>

            <!---
        Id:  -->
            <asp:TextBox ID="txt_id" Visible="false" runat="server"></asp:TextBox>
            <div class="row">
                <label>Carga tu archivo xlsx</label>
                <div class="file-field input-field ">
                    <div class="btn waves-effect waves-light blue-grey-text text-darken-2 blue-grey lighten-5 ">
                        <span>Elegir archivo</span>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </div>

                    <div class="file-path-wrapper">
                        <input class="file-path validate" type="text"
                            placeholder="Upload file" />
                    </div>
                </div>
            </div>
          <div class="row">
               ¿Desea eliminar la tabla destino? Solo funciona al insertar registros, no en actualización. Elimina el contenido y agrega el nuevo.
                     <div class="switch">
                         <label>
                             Off
      <asp:CheckBox ID="chk_eliminar_tabla_destino"  
          Checked="false" ClientIDMode="Static"   runat="server" />
                             <span class="lever"></span>
                             On
                         </label>
                     </div>


          </div>
            <div class="row center-elign">
                <asp:Button ID="btnImport" OnClientClick="if ( ! Insertar()) return false;" class="waves-effect waves-light btn red" runat="server" Text="Insertar" OnClick="ImportExcel" />
                <asp:Button ID="btnUpdate" OnClientClick="if ( ! Actualización()) return false;" class="waves-effect waves-light btn blue"
                    Visible="true" runat="server" Text="Actualizar" OnClick="UpdateExcel" />

            </div>
            <div class="row">
                <h4>Log de resultado</h4>
                <asp:TextBox TextMode="MultiLine" ID="txt_log" CssClass="materialize-textarea" runat="server">

                </asp:TextBox>
            </div>
      
           
    </div>
    <script>
        function Actualización() {
            return confirm("Confirma que deseas ACTUALIZAR registros.");
        }

        function Insertar() {
            return confirm("Confirma que deseas INSERTAR registros.");
        }
    </script>
</asp:Content>




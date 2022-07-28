<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/bulmaCSS/basic.master" CodeFile="glosario-admin.aspx.cs" Inherits="enseñanza_glosario_admin" %>
<%@ Register Src="~/enseñanza/uc_Header.ascx" TagName="menuPrincipal" TagPrefix="UI" %>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

    <UI:menuPrincipal ID="UI_MenuPrincipal" runat="server" />

        <div class="section">  <div class="container">
    <div class="columns ">
        <div class="column is-full">
            <h1 class="title is-1">Administrador de Glosario</h1>
        </div>
    </div>
    <div class="columns ">
        <div class="column is-four-fifths">
            <p>Selecciona un archivo y cargalo, se borrará el contenido actual por el contenido del archivo.</p>
            <p>Las columnas <strong>[término] y [descripción] </strong>son obligatorias, la hoja que se carga debe nombrarse <strong>[glosario]</strong></p>
            <br /><p>Actualmente hay <strong><asp:Label ID="lbl_registrosActuales" class="has-text-info" runat="server"></asp:Label></strong> términos en la base de datos.</p>
        </div>
        <div class="column">
            <strong>Columnas:</strong><ul>
                <li>[término]* 100 max</li>
                <li>[términoInglés] 100 max</li>
                <li>[descripción]* 800 max</li>
                <li>[simbolo] 25 max</li>
                <li>[link] 250 </li>
            </ul>
        </div>
    </div>

    <div class="columns ">
        <div class="column">
            <div class="file has-name is-fullwidth">
                <label class="file-label">
                    <asp:FileUpload ID="FileUpload" ClientIDMode="Static" class="file-input" runat="server" />
                    <span class="file-cta">
                        <span class="file-icon">
                            <i class="fas fa-upload"></i>
                        </span>
                        <span class="file-label">Selecciona un archivo
                        </span>
                    </span>
                    <span class="file-name">Selecciona
                    </span>
                </label>
            </div>

        </div>
        <div class="column">
            <asp:Button ID="btn_subirGlosario" class="button is-link" OnClick="btn_subirGlosario_Click" runat="server" Text="Subir" />
        </div>
    </div>
        <div class="columns ">
        <div class="column is-full">
            <p>Resultado:</p>
            <asp:TextBox ID="txt_log" class="textarea"  TextMode="MultiLine" runat="server"></asp:TextBox>
        </div>
    </div></div></div>
    <script>
          document.addEventListener("DOMContentLoaded", function (event) {
              fileUpload("#FileUpload", ".file-name");
          });



    </script>
</asp:Content>

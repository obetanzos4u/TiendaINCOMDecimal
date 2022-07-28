<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/bulmaCSS/basic.master" CodeFile="infografía-admin.aspx.cs" Inherits="enseñanza_infografía_admin" %>
<%@ Register Src="~/enseñanza/uc_Header.ascx" TagName="menuPrincipal" TagPrefix="UI" %>
<%@ Register Src="~/enseñanza/menu-admin-infografías.ascx" TagName="menu" TagPrefix="admin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
<UI:menuPrincipal ID="UI_MenuPrincipal" runat="server" />

    <div class="section">
        <div class="container">
            <div class="columns is-multiline">
                <div class="column is-four-fifths">
                    <h1 class="title is-1">Crear infografía</h1>
                    <p>
                    </p>
                </div>
                <div id="AdminOptions" runat="server" class="column">
                    <a class="button" href="infografía-admin.aspx" ><span>Crear</span>
                        <span class="icon is-small"><i class="fas fa-plus"></i></span>
                    </a>
                </div>
                                 <div class="column is-full ">
                     <admin:menu  ID="menuInfografias" runat="server" />
                 </div>
            </div>
            <div class="columns">
                <div class="column is-full">
                    <div class="field">
                        <div class="control">
                            <div class="control">
                                <label>Título</label>
                                <asp:TextBox ID="txt_titulo" class="input is-primary is-medium" MaxLength="100" placeholder="Título" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="field">
                        <div class="control">
                            <label>Descripción</label>
                            <asp:TextBox ID="txt_descripción" TextMode="MultiLine" Height="60px" class="input" MaxLength="100" placeholder="Descripción" runat="server"></asp:TextBox>

                        </div>
                    </div>
                    <div class="field">    <label>Archivo de imagen (miniatura)</label>  <div class="control">
                        <div class="file has-name is-fullwidth">                       
                            <label class="file-label">
                                <asp:FileUpload  ID="file_miniatura" ClientIDMode="Static" class="file-input" runat="server" />
                          
                                <span class="file-cta">
                                    <span class="file-icon">
                                        <i class="fas fa-upload"></i>
                                    </span>
                                    <span class="file-label">Selecciona miniatura
                                    </span>
                                </span>
                                <span id="file-name-miniatura" class="file-name">
                                </span>
                            </label>
                        </div>

                   </div>


                    </div>
                    <div class="field">
                        <label>Infografía [pdf/magen]</label>
                        <div class="control">
                            <div class="file has-name is-fullwidth">
                                <label class="file-label">
                                    <asp:FileUpload ID="file_infografía" ClientIDMode="Static" class="file-input" runat="server" />

                                    <span class="file-cta">
                                        <span class="file-icon">
                                            <i class="fas fa-upload"></i>
                                        </span>
                                        <span class="file-label">Selecciona infografía
                                        </span>
                                    </span>
                                    <span id="file-name-infografia" class="file-name"></span>
                                </label>
                            </div>
                        </div>



                    </div>
                     
                    <div class="field">
                        <div class="control">
 
                            <asp:Button ID="btn_crearInfografía" OnClick="btn_crearInfografía_Click" class="button is-link" runat="server" Text="Subir infografía" />

                        </div>
                       
                    </div>
                    <div id="contentResultado"></div>
                </div>
            </div>
        </div>
        </div>
    <script>
        document.addEventListener("DOMContentLoaded", function (event) {
            fileUpload("#file_miniatura", "#file-name-miniatura");

            fileUpload("#file_infografía", "#file-name-infografia");

        });

    </script>
</asp:Content>

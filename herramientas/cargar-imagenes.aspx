<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/herramientas/_masterConfiguraciones.master" Async="true" 
    CodeFile="cargar-imagenes.aspx.cs"
  Inherits="herramientas_configuraciones_cargar_imagenes" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <h1 class="center-align">Carga de imágenes tienda</h1>
    <h3 class="center-align">Powered by IT4U development</h3>
    <div class="container">
        <div class="row"> <div class="col s12 m6 l4 xl2">
            <h2 class="c">Selecciona un destino o módulo</h2>
            <p>
                <asp:DropDownList ID="ddlActionOption" ClientIDMode="Static" runat="server">
                    <asp:ListItem Value="" Selected="True" disabled>Selecciona</asp:ListItem>
                     <asp:ListItem Value="\img\firmas\">Firmas</asp:ListItem>
                    <asp:ListItem Value="\img\webUI\categorias\">Categorias</asp:ListItem>
                    <asp:ListItem Value="\img\webUI\sliderCatalogos\">Catálogos slider</asp:ListItem>
                    <asp:ListItem Value="\img_catalog\">Productos Large</asp:ListItem>
                    <asp:ListItem Value="\img_catalog\min\">Productos Small</asp:ListItem>
                    <asp:ListItem Value="\img_catalog\personalizado\">Productos / Personalizado</asp:ListItem>
                    <asp:ListItem Value="\documents\pdf\">PDF Fichas técnicas </asp:ListItem>
                    <asp:ListItem Value="\documents\promos\">PDF Promos </asp:ListItem>
                    <asp:ListItem Value="\documents\">PDF (documents)</asp:ListItem>
                    <asp:ListItem Value="\documents\pdf \personalizado\">PDF / Personalizado</asp:ListItem>
                </asp:DropDownList>
        </div></div>
        <div class="row">
            <div class="col s12">
                <ul class="tabs">
                    <li class="tab col s3"><a href="#ContentCarga">Cargar</a></li>
                    <li class="tab col s3"><a href="#ContentEliminar">Eliminar</a></li>

                </ul>
            </div>
            <div id="ContentCarga" class="col s12">
                <p>Carga los elementos definidos, si los archivos ya existen los reemplaza.</p>

                <asp:FileUpload ID="fu_CargaImagenes" AllowMultiple="true" runat="server" />
                <div class="col s12 margin-t-4x">
                    <p style="padding: 5px 10px; display: inline-block;" class="grey lighten-3">
                        <strong>Aviso:</strong> Las imágenes se cambiarán a la extensión .webp
                    </p>
                    <br />
                    <asp:LinkButton ID="btn_CargarImagenes"  OnClientClick="return confirm('Seguro?');"
                        OnClick="btn_CargarImagenes_Click" ClientIDMode="Static" class="btn blue" runat="server">Cargar</asp:LinkButton>
                </div>
            </div>
            <div id="ContentEliminar" class="row">
                <p>Elimina archivo</p>
                 <div class="col s12">
                     Buscar nombres de archivos
                     <asp:TextBox ID="txt_text_buscar_archivos" ClientIDMode="Static" runat="server"></asp:TextBox>
 
                     <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                         <ContentTemplate>
                             <p style="padding: 5px 10px; display: inline-block;" class="grey lighten-3">
                        <strong>Aviso:</strong> Solo traerá 70 resultados máximo.
                    </p><br />
                             <asp:LinkButton ID="btn_buscar_archivos" ClientIDMode="Static" class="btn blue" OnClick="btn_buscar_archivos_Click" runat="server">Buscar archivos</asp:LinkButton>
                         </ContentTemplate>
                         <Triggers>
                             <asp:AsyncPostBackTrigger ControlID="btn_buscar_archivos" EventName="Click" />
                         </Triggers>
                     </asp:UpdatePanel>
                 </div>
                <div class="col s12">

                    <asp:UpdatePanel ID="up_ListadoDeArchivos" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <h3>Resultados 70 de
                                <asp:Literal ID="lt_cantidad_archivos_encontrados" runat="server"></asp:Literal></h3>
                            <asp:ListView ID="Lv_ListadoDeArchivos" OnItemDataBound="Lv_ListadoDeArchivos_ItemDataBound" runat="server">
                                <LayoutTemplate>
                              
                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                  
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <div class="col s12 m4 l4 xl2 ">
                                        <div class="card">
                                            <div class="card-image"  >
                                                <asp:Image ID="imgFile"  
                                                    data-tooltip="<%# (Container.DataItem as dynamic).FileName %>" class="responsive-img materialboxed" runat="server" />
                                            </div>
                                            <div class="card-content truncate" style="height: 100px;position: relative;background: white;">
                                                <asp:Label ID="lbl_file_name" class="title" data-tooltip="<%# (Container.DataItem as dynamic).FileName %>" runat="server">
                                             <strong>  <%# (Container.DataItem as dynamic).FileName %></strong>
                                                </asp:Label>
                                                <p class="hide " style="font-size: small">
                                                    <asp:Label ID="lbl_file_path" runat="server" Text="<%# (Container.DataItem as dynamic).FilePath %>">
                                                         
                                                    </asp:Label>

                                                </p>
                                            </div>
                                            <asp:UpdatePanel ID="up_ListadoDeArchivos" class="card-action" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="btn_eliminar" OnClick="btn_eliminar_Click" runat="server" class="btn red">Eliminar
                                                  <i class="material-icons left">delete</i>
                                                    </asp:LinkButton>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btn_eliminar" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                           
                                        </div>
                                    </div>

                                </ItemTemplate>

                            </asp:ListView>
                        </ContentTemplate>

                    </asp:UpdatePanel>

                </div>

            </div>

        </div>
        <div class="row">
            <div class="col s12">
                <h3>Log de resultados</h3>
                <asp:TextBox ID="txt_log_carga_imagenes" class="materialize-textarea" TextMode="MultiLine" Style="height: 100px;" runat="server">

                </asp:TextBox>
    </div></div>
    </div>
    <script>
        let ddlActionOption = document.querySelector("#ddlActionOption");
        let btn_CargarImagenes = document.querySelector("#btn_CargarImagenes");
        let btn_EliminarImagenes = document.querySelector("#btn_EliminarImagenes");

        ddlActionOption.addEventListener("change", function () {

            let selectedText = ddlActionOption.options[ddlActionOption.selectedIndex].text;
            console.log(selectedText);
            if (selectedText !== "") {
                btn_CargarImagenes.textContent = "Cargar > " + selectedText;
              //  btn_EliminarImagenes.textContent = " > " + selectedText;

                btn_CargarImagenes.setAttribute("onclick", `return confirm('Confirma cargar a ${selectedText} ?');`);
            } else {
              //  btn_EliminarImagenes.textContent ="No haz seleccionada destino";
            }
           
        });
 
        var txt = document.querySelector("#txt_text_buscar_archivos");
        var btn = document.querySelector("#btn_buscar_archivos");

        txt.addEventListener("keyup", function (event) {
            event.preventDefault();
            if (event.keyCode === 13) {
                console.log("apretaste enter");
                btn.click();
            }
        });

        var elems = document.querySelectorAll('.tooltipped');
        var instances = M.Tooltip.init(elems, null);

    </script>
</asp:Content>




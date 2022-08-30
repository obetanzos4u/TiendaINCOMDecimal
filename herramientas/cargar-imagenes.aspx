<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/herramientas/_masterConfiguraciones.master" Async="true"
    CodeFile="cargar-imagenes.aspx.cs"
    Inherits="herramientas_configuraciones_cargar_imagenes" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="is-container">
        <div class="is-flex is-justify-center is-items-center is-w-full">
            <div class="is-w-4_5">
                <h2 class="is-font-semibold is-select-none is-px-2">Carga multimedia</h2>
                <div class="is-px-4">
                    <div class="is-flex is-justify-start is-items-center">
                        <p class="is-pr-2">Seleccionar módulo: </p>
                        <div class="is-w-1_3">
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
                        </div>
                    </div>
                    <div class="is-py-4">
                        <div class="is-flex is-flex-wrap is-justify-center is-items-center ">
                            <div id="uploadTab" class="is-w-1_2 is-text-center is-py-2 is-bg-gray-300 hover:is-bg-gray-300  is-transition is-duration-700 is-rounded-t-lg is-non-active">
                                <a class="is-w-full is-inline-block is-text-black is-font-semibold is-cursor-pointer is-py-1 hover:is-text-white is-transition is-duration-500" onclick="cargarImagenesSection();">Subir</a>
                            </div>
                            <div id="deleteTab" class="is-w-1_2 is-text-center is-py-2 is-bg-gray-300 hover:is-bg-gray-300  is-transition is-duration-700 is-rounded-t-lg is-non-active">
                                <a class="is-w-full is-inline-block is-text-black is-font-semibold is-cursor-pointer is-py-1 hover:is-text-white is-transition is-duration-500" onclick="eliminarImagenesSection();">Eliminar</a>
                            </div>
                        </div>
                        <div id="ContentCarga" class="is-hidden">
                            <div class="is-container is-p-4">
                                <p class="is-px-4 is-italic is-select-none"><span class="is-font-semibold is-non-italic">&#9432;</span> Si los archivos existen, serán reemplazados.</p>
                                <div class="is-flex is-justify-center is-items-center">
                                    <%--                                    <label for="top_contenido_fu_CargaImagenes" class="is-w-1_2 is-h-8 is-inline-block is-text-center is-py-8 is-bg-white is-cursor-pointer is-rounded-lg is-border is-border-black">
                                        Seleccionar archivos
                                        <asp:FileUpload ID="fu_CargaImagenes" AllowMultiple="true" class="is-hidden" accept="image/jpeg, image/png, image/jpg" runat="server" />
                                    </label>--%>
                                    <asp:FileUpload ID="fu_CargaImagenes" AllowMultiple="true" class="is-custom-input-file" accept="image/jpeg, image/png, image/jpg" data-multiple-caption="{count} archivos seleccionados" runat="server" />
                                    <label for="top_contenido_fu_CargaImagenes" id="lbl_cargaImagenes">Seleccionar archivos</label>
                                </div>
                                <div class="is-flex is-justify-end is-items-center">
                                    <p class="is-inline-block is-px-2 is-select-none">
                                        <strong>Aviso:</strong> Las imágenes JPG se duplicarán en WEBP.
                                    </p>
                                </div>
                                <div class="is-flex is-justify-end is-items-center">
                                    <asp:LinkButton ID="btn_CargarImagenes" OnClientClick="return confirm('Se cargarán las imágenes elegidas.');" OnClick="btn_CargarImagenes_Click" ClientIDMode="Static" class="is-text-white is-font-semibold is-rounded is-bg-blue-500 hover:is-bg-blue-600 is-px-4 is-py-1 is-transition is-duration-300" runat="server">Subir</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div id="ContentEliminar">
                            <div class="is-container is-p-4">
                                <div>
                                    <p class="is-px-4 is-italic is-select-none"><span class="is-font-semibold is-non-italic">&#9432;</span> Solamente se mostrarán <strong>100</strong> archivos.</p>
                                    <div class="is-py-2">
                                        <div class="is-flex is-justify-center is-items-center">
                                            <p class="is-px-2 is-font-semibold">Buscar: </p>
                                            <asp:TextBox ID="txt_text_buscar_archivos" ClientIDMode="Static" style="background-color: white; padding: 0 0.5rem" runat="server"></asp:TextBox>
                                        </div>
                                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                            <contenttemplate>
                                                <div class="is-flex is-justify-end is-items-center is-py-2">
                                                    <asp:LinkButton ID="btn_buscar_archivos" ClientIDMode="Static" class="is-text-white is-font-semibold is-rounded is-bg-blue-500 hover:is-bg-blue-600 is-px-4 is-py-1 is-transition is-duration-300" OnClick="btn_buscar_archivos_Click" runat="server">Buscar</asp:LinkButton>
                                                </div>
                                            </contenttemplate>
                                            <triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btn_buscar_archivos" EventName="Click" />
                                            </triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="is-grid is-col-10" style="border: 2px solid blue;">
                                    <asp:UpdatePanel ID="up_ListadoDeArchivos" UpdateMode="Conditional" runat="server">
                                        <contenttemplate>
                                            <asp:Literal ID="lt_cantidad_archivos_encontrados" runat="server"></asp:Literal>
                                            <asp:ListView ID="Lv_ListadoDeArchivos" OnItemDataBound="Lv_ListadoDeArchivos_ItemDataBound" runat="server">
                                                <layouttemplate>
                                                    <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                                                </layouttemplate>
                                                <itemtemplate>
                                                    <div class="is-inline-block borderTest">
                                                        <div class="">
                                                            <div>
                                                                <asp:Image ID="imgFile" data-tooltip="<%# (Container.DataItem as dynamic).FileName %>" class="" style="width: 100px; height: 100px" runat="server" />
                                                            </div>
                                                            <div class="">
                                                                <asp:Label ID="lbl_file_name" class="title" data-tooltip="<%# (Container.DataItem as dynamic).FileName %>" runat="server">
                                                                    <strong><%# (Container.DataItem as dynamic).FileName %></strong>
                                                                </asp:Label>
                                                                <p class="hide" style="font-size: small">
                                                                    <asp:Label ID="lbl_file_path" runat="server" Text="<%# (Container.DataItem as dynamic).FilePath %>"></asp:Label>
                                                                </p>
                                                            </div>
                                                            <asp:UpdatePanel ID="UpdatePanel2" class="" UpdateMode="Conditional" runat="server">
                                                                <contenttemplate>
                                                                    <asp:LinkButton ID="btn_eliminar" OnClick="btn_eliminar_Click" runat="server" class="">
                                                                        Eliminar
                                                                    </asp:LinkButton>
                                                                </contenttemplate>
                                                                <triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="btn_eliminar" EventName="Click" />
                                                                </triggers>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </itemtemplate>
                                            </asp:ListView>
                                        </contenttemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="is-w-full is-py-4">
                            <div>
                                <h4 class="is-select-none">Registro de eventos</h4>
                                <asp:TextBox ID="txt_log_carga_imagenes" onfocus="Resize(this);" ClientIDMode="Static" class="form-control is-resize-none" ReadOnly="true" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <%--    <div class="container">--%>
    <%--        <div class="row">
            <div class="col s12 m6 l4 xl2">
                <h2 class="c">Selecciona un destino o módulo</h2>
                <p>--%>
    <%--                <asp:DropDownList ID="ddlActionOption" ClientIDMode="Static" runat="server">
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
                </asp:DropDownList>--%>
    <%--            </div>
        </div>--%>
    <%--        <div class="row">
            <div class="col s12">
                <ul class="tabs">
                    <li class="tab col s3"><a href="#ContentCarga">Cargar</a></li>
                    <li class="tab col s3"><a href="#ContentEliminar">Eliminar</a></li>
                </ul>
            </div>

        </div>--%>
    <%--        <div class="row">
            <div class="col s12">
                <h3>Log de resultados</h3>
                <asp:TextBox ID="txt_log_carga_imagenes" class="materialize-textarea" TextMode="MultiLine" Style="height: 100px;" runat="server"></asp:TextBox>
            </div>
        </div>--%>
    <%--    </div>--%>
    <script>
        const ddlActionOption = document.querySelector("#ddlActionOption");
        const btn_CargarImagenes = document.querySelector("#btn_CargarImagenes");
        const btn_EliminarImagenes = document.querySelector("#btn_EliminarImagenes");
        const contentCarga = document.querySelector("#ContentCarga");
        const contentEliminar = document.querySelector("#ContentEliminar");
        const uploadTab = document.querySelector("#uploadTab");
        const deleteTab = document.querySelector("#deleteTab");
        const inputFiles = document.querySelector("#top_contenido_fu_CargaImagenes");
        const inputFilesSpan = document.querySelector("#lbl_cargaImagenes");

        const cargarImagenesSection = () => {
            contentCarga.classList.remove("is-hidden");
            contentEliminar.classList.add("is-hidden");
            contentCarga.classList.add("is-bg-gray-100");
            uploadTab.classList.remove("is-non-active");
            uploadTab.classList.add("is-active")
            deleteTab.classList.remove("is-active");
            deleteTab.classList.add("is-non-active");
        };

        const eliminarImagenesSection = () => {
            contentEliminar.classList.remove("is-hidden");
            contentCarga.classList.add("is-hidden");
            contentEliminar.classList.add("is-bg-gray-100");
            deleteTab.classList.remove("is-non-active");
            deleteTab.classList.add("is-active");
            uploadTab.classList.remove("is-active");
            uploadTab.classList.add("is-non-active");
        };

        inputFiles.addEventListener("input", () => {
            const filesList = Array.from(inputFiles.files);
            let fileName = "";
            if (filesList.length > 1) {
                fileName = inputFiles.getAttribute("data-multiple-caption").replace("{count}", filesList.length);
                console.log(fileName);
            }
            else {
                fileName = filesList[0].name;
                console.log(fileName);
            }

            if (fileName !== "") {
                inputFilesSpan.innerHTML = fileName;
            }
        });

        ddlActionOption.addEventListener("change", () => {
            const selectedText = ddlActionOption.options[ddlActionOption.selectedIndex].text;
            if (selectedText !== "") {
                btn_CargarImagenes.textContent = "Subir en " + selectedText;
                btn_CargarImagenes.setAttribute("onclick", `return confirm('Las imágenes se subiran a ${selectedText}');`);
            }
        });

        const txt = document.querySelector("#txt_text_buscar_archivos");
        const btn = document.querySelector("#btn_buscar_archivos");

        txt.addEventListener("keyup", (event) => {
            event.preventDefault();
            if (event.keyCode === 13) {
                btn.click();
            }
        })

        const elems = document.querySelectorAll('.tooltipped');
        const instances = M.Tooltip.init(elems, null);
    </script>
</asp:Content>

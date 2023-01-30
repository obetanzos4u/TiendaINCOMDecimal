<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    EnableEventValidation="false"
    Async="true" CodeFile="configuraciones-home-principal.aspx.cs"
    MasterPageFile="~/herramientas/_masterConfiguraciones.master" Inherits="herramientas_configuraciones_home_principal" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <h1 class="title-banner_avisos center-align"> Gestor de Banner y Avisos</h1>
    <div class="section">
        <div class="container-configuraciones_home">
            <div class="row is-bt-4">
                <div class="center-align col s12 l12">
                    <a class="waves-effect waves-light modal-trigger is-btn-blue is-text-center" href="#modal_agregar_slider">Agregar slider</a>
                </div>
            </div>
            <div class="row">
                <asp:UpdatePanel ID="up_Lv_Slider" UpdateMode="Conditional" class="col s12  m12 l12 margin-t-4x" runat="server">
                    <contenttemplate>
                        <asp:ListView ID="lv_imagenes" OnItemDataBound="lv_imagenes_ItemDataBound" runat="server">
                            <layouttemplate>
                                <div runat="server" id="itemPlaceholder"></div>
                            </layouttemplate>
                            <itemtemplate>
                                <asp:HiddenField ID="hf_idSlider" Value='<%#Eval("id")%>' runat="server" />
                                <div class="col s12  m6 l6 xl4 wrapper_card-configuraciones-home">
                                    <div class="card card-configuraciones-home is-flex is-flex-col">
                                        <div class="card-image configuraciones-home">
                                            <asp:Image ID="imgSlider" ImageUrl='<%# configuracion_sliders.directorioSliderRelativo + Eval("nombreArchivo") %>' runat="server" />
                                            <asp:Label ID="lbl_tituloSlider" class="card-title" runat="server" Text=""></asp:Label>

                                            <asp:LinkButton ID="btn_editSliderModal" OnClick="btn_editSliderModal_Click"
                                                class="btn-floating halfway-fab waves-effect waves-light blue"
                                                style="border-radius: 10px; padding-top: 8px; height: 38px;"
                                                runat="server">
                                                    <img class="icon-edit-blue" src="/img/webUI/newdesign/Edit-white.png">
                                            </asp:LinkButton>
                                        </div>
                                        <div class="card-content is-flex is-flex-col" style="flex: 1;">
                                            <h2 class="" style="font-size: 16px !important; font-weight: 600; margin-top: 0; margin-bottom: 1rem;">
                                                <%#  string.IsNullOrWhiteSpace(Eval("titulo").ToString()) ? "----" : Eval("titulo").ToString() %>
                                            </h2>
                                            Descripción:
                                            <span class="is-bg-gray-100 is-bt-1">
                                                <%#  string.IsNullOrWhiteSpace(Eval("descripcion").ToString()) ? "----" : Eval("descripcion").ToString() %>
                                            </span>
                                            <br>
                                            <br>
                                            Nombre del archivo:
                                            <asp:Label ID="lbl_descripcion" class="is-bg-gray-100 is-block is-bt-1" runat="server" style="font-size: 14px; line-height: 1; word-wrap: break-word;"
                                                Text='<%#  Eval("nombreArchivo") %>'></asp:Label>
                                            Link:
                                            <span class="nota truncate is-font-normal is-bg-gray-100 is-bt-1" style="margin-left: 0;">
                                                <%#  string.IsNullOrWhiteSpace(Eval("link").ToString()) ? "----" : Eval("link").ToString() %>
                                            </span>
                                            <div style="border-top: 1px solid rgba(160,160,160,0.2);
                                            border-bottom: 1px solid rgba(160,160,160,0.2);">
                                                <asp:LinkButton ID="btn_eliminarSlider"
                                                    class="eliminar_slider-configuraciones_home is-top-1 is-bt-1"
                                                    OnClick="btn_eliminarSlider_Click" runat="server">
                                                Eliminar Slider
                                                </asp:LinkButton>
                                            </div>
                                            <div class="card-action card-avisos is-flex" style="flex: 1; align-items: end; padding: 0; border-top: none">
                                                <div class="switch">
                                                    <label class="is-text-black">
                                                        Desactivado
                                                        <asp:CheckBox ID="chk_activo" OnCheckedChanged="chk_activo_CheckedChanged"
                                                        AutoPostBack="true" runat="server" />
                                                        <span class="lever"></span>
                                                        Activado
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </itemtemplate>

                            <emptydatatemplate>
                                <div class="row">
                                    <div class="center-align col s12 l12 xl12">
                                        <h3 class="center-align">Aún no hay Slider Activos </h3>
                                        <a class="waves-effect waves-light btn blue-grey lighten-5 blue-grey-text text-darken-4 modal-trigger"
                                            href="#modal_agregar_slider"><i class="left large material-icons">slideshow</i> Agregar</a>
                                    </div>
                                </div>
                            </emptydatatemplate>
                            <edititemtemplate>
                                <h2>No hay slideres </h2>
                            </edititemtemplate>
                        </asp:ListView>
                    </contenttemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <hr>
    <div class="cargar_imagenes_home">
        <div class="is-flex is-justify-center">
            <div class="card-galeria-imagenes">
                    <h2 class="is-text-center">Cargar imágenes a la galería</h2>
                <div class="cargar-imagenes is-flex is-w-full is-m-auto is-justify-center is-w-fit is-items-baseline">
                    <div class="is-btn-gray" style="margin-right: 2rem;">
                        <span>Elegir imagen</span>
                    </div>
                    <div class="file-field input-field" style="margin-right: 1.5rem;">
                        <asp:FileUpload ID="fu_imagenSlider" runat="server" />
                        <div class="file-path-wrapper" style="padding-left: 0;">
                            <input class="file-path validate" type="text" style="width: 90%; margin-top: 1rem; padding-left: 1rem;"
                                placeholder="Nombre del archivo" />
                        </div>
                    </div>
                    <div class="is-btn-blue">
                        <asp:LinkButton CssClass="is-text-white"
                            ID="btn_cargarImagenSlider" OnClick="btn_cargarImagenSlider_Click" runat="server">
                            Cargar
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="row avisos-wrapper">
            <asp:ListView ID="lv_galeriaDeImagenes" runat="server">
                <layouttemplate>
                    <div runat="server" id="itemPlaceholder"></div>
                </layouttemplate>
                <itemtemplate>
                    <asp:HiddenField ID="hf_imgFileName" Value='<%#Eval("Value")%>' runat="server" />
                    <div class="col s12  m6 l6 xl4">
                        <div class="card avisos_card-borde">
                            <div id="home-avisos_card-image" class="card-image" style="overflow: auto;">
                                <asp:Image ID="imgSlider" ImageUrl='<%# configuracion_sliders.directorioSliderRelativo + Eval("Value") %>' runat="server" />
                                <asp:Label ID="lbl_tituloSlider" class="card-title" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="card-content" style="padding: 0rem 0rem 1rem 1rem; word-break: break-all;">
                                <asp:Label ID="lbl_descripcion" style="font-size: 14px; height: 60px;
                                display: flex; align-items: center;"  runat="server"
                                    Text='<%#  Eval("Value") %>'></asp:Label>
                            </div>
                            <div class="card-action" style="padding: 1rem;">
                                <asp:LinkButton ID="btn_eliminar_imagenh" class="eliminar_slider-configuraciones_home" style="color: rgb(240, 76, 76); text-transform: capitalize;" OnClick="btn_eliminar_imagenh_Click" runat="server">Eliminar Slider</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </itemtemplate>

                <emptydatatemplate>
                    <div class="row">
                        <div class="center-align col s12 l12 xl12">
                            <h3 class="center-align">Aún no hay Slider Activos </h3>
                            <a class="waves-effect waves-light btn blue-grey lighten-5 blue-grey-text text-darken-4 modal-trigger"
                                href="#modal_agregar_slider"><i class="left large material-icons">slideshow</i> Agregar</a>
                        </div>
                    </div>
                </emptydatatemplate>
                <edititemtemplate>
                    <h2>No hay imagenes cargadas </h2>
                </edititemtemplate>
            </asp:ListView>
        </div>
    </div>
    <!-- Modal Agregar Slider -->
    <div id="modal_agregar_slider" class="modal is-rounded-lg">
        <div class="modal-content is-m-0">
            <div class="is-flex is-justify-between is-items-center">
                <h2 class="is-text-lg is-font-semibold is-select-none">Agregar nuevo slider</h2>
                <a href="#!" class="modal-action modal-close is-text-black">
                    <svg class="is-w-6 is-h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12"></path>
                    </svg>
                </a>
            </div>
            <div class="row">
                <div class="is-flex is-justify-between is-items-center is-px-4">
                    <label for="<%=txt_titulo.ClientID %>" class="is-text-base is-pr-4">Título: </label>
                    <div class="is-w-4_5">
                        <asp:TextBox ID="txt_titulo" placeholder="Título de la imagen" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="is-flex is-justify-between is-items-center is-px-4">
                    <label for="<%=txt_descripcion.ClientID %>" class="is-text-base is-pr-4">Descripción:</label>
                    <div class="is-w-4_5">
                        <asp:TextBox ID="txt_descripcion" placeholder="Descripción de la imagen" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="is-flex is-justify-between is-items-center is-px-4">
                    <label for="<%=ddl_imagen.ClientID %>" class="is-text-base is-pr-4">Imagen: </label>
                    <div class="is-w-4_5">
                        <asp:DropDownList ID="ddl_imagen" class="selectize-select browser-default" runat="server"></asp:DropDownList>
                    </div>
                </div>
                <hr class="is-w-4_5 is-my-2" />
                <div class="is-flex is-justify-between is-items-center is-px-4">
                    <label for="<%=txt_link.ClientID %>" class="is-text-base is-pr-4">Enlace: </label>
                    <div class="is-w-4_5">
                        <asp:TextBox ID="txt_link" placeholder="Enlace al que dirigirá el slider" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="is-flex is-justify-between is-items-center is-px-4">
                    <label for="<%=ddl_duracion.ClientID %>" class="is-text-base is-pr-4">Duración: </label>
                    <div class="is-w-4_5">
                        <asp:DropDownList ID="ddl_duracion" runat="server">
                            <asp:ListItem Value="1" Text="Fijo"></asp:ListItem>
                            <asp:ListItem Value="2000" Text="2 seg."></asp:ListItem>
                            <asp:ListItem Value="3000" Text="3 seg."></asp:ListItem>
                            <asp:ListItem Value="4000" Text="4 seg."></asp:ListItem>
                            <asp:ListItem Value="5000" Text="5 seg."></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="is-flex is-justify-between is-items-center is-px-4">
                    <label for="<%=ddl_posicion.ClientID %>" class="is-text-base is-pr-4">Posicion: </label>
                    <div class="is-w-4_5">
                        <asp:DropDownList ID="ddl_posicion" runat="server">
                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                            <asp:ListItem Value="5" Text="5"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <hr class="is-w-4_5 is-my-2" />
                <div class="is-flex is-flex-col is-justify-between is-items-start is-px-4">
                    <label for="<%=txt_opciones.ClientID %>" class="is-text-base">Opcional: Ingresar "_blank" para que el enlace se abra en nueva pestaña.</label>
                    <asp:TextBox ID="txt_opciones" TextMode="SingleLine" runat="server"></asp:TextBox>
                </div>
                <div class="is-flex is-justify-center is-items-center is-py-2">
                    <label>
                        <asp:CheckBox ID="chk_activa" runat="server" />
                        <span for="<%=chk_activa.ClientID %>">Activo</span>
                    </label>
                </div>
                <asp:UpdatePanel ID="up_agregarSlider" UpdateMode="Conditional" class="is-flex is-justify-center is-items-center is-py-2" runat="server">
                    <contenttemplate>
                        <asp:LinkButton ID="btn_agregarSlider" CssClass="is-btn-blue" OnClick="btn_agregarSlider_Click" runat="server">Agregar</asp:LinkButton>
                    </contenttemplate>
                    <triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_agregarSlider" EventName="Click" />
                    </triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Modal Editar Slider -->
    <div id="modal_editar_slider" class="modal bottom-sheet">
        <div class="modal-content">
            <h4>Editar Slider</h4>
            <asp:HiddenField ID="hf_idSliderEdit" Value='<%#Eval("id")%>' runat="server" />
            <div class="row">
                <div class="input-field col s12 m6 l6">
                    <asp:TextBox ID="txt_tituloEdit" runat="server"></asp:TextBox>
                    <label for="<%=txt_tituloEdit.ClientID %>">Título:</label>
                </div>
                <div class="input-field col s12 m6 l6">
                    <asp:TextBox ID="txt_descripcionEdit" runat="server"></asp:TextBox>
                    <label for="<%=txt_descripcionEdit.ClientID %>">Descripción:</label>

                </div>
                <div class="input-field col s12 m12 l6">
                    <asp:DropDownList ID="ddl_imagenEdit" class="selectize-select browser-default " runat="server"></asp:DropDownList>
                    <label for="<%=ddl_imagenEdit.ClientID %>">Imagen:</label>

                </div>
                <div class="input-field  col s12 m12 l12">
                    <asp:TextBox ID="txt_linkEdit" runat="server"></asp:TextBox>
                    <label for="<%=txt_linkEdit.ClientID %>">Link:</label>
                </div>

                <div class="  col s6 m4 l2">
                    <asp:DropDownList ID="ddl_duracionEdit" runat="server">
                        <asp:ListItem Value="2000" Text="2 seg."></asp:ListItem>
                        <asp:ListItem Value="3000" Text="3 seg."></asp:ListItem>
                        <asp:ListItem Value="3000" Text="4 seg."></asp:ListItem>
                        <asp:ListItem Value="5000" Text="5 seg."></asp:ListItem>

                    </asp:DropDownList>
                    <label for="<%=ddl_duracionEdit.ClientID %>">Duración</label>
                </div>
                <div class="  col s6 m3 l2">
                    <asp:DropDownList ID="ddl_posicionEdit" runat="server">
                        <asp:ListItem Value="1" Text="1"></asp:ListItem>
                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                        <asp:ListItem Value="3" Text="3"></asp:ListItem>
                        <asp:ListItem Value="4" Text="5"></asp:ListItem>
                        <asp:ListItem Value="5" Text="5"></asp:ListItem>
                    </asp:DropDownList>
                    <label for="<%=ddl_posicionEdit.ClientID %>">Posicion</label>
                </div>
                <div class="input-field col s12 m12 l12 l12 ">
                    <span>Opciones: Usar "_blank" para enlace en nueva pestaña o dejar vacio para link en la misma pestaña.</span>
                    <asp:TextBox ID="txt_opcionesEdit" CssClass="materialize-textarea" TextMode="MultiLine" runat="server"></asp:TextBox>
                </div>
                <div class="col s12  m14 l4">
                    <label>
                        <asp:CheckBox ID="chk_activoEdit" runat="server" />
                        <span>Activo</span>
                        <br />
                        <br />
                    </label>
                </div>
                <div class="col s12  m112 l12">
                    <asp:UpdatePanel UpdateMode="Conditional" runat="server">
                        <contenttemplate>
                            <asp:LinkButton ID="btn_editarSlider" OnClientClick="  $('#modal_editar_slider').modal('close');" CssClass="is-btn-gray"
                                OnClick="btn_editarSlider_Click" runat="server">
                                Editar Slider</asp:LinkButton>
                        </contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_editarSlider" EventName="Click" />
                        </triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
        <div class="modal-footer">
            <a href="#!" class="modal-action modal-close waves-effect waves-green btn-flat">Cerrar</a>
        </div>
    </div>
</asp:Content>




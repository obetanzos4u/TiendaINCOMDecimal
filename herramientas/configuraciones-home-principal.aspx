<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true"
    EnableEventValidation="false"
    Async="true" CodeFile="configuraciones-home-principal.aspx.cs"
    MasterPageFile="~/herramientas/_masterConfiguraciones.master" Inherits="herramientas_configuraciones_home_principal" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <h1 class="center-align margin-b-2x">Avisos</h1>
    <div class="section">
        <div class="container">
            <div class="row">
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
                                <div class="col s12  m6 l6 xl4">
                                    <div class="card ">
                                        <div class="card-image">
                                            <asp:Image ID="imgSlider" ImageUrl='<%# configuracion_sliders.directorioSliderRelativo + Eval("nombreArchivo") %>' runat="server" />
                                            <asp:Label ID="lbl_tituloSlider" class="card-title" runat="server" Text=""></asp:Label>

                                            <asp:LinkButton ID="btn_editSliderModal" OnClick="btn_editSliderModal_Click"
                                                class="btn-floating halfway-fab waves-effect waves-light blue"
                                                runat="server">
                                                <i class="material-icons">edit</i></asp:LinkButton>
                                        </div>
                                        <div class="card-content">
                                            <h2 class="margin-b-2x margin-t-2x">
                                                <%#  string.IsNullOrWhiteSpace(Eval("titulo").ToString()) ? "----" : Eval("titulo").ToString() %>

                                            </h2>
                                            <p>
                                                <%#  string.IsNullOrWhiteSpace(Eval("descripcion").ToString()) ? "----" : Eval("descripcion").ToString() %>
                                            </p>

                                            <asp:Label ID="lbl_descripcion" runat="server"
                                                Text='<%#  Eval("nombreArchivo") %>'></asp:Label>

                                            <br />
                                            Link:
                                            <p class="nota truncate">
                                                <%#  string.IsNullOrWhiteSpace(Eval("link").ToString()) ? "----" : Eval("link").ToString() %>
                                            </p>
                                            <br />
                                            <asp:LinkButton ID="btn_eliminarSlider"
                                                OnClick="btn_eliminarSlider_Click" runat="server">
                                                Eliminar Slider</asp:LinkButton>

                                        </div>
                                        <div class="card-action">

                                            <div class="switch">
                                                <label>
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
    <div class="container">
        <div class="divider"></div>
    </div>
    <div class="section">
        <div class="row">

            <div class="col s12 l12">
                <h2>Galería de imágenes</h2>
            </div>
            <div class="col s12 l4">

                <div class="file-field input-field ">
                    <div class="btn btn-s waves-effect waves-light blue-grey-text text-darken-2 blue-grey lighten-5 ">
                        <span>Elegir imagen</span>
                        <asp:FileUpload ID="fu_imagenSlider" runat="server" />
                    </div>

                    <div class="file-path-wrapper">
                        <input class="file-path validate" type="text"
                            placeholder="Upload file" />
                    </div>
                </div>
            </div>
            <div class="col s12 l4">


                <asp:LinkButton CssClass="waves-effect waves-light btn blue-grey lighten-5 blue-grey-text text-darken-4"
                    ID="btn_cargarImagenSlider" OnClick="btn_cargarImagenSlider_Click" runat="server">
                    Subir imagen
                     <i class="left large material-icons">image</i>
                </asp:LinkButton>


            </div>

        </div>
        <div class="row">
            <asp:ListView ID="lv_galeriaDeImagenes" runat="server">
                <layouttemplate>
                    <div runat="server" id="itemPlaceholder"></div>
                </layouttemplate>
                <itemtemplate>
                    <asp:HiddenField ID="hf_imgFileName" Value='<%#Eval("Value")%>' runat="server" />
                    <div class="col s12  m6 l6 xl4">
                        <div class="card">
                            <div class="card-image">
                                <asp:Image ID="imgSlider" ImageUrl='<%# configuracion_sliders.directorioSliderRelativo + Eval("Value") %>' runat="server" />
                                <asp:Label ID="lbl_tituloSlider" class="card-title" runat="server" Text=""></asp:Label>


                            </div>
                            <div class="card-content">
                                <asp:Label ID="lbl_descripcion" runat="server"
                                    Text='<%#  Eval("Value") %>'></asp:Label>
                            </div>
                            <div class="card-action">
                                <asp:LinkButton ID="btn_eliminar_imagenh" OnClick="btn_eliminar_imagenh_Click" runat="server">Eliminar</asp:LinkButton>
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
                    <label for="<%=txt_descripcion.ClientID %>" class="is-text-base is-pr-4">Descripción: </label>
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

            <h4>Editar Slider Slider</h4>
            <asp:HiddenField ID="hf_idSliderEdit" Value='<%#Eval("id")%>' runat="server" />
            <div class="row">
                <div class="input-field col s12 m6 l6">
                    <asp:TextBox ID="txt_tituloEdit" runat="server"></asp:TextBox>
                    <label for="<%=txt_tituloEdit.ClientID %>">Titulo</label>
                </div>
                <div class="input-field col s12 m6 l6">
                    <asp:TextBox ID="txt_descripcionEdit" runat="server"></asp:TextBox>
                    <label for="<%=txt_descripcionEdit.ClientID %>">Descripción</label>

                </div>
                <div class="input-field col s12 m12 l6">
                    <asp:DropDownList ID="ddl_imagenEdit" class="selectize-select browser-default " runat="server"></asp:DropDownList>
                    <label for="<%=ddl_imagenEdit.ClientID %>">Imagen</label>

                </div>
                <div class="input-field  col s12 m12 l12">
                    <asp:TextBox ID="txt_linkEdit" runat="server"></asp:TextBox>
                    <label for="<%=txt_linkEdit.ClientID %>">Link</label>
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
                    <span>Opciones usar "_blank" para enlace en nueva pestaña, dejar vacio para link en la misma pestaña.</span>

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
                            <asp:LinkButton ID="btn_editarSlider" OnClientClick="  $('#modal_editar_slider').modal('close');" CssClass="waves-effect waves-light btn blue-grey lighten-5 blue-grey-text text-darken-4"
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




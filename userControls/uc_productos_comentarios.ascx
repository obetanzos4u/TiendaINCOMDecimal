<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_productos_comentarios.ascx.cs" Inherits="userControls_uc_productos_comentarios" %>


<div id="Content_ProductosComentarios" runat="server">
    <asp:HiddenField ID="hf_numero_parte" runat="server"></asp:HiddenField>
    <div class="col s12 m12 l12">
        <h2 id="encabezado" runat="server" class="margin-t-8x">Comentarios y opiniones</h2>
    </div>
    <div class="col s12 m12 l12">
        <asp:LoginView ID="LoginView1" runat="server">
            <AnonymousTemplate>
                <p>Inicia sesión para agregar un comentario  </p>
                <p>
                    <asp:LoginStatus ID="LoginStatus" class="waves-effect waves-light btn-small blue" ToolTip="Sesión de usuario" runat="server" LoginText="Iniciar Sesión"
                        LogoutText="Cerrar Sesión" />
                    ó 
             <a title="Crear cuenta" class="waves-effect waves-light btn-small blue" href='<%= ResolveUrl("~/registro-de-usuario.aspx") %>'>Crear cuenta</a>
            </AnonymousTemplate>
            <LoggedInTemplate>Si haz usado este producto, agrega una calificación de este producto.</LoggedInTemplate>

        </asp:LoginView>
    </div>
    <div class="col s12 m12 l12">

        <span id="myRating" runat="server" class="rating" data-stars="5" data-default-rating="0"></span>
        <p id="avisoLogin"></p>
    </div>
    <asp:UpdatePanel ID="up_agregar_comentarios" runat="server" RenderMode="Block" class="row">

        <ContentTemplate>
            <div class="col s12 m12 l12">
                <asp:TextBox ID="txt_calificacion" class="hide" runat="server"></asp:TextBox>

                <asp:TextBox ID="txt_comentario" class="materialize-textarea" placeholder="Escribe un comentario" TextMode="MultiLine" runat="server"></asp:TextBox>
            </div>


            <div class="col s12 m12 l12">
                <asp:LinkButton ID="btn_agregar_comentario" CssClass="btn blue" OnClick="btn_agregar_comentario_Click" runat="server">Agregar</asp:LinkButton>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_agregar_comentario" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="col s12 m12 l12">
        <asp:UpdatePanel ID="up_comentarios" runat="server">
            <ContentTemplate>
                <asp:ListView ID="lv_comentarios" OnItemDataBound="lv_comentarios_ItemDataBound" runat="server">
                    <LayoutTemplate>
                        <ul class="collection">
                            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
                        </ul>
                    </LayoutTemplate>
                    <ItemTemplate>

                        <li class="collection-item  avatar">
                            <i class="material-icons circle left ">account_circle</i>
                            <span class="title"><strong><%# Eval("nombreUsuario")%></strong></span>
                            <span class="title blue-text"><%# Eval("fechaComentario")%></span> <span class="hide">[<%# Eval("idComentario")%>]</span>
                            <div id="calificación" runat="server" class="  yellow-text text-darken-2"></div>
                            <p>

                                <%# Eval("comentario")%>
                            </p>


                            <!--  <i class="material-icons">grade</i> -->
                        </li>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        Sé el primero.</h3>
                    </EmptyDataTemplate>
                </asp:ListView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script>
        var r = new SimpleStarRating(document.getElementById('<%= myRating.ClientID  %>'));

        document.getElementById('<%= myRating.ClientID  %>').addEventListener('rate', function (e) {

            if ("<%= !HttpContext.Current.User.Identity.IsAuthenticated %>" == "True") {
                document.getElementById("avisoLogin").innerHTML = "<strong> Inicia sesión para poder calificar</strong>";
            }

            var calificacion = e.detail; //contains the rating

            var txt_calificacion = document.querySelector('#<%= txt_calificacion.ClientID  %>');
            txt_calificacion.value = calificacion;

            document.getElementById('<%= txt_comentario.ClientID  %>').focus();




        });


    </script>
</div>

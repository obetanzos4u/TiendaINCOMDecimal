<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/bulmaCSS/basic.master" CodeFile="infografías.aspx.cs" Inherits="enseñanza_infografías" %>
<%@ Register Src="~/enseñanza/uc_Header.ascx" TagName="menuPrincipal" TagPrefix="UI" %>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <UI:menuPrincipal ID="UI_MenuPrincipal" runat="server" />

    <div class="section">
        <div class="container">
            <div class="columns is-multiline">
                <div class="column is-four-fifths">
                    <h1 class="title is-1">Infografías </h1>
                    <p>
                    </p>
                </div>
                <div id="AdminOptions" visible="false" runat="server" class="column">
                    <asp:HyperLink class="button" NavigateUrl="~/enseñanza/infografía-admin.aspx" runat="server"><span>Administrar</span>
                     <span class="icon is-small"> <i class="fas fa-cog"></i></span>
                    </asp:HyperLink>
                </div>

            </div>
            <div class="columns is-multiline">
                <div class="column is-full ">
                    <div class="control has-icons-left has-icons-right">

                        <asp:TextBox ID="txt_buscador" class="input is-small"  AutoPostBack="true" placeholder="buscador" runat="server"></asp:TextBox>
                        <span class="icon is-small is-left">
                            <i class="fas fa-search"></i>

                        </span><span class="icon is-small is-right">
                            <asp:LinkButton ID="btn_restablecer_búsqueda"  class="delete  is-small" runat="server"></asp:LinkButton>

                        </span>
                    </div>
                </div>
                            <div class="column">
                <nav class="pagination is-small   is-centered" role="navigation" aria-label="pagination">

                    <ul id="menu_abecedario" runat="server" class="pagination-list">
                    </ul>
                </nav>
            </div>
              <div class="column is-full ">                   <div id="contentResultado"></div></div>
            </div>

            <div class="columns is-multiline ">

                <asp:ListView ID="lv_infografías" OnItemDataBound="lv_infografías_ItemDataBound" runat="server">

                    <ItemTemplate>
                        <asp:HiddenField ID="hf_idInfografia" Value='<%# Eval("id") %>' runat="server" />
                        <div class="column is-one-third">


                            <div class="card">
                                <div class="card-image">
                                    <figure class="image is-4by3">
                                       <asp:HyperLink ID="link_img_miniatura"  runat="server">
                                           <img src="/img/infografías/miniatura/<%# Eval("nombreImagenMiniatura") %>" title='<%# Eval("titulo") %>'
                                               alt='<%# Eval("titulo") %>' />

                                       </asp:HyperLink>
                                    </figure>
                                </div>
                                <div class="card-content">

                                    <div class="content">
                                         <asp:HyperLink ID="link_infografia"  runat="server">
                                            <h2 class="title is-4"><%# Eval("titulo") %></h2>
                                       </asp:HyperLink>
                                        <%# Eval("descripción") %>
                                     <br />   <time class="has-text-info" datetime="<%# Eval("fecha") %>"><%# Eval("fecha") %></time>
                                    </div>
                                </div>
                                 <footer id="admin_options" visible="false" class="card-footer" runat="server">
                                       
                                      <a href="infografía-admin-editar.aspx?idInfografia=<%# Eval("id") %>"class="card-footer-item">Editar</a>
                                     <asp:LinkButton id="btn_eliminar" OnClick="btn_eliminar_Click" 
                                         OnClientClick="return confirm('¿Quieres eliminar esta infografía?');" 

                                         runat="server" class="card-footer-item">Eliminar </asp:LinkButton>
                                      <a href="#" </a>
                                  </footer>  
                            </div>






                        </div>
                    </ItemTemplate>
                </asp:ListView>
            </div>
        </div>
    </div>


</asp:Content>

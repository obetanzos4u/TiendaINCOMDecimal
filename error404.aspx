<%@ Page Language="C#" AutoEventWireup="true"   Async="true" MasterPageFile="~/general.master" CodeFile="error404.aspx.cs" Inherits="error_404" %>
<%@ Register Src="~/userControls/categoriasTodas.ascx" TagName="categoriasTodas" TagPrefix="uc_cat" %>
 <asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="container">
        <div class="row">
            <h1 class="center-align hide"> ¯\_(ツ)_/¯</h1>
            <h1  class="center-align">Página no encontrada</h1>
            <h2 class="center-align">La dirección ha cambiado o no se encuentra disponible temporalmente. [404]</h2>
            <pre class="center-align" style="    letter-spacing: 3px;zoom: .8;">
                                     ▄              ▄    
                                    ▌▒█           ▄▀▒▌   
                                    ▌▒▒█        ▄▀▒▒▒▐   
                                   ▐▄█▒▒▀▀▀▀▄▄▄▀▒▒▒▒▒▐   
                                 ▄▄▀▒▒▒▒▒▒▒▒▒▒▒█▒▒▄█▒▐   
                               ▄▀▒▒▒░░░▒▒▒░░░▒▒▒▀██▀▒▌   
                              ▐▒▒▒▄▄▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▀▄▒▌  
                              ▌░░▌█▀▒▒▒▒▒▄▀█▄▒▒▒▒▒▒▒█▒▐  
                             ▐░░░▒▒▒▒▒▒▒▒▌██▀▒▒░░░▒▒▒▀▄▌ 
                             ▌░▒▒▒▒▒▒▒▒▒▒▒▒▒▒░░░░░░▒▒▒▒▌ 
                            ▌▒▒▒▄██▄▒▒▒▒▒▒▒▒░░░░░░░░▒▒▒▐ 
                            ▐▒▒▐▄█▄█▌▒▒▒▒▒▒▒▒▒▒░▒░▒░▒▒▒▒▌
                            ▐▒▒▐▀▐▀▒▒▒▒▒▒▒▒▒▒▒▒▒░▒░▒░▒▒▐ 
                             ▌▒▒▀▄▄▄▄▄▄▒▒▒▒▒▒▒▒░▒░▒░▒▒▒▌ 
                             ▐▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒░▒░▒▒▄▒▒▐  
                              ▀▄▒▒▒▒▒▒▒▒▒▒▒▒▒░▒░▒▄▒▒▒▒▌  
                                ▀▄▒▒▒▒▒▒▒▒▒▒▄▄▄▀▒▒▒▒▄▀   
                                  ▀▄▄▄▄▄▄▀▀▀▒▒▒▒▒▄▄▀     
                                     ▀▀▀▀▀▀▀▀▀▀▀▀        

            </pre>
           
        </div>

          <div class="row">
            <h1 class="center-align">Sin embargo, navega entre más de 2,500 productos</h1>
        </div>
        <div class="row">
            <uc_cat:categoriasTodas runat="server"></uc_cat:categoriasTodas>
        </div>
        </div>
</asp:Content>

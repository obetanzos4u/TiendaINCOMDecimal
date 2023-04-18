<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/bulmaCSS/basic.master" CodeFile="infografía.aspx.cs" Inherits="enseñanza_base" %>

<%@ Register Src="~/enseñanza/uc_Header.ascx" TagName="menuPrincipal" TagPrefix="UI" %>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

    <UI:menuPrincipal ID="UI_MenuPrincipal" runat="server" />
    <div class="section">
        <div class="container">
            <div class="columns">
                <div class="column">
                    <nav class="breadcrumb is-centered" aria-label="breadcrumbs">
                        <ul>
                            <li>
                                <asp:HyperLink ID="link_infografias" runat="server"></asp:HyperLink></li>
                            <li class="is-active">
                                <asp:HyperLink ID="link_infografiaActual"   aria-current="location" runat="server"></asp:HyperLink></li>

                        </ul>
                    </nav>
                    <h1 id="TituloInfografia" class="title is-1" runat="server"></h1>
                    <p>
                        <asp:Literal ID="lt_descripción" runat="server"></asp:Literal></p>
                    <div class="sharethis-inline-share-buttons"></div>
                    <br /><p>Si tienes alguna duda, comentarios no dudes en contactarnos.
                        <br />
                        <a target="_blank" class="button is-link" 
                            href="https://www.incom.mx/informacion/ubicacion-y-sucursales.aspx#contacto">
                            Contáctanos
                        </a>
                    </p>
                    <a>
                </div>
            </div>
        </div>
    </div>
    <div class="container">

        <asp:Panel ID="Content_Infografia" CssClass="ContentInfografiaIframe" runat="server"></asp:Panel>
    </div>
    <style>
        .ContentInfografiaIframe {
            display: flex;
            flex-direction: column;
            min-height: 80vh;
        }

            .ContentInfografiaIframe iframe {
                width: 100%;
                flex-grow: 1;
            }
    </style>
</asp:Content>



<!-- Google tag (gtag.js) -->
<script async src="https://www.googletagmanager.com/gtag/js?id=G-T66EBL710F"></script>
<script>
  window.dataLayer = window.dataLayer || [];
  function gtag(){dataLayer.push(arguments);}
  gtag('js', new Date());

  gtag('config', 'G-T66EBL710F');
</script>

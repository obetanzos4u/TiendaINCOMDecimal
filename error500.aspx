<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeFile="error500.aspx.cs" Inherits="error_500" %>

    <div class="row z-depth-1">

    <div class="col s12 m12 l12  center-align" style="padding: 12px 26px;">
        <a title="Incom Retail" href='<%= HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) %>'>
            <img src='<%=ResolveUrl("~/img/webUI/incom_retail_logo_header.png") %>'
            alt="Logo Incom"   title="Incom,  La ferretera de las telecomunicaciones" style="max-height: 2.5rem;" class="responsive-img" />

        </a>
       
    </div>

</div>
    <div class="container center-align">
        <div class="row">
            <h1>Error al procesar solicitud</h1>
            <p class="center-align"><a class="btn blue" href="<%= Request.Url.GetLeftPart(UriPartial.Authority) %>">Regresar al inicio</a></p>
        </div>
    </div>

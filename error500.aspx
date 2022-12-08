<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeFile="error500.aspx.cs" Inherits="error_500" %>

<div style="width: 100%; margin: 0;">
    <div style="display: flex; justify-content: center; max-width: 100%; padding: 0.5rem 0;">
        <a title="INCOM" href='<%= HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) %>'>
            <img src='<%=ResolveUrl("~/img/webUI/newdesign/Incom_nuevo.png") %>' alt="Logo Incom" title="INCOM - La ferretera de las telecomunicaciones" style="max-height: 3rem;" class="responsive-img" />
        </a>
    </div>
    <div style="display: flex; flex-direction: column; justify-content: center; align-items: center; padding: 1rem 0; margin: 2rem 0">
        <img src="https://www.incom.mx/img/webUI/newdesign/500.png" style="filter: drop-shadow(0 0 0.35rem rgba(0, 148, 255, 0.4));" alt="Error 500" />
        <h1 style="font-weight: 600; font-size: 2rem; font-family: monospace">Error interno del servidor</h1>
        <a style="text-decoration: none; font-size: 1.2rem" href="<%= Request.Url.GetLeftPart(UriPartial.Authority) %>">Regresar al inicio</a>
    </div>
</div>

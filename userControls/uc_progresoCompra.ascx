<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_progresoCompra.ascx.cs" Inherits="uc_progresoCompra" %>

<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div class="container-pay-process is-flex is-justify-evenly is-items-center is-select-none is-py-1">
            <div class="is-flex is-flex-col is-justify-center is-items-center">
                <span id="spn_resumen" runat="server">
                    <svg class="svg_resumen is-w-12 is-h-12" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z"></path>
                    </svg>
                </span>
                <p class="text-resumen">Confirma tu pedido</p>
            </div>
            <div>
                <span id="spn_resumen_puntos" runat="server">
                    <svg class="svg_resumen_puntos is-w-12 is-h-12" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 12h.01M12 12h.01M19 12h.01M6 12a1 1 0 11-2 0 1 1 0 012 0zm7 0a1 1 0 11-2 0 1 1 0 012 0zm7 0a1 1 0 11-2 0 1 1 0 012 0z"></path>
                    </svg>
                </span>
            </div>
            <div class="is-flex is-flex-col is-justify-center is-items-center">
                <span id="spn_pago" runat="server">
                    <svg class="svg_pago is-w-12 is-h-12" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 10h18M7 15h1m4 0h1m-7 4h12a3 3 0 003-3V8a3 3 0 00-3-3H6a3 3 0 00-3 3v8a3 3 0 003 3z"></path>
                    </svg>
                </span>
                <p class="text-resumen">Completa tu pago</p>
            </div>
            <div>
                <span id="spn_pago_puntos" runat="server">
                    <svg class="svg_pago_puntos is-w-12 is-h-12" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 12h.01M12 12h.01M19 12h.01M6 12a1 1 0 11-2 0 1 1 0 012 0zm7 0a1 1 0 11-2 0 1 1 0 012 0zm7 0a1 1 0 11-2 0 1 1 0 012 0z"></path>
                    </svg>
                </span>
            </div>
            <div class="is-flex is-flex-col is-justify-center is-items-center">
                <span id="spn_finalizar" runat="server" style="color: #71717a">
                    <svg class="svg_finalizar is-w-12 is-h-12" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M16 11V7a4 4 0 00-8 0v4M5 9h14l1 12H4L5 9z"></path>
                    </svg>
                </span>
                <p class="text-resumen">Finaliza tu compra</p>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

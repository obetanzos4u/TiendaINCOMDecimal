<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cotizacion-visualizar.aspx.cs" Async="true" Inherits="usuario_mi_cuenta_cotizacion_visualizar" %>
<%@ Register TagPrefix="uc_1" TagName="operacionMensaje" Src="~/userControls/operaciones/uc_mensajeOperacion.ascx" %>
<%@ Register TagPrefix="uc_1" TagName="operacion" Src="~/userControls/operaciones/uc_cotizacion_visualizar.ascx" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Cotización</title>
    <link type="text/css" rel="stylesheet" href="<%= ResolveUrl("/css/incomDev.css") %>?v=0.1" media="screen,projection" />
    <style>
        .MultiFile-label {
                   float: left;
    padding: 2px 5px;
    background: #0b79d0;
    margin: 4px 5px;
    border-radius: 7px;
    color: white;
    font-size: 12px;
}
            

                .MultiFile-label > a {
                    color: white;
                    font-size: 13px;
                    padding: 1px 6px 4px 6px;
                    background: rgb(15, 81, 142);
                    line-height: 28px;
                    text-decoration: none;
                    border-radius: 5px;
                }

                    .MultiFile-label > a:hover {
                        background: rgb(255, 19, 61);
                    }
      
             /* INICIO - animación de cargando */
        .lds-ellipsis {
            display: inline-block;
            position: relative;
            width: 64px;
            height: 38px;
        }

            .lds-ellipsis div {
                position: absolute;
                top: 27px;
                width: 11px;
                height: 11px;
                border-radius: 50%;
                background: #cef;
                animation-timing-function: cubic-bezier(0, 1, 1, 0);
            }

                .lds-ellipsis div:nth-child(1) {
                    left: 6px;
                    animation: lds-ellipsis1 0.6s infinite;
                }

                .lds-ellipsis div:nth-child(2) {
                    left: 6px;
                    animation: lds-ellipsis2 0.6s infinite;
                }

                .lds-ellipsis div:nth-child(3) {
                    left: 26px;
                    animation: lds-ellipsis2 0.6s infinite;
                }

                .lds-ellipsis div:nth-child(4) {
                    left: 45px;
                    animation: lds-ellipsis3 0.6s infinite;
                }

        @keyframes lds-ellipsis1 {
            0% {
                transform: scale(0);
            }

            100% {
                transform: scale(1);
            }
        }

        @keyframes lds-ellipsis3 {
            0% {
                transform: scale(1);
            }

            100% {
                transform: scale(0);
            }
        }

        @keyframes lds-ellipsis2 {
            0% {
                transform: translate(0, 0);
            }

            100% {
                transform: translate(19px, 0);
            }
        }
        /* FIN - animación de cargando */
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="content_enviarEmail" class="container" style="position: fixed; top: 0px; width: 98%">
            <asp:Label ID="lbl_mensaje" runat="server"></asp:Label>
            <div class="row">
                <label>Email Destinatario(s):</label>
                <asp:TextBox ID="txt_destinatarios" CssClass="txt" placeholder="Multiples destinatarios separados por coma" Style="width: 50%" runat="server"></asp:TextBox>
                  <asp:CheckBox ID="chk_cc_telemarketing" Checked="true" Visible="false" Text="CC Telemarketing" runat="server" />
            </div>
            <div class="row">
              
                <asp:Button CssClass="btn" OnClientClick="btnLoading(this);" ID="btn_enviarEmail" OnClick="btn_enviarEmail_Click" runat="server" Text="Enviar" />
                &nbsp; <a class="btn bg-grey" href="/usuario/mi-cuenta/cotizaciones">Regresar a Cotizaciones</a><br />

            </div>
        <div id="content_adjuntos" style="overflow: scroll;" runat="server" visible="false" class="row">
            <asp:FileUpload ID="fl_adjuntos" CssClass="multi pure-button"
                Style="color: transparent;" AllowMultiple="true" runat="server" />
        </div>
        <uc_1:operacionMensaje ID="operacionMensaje" runat="server" />
        </div>
        <div id="content_operacion">
            <uc_1:operacion ID="operacion" runat="server" />
        </div>

        <script src="<%= ResolveUrl("/js/jquery.MultiFile.js") %> " type="text/javascript"></script>
        <script>

            $(document).ready(function () {
                var alto = $("#content_enviarEmail").height()

                $("#content_operacion").css({ 'margin-top': alto + 'px' });
            });

            // Función que inserta la barra de carga en el mismo nivel del objeto que llama ocultando este
function btnLoading(btn) {
    btn.style.display = "none";
    var padre = btn.parentNode;
    var progress = document.createElement("div");
    progress.classList.add("lds-ellipsis");
    progress.innerHTML = `<div></div><div></div><div></div><div></div>`;
    padre.insertBefore(progress, btn);
}
        </script>

        <div style="position: fixed;
    bottom: 0px; text-align: center !important;width: 100%;background: rgba(0, 0, 0, 0.47843137254901963);display: block;height: 50px;line-height: 44px;">
                        <asp:HyperLink CssClass="btn bg-blue" ID="btn_editarCotizacion" runat="server"> 
                                                     Editar Datos de Contacto, envío y facturación
            </asp:HyperLink> &nbsp;
            <asp:HyperLink CssClass="btn blue" ID="btn_editarProductos" runat="server">
                                                         Editar Productos

            </asp:HyperLink>
        </div>
    </form>
    
    
</body>

</html>

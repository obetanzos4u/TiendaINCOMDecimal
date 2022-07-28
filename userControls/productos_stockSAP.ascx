<%@ Control Language="C#" AutoEventWireup="true" CodeFile="productos_stockSAP.ascx.cs" Inherits="uc_productos_stockSAP" %>




        <asp:Panel ID="content_usuario_logeado" Visible="false" style="display:inline;" runat="server">
            <asp:HiddenField ID="hf_numero_parte" runat="server" />
 

            <a class="btn green" onclick="consultarDisponibilidad(this);" href="#">Ver disponibilidad</a>
            <br /> <br />
            <div id="productoDisponibilidad" ></div>
            *La disponibilidad puede cambiar sin previo aviso.

            <script>

                function consultarDisponibilidad(btn) {
                    var txt_cantidad = document.querySelector(".txt_cantidadCarrito");

                   
                    loadDisponibilidad(btn, txt_cantidad, "<%=hf_numero_parte.Value %>");

                }
                document.addEventListener("DOMContentLoaded", function (event) {
                    //  $(".cargarStock_Click").click();
                });
            </script>
        </asp:Panel>

        <asp:Panel ID="content_usuario_visitante" style="display:inline;" Visible="false" runat="server"><p>
            Para consultar la disponibilidad  
                                                          <a href="#" onclick="LoginAjaxOpenModal();">Iniciar Sesión</a>
            ó 
                            <a title="Crear cuenta" href='<%= ResolveUrl("~/registro-de-usuario.aspx") %>'>Crear cuenta</a>
            </p>
        </asp:Panel>


<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeFile="ubicacion-y-sucursales.aspx.cs" MasterPageFile="~/general.master" Inherits="aviso_de_privacidad" %>

<asp:Content ID="Content2" ContentPlaceHolderID="contenido" runat="Server">
    <div class="is-container">
        <div class="is-flex is-flex-col is-justify-center is-items-center is-w-full">
            <h1 class="title-ubicacion_sucursales is-font-bold is-select-none">Contacto y ubicación</h1>
            <div class="is-px-4 is-w-1_2">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div runat="server" id="gracias" visible="false" class="is-flex is-flex-col is-justify-center is-items-center">
                            <h2>Gracias por escribirnos</h2>
                            <h4>Nos pondremos en contacto contigo a la brevedad.</h4>
                        </div>
                        <div id="formulario" runat="server">
                            <div class="is-py-4">
                                <h2 id="contacto" class="is-font-semibold is-m-0">Contacto</h2>
                                <div class="is-py-4">
                                    <p class="is-m-0">¿Tienes dudas de alguno de nuestros productos o servicios?</p>
                                    <p class="is-m-0">Envíanos tus dudas, comentarios o requirimientos técnicos, con gusto te asesoraremos.</p>
                                </div>
                            </div>
                            <div class="is-px-4">
                                <div>
                                    <label for="<%= txt_nombre.ClientID %>" class="is-text-base">Nombre completo:</label>
                                    <asp:TextBox ID="txt_nombre" TabIndex="1" runat="server" Style="border: 2px solid red" required></asp:TextBox>
                                </div>
                                <div>
                                    <label for="<%= txt_email.ClientID %>" class="is-text-base">Correo:</label>
                                    <asp:TextBox ID="txt_email" TabIndex="2" runat="server" required></asp:TextBox>
                                </div>
                                <div>
                                    <label for="<%= txt_telefono.ClientID %>" class="is-text-base">Teléfono:</label>
                                    <asp:TextBox ID="txt_telefono" TabIndex="3" runat="server"></asp:TextBox>
                                </div>
                                <div>
                                    <label for="<%= txt_mensaje.ClientID  %>" class="is-text-base">Mensaje:</label>
                                    <asp:TextBox ID="txt_mensaje" TextMode="MultiLine" CssClass="materialize-textarea" TabIndex="5" runat="server" required></asp:TextBox>
                                </div>
                            </div>
                            <div class="is-py-4 is-flex is-justify-end is-items-center">
                                <asp:LinkButton ID="btn_enviarEmailContacto" class="is-text-white is-font-semibold is-px-4 is-py-2 is-bg-blue is-rounded" OnClick="btn_enviarEmailContacto_Click" OnClientClick="" runat="server" TabIndex="4">Enviar</asp:LinkButton>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <hr />
                <div class="is-py-4">
                    <h2 id="Ubicaciones" class="is-font-semibold is-m-0">Ubicaciones</h2>
                    <div class="is-flex is-justify-center is-items-center">

                        <div class="is-w-1_2 is-h-full is-p-8">
                            <img class="responsive-img" src="/img/informacion/fachada_plutarco_contacto.jpg" width="575" height="357" alt="Sucursal ciudad de méxico Incom">
                            <div class="is-flex is-justify-around is-items-center is-py-4">
                                <a href="https://g.page/Incom_CDMX?share" class="is-text-white is-font-semibold is-px-4 is-py-2 is-bg-blue is-rounded" target="_blank">Mapa</a>
                                <a href="/documents/pdf/croquis-plutarco.pdf" class="is-text-white is-font-semibold is-px-4 is-py-2 is-bg-blue is-rounded" target="_blank">Croquis</a>
                            </div>
                        </div>
                        <div class="is-w-1_2 is-flex is-flex-col is-justify-start is-items-center is-py-4">
                            <p class="is-font-semibold is-text-lg is-m-0 is-p-4">INCOM Ciudad de México</p>
                            <div class="is-w-full">
                                <p class="is-m-0">Av. Presidente Plutarco Elías Calles 276, Col. Tlazintla, Iztacalco, C.P. 08710, Ciudad de México.</p>
                                <p class="is-font-semibold is-m-0 is-py-2">Atención a clientes: </p>
                                <strong>Teléfonos: </strong>
                                <div>
                                    <p class="is-m-0 is-p-0">CDMX y área metropolitana: <a href="tel:5552436900">(55) 5243-6900</a> y <a href="tel:5552436902">(55) 5243-6902</a></p>
                                    <p class="is-m-0 is-p-0">Del interior sin costo: <a href="tel:8004626600">(800) 46266-00</a></p>
                                </div>
                                <p class="is-font-semibold is-m-0 is-py-2">Atención a proveedores: </p>
                                <strong>Teléfonos: </strong>
                                <div>
                                    <p class="is-m-0 is-p-0"><a href="tel:5552437200">(55) 5243-7200</a> y <a href="tel:5552437201">(55) 5243-7201</a></p>
                                </div>
                                <p class="is-font-semibold is-m-0 is-py-2">Horarios de atención: </p>
                                <p class="is-m-0">Lunes a jueves de 8:30 a 18:30 hrs.</p>
                                <p class="is-m-0">Viernes de 8:30 a 17:30 hrs.</p>
                            </div>
                        </div>
                    </div>
                    <div class="is-flex is-justify-center is-items-start">
                        <div class="is-w-1_2 is-h-full is-p-8">
                            <img class="responsive-img" src="/img/informacion/fachada_planta_contacto.jpg" width="575" height="357" alt="Planta Polymeric (Jilotepec)">
                            <div class="is-flex is-justify-around is-items-center is-py-4">
                                <a href="https://goo.gl/maps/QADzsEBgwwkvYXdQ9" class="is-text-white is-font-semibold is-px-4 is-py-2 is-bg-blue is-rounded" target="_blank">Mapa</a>
                                <a href="/documents/pdf/Plano-para-llegar-a-PLANTA-INCOM.pdf" class="is-text-white is-font-semibold is-px-4 is-py-2 is-bg-blue is-rounded" target="_blank">Croquis</a>
                            </div>
                        </div>
                        <div class="is-w-1_2 is-flex is-flex-col is-justify-center is-items-center is-py-4">
                            <p class="is-font-semibold is-text-lg is-m-0 is-p-4">Planta Polymerico</p>
                            <div class="is-w-full">
                                <p class="is-m-0">KM 99.5 carretera México Querétaro.</p>
                                <p class="is-font-semibold is-m-0">Atención a clientes: </p>
                                <p class="is-m-0 is-p-0">Móvil: <a href="tel:5559534428">(55) 5953-4428</a></p>
                                <%--                                <p class="is-font-semibold is-m-0">Correo: </p>
                                <p class="is-m-0 is-p-0"><a href="mailto:mmartinez@polymeric.mx">mmartinez@polymeric.mx</a></p>--%>
                                <p class="is-font-semibold is-m-0 is-py-2">Horarios de atención: </p>
                                <p class="is-m-0">Lunes a jueves de 7:00 a 17:00 hrs.</p>
                                <p class="is-m-0">Viernes de 7:00 a 15:00 hrs.</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true"   Async="true" CodeFile="ubicacion-y-sucursales.aspx.cs" MasterPageFile="~/general.master" Inherits="aviso_de_privacidad" %>


<asp:Content ID="Content2" ContentPlaceHolderID="contenido" Runat="Server" >
  <div class="container"> <div class="row">
      <h1>Contacto y ubicación.</h1>

      
      <asp:UpdatePanel ID="UpdatePanel1" class="row" runat="server">
          <ContentTemplate>
              <div runat="server" id="gracias" visible="false" class="col s12 m6">
                  <h1>Mensaje enviado con éxito ✓</h1>
                  <h2 style="text-align: center;">
                      Gracias por contactarnos, muy pronto recibirás respuesta a tu solicitud.<br />
                  </h2>
              </div>
              <div id="formulario" runat="server" class="row">
                  <h2 id="contacto">Contacto y/o Soporte Técnico</h2>
                  <p>
                      Contáctanos fácilmente y envíanos tus dudas, comentarios o requerimientos técnicos. <br />Recuerda que el soporte técnico es totalmente gratuito para nuestros clientes.<br>
                      <br /><em>¿Aún no eres cliente y tienes dudas de alguno de nuestros productos o servicios? contáctanos y con gusto responderemos a todas tus dudas.</em>
                  </p>

                  <div class="input-field col s12  ">
                    
     	 <asp:TextBox ID="txt_nombre" required placeholder="Ingresa tu nombre, Ej. Juan Torres" TabIndex="1" runat="server"></asp:TextBox>

                      <label for="<%= txt_nombre.ClientID %>">Nombre</label>

                  </div>
                  <div class="input-field col s12  ">
                      <asp:TextBox ID="txt_email" TabIndex="2" required placeholder="Ingresa tu correo, Ej. minombre@correo.com" runat="server"></asp:TextBox>
                      <label for="<%= txt_email.ClientID %>">Correo</label>

                  </div>
                  <div class="input-field col s12 m12 ">
                      <asp:TextBox ID="txt_telefono" placeholder="Ingresa tu número telefónico" runat="server"></asp:TextBox>
                        <label for="<%= txt_telefono.ClientID %>">Teléfono</label>
                  </div>
                  <div class="input-field  col s12  ">

                      <asp:TextBox ID="txt_mensaje" TextMode="MultiLine" CssClass="materialize-textarea" TabIndex="5"  runat="server" required></asp:TextBox>
                      <label for="<%= txt_mensaje.ClientID  %>">Mensaje: </label>

                  </div>
                  <div class="col s12 m12">
                      <asp:LinkButton ID="btn_enviarEmailContacto" CssClass="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5"
                         OnClick="btn_enviarEmailContacto_Click"  OnClientClick="btnLoading(this);" runat="server" TabIndex="4">Enviar</asp:LinkButton>
                     
                          
                    
                  </div>
              </div>
          </ContentTemplate>
      </asp:UpdatePanel>

      <div class="row">
           <h2>Ubicaciones</h2>
          <div class="col s12 m6">
             <h2>Sucursal Incom Ciudad de México</></h2>
              <p>
                 Av. Presidente Plutarco Elías Calles 276, Col. Tlazintla, Iztacalco, C.P. 08710, Ciudad de México.
                  <br>
                  <br>
                 <strong>Teléfonos</strong><br>
                  <strong>Para la CDMX y área metropolitana: </strong>
                  <br>
                 <strong>Atención a clientes:</strong> (55) 5243-6900 <br />
                  <strong>Administración y Operaciones:</strong> (55) 5243-6902 <br />
                  <strong>Atención a proveedores:</strong> (55) 5243-7200 y  (55) 5243-7201 <br />
                  <br>
                  <strong>Del interior sin costo:</strong><br>
                  800-INCOM(46266)-00    


              </p>
              <p>
                  <a class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5"
                      href="https://www.google.com/maps/place/Incom/@19.397827,-99.111646,16z/data=!4m2!3m1!1s0x0:0xe8681194a59f3b5a?hl=es-ES"
                      target="_blank">Ubicación en Google Maps</a><br>
                  <br />
                  <a class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5"
                      href="/documents/pdf/croquis-plutarco.pdf" target="_blank">Croquis de Oficinas</a>  
                     <br>
                     <br><img class="responsive-img" src="/img/informacion/fachada_plutarco_contacto.jpg" width="575" height="357" alt="Sucursal ciudad de méxico Incom">
                  <br>
              </p>


          </div>
          <div class="col s12 m6"> 
              <h2>Planta Polymeric (Jilotepec)</></h2>
              <p>                Ubicada en KM 99.5 carretera México Querétaro,
                   <br />  <br /> 
                  <a class="waves-effect waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5"
                      href="/documents/pdf/Plano-para-llegar-a-PLANTA-INCOM.pdf" target="_blank">Croquis de la Planta Jilotepec.</a>
                  <br /><br />
                   <img class="responsive-img" src="/img/informacion/fachada_planta_contacto.jpg" width="575" height="357"alt="Planta Polymeric (Jilotepec)">
              </p>
          </div>
      </div>
  </div>
  </div>
</asp:Content>

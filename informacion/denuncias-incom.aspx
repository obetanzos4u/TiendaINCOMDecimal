<%@ Page Language="C#" AutoEventWireup="true" CodeFile="denuncias-incom.aspx.cs" MasterPageFile="~/general.master" Inherits="informacion_denuncias_incom" %>





<asp:Content ID="Content" ContentPlaceHolderID="contenido" runat="Server">


    <div class="container" style="align-content:center">

        <h1><b>Buzón web de quejas, sugerencias, denuncias y felicitaciones.</b></h1>


        <div class="row">
            <div class="col m12 l12 l12 ">
                <h2>Tu opinión es importante para nosotros, agrega un comentario que tengas, en <b>INCOM</b> trabajamos para mejorar constantemente. </h2>

                <h6 id="logueado" style="color:red" runat="server" visible="false"><i>
                    <b>* Tu comentario se guardara con los datos de tu usuario con el que iniciaste sesión, si deseas que tu comentario sea anónimo cierra sesión antes de enviarlo.</b></i></h6>
                <h6 id="anonimo" style="color:red" runat="server" visible="false"><i>
                    <b>* Tu comentario será anónimo , si deseas que se guarden tus datos para poder recibir una respuesta o seguimiento al comentario que mandes inicia sesión antes de agregar el comentario. </b></i></h6>
                <br />
                <h3>¿Qué tipo de comentario deseas enviar?</h3>
                <p></p>
            </div>
        </div>



        <asp:UpdatePanel ID="updp_quejas" class="row" runat="server">
            <ContentTemplate>
                <div>

                    <h6 style="color: silver">Selecciona el tipo de comentario que desea mandar <span style="color: red">*</span> </h6>
                    <asp:DropDownList ID="ddl_opciones" OnSelectedIndexChanged="ddl_opciones_SelectedIndexChanged" AutoPostBack="true" runat="server">
                        <asp:ListItem Value="0" Text="Selecciona una opción: "></asp:ListItem>
                        <asp:ListItem Value="quejas" Text="Quejas"></asp:ListItem>
                        <asp:ListItem Value="sugerencias" Text="Sugerencias"></asp:ListItem>
                        <asp:ListItem Value="denuncias" Text="Denuncias"></asp:ListItem>
                        <asp:ListItem Value="felicitaciones" Text="Felicitaciones"></asp:ListItem>

                    </asp:DropDownList>

                    <asp:Label ID="lb_comentario" runat="server"></asp:Label>


                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

         <div id="enviandoText" style="background: #64b5f6; padding: 11px 10px;border: solid 1px #4dd28e;border-radius: 5px;display: none;">Enviando....</div>
        <br />
        <br />
        <br />





    </div>

    <!--MODAL DE QUEJAS  -->

    <div id="modal_quejas" class="modal fade">


        <div class="modal-content" style=" border: 0px;">
            <h2 class="modal-title"><b>Agrega tu queja</b></h2>

            <br />

            <h4><i>Selecciona una opción de comentario y si deseas detallar más tu <b>QUEJA</b> agrega los detalles en la caja de texto.</i> </h4>
            <br />

            <asp:Label ID="lb_queja" runat="server" Text="Opciones de queja" BorderColor="YellowGreen"></asp:Label>
            <asp:DropDownList ID="ddl_comentario_queja" runat="server">
                <asp:ListItem Value="0" Text="Elige una opción de queja"></asp:ListItem>
                <asp:ListItem Value="Mala atención" Text="Mala atención"></asp:ListItem>
                <asp:ListItem Value="Entrega de producto con retraso" Text="Entrega de producto con retraso"></asp:ListItem>
                <asp:ListItem Value="Entrega de producto en mal estado" Text="Entrega de producto en mal estado"></asp:ListItem>
               
            </asp:DropDownList>

     
         
            <br />


            <h4><i>Agrega más detalles a tu <strong>QUEJA</strong> en el siguiente espacio: </i></h4>


            <asp:TextBox ID="txt_coment" runat="server" Style="border: double" placeholder="Escribe tu comentario.." TextMode="MultiLine" CssClass="materialize-textarea"></asp:TextBox>



            <div class="modal-footer">
                <asp:LinkButton ID="btn_enviar_queja" OnClientClick="btn_click(this)" ClientIDMode="Static"
                    Class="appLoading modal-action modal-close waves-effect waves-green btn blue darken-1"
                    runat="server" OnClick="btn_enviar_queja_Click" Text="Enviar"></asp:LinkButton>

                <a href="#!" class="modal-action modal-close waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5">Cerrar</a>
            </div>


        </div>


    </div>

    <!--MODAL DE SUGERENCIAS  -->


    <div id="modal_sugerencias" class="modal fade">

        <div class="modal-content" style=" border: 0px;">
            <h2 class="modal-title"><b>Agrega tu sugerencia </b></h2>
            <h4><i>Selecciona una opción de comentario y si deseas detallar más tu <b>SUGERENCIA</b> agrega los detalles en la caja de texto.</i> </h4>
            <br />



        
            <asp:Label ID="lbl_sugerencia" runat="server" Text="Opciones de sugerencias" BorderColor="YellowGreen"></asp:Label>
            <asp:DropDownList ID="ddl_comentario_sugerencia" runat="server">
                <asp:ListItem Value="0" Text="Elige una opción de sugerencia"></asp:ListItem>
                <asp:ListItem Value="Mejorar atención a cliente" Text="Mejorar atención a cliente"></asp:ListItem>
                <asp:ListItem Value="Mejorar los procesos en las áreas" Text="Mejorar los procesos en las áreas"></asp:ListItem>
                <asp:ListItem Value="Mejorar promociones" Text="Mejorar promociones"></asp:ListItem>
                <asp:ListItem Value="Mejorar tiempos de entrega" Text="Mejorar tiempos de entrega"></asp:ListItem>
            </asp:DropDownList>
            <br />

            <h4><i>Si deseas agregar detalles a tu <b>SUGERENCIA</b> agrega en el siguiente espacio:</i> </h4>
            <asp:TextBox ID="txt_sugerencias" runat="server" Style="border: double" placeholder="Escribe tu comentario.." TextMode="MultiLine" CssClass="materialize-textarea"> </asp:TextBox>

            <div class="modal-footer">
                <asp:LinkButton ID="btn_enviar_sugerencia" ClientIDMode="Static" OnClientClick="btn_click(this)"
                    Class="appLoading modal-action modal-close waves-effect waves-green btn blue darken-1"
                    runat="server" OnClick="btn_enviar_sugerencia_Click" Text="Enviar"></asp:LinkButton>

                <a href="#!" class="modal-action modal-close waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5">Cerrar</a>
            </div>

        </div>





    </div>

    <!--MODAL DENUNCIAS -->
    <div id="modal_denuncias" class="modal fade">
        <div class="container">

            <div class="modal-content" style="border: 0px;"></div>
            <h2 class="modal-title"><b>Agrega tu denuncia</b></h2>
            <h4><i>Selecciona una opción de comentario y si deseas detallar más tu <b>DENUNCIA</b> agrega los detalles en la caja de texto.</i></h4>
            <br />
           
             <asp:Label ID="lbl_denuncia" runat="server" Text="Opciones de denuncia" BorderColor="YellowGreen"></asp:Label>
            <asp:DropDownList ID="ddl_comentario_denuncia" runat="server">
                <asp:ListItem Value="0" Text="Elige una opción de denuncia"></asp:ListItem>
                <asp:ListItem Value="Acto de corrupción" Text="Acto de corrupción"></asp:ListItem>
                <asp:ListItem Value="Mal trato al cliente" Text="Mal trato al cliente"></asp:ListItem>
                <asp:ListItem Value="Robo" Text="Robo"></asp:ListItem>
                <asp:ListItem Value="Discriminación" Text="Discriminación"></asp:ListItem>
                <asp:ListItem Value="Caso de negligencia" Text="Caso de negligencia"></asp:ListItem>
            </asp:DropDownList>
            <br />
        

            <h4><i>Si  deseas agregar detalles a tu <b>DENUNCIA</b> agregalo en el siguiente espacio:</i> </h4>
            <asp:TextBox ID="txtdenuncias" runat="server" Style="border: double" placeholder="Escribe tu comentario.." TextMode="MultiLine" CssClass="materialize-textarea"> </asp:TextBox>

           
            <div class="modal-footer">
                <asp:LinkButton ID="btn_enviar_denuncia" ClientIDMode="Static"
                    class="mouse-cursor-gradient-tracking waves-effect waves-green btn blue darken-1"
                    runat="server" OnClick="btn_enviar_denuncia_Click" OnClientClick="btn_click(this)" Text="Enviar"></asp:LinkButton>

                <a href="#!" class="modal-action modal-close waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5">Cerrar</a>
            </div>
        </div>
    </div>



    <!--MODAL FELICITACIONES-->

    <div id="modal_felicitaciones" class="modal fade">
        <div class="container">

            <div class="modal-content" style="border: 0px;">
                <h2 class="modal-title"><b>Agrega tu felicitación</b></h2>
                <h4><i>Selecciona una opción de comentario y si deseas detallar más tu <b>FELICITACIÓN</b> agrega los detalles en la caja de texto.</i></h4>
                <br />
               
                 <asp:Label ID="lbl_felicitacion" runat="server" Text="Opciones de felicitación" BorderColor="YellowGreen"></asp:Label>
            <asp:DropDownList ID="ddl_comentario_felicitacion" runat="server">
                <asp:ListItem Value="0" Text="Elige una opción de felicitación"></asp:ListItem>
                <asp:ListItem Value="Excelente asesoría de venta" Text="Excelente asesoría de venta"></asp:ListItem>
                <asp:ListItem Value="Calidad de producto excelente" Text="Calidad de producto excelente"></asp:ListItem>
                <asp:ListItem Value="Instalaciones confortables" Text="Instalaciones confortables"></asp:ListItem>
                <asp:ListItem Value="Amabilidad y buen trato" Text="Amabilidad y buen trato"></asp:ListItem>
               
            </asp:DropDownList>
                <br />
                <h4><i>Agrega más detalles a tu <strong>FELICITACIÓN</strong> en el siguiente espacio: </i></h4>
                <asp:TextBox ID="txt_felicitaciones" runat="server" Style="border: double" placeholder="Escribe tu comentario.." TextMode="MultiLine" CssClass="materialize-textarea"> </asp:TextBox>


                <div class="modal-footer">
                    <asp:LinkButton ID="btn_enviar_felicitacion" ClientIDMode="Static" OnClientClick="btn_click(this)"
                        class="mouse-cursor-gradient-tracking waves-effect waves-green btn blue darken-1"
                        runat="server" OnClick="btn_enviar_felicitacion_Click" Text="Enviar"></asp:LinkButton>

                    <a href="#!" class="modal-action modal-close waves-light btn blue-grey-text text-darken-2 blue-grey lighten-5">Cerrar</a>
                </div>

            </div>


        </div>

    </div>


    





<script>
       function btn_click(t) {
           t.style.display = 'none';

           document.querySelector("#enviandoText").style.display ='block';
       }
   </script>



</asp:Content>






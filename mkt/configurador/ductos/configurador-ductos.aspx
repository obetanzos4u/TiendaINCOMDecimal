<%@ Page Language="C#" AutoEventWireup="true" CodeFile="configurador-ductos.aspx.cs" Inherits="configurador_ductos" %>

<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Configurador de ductos</title>
    <link href="style.css" rel="stylesheet" type="text/css">
</head>
<body>
    
    <div class="header"><img src="img/logo_incom.png" style="height: 60px;"/> </div>
    <div class="contenido">
        <h1>Configurador de ductos</h1>
        <p>Selecciona la configuración requerida para tu ducto y automaticamente se generará un número de parte y una vista previa para que puedas cotizarlo.</p>

        <div class="columna1">
            <div class="uno">
                <label for="modelo">Pared:</label>
                <select required style="margin: 15px 20px 0px 92px;" onchange="modelo_to_diametro(); generador_no_parte(); " name="select" id="modelo" title="Seleccione un modelo">
                    <option id="SDR_11" value="SDR11">SDR 11</option>
                    <option id="SDR_13" value="SDR13">SDR 13.5</option>
                    <option id="SDR_17" value="SDR17">SDR 17</option>
                </select>
            </div>

            <div class="dos">
                <label for="diametro">Diametro:</label>
                <select style="margin: 15px 20px 0px 62px;" name="select" id="diametro" title="Seleccione un diametro" onchange="modelo_to_diametro(); generador_no_parte(); otro_diametro(); " >
                    <option id="SeDiametro" value="">Seleccione un diámetro</option>
                    <option id="diametro_1" value="10">1"</option>
                    <option id="diametro_1_1/4" value="12">1 1/4"</option>
                    <option id="diametro_1_1/2" value="15">1 1/2"</option>
                    <option id="diametro_2" value="20">2"</option>
                    <option id="otro_diametro" value="*">Otro diametro</option>
                </select>
            </div>

            <div id="diametro_diferente" onkeyup="generador_no_parte(); otro_diametro(); ">

                Otro Diametro: <input id="selec_diametro" type="text" onkeyup="generador_no_parte();" />


            </div>


 

            <div class="tres">
                <label for="select">Lubricado:</label>
                <select style="margin: 15px 20px 0px 54px;" name="select" id="lubricado" title="Seleccione un lubricado" onchange="generador_no_parte(); " >
                    <option id="SeLubricado" value="">Seleccione un lubricado</option>
                    <option id="con_lubricado" value="P">Pre lubricado</option>
                    <option id="sin_lubricado" value="S">Sin lubriado</option>
                </select>
            </div>

            <div class="cuatro">
                <label for="cinta_de_jalado">Cinta de jalado:</label>
                <select name="select" id="cinta_de_jalado" title="Seleccione tipo de cinta" onchange="generador_no_parte(); ">
                    <option id="SeCinta" value="">Seleccione cinta</option>
                    <option id="con_cinta" value="C">Con Cinta</option>
                    <option id="sin cinta" value="S">Sin Cinta</option>
                </select>
            </div>

            <div class="cinco">
                <label for="presentacion">Presentación:</label>
                <select style="margin: 15px 0px 0px 28px;" name="select" id="presentacion" title="Seleccione una presentacion" onchange="presentacion_to_empaque(); generador_no_parte(); otra_presentacion(); color2();">
                    <option id="SePresentacion" value="" >Seleccione una presentación</option>
                    <option id="unitubo" value="1">Unitubo</option>
                    <option id="bifurcado" value="2">Bifurcado</option>
                    <option id="trifurcado" value="3">Trifurcado</option>
                    <option id="otra_presentacion" value="*">Otra presentacion</option>
                </select>
            </div>

            <div id="presentacion_diferente" onkeyup="generador_no_parte();">

                Otra Presentacion: <input id="selec_presentacion" type="text" onkeyup="generador_no_parte();" />

            </div>


            <div class="seis">
                <label for="color">Color:</label>
                <select style="margin: 15px 20px 0px 94px;" name="select" id="color" title="Seleccione un color" onchange="generador_no_parte(); otro_color(); colores();">
                    <option id="SeColor" value="">Seleccione un color</option>
                    <option id="amarillo" value="A">Amarillo</option>
                    <option id="blanco" value="W">Blanco</option>
                    <option id="naranja" value="N">Naranja</option>
                    <option id="verde" value="V">Verde</option>
                    <option id="azul_marino" value="Z">Azul Marino</option>
                    <option id="otro_color" value="*">Otro color</option>
                </select>
            </div>



            <div id="color_diferente" onkeyup="generador_no_parte(); otro_color();">

                Otro Color: <input id="selec_color" type="text" onkeyup="generador_no_parte();" />

            </div>

            <div id="contenedor_color2" style="margin:0px 0px 0px 30px;">
                <label for="color">Color 2:</label>
                <select name="select" style="margin:15px 20px 0px 80px;" id="color2" title="Seleccione un color" onchange="generador_no_parte(); otro_color2(); color2(); colores();">
                    <option id="SeColor2" value="">Seleccione un color</option>
                    <option id="amarillo2" value="A">Amarillo</option>
                    <option id="blanco2" value="W">Blanco</option>
                    <option id="naranja2" value="N">Naranja</option>
                    <option id="verde2" value="V">Verde</option>
                    <option id="azul_marino2" value="Z">Azul Marino</option>
                    <option id="otro_color2" value="*">Otro color</option>
                </select>
            </div>



            <div id="color_diferente2" onkeyup="generador_no_parte(); otro_color2(); color2();">

                Otro Color: <input id="selec_color2" type="text" onkeyup="generador_no_parte();" />

            </div>

            <div id="contenedor_color3" style="margin:0px 0px 0px 30px;">
                <label for="color">Color 3:</label>
                <select style="margin:15px 20px 0px 80px;" name="select" id="color3" title="Seleccione un color" onchange="generador_no_parte(); otro_color3(); color2(); colores();">
                    <option id="SeColor3" value="">Seleccione un color</option>
                    <option id="amarillo3" value="A">Amarillo</option>
                    <option id="blanco3" value="W">Blanco</option>
                    <option id="naranja3" value="N">Naranja</option>
                    <option id="verde3" value="V">Verde</option>
                    <option id="azul_marino3" value="Z">Azul Marino</option>
                    <option id="otro_color3" value="*">Otro color</option>
                </select>
            </div>



            <div id="color_diferente3" onkeyup="generador_no_parte(); otro_color3(); color2();">

                Otro Color: <input id="selec_color3" type="text" onkeyup="generador_no_parte();" />

            </div>


            <div class="siete">
                <label for="empaque">Empaque:</label>
                <select style="margin: 15px 20px 0px 55px;" name="select" id="empaque" title="Seleccione un empaque" onchange="presentacion_to_empaque(); generador_no_parte(); ">
                    <option id="SeEmpaque" value="">Seleccione un empaque</option>
                    <option id="carrete_metalico" value="M">Carrete metálico</option>
                    <option id="rollo_flejado" value="R">Rollo flejado</option>
                </select>
            </div>

            <div class="ocho">
                <label for="metros">Cantidad:</label>
                <select style="margin: 15px 20px 0px 56px;"  name="select" id="metros" title="Seleccione los metros que requiera" onchange="generador_no_parte(); metros_quintado(); otros_metros(); quintado();">
                    <option id="SeMetros" value="">Seleccione los metros</option>
                    <option id="250mts" value="250">250 mts</option>
                    <option id="500mts" value="500">500 mts</option>
                    <option id="5000mts" value="5000">5000 mts</option>
                    <option onchange="quintado();" id="otros_metros" value="*">Otra Cantidad</option>
                </select>
            </div>

            <div id="metros_diferente" onkeyup="generador_no_parte();" onchange="quintado();" title="Multiplos de 250" >
                
                Otra Cantidad: <input id="selec_metros" type="number"  onkeyup="generador_no_parte();" onchange="quintado();" />

            </div>


            <div id="bono_div">
                <label id="bono1" for="metros">Opcion:</label>
                <select name="select" id="bono" onchange="metros_quintado(); quintado(); generador_no_parte();">
                    <option id="Opcion" value="">Seleccione Quintado o Personalizado</option>
                    <option id="quintado" value="Q">Quintado</option>
                    <option id="personalizado" value="P">Personalizado</option>
                    <option id="quintado_personalizado" value="QP">Quintado & Personalizado</option>
                </select>
            </div>

            <div style="display:none;" id="txt_personalizado" onkeyup="generador_no_parte();">
               
               <div style="font-size:14px">Leyenda de personalizado</div>  <textarea id="leyenda"  onkeyup="generador_no_parte();" rows="4" cols="45"></textarea>

            </div>


            <br/>
            <br/>

<form runat="server">
            <div id="envio">

                <p style="color:#1054A4;">Lugar de Entrega</p>
                <asp:TextBox ID="lugar_envio" TextMode="MultiLine" rows="5"   runat="server" Width="435px"></asp:TextBox>


            </div>


            <div id="comentarios">

                <p style="color:#1054A4;">Comentarios adicionales</p>


                 <asp:TextBox ID="caja_comentarios" TextMode="MultiLine" rows="8"   Width="435px" runat="server"></asp:TextBox>




            </div>


            </div>

        <div class="columna2">
            <p style="font-size:24px; color: #1054A4; font-weight:bold; "> Numero de parte: </p>  <div id="no_parte"></div>


            <div id="img_parte" style="text-align: right;">
                <img src="img/tuliko.png" style="height: 25px; padding: 0px 90px 50px 0px;" />

                <img style="width:300px" id="img_ducto" src="img/ducto.png" />
            </div>

            <p style="font-size:24px; color: #1054A4; font-weight:bold; "> Descripcion de producto: </p>

            <div id="descripcion_parte"></div>  
            <asp:TextBox ID="_descripcion_parte" TextMode="MultiLine" style="display:none;" runat="server"></asp:TextBox>

            <div id="final" style="margin:40px 0px 0px 70px">
                <a style="margin:0px 0px 0px 28px;">Ingresa tu correo electrónico para cotizar</a>
              
               <asp:TextBox runat="server" name="correo" type="text" id="correo" size="45" title="Ingresa un formato de correo válido" placeholder="Ingresa tu email"
                    style="text-align: center; border-radius: 64px 64px 64px 64px; -moz-border-radius: 64px 64px 64px 64px;
                -webkit-border-radius: 64px 64px 64px 64px;
                border: 1px solid #1054A4;
                font-size:16px;
                color: #1054A4;
                padding: 5px 15px;
                background: white; margin: 15px 20px 0px 5px;"></asp:TextBox>
            </div>
            <asp:Button ID="cotizar" OnClick="cotizar_Click" 
              OnClientClick="document.querySelector('#_descripcion_parte').value = document.querySelector('#descripcion_parte').innerText;" 
                runat="server" Text="Cotizar" />

        </div>

 

    </div>



    <div id="tablas" class="tabla">


    </div>
</form>
    <script>

        generador_no_parte();    
        metros_quintado();
        otro_color();
        otro_diametro();
        otra_presentacion();
        otros_metros();
        quintado();
        color2();
        otro_color2();
        otro_color3();
        presentacion_to_empaque();
        colores();

        function modelo_to_diametro() {

            var modelo = document.getElementById("modelo").value;
            var diametro_2 = document.getElementById("diametro_2");
           

            if (modelo == "SDR17") diametro_2.style.display = "none"; else diametro_2.style.display = "block";
            
          
        }

        function presentacion_to_empaque() {

            var presentacion = document.getElementById("presentacion").value;
            var rollo_flejado = document.getElementById("rollo_flejado");
            
            if (presentacion === "3") rollo_flejado.style.display = "none";
            if (presentacion === "*") rollo_flejado.style.display = "none";
            if (presentacion === "1") rollo_flejado.style.display = "block"; 
            if (presentacion === "2") rollo_flejado.style.display = "block";

            if (presentacion === "3") document.getElementById('empaque').value = 'M';
        
        }

        function metros_quintado() {

            var metros = document.getElementById("metros").value;
            var quintado = document.getElementById("bono_div");


            if (metros == "250" || "500" || "otros_metros") quintado.style.display = "none";
            if (metros === "5000") quintado.style.display = "block";



        }


        function otro_color() {

            var color = document.getElementById("color").value;
            var otro_color = document.getElementById("color_diferente");

            if (color == "A" || "B" || "N" || "V" || "Z") otro_color.style.display = "none";
            if (color == "*") otro_color.style.display = "block";



        }

        function otro_color2() {

            var color = document.getElementById("color2").value;
            var otro_color = document.getElementById("color_diferente2");

            if (color == "SeColor2" || "A" || "B" || "N" || "V" || "Z") otro_color.style.display = "none";
            if (color == "*") otro_color.style.display = "block";



        }

        function otro_color3() {

            var color = document.getElementById("color3").value;
            var otro_color = document.getElementById("color_diferente3");

            if (color == "A" || "B" || "N" || "V" || "Z") otro_color.style.display = "none";
            if (color == "*") otro_color.style.display = "block";



        }


        function otro_diametro() {

            var diametro = document.getElementById("diametro").value;
            var otro_diametro = document.getElementById("diametro_diferente");

            if (diametro == "10" || "12" || "15" || "20") otro_diametro.style.display = "none";
            if (diametro == "*") otro_diametro.style.display = "block";



        }

        function otra_presentacion() {

            var presentacion = document.getElementById("presentacion").value;
            var otra_presentacion = document.getElementById("presentacion_diferente");

            if (presentacion == "1" || "2" || "3") otra_presentacion.style.display = "none";
            if (presentacion == "*") otra_presentacion.style.display = "block";

        }

        function otros_metros() {

            var metros = document.getElementById("metros").value;
            var otros_metros = document.getElementById("metros_diferente");

            if (metros == "250" || "500" || "5000") otros_metros.style.display = "none";
            if (metros == "*") otros_metros.style.display = "block";



        }


        function color2() {

            var color2 = document.getElementById("contenedor_color2");
            var color3 = document.getElementById("contenedor_color3");
            var presentacion = document.getElementById("presentacion").value;
 
            if (presentacion === "" || "1") {color2.style.display = "none"; color3.style.display = "none";}
            if (presentacion === "2") { color2.style.display = "block"; color3.style.display = "none"; }
            if (presentacion === "3") { color2.style.display = "block"; color3.style.display = "block"; }


      

        }


        function colores() {

            var color = document.getElementById("color").value;
            var color2 = document.getElementById("color2").value;
            var color3 = document.getElementById("color3").value;

            var amarillo = document.getElementById("amarillo");
            var blanco = document.getElementById("blanco");
            var naranja = document.getElementById("naranja");
            var verde = document.getElementById("verde");
            var azul_marino = document.getElementById("azul_marino");
            var amarillo_2 = document.getElementById("amarillo2");
            var blanco_2 = document.getElementById("blanco2");
            var naranja_2 = document.getElementById("naranja2");
            var verde_2 = document.getElementById("verde2");
            var azul_marino_2 = document.getElementById("azul_marino2");
            var amarillo_3 = document.getElementById("amarillo3");
            var blanco_3 = document.getElementById("blanco3");
            var naranja_3 = document.getElementById("naranja3");
            var verde_3 = document.getElementById("verde3");
            var azul_marino_3 = document.getElementById("azul_marino3");
         

            if (color === "A") { amarillo_2.style.display = "none"; amarillo_3.style.display = "none"; } else { amarillo_2.style.display = "block"; amarillo_3.style.display = "block";}
            if (color === "W") { blanco_2.style.display = "none"; blanco_3.style.display = "none"; } else { blanco_2.style.display = "block"; blanco_3.style.display = "block"; }
            if (color === "N") { naranja_2.style.display = "none"; naranja_3.style.display = "none"; } else { naranja_2.style.display = "block"; naranja_3.style.display = "block"; }
            if (color === "V") { verde_2.style.display = "none"; verde_3.style.display = "none"; } else { verde_2.style.display = "block"; verde_3.style.display = "block"; }
            if (color === "Z") { azul_marino_2.style.display = "none"; azul_marino_3.style.display = "none"; } else { azul_marino_2.style.display = "block"; azul_marino_3.style.display = "block"; }
            if (color2 === "A") { amarillo.style.display = "none"; amarillo_3.style.display = "none"; } else { amarillo.style.display = "block"; }
            if (color2 === "W") { blanco.style.display = "none"; blanco_3.style.display = "none"; } else { blanco.style.display = "block"; }
            if (color2 === "N") { naranja.style.display = "none"; naranja_3.style.display = "none"; } else { naranja.style.display = "block"; }
            if (color2 === "V") { verde.style.display = "none"; verde_3.style.display = "none"; } else { verde.style.display = "block"; }
            if (color2 === "Z") { azul_marino.style.display = "none"; azul_marino_3.style.display = "none"; } else { azul_marino.style.display = "block"; }
            if (color3 === "A") { amarillo_2.style.display = "none"; amarillo.style.display = "none"; } 
            if (color3 === "W") { blanco_2.style.display = "none"; blanco.style.display = "none"; } 
            if (color3 === "N") { naranja_2.style.display = "none"; naranja.style.display = "none"; } 
            if (color3 === "V") { verde_2.style.display = "none"; verde.style.display = "none"; } 
            if (color3 === "Z") { azul_marino_2.style.display = "none"; azul_marino.style.display = "none"; } 


        }


        function quintado() {

            var metros = document.getElementById("metros").value;
            var bono = document.getElementById("bono").value;
            var texto = document.getElementById("txt_personalizado");
            var quintado = document.getElementById("bono_div");
            var mas_5000 = parseInt(document.getElementById("otros_metros").value);


            if (bono == "P" || bono == "QP") texto.style.display = "block"; else texto.style.display = "none";
            if (bono == "Q") texto.style.display = "none"; 
            if (metros == "250") {texto.style.display = "none"; quintado.style.display = "none";}
            if (metros == "SeMetros") {texto.style.display = "none"; quintado.style.display = "none";}
            if (metros == "500") {texto.style.display = "none"; quintado.style.display = "none";}
            if (metros == "*") {texto.style.display = "none"; quintado.style.display = "none";}

            if (mas_5000 >= 5000) quintado.style.display = "block"; 
        }
      


        function generador_no_parte() {

            var modelo = document.getElementById("modelo").value;
            var diametro = document.getElementById("diametro").value;
            var color = document.getElementById("color").value;
            var color2 = document.getElementById("color2").value;
            var color3 = document.getElementById("color3").value;

            var all_colors = [color, color2, color3];

            all_colors = all_colors.sort(function (a, b) {
                return a.localeCompare(b);
            });

            var orden_alfabetico = ""

            for (i = 0; i < all_colors.length; i++) {

                orden_alfabetico = orden_alfabetico + all_colors[i];


                var lubricado = document.getElementById("lubricado").value;
                var cinta_de_jalado = document.getElementById("cinta_de_jalado").value;
                var presentacion = document.getElementById("presentacion").value;
                var empaque = document.getElementById("empaque").value;
                var metros = document.getElementById("metros").value;
                var bono = document.getElementById("bono").value;

                document.getElementById("no_parte").innerHTML = diametro + modelo + orden_alfabetico + lubricado + cinta_de_jalado + presentacion + empaque;

                // INICIO de llamado a otras funciones
                cambiar_color(color);
                descripcion_parte(modelo, diametro, color, color2, color3, lubricado, cinta_de_jalado, presentacion, empaque, metros, bono);

                // FIN de llamado a otras funciones



            }

        }

      /*  function descripcion_parte() {

            var opciones = ["modelo" , "diametro" , "color" , "lubricado" , "cinta_de_jalado" , "presentacion" , "empaque"]
            
            var modelo = document.getElementById("modelo").innerHTML;
            var diametro = document.getElementById("diametro").innerHTML;
            var color = document.getElementById("color").innerHTML;
            var lubricado = document.getElementById("lubricado").innerHTML;
            var cinta_de_jalado = document.getElementById("cinta_de_jalado").innerHTML;
            var presentacion = document.getElementById("presentacion").innerHTML;
            var empaque = document.getElementById("empaque").innerHTML;


            document.getElementById("des_parte").innerHTML = diametro + modelo;



        } */




        function cambiar_color(color) {
            var ducto = document.getElementById("img_ducto");

            switch (color) {
                case "A": ducto.style.filter = "hue-rotate(38deg) brightness(153%)"; break;
                case "W": ducto.style.filter = "grayscale(100%) contrast(50%) brightness(178%)"; break;
                case "N": ducto.style.filter = "hue-rotate(0deg) brightness(100%)"; break;
                case "V": ducto.style.filter = "hue-rotate(158deg) brightness(50%)"; break;
                case "Z": ducto.style.filter = "hue-rotate(224deg) brightness(41%)"; break;
                case "A": ducto.style.filter = "hue-rotate(38deg) brightness(153%)"; break;

            }

        }
        function descripcion_parte(modelo, diametro, color, color2, color3, lubricado, cinta_de_jalado, presentacion, empaque, metros, bono) {
         
            var otro_diametro = document.getElementById("selec_diametro").value;
            var otro_color = document.getElementById("selec_color").value;
            var otro_color2 = document.getElementById("selec_color2").value;
            var otro_color3 = document.getElementById("selec_color3").value;
            var otra_presentacion = document.getElementById("selec_presentacion").value;
            var otros_metros = document.getElementById("selec_metros").value;
            var leyenda = document.getElementById("leyenda").value;

            var diametro_originales = ['1"', '1 1/4"', '1 1/2"', '2"', otro_diametro, ""];

            var posicion;
            var diametro_exterior = ['1.315" (33.40mm)', '1.660" (42.16mm)', '1.900" (48.26mm)', '2.375" (60.33mm)', "", ""];
            var diametro_interior;
            var espesor;
          

            switch (diametro) {
                case "10": posicion = 0; break;
                case "12": posicion = 1; break;
                case "15": posicion = 2; break;
                case "20": posicion = 3; break;
                case "*": posicion = 4; break;
                case "": posicion = 5; break;
            }

            diametro = diametro_originales[posicion];

            switch (modelo) {
                case "SDR11": diametro_interior = ['1.057" (26.85mm)', '1.338" (33.99mm)', '1.534" (38.96mm)', '1.922" (48.62mm)', "", ""]; break;
                case "SDR13": diametro_interior = ['1.101"(27.97mm)', '1.394" (35.41mm)', '1.598" (40.59)', '2.002" (50.85mm)', "", ""]; break;
                case "SDR17": diametro_interior = ['1.14" (28.96mm)', '1.445" (36.70mm)', '1.656" (42.062mm)', "", ""]; break;
            }

            switch (modelo) {
                case "SDR11": espesor = ['0.119" (3.02mm)', '0.151" (3.84mm)', '0.173" (4.39mm)', '0.216" (5.49mm)', "", ""]; break;
                case "SDR13": espesor = ['0.097" (2.46mm)', '0.123" (3.12mm)', '0.141" (3.58mm)', '0.176" (4.47mm)', "", ""]; break;
                case "SDR17": espesor = ['0.077" (1.96mm)', '0.098" (2.49mm)', '0.112" (2.84mm)', "", ""]; break;
            }

            var c1 = "<strong>Color - </strong>"
            var c2 = "<strong>Color 2 -  </strong>"
            var c3 = "<strong>Color 3 - </strong>"
            var s = "<br>";

            switch (color) {

                case "A": color = c1 + "Amarillo" + s; break;
                case "W": color = c1 + "Blanco" + s; break;
                case "N": color = c1 + "Naranja" + s; break;
                case "V": color = c1 + "Verde" + s; break;
                case "Z": color = c1 + "Azul Marino" + s; break;
                case "*": color = c1 + otro_color + s; break;

            }

          

            switch (color2) {

                case "A": color2 = c2 + "Amarillo" + s; break;
                case "W": color2 = c2 + "Blanco" + s; break;
                case "N": color2 = c2 + "Naranja" + s; break;
                case "V": color2 = c2 + "Verde" + s; break;
                case "Z": color2 = c2 + "Azul Marino" + s; break;
                case "*": color2 = c2 + otro_color2 + s; break;

            }

            switch (color3) {

                case "A": color3 = c3 + "Amarillo" + s; break;
                case "W": color3 = c3 + "Blanco" + s; break;
                case "N": color3 = c3 + "Naranja" + s; break;
                case "V": color3 = c3 + "Verde" + s; break;
                case "Z": color3 = c3 + "Azul Marino" + s; break;
                case "*": color3 = c3 + otro_color3 + s; break;

            }

            switch (lubricado) {

                case "P": lubricado = "Pre lubricado"; break;
                case "S": lubricado = "Sin lubricado"; break;

            }
           
            switch (cinta_de_jalado) {

                case "C": cinta_de_jalado = "Con cinta"; break;
                case "S": cinta_de_jalado = "Sin cinta"; break;

            }

            switch (presentacion) {

                case "1": presentacion = "Unitubo"; break;
                case "2": presentacion = "Bifurcado"; break;
                case "3": presentacion = "Trifurcado"; break;
                case "*": presentacion = otra_presentacion; break;
            }

            switch (empaque) {

                case "M": empaque = "Carrete metalico"; break;
                case "R": empaque = "Rollo flejado"; break;
            }

            switch (metros) {

                case "250": metros = 250 + "mts"; bono = ""; break;
                case "500": metros = 500 + "mts"; bono = ""; break;
                case "5000": metros = 5000 + "mts"; break;
                case "*": metros = otros_metros + "mts"; bono = ""; break;


            }


            switch (bono) {

                case "Q": bono = "<strong>Quintado</strong>"; break;
                case "P": bono = "<strong>Personalizado</strong>"; break;
                case "QP": bono = "<strong>Quintado & Personalizado</strong>"; break;


            }

            switch (presentacion) {

                case "Unitubo": color2 = ""; color3 = ""; break;
                case "Bifurcado": color3 = ""; break;
                case "Trifurcado": break;            
            }

            


               
            
            var descripcion = `<strong> Modelo </strong> - ${modelo}  <br/> <strong> Diámetro </strong> - 
                 ${diametro_originales[posicion]}  <br/> <strong> Diámetro Exterior - </strong> ${diametro_exterior[posicion]} <br/> <strong> Diámetro interior - </strong>
                 ${diametro_interior[posicion]} <br/> <strong> Espesor - </strong>  ${espesor[posicion]} <br/> ${color} ${color2} ${color3}
                  <strong> Lubricado - </strong>  ${lubricado} <br/> <strong> Cinta de Jalado - </strong> ${cinta_de_jalado}
                  <br/> <strong> Presentacion - </strong>  ${presentacion} <br/> <strong> Empaque - </strong>  ${empaque} <br/> <strong> Metros - </strong>  ${metros} <br/> ${bono}  ${leyenda}
               `;

            document.getElementById("descripcion_parte").innerHTML = descripcion;
}
       

     
  

    </script>

    
</body>
</html>
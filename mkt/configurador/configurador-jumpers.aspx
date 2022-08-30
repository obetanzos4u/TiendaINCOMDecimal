<%@ Page Title="Configurador de jumpers" Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" ValidateRequest="false" %>

<!doctype html>
<html>
<head>
    <meta charset="utf-8">
    <title>Configurador de Jumpers de Fibra Óptica</title>
    <link href="style.css" rel="stylesheet" type="text/css">
</head>

<body onload="generar();explorer()">
    <div class="header"></div>

    <div class="contenido">
        <h1>Configurador de Jumpers Fibra Óptica</h1>
        <p style="text-align: center;">Selecciona la configuración requerida y automaticamente se generará un número de parte y una vista previa.</p>
        <br>
        <br>

        <div class="columna1">

            <div class=" uno">
                <label for="select">Presentación:</label>
                <select name="select" id="presentacion">
                    <option value="1">Jumper</option>
                    <option value="2">Pigtail</option>
                </select>
            </div>
            <br>
            <br>
            <div class="dos">
                <label for="select">Tipo de Cable:	</label>
                <select name="select" id="opTipoCab">
                    <option class="op_S" value="simplex">Simplex</option>
                    <option class="op_D" value="duplex">Duplex</option>
                    <option class="op_Uni6" value="unitubo 6 fibras">Unitubo de 6 fibras</option>
                    <option class="op_Uni12" value="unitubo 12 fibras">Unitubo de 12 fibras</option>
                </select>
            </div>

            <br>
            <br>
            <div class="tres">
                <label for="select">Tipo de Fibra:</label>
                <select name="select" id="opTipoFib">
                    <option value="modomodoG652">Monomodo G.652 D (9/125um)</option>

                    <option value="modomodoG657">Monomodo G.657 A2 (9/125um)</option>
                    <option value="multimodoOM1">Multimodo OM1 (62.5/125 um)</option>
                    <option value="multimodoOM2">Multimodo OM2 (50/125 um)</option>
                    <option value="multimodoOM3">Multimodo OM3 (50/125 um)</option>
                    <option value="multimodoOM4">Multimodo OM4(50/125um)</option>
                </select>
            </div>
            <br>
            <br>
            <div class="cuatro">
                <label for="select">Diametro de Cable</label>
                <select name="select" id="opDiamCab">
                    <option class="OP_Para_SD" value="1.6 mm">1.6 mm</option>
                    <option class="OP_Para_SD" value="2 mm">2 mm</option>

                    <option class="op_6mm" value="5.2 mm">Unitubo 5mm (6 hilos)</option>
                    <option class="op_7mm" value="7 mm">Unitubo 7 mm (12 hilos)</option>
                </select>
            </div>
            <br>
            <br>
            <div class="cinco">
                Conector Izquierdo
  <select name="select" id="opConectIzq">

      <option value="1">FC/UPC</option>
      <option class="opConect_APC" value="2">FC/APC</option>
      <option value="3">LC/UPC</option>

      <option value="4">SC/UPC</option>
      <option class="opConect_APC" value="5">SC/APC</option>
      <option value="6">ST/UPC</option>
      <%--     <option class="opConect_E2000" value="8">E20/APC(E2000 con licencia)</option>
      <option value="9">E20/UPC(E2000 con licencia)</option>
      <option class="opConect_E2000" value="10">LSH/APC(E2000 genérico)</option>
      <option value="11">LSH/UPC(E2000 genérico)</option>--%>
  </select>
            </div>
            <br>
            <br>
            <div class="seis">
                Conector derecho
  <select name="select" id="opConectDer">

      <option value="1">FC/UPC</option>
      <option class="opConect_APC" value="2">FC/APC</option>
      <option value="3">LC/UPC</option>
      <option value="4">SC/UPC</option>
      <option class="opConect_APC" value="5">SC/APC</option>
      <option value="6">ST/UPC</option>


      <%--   <option class="opConect_E2000" value="8">E20/APC(E2000 con licencia)</option>
      <option value="9">E20/UPC(E2000 con licencia)</option>
      <option class="opConect_E2000" value="10">LSH/APC(E2000 genérico)</option>
      <option value="11">LSH/UPC(E2000 genérico)</option>--%>
  </select>
            </div>
            <br>
            <br>
            <div class="siete">
                Longitud
  <input style="width: 60px;" maxlength="4" id="opLongitud" value="1" size="3" type="number" min="1">
                (metros)
            </div>
            <br>
            <br>
            <div class="ocho">
                Cantidad
  <input style="width: 60px;" maxlength="4" id="opCantidad" value="1" size="3" type="number" min="1">
                (piezas)
            </div>
        </div>
        <div class="columna2">
            <span id="tipoDeCable"></span>
            <span id="tipoDeFibra"></span>
            <span id="diametro"></span>
            <span id="conector1"></span>
            <span id="conector2"></span>
            <span id="longitud"></span>
            <span id="precioTotal" style="display: none;"></span>

            <p class="productoCotizado">
                <strong>Número de Parte: &nbsp;</strong> <span id="noParte">############</span>    <span id="agregar" runat="server" class="buttonA" onclientclick="btn_clickCalcular">Agregar +</span><br>
                <br>
                <strong><span class="listaDes"></span></strong>
                <br>
            </p>
            <div class="productoEncontrado">
                El Producto: <strong><span id="noParte2">############</span></strong> esta disponible para comprar en linea:<br>
                <a id="verAhora" href="" target="_blank">Ver detalles</a>
            </div>
            <span style="font-size: 16px;">* Agrega los productos que deseas cotizar.</span><br>


            <div id="calculando" style="background: linear-gradient(90deg, rgb(221 145 68) 39%, rgb(221 162 89) 58%, rgb(223 177 73) 91%); padding: 5px 12px; border: solid 1px #dfdbd2; border-radius: 12px; text-align: center; display: none">
                <b style="color: aliceblue">Calculando precio....</b>
            </div>


            <br>
            Mi lista:
  <ol id="miLista">
  </ol>

            <div id="datos_form" style="text-align: center;">
                <form runat="server">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>

                            <div runat="server" id="errorLista" style="background: #cbeaf6; border: solid 1px #33e3ff; padding: 5px 5px; border-radius: 5px;" visible="false">
                                <b>¡¡No hay productos en la lista!!</b> &nbsp;&nbsp; <a href="javascript:cerrarError();" style="color: red">Cerrar </a>

                            </div>


                            <br />
                            <br />
                            <br />
                            Ingresa tu correo para finalizar cotización*:
      <asp:TextBox ID="correo" runat="server" size="45" title="Ingresa un formato de correo válido" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" placeholder="Ingresa tu email" Style="text-align: center; margin-top: 5px; margin-bottom: 5px;" required></asp:TextBox>
                            <br>
                            <asp:Button ID="Button1" runat="server" class="button" Text="Cotizar" OnClick="Button1_Click" />
                            <div runat="server" id="coti_exitosa" visible="false" style="color: #099225">
                                <strong>Solicitud enviada con éxito ✓</strong><br>
                                Esta página se recargara en <strong><span id="cuentaRegresiva">8</span></strong> segundos...



                            </div>
                            <asp:TextBox TextMode="MultiLine" ID="__email" runat="server" Style="display: none;"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </form>

            </div>


        </div>
    </div>
    <br>
    <div id="resultado" class="contenido configRes">
        <div id="tipo_fibra" class="monomodo"></div>
        <div id="tipo_conector_derec"></div>
        <div id="tipo_conector_izq"></div>
        <div id="cuadro_blanco"></div>
    </div>
    <div id="clone" class="contenido configRes">
    </div>

    <div class="contenido longitud"><span id="resultadoLongitud" style="background: #FFFFFF; padding: 5px;">1</span></div>
    <br>



    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>



    <script type="text/javascript">

        function generar() {

            var presentacion = document.getElementById("presentacion").value; // // obtiene el valor del tipo de presentación
            var opTipoCab = document.getElementById("opTipoCab").value;  // obtiene el valor del tipo de cable (simplex-duplex)
            var opTipoFib = document.getElementById("opTipoFib").value; //obtiene el valor de tipo de fibra
            var opDiamCab = document.getElementById("opDiamCab").value; //obtiene el valor del diametro de la fibra
            var opConectDer = document.getElementById("opConectDer").value; // obtiene el valor del conector derecho
            var opConectIzq = document.getElementById("opConectIzq").value; // obtiene el valor del conector izquierdo
            var opLongitud = document.getElementById("opLongitud").value; // Obtiene la longitud
            var opCantidad = document.getElementById("opCantidad").value; // Obtiene la longitud

            var uno, dos, tres, cuatro, cinco, seis, siete;

            switch (presentacion) {
                case "1":
                    $("#opConectDer,#tipo_conector_derec, #cuadro_blanco").css({ 'visibility': 'visible' });
                    $(".OP_Para_SD,.op_6mm,.op_7mm").css({ 'display': 'block' });
                    uno = "J";
                    break;

                case "2":
                    $("#opConectDer,#tipo_conector_derec,#cuadro_blanco").css({ 'visibility': 'hidden' });
                    $(".OP_Para_SD,.op_6mm,.op_7mm").css({ 'display': 'block' });
                    uno = "P";
                    break;
            }

            switch (opTipoCab) {
                case "simplex":
                    $(".op_6mm, .op_7mm").css({ 'display': 'none' });

                    dos = "S";


                    break;
                case "duplex":
                    $(".op_6mm, .op_7mm").css({ 'display': 'none' });
                    dos = "D"
                    break;

                case "unitubo 6 fibras": dos = "6";
                    $(".OP_Para_SD,.op_7mm").css({ 'display': 'none' });

                    break;
                case "unitubo 12 fibras":
                    $(".OP_Para_SD,.op_6mm").css({ 'display': 'none' });
                    dos = "12"; break;

            }

            switch (opTipoFib) {
                case "modomodoG652":
                    $("#tipo_fibra").removeClass();
                    $("#tipo_fibra").addClass("monomodo");
                    $(".opConect_APC").css({ 'display': 'block' });
                    $(".opConect_E2000").css({ 'display': 'block' });

                    tres = "9";

                    break;
                case "multimodoOM2":
                    $("#tipo_fibra").removeClass();
                    $("#tipo_fibra").addClass("multimodo");
                    $(".opConect_APC").css({ 'display': 'none' });
                    $(".opConect_E2000").css({ 'display': 'none' });
                    tres = "5";

                    break;
                //case 3:  break; 
                case "multimodoOM1":
                    $("#tipo_fibra").removeClass();
                    $("#tipo_fibra").addClass("multimodoOM1");
                    $(".opConect_APC").css({ 'display': 'none' });
                    $(".opConect_E2000").css({ 'display': 'none' });
                    tres = "6";

                    break;

                case "multimodoOM3":
                    $("#tipo_fibra").removeClass();
                    $("#tipo_fibra").addClass("multimodo10gb");
                    $(".opConect_APC").css({ 'display': 'none' });
                    $(".opConect_E2000").css({ 'display': 'none' });
                    tres = "O";
                    break;
                case "modomodoG657":
                    $("#tipo_fibra").removeClass();
                    $("#tipo_fibra").addClass("monomodo-g657");
                    $(".opConect_APC").css({ 'display': 'block' });
                    $(".opConect_E2000").css({ 'display': 'block' });

                    tres = "A";
                    break;





                case "multimodoOM4":
                    $("#tipo_fibra").removeClass();
                    $("#tipo_fibra").addClass("multimodoOM4");
                    $(".opConect_APC").css({ 'display': 'none' });
                    $(".opConect_E2000").css({ 'display': 'none' });

                    tres = "4";
                    break;


            }
            switch (opDiamCab) {
                case "1.6 mm":

                    cuatro = "16";
                    break;
                case "2 mm":

                    cuatro = "2";
                    break;
                case "3":

                    cuatro = "3"; break;
                case "5.2 mm": cuatro = "5"; break;
                case "7 mm": cuatro = "7"; break;
            }

            switch (opConectIzq) {
                case "1":
                    $("#tipo_conector_izq ").removeClass();
                    $("#tipo_conector_izq").addClass("fcu");
                    cinco = "FCU";
                    break;

                case "2":
                    $("#tipo_conector_izq").removeClass();
                    $("#tipo_conector_izq").addClass("fca");
                    cinco = "FCA";
                    break;

                case "3":
                    $("#tipo_conector_izq").removeClass();
                    $("#tipo_conector_izq").addClass("lcu");
                    cinco = "LCU";

                    break;
                case "4":
                    $("#tipo_conector_izq").removeClass();
                    $("#tipo_conector_izq").addClass("scu");
                    cinco = "SCU";
                    break;
                case "5":
                    $("#tipo_conector_izq").removeClass();
                    $("#tipo_conector_izq").addClass("sca");
                    cinco = "SCA";
                    break;
                case "6":
                    $("#tipo_conector_izq").removeClass();
                    $("#tipo_conector_izq").addClass("stu");
                    cinco = "STU";
                    break;
                //Agregar imagenes de conectores a partir de éste (checar codigo para agregarlas)
                //case "8":
                //    $("#tipo_conector_izq").removeClass();
                //    $("#tipo_conector_izq").addClass("testConectorIzquierdo");
                //    cinco = "E20A";
                //    break;
                //case "9":
                //    $("#tipo_conector_izq").removeClass();
                //    $("#tipo_conector_izq").addClass("testConectorIzquierdo");
                //    cinco = "E20U";
                //    break;
                //case "10":
                //    $("#tipo_conector_izq").removeClass();
                //    $("#tipo_conector_izq").addClass("testConectorIzquierdo");
                //    cinco = "LSHA";
                //    break;
                //case "11":
                //    $("#tipo_conector_izq").removeClass();
                //    $("#tipo_conector_izq").addClass("testConectorIzquierdo");
                //    cinco = "LSHU";
                //    break;




            }

            switch (opConectDer) {
                case "1":
                    $("#tipo_conector_derec").removeClass();
                    $("#tipo_conector_derec").addClass("fcu");
                    seis = "FCU";

                    break;

                case "2":
                    $("#tipo_conector_derec").removeClass();
                    $("#tipo_conector_derec").addClass("fca");
                    seis = "FCA";
                    break;

                case "3":
                    $("#tipo_conector_derec").removeClass();
                    $("#tipo_conector_derec").addClass("lcu");
                    seis = "LCU";
                    break;

                case "4":
                    $("#tipo_conector_derec").removeClass();
                    $("#tipo_conector_derec").addClass("scu");
                    seis = "SCU";
                    break;
                case "5":
                    $("#tipo_conector_derec").removeClass();
                    $("#tipo_conector_derec").addClass("sca");
                    seis = "SCA";
                    break;
                case "6":
                    $("#tipo_conector_derec").removeClass();
                    $("#tipo_conector_derec").addClass("stu");
                    seis = "STU";
                    break;
                //case 3:  break; 

                //Agregar imagenes de conectores a partir de éste 

                //case "8":
                //    $("#tipo_conector_derec").removeClass();
                //    $("#tipo_conector_derec").addClass("test");
                //    seis = "E20A";
                //    break;
                //case "9":
                //    $("#tipo_conector_derec").removeClass();
                //    $("#tipo_conector_derec").addClass("test");
                //    seis = "E20U";
                //    break;
                //case "10":
                //    $("#tipo_conector_derec").removeClass();
                //    $("#tipo_conector_derec").addClass("test");
                //    seis = "LSHA";
                //    break;
                //case "11":
                //    $("#tipo_conector_derec").removeClass();
                //    $("#tipo_conector_derec").addClass("test");
                //    seis = "LSHU";
                //    break;


            }







            if (opTipoFib == "multimodoOM2" && opConectDer == "1") { fcu_multimodo_derec(); } // Evalua el tipo de fibra es multimodo y el conector derecho
            if (opTipoFib == "multimodoOM1" && opConectDer == "1") { fcu_multimodo_derec(); } // Evalua el tipo de fibra es multimodo y el conector derecho
            if (opTipoFib == "multimodoOM3" && opConectDer == "1") { fcu_multimodo_derec(); } // Evalua el tipo de fibra es multimodo y el conector derecho
            if (opTipoFib == "multimodoOM4" && opConectDer == "1") { fcu_multimodo_derec(); }


            if (opTipoFib == "multimodoOM2" && opConectIzq == "1") { fcu_multimodo_izq(); } // Evalua el tipo de fibra es multimodo y el conector izquierdo
            if (opTipoFib == "multimodoOM1" && opConectIzq == "1") { fcu_multimodo_izq(); } // Evalua el tipo de fibra es multimodo y el conector izquierdo
            if (opTipoFib == "multimodoOM3" && opConectIzq == "1") { fcu_multimodo_izq(); } // Evalua el tipo de fibra es multimodo y el conector izquierdo
            if (opTipoFib == "multimodoOM4" && opConectIzq == "1") { fcu_multimodo_izq(); }

            if (opTipoFib == "multimodoOM2" && opConectDer == "3") { lcu_multimodo_derec(); } // Evalua el tipo de fibra es multimodo y el conector derecho es LCU
            if (opTipoFib == "multimodoOM1" && opConectDer == "3") { lcu_multimodo_derec(); } // Evalua el tipo de fibra es multimodo y el conector derecho es LCU
            if (opTipoFib == "multimodoOM3" && opConectDer == "3") { lcu_multimodo_derec(); } // Evalua el tipo de fibra es multimodo y el conector derecho es LCU
            if (opTipoCab == "multimodoOM4" && opConectDer == "3") { lcu_multimodo_derec(); }

            if (opTipoFib == "multimodoOM2" && opConectIzq == "3") { lcu_multimodo_izq(); } // Evalua el tipo de fibra es multimodo y el conector izquierdo es LCU
            if (opTipoFib == "multimodoOM1" && opConectIzq == "3") { lcu_multimodo_izq(); } // Evalua el tipo de fibra es multimodo y el conector izquierdo es LCU
            if (opTipoFib == "multimodoOM3" && opConectIzq == "3") { lcu_multimodo_izq(); } // Evalua el tipo de fibra es multimodo y el conector izquierdo es LCU
            if (opTipoFib == "multimodoOM4" && opConectIzq == "3") { lcu_multimodo_izq(); }

            if (opTipoFib == "multimodoOM2" && opConectDer == "4") { scu_multimodo_derec(); } // Evalua el tipo de fibra es multimodo y el conector derecho es SCU
            if (opTipoFib == "multimodoOM1" && opConectDer == "4") { scu_multimodo_derec(); } // Evalua el tipo de fibra es multimodo y el conector derecho es SCU
            if (opTipoFib == "multimodoOM3" && opConectDer == "4") { scu_multimodo_derec(); } // Evalua el tipo de fibra es multimodo y el conector derecho es SCU
            if (opTipoFib == "multimodoOM4" && opConectDer == "4") { scu_multimodo_derec(); }

            if (opTipoFib == "multimodoOM2" && opConectIzq == "4") { scu_multimodo_izq(); } // Evalua el tipo de fibra es multimodo y el conector izquierdo es SCU
            if (opTipoFib == "multimodoOM1" && opConectIzq == "4") { scu_multimodo_izq(); } // Evalua el tipo de fibra es multimodo y el conector izquierdo es SCU
            if (opTipoFib == "multimodoOM3" && opConectIzq == "4") { scu_multimodo_izq(); } // Evalua el tipo de fibra es multimodo y el conector izquierdo es SCU
            if (opTipoFib == "multimodoOM4" && opConectIzq == "4") { scu_multimodo_izq(); }


            if (opTipoFib == "multimodoOM2" && opConectDer == "6") { stu_multimodo_derec(); } // Evalua el tipo de fibra es multimodo y el conector derecho es STU
            if (opTipoFib == "multimodoOM1" && opConectDer == "6") { stu_multimodo_derec(); } // Evalua el tipo de fibra es multimodo y el conector derecho es STU
            if (opTipoFib == "multimodoOM3" && opConectDer == "6") { stu_multimodo_derec(); } // Evalua el tipo de fibra es multimodo y el conector derecho es STU
            if (opTipoCab == "multimodoOM4" && opConectDer == "6") { stu_multimodo_derec(); }

            if (opTipoFib == "multimodoOM2" && opConectIzq == "6") { stu_multimodo_izq(); } // Evalua el tipo de fibra es multimodo y el conector izquierdo es STU
            if (opTipoFib == "multimodoOM1" && opConectIzq == "6") { stu_multimodo_izq(); } // Evalua el tipo de fibra es multimodo y el conector izquierdo es STU
            if (opTipoFib == "multimodoOM3" && opConectIzq == "6") { stu_multimodo_izq(); } // Evalua el tipo de fibra es multimodo y el conector izquierdo es STU
            if (opTipoFib == "multimodoOM4" && opConectIzq == "6") { stu_multimodo_izq(); }


            var opTipoCab_HTML = $("#opTipoCab").html();
            if (opTipoCab == "duplex") {
                $("#clone").show();
                $("#clone").html(" ");
                $("#resultado").clone().prependTo("#clone");
            }
            else {
                $("#clone").hide();
            };

            $("#resultadoLongitud").text(opLongitud + " m");

            siete = parseFloat(opLongitud);

            var cero = "";
            if (siete < 10) { cero = "0" }

            siete = opLongitud;

            var punto = siete.indexOf('.'); if (punto == 0) { cero = "00" } //Función que detecta si la longitud es tecleada de la forma fración ejemplo: ".5" o "0.5"

            if (presentacion == "2") { seis = ""; } // Calcula la presentacion para quitar o poner la "/" que separa el número de parte



            var noParte = uno + dos + tres + cuatro + cinco + "/" + seis + cero + siete; // Genera el numero de parte a partir del valor de las variables

            var presentacion = $("#presentacion option:selected").text(); // // obtiene el valor del tipo de presentación
            var opTipoCab = $("#opTipoCab option:selected").text();  // obtiene el valor del tipo de cable (simplex-duplex)
            var opTipoFib = $("#opTipoFib option:selected").text(); //obtiene el valor de tipo de fibra
            var opDiamCab = $("#opDiamCab option:selected").text(); //obtiene el valor del diametro de la fibra
            var opConectDer = $("#opConectDer option:selected").text(); // obtiene el valor del conector derecho
            var opConectIzq = $("#opConectIzq option:selected").text(); // obtiene el valor del conector izquierdo
            var opLongitud = $("#opLongitud").val(); // Obtiene la longitud



            $("#noParte,#noParte2 ").text(noParte); // Insert el numero de parte en el html
            //Genera la descripción
            if (presentacion == "Pigtail") { opConectDer = ""; }
            $(".listaDes").text(presentacion + " " + opTipoCab + " " + opTipoFib + " " + opDiamCab + " " + opConectIzq + " - " + opConectDer + " " + opLongitud + "m • " + opCantidad + "pza");
            var noParte = $("#noParte").text();

            alta = { "JS92FCU/LCU05": "a", "JS92LCU/SCU05": " ", "JS92FCU/SCU03": " ", "JS92SCU/SCU05": " ", "JS62LCU/LCU03": " ", "JD62LCU/LCU15": " ", "JS62LCU/SCU30": " ", "JS92FCA/FCA01": " ", "JS92FCA/FCA02": " ", "JS92FCA/FCA03": " ", "JS92FCA/FCA05": " ", "JS92FCA/FCA10": " ", "JS92FCU/FCU01": " ", "JS92FCU/FCU02": " ", "JS92FCU/FCU03": " ", "JS92FCU/FCU05": " ", "JS92FCU/FCU10": " ", "JS92FCU/LCU01": " ", "JS92FCU/LCU02": " ", "JS92FCU/LCU03": " ", "JS92FCU/LCU10": " ", "JS92FCU/SCU01": " ", "JS92FCU/SCU02": " ", "JS92FCU/SCU05": " ", "JS92FCU/SCU10": " ", "JS92FCU/STU01": " ", "JS92FCU/STU02": " ", "JS92FCU/STU03": " ", "JS92FCU/STU05": " ", "JS92FCU/STU10": " ", "JS92LCU/LCU01": " ", "JS92LCU/LCU02": " ", "JS92LCU/LCU03": " ", "JS92LCU/LCU05": " ", "JS92LCU/LCU10": " ", "JS92LCU/SCA01": " ", "JS92LCU/SCA02": " ", "JS92LCU/SCA03": " ", "JS92LCU/SCA05": " ", "JS92LCU/SCA10": " ", "JS92LCU/SCU01": " ", "JS92LCU/SCU02": " ", "JS92LCU/SCU03": " ", "JS92LCU/SCU10": " ", "JS92SCA/SCA01": " ", "JS92SCA/SCA02": " ", "JS92SCA/SCA03": " ", "JS92SCA/SCA05": " ", "JS92SCA/SCA10": " ", "JS92SCU/SCU01": " ", "JS92SCU/SCU02": " ", "JS92SCU/SCU03": " ", "JS92SCU/SCU10": " ", "JS92STU/STU01": " ", "JS92STU/STU02": " ", "JS92STU/STU03": " ", "JS92STU/STU05": " ", "JS92STU/STU10": " ", "JD62LCU/LCU05": " ", "JD62LCU/LCU20": " ", "JD62SCU/SCU03": " ", "JD62SCU/SCU05": " ", "JD92FCU/SCU20": " ", "JD92LCU/SCU03": " ", "JS62LCU/LCU60": " ", "JS62LCU/SCU15": " ", "JS62LCU/SCU20": " ", "JS62LCU/SCU40": " ", "JS62SCU/SCU05": " ", "JS62SCU/SCU10": " ", "JS92FCU/SCU15": " ", "JS92LCU/SCU15": " ", "JS92LCU/SCU20": " ", "JS92LCU/SCU30": " ", "JD92LCU/LCU02": " ", "JD92LCU/SCU02": " ", "JS52LCU/LCU03": " ", "JS52LCU/SCU03": " ", "JS52SCU/SCU05": " ", "JS52SCU/SCU15": " ", "JS62LCU/LCU01": " ", "JS62SCU/SCU02": " ", "JSO16LCU/LCU02": " ", "JSO16LCU/SCU02": " ", "JSO16SCU/SCU02": " ", "JS53FCU/SCU03": " ", "JS53LCU/LCU03": " ", "JS53LCU/LCU00.6": " ", "JS52FCU/SCU02.5": " ", "JS62LCU/SCU05.6": " ", "JS62LCU/SCU06.3": " ", "JS62LCU/SCU06.4": " ", "JS62LCU/SCU06.6": " ", "JS62LCU/SCU06.9": " ", "JS62LCU/SCU07": " ", "JS62LCU/SCU07.3": " ", "JS62LCU/SCU07.4": " ", "JS62LCU/SCU07.5": " ", "JS62LCU/SCU07.6": " ", "JS62LCU/SCU07.8": " ", "JS62LCU/SCU07.9": " ", "JS62LCU/SCU08.1": " ", "JS62LCU/SCU09.2": " ", "JS62LCU/SCU17": " ", "JS63FCU/FCU03": " ", "JS63FCU/FCU08": " ", "JS63FCU/SCU01": " ", "JS63FCU/SCU03": " ", "JS63FCU/STU03": " ", "JS63LCU/LCU05": " ", "JS63LCU/SCU19.4": " ", "JS63LCU/SCU19.7": " ", "JS63LCU/SCU20": " ", "JS63STU/STU05": " ", "JS916FCU/FCU03": " ", "JS916LCU/SCU05": " ", "JS916LCU/SCU09": " ", "JS916LCU/SCU30": " ", "JS916SCU/SCU05": " ", "JS916LCU/SCU10.5": " ", "JS92FCA/FCU05": " ", "JS92FCA/LCU05": " ", "JS92FCU/LCU17.5": " ", "JS92FCU/LCU18.5": " ", "JS92FCU/LCU23": " ", "JS92FCU/SCA05": " ", "JS92FCU/SCU30": " ", "JS92LCU/SCU03.3": " ", "JS92LCU/SCU03.8": " ", "JS92LCU/SCU04.7": " ", "JS92LCU/SCU05.2": " ", "JD62FCU/LCU10": " ", "JD62FCU/LCU05": " ", "JD62FCU/FCU05": " ", "JD93FCU/LCU10": " " };



            var valorMasGrande = -1;
            var elementoMasGrande = null;

            for (var i in alta) {
                valorMasGrande = alta[i]
                elementoMasGrande = i;
                if (noParte == elementoMasGrande) {
                    break;
                }


            }


            if (noParte == elementoMasGrande) {
                $('.productoEncontrado').fadeIn(800);
                $('#verAhora').attr({ "href": "https://www.incom.mx/productos/buscar?busqueda=" + elementoMasGrande, "target": "_blank" });

            }
            else { $('.productoEncontrado').fadeOut(400); }

            lista()


            //document.getElementById("tipo_conector_izq").style.msTransform = "rotateY(180deg)"; 

        } // fin de mi funcion generar


        //funciones para mostrar el tipo de conector en base a multimono  o monomodo 

        function fcu_multimodo_derec() { //Funcion que aplica el estilo de conector multimodo FCU del lado derecho
            $("#tipo_conector_derec").removeClass(); $("#tipo_conector_derec").addClass("fcu_multimodo");
        }
        function fcu_multimodo_izq() { //Funcion que aplica el estilo de conector multimodo FCU del lado izquierdo
            $("#tipo_conector_izq").removeClass(); $("#tipo_conector_izq").addClass("fcu_multimodo");
        }

        function lcu_multimodo_derec() { //Funcion que aplica el estilo de conector multimodo LCU del lado derecho
            $("#tipo_conector_derec").removeClass(); $("#tipo_conector_derec").addClass("lcu_multimodo");
        }

        function lcu_multimodo_izq() { //Funcion que aplica el estilo de conector multimodo LCU del lado izquierdo
            $("#tipo_conector_izq").removeClass(); $("#tipo_conector_izq").addClass("lcu_multimodo");
        }

        function scu_multimodo_derec() { //Funcion que aplica el estilo de conector multimodo LCU del lado derecho
            $("#tipo_conector_derec").removeClass(); $("#tipo_conector_derec").addClass("scu_multimodo");
        }

        function scu_multimodo_izq() { //Funcion que aplica el estilo de conector multimodo LCU del lado derecho
            $("#tipo_conector_izq").removeClass(); $("#tipo_conector_izq").addClass("scu_multimodo");
        }

        function stu_multimodo_derec() { //Funcion que aplica el estilo de conector multimodo LCU del lado derecho
            $("#tipo_conector_derec").removeClass(); $("#tipo_conector_derec").addClass("stu_multimodo");
        }

        function stu_multimodo_izq() { //Funcion que aplica el estilo de conector multimodo LCU del lado derecho
            $("#tipo_conector_izq").removeClass(); $("#tipo_conector_izq").addClass("stu_multimodo");
        }

        function btn_clickCalcular(t) {



        }

        function mostrarLoad() {
            div = document.getElementById('calculando');
            div.style.display = 'block';
        }
        function ocultarLoad() {
            div1 = document.getElementById('calculando');
            div1.style.display = 'none';
        }

        $(document).ready(function () {



            $("#opTipoFib,#opConectDer, .contenido").on("click keyup mouseleave mouseenter focus hover mousemove", function () {
                generar();
            });

            $("#cotizar").click(function () {
                alert("Cotización enviada con éxito");
            });






            $("#agregar").click(function () {
                btn_clickCalcular(this);


                mostrarLoad();
                CalcularPrecio();

                setTimeout(() => {

                    var noParteContent = $("#noParte").text();

                    var presentacion = $("#presentacion option:selected").text(); // // obtiene el valor del tipo de presentación
                    var opTipoCab = $("#opTipoCab option:selected").text();  // obtiene el valor del tipo de cable (simplex-duplex)
                    var opTipoFib = $("#opTipoFib option:selected").text(); //obtiene el valor de tipo de fibra
                    var opDiamCab = $("#opDiamCab option:selected").text(); //obtiene el valor del diametro de la fibra
                    var opConectDer = $("#opConectDer option:selected").text(); // obtiene el valor del conector derecho
                    var opConectIzq = $("#opConectIzq option:selected").text(); // obtiene el valor del conector izquierdo
                    var opLongitud = $("#opLongitud").val(); // Obtiene la longitud
                    var opCantidad = $("#opCantidad").val(); // Obtiene la cantidad



                    let precioTotal = $("#precioTotal").text();


                    if (precioTotal == "$0.00 USD") {
                        precioTotal = "- Cotizalo";
                    }
                    else {
                        precioTotal = precioTotal;
                    }




                    if (presentacion == "Pigtail") { opConectDer = "" }
                    $("ol").append("<li onclick='$(this).remove();'><strong>" + noParteContent + "</strong> <br> <span style='font-size:14px;'>"
                        + presentacion + " " + opTipoCab + " " + opTipoFib + " "
                        + opDiamCab + " " + opConectIzq + " - " + opConectDer + "<br> "
                        + opLongitud + " mt • " + opCantidad + " pza</span>  <strong style='color: #383738ad'> " + precioTotal + " </strong></li> <br>");

                    lista()
                    explorer()
                    ocultarLoad()




                }, 4000);
















            });

        });



        function CalcularPrecio() {

            /* $(".opConect_E2000").css({ 'display': 'none' });*/

            //var msj = document.getElementById("calculando").value;
            //msj.css = ({ 'display': 'block' });


            //codigo nuevo 22022022 para api

            var presentacion = document.getElementById("presentacion").value; // // obtiene el valor del tipo de presentación
            var opTipoCab = document.getElementById("opTipoCab").value;  // obtiene el valor del tipo de cable (simplex-duplex)
            var opTipoFib = document.getElementById("opTipoFib").value; //obtiene el valor de tipo de fibra
            var opDiamCab = document.getElementById("opDiamCab").value; //obtiene el valor del diametro de la fibra
            var opConectDer = document.getElementById("opConectDer").value; // obtiene el valor del conector derecho
            var opConectIzq = document.getElementById("opConectIzq").value; // obtiene el valor del conector izquierdo
            var opLongitud = document.getElementById("opLongitud").value; // Obtiene la longitud
            var opCantidad = document.getElementById("opCantidad").value; //Obtiene la cantidad de pzas

            let precioTotal = 0;



            //Llamada con ajax

            const request = new XMLHttpRequest();


            var parametrosJumper = opTipoCab + "/" + opTipoFib + "/" + opDiamCab + "/" + opConectIzq + "/" + opConectDer + "/" + opLongitud + "/" + opCantidad;
            var parametrosPigtail = opTipoCab + "/" + opTipoFib + "/" + opDiamCab + "/" + opConectIzq + "/" + opLongitud + "/" + opCantidad;

            const urlJ = 'https://apiincom.it4you.mx/api/cotizadorJsJumper/' + parametrosJumper + ' ';
            const urlP = 'https://apiincom.it4you.mx/api/cotizadorJsPigtail/' + parametrosPigtail + ' ';

            //const urlJ = '   http://localhost:1265/api/cotizadorJsJumper/' + parametrosJumper + ' ';
            //const urlP = '   http://localhost:1265/api/cotizadorJsPigtail/' + parametrosPigtail + ' ';


            let urlFnal = "";

            if (presentacion == "1") {
                urlFnal = urlJ;
            }
            else {
                urlFnal = urlP;
            }

            request.open('GET', urlFnal, true);

            request.send();

            request.onreadystatechange = function () {

                if (request.readyState == 4) {

                    if (request.status == 200) {

                        precioTotal = parseFloat(request.responseText).toFixed(2);
                        console.log(precioTotal);
                        document.getElementById('precioTotal').innerHTML = "$" + precioTotal + " USD";
                    }
                    else {

                        precioTotal = "Cotizalo";
                        console.log("Ocurrio un error"
                            + request.responseText);
                        document.getElementById('precioTotal').innerHTML = precioTotal;

                    }
                }
            }






        }






        function lista() {
            textarea = $("#__email").val();
            listaD = $("#miLista").html()
            noParteG = $("#noParte").text(); // Obtiene el número de parte generado
            descG = $(".listaDes").text(); // Obtiene  la descripción generada

            //listaD = listaD.replace(/this/g, " ").replace(/onclick/g, " ").replace(/remove()/g, " ").replace(/();"/g, " ").replace(/$/g, " ");

            $("#__email").val(listaD);

        }
        function bloqueo_explorer() {
            $("#tipo_fibra,#tipo_conector_derec,#tipo_conector_izq,#cuadro_blanco,#clone").hide();
            $("#resultado").html("<img src='img/mensaje_ie.gif' />");
        }

        function explorer()
        // Returns the version of Internet Explorer or a -1
        // (indicating the use of another browser).
        {
            var nav = navigator.appName;

            // Detectamos si nos visitan desde IE
            if (nav == "Microsoft Internet Explorer") {
                // Convertimos en minusculas la cadena que devuelve userAgent
                var ie = navigator.userAgent.toLowerCase();
                // Extraemos de la cadena la version de IE
                var version = parseInt(ie.split('msie')[1]);

                // Dependiendo de la version mostramos un resultado
                switch (version) {
                    case 6:
                        bloqueo_explorer();
                        break;
                    case 7:
                        bloqueo_explorer();
                        break;
                    case 8:
                        bloqueo_explorer();
                        break;
                    case 9:
                        bloqueo_explorer();
                        break;
                    default:

                        break;
                }
            }
        }


        function cerrarError() {
            div = document.getElementById('errorLista');
            div.style.display = 'none';

        }
    </script>


</body>



</html>
<style>
    .loader {
        border: 16px solid #f3f3f3; /* Light grey */
        border-top: 16px solid #3498db; /* Blue */
        border-radius: 50%;
        width: 120px;
        height: 120px;
        animation: spin 2s linear infinite;
    }

    @keyframes spin {
        0% {
            transform: rotate(0deg);
        }

        100% {
            transform: rotate(360deg);
        }
    }
</style>

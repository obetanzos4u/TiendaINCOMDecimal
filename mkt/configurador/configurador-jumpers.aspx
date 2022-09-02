<%@ Page Title="Configurador de jumpers" Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" ValidateRequest="false" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <title>Configurador de jumpers</title>
    <link href="style.css" rel="stylesheet" type="text/css">
    <%--<script src="tailwind.js"></script>--%>
    <%--<link rel="stylesheet" href="../../css/IncomStyles.css" type="text/css" />--%>
</head>

<body onload="generar();explorer()">
    <div class="header"></div>

    <div class="contenido">
        <h1 class="borderTest">Configurador de Jumpers Fibra Óptica</h1>
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
    <div class="contenido longitud">
        <span id="resultadoLongitud" style="background: #FFFFFF; padding: 5px;">1</span>
    </div>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="script.js" type="text/javascript"></script>
</body>
</html>

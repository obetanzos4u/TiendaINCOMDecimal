<%@ Page Language="C#" AutoEventWireup="true" CodeFile="_testFetch-js.aspx.cs" Inherits="_testFetch_js" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input id="txt_numero_parte" type="text" placeholder="Ingresa No.Parte" />
            <input type="button" onclick="obtenerStock();" value="Obtener" />

            <br />
            <div id="disponibilidad"></div>
        </div>
    </form>
</body>

    <script>

        function obtenerStock() {
            var numero_parte = document.querySelector("#txt_numero_parte").value;
            fetch('http://localhost:52310/service/productos_stock.aspx?numero_parte=' + numero_parte)
                .then(function (response) {
                    return response.json();
                })
                .then(function (myJson) {
                    console.log(myJson);


                    construirTabla(myJson);

                });
        }

        function construirTabla(json) {
            var filas = "";

            Object.entries(json).forEach(([key, value]) => {
                var numero_parte = value.numero_parte;
                var PlanningAreaID = value.PlanningAreaID;
                var totalDisponible = value.totalDisponible;
                filas += `
                <tr>

                    <td> ${PlanningAreaID} </td>
                        <td> ${totalDisponible} </td>

                    </tr>`;


                
            })




            var table = `<table>
              <th>Ubicación</th>
              <th>Cantidad</th>
                
                    ${filas} 

        </table>`;



            document.querySelector("#disponibilidad").innerHTML = table;
        }
    </script>
</html>

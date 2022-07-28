var IDcontentTable;
var elementsTxtCantidad;



/*

function stockDisponiblidad(_IDcontentTable, _elementsTxtCantidad) {

    IDcontentTable = _IDcontentTable;
    elementsTxtCantidad = _elementsTxtCantidad;

    console.log(elementsTxtCantidad);
    console.log(elementsTxtCantidad.length);
 
    var i;
    
    for (i = 0; i < elementsTxtCantidad.length; ++i) {
        console.log(elementsTxtCantidad[i]);
        elementsTxtCantidad[i].setAttribute("input", " alert('hola');");  
    }

}*/
function loadDisponibilidad(btn, txtCantidad, numero_parte) {
 
    console.log(btn);
    var idLoading = btnLoadingHide(btn);
    console.log(idLoading);
    fetch('/service/productos_stock.aspx?numero_parte=' + numero_parte + '&cantidad=' + txtCantidad.value)
        .then(function (response) {
            console.log(response);
            return response.json();
        })
        .then(function (myJson) {
            console.log(myJson);
            if (myJson === null) {
                productoDisponibilidadDataTable("null");
            } else {
                productoDisponibilidadDataTable(myJson);
            }
            btnLoadingShow(btn, idLoading);

        });


}


function productoDisponibilidadDataTable(json) {

    if (json === "null") {

        document.querySelector("#productoDisponibilidad").innerHTML = `Para consultar la disponibilidad Inicia Sesión`;


        return null;
    } else {

        var filas = "";

        Object.entries(json).forEach(([key, value]) => {
            var numero_parte = value.numero_parte;
            var PlanningAreaID = value.PlanningAreaID;
            var PlanningAreaName = value.PlanningAreaName;
            var totalDisponible = value.totalDisponible_str;
            filas += `
                <tr>

                    <td> ${PlanningAreaName} </td>
                        <td> ${totalDisponible} </td>

                    </tr>`;

        });


        var table = `<table style="width:400px;">
              <th>Ubicación</th>
              <th>Disponibilidad</th>
                
                    ${filas} 

        </table>`;



        document.querySelector("#productoDisponibilidad").innerHTML = table;

 

    }
    }
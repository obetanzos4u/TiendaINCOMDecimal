<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/herramientas/_masterConfigV2.master" Async="true"
    CodeFile="admin-precios.aspx.cs"
    Inherits="herramientas_admin_precios" %>

<asp:Content runat="server" ContentPlaceHolderID="contenido">
    <div class="container">
        <div class="row">
            <h1 class="center-align">FastAdmin Precios</h1>

        </div>
        <div class="row">
            <h2>Load by text</h2>

        </div>

        <div class="row justify-content-between">
            <div class="col-9 ">

                <p>
                    Selecciona una plantilla del listado para obtener las columnas y orden de la información que vayas a cargar/actualizar/eliminar.
                    Posteriormente pega tu información desde excel y obten la vista previa.
                </p>
                <p>
                    <strong>Orden y columnas necesarios (no es necesario que las columnas se llamen igual, solo el orden): </strong>
                    <br />
                    <span id="FormatoCarga"></span>
                </p>
                <asp:LinkButton ID="btn_ObtenerVistaPrevia" OnClick="btn_ObtenerVistaPrevia2_Click" class="btn btn-info" runat="server">
                         Obtener vista previa
                </asp:LinkButton>
                <a  class="btn btn-outline-dark " onclick="ClearTxt();">Resetear campos</a>
            </div>
            <div class="col-3 ">


                <select id="ddl_Plantillas" class="form-select" onchange="MostrarPlantillasCarga()">
                    <option value="">Selecciona plantilla</option>
                    <option value="precios1">Cargar Precio (1) rango</option>
                    <option value="precios5">Cargar Precio (5) rangos</option>
                    <option value="cotizalo">Cargar Cotizalo</option>
                    <option value="listaFija">Cargar Precios Lista fija</option>
                    <option value="fantasma">Cargar Fantasma</option>
                    <option value="eliminar">Eliminar</option>
                    <option value="informe">Selección/Informe</option>
                </select>


            </div>
        </div>
     
        <div class="row justify-content-between d-none">
            <div class="col-8 ">

                <p>
                    Ingrese el número de parte por fila y orden de columnas establecidas
                </p>
            </div>
            <div class="col-4 ">
                <div class="row">
                    <div class="col-auto">
                        <select id="ddl_ColumnList" class="form-select ">
                            <option value="">Selecciona</option>
                            <option value="numero_parte">Número parte</option>

                            <option value="moneda">Moneda</option>
                            <option value="moneda">USD Moneda</option>
                            <option value="moneda">MXN Moneda</option>

                            <option value="precio">Precio</option>
                            <option value="max1">Max 1</option>

                            <option value="precio2">Precio 2</option>
                            <option value="max2">Max 2</option>

                            <option value="precio3">precio3</option>
                            <option value="max3">Max 3</option>

                        </select>
                    </div>
                    <div class="col-auto">
                        <a id="btn_AgregarColumn" onclick="AgregarColumn();" class="btn btn-primary" runat="server">Agregar</a>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-12  mb-3">
                <asp:TextBox ID="txt_Columns" ClientIDMode="Static" runat="server" CssClass="d-none"></asp:TextBox>
            </div>
            <div class="col-12 ">

                <div id="ContentButtonsColumms" class="col-6  btn-group" role="group">
                </div>
            </div>

            <div class="col-12  mb-3">
                <h3>Campo de carga</h3>
                <p>Es obligatorio que la primer linea sea el nombre de las columnas.</p>
                <asp:TextBox ID="txt_ListadoProductosActualizarPrecio" ClientIDMode="Static" style="height: 200px;" class="form-control"
                    TextMode="MultiLine" runat="server"></asp:TextBox>
            </div>


            <div class="col-12  mt-2 mb-3">
                <asp:GridView ID="gv_VistaPreview" class="table table-striped" runat="server"></asp:GridView>

            </div>
            <div class="col-12 ">

                <asp:LinkButton  ID="btn_ActualizarPreciosFromSAP" OnClientClick="return confirm('!! Confirma la acción de ACTUALIZAR precios desde SAP ¡¡ '); "
                  OnClick="btn_ActualizarPreciosFromSAP_Click"     CssClass="btn  btn-primary"  runat="server">
                  Actualizar precios from SAP
                </asp:LinkButton>


                <div class="btn-group">
                    <button class="btn btn-outline-primary dropdown-toggle" type="button" id="dd_Herramientas" data-bs-toggle="dropdown" aria-expanded="false">
                        Herramientas
                    </button>
                    <ul class="dropdown-menu" aria-labelledby="dd_Herramientas">
                        <li>
                            <asp:LinkButton ID="btn_ValidarExistencia" OnClick="btn_ValidarExistencia_Click" class="dropdown-item" runat="server">
                        Obtener informe de producto(s)  
                            </asp:LinkButton>

                        </li>


                    </ul>
                </div>
                <div class="btn-group">
                    <button class="btn btn-outline-primary dropdown-toggle" type="button" id="dd_Precios" data-bs-toggle="dropdown" aria-expanded="false">
                        Precios rangos o de lista.
                    </button>
                    <ul class="dropdown-menu" aria-labelledby="dd_Precios">

                        <li>
                            <asp:LinkButton ID="btn_DescargarPreciosRangos" OnClick="btn_DescargarPreciosRangos_Click" class="dropdown-item" runat="server">
                             Descargar Precios Rangos (xlsx)
                            </asp:LinkButton>

                        </li>
                        <li>
                            
                            <asp:LinkButton ID="btn_CargarPrecios1Rango" OnClick="btn_CargarPrecios1Rango_Click"
                                
                              class="dropdown-item" runat="server">
                           Cargar precios 1 rango
                            </asp:LinkButton>

                            <asp:LinkButton ID="btn_CargarPrecios5Rangos"  OnClick="btn_CargarPrecios5Rangos_Click"
                                OnClientClick="return confirm('!! Confirma la acción de cargar producto(s) en la tabla de precios rangos (5)¡¡ '); " 
                                class="dropdown-item" runat="server">
                            Cargar precios con 5 rangos
                            </asp:LinkButton>

                        </li>
                        <li>
                            <asp:LinkButton ID="btn_eliminarPreciosRangos"  OnClientClick="return confirm('!! Confirma la acción de eliminar producto(s) en la tabla de precios  rangos (5)¡¡ '); "
                                OnClick="btn_eliminarPreciosRangos_Click" class="dropdown-item" runat="server">
                       Eliminar producto(s) de rangos
                            </asp:LinkButton>

                            <asp:LinkButton ID="btn_eliminarPreciosTodo" OnClientClick="return confirm('!! Confirma la acción de eliminar producto(s) indicados de las tablas referentes a precios¡¡ '); "
                                OnClick="btn_eliminarPreciosTodo_Click" class="dropdown-item" runat="server">
                       Eliminar producto(s) en: rangos, fija, fantasma,cotizalo)
                            </asp:LinkButton>

                        </li>


                    </ul>
                </div>



                <div class="btn-group">
                    <button class="btn btn-outline-primary dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                        Precios Fijos y/o especiales
                    </button>
                    <ul class="dropdown-menu" aria-labelledby="dd_Precios">


                        <asp:LinkButton ID="btn_eliminarPreciosListaFija" OnClientClick="return confirm('!! Confirma la acción de eliminar producto(s) en la tabla de precios lista fija¡¡ '); "
                            OnClick="btn_eliminarPreciosListaFija_Click" class="dropdown-item" runat="server">
                       Eliminar producto(s) de lista fija
                        </asp:LinkButton>


                    </ul>
                </div>






                <div class="btn-group">
                    <button class="btn btn-outline-primary dropdown-toggle" type="button" id="dd_Fantasma" data-bs-toggle="dropdown" aria-expanded="false">
                        Fantasma
                    </button>
                    <ul class="dropdown-menu" aria-labelledby="dd_Fantasma">
                        <li>
                            <asp:LinkButton ID="btn_ObtenerListadoPreciosFantasma" OnClientClick="return confirm('!! Descargar xlsx de registros en la tabla Fantasma ¡¡ '); "
                                OnClick="btn_ObtenerListadoPreciosFantasma_Click" class="dropdown-item" runat="server">
                         Descargar registros (xlsx)
                            </asp:LinkButton>

                        </li>
                        <li>
                            <asp:LinkButton ID="btn_CargarPreciosFantasma" OnClientClick="return confirm('!! Cargar producto(s) en la lista fantasma ¡¡ '); "
                                OnClick="btn_CargarPreciosFantasma_Click" 
                                class="dropdown-item" runat="server">
                       Cargar Fantasma
                            </asp:LinkButton>

                        </li>
                        <li>
                            <asp:LinkButton ID="btn_EliminarPreciosFantasma_Click" OnClientClick="return confirm('!! Eliminar producto(s) de la lista fantasma ¡¡ '); "
                                OnClick="btn_EliminarPreciosFantasma_Click_Click" class="dropdown-item" runat="server">
                       Eliminar Fantasma
                            </asp:LinkButton></li>

                    </ul>
                </div>
                <div class="btn-group">
                    <button class="btn btn-outline-primary dropdown-toggle" type="button" id="dd_Cotizalo" data-bs-toggle="dropdown" aria-expanded="false">
                        Cotizalo
                    </button>
                    <ul class="dropdown-menu" aria-labelledby="dd_Cotizalo">
                        <li>
                            <asp:LinkButton ID="btn_DescargarProductosCotizalo"  OnClientClick="return confirm('!! Descargar xlsx de productos en Cotizalo ¡¡ '); "
                                OnClick="btn_DescargarProductosCotizalo_Click" class="dropdown-item" runat="server">
                       Descargar registros (xlsx)
                            </asp:LinkButton>

                        </li>
                        <li>
                            <asp:LinkButton ID="btn_CargarProductosCotizalo" OnClientClick="return confirm('!! Confirma la acción de cargar producto(s) a Cotizalo ¡¡ '); "
                                OnClick="btn_CargarProductosCotizalo_Click" class="dropdown-item" runat="server">
                       Cargar 
                            </asp:LinkButton>

                        </li>
                        <li>
                            <asp:LinkButton ID="btn_EliminarProductosCotizalo" OnClientClick="return confirm('!! Confirma la acción de eliminar producto(s) a Cotizalo ¡¡ '); "
                                 OnClick="btn_EliminarProductosCotizalo_Click" class="dropdown-item" runat="server">
                       Eliminar 
                            </asp:LinkButton></li>
                        <li>
                            <asp:LinkButton ID="btn_EliminarTodosProductosCotizalo" OnClientClick="return confirm('!! Confirma la acción de ELIMINAR TODOS LOS producto(s) de Cotizalo ¡¡ '); "
                                OnClick="btn_EliminarTodosProductosCotizalo_Click" class="dropdown-item red" runat="server">
                       Eliminar todos
                            </asp:LinkButton></li>

                    </ul>
                </div>


            </div>
        </div>

        <div class="row mt-4">
            <div class="col-12  ">
                <h2>Log resultado operación</h2>

                <asp:TextBox ID="txt_log_result" onfocus="Resize(this);" ClientIDMode="Static" Style="height: 200px;" class="form-control" TextMode="MultiLine" runat="server">

                </asp:TextBox>
                <br />
                <br />
                <br />
                <br />
                <br />
            </div>

        </div>

    </div>
    <script>
        function ClearTxt() {
            var txt_ListadoProductosActualizarPrecio = document.querySelector("#txt_ListadoProductosActualizarPrecio");
            var txt_log_result = document.querySelector("#txt_log_result");

            txt_log_result.value = "";
            txt_ListadoProductosActualizarPrecio.value = "";
            document.forms[0].submit();
        

        }
        function Resize(t) {
            const textarea = t;

            textarea.style.height = "1px";
            textarea.style.height = (25 + textarea.scrollHeight) + "px";
            console.log("test");
        }
        

        function MostrarPlantillasCarga() {
            let ddl_Plantillas = document.querySelector("#ddl_Plantillas");
            let txtPlantilla = document.querySelector("#FormatoCarga");

            let value = ddl_Plantillas.options[ddl_Plantillas.selectedIndex].value;
            switch (value) {

                case "precios1":
                    txtPlantilla.innerText = "Numero de Parte | Moneda | Precio";
                    break;
                case "precios5":
                    txtPlantilla.innerText = "Numero de Parte | Moneda | precio1 | max1 | Precio 2 | max2 | Precio 3 | max3 | Precio 4 | max4 | Precio 5 | max5";
                    break;
                case "cotizalo":
                    txtPlantilla.innerText = "Numero de Parte |";
                    break;
                case "listaFija":
                    txtPlantilla.innerText = "id_clientePag  | Numero de Parte | Moneda | Precio";
                    break;

                case "fantasma":
                    txtPlantilla.innerText = "Numero de Parte | Precio";
                    break;
                case "eliminar":
                    txtPlantilla.innerText = "Numero de Parte | ";
                    break;
                case "informe":
                    txtPlantilla.innerText = "Numero de Parte | ";
                    break;
                    

                default:
                    txtPlantilla.innerText = "Selecciona una opción para mostrar la plantilla";
                    break;
            }


        }
    </script>
    <script>

        var txt_Columns = document.querySelector("#txt_Columns");
        var ContentButtonsColumms = document.querySelector("#ContentButtonsColumms");
        var Select = document.querySelector("#ddl_ColumnList");

        function removeColum(button) {

            let ValueColumn = button.getAttribute("ValueColumn");


            let str_txt_Columns = txt_Columns.value;
            str_txt_Columns = str_txt_Columns.replace(ValueColumn + ",", '');
            txt_Columns.value = str_txt_Columns;
            button.remove();

            const option = document.createElement("option");
            option.innerText = button.innerText;
            option.setAttribute('Value', ValueColumn)
            Select.appendChild(option);

        }
        function AgregarColumn() {


            var TextColumn = Select.options[Select.selectedIndex].text;
            var ValueColumn = Select.options[Select.selectedIndex].value;

            if (ValueColumn === "") { return; }
            let str_txt_Columns = txt_Columns.value;
            txt_Columns.value = str_txt_Columns + ValueColumn + ",";


            // Creando el button
            const button = document.createElement("a");
            button.innerText = TextColumn;
            button.setAttribute('ValueColumn', ValueColumn)
            button.classList.add("btn", "btn-outline-primary");
            button.id = "btn_" + ValueColumn;
            button.setAttribute("onclick", "removeColum(this);");
            ContentButtonsColumms.appendChild(button);
            Select.remove(Select.selectedIndex);

        }
    </script>
</asp:Content>




using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de preciosTienda
/// </summary>
public class preciosTienda 
{
    private SqlConnection con { get; set; }
    private SqlCommand cmd { get; set; }
    private SqlDataAdapter da { get; set; }
    private DataSet ds { get; set; }
    private DataTable dt { get; set; }

    /// <summary>
    /// Se pasa el parametro de la moneda en la tienda o moneda de la operación trabajada
    /// </summary>
    /// 
    public string monedaTienda { get; set; }

    protected void dbConexion()
    {

        con = new SqlConnection(conexiones.conexionTienda());
        cmd = new SqlCommand();
        cmd.Connection = con;

    }

    public DataTable obtener_multiplicadores()
    {

        dbConexion();
        using (con) {
           
            string query = @"SET LANGUAGE English; SELECT * FROM precios_multiplicador ";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            con.Open();
            da.Fill(ds);
            return ds.Tables[0];
        }
    }
    public List<model_precios_Multiplicador> multiplicadoresToList(DataTable dt) {

     


        if (dt.Rows.Count >= 1)
        {
            List<model_precios_Multiplicador> precios_Multiplicador = new List<model_precios_Multiplicador>();

            foreach (DataRow r in dt.Rows)
            {

                precios_Multiplicador.Add(new model_precios_Multiplicador() {
                    id = int.Parse(r["id"].ToString()),
                    nivel = int.Parse(r["nivel"].ToString()),
                    nombre_multiplicador = r["nombre_multiplicador"].ToString(),

                });
            }

            return precios_Multiplicador;
        } else return null;
        
    }

    /// <summary>
    /// Recibe DT de productos y calcula precios, tablas de precios de acuerdo al cliente / modalidad y moneda en visualización
    /// </summary>
    /// 
    public DataTable procesarProductos(DataTable dt_productos)
    {
        decimal tipoCambio = operacionesConfiguraciones.obtenerTipoDeCambio();
       //string monedaTienda = (string)HttpContext.Current.Session["monedaTienda"];

        //Obtenemos los multiplicadores de productos disponibles para calcular los costos cuando este no sea fijo
        DataTable multiplicadoresTienda = obtener_multiplicadores();

        

        // Obtenemos los datos del usuario iniciado
        usuarios usuario = usuarios.modoAsesor();

        dt_productos.Columns.Add("min1", typeof(decimal));
        dt_productos.Columns.Add("min2", typeof(decimal));
        dt_productos.Columns.Add("min3", typeof(decimal));
        dt_productos.Columns.Add("min4", typeof(decimal));
        dt_productos.Columns.Add("min5", typeof(decimal));

        // Comenzamos a recorrer cada producto
        foreach (DataRow r in dt_productos.Rows)
        {
            // Validamos que el producto solo sea pra visualización y no para operaciones evitando el procesado de precio
            if(dt_productos.Columns.Contains("solo_para_Visualizar")) { 
                string str_solo_para_Visualizar = r["solo_para_Visualizar"].ToString();

                // Si no se ha definido esta característica: se establece en [false], si se ha definido, toma su valor establecido [true/false].
                bool solo_para_Visualizar = string.IsNullOrEmpty(str_solo_para_Visualizar) ? false : bool.Parse(str_solo_para_Visualizar);

                if (solo_para_Visualizar)
                {
                    continue;
                }
            }


            // Obtenemos el id del cliente del producto
            string id_cliente = r["id_cliente"].ToString();

            // VAR precio = precio de la lista fija;
            decimal precio = decimal.MinusOne;

            // Si el ID del cliente del producto (precio fijo) es igual al id SAP con el que se esta trabajando agregamos el precio fijo
            if (usuario.idSAP != "" && id_cliente != "" && usuario.idSAP == id_cliente) {

                string moneda_fija = r["moneda_fija"].ToString();
                         


                if (r["precio"] != DBNull.Value)
                {
                    precio = decimal.Parse(r["precio"].ToString());
                }

                precio = precio_a_MonedaTienda(moneda_fija, precio);

                r["precio"] = precio;

                // Como se convirtio a la moneda de la tienda, se cambia el tipo de moneda en la columa "moneda_fija"
                r["moneda_fija"] = monedaTienda;
            } else // Si es falso, procesamos los rangos de precios
            {

                r["precio"] = DBNull.Value;
                // INICIO proceso de precios de rangos

                // Cada vez que encuentre un rango va a sumar uno para tener el contador
                int no_rangos = 0;
                string moneda_rangos = r["moneda_rangos"].ToString().ToUpper();

                decimal precio1 = 0;
                decimal min1 = 0;
                decimal max1 = 0;


                if (r["precio1"] != DBNull.Value)
                {
                    precio1 = decimal.Parse(r["precio1"].ToString()); no_rangos = 1;


                    min1 = 0;
                    r["min1"] = min1;
                    max1 = decimal.Parse(r["max1"].ToString());


                };

                // Si no encontró precio se elimina el producto y pasa al siguiente producto.
                if (precio <= 0 && precio1 <= 0)
                {

                    r.Delete();
                    
                    continue;
                }


                decimal precio2 = 0;
                decimal min2 = 0;
                decimal max2 = 0;
                if (r["precio2"] != DBNull.Value)
                {
                    precio2 = decimal.Parse(r["precio2"].ToString()); no_rangos = 2;
                   
                    min2 = max1+1;
                    r["min2"] = min2;
                    max2 = decimal.Parse(r["max2"].ToString());

                };

                decimal precio3 = 0;
                decimal min3 = 0;
                decimal max3 = 0;
                if (r["precio3"] != DBNull.Value)
                {
                    precio3 = decimal.Parse(r["precio3"].ToString()); no_rangos = 3;


                    min3 = max2 + 1;
                    r["min3"] = min3;
                    max3 = decimal.Parse(r["max3"].ToString());

                };

                decimal precio4 = 0;
                decimal min4 = 0;
                decimal max4 = 0;
                if (r["precio4"] != DBNull.Value)
                {
                    precio4 = decimal.Parse(r["precio4"].ToString()); no_rangos = 4;

                    min4 = max3 + 1;
                    r["min4"] = min4;
                    max4 = decimal.Parse(r["max4"].ToString());
                };

                decimal precio5 = 0;
                decimal min5 = 0;
                decimal max5 = 0;
                if (r["precio5"] != DBNull.Value)
                {
                    precio5 = decimal.Parse(r["precio5"].ToString()); no_rangos = 5;

                    min5 = max4 + 1;
                    r["min5"] = min5;
                    max5 = decimal.Parse(r["max5"].ToString());
                };
                // FIN proceso de precios de rangos



                /* INICIO del proceso de obtener los multiplicadores disponibles relacion: rol usuario, rol aceptados por el producto
                y el número a multiplicar de la tabla multiplicadores 
                */




                string usuarioMultiplicador = usuario.rol_precios_multiplicador;



                /* Variable de los roles aceptados por el producto. Es necesario remplazar espacios para hacer un split y comparacion correcta
                 ya que este dato se obtiene de la db y a su vez
                 esta es creada por una persona y cargada directamente a la db sin validad el formato 
             */
                string[] rol_preciosMultiplicador = r["rol_preciosMultiplicador"].ToString().Replace(" ", "").Split(',');

                /* Esta variable nos sirve para validar que procesos debe seguir o no seguir para calcular el precio */
                bool encontroMultiplicador = false;

                /* Si de los roles admitidos por el producto solo es general, lo establecemos en true*/
                bool soloMultiplicadorGeneral = false;

                // Si no tiene ningún rol definido, no podemos dar precio, por lo tanto el producto no estará disponible
                if (rol_preciosMultiplicador == null || rol_preciosMultiplicador.Length  == 0 || rol_preciosMultiplicador[0] == "")
                {
                    r.Delete();
                   
                    continue;
                }
                // Validamos que el producto solo admita rol general para evitar encontrar el cálculo más adecuado
                if (rol_preciosMultiplicador.Length == 1 && rol_preciosMultiplicador[0] == "general" ) {
                    soloMultiplicadorGeneral = true;
                }


                if (!soloMultiplicadorGeneral) { 
                foreach (string rol in rol_preciosMultiplicador)
                {
                    if (usuarioMultiplicador.Contains(rol)) {
                        // Si encontro el rol del usuario en los roles admitidos del producto, procede a aplicar dichos descuentos

                        string filtro = "[nombre_multiplicador] = '" + rol + "'";

                        DataRow[] drMultiplicador = multiplicadoresTienda.Select(filtro);

                        decimal multiplicador_Valor = decimal.Parse(drMultiplicador[0]["multiplicador_valor"].ToString().Replace(" ", ""));

                        precio1 = precio1 * multiplicador_Valor;
                        precio2 = precio2 * multiplicador_Valor;
                        precio3 = precio3 * multiplicador_Valor;
                        precio4 = precio4 * multiplicador_Valor;
                        precio5 = precio5 * multiplicador_Valor;

                        /* Como encontró y calculó el precio, establecemos en true para que no prosiga 
                           en los otros procesos de obtención del multiplicador adecuado. */
                        encontroMultiplicador = true;

                    }
                }


                }
              
                    // Si no encontro un multiplicador exacto (a = b), procedemos a encontrar el precio más bajo que no sea el General
                    if (encontroMultiplicador == false && soloMultiplicadorGeneral == false)
                    {
                        // Obtenemos el nivel del multiplicador que tenga el usuario
                        string filtro_Nivel_MultiplicadorUsuario = "[nombre_multiplicador] = '" + usuarioMultiplicador + "'";
                        DataRow[] drMultiplicadorUsuario = multiplicadoresTienda.Select(filtro_Nivel_MultiplicadorUsuario);
                        int usuarioMultiplicadorNivel = int.Parse(drMultiplicadorUsuario[0]["nivel"].ToString());
                        // *********


                        /* Si el nivel del multiplicador del usuario es mayor que 1 (1 es el general),
                          por lo tanto puede tener un mejor precio que el general */

                        while (usuarioMultiplicadorNivel > 1 && encontroMultiplicador == false)
                        {
                            //  Restamos 1 en cada ciclo
                            usuarioMultiplicadorNivel = usuarioMultiplicadorNivel - 1;

                            // Buscamos un multiplicador de un nivel inferior
                            string filtro = "[nivel] = " + usuarioMultiplicadorNivel.ToString() + "";

                            DataRow[] drMultiplicador = multiplicadoresTienda.Select(filtro);

                            //Si encontró algo, procedemos.
                            if (drMultiplicador != null && drMultiplicador.Count() >= 1)
                            {

                            string nombreMultiplicadorMenor = drMultiplicador[0]["nombre_multiplicador"].ToString().Replace(" ", "");

                                // Validamos que el multiplicador obtenido sea admitido por dicho producto, si no proseguimos con el ciclo.
                            foreach (string rol in rol_preciosMultiplicador)
                                {
                                    if (nombreMultiplicadorMenor.Contains(rol))
                                    {
                                    decimal multiplicador_Valor = decimal.Parse(drMultiplicador[0]["multiplicador_valor"].ToString().Replace(" ", ""));

                                        precio1 = precio1 * multiplicador_Valor;
                                        precio2 = precio2 * multiplicador_Valor;
                                        precio3 = precio3 * multiplicador_Valor;
                                        precio4 = precio4 * multiplicador_Valor;
                                        precio5 = precio5 * multiplicador_Valor;
                                        encontroMultiplicador = true;

                                    }
                                }
                            }
                        }
                    }

                


                // Si llega hasta este punto, quiere decir que tendremos que usar el general
               if(soloMultiplicadorGeneral == true || encontroMultiplicador == false)
                {
                    string filtro_Nivel_MultiplicadorGeneral = "[nombre_multiplicador] = 'general'";
                    DataRow[] drMultiplicadorGeneral = multiplicadoresTienda.Select(filtro_Nivel_MultiplicadorGeneral);

                    decimal multiplicador_Valor = decimal.Parse(drMultiplicadorGeneral[0]["multiplicador_valor"].ToString().Replace(" ", ""));

                    precio1 = Math.Round(precio1 * multiplicador_Valor,2);
                    precio2 = Math.Round(precio2 * multiplicador_Valor, 2);
                    precio3 = Math.Round(precio3 * multiplicador_Valor, 2);
                    precio4 = Math.Round(precio4 * multiplicador_Valor, 2);
                    precio5 = Math.Round(precio5 * multiplicador_Valor, 2);
                }


                r["precio1"] = precio_a_MonedaTienda(moneda_rangos, precio1);




                // Iniciamos actualización de los precios
                if (r["precio1"] != DBNull.Value) { r["precio1"] = precio_a_MonedaTienda(moneda_rangos, precio1); r["moneda_rangos"] = monedaTienda; };
                if (r["precio2"] != DBNull.Value) r["precio2"] = precio_a_MonedaTienda(moneda_rangos, precio2);
                if (r["precio3"] != DBNull.Value) r["precio3"] = precio_a_MonedaTienda(moneda_rangos, precio3);
                if (r["precio4"] != DBNull.Value) r["precio4"] = precio_a_MonedaTienda(moneda_rangos, precio4);
                if (r["precio5"] != DBNull.Value) r["precio5"] = precio_a_MonedaTienda(moneda_rangos, precio5);


                #region Precios fantasma
                DataColumnCollection columns = dt_productos.Columns;
                if (columns.Contains("preciosFantasma"))
                {
                    if (r["preciosFantasma"] != DBNull.Value)
                    {
                        decimal preciosFantasma = decimal.Parse(r["preciosFantasma"].ToString());

                        r["preciosFantasma"] = precio_a_MonedaTienda(moneda_rangos, preciosFantasma);
                    }



                }
                #endregion

            }


        }

      
           
        dt_productos.AcceptChanges();
        return dt_productos;
    }

    /// <summary>
    /// PROCESA LOS PRECIOS COMO USUARIO retail@incom.mx (general)
    /// Recibe DT de productos y calcula precios, tablas de precios de acuerdo al cliente / modalidad y moneda en visualización
    /// </summary>
    ///

    public DataTable procesarProductosGeneral(DataTable dt_productos)
    {
        decimal tipoCambio = operacionesConfiguraciones.obtenerTipoDeCambio();
        //string monedaTienda = (string)HttpContext.Current.Session["monedaTienda"];

        //Obtenemos los multiplicadores de productos disponibles para calcular los costos cuando este no sea fijo
        DataTable multiplicadoresTienda = obtener_multiplicadores();



        // Obtenemos los datos del usuario iniciado
        usuarios usuario = usuarios.recuperar_DatosUsuario("retail@incom.mx");

        dt_productos.Columns.Add("min1", typeof(decimal));
        dt_productos.Columns.Add("min2", typeof(decimal));
        dt_productos.Columns.Add("min3", typeof(decimal));
        dt_productos.Columns.Add("min4", typeof(decimal));
        dt_productos.Columns.Add("min5", typeof(decimal));

        // Comenzamos a recorrer cada producto
        foreach (DataRow r in dt_productos.Rows)
        {
            // Obtenemos el id del cliente del producto
            string id_cliente = r["id_cliente"].ToString();

            // VAR precio = precio de la lista fija;
            decimal precio = decimal.MinusOne;

            // Si el ID del cliente del producto (precio fijo) es igual al id SAP con el que se esta trabajando agregamos el precio fijo
            if (usuario.idSAP != "" && id_cliente != "" && usuario.idSAP == id_cliente)
            {

                string moneda_fija = r["moneda_fija"].ToString();



                if (r["precio"] != DBNull.Value)
                {
                    precio = decimal.Parse(r["precio"].ToString());
                }

                precio = precio_a_MonedaTienda(moneda_fija, precio);

                r["precio"] = precio;

                // Como se convirtio a la moneda de la tienda, se cambia el tipo de moneda en la columa "moneda_fija"
                r["moneda_fija"] = monedaTienda;
            }
            else // Si es falso, procesamos los rangos de precios
            {

                r["precio"] = DBNull.Value;
                // INICIO proceso de precios de rangos

                // Cada vez que encuentre un rango va a sumar uno para tener el contador
                int no_rangos = 0;
                string moneda_rangos = r["moneda_rangos"].ToString().ToUpper();

                decimal precio1 = 0;
                decimal min1 = 0;
                decimal max1 = 0;


                if (r["precio1"] != DBNull.Value)
                {
                    precio1 = Math.Round( decimal.Parse(r["precio1"].ToString()),2); no_rangos = 1;


                    min1 = 0;
                    r["min1"] = min1;
                    max1 = decimal.Parse(r["max1"].ToString());


                };

                // Si no encontró precio se elimina el producto y pasa al siguiente producto.
                if (precio <= 0 && precio1 <= 0)
                {

                    r.Delete();

                    continue;
                }


                decimal precio2 = 0;
                decimal min2 = 0;
                decimal max2 = 0;
                if (r["precio2"] != DBNull.Value)
                {
                    precio2 = Math.Round(decimal.Parse(r["precio2"].ToString()),2); no_rangos = 2;

                    min2 = max1 + 1;
                    r["min2"] = min2;
                    max2 = decimal.Parse(r["max2"].ToString());

                };

                decimal precio3 = 0;
                decimal min3 = 0;
                decimal max3 = 0;
                if (r["precio3"] != DBNull.Value)
                {
                    precio3 = Math.Round(decimal.Parse(r["precio3"].ToString()),2); no_rangos = 3;


                    min3 = max2 + 1;
                    r["min3"] = min3;
                    max3 = decimal.Parse(r["max3"].ToString());

                };

                decimal precio4 = 0;
                decimal min4 = 0;
                decimal max4 = 0;
                if (r["precio4"] != DBNull.Value)
                {
                    precio4 = Math.Round(decimal.Parse(r["precio4"].ToString()),2); no_rangos = 4;

                    min4 = max3 + 1;
                    r["min4"] = min4;
                    max4 = decimal.Parse(r["max4"].ToString());
                };

                decimal precio5 = 0;
                decimal min5 = 0;
                decimal max5 = 0;
                if (r["precio5"] != DBNull.Value)
                {
                    precio5 = Math.Round(decimal.Parse(r["precio5"].ToString()),2); no_rangos = 5;

                    min5 = max4 + 1;
                    r["min5"] = min5;
                    max5 = decimal.Parse(r["max5"].ToString());
                };
                // FIN proceso de precios de rangos



                /* INICIO del proceso de obtener los multiplicadores disponibles relacion: rol usuario, rol aceptados por el producto
                y el número a multiplicar de la tabla multiplicadores 
                */




                string usuarioMultiplicador = usuario.rol_precios_multiplicador;



                /* Variable de los roles aceptados por el producto. Es necesario remplazar espacios para hacer un split y comparacion correcta
                 ya que este dato se obtiene de la db y a su vez
                 esta es creada por una persona y cargada directamente a la db sin validad el formato 
             */
                string[] rol_preciosMultiplicador = r["rol_preciosMultiplicador"].ToString().Replace(" ", "").Split(',');

                /* Esta variable nos sirve para validar que procesos debe seguir o no seguir para calcular el precio */
                bool encontroMultiplicador = false;

                /* Si de los roles admitidos por el producto solo es general, lo establecemos en true*/
                bool soloMultiplicadorGeneral = false;

                // Si no tiene ningún rol definido, no podemos dar precio, por lo tanto el producto no estará disponible
                if (rol_preciosMultiplicador == null || rol_preciosMultiplicador.Length == 0 || rol_preciosMultiplicador[0] == "")
                {
                    r.Delete();

                    continue;
                }
                // Validamos que el producto solo admita rol general para evitar encontrar el cálculo más adecuado
                if (rol_preciosMultiplicador.Length == 1 && rol_preciosMultiplicador[0] == "general")
                {
                    soloMultiplicadorGeneral = true;
                }


                if (!soloMultiplicadorGeneral)
                {
                    foreach (string rol in rol_preciosMultiplicador)
                    {
                        if (usuarioMultiplicador.Contains(rol))
                        {
                            // Si encontro el rol del usuario en los roles admitidos del producto, procede a aplicar dichos descuentos

                            string filtro = "[nombre_multiplicador] = '" + rol + "'";

                            DataRow[] drMultiplicador = multiplicadoresTienda.Select(filtro);

                            decimal multiplicador_Valor = decimal.Parse(drMultiplicador[0]["multiplicador_valor"].ToString().Replace(" ", ""));

                            precio1 = precio1 * multiplicador_Valor;
                            precio2 = precio2 * multiplicador_Valor;
                            precio3 = precio3 * multiplicador_Valor;
                            precio4 = precio4 * multiplicador_Valor;
                            precio5 = precio5 * multiplicador_Valor;

                            /* Como encontró y calculó el precio, establecemos en true para que no prosiga 
                               en los otros procesos de obtención del multiplicador adecuado. */
                            encontroMultiplicador = true;

                        }
                    }


                }

                // Si no encontro un multiplicador exacto (a = b), procedemos a encontrar el precio más bajo que no sea el General
                if (encontroMultiplicador == false && soloMultiplicadorGeneral == false)
                {
                    // Obtenemos el nivel del multiplicador que tenga el usuario
                    string filtro_Nivel_MultiplicadorUsuario = "[nombre_multiplicador] = '" + usuarioMultiplicador + "'";
                    DataRow[] drMultiplicadorUsuario = multiplicadoresTienda.Select(filtro_Nivel_MultiplicadorUsuario);
                    int usuarioMultiplicadorNivel = int.Parse(drMultiplicadorUsuario[0]["nivel"].ToString());
                    // *********


                    /* Si el nivel del multiplicador del usuario es mayor que 1 (1 es el general),
                      por lo tanto puede tener un mejor precio que el general */

                    while (usuarioMultiplicadorNivel > 1 && encontroMultiplicador == false)
                    {
                        //  Restamos 1 en cada ciclo
                        usuarioMultiplicadorNivel = usuarioMultiplicadorNivel - 1;

                        // Buscamos un multiplicador de un nivel inferior
                        string filtro = "[nivel] = " + usuarioMultiplicadorNivel.ToString() + "";

                        DataRow[] drMultiplicador = multiplicadoresTienda.Select(filtro);

                        //Si encontró algo, procedemos.
                        if (drMultiplicador != null && drMultiplicador.Count() >= 1)
                        {

                            string nombreMultiplicadorMenor = drMultiplicador[0]["nombre_multiplicador"].ToString().Replace(" ", "");

                            // Validamos que el multiplicador obtenido sea admitido por dicho producto, si no proseguimos con el ciclo.
                            foreach (string rol in rol_preciosMultiplicador)
                            {
                                if (nombreMultiplicadorMenor.Contains(rol))
                                {
                                    decimal multiplicador_Valor = decimal.Parse(drMultiplicador[0]["multiplicador_valor"].ToString().Replace(" ", ""));

                                    precio1 = precio1 * multiplicador_Valor;
                                    precio2 = precio2 * multiplicador_Valor;
                                    precio3 = precio3 * multiplicador_Valor;
                                    precio4 = precio4 * multiplicador_Valor;
                                    precio5 = precio5 * multiplicador_Valor;
                                    encontroMultiplicador = true;

                                }
                            }
                        }
                    }
                }




                // Si llega hasta este punto, quiere decir que tendremos que usar el general
                if (soloMultiplicadorGeneral == true || encontroMultiplicador == false)
                {
                    string filtro_Nivel_MultiplicadorGeneral = "[nombre_multiplicador] = 'general'";
                    DataRow[] drMultiplicadorGeneral = multiplicadoresTienda.Select(filtro_Nivel_MultiplicadorGeneral);

                    decimal multiplicador_Valor = decimal.Parse(drMultiplicadorGeneral[0]["multiplicador_valor"].ToString().Replace(" ", ""));

                    precio1 = precio1 * multiplicador_Valor;
                    precio2 = precio2 * multiplicador_Valor;
                    precio3 = precio3 * multiplicador_Valor;
                    precio4 = precio4 * multiplicador_Valor;
                    precio5 = precio5 * multiplicador_Valor;
                }


                r["precio1"] = precio_a_MonedaTienda(moneda_rangos, precio1);

                // Iniciamos actualización de los precios
                if (r["precio1"] != DBNull.Value) { r["precio1"] = precio_a_MonedaTienda(moneda_rangos, precio1); r["moneda_rangos"] = monedaTienda; };
                if (r["precio2"] != DBNull.Value) r["precio2"] = precio_a_MonedaTienda(moneda_rangos, precio2);
                if (r["precio3"] != DBNull.Value) r["precio3"] = precio_a_MonedaTienda(moneda_rangos, precio3);
                if (r["precio4"] != DBNull.Value) r["precio4"] = precio_a_MonedaTienda(moneda_rangos, precio4);
                if (r["precio5"] != DBNull.Value) r["precio5"] = precio_a_MonedaTienda(moneda_rangos, precio5);
            }

        }
        dt_productos.AcceptChanges();
        return dt_productos;
    }
    /// <summary>
    /// Recibe un precio con su moneda y devuelve el precio a la moneda establecida en la selección de la tienda.
    /// </summary>
    public decimal precio_a_MonedaTienda(string moneda, decimal precio)
    {

        decimal tipoCambio = operacionesConfiguraciones.obtenerTipoDeCambio();


        //Si la moneda de trabajo de la tienda es USD y el producto en moneda del precio de lista fija es a MXN hacemos el cálculo a MXN
        if (monedaTienda == "USD" && moneda == "MXN")
        {

            precio = Math.Round(precio / tipoCambio,2);

        } else if (monedaTienda == "MXN" && moneda == "USD")
        {

            precio = Math.Round(precio * tipoCambio,2);
        }

        return precio;
    }
    /// <summary>
    /// Recibe un precio con su moneda y devuelve el precio a la moneda establecida en la selección de la tienda.
    /// </summary>
    public decimal precio_a_MonedaOperacion(string monedaProducto, string monedaOperacion, decimal precio) {

        decimal tipoCambio = operacionesConfiguraciones.obtenerTipoDeCambio();


        //Si la moneda de trabajo de la tienda es USD y el producto en moneda del precio de lista fija es a MXN hacemos el cálculo a MXN
        if (monedaOperacion == "USD" && monedaProducto == "MXN") {

            precio = Math.Round(precio / tipoCambio,2);

            } else if (monedaOperacion == "MXN" && monedaProducto == "USD") {

            precio = Math.Round(precio * tipoCambio,2);
            }

        return precio;
        }
    /// <summary>
    /// Retorna el precio de lista de un producto a la moneda de la tienda
    /// </summary>
    /// 
    public static DataTable obtenerProductoPrecioLista(string numero_parte) {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        string _precio = string.Empty;
        using (con) {

            string query = @"SET LANGUAGE English; 
                SELECT * FROM precios_ListaFija WHERE numero_parte =@numero_parte AND id_cliente = 0";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 50);
            cmd.Parameters["@numero_parte"].Value = numero_parte;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);


            con.Open();
            da.Fill(ds);
            DataTable dtProductos = ds.Tables[0];
            if(dtProductos != null && dtProductos.Rows.Count >= 1) {

                return dtProductos;
             

            }

        }
        return null;
    }
            /// <summary>
            /// Recibe un precio con su moneda, tipo de cambio y devuelve el precio a la moneda establecida en la selección de la tienda.
            /// </summary>
            /// 
    public decimal precio_a_MonedaTienda(decimal tipoCambio, string moneda, decimal precio) {

        //Si la moneda de trabajo de la tienda es USD y el producto en moneda del precio de lista fija es a MXN hacemos el cálculo a MXN
        if (monedaTienda == "USD" && moneda == "MXN")
        {

            precio = Math.Round(precio / tipoCambio,2);

        } else if (monedaTienda == "MXN" && moneda == "USD")
        {

            precio = Math.Round(precio * tipoCambio,2);
        }

        return precio;
    }
    /// <summary>
    /// Devuelve un precio de acuerdo a la cantidad, no hace cálculos de monedas ni multiplicadores
    /// </summary>
    static public decimal precioRango(List<preciosTabulador> lista_precios, decimal cantidad) {

        // Removemos los rangos vacios
        lista_precios.RemoveAll(x => x.min < 0);

        // Si quedó un solo rango mandamos el único precio disponible
        if (lista_precios.Count == 1)
        {
            return Math.Round(lista_precios[0].precio,2);

        }
        // Si la cantidad solicitada es mayor que el máximo disponible, envía el mejor precio (último de la tabla)
        else if(lista_precios.Count >= 2 && cantidad > lista_precios.OrderByDescending(item => item.max).First().max)
        {
            return Math.Round(lista_precios.OrderByDescending(item => item.max).First().precio,2);
        }

        // si llego hasta este punto envía el precio adecuado dependiendo el rango
        var precioFinal = lista_precios.Where(p => p.min <= cantidad && cantidad <= p.max).ToList();

        return Math.Round(precioFinal[0].precio,2);
    }

    static public decimal precioRango(List<preciosTabulador> lista_precios, decimal cantidad, string moneda)
    {
        preciosTienda procesar = new preciosTienda();

        lista_precios.RemoveAll(x => x.min < 0);

        // Si quedó un solo rango mandamos el único precio disponible
        if (lista_precios.Count == 1)
        {
            return Math.Round(lista_precios[0].precio,2);

        }
        // Si la cantidad solicitada es mayor que el máximo disponible, envía el mejor precio (último de la tabla)
        else if (lista_precios.Count >= 2 && cantidad > lista_precios.OrderByDescending(item => item.max).First().max)
        {
            return Math.Round(lista_precios.OrderByDescending(item => item.max).First().precio,2);
        }

        var precioFinal = lista_precios.Where(p => p.min <= cantidad && cantidad < p.max).ToList();

        return Math.Round(procesar.precio_a_MonedaTienda(moneda, decimal.Parse(precioFinal[0].ToString())),2);

    }

      public decimal? obtenerPrecioGeneralProducto(string numero_parte ) {

        productosTienda obtener = new productosTienda();
        DataTable dtProducto = obtener.obtenerProducto(numero_parte);

        dtProducto = procesarProductosGeneral(dtProducto);

        decimal? precioGeneral = null;
            if (dtProducto.Rows[0]["precio1"] != DBNull.Value)
        {
            precioGeneral = Math.Round(decimal.Parse(dtProducto.Rows[0]["precio1"].ToString()),2);
            return precioGeneral;
        }
        else return null;



    }
}
public class preciosTabulador {
    public decimal min { get; set; }
    public decimal max { get; set; }
    public decimal precio { get; set; }
}
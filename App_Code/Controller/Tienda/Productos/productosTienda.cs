using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
/// <summary>
/// Descripción breve de productosTienda
/// </summary>
public class productosTienda {
    private SqlConnection con { get; set; }
    private SqlCommand cmd { get; set; }
    private SqlDataAdapter da { get; set; }
    private DataSet ds { get; set; }
    private DataTable dt { get; set; }

    static private string queryObtenerProductoFullTextSearch = @"SELECT    producto.id, 
                producto.numero_parte, 
                producto.titulo, 
                producto.descripcion_corta,
                producto.titulo_corto_ingles,
                producto.especificaciones,
                producto.marca,
                producto.categoria_identificador,
                producto.imagenes,
                producto.metatags,
                producto.peso,
                producto.alto,
                producto.ancho,
                producto.profundidad,
                producto.pdf,
                producto.video,
                producto.unidad_venta,
                producto.cantidad,
                producto.unidad,
                producto.producto_alternativo,
                producto.productos_relacionados,
                producto.atributos,
                producto.noParte_proveedor,
                producto.noParte_Sap,
                producto.noParte_interno,
                producto.upc,
                producto.noParte_Competidor,
               (SELECT IIF(producto.orden IS NULL, 999, producto.orden)) as orden,
                producto.etiquetas,
                producto.disponibleVenta,
                producto.disponibleEnvio,
                producto.avisos,

                roles.rol_visibilidad,
                roles.rol_preciosMultiplicador,

                preciosFantasma.preciosFantasma,
                preciosFantasma.porcentajeFantasma,
                preciosFijos.id_cliente,
                preciosFijos.moneda_fija,
                preciosFijos.precio,
                
                preciosRangos.moneda_rangos,
                preciosRangos.precio1,
                preciosRangos.max1,


                preciosRangos.precio2,
                preciosRangos.max2,

                preciosRangos.precio3,
                preciosRangos.max3,

                preciosRangos.precio4,
                preciosRangos.max4,

                preciosRangos.precio5,
                preciosRangos.max5,
                visualizacion.solo_para_Visualizar
 
                FROM    productos_Datos as producto 
                FULL OUTER JOIN precios_ListaFija preciosFijos ON producto.numero_parte = preciosFijos.numero_parte
                FULL OUTER JOIN precios_rangos preciosRangos ON producto.numero_parte = preciosRangos.numero_parte  
                FULL OUTER JOIN productos_Roles roles ON producto.numero_parte = roles.numero_parte 
                FULL OUTER JOIN productos_solo_visualizacion visualizacion ON producto.numero_parte = visualizacion.numero_parte 
                FULL OUTER JOIN precios_fantasma preciosFantasma ON producto.numero_parte = preciosFantasma.numero_parte 
 ";

    static private string queryObtenerProducto =
                    @"SELECT DISTINCT
                producto.id, 
                producto.numero_parte, 
                producto.titulo, 
                producto.descripcion_corta,
                producto.titulo_corto_ingles,
                producto.especificaciones,
                producto.marca,
                producto.categoria_identificador,
                producto.imagenes,
                producto.metatags,
                producto.peso,
                producto.alto,
                producto.ancho,
                producto.profundidad,
                producto.pdf,
                producto.video,

                producto.unidad_venta,

                producto.cantidad,

                producto.unidad,
                producto.producto_alternativo,
                producto.productos_relacionados,
                producto.atributos,

                producto.noParte_proveedor,

                producto.noParte_Sap,
                producto.noParte_interno,
                producto.upc,
                producto.noParte_Competidor,
               (SELECT IIF(producto.orden IS NULL, 999, producto.orden)) as orden,
                producto.etiquetas,
                producto.disponibleVenta,
                producto.disponibleEnvio,
                producto.avisos,

                roles.rol_visibilidad,
                roles.rol_preciosMultiplicador,

                preciosFijos.id_cliente,
                preciosFijos.moneda_fija,
                preciosFijos.precio,
                

   preciosFantasma.preciosFantasma,
   preciosFantasma.porcentajeFantasma,

                preciosRangos.moneda_rangos,
                preciosRangos.precio1,
                preciosRangos.max1,


                preciosRangos.precio2,
                preciosRangos.max2,

                preciosRangos.precio3,
                preciosRangos.max3,

                preciosRangos.precio4,
                preciosRangos.max4,

                preciosRangos.precio5,
                preciosRangos.max5,
                visualizacion.solo_para_Visualizar

                FROM productos_Datos as producto 
                FULL OUTER JOIN precios_ListaFija preciosFijos ON producto.numero_parte = preciosFijos.numero_parte
                FULL OUTER JOIN precios_rangos preciosRangos ON producto.numero_parte = preciosRangos.numero_parte  
                FULL OUTER JOIN productos_Roles roles ON producto.numero_parte = roles.numero_parte 
                FULL OUTER JOIN productos_solo_visualizacion visualizacion ON producto.numero_parte = visualizacion.numero_parte 
   FULL OUTER JOIN precios_fantasma preciosFantasma ON producto.numero_parte = preciosFantasma.numero_parte 
WHERE producto.numero_parte  = @numero_parte 
                ";
    string queryObtenerProductoPrecios =
                    @"SELECT DISTINCT
                producto.id, 
                producto.numero_parte, 
            
                producto.disponibleVenta,

                roles.rol_visibilidad,
                roles.rol_preciosMultiplicador,

                preciosFijos.id_cliente,
                preciosFijos.moneda_fija,
                preciosFijos.precio,
                
                preciosRangos.moneda_rangos,
                preciosRangos.precio1,
                preciosRangos.max1,


                preciosRangos.precio2,
                preciosRangos.max2,

                preciosRangos.precio3,
                preciosRangos.max3,

                preciosRangos.precio4,
                preciosRangos.max4,

                preciosRangos.precio5,
                preciosRangos.max5,

                visualizacion.solo_para_Visualizar 

                FROM productos_Datos as producto 
                FULL OUTER JOIN precios_ListaFija preciosFijos ON producto.numero_parte = preciosFijos.numero_parte
                FULL OUTER JOIN productos_solo_visualizacion visualizacion ON producto.numero_parte = visualizacion.numero_parte 
                FULL OUTER JOIN precios_rangos preciosRangos ON producto.numero_parte = preciosRangos.numero_parte  
                FULL OUTER JOIN productos_Roles roles ON producto.numero_parte = roles.numero_parte 
                WHERE producto.numero_parte  = @numero_parte
                ";
    protected void dbConexion()
    {

        con = new SqlConnection(conexiones.conexionTienda());
        cmd = new SqlCommand();
        cmd.Connection = con;

    }
     
    /// <summary>
    /// Obtiene los productos asociados a una categoria (identificador) y para cierto rol de visibilidad.
    /// </summary>
    public DataTable obtenerProductos_RolyCat(string rol_visibilidad, string categoria_identificador)
    {


        dbConexion();
        using (con)
        {
            StringBuilder sel = new StringBuilder();

                {
                sel.Append(
                    @"SELECT DISTINCT id AS idSQL,* FROM  (SELECT 
                producto.id, 
                producto.numero_parte, 
                producto.titulo, 
                producto.descripcion_corta,
                producto.marca,
                producto.categoria_identificador,
                producto.noParte_Sap,
                producto.cantidad,
                producto.unidad,
                producto.imagenes,
                producto.metatags,
                producto.avisos,
                producto.unidad_venta,
                     (SELECT IIF(producto.orden IS NULL, 999, producto.orden)) AS orden,
                producto.disponibleVenta,
                producto.disponibleEnvio,
                roles.rol_visibilidad,
                roles.rol_preciosMultiplicador,
                preciosFantasma.preciosFantasma,
                preciosFantasma.porcentajeFantasma,
                preciosFijos.id_cliente,
                preciosFijos.moneda_fija,
                preciosFijos.precio,
                preciosRangos.moneda_rangos,
                preciosRangos.precio1,
                preciosRangos.max1,
                preciosRangos.precio2,
                preciosRangos.max2,
                preciosRangos.precio3,
                preciosRangos.max3,
                preciosRangos.precio4,
                preciosRangos.max4,
                preciosRangos.precio5,
                preciosRangos.max5,
                visualizacion.solo_para_Visualizar
                FROM productos_Datos as producto 
                FULL OUTER JOIN precios_ListaFija preciosFijos ON producto.numero_parte = preciosFijos.numero_parte
                FULL OUTER JOIN precios_rangos preciosRangos ON producto.numero_parte = preciosRangos.numero_parte  
                FULL OUTER JOIN productos_Roles roles ON producto.numero_parte = roles.numero_parte 
                FULL OUTER JOIN productos_solo_visualizacion visualizacion ON producto.numero_parte = visualizacion.numero_parte 
                FULL OUTER JOIN precios_fantasma preciosFantasma ON producto.numero_parte = preciosFantasma.numero_parte 
            ");

              
            sel.Append(@"
                WHERE roles.rol_visibilidad LIKE  '%'+@rol_visibilidad+'%' 
                OR roles.rol_visibilidad LIKE  '%general%'
                ) as t
                WHERE categoria_identificador LIKE  '%'+@categoria_identificador+'%'

                order by ORDEN asc");
            }

            string query = sel.ToString();
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@rol_visibilidad", SqlDbType.NVarChar, 25);
            cmd.Parameters["@rol_visibilidad"].Value = rol_visibilidad;

            cmd.Parameters.Add("@categoria_identificador", SqlDbType.NVarChar, 25);
            cmd.Parameters["@categoria_identificador"].Value = categoria_identificador;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
           
            
            return ds.Tables[0]; 
        }


    }
    /// <summary>
    /// Obtiene producto_Datos, roles, precios lista fija y precios rangos
    /// </summary>
    public DataTable obtenerProducto(string numero_parte)
    {


        dbConexion();
        using (con)
        {


            cmd.CommandText = queryObtenerProducto;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 50);
            cmd.Parameters["@numero_parte"].Value = numero_parte;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);


            return ds.Tables[0];
        }


    }
    /// <summary>
    /// Obtiene  precios lista fija y precios rangos
    /// </summary>
    public DataTable obtenerProductoPrecios(string numero_parte) {


        dbConexion();
        using (con) {


            cmd.CommandText = queryObtenerProductoPrecios;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 50);
            cmd.Parameters["@numero_parte"].Value = numero_parte;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);


            return ds.Tables[0];
            }


        }
    /// <summary>
    /// Obtiene producto_Datos, roles, precios lista fija y precios rangos
    /// </summary>
    public DataTable obtenerProductos(string numero_parte, string terminos) {
        terminos = textTools.lineSimple(terminos);
        terminos = terminos.Replace("de", "");

        string[] filtros = { "de", "para", "el", "la", "con", "a", "como", "desde", "hasta", "o", "y", "un", "máximo", "mínimo" };

        foreach (string f in filtros) {

            terminos = terminos.Replace(" " + f + " ", "");
        }



        string[] descripcion = terminos.Split(' ');

        terminos = string.Empty;

        foreach (string t in descripcion) {
            terminos = terminos + " OR producto.numero_parte   LIKE '%" + t + "%' ";
            terminos = terminos + " OR producto.titulo COLLATE SQL_Latin1_General_Cp1_CI_AI LIKE '%" + t + "%' ";


            terminos = terminos + " OR producto.etiquetas COLLATE SQL_Latin1_General_Cp1_CI_AI LIKE '%" + t + "%' ";
            terminos = terminos + " OR producto.marca COLLATE SQL_Latin1_General_Cp1_CI_AI LIKE '%" + t + "%' ";
        }
        dbConexion();
        using (con) {


            cmd.CommandText = queryObtenerProducto + terminos;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 50);
            cmd.Parameters["@numero_parte"].Value = numero_parte;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            DataTable dtResultados = ds.Tables[0];



            //  var y = dtResultados.AsEnumerable().Where(r => r.Field<String>("descripcion_corta").Contains(descripcion[1]));

            //  var result = from r in dtResultados.AsEnumerable() where descripcion.All(t => r.Field<String>("descripcion_corta").Contains(t)) select r;
            // var result =  dtResultados.AsEnumerable().Where(r => descripcion.Any(a => r.Field<string>("descripcion_corta").Contains(a)) );
            return dtResultados;
            //  return result.CopyToDataTable();
            }


        }

    /// <summary>
    /// Obtiene producto_Datos, roles, precios lista fija y precios rangos usando funciones FullTextSearch
    /// </summary>

    /// <summary>
    /// Obtiene producto_Datos, roles, precios lista fija y precios rangos usando funciones FullTextSearch
    /// </summary>
    public DataTable obtenerProductosFullTextSearch(string terminos) {

        terminos = textTools.lineSimple(terminos);
        string numero_parte = "";
        string queryNormal = "";
        if (terminos.Contains(" ")) {
            string[] filtros = { "de", "para", "el", "la", "con", "a", "como", "desde", "hasta", "o", "y", "un", "por", "máximo", "mínimo" };

            foreach (string f in filtros) {

                terminos = terminos.Replace(" " + f + " ", " ");
            }



            terminos = terminos.Replace(" ", " NEAR ");
        } else {
            numero_parte = terminos;
            queryNormal = queryObtenerProducto + " OR producto.numero_parte   LIKE '' + @numero_parte + '%'; ";
        }



        string queryFullTextSearch = queryObtenerProductoFullTextSearch + @"  INNER JOIN   CONTAINSTABLE (productos_Datos,  descripcion_corta, '(" + terminos + @")'  ) AS KEY_RES  ON producto.id = KEY_RES.[KEY]  WHERE KEY_RES.RANK > 5; ";


        dbConexion();

        using (con) {


            cmd.CommandText = queryFullTextSearch + queryNormal;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 50);
            cmd.Parameters["@numero_parte"].Value = numero_parte;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);

            DataTable dtResultados = new DataTable();

            dtResultados.Merge(ds.Tables[0]);
            if (ds.Tables.Count > 1) {
                dtResultados.Merge(ds.Tables[1]);
            }




            return dtResultados;

        }


    }


    public DataTable buscarPorCategoria (string categoria) {
        categoria = textTools.lineSimple(categoria);
        string queryCategoria = "";
        if (!string.IsNullOrWhiteSpace(categoria)) {

              queryCategoria = "DECLARE @categoria_identificador NVARCHAR(100); " +
                "SET @categoria_identificador = (SELECT TOP(1)  identificador FROM categorias WHERE nombre = @categoria)  ;  "
                + queryObtenerProductoFullTextSearch + " WHERE " + "  producto.categoria_identificador = @categoria_identificador; ";
        dbConexion();


        using (con) {


            cmd.CommandText = queryCategoria;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@categoria", SqlDbType.NVarChar, 50);
            cmd.Parameters["@categoria"].Value = categoria; 


            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);


            return ds.Tables[0];
             

         

        }
        } else { return null; }
    }
    static DataTable ordenarProductosPorAparicionDeTermino(DataTable dtProductos, string termino) {

        if (dtProductos.Rows.Count > 1) {
            dtProductos.Columns.Add("posicion", typeof(int));

        foreach (DataRow r in dtProductos.Rows) {
            string titulo = r["titulo"].ToString().ToLower();

            int posicion = titulo.IndexOf(termino);
            if (posicion == -1) posicion = 999;
            r["posicion"] = posicion;
        }
        dtProductos.AcceptChanges();
 
        DataView dv = dtProductos.DefaultView;
        dv.Sort = "posicion ASC";

        dtProductos= dv.ToTable();
        }
        return dtProductos;
    }

    /// <summary>
    /// [20190820] - Obtiene unicamente los datos de producto de la tabla SQL [productos_Datos]
    /// </summary>
    public model_productosTienda obtener_datos_de_producto(string numero_parte)
    {
        return null;
    }
    public DataTable obtenerProductosFullTextSearch_Contains(string terminos) {

        string terminoOriginal =  terminos;
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.CommandTimeout = 30;
        cmd.Connection = con;
        
       terminos = textTools.lineSimple(terminos);

        // INI Protección de palabras restringidas para SQL Injections y simbolos
        terminos = terminos.ToLower();
        terminos = terminos.Replace("select", "").Replace("999999.9", "").Replace("||", "").Replace("**", "").Replace("/*","")
            .Replace("*/", "").Replace("/**/", "").Replace("cast", "").Replace(" 0x", "").Replace("get_host_address", "").Replace("convert(", "")
            .Replace("chr(", "");
        terminos = terminos.Replace("(", "").Replace(")", "");
        terminos = terminos.Replace("//", " ");
        terminos = terminos.Replace("/", " ");
        terminos = terminos.Replace("-", " ");

        // Símbolos de escape
        terminos = terminos.Replace(@"""", @"""""");

        // FIN Protección de palabras restringidas para SQL Injections





        string queryFullTextSearch;
        DataTable dtProductosXCategoria = null;

        // Si se piensa buscar múltiples términos, se depura ciertas palabras
        if (terminos.Contains(" "))
        {
            
            string[] filtros = {
                "de", "para", "el", "la", "con", "a", "como", 
                "desde", "hasta", "o", "y", "un", "por", "máximo", "mínimo",
            };

            foreach (string f in filtros){
                terminos = terminos.Replace(" " + f + " ", " ");

            }

            string[] palabrasBusqueda = terminos.Split(' ');
 

           var query = string.Empty;
            string IniQuery = string.Empty;
            int i= 0;
            foreach (string term in palabrasBusqueda)
            {

                cmd.Parameters.Add("@termino"+i, SqlDbType.NVarChar, 20);
                cmd.Parameters["@termino"+i].Value = term;

                  IniQuery += $@"
                     
                    DECLARE @SearchString" + i + @" NVARCHAR(150);


                    SET @termino" + i + @" = '" + term + @"';
                    SET @SearchString" + i+ @" = 'FORMSOF (INFLECTIONAL, ""' + @termino" + i + @" + '"") OR FORMSOF (THESAURUS, ""' + @termino" + i + @" + '"")'; 
                ";


                query += " CONTAINS((producto.upc, producto.noParte_interno, producto.noParte_proveedor,  producto.noParte_Sap, producto.titulo_corto_ingles, producto.titulo, " +
                                       "producto.marca, producto.metatags, producto.numero_parte, producto.descripcion_corta, producto.avisos), @SearchString" + i + ") OR";
                i++;
            }

            query = query.TrimEnd('R');
            query = query.TrimEnd('O');

            /*
          terminos = query.TrimEnd('D');
          terminos = query.TrimEnd('N');
          terminos = query.TrimEnd('A');
            */

            queryFullTextSearch = IniQuery + " "+queryObtenerProductoFullTextSearch + " WHERE " + query;
        }
        // Si solo es una palabra
        else {
            cmd.Parameters.Add("@termino" , SqlDbType.NVarChar, 20);
            cmd.Parameters["@termino" ].Value = terminos;

            string IniQuery = $@"
                 
                    DECLARE @SearchString NVARCHAR(150);

                    SET @termino = '{terminos}';
                    SET @SearchString = 'FORMSOF (INFLECTIONAL, ""' + @termino + '"") OR FORMSOF (THESAURUS, ""' + @termino + '"")'; 
                ";
 



            queryFullTextSearch = IniQuery+ queryObtenerProductoFullTextSearch + " WHERE " + " " +
                "CONTAINS((producto.upc, producto.noParte_interno, producto.noParte_proveedor, producto.titulo_corto_ingles,   producto.titulo, producto.marca," +
                " producto.metatags, producto.categoria_identificador, producto.numero_parte,producto.descripcion_corta, producto.avisos), @SearchString) " +
                "  OR  producto.numero_parte LIKE  '%'+ @termino + '%'" +
                "  OR  producto.noParte_Sap LIKE  '%'+ @termino + '%'; ";

            dtProductosXCategoria = buscarPorCategoria(terminos);
        }



        
        DataSet ds = new DataSet();
        using (con) {


            cmd.CommandText = queryFullTextSearch;
            cmd.CommandType = CommandType.Text;

          

            
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);

            
        }

        DataTable dtResultados = new DataTable();

        dtResultados.Merge(ds.Tables[0]);


        if (ds.Tables.Count > 1) {
            dtResultados.Merge(ds.Tables[1]);
        }

        if (!terminos.Contains(" ") && dtProductosXCategoria != null && dtProductosXCategoria.Rows.Count > 1) {
             dtResultados.Merge(dtProductosXCategoria);
        } else {
     //       dtResultados = ordenarProductosPorAparicionDeTermino(dtResultados, terminos);
        }
        
        dtResultados = OrdenarPorAlgoritmo(dtResultados, terminos, terminoOriginal);
        dtResultados = dtResultados.DefaultView.ToTable(true);

        return dtResultados;

    }
    /// <summary>
    ///  20210412 - Ordenar por algoritmo
    /// </summary>
    private static DataTable OrdenarPorAlgoritmo(DataTable DTproductos, string terminos, string terminoOriginal)
    {
        terminoOriginal= terminoOriginal.ToLower();
        if (DTproductos.Rows.Count == 0) return DTproductos;
        DTproductos.Columns.Add("puntajeBusqueda", typeof(Int32));
        DTproductos.Columns.Add("permitirBusqueda", typeof(Boolean));

        terminos = terminos.ToLower();
        terminos = textTools.QuitarAcentos(terminos);
        string[] palabrasBusqueda = terminos.Split(' ');

        int puntajeTotal = 0;
        int totalProductos = DTproductos.Rows.Count;

        foreach (DataRow r in DTproductos.Rows)
        {
            r["puntajeBusqueda"] = 0;
            r["permitirBusqueda"] = false;
            string numero_parte = textTools.QuitarAcentos(r["numero_parte"].ToString().ToLower());
            string noParte_Sap = textTools.QuitarAcentos(r["noParte_Sap"].ToString().ToLower());
            string titulo = textTools.QuitarAcentos(r["titulo"].ToString().ToLower());
            string descripcion_corta = textTools.QuitarAcentos(r["descripcion_corta"].ToString().ToLower());
            string marca = textTools.QuitarAcentos(r["marca"].ToString().ToLower());
            string especificaciones = textTools.QuitarAcentos(r["especificaciones"].ToString().ToLower());
            string metatags = textTools.QuitarAcentos(r["metatags"].ToString().ToLower());
            string etiquetas = textTools.QuitarAcentos(r["etiquetas"].ToString().ToLower());
            string atributos = textTools.QuitarAcentos(r["atributos"].ToString().ToLower());
            int puntajeBusqueda = int.Parse(r["puntajeBusqueda"].ToString());
    
            foreach (var p in palabrasBusqueda)
            {


                if (numero_parte.Replace("/"," ").Contains(p))
                {
                    // si se encuentra la palabra, contamos la cantidad de ocurrencias.
                    int count = numero_parte.Split('/').Count(str => str.Contains(p));
                   
                    puntajeBusqueda += count * 35;
                  }
                if(numero_parte == terminoOriginal) 
                    puntajeBusqueda += 70;

                if (noParte_Sap == terminoOriginal)
                    puntajeBusqueda += 70;

                if (titulo.Replace("/", " ").Contains(p)) puntajeBusqueda += 10;

                int posicion = Array.IndexOf(titulo.Split(' '), p);
                if (posicion != -1) {
                    if(posicion == 0) {
                        posicion = 1;
                        puntajeBusqueda += 10;
                    }
                    puntajeBusqueda += (1/posicion)*60;
                }



                else puntajeBusqueda += -20;

                if (descripcion_corta.Contains(p)) puntajeBusqueda += 5;
                else puntajeBusqueda += -10;

                if (marca.Contains(p)) { 
                    puntajeBusqueda += 20;
                    r["permitirBusqueda"] = true;
                }
                if (especificaciones.Contains(p)) puntajeBusqueda += 1;
                if (metatags.Contains(p)) puntajeBusqueda += 30;

                if (etiquetas.Contains(p)) puntajeBusqueda += 10;

                if (atributos.Contains(p)) puntajeBusqueda += 1;
                r["puntajeBusqueda"] = puntajeBusqueda;

                puntajeTotal += puntajeBusqueda;
            }
            DTproductos.AcceptChanges();
        }

        var promedio = (puntajeTotal / totalProductos) * 1;

        var table = DTproductos.AsEnumerable()
            .OrderByDescending(t => DTproductos.Columns["puntajeBusqueda"]).CopyToDataTable();

        goto OmitirFiltrado;
        if (table.Rows.Count > 5) {  
        var valorMedio1 = int.Parse(table.Rows[totalProductos / 2]["puntajeBusqueda"].ToString());
        var valorMedio2 = int.Parse(table.Rows[totalProductos / 2 + 1]["puntajeBusqueda"].ToString());
        var valorMediana = (valorMedio1 + valorMedio2) / 2;

        table = table.AsEnumerable().Where(r => 
        r.Field<int>("puntajeBusqueda") >= valorMediana
        || r.Field<bool>("permitirBusqueda") == true )
                .CopyToDataTable();
             }

        OmitirFiltrado:
        DataView dv = table.DefaultView;
        dv.Sort = "puntajeBusqueda DESC";

        DTproductos = dv.ToTable();

        return DTproductos;
    }
    /// <summary>
    /// Recibe un DataTable de un producto: row[0](Ya procesado con precios) para convertir a List<model_productosTienda>
    /// </summary>
    public model_productosTienda dtProductoToList(DataTable DTproducto)
    {
        model_productosTienda producto = new model_productosTienda();


        producto.id = int.Parse(DTproducto.Rows[0]["id"].ToString());
        producto.titulo = DTproducto.Rows[0]["titulo"].ToString();
        producto.numero_parte = DTproducto.Rows[0]["numero_parte"].ToString();
        producto.descripcion_corta = DTproducto.Rows[0]["descripcion_corta"].ToString();
        producto.titulo_corto_ingles = DTproducto.Rows[0]["titulo_corto_ingles"].ToString();
        producto.especificaciones = DTproducto.Rows[0]["especificaciones"].ToString();
        producto.marca = DTproducto.Rows[0]["marca"].ToString();
        producto.categoria_identificador = DTproducto.Rows[0]["categoria_identificador"].ToString().Replace(" ", "").Split(',');
        producto.imagenes = DTproducto.Rows[0]["imagenes"].ToString().Replace(" ","").Split(',');
        producto.metatags = DTproducto.Rows[0]["marca"].ToString().Replace(" ", "").Split(',');

        if (DTproducto.Rows[0]["peso"] != DBNull.Value)
            producto.peso = float.Parse(DTproducto.Rows[0]["peso"].ToString());
         else
            producto.peso = float.NaN;


        if (DTproducto.Rows[0]["alto"] != DBNull.Value)
            producto.alto = float.Parse(DTproducto.Rows[0]["alto"].ToString());
        else
            producto.alto = float.NaN;


        if (DTproducto.Rows[0]["ancho"] != DBNull.Value)
            producto.ancho = float.Parse(DTproducto.Rows[0]["ancho"].ToString());
        else
            producto.ancho = float.NaN;


        if (DTproducto.Rows[0]["profundidad"] != DBNull.Value)
            producto.profundidad = float.Parse(DTproducto.Rows[0]["profundidad"].ToString());
        else
            producto.profundidad = float.NaN;


   
      
        producto.pdf = DTproducto.Rows[0]["pdf"].ToString();
        producto.video = DTproducto.Rows[0]["video"].ToString();
        producto.unidad_venta = DTproducto.Rows[0]["unidad_venta"].ToString();
        producto.cantidad = decimal.Parse(DTproducto.Rows[0]["cantidad"].ToString());
        producto.unidad = DTproducto.Rows[0]["unidad"].ToString();

        producto.producto_alternativo = DTproducto.Rows[0]["producto_alternativo"].ToString();

        producto.productos_relacionados = DTproducto.Rows[0]["productos_relacionados"].ToString();

        producto.atributos = DTproducto.Rows[0]["atributos"].ToString();

        producto.noParte_proveedor = DTproducto.Rows[0]["noParte_proveedor"].ToString();
        producto.noParte_interno = DTproducto.Rows[0]["noParte_interno"].ToString();

        producto.upc = DTproducto.Rows[0]["upc"].ToString();
        producto.noParte_Competidor = DTproducto.Rows[0]["noParte_Competidor"].ToString();


        if (DTproducto.Rows[0]["orden"] != DBNull.Value)
        {
            producto.orden = int.Parse(DTproducto.Rows[0]["orden"].ToString());
        } else
        {
            producto.orden = 9999;
        }

        producto.etiquetas = DTproducto.Rows[0]["etiquetas"].ToString().Replace(" ", "").Split(','); ;
        producto.disponibleVenta = int.Parse(DTproducto.Rows[0]["disponibleVenta"].ToString());


      
        producto.rol_visibilidad = DTproducto.Rows[0]["rol_visibilidad"].ToString().Replace(" ", "").Split(',');
        producto.rol_preciosMultiplicador = DTproducto.Rows[0]["rol_preciosMultiplicador"].ToString().Replace(" ", "").Split(',');



        if (DTproducto.Rows[0]["precio"] != DBNull.Value)
        {
            producto.precio = decimal.Parse(DTproducto.Rows[0]["precio"].ToString());
            producto.moneda_fija = DTproducto.Rows[0]["moneda_fija"].ToString();
        } else {

            producto.precio = decimal.MinusOne;





            if (DTproducto.Rows[0]["precio1"] != DBNull.Value)
            {
                producto.moneda_rangos = DTproducto.Rows[0]["moneda_rangos"].ToString();
                producto.precio1 = decimal.Parse(DTproducto.Rows[0]["precio1"].ToString());
                producto.min1 = decimal.Parse(DTproducto.Rows[0]["min1"].ToString());
                producto.max1 = decimal.Parse(DTproducto.Rows[0]["max1"].ToString());
            } else
            {
                producto.precio1 = decimal.MinusOne;
                producto.min1 = decimal.MinusOne;
                producto.max1 = decimal.MinusOne;
            };


            if (DTproducto.Rows[0]["precio2"] != DBNull.Value)
            {
                producto.precio2 = decimal.Parse(DTproducto.Rows[0]["precio2"].ToString());
                producto.min2 = decimal.Parse(DTproducto.Rows[0]["min2"].ToString());
                producto.max2 = decimal.Parse(DTproducto.Rows[0]["max2"].ToString());
            } else
            {
                producto.precio2 = decimal.MinusOne;
                producto.min2 = decimal.MinusOne;
                producto.max2 = decimal.MinusOne;
            };

            if (DTproducto.Rows[0]["precio3"] != DBNull.Value)
            {
                producto.precio3 = decimal.Parse(DTproducto.Rows[0]["precio3"].ToString());
                producto.min3 = decimal.Parse(DTproducto.Rows[0]["min3"].ToString());
                producto.max3 = decimal.Parse(DTproducto.Rows[0]["max3"].ToString());
            } else
            {
                producto.precio3 = decimal.MinusOne;
                producto.min3 = decimal.MinusOne;
                producto.max3 = decimal.MinusOne;
            };

            if (DTproducto.Rows[0]["precio4"] != DBNull.Value)
            {
                producto.precio4 = decimal.Parse(DTproducto.Rows[0]["precio4"].ToString());
                producto.min4 = decimal.Parse(DTproducto.Rows[0]["min4"].ToString());
                producto.max4 = decimal.Parse(DTproducto.Rows[0]["max4"].ToString());
            } else
            {
                producto.precio4 = decimal.MinusOne;
                producto.min4 = decimal.MinusOne;
                producto.max4 = decimal.MinusOne;
            };

            if (DTproducto.Rows[0]["precio5"] != DBNull.Value)
            {
                producto.precio5 = decimal.Parse(DTproducto.Rows[0]["precio5"].ToString());
                producto.min5 = decimal.Parse(DTproducto.Rows[0]["min5"].ToString());
                producto.max5 = decimal.Parse(DTproducto.Rows[0]["max5"].ToString());
            } else
            {
                producto.precio5 = decimal.MinusOne;
                producto.min5 = decimal.MinusOne;
                producto.max5 = decimal.MinusOne;
            };

        } 
        

        return producto;
    }
    public model_productosTienda dtProductoToListPrecio(DataTable DTproducto) {
        
        model_productosTienda producto = new model_productosTienda();


        producto.id = int.Parse(DTproducto.Rows[0]["id"].ToString());
      
        producto.numero_parte = DTproducto.Rows[0]["numero_parte"].ToString();
       
         
        
        producto.disponibleVenta = int.Parse(DTproducto.Rows[0]["disponibleVenta"].ToString());



        producto.rol_visibilidad = DTproducto.Rows[0]["rol_visibilidad"].ToString().Replace(" ", "").Split(',');
        producto.rol_preciosMultiplicador = DTproducto.Rows[0]["rol_preciosMultiplicador"].ToString().Replace(" ", "").Split(',');



        if (DTproducto.Rows[0]["precio"] != DBNull.Value) {
            producto.precio = decimal.Parse(DTproducto.Rows[0]["precio"].ToString());
            producto.moneda_fija = DTproducto.Rows[0]["moneda_fija"].ToString();
            } else {

            producto.precio = decimal.MinusOne;





            if (DTproducto.Rows[0]["precio1"] != DBNull.Value) {
                producto.moneda_rangos = DTproducto.Rows[0]["moneda_rangos"].ToString();
                producto.precio1 = decimal.Parse(DTproducto.Rows[0]["precio1"].ToString());
                producto.min1 = decimal.Parse(DTproducto.Rows[0]["min1"].ToString());
                producto.max1 = decimal.Parse(DTproducto.Rows[0]["max1"].ToString());
                } else {
                producto.precio1 = decimal.MinusOne;
                producto.min1 = decimal.MinusOne;
                producto.max1 = decimal.MinusOne;
                };


            if (DTproducto.Rows[0]["precio2"] != DBNull.Value) {
                producto.precio2 = decimal.Parse(DTproducto.Rows[0]["precio2"].ToString());
                producto.min2 = decimal.Parse(DTproducto.Rows[0]["min2"].ToString());
                producto.max2 = decimal.Parse(DTproducto.Rows[0]["max2"].ToString());
                } else {
                producto.precio2 = decimal.MinusOne;
                producto.min2 = decimal.MinusOne;
                producto.max2 = decimal.MinusOne;
                };

            if (DTproducto.Rows[0]["precio3"] != DBNull.Value) {
                producto.precio3 = decimal.Parse(DTproducto.Rows[0]["precio3"].ToString());
                producto.min3 = decimal.Parse(DTproducto.Rows[0]["min3"].ToString());
                producto.max3 = decimal.Parse(DTproducto.Rows[0]["max3"].ToString());
                } else {
                producto.precio3 = decimal.MinusOne;
                producto.min3 = decimal.MinusOne;
                producto.max3 = decimal.MinusOne;
                };

            if (DTproducto.Rows[0]["precio4"] != DBNull.Value) {
                producto.precio4 = decimal.Parse(DTproducto.Rows[0]["precio4"].ToString());
                producto.min4 = decimal.Parse(DTproducto.Rows[0]["min4"].ToString());
                producto.max4 = decimal.Parse(DTproducto.Rows[0]["max4"].ToString());
                } else {
                producto.precio4 = decimal.MinusOne;
                producto.min4 = decimal.MinusOne;
                producto.max4 = decimal.MinusOne;
                };

            if (DTproducto.Rows[0]["precio5"] != DBNull.Value) {
                producto.precio5 = decimal.Parse(DTproducto.Rows[0]["precio5"].ToString());
                producto.min5 = decimal.Parse(DTproducto.Rows[0]["min5"].ToString());
                producto.max5 = decimal.Parse(DTproducto.Rows[0]["max5"].ToString());
                } else {
                producto.precio5 = decimal.MinusOne;
                producto.min5 = decimal.MinusOne;
                producto.max5 = decimal.MinusOne;
                };

            }


        return producto;
        }


    /// <summary>
    /// Actualiza un campo de un producto de la tabla productos_Datos
    /// </summary>
    public static bool actualizarCampoProducto(string numero_parte, string campo, string valor) {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {
           
            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar);
            cmd.Parameters["@numero_parte"].Value = numero_parte;

            cmd.Parameters.Add("@campo", SqlDbType.NVarChar);
            cmd.Parameters["@campo"].Value = campo;

            cmd.Parameters.Add("@valor", SqlDbType.NVarChar);
            cmd.Parameters["@valor"].Value = valor;

            cmd.CommandText = "SET LANGUAGE English; UPDATE  productos_Datos SET "+ campo + "=@valor WHERE numero_parte = @numero_parte ";
            cmd.CommandType = CommandType.Text;
 

            con.Open();
            try {
                int resultado = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) {
                devNotificaciones.error("Actualizar valor de un producto: [" + numero_parte + "] campo:[" + campo + "] valor: [" + valor+"]", ex);
                return false;
            }


        }



    }
    /// <summary>
    /// Actualiza un campo de un producto de la tabla productos_Datos
    /// </summary>
    public static bool actualizarCampoProductoPrecioRango(string numero_parte, string campo, string valor) {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar);
            cmd.Parameters["@numero_parte"].Value = numero_parte;

            cmd.Parameters.Add("@campo", SqlDbType.NVarChar);
            cmd.Parameters["@campo"].Value = campo;

            cmd.Parameters.Add("@valor", SqlDbType.NVarChar);
            cmd.Parameters["@valor"].Value = valor;

            cmd.CommandText = "SET LANGUAGE English; UPDATE  precios_rangos SET " + campo + "=@valor WHERE numero_parte = @numero_parte ";
            cmd.CommandType = CommandType.Text;


            con.Open();
            try {
                int resultado = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) {
                devNotificaciones.error("Actualizar valor de un producto rango precio: [" + numero_parte + "] campo:[" + campo + "] valor: [" + valor + "]", ex);
                return false;
            }


        }



    }



    /// <summary>
    /// [20190913] Elimina un producto de las siguientes tablas: [productos_Datos], [productos_Roles], [precios_rangos], [precios_ListaFija]
    /// </summary>
    public static bool eliminar_producto(string numero_parte) {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con)
        {

            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar,100);
            cmd.Parameters["@numero_parte"].Value = numero_parte;

       

            cmd.CommandText = @"
                                DELETE FROM  productos_Datos WHERE numero_parte =  @numero_parte;
                                DELETE FROM productos_Roles WHERE numero_parte = @numero_parte;
                                DELETE FROM  precios_rangos WHERE numero_parte =  @numero_parte;
                                DELETE FROM  precios_ListaFija WHERE numero_parte =  @numero_parte;
                                DELETE FROM  productos_solo_visualizacion WHERE numero_parte =  @numero_parte;
                                DELETE FROM productos_stock WHERE numero_parte =  @numero_parte;
                                 ";
            cmd.CommandType = CommandType.Text;


            con.Open();
            try
            {
                int resultado = cmd.ExecuteNonQuery();
                if (resultado >= 1) return true;
                else return false;

            }
            catch (Exception ex)
            {
                devNotificaciones.ErrorSQL("Eliminar un producto de la tienda: [" + numero_parte + "]" , ex,null);
                return false;
            }


        }
    }


    /// <summary>
    /// [20191122] Devuelve [true][false] si el producto esta disponible para venta, campo: [productos_Datos][disponibleVenta]
    /// </summary>
    public static bool productoDisponibleVenta(string numero_parte) {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar);
            cmd.Parameters["@numero_parte"].Value = numero_parte;



            cmd.CommandText = @"SELECT disponibleVenta FROM productos_Datos WHERE numero_parte = @numero_parte";
            cmd.CommandType = CommandType.Text;


            con.Open();
            try {
                int resultado = int.Parse(cmd.ExecuteScalar().ToString());
                if (resultado == 1) return true;
                else return false;

                }
            catch (Exception ex) {
                devNotificaciones.ErrorSQL("Consultar disponibilidad de venta: [" + numero_parte + "]", ex, null);
                return false;
                }


            }
        }

    /// <summary>
    /// [20200228] Devuelve [true][false] si el producto esta disponible para ser enviado de manera gratuita, campo: [productos_Datos][disponibleEnvio]
    /// </summary>
    /// CarlosM
    public static bool productoDisponibleEnvio(string numero_parte)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con)
        {

            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar);
            cmd.Parameters["@numero_parte"].Value = numero_parte;



            cmd.CommandText = @"SELECT disponibleEnvio FROM productos_Datos WHERE numero_parte = @numero_parte";
            cmd.CommandType = CommandType.Text;


            con.Open();
            try
            {
                int resultado = int.Parse(cmd.ExecuteScalar().ToString());
                if (resultado == 1) return true;
                else return false;

            }
            catch (Exception ex)
            {
                devNotificaciones.ErrorSQL("Consultar disponibilidad de venta: [" + numero_parte + "]", ex, null);
                return false;
            }


        }
    }


    /// <summary>
    /// [20200402] Devuelve [true] solo para visualización, si el producto esta disponible para operaciones [false]  , campo: [productos_solo_visualizacion][solo_para_Visualizar]
    /// </summary>
    /// CarlosM
    public static bool productoVisualización (string numero_parte)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con)
        {

            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar);
            cmd.Parameters["@numero_parte"].Value = numero_parte;



            cmd.CommandText = @"SELECT solo_para_Visualizar FROM productos_solo_visualizacion WHERE numero_parte = @numero_parte";
            cmd.CommandType = CommandType.Text;


            con.Open();
            try
            {
                string resultado = cmd.ExecuteScalar().ToString();

                // Si no se ha definido esta característica: se establece en [false], si se ha definido, toma su valor establecido [true/false].
                bool solo_para_Visualizar = string.IsNullOrEmpty(resultado) ? false : bool.Parse(resultado);


                
                  return solo_para_Visualizar;

            }
            catch (Exception ex)
            {
                devNotificaciones.ErrorSQL("Disponible para operaciones: [" + numero_parte + "]", ex, null);
                return false;
            }


        }
    }
}
     
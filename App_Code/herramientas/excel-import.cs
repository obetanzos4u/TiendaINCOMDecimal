using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de excel_import
/// </summary>
public class excelImport : model_excelImport {
    public excelImport() {
        numeroHoja = 1;
        }
    /// <summary>
    /// Hace un Truncate a la tabla destino, esto con el fin de eliminar el contenido y remplazarlo por el nuevo.
    /// </summary>
    public static void eliminarTabla(string nombreTabla) {
        
            StringBuilder query = new StringBuilder();

            query.Append("SET LANGUAGE English; TRUNCATE TABLE "+nombreTabla+";");

        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = con;

        using (con) {

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                 

                con.Open();

                cmd.ExecuteNonQuery();
            }

        
    }

     public DataTable XlsxToDataTableOLEDB() {
        DataTable dt = new DataTable();
        string strExcelConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1'";
        using (OleDbConnection connExcel = new OleDbConnection(strExcelConn)) {
            string selectString = "SELECT * FROM ["+ nombreHoja+"$]";
            using (OleDbCommand cmdExcel = new OleDbCommand(selectString, connExcel)) {
                cmdExcel.Connection = connExcel;
                connExcel.Open();

                OleDbDataAdapter adp = new OleDbDataAdapter();
                adp.SelectCommand = cmdExcel;
                adp.FillSchema(dt, SchemaType.Source);
                adp.Fill(dt);
                int range = dt.Columns.Count;
                int row = dt.Rows.Count;
                }
            }

        return dt;
        }
    public DataTable toDT() {

        DataTable dt = new DataTable();

        using (XLWorkbook workBook = new XLWorkbook(filePath)) {
            //Read the first Sheet from Excel file.
            IXLWorksheet workSheet = workBook.Worksheet(1);

            //Create a new DataTable.


            //Loop through the Worksheet rows.
            bool firstRow = true;
            foreach (IXLRow row in workSheet.Rows()) {
                //Use the first row to add columns to DataTable.
                if (firstRow) {
                    foreach (IXLCell cell in row.Cells()) {
                        dt.Columns.Add(cell.Value.ToString());
                        }
                    firstRow = false;
                    } else {
                    //Add rows to DataTable.
                    dt.Rows.Add();
                    int i = 0;
                    foreach (IXLCell cell in row.Cells()) {
                        dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                        i++;
                        }
                    }



                }
            }
        return dt;
        }

    /// <summary>
    /// Inserta registros a una tabla [ Nombre de la tabla + DataTable dt]
    /// </summary>
    public string insertar(string tabla, DataTable data, string validacion) {



        string campos = "";
        foreach (DataColumn col in data.Columns) {
            campos = campos + col.ColumnName.ToString() + ",";
            }

        campos = campos.TrimEnd(',', ' ');

        StringBuilder log = new StringBuilder();

        // Sirve para establecer de donde empezará a guardar (para resolver el problema de longitudes > 255 se ingreso el 1er valor en xxxxxx..... > 250
        int valorRow = 0;
        if (tabla.ToLower() == "productos_datos") valorRow = 1;


            for (int r = valorRow; r < data.Rows.Count; r++) {

            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            using (con) {
             
                string Variables = "";
                string ValoresVariables = "";
                string v = "";
                for (int i = 0; i < data.Columns.Count; i++) {

                    v = textTools.lineSimple(data.Rows[r][i].ToString());
                    if(v == "VIEW5") {
                        Console.WriteLine("");
                    }
                    // Inicia proceso de validación de columnas
                   
                    // Si no esta vacio quiere decir que se selecciono un elemento de la DDL el cual recibe este parametro
                    if (!string.IsNullOrWhiteSpace(validacion)) {

                        // Obtenemos el nombre de la columna actual
                        string nombreColumna = data.Columns[i].ColumnName;

                        Dictionary<string, int> datosValidar = importProductos.validar(tabla.ToLower());

                        bool contieneInfoAvalidar = datosValidar.ContainsKey(nombreColumna);
                       
                        if (contieneInfoAvalidar) {
                            int longitud = datosValidar[nombreColumna];
                        
                            if (v.Length > longitud) {

                                log.AppendLine("La columna: " + nombreColumna + " supero el limite. Col 1: " + data.Rows[r][0].ToString());

                                v = v.Substring(0, longitud);

                                }
                            }
                      
                        }
                    // Validación
                    if (string.IsNullOrWhiteSpace(v)) {

                        cmd.Parameters.AddWithValue("valor" + i, DBNull.Value);


                        } else {
                        //  fileWriter.Write(v + " NO es nulo " + "\r\n");
                        cmd.Parameters.AddWithValue("valor" + i, v);
                        }
                    ValoresVariables += v + ", ";
                    Variables = Variables + "@valor" + i + ", ";

                    }

                Variables = Variables.TrimEnd(',', ' ');
                string query = (@"SET LANGUAGE English;  INSERT INTO " + tabla + " (" + campos + ")  VALUES (" + Variables + "); ");
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                con.Open();
                try {
                    string resultado = Convert.ToString(cmd.ExecuteScalar());
                    }
                catch (Exception ex) {
                    log.AppendLine("Linea: "+ r +" Valores: \r\n" + ValoresVariables + "[" + v + "] \r\n");
                    log.AppendLine(query);
                    log.AppendLine("Excepcion: "+ex.ToString() +"");
                    log.AppendLine("\r\n -------------------------- \r\n");

                }

                }

            }

        return log.ToString();
        }

    /// <summary>
    /// Actualiza registros a una tabla [ Nombre de la tabla + id Identificador + DataTable dt]
    /// </summary>

   
    public string actualizar(string tabla, DataTable data, string validacion, string campoReferencia) {



        string campos = "";
        foreach (DataColumn col in data.Columns) {
            campos = campos + col.ColumnName.ToString() + ",";
        }

        campos = campos.TrimEnd(',', ' ');

        StringBuilder log = new StringBuilder();

        // Sirve para establecer de donde empezará a guardar (para resolver el problema de longitudes > 255 se ingreso el 1er valor en xxxxxx..... > 250
        int valorRow = 0;
        if (tabla.ToLower() == "productos_datos") valorRow = 1;


        for (int r = valorRow; r < data.Rows.Count; r++) {

            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            using (con) {

                string valores = "";
                string v = "";
                for (int i = 0; i < data.Columns.Count; i++) {

                    v = data.Rows[r][i].ToString().Replace("\t", "").Replace("\r", "").Replace("\n", "").Replace("\r\n", "").Replace("\v", "");

                    // Obtenemos el nombre de la columna actual
                    string nombreColumna = data.Columns[i].ColumnName;
                   
                    // Inicia proceso de validación de columnas

                    // Si no esta vacio quiere decir que se selecciono un elemento de la DDL el cual recibe este parametro
                    if (validacion != "") {

                        Dictionary<string, int> datosValidar = importProductos.validar(tabla.ToLower());

                        bool contieneInfoAvalidar = datosValidar.ContainsKey(nombreColumna);

                        if (contieneInfoAvalidar) {
                            int longitud = datosValidar[nombreColumna];
                           
                            if (v.Length > longitud) {

                                log.AppendLine("La columna: " + nombreColumna + " supero el limite. Col 1: " + data.Rows[r][0].ToString());

                                v = v.Substring(0, longitud);

                            }
                        }

                    }
                    // Validación
                    if (string.IsNullOrWhiteSpace(v)) {

                        cmd.Parameters.AddWithValue("valor" + i, DBNull.Value);


                    } else {
                        //  fileWriter.Write(v + " NO es nulo " + "\r\n");
                        cmd.Parameters.AddWithValue("valor" + i, v);
                    }

                    valores = valores + nombreColumna + " = @valor" + i+ ", ";


                }

                valores = valores.TrimEnd(',', ' ');
                string query = (@"SET LANGUAGE English;  UPDATE " + tabla + " SET " + valores + " WHERE " + campoReferencia + " = '" + data.Rows[r][campoReferencia].ToString() + "' ;");
                // string query = (@"SET LANGUAGE English;  INSERT INTO " + tabla + " (" + campos + ")  VALUES (" + valores + "); ");
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                con.Open();
                try {
                    int resultado = int.Parse(cmd.ExecuteNonQuery().ToString());
                    // Si no se afecta ningún registro
                    if (resultado  == 0) {
                        log.AppendLine("El siguiente registro no encontré ninguna referencia para su actualización, Referencia:" + campoReferencia + " Valor: " + data.Rows[r][campoReferencia].ToString());
                        log.AppendLine("Valores: \r\n" + valores);
                        log.AppendLine("\r\n -------------------------- \r\n");

                    }

                    // Si hay más de 2 registros
                    if (resultado == 0) {
                        log.AppendLine("Se actualizó más de 2 incidencias con la referencia dada, Referencia:" + campoReferencia + " Valor: " + data.Rows[r][campoReferencia].ToString());
                        log.AppendLine("Valores: \r\n" + valores);
                        log.AppendLine("\r\n -------------------------- \r\n");

                    }
                }
                catch (Exception ex) {

                    log.AppendLine(ex.Message.ToString());
                    log.AppendLine("Valores: \r\n" + valores);
                    log.AppendLine("\r\n -------------------------- \r\n");

                }

            }

        }

        return log.ToString();
    }

    public void actualizar(string tabla, string id, DataTable data) {



        for (int r = 0; r < data.Rows.Count; r++) {
            // R = número de filas  a recorrer
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            using (con) {
                string valores = "";

                // R = número de columnas  a recorrer por cada fila, definido del ciclo superior en proceso
                foreach (DataColumn col in data.Columns) {


                    /* Guardamos el valor del dato en una variable llamada "v", accediendo a ella  donde "r" = a la fila, e "i" es la columna,
                    remplazando saltos de linea, tabuladores */
                    string v = data.Rows[r][col.ColumnName].ToString().Replace("\t", "").Replace("\r", "").Replace("\n", "").Replace("\r\n", "").Replace("\v", "");

                    // Si el valor es nulo o espacios en blanco, evita el error de variable vacia con el tipo DBNull.Value
                    if (string.IsNullOrWhiteSpace(v)) {
                        cmd.Parameters.AddWithValue("@V" + col.ColumnName.ToString(), DBNull.Value);
                        } else {
                        cmd.Parameters.AddWithValue("@V" + col.ColumnName.ToString(), v);
                        }

                    valores = valores + col.ColumnName.ToString() + " = @V" + col.ColumnName.ToString() + ", ";





                    }
                valores = valores.TrimEnd(',', ' ');

                // Fin del ciclo foreach DataColumn
                string query = (@"SET LANGUAGE English;  UPDATE " + tabla + " SET " + valores + " WHERE " + id + " = '" + data.Rows[r][id].ToString() + "' ;");

                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                con.Open();
                string resultado = Convert.ToString(cmd.ExecuteScalar());
                }
            } // Fin del ciclo


        }
    }
public class model_excelImport {
    public string fileName { get; set; }
    public string filePath { get; set; }
    public string archivo { get; set; }
    public int numeroHoja { get; set; }
    public string nombreHoja { get; set; }
    }
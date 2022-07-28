using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Descripción breve de usuarios
/// </summary>
 
public partial class reportesVentasDiario {

    ///<summary>
    /// Recuperar el monto total de PEDIDOS en un listado de todos los asesores entre un rango de fechas
    ///</summary>
    public static DataTable recuperarMonto_pedidosAsesores(DateTime fechaDesde, DateTime fechaHasta) {


        //  Si no se recibe parametro fechaDesde, por default los últimos 30 días
        if (fechaDesde == null) fechaDesde = utilidad_fechas.obtenerCentral().AddDays(-30);

        // Si no se recibe parametro fechaHasta, se estable al día actual
        if (fechaHasta == null) fechaHasta = utilidad_fechas.obtenerCentral(); ;

        TimeSpan tsInicio = new TimeSpan(0, 0, 0);
        TimeSpan tsFinal = new TimeSpan(24, 59, 59);

        fechaDesde = fechaDesde.Date + tsInicio;
        fechaHasta = fechaHasta.Date + tsFinal;

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            StringBuilder sel = new StringBuilder();
            sel.Append("SELECT SUM(montoPedido) AS montoPedidos ,nombreVendedor as NombreVendedor FROM ReportesVentas_RegistrosPedidosDiaADia ");
            sel.Append(" WHERE fechaPedido BETWEEN @fechaDesde AND  @fechaHasta ");
            sel.Append(" GROUP BY nombreVendedor ORDER BY montoPedidos DESC");

            cmd.CommandText = sel.ToString();
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@fechaDesde", SqlDbType.DateTime);
            cmd.Parameters["@fechaDesde"].Value = fechaDesde;

            cmd.Parameters.Add("@fechaHasta", SqlDbType.DateTime);
            cmd.Parameters["@fechaHasta"].Value = fechaHasta;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);

            return ds.Tables[0];
        }


    }

    ///<summary>
    /// Recuperar el monto total FACTURADO en un listado de todos los asesores entre un rango de fechas
    ///</summary>
    public static DataTable recuperarMonto_facturadoAsesores(DateTime fechaDesde, DateTime fechaHasta) {


        //  Si no se recibe parametro fechaDesde, por default los últimos 30 días
        if (fechaDesde == null) fechaDesde = utilidad_fechas.obtenerCentral().AddDays(-30);

        // Si no se recibe parametro fechaHasta, se estable al día actual
        if (fechaHasta == null) fechaHasta = utilidad_fechas.obtenerCentral(); ;

        TimeSpan tsInicio = new TimeSpan(0, 0, 0);
        TimeSpan tsFinal = new TimeSpan(24, 59, 59);

        fechaDesde = fechaDesde.Date + tsInicio;
        fechaHasta = fechaHasta.Date + tsFinal;

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            StringBuilder sel = new StringBuilder();
            sel.Append("SELECT SUM(montoFacturado) AS MontoFacturado ,nombreVendedor as NombreVendedor FROM ReportesVentas_RegistrosFacturacionDiaADia ");
            sel.Append(" WHERE fechaFacturado BETWEEN @fechaDesde AND  @fechaHasta ");
            sel.Append(" GROUP BY nombreVendedor ORDER BY  MontoFacturado DESC");

            cmd.CommandText = sel.ToString();
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@fechaDesde", SqlDbType.DateTime);
            cmd.Parameters["@fechaDesde"].Value = fechaDesde;

            cmd.Parameters.Add("@fechaHasta", SqlDbType.DateTime);
            cmd.Parameters["@fechaHasta"].Value = fechaHasta;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);

            return ds.Tables[0];
        }


    }

    ///<summary>
    /// Recuperar últimos N cantidad de registros de los productos añadidos a la base de datos
    ///</summary>
    public static DataTable recuperarUltimosProductosRegistrados(int? cantidad, DateTime fechaDesde, DateTime fechaHasta) {
        if(cantidad == null) {
            cantidad = 10;
        }
      

        //  Si no se recibe parametro fechaDesde, por default los últimos 30 días
        if (fechaDesde == null) fechaDesde = utilidad_fechas.obtenerCentral().AddDays(-30);

        // Si no se recibe parametro fechaHasta, se estable al día actual
        if (fechaHasta == null) fechaHasta = utilidad_fechas.obtenerCentral(); ;

        TimeSpan tsInicio = new TimeSpan(0, 0, 0);
        TimeSpan tsFinal = new TimeSpan(24, 59, 59);

        fechaDesde = fechaDesde.Date + tsInicio;
        fechaHasta = fechaHasta.Date + tsFinal;

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            StringBuilder sel = new StringBuilder();
            sel.Append("SELECT TOP(@cantidad) numero_parte, montoFacturado, nombreVendedor, fechaFacturado FROM ReportesVentas_RegistrosFacturacionDiaADia ");
            sel.Append(" WHERE fechaFacturado BETWEEN @fechaDesde AND  @fechaHasta ");
            sel.Append("   ORDER BY montoFacturado DESC");

            cmd.CommandText = sel.ToString();
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@cantidad", SqlDbType.Int);
            cmd.Parameters["@cantidad"].Value = cantidad;

            cmd.Parameters.Add("@fechaDesde", SqlDbType.DateTime);
            cmd.Parameters["@fechaDesde"].Value = fechaDesde;

            cmd.Parameters.Add("@fechaHasta", SqlDbType.DateTime);
            cmd.Parameters["@fechaHasta"].Value = fechaHasta;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);

            return ds.Tables[0];
        }


    }
    /// <summary>
    /// Obtiene el monto total en un periodo especificado
    /// </summary>
    public static decimal montoFacturado(DateTime fechaDesde, DateTime fechaHasta) {

        //  Si no se recibe parametro fechaDesde, por default los últimos 30 días
        if (fechaDesde == null) fechaDesde = utilidad_fechas.obtenerCentral().AddDays(-30);

        // Si no se recibe parametro fechaHasta, se estable al día actual
        if (fechaHasta == null) fechaHasta = utilidad_fechas.obtenerCentral(); ;

        TimeSpan tsInicio = new TimeSpan(0, 0, 0);
        TimeSpan tsFinal = new TimeSpan(23, 59, 59);

        fechaDesde = fechaDesde.Date + tsInicio;
        fechaHasta = fechaHasta.Date + tsFinal;

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            StringBuilder sel = new StringBuilder();

            sel.Append(" SELECT SUM(montoFacturado) FROM ReportesVentas_RegistrosFacturacionDiaADia ");
            sel.Append(" WHERE fechaFacturado BETWEEN   @fechaDesde AND  @fechaHasta");


            cmd.CommandText = sel.ToString();
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@fechaDesde", SqlDbType.DateTime);
            cmd.Parameters["@fechaDesde"].Value = fechaDesde;

            cmd.Parameters.Add("@fechaHasta", SqlDbType.DateTime);
            cmd.Parameters["@fechaHasta"].Value = fechaHasta;

            con.Open();
            decimal montoFacturado;

            string _montoFacturado = cmd.ExecuteScalar().ToString();
            if(string.IsNullOrWhiteSpace(_montoFacturado)) {
                return 0;
            } else {
                  montoFacturado = decimal.Parse(_montoFacturado);
            }
          
         
            return montoFacturado;
        }

    }

    /// <summary>
    /// Obtiene el monto total en un periodo especificado
    /// </summary>
    public static decimal montoFacturado(DateTime fechaDesde, DateTime fechaHasta, string filtroDepartamento) {

        //  Si no se recibe parametro fechaDesde, por default los últimos 30 días
        if (fechaDesde == null) fechaDesde = utilidad_fechas.obtenerCentral().AddDays(-30);

        // Si no se recibe parametro fechaHasta, se estable al día actual
        if (fechaHasta == null) fechaHasta = utilidad_fechas.obtenerCentral(); ;

        TimeSpan tsInicio = new TimeSpan(0, 0, 0);
        TimeSpan tsFinal = new TimeSpan(23, 59, 59);

        fechaDesde = fechaDesde.Date + tsInicio;
        fechaHasta = fechaHasta.Date + tsFinal;

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            StringBuilder sel = new StringBuilder();

            sel.Append(" SELECT SUM(montoFacturado) FROM  (SELECT * FROM (SELECT *, ");
            sel.Append("(SELECT departamento FROM ReportesVentas_RelacionUsuariosSAP  as t ");
            sel.Append(" WHERE e.nombreVendedor = t.nombreVendedor) as departamento ");
            sel.Append(" FROM ReportesVentas_RegistrosFacturacionDiaADia as e) as tablaUnion)  as tFinal  ");
            sel.Append(" WHERE departamento = @departamento AND fechaFacturado BETWEEN   @fechaDesde AND  @fechaHasta");


            cmd.CommandText = sel.ToString();
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@fechaDesde", SqlDbType.DateTime);
            cmd.Parameters["@fechaDesde"].Value = fechaDesde;

            cmd.Parameters.Add("@fechaHasta", SqlDbType.DateTime);
            cmd.Parameters["@fechaHasta"].Value = fechaHasta;

            cmd.Parameters.Add("@departamento", SqlDbType.NVarChar,100);
            cmd.Parameters["@departamento"].Value = filtroDepartamento;
        
            con.Open();
            decimal montoFacturado;

            string _montoFacturado = cmd.ExecuteScalar().ToString();
            if (string.IsNullOrWhiteSpace(_montoFacturado)) {
                return 0;
            } else {
                montoFacturado = decimal.Parse(_montoFacturado);
            }


            return montoFacturado;
        }

    }

    /// <summary>
    /// Obtiene el monto meta facturado de un mes y año
    /// </summary>
    public static decimal montoMetaMes(int año, int mes) {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            StringBuilder sel = new StringBuilder();

            sel.Append(" SELECT montoProyeccion FROM ReportesVentas_ProyeccionFacturado ");
            sel.Append(" WHERE mes = @mes AND año = @año");


            cmd.CommandText = sel.ToString();
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@mes", SqlDbType.Int);
            cmd.Parameters["@mes"].Value = mes;

            cmd.Parameters.Add("@año", SqlDbType.Int);
            cmd.Parameters["@año"].Value = año;

            con.Open();
            decimal montoProyeccion = decimal.Parse(cmd.ExecuteScalar().ToString());

            return montoProyeccion;
        }

    }
   
    /// <summary>
    /// Obtiene días laboralesde un determinado mes y años
    /// </summary>
    public static string obtenerDiasLAborales(int año, int mes) {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            StringBuilder sel = new StringBuilder();

            sel.Append(" SELECT diasLaborales FROM ReportesVentas_DiasLaborales ");
            sel.Append(" WHERE mes = @mes AND año = @año");


            cmd.CommandText = sel.ToString();
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@mes", SqlDbType.Int);
            cmd.Parameters["@mes"].Value = mes;

            cmd.Parameters.Add("@año", SqlDbType.Int);
            cmd.Parameters["@año"].Value = año;

            con.Open();

            string diasLaborales =  cmd.ExecuteScalar().ToString();

            return diasLaborales;
        }

    }

    /// <summary>
    /// Obtiene el monto total en un periodo especificado
    /// </summary>
    public static decimal montoPedidosIngresados(DateTime fechaDesde, DateTime fechaHasta) {

        //  Si no se recibe parametro fechaDesde, por default los últimos 30 días
        if (fechaDesde == null) fechaDesde = utilidad_fechas.obtenerCentral().AddDays(-30);

        // Si no se recibe parametro fechaHasta, se estable al día actual
        if (fechaHasta == null) fechaHasta = utilidad_fechas.obtenerCentral(); ;

        TimeSpan tsInicio = new TimeSpan(0, 0, 0);
        TimeSpan tsFinal = new TimeSpan(23, 59, 59);

        fechaDesde = fechaDesde.Date + tsInicio;
        fechaHasta = fechaHasta.Date + tsFinal;

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            StringBuilder sel = new StringBuilder();

            sel.Append(" SELECT SUM(montoPedido) FROM ReportesVentas_RegistrosPedidosDiaADia ");
            sel.Append(" WHERE fechaPedido BETWEEN   @fechaDesde AND  @fechaHasta");


            cmd.CommandText = sel.ToString();
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@fechaDesde", SqlDbType.DateTime);
            cmd.Parameters["@fechaDesde"].Value = fechaDesde;

            cmd.Parameters.Add("@fechaHasta", SqlDbType.DateTime);
            cmd.Parameters["@fechaHasta"].Value = fechaHasta;

            con.Open();
            decimal montoFacturado;

            string _montoFacturado = cmd.ExecuteScalar().ToString();
            if (string.IsNullOrWhiteSpace(_montoFacturado)) {
                return 0;
            } else {
                montoFacturado = decimal.Parse(_montoFacturado);
            }


            return montoFacturado;
        }

    }
}
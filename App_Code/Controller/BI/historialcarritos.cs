using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// CRUD y administrador de los historiales carritos
/// </summary>
public class historialCarritos {
    public int id { get; set; }
    public string usuario { get; set; }
    public string agregado_por { get; set; }
    public DateTime fecha_creacion { get; set; }
    public string numero_parte { get; set; }
    public string moneda { get; set; }
    public decimal tipo_cambio { get; set; }
    public string unidad { get; set; }
    public decimal precio_unitario { get; set; }
    public decimal cantidad { get; set; }
    public decimal precio_total { get; set; }
    public float? stock1 { get; set; }
    public DateTime? stock1_fecha { get; set; }

    int idAccion { get; set; }

    /// <summary>
    /// Obtener el historial del carrito de un usuario
    /// </summary>
    public List<historialCarritos> obtenerHistorialCarrito(string usuario ) {

        SqlDataReader dr;
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {
            StringBuilder sel = new StringBuilder();
            sel.Append("SELECT * FROM historial_carritos WHERE usuario=@usuario");

            string query = sel.ToString();
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 60);
            cmd.Parameters["@usuario"].Value = usuario;



            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows) {

                List<historialCarritos> listadoProductos = new List<historialCarritos>();
               
                while (dr.Read()) {

                    historialCarritos carrito = new historialCarritos();

                    carrito.id = int.Parse(dr["id"].ToString());
                    carrito.usuario = dr["usuario"].ToString();
                    carrito.agregado_por = dr["agregado_por"].ToString();
                    carrito.fecha_creacion = DateTime.Parse(dr["fecha_creacion"].ToString());
                    carrito.numero_parte = dr["numero_parte"].ToString();
                    carrito.moneda = dr["moneda"].ToString();
                    carrito.tipo_cambio = decimal.Parse(dr["tipo_cambio"].ToString());
                    carrito.precio_unitario = decimal.Parse(dr["precio_unitario"].ToString());
                    carrito.cantidad = decimal.Parse(dr["cantidad"].ToString());
                    carrito.precio_total = decimal.Parse(dr["precio_total"].ToString());
                    carrito.stock1 = textTools.nullableParse(dr["stock1"]);
                    carrito.stock1_fecha = textTools.nullableParse(dr["stock1_fecha"]);

                    carrito.idAccion = textTools.nullableParse(dr["idAccion"]);

                    listadoProductos.Add(carrito);
                }

                return listadoProductos;
            } else {  return null; }
        }
      
    }
    
    protected void agregarProductoHistorial(string usuario)
    {
        StringBuilder query = new StringBuilder();

        query.Append("SET LANGUAGE English; INSERT INTO historial_carritos  ");
        query.Append("(usuario, agregado_por, fecha_creacion, numero_parte, moneda, tipo_cambio, unidad, precio_unitario ");
        query.Append("cantidad, precio_total, stock1, stock1_fecha ) ");

        query.Append(" VALUES (@usuario, @agregado_por, @fecha_creacion, @numero_parte, @moneda, @tipo_cambio, @unidad, @precio_unitariod ");
        query.Append("@cantidad, @precio_total, @stock1, @stock1_fecha )  ");

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {  

            cmd.CommandText = query.ToString(); 
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 50);
            cmd.Parameters["@usuario"].Value = usuario;

            cmd.Parameters.Add("@agregado_por", SqlDbType.NVarChar,60);
            cmd.Parameters["@agregado_por"].Value = agregado_por;

            cmd.Parameters.Add("@fecha_creacion", SqlDbType.DateTime);
            cmd.Parameters["@fecha_creacion"].Value = fecha_creacion;


            cmd.Parameters.Add("@numero_parte", SqlDbType.NVarChar, 50);
            cmd.Parameters["@numero_parte"].Value = numero_parte;

            cmd.Parameters.Add("@moneda", SqlDbType.NVarChar, 5);
            cmd.Parameters["@moneda"].Value = moneda;

            cmd.Parameters.Add("@tipo_cambio", SqlDbType.Money);
            cmd.Parameters["@tipo_cambio"].Value = tipo_cambio;


            cmd.Parameters.Add("@unidad", SqlDbType.NVarChar, 25);
            cmd.Parameters["@unidad"].Value = unidad;

            cmd.Parameters.Add("@precio_unitario", SqlDbType.Money);
            cmd.Parameters["@precio_unitario"].Value = precio_unitario;


            cmd.Parameters.Add("@cantidad", SqlDbType.Money);
            cmd.Parameters["@cantidad"].Value = cantidad;

            cmd.Parameters.Add("@precio_total", SqlDbType.Money);
            cmd.Parameters["@precio_total"].Value = precio_total;


            cmd.Parameters.Add("@stock1", SqlDbType.Float);
            cmd.Parameters["@stock1"].Value = stock1;

            cmd.Parameters.Add("@stock1_fecha", SqlDbType.DateTime);
            cmd.Parameters["@stock1_fecha"].Value = stock1_fecha;

            con.Open();

            cmd.ExecuteNonQuery();
        }

    }

  
    }
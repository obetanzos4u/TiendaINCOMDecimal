using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Aplica un porcentaje del descuento sobre el subtotal de una cotización o pedido.
/// </summary>
public class operacionesDescuentos
    {
    private string resultadoMensaje;
    private bool resultado;
    
    public decimal descuento { get; set; }
    public string numero_operacion { get; set; }
    public eTipo_operacion tipo_operacion { get; set; }
    public enum eTipo_operacion
        {
        cotización, pedido 
        }
    public operacionesDescuentos(decimal _descuento, string _numero_operacion, eTipo_operacion _tipo_operacion) {

        descuento = _descuento;
        numero_operacion = _numero_operacion;
        tipo_operacion = _tipo_operacion;

        }

    public void aplicarDescuento() {

        if (descuento  <= 0 || descuento > 100) {
            resultadoMensaje = "El rango de descuento debe ser mayor que 0 y menor que 100";
            resultado = false; 
            throw new System.ArgumentException(resultadoMensaje, "descuento");
            }
        if (string.IsNullOrWhiteSpace(numero_operacion)) {
            resultadoMensaje ="El número de operación es obligatorio";
            resultado = false;
            throw new System.ArgumentException(resultadoMensaje,"numero_operacion");
            
            }



        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {

            cmd.CommandText ="UPDATE subtotal";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@numero_operacion", SqlDbType.NVarChar, 100);
            cmd.Parameters["@numero_operacion"].Value = numero_operacion ;

            cmd.Parameters.Add("@descuento", SqlDbType.SmallMoney);
            cmd.Parameters["@descuento"].Value = descuento;


            con.Open();

            cmd.ExecuteNonQuery();
            }
        }
  

    }


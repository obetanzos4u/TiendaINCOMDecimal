using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de usuarios
/// </summary>
public class direccionesFacturacion : model_direccionesFacturacion
{
    private SqlConnection con { get; set; }
    private SqlCommand cmd { get; set; }
    private SqlDataAdapter da { get; set; }
    private DataSet ds { get; set; }
    private DataTable dt { get; set; }



    protected void dbConexion()
    {

        con = new SqlConnection(conexiones.conexionTienda());
        cmd = new SqlCommand();
        cmd.Connection = con;
        
    }
    ///<summary>
    /// Devuelve un DT con las direcciones de facturación de un cliente
    ///</summary>
    public DataTable obtenerDirecciones(int id_cliente)
    {

       
        dbConexion();
        using (con)
        {

            StringBuilder sel = new StringBuilder();

            sel.Append("SELECT ");
            sel.Append(" * ");
            sel.Append(" FROM direcciones_facturacion WHERE id_cliente = @id_cliente ORDER BY razon_social ASC; ");

            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@id_cliente", SqlDbType.Int);
            cmd.Parameters["@id_cliente"].Value = id_cliente;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            return ds.Tables[0];
        }
           

        }

    ///<summary>
    /// Crear una dirección de facturación para un ID Cliente
    ///</summary>
    public string crearDireccion(int id_cliente)
    {

        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; INSERT INTO direcciones_facturacion ");
                query.Append(" (id_cliente, nombre_direccion, razon_social, rfc, calle, numero, colonia, delegacion_municipio, estado, codigo_postal, pais) ");
                query.Append(" VALUES (@id_cliente, @nombre_direccion, @razon_social, @rfc, @calle, @numero, @colonia, @delegacion_municipio, @estado, @codigo_postal, @pais)  ");
             

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@id_cliente", SqlDbType.Int);
                cmd.Parameters["@id_cliente"].Value = id_cliente;

                cmd.Parameters.Add("@nombre_direccion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@nombre_direccion"].Value = nombre_direccion.Trim(' ');

                cmd.Parameters.Add("@razon_social", SqlDbType.NVarChar, 150);
                cmd.Parameters["@razon_social"].Value = razon_social.Trim(' ').ToUpper();

                cmd.Parameters.Add("@rfc", SqlDbType.NVarChar, 15);
                cmd.Parameters["@rfc"].Value = rfc.Trim(' ').Replace(" ","").ToUpper();

                cmd.Parameters.Add("@calle", SqlDbType.NVarChar, 50);
                cmd.Parameters["@calle"].Value = calle.Trim(' ').Replace("\t", "");

                cmd.Parameters.Add("@numero", SqlDbType.NVarChar, 30);
                cmd.Parameters["@numero"].Value = numero.Trim(' ');

                cmd.Parameters.Add("@colonia", SqlDbType.NVarChar, 35);
                cmd.Parameters["@colonia"].Value = colonia.Trim(' ');

                cmd.Parameters.Add("@delegacion_municipio", SqlDbType.NVarChar, 35);
                cmd.Parameters["@delegacion_municipio"].Value = delegacion_municipio.Trim(' ');

                cmd.Parameters.Add("@estado", SqlDbType.NVarChar, 35);
                cmd.Parameters["@estado"].Value = estado.Trim(' ');

                cmd.Parameters.Add("@codigo_postal", SqlDbType.NVarChar, 15);
                cmd.Parameters["@codigo_postal"].Value = codigo_postal.Trim(' ');

                cmd.Parameters.Add("@pais", SqlDbType.NVarChar, 35);
                cmd.Parameters["@pais"].Value = pais;

                
                con.Open();

                cmd.ExecuteNonQuery();
                return "";
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Crear dirección de facturación", ex);
            return null;
        }
    }

    ///<summary>
    ///Actualiza una dirección de facturación con objeto creado
    ///</summary>
    public string actualizarDireccion()
    {
      
            try
            {
                dbConexion();
                using (con)
                {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; UPDATE direcciones_facturacion ");
                query.Append(" SET nombre_direccion=@nombre_direccion, razon_social=@razon_social, rfc=@rfc, calle = @calle, numero = @numero, colonia = @colonia, delegacion_municipio = @delegacion_municipio, estado = @estado, ");

                query.Append("  codigo_postal=@codigo_postal, pais = @pais ");
                query.Append(" WHERE id = @id");
             
                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = id;

                cmd.Parameters.Add("@nombre_direccion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@nombre_direccion"].Value = nombre_direccion.Trim(' ');

                cmd.Parameters.Add("@razon_social", SqlDbType.NVarChar, 150);
                cmd.Parameters["@razon_social"].Value = razon_social.Trim(' ').ToUpper();

                cmd.Parameters.Add("@rfc", SqlDbType.NVarChar, 15);
                cmd.Parameters["@rfc"].Value = rfc.Trim(' ').Replace(" ", "").ToUpper();

                cmd.Parameters.Add("@calle", SqlDbType.NVarChar, 50);
                cmd.Parameters["@calle"].Value = calle.Trim(' ');

                cmd.Parameters.Add("@numero", SqlDbType.NVarChar, 30);
                cmd.Parameters["@numero"].Value = numero.Trim(' ');

                cmd.Parameters.Add("@colonia", SqlDbType.NVarChar, 35);
                cmd.Parameters["@colonia"].Value = colonia.Trim(' ');

                cmd.Parameters.Add("@delegacion_municipio", SqlDbType.NVarChar, 35);
                cmd.Parameters["@delegacion_municipio"].Value = delegacion_municipio.Trim(' ');

                cmd.Parameters.Add("@estado", SqlDbType.NVarChar, 35);
                cmd.Parameters["@estado"].Value = estado;

                cmd.Parameters.Add("@codigo_postal", SqlDbType.NVarChar, 15);
                cmd.Parameters["@codigo_postal"].Value = codigo_postal;

                cmd.Parameters.Add("@pais", SqlDbType.NVarChar, 35);
                cmd.Parameters["@pais"].Value = pais;

                con.Open();

                    byte resultado = Convert.ToByte(cmd.ExecuteNonQuery());
                    return "";
                }
            }
            catch (Exception ex)
            {
                devNotificaciones.error("Actualizar dirección de facturación", ex);
                return null;
            }
        }

    ///<summary>
    /// Recibe el ID de la dirección de facturación a eliminar
    ///</summary>
    public bool eliminardireccion(int id_sql_direccion)
    {

        try
        {
            dbConexion();
            using (con)
            {
                string query = @"SET LANGUAGE English; DELETE FROM direcciones_facturacion WHERE id=@id_sql_direccion;";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;



                cmd.Parameters.Add("@id_sql_direccion", SqlDbType.Int);
                cmd.Parameters["@id_sql_direccion"].Value = id_sql_direccion;

                con.Open();

                cmd.ExecuteNonQuery();
                return true;
            }
        }
        catch(Exception ex)
        {
            devNotificaciones.error("Eliminar Dirección de facturación", ex);
            return false;
        }
           

    }
    ///<summary>
    /// Devuelve un tipo "direccionesEnvio" con las direcciones de envío de un cliente
    ///</summary>
    public direccionesFacturacion obtenerDireccion(int id) {

        DataTable dtDireccion = new DataTable();
        dbConexion();
        using (con) {

            StringBuilder sel = new StringBuilder();

            sel.Append("SELECT ");
            sel.Append(" * ");
            sel.Append(" FROM direcciones_facturacion WHERE id = @id;");

            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = id;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            dtDireccion = ds.Tables[0];
            }

        direccionesFacturacion direccion = new direccionesFacturacion();

        direccion.id = int.Parse(dtDireccion.Rows[0]["id"].ToString());
        direccion.id_cliente = int.Parse(dtDireccion.Rows[0]["id_cliente"].ToString());
        direccion.nombre_direccion = dtDireccion.Rows[0]["nombre_direccion"].ToString();

        direccion.razon_social = dtDireccion.Rows[0]["razon_social"].ToString();
        direccion.rfc = dtDireccion.Rows[0]["rfc"].ToString();

        direccion.calle = dtDireccion.Rows[0]["calle"].ToString();
        direccion.numero = dtDireccion.Rows[0]["numero"].ToString();
        direccion.colonia = dtDireccion.Rows[0]["colonia"].ToString();
        direccion.delegacion_municipio = dtDireccion.Rows[0]["delegacion_municipio"].ToString();
        direccion.estado = dtDireccion.Rows[0]["estado"].ToString();
        direccion.codigo_postal = dtDireccion.Rows[0]["codigo_postal"].ToString();
        direccion.pais = dtDireccion.Rows[0]["pais"].ToString();
      
        return direccion;
        }

    }
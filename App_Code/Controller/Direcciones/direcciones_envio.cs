using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de direccionesEnvio
/// </summary>


 [ObsoleteAttribute("Se recomienda usar la clase [direcciones_envio_EF] que usa EF", false)]
public class direccionesEnvio: model_direccionesEnvio
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
    /// Devuelve un DT con las direcciones de envío de un cliente
    ///</summary>
    public DataTable obtenerDirecciones(int id_cliente)
    {

       
        dbConexion();
        using (con)
        {

            StringBuilder sel = new StringBuilder();

            sel.Append("SELECT ");
            sel.Append(" * ");
            sel.Append(" FROM direcciones_envio WHERE id_cliente = @id_cliente;");

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
    /// Devuelve un tipo "direccionesEnvio" con las direcciones de envío de un cliente
    ///</summary>
    public direccionesEnvio obtenerDireccion(int id) {

        DataTable dtDireccion = new DataTable();
        dbConexion();
        using (con) {

            StringBuilder sel = new StringBuilder();

            sel.Append("SELECT ");
            sel.Append(" * ");
            sel.Append(" FROM direcciones_envio WHERE id = @id;");

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
        direccionesEnvio direccion = new direccionesEnvio();

        direccion.id =  int.Parse(dtDireccion.Rows[0]["id"].ToString());
        direccion.id_cliente = int.Parse(dtDireccion.Rows[0]["id_cliente"].ToString());
        direccion.nombre_direccion = dtDireccion.Rows[0]["nombre_direccion"].ToString();
        direccion.calle = dtDireccion.Rows[0]["calle"].ToString();
        direccion.numero = dtDireccion.Rows[0]["numero"].ToString();
        direccion.colonia = dtDireccion.Rows[0]["colonia"].ToString();
        direccion.delegacion_municipio = dtDireccion.Rows[0]["delegacion_municipio"].ToString();
        direccion.ciudad = dtDireccion.Rows[0]["ciudad"].ToString();
        direccion.estado = dtDireccion.Rows[0]["estado"].ToString();
        direccion.codigo_postal = dtDireccion.Rows[0]["codigo_postal"].ToString();
        direccion.pais = dtDireccion.Rows[0]["pais"].ToString();
        direccion.referencias = dtDireccion.Rows[0]["referencias"].ToString();

        return direccion;
        }

    ///<summary>
    /// Devuelve un tipo "direccionesEnvio" con las direcciones de envío de un cliente
    ///</summary>
    public static direccionesEnvio obtenerDireccionStatic(int id)
    {
        try { 
        DataTable dtDireccion = new DataTable();
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        using (con)
        {

            StringBuilder sel = new StringBuilder();

            sel.Append("SELECT ");
            sel.Append(" * ");
            sel.Append(" FROM direcciones_envio WHERE id = @id;");

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
        direccionesEnvio direccion = new direccionesEnvio();

        direccion.id = int.Parse(dtDireccion.Rows[0]["id"].ToString());
        direccion.id_cliente = int.Parse(dtDireccion.Rows[0]["id_cliente"].ToString());
        direccion.nombre_direccion = dtDireccion.Rows[0]["nombre_direccion"].ToString();
        direccion.calle = dtDireccion.Rows[0]["calle"].ToString();
        direccion.numero = dtDireccion.Rows[0]["numero"].ToString();
        direccion.colonia = dtDireccion.Rows[0]["colonia"].ToString();
        direccion.delegacion_municipio = dtDireccion.Rows[0]["delegacion_municipio"].ToString();
            direccion.ciudad = dtDireccion.Rows[0]["ciudad"].ToString();
            direccion.estado = dtDireccion.Rows[0]["estado"].ToString();
        direccion.codigo_postal = dtDireccion.Rows[0]["codigo_postal"].ToString();
        direccion.pais = dtDireccion.Rows[0]["pais"].ToString();
        direccion.referencias = dtDireccion.Rows[0]["referencias"].ToString();

        return direccion;
        }
        catch(Exception ex)
        {
            return null;
        }
    }



    ///<summary>
    /// Crear una dirección de envío para un ID Cliente
    ///</summary>
    public string crearDireccion(int id_cliente)
    {

        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; INSERT INTO direcciones_envio  ");
                query.Append(" (id_cliente, nombre_direccion, calle, numero, colonia, delegacion_municipio, ciudad, estado, codigo_postal, pais, referencias) ");
                query.Append(" VALUES (@id_cliente, @nombre_direccion, @calle, @numero, @colonia, @delegacion_municipio, @ciudad, @estado, @codigo_postal, @pais, @referencias)  ");
             

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;
                

                cmd.Parameters.Add("@id_cliente", SqlDbType.Int);
                cmd.Parameters["@id_cliente"].Value = id_cliente;

                cmd.Parameters.Add("@nombre_direccion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@nombre_direccion"].Value = textTools.lineSimple(nombre_direccion);

                cmd.Parameters.Add("@calle", SqlDbType.NVarChar, 50);
                cmd.Parameters["@calle"].Value = textTools.lineSimple(calle);

                cmd.Parameters.Add("@numero", SqlDbType.NVarChar, 30);
                cmd.Parameters["@numero"].Value = textTools.lineSimple(numero);

                cmd.Parameters.Add("@colonia", SqlDbType.NVarChar, 70);
                cmd.Parameters["@colonia"].Value = textTools.lineSimple(colonia);

                cmd.Parameters.Add("@delegacion_municipio", SqlDbType.NVarChar, 50);
                cmd.Parameters["@delegacion_municipio"].Value = textTools.lineSimple(delegacion_municipio);



                cmd.Parameters.Add("@ciudad", SqlDbType.NVarChar, 60);
                if (string.IsNullOrWhiteSpace(ciudad)) cmd.Parameters["@ciudad"].Value = DBNull.Value;
                else cmd.Parameters["@ciudad"].Value = textTools.lineSimple(ciudad);


 

                cmd.Parameters.Add("@estado", SqlDbType.NVarChar, 35);
                cmd.Parameters["@estado"].Value = textTools.lineSimple(estado);

                cmd.Parameters.Add("@codigo_postal", SqlDbType.NVarChar, 15);
                cmd.Parameters["@codigo_postal"].Value = textTools.lineSimple(codigo_postal);

                cmd.Parameters.Add("@pais", SqlDbType.NVarChar, 35);
                cmd.Parameters["@pais"].Value = pais;

                cmd.Parameters.Add("@referencias", SqlDbType.NVarChar, 100);
                cmd.Parameters["@referencias"].Value = textTools.lineSimple(referencias);
                
                con.Open();

                cmd.ExecuteNonQuery();
                return "";
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Crear dirección de envío", ex);
            return null;
        }
    }

    ///<summary>
    ///Actualiza una dirección de envío con objeto creado
    ///</summary>
    public string actualizarDireccion()
    {
      
            try
            {
                dbConexion();
                using (con)
                {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; UPDATE direcciones_envio ");
                query.Append(" SET nombre_direccion=@nombre_direccion, calle = @calle, numero = @numero, colonia = @colonia, ");
                query.Append(" delegacion_municipio = @delegacion_municipio,  ciudad = @ciudad, estado = @estado, ");

                query.Append("  codigo_postal=@codigo_postal, pais = @pais, referencias = @referencias ");
                query.Append(" WHERE id = @id");
             
                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = id;

                cmd.Parameters.Add("@nombre_direccion", SqlDbType.NVarChar, 20);
                cmd.Parameters["@nombre_direccion"].Value = textTools.lineSimple(nombre_direccion);

                cmd.Parameters.Add("@calle", SqlDbType.NVarChar, 50);
                cmd.Parameters["@calle"].Value = textTools.lineSimple(calle);

                cmd.Parameters.Add("@numero", SqlDbType.NVarChar, 30);
                cmd.Parameters["@numero"].Value = textTools.lineSimple(numero);

                cmd.Parameters.Add("@colonia", SqlDbType.NVarChar, 70);
                cmd.Parameters["@colonia"].Value = textTools.lineSimple(colonia);

                cmd.Parameters.Add("@delegacion_municipio", SqlDbType.NVarChar, 50);
                cmd.Parameters["@delegacion_municipio"].Value = textTools.lineSimple(delegacion_municipio);

                cmd.Parameters.Add("@ciudad", SqlDbType.NVarChar, 60);
                cmd.Parameters["@ciudad"].Value = textTools.lineSimple(ciudad);

                cmd.Parameters.Add("@estado", SqlDbType.NVarChar, 35);
                cmd.Parameters["@estado"].Value = textTools.lineSimple(estado);

                cmd.Parameters.Add("@codigo_postal", SqlDbType.NVarChar, 15);
                cmd.Parameters["@codigo_postal"].Value = textTools.lineSimple(codigo_postal);

                cmd.Parameters.Add("@pais", SqlDbType.NVarChar, 35);
                cmd.Parameters["@pais"].Value = textTools.lineSimple(pais);

                cmd.Parameters.Add("@referencias", SqlDbType.NVarChar, 100);
                cmd.Parameters["@referencias"].Value = textTools.lineSimple(referencias);

                con.Open();

                    byte resultado = Convert.ToByte(cmd.ExecuteNonQuery());
                    return "";
                }
            }
            catch (Exception ex)
            {
                devNotificaciones.error("Actualizar dirección de envío", ex);
                return null;
            }
        }

    ///<summary>
    /// Recibe el ID de la dirección de envío a eliminar
    ///</summary>
    public bool eliminardireccion(int id_contacto)
    {

        try
        {
            dbConexion();
            using (con)
            {
                string query = @"SET LANGUAGE English; 
                                     DELETE FROM direcciones_envio WHERE id=@id_contacto;";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;



                cmd.Parameters.Add("@id_contacto", SqlDbType.Int);
                cmd.Parameters["@id_contacto"].Value = id_contacto;

                con.Open();

                cmd.ExecuteNonQuery();
                return true;
            }
        }
        catch(Exception ex)
        {
            devNotificaciones.error("Eliminar Dirección de envío", ex);
            return false;
        }
           

    }

    ///<summary>
    /// 20201014 RC - Establece la dirección de envío preferida de un determinado cliente
    ///</summary>
    public static void EstablerDirecciónEnvioPredeterminada(int id_cliente, int idDireccionEnvio)
    {

        try
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conexiones.conexionTienda());
            cmd.Connection = con;

            using (con)
            {

                StringBuilder query = new StringBuilder();

                // Establecemos todas las direcciones en 0 en el campo [direccion_preferida]
                query.Append("UPDATE  direcciones_envio  SET direccion_predeterminada = 0 WHERE id_cliente = @id_cliente ");

                // Actualizamos la dirección preferida en valor 1 en el campo [direccion_preferida]
                query.Append("UPDATE direcciones_envio SET direccion_predeterminada = 1  WHERE id = @idDireccionEnvio AND id_cliente = @id_cliente");


                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.Add("@idDireccionEnvio", SqlDbType.Int);
                cmd.Parameters["@idDireccionEnvio"].Value = idDireccionEnvio;

                cmd.Parameters.Add("@id_cliente", SqlDbType.Int);
                cmd.Parameters["@id_cliente"].Value = id_cliente;

                con.Open();

                 cmd.ExecuteNonQuery();
               
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Actualizar dirección de envío", ex);
          
        }
    }
}
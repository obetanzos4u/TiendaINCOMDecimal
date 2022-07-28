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
public class contactos : model_contactos
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
    /// Obtiene los contactos asociados a un cliente
    ///</summary>
    public DataTable obtenerContactos(int id_cliente)
    {

       
        dbConexion();
        using (con)
        {

            StringBuilder sel = new StringBuilder();

            sel.Append("SELECT ");

            sel.Append(" * ");

            sel.Append(" FROM contactos WHERE id_cliente = @id_cliente ORDER BY nombre ASC; ");

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
    public contactos obtenerContacto(int id) {

        DataTable dt = new DataTable();
        dbConexion();
        using (con) {

            StringBuilder sel = new StringBuilder();

            sel.Append("SELECT ");

            // Campos básicos
            sel.Append(" * ");


            sel.Append(" FROM contactos WHERE id= @id;");

            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@id_cliente", SqlDbType.Int);
            cmd.Parameters["@id_cliente"].Value = id_cliente;

            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = id;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
             dt = ds.Tables[0];

            }

        contactos contacto = new contactos();
        contacto.id = int.Parse(dt.Rows[0]["id"].ToString());
        if (dt.Rows[0]["id_cliente"] != DBNull.Value) contacto.id_cliente = int.Parse(dt.Rows[0]["id_cliente"].ToString());

        contacto.nombre = dt.Rows[0]["nombre"].ToString();
        contacto.apellido_paterno = dt.Rows[0]["apellido_paterno"].ToString();
        contacto.apellido_materno = dt.Rows[0]["apellido_materno"].ToString();
        contacto.email = dt.Rows[0]["email"].ToString();
        contacto.telefono = dt.Rows[0]["telefono"].ToString();
        contacto.celular = dt.Rows[0]["celular"].ToString();
        return contacto;

        }
    public DataTable obtenerContactos(int id_cliente, int id) {


        dbConexion();
        using (con) {

            StringBuilder sel = new StringBuilder();

            sel.Append("SELECT ");

            // Campos básicos
            sel.Append(" * ");


            sel.Append(" FROM contactos WHERE id_cliente = @id_cliente AND id= @id ORDER BY nombre ASC;");

            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@id_cliente", SqlDbType.Int);
            cmd.Parameters["@id_cliente"].Value = id_cliente;

            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = id;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);
            return ds.Tables[0];
            }


        }
    public string crearContacto()
    {

        try
        {
            dbConexion();
            using (con)
            {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; INSERT INTO contactos  ");
                query.Append(" (id_cliente, nombre, apellido_paterno, apellido_materno, email, telefono, celular) ");
                query.Append(" VALUES (@id_cliente, @nombre, @apellido_paterno,  @apellido_materno, @email, @telefono, @celular)  ");
               

                cmd.CommandText = query.ToString();
                cmd.CommandType = CommandType.Text;


                cmd.Parameters.Add("@id_cliente", SqlDbType.Int);
                cmd.Parameters["@id_cliente"].Value = id_cliente;

                cmd.Parameters.Add("@nombre", SqlDbType.NVarChar, 20);
                cmd.Parameters["@nombre"].Value = nombre.Trim(' ');

                cmd.Parameters.Add("@apellido_paterno", SqlDbType.NVarChar, 20);
                cmd.Parameters["@apellido_paterno"].Value = apellido_paterno.Trim(' ');

                cmd.Parameters.Add("@apellido_materno", SqlDbType.NVarChar, 20);
                cmd.Parameters["@apellido_materno"].Value = apellido_materno.Trim(' ');

                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 60);
                cmd.Parameters["@email"].Value = email.Trim(' ');

                cmd.Parameters.Add("@telefono", SqlDbType.NVarChar, 50);
                cmd.Parameters["@telefono"].Value = telefono;

                cmd.Parameters.Add("@celular", SqlDbType.NVarChar, 50);
                cmd.Parameters["@celular"].Value = celular;

                con.Open();

                cmd.ExecuteNonQuery();
                return "";
            }
        }
        catch (Exception ex)
        {
            devNotificaciones.error("Actualizar Contacto", ex);
            return null;
        }
    }

    public string actualizarContacto()
    {
      
            try
            {
                dbConexion();
                using (con)
                {

                StringBuilder query = new StringBuilder();

                query.Append("SET LANGUAGE English; UPDATE contactos  ");
                query.Append(" SET nombre = @nombre, apellido_paterno = @apellido_paterno, apellido_materno = @apellido_materno,  ");

                query.Append(" email = @email, telefono = @telefono, celular=@celular ");
                query.Append(" WHERE id = @id");
             
                    cmd.CommandText = query.ToString();
                    cmd.CommandType = CommandType.Text;


                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = id;

                cmd.Parameters.Add("@nombre", SqlDbType.NVarChar, 20);
                    cmd.Parameters["@nombre"].Value = textTools.lineSimple(nombre.Trim(' '));

                    cmd.Parameters.Add("@apellido_paterno", SqlDbType.NVarChar, 20);
                    cmd.Parameters["@apellido_paterno"].Value = textTools.lineSimple(apellido_paterno.Trim(' '));

                    cmd.Parameters.Add("@apellido_materno", SqlDbType.NVarChar, 20);
                    cmd.Parameters["@apellido_materno"].Value = textTools.lineSimple(apellido_materno.Trim(' '));

                    cmd.Parameters.Add("@email", SqlDbType.NVarChar, 60);
                    cmd.Parameters["@email"].Value = textTools.lineSimple(email.Trim(' '));

                    cmd.Parameters.Add("@telefono", SqlDbType.NVarChar, 50);
                    cmd.Parameters["@telefono"].Value = textTools.lineSimple(telefono);

                    cmd.Parameters.Add("@celular", SqlDbType.NVarChar, 50);
                    cmd.Parameters["@celular"].Value = celular;

                    con.Open();

                    byte resultado = Convert.ToByte(cmd.ExecuteNonQuery());
                    return "";
                }
            }
            catch (Exception ex)
            {
                devNotificaciones.error("Actualizar Contacto", ex);
                return null;
            }
        }

    ///<summary>
    /// Recibe el ID del contacto a eliminar
    ///</summary>
    public bool eliminarContacto(int id_contacto)
    {

        try
        {
            dbConexion();
            using (con)
            {
                string query = @"SET LANGUAGE English; 
                                     DELETE FROM contactos WHERE id=@id_contacto;";
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
            devNotificaciones.error("Eliminar Contacto", ex);
            return false;
        }
           

    }


}
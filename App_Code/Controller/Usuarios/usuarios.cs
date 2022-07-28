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
[Serializable()]
public partial class usuarios : model_usuarios
{
    ///<summary>
    /// Retorna [HttpContext.Current.User.Identity.Name
    ///</summary>
    static public string userLoginName() {

        return HttpContext.Current.User.Identity.Name;
            }
    ///<summary>
    /// Registra un usuario en el sistema
    ///</summary>
    public bool crear_usuarioRegistro()
    {
     if(validar_Existencia_Usuario(email) == 0) { 
        try
        {
                seguridad cifrar = new seguridad();
                SqlCommand cmd = new SqlCommand();
                SqlConnection con = new SqlConnection(conexiones.conexionTienda());
                cmd.Connection = con;

                using (con)
            {
                string query = @"SET LANGUAGE English; 
                INSERT INTO usuarios  (nombre, apellido_paterno, apellido_materno, email, password, tipo_de_usuario, rango, 
                                        grupo_asesores_adicional) 
                              VALUES (@nombre, @apellido_paterno, @apellido_materno, @email, @password, @tipo_de_usuario, @rango, 
                                       @grupo_asesores_adicional);";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@nombre", SqlDbType.NVarChar, 100);
                cmd.Parameters["@nombre"].Value = textTools.lineSimple(nombre);

                cmd.Parameters.Add("@apellido_paterno", SqlDbType.NVarChar, 20);
                cmd.Parameters["@apellido_paterno"].Value = textTools.lineSimple(apellido_paterno);

                cmd.Parameters.Add("@apellido_materno", SqlDbType.NVarChar, 20);
                cmd.Parameters["@apellido_materno"].Value = textTools.lineSimple(apellido_materno);

                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 60);
                cmd.Parameters["@email"].Value = textTools.lineSimple(email);

                cmd.Parameters.Add("@password", SqlDbType.NVarChar, 100);
                cmd.Parameters["@password"].Value = cifrar.passwordUser(password);

                cmd.Parameters.Add("@tipo_de_usuario", SqlDbType.NVarChar, 100);
                cmd.Parameters["@tipo_de_usuario"].Value = tipo_de_usuario;

                cmd.Parameters.Add("@rango", SqlDbType.Int);
                cmd.Parameters["@rango"].Value = rango;

                  cmd.Parameters.Add("@grupo_asesores_adicional", SqlDbType.NVarChar);
                 cmd.Parameters["@grupo_asesores_adicional"].Value = grupo_asesores_adicional[0];

                    con.Open();

                    byte resultado = Convert.ToByte(cmd.ExecuteNonQuery());
                    return true;
                }
        }
        catch (Exception ex)
        {
                devNotificaciones.error("Registrar, crear usuario DB", ex);
                return false;
        }
    } return false;

    }
    ///<summary>
    /// Registra un usuario en el sistema
    ///</summary>
    ///
    public bool crear_usuarioRegistroPorAsesor() {
        if (validar_Existencia_Usuario(email) == 0) {
            try {
                seguridad cifrar = new seguridad();
                SqlCommand cmd = new SqlCommand();
                SqlConnection con = new SqlConnection(conexiones.conexionTienda());
                cmd.Connection = con;

                using (con) {
                    string query = @"SET LANGUAGE English; 
                INSERT INTO usuarios  (id_cliente, nombre, apellido_paterno, apellido_materno, email, password, tipo_de_usuario, rango, 
                                        grupo_asesores_adicional, registrado_por) 
                              VALUES (@id_cliente, @nombre, @apellido_paterno, @apellido_materno, @email, @password, @tipo_de_usuario, @rango, 
                                       @grupo_asesores_adicional, @registrado_por);";
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    
                    cmd.Parameters.Add("@id_cliente", SqlDbType.NVarChar, 15);
                    cmd.Parameters["@id_cliente"].Value = textTools.lineSimple(idSAP);

                    cmd.Parameters.Add("@nombre", SqlDbType.NVarChar, 100);
                    cmd.Parameters["@nombre"].Value = textTools.lineSimple(nombre);

                    cmd.Parameters.Add("@apellido_paterno", SqlDbType.NVarChar, 20);
                    cmd.Parameters["@apellido_paterno"].Value = textTools.lineSimple(apellido_paterno);

                    cmd.Parameters.Add("@apellido_materno", SqlDbType.NVarChar, 20);
                    cmd.Parameters["@apellido_materno"].Value = textTools.lineSimple(apellido_materno);

                    cmd.Parameters.Add("@email", SqlDbType.NVarChar, 60);
                    cmd.Parameters["@email"].Value = textTools.lineSimple(email);

                    cmd.Parameters.Add("@password", SqlDbType.NVarChar, 100);
                    cmd.Parameters["@password"].Value = cifrar.passwordUser(password);

                    cmd.Parameters.Add("@tipo_de_usuario", SqlDbType.NVarChar, 100);
                    cmd.Parameters["@tipo_de_usuario"].Value = tipo_de_usuario;

                    cmd.Parameters.Add("@rango", SqlDbType.Int);
                    cmd.Parameters["@rango"].Value = rango;

                    cmd.Parameters.Add("@grupo_asesores_adicional", SqlDbType.NVarChar);
                    cmd.Parameters["@grupo_asesores_adicional"].Value = grupo_asesores_adicional[0];

                    cmd.Parameters.Add("@registrado_por", SqlDbType.NVarChar);
                    cmd.Parameters["@registrado_por"].Value = registrado_por;

                    con.Open();

                    byte resultado = Convert.ToByte(cmd.ExecuteNonQuery());
                    return true;
                }
            }
            catch (Exception ex) {
                devNotificaciones.error("Registrar, crear usuario DB", ex);
                return false;
            }
        }
        return false;

    }
    ///<summary>
    /// Returna  el número de registros con ese usuario en la tabla [usuarios]
    ///</summary>
    public  byte validar_Existencia_Usuario(string email)
    {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con)
            {
                string query = @"SET LANGUAGE English;  SELECT COUNT(email) FROM usuarios WHERE email = @email";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@email", SqlDbType.NVarChar, 60);
                cmd.Parameters["@email"].Value = email;

              
                con.Open();
                byte resultado = Convert.ToByte(cmd.ExecuteScalar());
                return resultado;

            }
        
       
    }

    ///<summary>
    /// Retorna los datos del usuario correcto validando la modalidad asesores este activada o no.
    ///</summary>
    public static  usuarios modoAsesor()
    {
         bool modalidadAsesor =   Boolean.Parse(HttpContext.Current.Session["modoAsesor"].ToString());
        
      
        // Si esta en modo asesor, los datos de usuario y privacidad, precios etc, serán con base en el cliente
        if (modalidadAsesor == true)
           return   (usuarios)System.Web.HttpContext.Current.Session["datosCliente"];
        else
            return (usuarios)System.Web.HttpContext.Current.Session["datosUsuario"];
        //-- FIN parte DatosUsuario

    }

    ///<summary>
    /// Retorna los datos del usuario logeado
    ///</summary>
    public static usuarios userLogin() {
     
        if(System.Web.HttpContext.Current != null  && HttpContext.Current.Session["datosUsuario"] != null) {
            return (usuarios)HttpContext.Current.Session["datosUsuario"];
        } else {
            return usuarios.recuperar_DatosUsuario(System.Web.HttpContext.Current.User.Identity.Name);
        }
           
       
        }
    ///<summary>
    /// Retorna |1| si la modalidad asesores esta activa y si no |0|
    ///</summary>
    public static int modoAsesorCotizacion()
        {
        bool modalidadAsesor = Boolean.Parse(System.Web.HttpContext.Current.Session["modoAsesor"].ToString());
        if (modalidadAsesor == true)
            return 1;
        else if (modalidadAsesor == false)
            return 0;
        else
            return 0;
        }
    ///<summary>
    /// Retorna |1| si la modalidad asesores esta activa y si no |0|
    ///</summary>
    public static int modoAsesorActivado() {
        try { 
        bool modalidadAsesor = Boolean.Parse(System.Web.HttpContext.Current.Session["modoAsesor"].ToString());
        if (modalidadAsesor == true)
            return 1;
        else if (modalidadAsesor == false)
            return 0;
        else
            return 0;
            }
        catch(Exception ex) {
            return 0;
            }
        }

    ///<summary>
    /// Retorna  true si la modalidad asesores esta activa y si no  false
    ///</summary>
    public static bool modoAsesorActivadoBool() {
        try {
            bool modalidadAsesor = Boolean.Parse(System.Web.HttpContext.Current.Session["modoAsesor"].ToString());
            if (modalidadAsesor == true)
                return true;
            else if (modalidadAsesor == false)
                return false;
            else
                return false;
        }
        catch (Exception ex) {
            return false;
        }
    }
    public bool  validar_inicio_sesión(string email, string password)
    {
        seguridad cifrar = new seguridad();
        password = cifrar.passwordUser(password);

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con)
        {
            string query = @"SET LANGUAGE English;  SELECT COUNT(*) FROM usuarios WHERE email = @email AND password = @password";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 60);
            cmd.Parameters["@email"].Value = email;

            cmd.Parameters.Add("@password", SqlDbType.NVarChar, 100);
            cmd.Parameters["@password"].Value = password;

            con.Open();
            byte resultado = Convert.ToByte(cmd.ExecuteScalar());

         

            if (resultado != 0 && resultado == 1) {

                return true; } else { return false; }
           

        }


    }

    ///<summary>
    /// Crea un registro para poder restablecer la contraseña
    ///</summary>
    public static  bool restablecimiento_contraseña(string usuario_email, string usuarioCifrado, string codigo_validacion) {
       

         SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;
        using (con) {
            string query = @"SET LANGUAGE English;
                    INSERT INTO usuarios_restablecimiento_password (fechaSolicitud,  usuario, usuarioCifrado, codigo_validacion,  activo) 
                                                            VALUES (@fechaSolicitud, @usuario, @usuarioCifrado, @codigo_validacion, 1 );";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@fechaSolicitud", SqlDbType.DateTime);
            cmd.Parameters["@fechaSolicitud"].Value = utilidad_fechas.obtenerCentral();

            cmd.Parameters.Add("@usuario", SqlDbType.NVarChar, 60);
            cmd.Parameters["@usuario"].Value = usuario_email;

            cmd.Parameters.Add("@usuarioCifrado", SqlDbType.NVarChar, 100);
            cmd.Parameters["@usuarioCifrado"].Value = usuarioCifrado;
           
            cmd.Parameters.Add("@codigo_validacion", SqlDbType.NVarChar, 100);
            cmd.Parameters["@codigo_validacion"].Value = codigo_validacion;

            try {

                con.Open();
                cmd.CommandText = query;
                cmd.ExecuteScalar();

                return true;

                }
            catch (Exception ex) {

             devNotificaciones.error("Al crear registro de restablecimiento, Email:" + usuario_email, ex);
             return false;
                } 




            }


        }
    
    ///<summary>
         /// Retorna verdadero si existe el registro de email, código de validación y es activo.  REQUIERE VALIDAR  VIEGENCIA CON EL MÉTODO
         /// [validar_vigencia_restablecimiento_contraseña]
         ///</summary>
    public static bool validar_restablecimiento_contraseña(string usuarioCifrado, string codigo_validacion) {
        

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {
            string query = @"SET LANGUAGE English;  SELECT COUNT(*) FROM usuarios_restablecimiento_password WHERE usuarioCifrado = @usuarioCifrado AND codigo_validacion = @codigo_validacion AND activo=1";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@usuarioCifrado", SqlDbType.NVarChar, 100);
            cmd.Parameters["@usuarioCifrado"].Value = usuarioCifrado;

            cmd.Parameters.Add("@codigo_validacion", SqlDbType.NVarChar, 100);
            cmd.Parameters["@codigo_validacion"].Value = codigo_validacion;

            int? resultado = null;
            try { 
            con.Open();
             
             resultado = Convert.ToByte(cmd.ExecuteScalar());
                }
            catch (Exception ex) {

                devNotificaciones.error("Error al validar el restablecimiento de contraseña", ex);
                return false;
                }


            if (resultado != 0 && resultado == 1) {

                return true;
                } else { return false; }


            }


        }



    ///<summary>
    /// Retorna TRUE si el periodo de restablecimiento no ha superado los 3 días REQUIERE VALIDAR QUE SEA ACTIVO CON LA FUNCIÓN 
    /// [validar_restablecimiento_contraseña] de l
    ///</summary>
    public static bool validar_restablecimiento_contraseñaVigencia(string usuarioCifrado, string codigo_validacion) {
       
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {
            string query = @"SET LANGUAGE English; 
                                SELECT DATEDiFF(day, fechaSolicitud, GETDATE()) AS dias
                                FROM  usuarios_restablecimiento_password  WHERE  
                usuarioCifrado= @usuarioCifrado  AND codigo_validacion = @codigo_validacion  ;
                            ";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@usuarioCifrado", SqlDbType.NVarChar, 100);
            cmd.Parameters["@usuarioCifrado"].Value = usuarioCifrado;

            cmd.Parameters.Add("@codigo_validacion", SqlDbType.NVarChar, 100);
            cmd.Parameters["@codigo_validacion"].Value = codigo_validacion;

            int  resultado;
       

            try {
                con.Open();
                string resultadoSTR = cmd.ExecuteScalar().ToString();
              resultado = int.Parse(resultadoSTR);
                }
            catch (Exception ex) {

                devNotificaciones.error("Error al validar vigencia del restablecimiento de contraseña", ex);
                return false;
                }

            // Si el resultado es mayor a 3 días no será válido el restablecimiento
            if (resultado <= 3) {

                return true;
                } else {
                return false; }


            }


        }


    ///<summary>
    /// Una vez que se actualizo la contraseña se desactivo dicho registro
    /// [validar_restablecimiento_contraseña] de l
    ///</summary>
    public static void desactivar_restablecimiento_contraseña(string usuarioCifrado, string codigo_validacion) {
       
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {
            string query = @"SET LANGUAGE English; UPDATE usuarios_restablecimiento_password SET fechaRestablecimiento=@fechaRestablecimiento, activo = 0
                              WHERE usuarioCifrado= @usuarioCifrado  AND codigo_validacion = @codigo_validacion ";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@fechaRestablecimiento", SqlDbType.DateTime);
            cmd.Parameters["@fechaRestablecimiento"].Value = utilidad_fechas.obtenerCentral();


            cmd.Parameters.Add("@usuarioCifrado", SqlDbType.NVarChar, 100);
            cmd.Parameters["@usuarioCifrado"].Value = usuarioCifrado;

            cmd.Parameters.Add("@codigo_validacion", SqlDbType.NVarChar, 100);
            cmd.Parameters["@codigo_validacion"].Value = codigo_validacion;


            con.Open();
            byte resultado = Convert.ToByte(cmd.ExecuteNonQuery());

            }
        }
    public static usuarios recuperar_DatosUsuario(string email) {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {
            SqlDataReader dr;
            StringBuilder sel = new StringBuilder();

            sel.Append("SELECT ");

            // Campos básicos
            sel.Append("id, nombre, apellido_paterno, apellido_materno, email, telefono, celular, ultimo_inicio_sesion,  ");

            // Campos de usuarios
            sel.Append("tipo_de_usuario, rango, departamento, ");

            // Campos propios del cliente
            sel.Append("perfil_cliente, id_cliente, rol_precios_multiplicador, rol_productos, rol_categorias, asesor_base, grupo_asesor, asesor_adicional," +
                " grupo_asesores_adicional, grupoPrivacidad, grupo_usuario, registrado_por, cuenta_activa ");
           
            sel.Append(" FROM usuarios WHERE email = @email;");

            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 60);
            cmd.Parameters["@email"].Value = email;

            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows) {

                usuarios datosUsuario = new usuarios();
                while (dr.Read()) {
                    datosUsuario.id = int.Parse(dr["id"].ToString());
                    datosUsuario.nombre = dr["nombre"].ToString();
                    datosUsuario.apellido_paterno = dr["apellido_paterno"].ToString();
                    datosUsuario.apellido_materno = dr["apellido_materno"].ToString();
                    datosUsuario.email = dr["email"].ToString();
                    datosUsuario.telefono = dr["telefono"].ToString();
                    datosUsuario.celular = dr["celular"].ToString();
                    datosUsuario.tipo_de_usuario = dr["tipo_de_usuario"].ToString();
                    datosUsuario.rango = int.Parse(dr["rango"].ToString());
                    datosUsuario.departamento = dr["departamento"].ToString();
                    datosUsuario.perfil_cliente = dr["perfil_cliente"].ToString();
                    datosUsuario.idSAP = dr["id_cliente"].ToString();
                    datosUsuario.rol_precios_multiplicador = dr["rol_precios_multiplicador"].ToString();
                    datosUsuario.rol_productos = dr["rol_productos"].ToString();
                    datosUsuario.rol_categorias = dr["rol_categorias"].ToString().Split(','); 
                    datosUsuario.registrado_por = dr["registrado_por"].ToString();
                    datosUsuario.cuenta_activa = dr["cuenta_activa"].ToString();
                    
                    if (string.IsNullOrWhiteSpace(dr["ultimo_inicio_sesion"].ToString())) datosUsuario.ultimo_inicio_sesion = new DateTime(2014,08,04);
                    else datosUsuario.ultimo_inicio_sesion = DateTime.Parse( dr["ultimo_inicio_sesion"].ToString());
                    
                    if (dr["asesor_base"].ToString().Replace(" ", "") == "") datosUsuario.asesor_base = null;
                    else datosUsuario.asesor_base = dr["asesor_base"].ToString();
                  
               

                    if (dr["grupo_asesor"].ToString().Replace(" ", "") == "") datosUsuario.grupo_asesor = null;
                    else datosUsuario.grupo_asesor = dr["grupo_asesor"].ToString();


                    if (dr["asesor_adicional"].ToString().Replace(" ", "") == "") datosUsuario.asesor_adicional = null;
                    else datosUsuario.asesor_adicional = dr["asesor_adicional"].ToString().Split(',');


                  

                    if (dr["grupo_asesores_adicional"].ToString().Replace(" ", "") == "") datosUsuario.grupo_asesores_adicional = null;
                    else datosUsuario.grupo_asesores_adicional = dr["grupo_asesores_adicional"].ToString().Split(',');

              
                    if (dr["grupoPrivacidad"].ToString().Replace(" ", "") == "") datosUsuario.grupoPrivacidad = null;
                    else datosUsuario.grupoPrivacidad = dr["grupoPrivacidad"].ToString();


                   

                    if (dr["grupo_usuario"].ToString().Replace(" ", "") == "") datosUsuario.grupo_usuario = null;
                    else datosUsuario.grupo_usuario = dr["grupo_usuario"].ToString().Replace(" ", "");
                    }


                return datosUsuario;
                } else {
                return null;
                }
            }
        }


    ///<summary>
    /// Recupera los campos [nombre], [apellidos], [nombre_completo], [email]
    ///</summary>
    ///<param name="tipo">["usuario"],["cliente"] Se refiere al filtro en la db del campo "tipo_de_usuario" </param>  

    public static DataTable recuperar_DatosUsuariosMin(string tipo_de_usuario) {

         
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {
          

            string query = "SELECT id, nombre, apellido_paterno, apellido_materno, email FROM usuarios  WHERE tipo_de_usuario = @tipo_de_usuario;";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@tipo_de_usuario", SqlDbType.NVarChar, 20);
            cmd.Parameters["@tipo_de_usuario"].Value = tipo_de_usuario;
 
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds);

            return ds.Tables[0];
        } 
             
        
    }
    public static usuarios recuperar_DatosUsuario(int id) {

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {
            SqlDataReader dr;
            StringBuilder sel = new StringBuilder();

            sel.Append("SELECT ");

            // Campos básicos
            sel.Append("id, nombre, apellido_paterno, apellido_materno, email, celular, telefono,  ultimo_inicio_sesion, ");

            // Campos de usuarios
            sel.Append("tipo_de_usuario, rango, departamento, ");

            // Campos propios del cliente
            sel.Append("perfil_cliente, id_cliente, rol_precios_multiplicador, rol_productos, rol_categorias, asesor_base, grupo_asesor, asesor_adicional, grupo_asesores_adicional, grupoPrivacidad, grupo_usuario, registrado_por ");
            sel.Append(" FROM usuarios WHERE id = @id;");

            string query = sel.ToString(); ;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@id", SqlDbType.Int);
            cmd.Parameters["@id"].Value = id;

            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows) {

                usuarios datosUsuario = new usuarios();
                while (dr.Read()) {
                    datosUsuario.id = int.Parse(dr["id"].ToString());
                    datosUsuario.nombre = dr["nombre"].ToString();
                    datosUsuario.apellido_paterno = dr["apellido_paterno"].ToString();
                    datosUsuario.apellido_materno = dr["apellido_materno"].ToString();
                    datosUsuario.email = dr["email"].ToString();
                    datosUsuario.telefono = dr["telefono"].ToString();
                    datosUsuario.celular = dr["celular"].ToString();
                    datosUsuario.tipo_de_usuario = dr["tipo_de_usuario"].ToString();
                    datosUsuario.rango = int.Parse(dr["rango"].ToString());
                    datosUsuario.departamento = dr["departamento"].ToString();
                    datosUsuario.perfil_cliente = dr["perfil_cliente"].ToString();
                    datosUsuario.idSAP = dr["id_cliente"].ToString();
                    datosUsuario.rol_precios_multiplicador = dr["rol_precios_multiplicador"].ToString();
                    datosUsuario.rol_productos = dr["rol_productos"].ToString();
                    datosUsuario.rol_categorias = dr["rol_categorias"].ToString().Split(','); ;
                    datosUsuario.asesor_base = dr["asesor_base"].ToString();
                    datosUsuario.grupo_asesor = dr["grupo_asesor"].ToString();
                    datosUsuario.asesor_adicional = dr["asesor_adicional"].ToString().Split(',');
                    datosUsuario.grupo_asesores_adicional = dr["grupo_asesores_adicional"].ToString().Split(',');
                    datosUsuario.grupoPrivacidad = dr["grupoPrivacidad"].ToString();
                    datosUsuario.grupo_usuario = dr["grupo_usuario"].ToString().Replace(" ", "");
                    datosUsuario.registrado_por = dr["registrado_por"].ToString();
                    if (string.IsNullOrWhiteSpace(dr["ultimo_inicio_sesion"].ToString())) datosUsuario.ultimo_inicio_sesion = new DateTime(2014, 08, 04);
                   else   datosUsuario.ultimo_inicio_sesion = DateTime.Parse(dr["ultimo_inicio_sesion"].ToString());

                }
               

                return datosUsuario;
                } else {
                return null;
                }
            }
        }
    ///<summary>
    /// Recupera los datos de un usuario para crear una variable Session llamada [datosUsuario] que contiene un tipo [List<usuarios>]
    ///</summary>
    public  void establecer_DatosUsuario(string email) {

        usuarios usuario = recuperar_DatosUsuario(email);
        HttpContext.Current.Session["datosUsuario"] = usuario;
 

    }
    static public void establecer_DatosUsuario_static(string email)
    {

        usuarios usuario = recuperar_DatosUsuario(email);
        HttpContext.Current.Session["datosUsuario"] = usuario;


    }

    public void establecer_DatosUsuarioVisitante(){
				usuarios datosUsuario = new usuarios();
        /*
					datosUsuario.id = int.Parse(dr["id"].ToString());
					datosUsuario.nombre = dr["nombre"].ToString();
					datosUsuario.apellido_paterno = dr["apellido_paterno"].ToString();
					datosUsuario.apellido_materno = dr["apellido_materno"].ToString();
					datosUsuario.email = dr["email"].ToString();
					datosUsuario.tipo_de_usuario = dr["tipo_de_usuario"].ToString();
					datosUsuario.rango = int.Parse(dr["rango"].ToString());
					datosUsuario.departamento = dr["departamento"].ToString();
					datosUsuario.perfil_cliente = dr["perfil_cliente"].ToString();
					datosUsuario.idSAP = dr["id_cliente"].ToString();
					*/
                    datosUsuario.idSAP = "0";
                    datosUsuario.rol_precios_multiplicador = "general";   

                    datosUsuario.rol_productos =  "general";

                    datosUsuario.rol_categorias = new string[] {"general"};
                    datosUsuario.grupoPrivacidad =  "visitante";
                /*	datosUsuario.asesor_base = dr["asesor_base"].ToString();
					datosUsuario.grupo_asesor = dr["grupo_asesor"].ToString();
					datosUsuario.asesor_adicional = dr["asesor_adicional"].ToString().Split(',');
					datosUsuario.grupo_asesores_adicional = dr["grupo_asesores_adicional"].ToString().Split(',');
	*/

        // Generamos la variable Session para que contenga los datos del usuario y no tenga que estar consultando en la DB cada vez que sea necesario.
        HttpContext.Current.Session["datosUsuario"] = datosUsuario;
			

		}
    public void establecer_DatosCliente(string emailCliente) {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {
                // Generamos la variable Session para que contenga los datos del usuario y no tenga que estar consultando en la DB cada vez que sea necesario.
                HttpContext.Current.Session["datosCliente"] = recuperar_DatosUsuario(emailCliente);
        }
    }
    public static void  establecer_DatosClienteStatic(string emailCliente)
    {
        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con)
        {
            // Generamos la variable Session para que contenga los datos del usuario y no tenga que estar consultando en la DB cada vez que sea necesario.
            HttpContext.Current.Session["datosCliente"] = recuperar_DatosUsuario(emailCliente);
        }
    }

    public void cambiar_password(string email, string password)
    {
        seguridad cifrar = new seguridad();
        password = cifrar.passwordUser(password);

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con)
        {
            string query = @"SET LANGUAGE English; UPDATE usuarios SET  password = @password WHERE email = @email";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 60);
            cmd.Parameters["@email"].Value = email;

            cmd.Parameters.Add("@password", SqlDbType.NVarChar, 100);
            cmd.Parameters["@password"].Value = password;

            con.Open();
            byte resultado = Convert.ToByte(cmd.ExecuteNonQuery());

        }
    }
    public static void ultimo_login(string email) {
        

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {
            string query = @"SET LANGUAGE English; UPDATE usuarios SET ultimo_inicio_sesion = @ultimo_inicio_sesion WHERE email = @email";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 60);
            cmd.Parameters["@email"].Value = email;

            cmd.Parameters.Add("@ultimo_inicio_sesion", SqlDbType.DateTime);
            cmd.Parameters["@ultimo_inicio_sesion"].Value = utilidad_fechas.obtenerCentral();

            con.Open();
             byte resultado =  Convert.ToByte(cmd.ExecuteNonQuery());

        }
    }
    public void cambiar_datosBasicos(string email,  string nombre, string apellido_paterno, string apellido_materno, string idSAP) {
        seguridad cifrar = new seguridad();
        password = cifrar.passwordUser(password);

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

        using (con) {
            string query = @"SET LANGUAGE English; UPDATE usuarios SET  
            nombre = @nombre, apellido_paterno = @apellido_paterno, apellido_materno = @apellido_materno,
            id_cliente = @id_cliente WHERE email = @email";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 60);
            cmd.Parameters["@email"].Value = email; 

            cmd.Parameters.Add("@nombre", SqlDbType.NVarChar, 20);
            cmd.Parameters["@nombre"].Value = textTools.lineSimple(nombre);

            cmd.Parameters.Add("@apellido_paterno", SqlDbType.NVarChar, 20);
            cmd.Parameters["@apellido_paterno"].Value = textTools.lineSimple(apellido_paterno);

            cmd.Parameters.Add("@apellido_materno", SqlDbType.NVarChar, 20);
            cmd.Parameters["@apellido_materno"].Value = textTools.lineSimple(apellido_materno);

            cmd.Parameters.Add("@id_cliente", SqlDbType.NVarChar, 15);
            cmd.Parameters["@id_cliente"].Value = textTools.lineSimple(idSAP);

            con.Open();
            byte resultado = Convert.ToByte(cmd.ExecuteNonQuery());
            string x = resultado.ToString();
            }
        }
    public static bool cambiar_campo_usuario(string email, string campo, string valor) {
        

        SqlCommand cmd = new SqlCommand();
        SqlConnection con = new SqlConnection(conexiones.conexionTienda());
        cmd.Connection = con;

    

        using (con) {
            string query = @"SET LANGUAGE English; UPDATE usuarios SET " + campo + " = @valor WHERE email = @email";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 60);
            cmd.Parameters["@email"].Value = email;

            cmd.Parameters.Add("@campo", SqlDbType.NVarChar);
            cmd.Parameters["@campo"].Value = campo;

            cmd.Parameters.Add("@valor", SqlDbType.NVarChar);
            cmd.Parameters["@valor"].Value = valor;

          
            con.Open();
            try {
                int resultado = cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex) {
                devNotificaciones.error("Actualizar valor de un usuario: [" + email + "] campo:[" + campo + "] valor: [" + valor + "]", ex);
                return false;
            }

        }
    }
}
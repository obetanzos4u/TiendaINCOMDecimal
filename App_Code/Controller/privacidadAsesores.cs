using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Modalidad Asesores y Clientes
/// </summary>
public class privacidadAsesores {
    private SqlConnection con { get; set; }
    private SqlCommand cmd { get; set; }
    private SqlDataAdapter da { get; set; }
    private DataSet ds { get; set; }
    private DataTable dt { get; set; }

    protected void dbConexion() {

        con = new SqlConnection(conexiones.conexionTienda());
        cmd = new SqlCommand();
        cmd.Connection = con;
        }


    ///<summary>
    /// Returna  TRUE si el usuario y la contraseña coincide.
    ///</summary>
    public byte validar_Existencia_Usuario(string email) {

        dbConexion();
        using (con) {
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
    public static usuarios modoAsesor() {
        bool modalidadAsesor = Boolean.Parse(System.Web.HttpContext.Current.Session["modoAsesor"].ToString());

        //-- INICIO parte DatosUsuario
        usuarios datosUsuario = new usuarios();
        // Si esta en modo asesor, los datos de usuario y privacidad, precios etc, serán con base en el cliente
        if (modalidadAsesor == true) {
           
            return (usuarios)System.Web.HttpContext.Current.Session["datosCliente"];
            }
         
        else
            if((usuarios)System.Web.HttpContext.Current.Session["datosUsuario"] != null) {
            return (usuarios)System.Web.HttpContext.Current.Session["datosUsuario"];
            } else {
            usuarios establecer = new usuarios();
            establecer.establecer_DatosUsuario(HttpContext.Current.User.Identity.Name);
            return (usuarios)System.Web.HttpContext.Current.Session["datosUsuario"];
            }

        //-- FIN parte DatosUsuario
 
        }
    ///<summary>
    /// Retorna true o false obtenido del valor Session ["modoAsesor"]
    ///</summary>
    public static bool modalidadAsesor() {
        if (System.Web.HttpContext.Current.Session["modoAsesor"] == null) return false;
        return Boolean.Parse(System.Web.HttpContext.Current.Session["modoAsesor"].ToString());
        }
    ///<summary>
    /// Retorna true si tiene permiso para visualizar en caso contrario falso
    ///</summary>
    static public bool validarOperacion(string usuario_cliente) {
        return true;
        try { 
        if (modalidadAsesor()) {
              
                usuarios datosClienteOperacion = usuarios.recuperar_DatosUsuario(usuario_cliente);
            usuarios datosUsuario = usuarios.recuperar_DatosUsuario(HttpContext.Current.User.Identity.Name);
                if (usuario_cliente == datosUsuario.email) {
                    return true;
                    }

                bool resultado_grupo_asesores_adicional = datosClienteOperacion.grupo_asesores_adicional.All(x => "general" == x);

       
                if (resultado_grupo_asesores_adicional == true) {
                    return true;
                    }

            if (datosUsuario.email == datosClienteOperacion.asesor_base) return true;

            var asesor_adicional = datosUsuario.asesor_adicional  == null ? false : datosUsuario.asesor_adicional.All(x =>   datosUsuario.email == x);
            
            if (asesor_adicional) return true;

            if (datosClienteOperacion.grupo_asesor  != null && datosUsuario.grupo_usuario  != null && datosClienteOperacion.grupo_asesor == datosUsuario.grupo_usuario ) return true;


            var grupo_asesores_adicional = (datosClienteOperacion.grupo_asesores_adicional == null ?  false  : datosClienteOperacion.grupo_asesores_adicional.All(x =>  datosUsuario.grupo_usuario == x ));
            if (asesor_adicional) return true;

            } 
            
            else {
            usuarios usuario = (usuarios)System.Web.HttpContext.Current.Session["datosUsuario"];
            if (usuario_cliente == usuario.email) {
                    return true;
                    }
                   
            }

     
            } catch(Exception ex) {

                 devNotificaciones.error("Calcular privacidad de operación", ex);

            return false;
            }
        return false;
        }
    private static void redireccionarMicuenta() {
        HttpContext.Current.Server.Transfer("~/usuario/mi-cuenta/mi-cuenta.aspx", true);
        }
    public bool validarUsuario(string nombrePagina, string email, string grupoPrivacidad) {

        dbConexion();
        using (con) {
            string query = @"SET LANGUAGE English;  SELECT COUNT(*) FROM usuarios WHERE email = @email AND password = @password";
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 60);
            cmd.Parameters["@email"].Value = email;



            con.Open();
            byte resultado = Convert.ToByte(cmd.ExecuteScalar());



            if (resultado != 0 && resultado == 1) {

                return true; } else { return false; }


            }


        }
    /// <summary>
    /// Filtra el listado de clientes de acuerdo al asesor
    /// </summary>
    public static DataTable validarListadoClientes(DataTable clientes, model_usuarios asesor) {
     
        clientes.Columns.Add("text", typeof(string), "nombre + ' ' + apellido_paterno + ', ' + email");
        string asesorCuenta = asesor.email;
        string grupo_usuario = asesor.grupo_usuario;


        for (int i = 0; i < clientes.Rows.Count; i++) {
            // Si es valido, no se eliminará del DataTable
            bool valido = false;
            string asesor_base = clientes.Rows[i]["asesor_base"].ToString().Replace(" ", "");
            string grupo_asesor = clientes.Rows[i]["grupo_asesor"].ToString().Replace(" ", "");
            string[] asesor_adicional = clientes.Rows[i]["asesor_adicional"].ToString().Replace(" ", "").Split(',');
            string[] grupo_asesores_adicional = clientes.Rows[i]["grupo_asesores_adicional"].ToString().Replace(" ", "").Split(',');

            if (asesorCuenta == asesor_base) continue;
            if (grupo_usuario == grupo_asesor) continue;


            foreach (string asesorAd in asesor_adicional) {
                if (asesorCuenta == asesorAd) { valido = true; continue;  }
                }
            foreach (string grupo in grupo_asesores_adicional) {
                if (grupo_usuario == grupo || grupo == "general") {
                    valido = true; continue;  
                    }
                }

           if(valido == false) clientes.Rows[i].Delete();
            }



        clientes.AcceptChanges();
        return clientes;
        }

     


   static public void establecer_DatosCliente(int id) {
       
            // Generamos la variable Session para que contenga los datos del usuario y no tenga que estar consultando en la DB cada vez que sea necesario.
            HttpContext.Current.Session["datosCliente"] = usuarios.recuperar_DatosUsuario(id);
            
        }



    }
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de usuarios
/// </summary>
public class privacidadPaginas
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
    /// Returna  TRUE si el usuario y la contraseña coincide.
    ///</summary>
    public byte validar_Existencia_Usuario(string email)
    {

        dbConexion();
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
    public static usuarios modoAsesor()
    {
        bool modalidadAsesor = Boolean.Parse(System.Web.HttpContext.Current.Session["modoAsesor"].ToString());

        //-- INICIO parte DatosUsuario
        usuarios datosUsuario = new usuarios();
        // Si esta en modo asesor, los datos de usuario y privacidad, precios etc, serán con base en el cliente
        if (modalidadAsesor == true)
            return (usuarios)System.Web.HttpContext.Current.Session["datosCliente"];
        else
            return (usuarios)System.Web.HttpContext.Current.Session["datosUsuario"];
        //-- FIN parte DatosUsuario

    }
    public static int modoAsesorCotizacion()
    {
        bool modalidadAsesor = Boolean.Parse(System.Web.HttpContext.Current.Session["modoAsesor"].ToString());
        if (modalidadAsesor == true)
            return 1;
        else
            return 0;


    }
    public static json_respuestas validarPermisoSeccion(string seccion_pagina, int idUsuario)
    {

        try
        {
            using (var db = new tiendaEntities())
            {
                var permiso = db.permisos_app
                    .Where(p => p.idUsuario == idUsuario && p.seccion_pagina == seccion_pagina)
                     .AsNoTracking()
                    .FirstOrDefault();

                if (permiso == null)
                {
                    return new json_respuestas(false, "No se encontró un permiso a esta sección");
                }
                else
                {
                    string message = "";
                    if (permiso.permiso == false) message = "No tienes permiso para esta sección-página.";
                    return new json_respuestas(permiso.permiso, message, false, permiso);
                }

            }
        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Error al validar permiso: " + ex.Message, true);
        }
    }
    ///<summary>
    /// Si el permiso ya existe: lo actualiza, si no exite: lo crea establece.
    ///</summary>
    public static json_respuestas establecerPermisoAppSeccion(string seccion_pagina, bool permiso, int idUsuario)
    {
        try
        {
            using (var db = new tiendaEntities())
            {
                var permiso_app = db.permisos_app
                    .Where(p => p.idUsuario == idUsuario && p.seccion_pagina == seccion_pagina)
                    .FirstOrDefault();

                if (permiso_app == null)
                {
                    permiso_app = new permisos_app();
                    permiso_app.idUsuario = idUsuario;
                    permiso_app.seccion_pagina = seccion_pagina;
                    permiso_app.permiso = permiso;

                    db.permisos_app.Add(permiso_app);
                    db.SaveChanges();
                    return new json_respuestas(true, "Permiso creado con éxito", false);
                }
                else
                {
                    permiso_app.idUsuario = idUsuario;
                    permiso_app.seccion_pagina = seccion_pagina;
                    permiso_app.permiso = permiso;

                    db.SaveChanges();

                    return new json_respuestas(true, "Permiso actualizado con éxito", false);
                }

            }
        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Error al establecer permiso: " + ex.Message, true);
        }
    }

    ///<summary>
    /// Obtiene los permisos de un determinado usuario.
    ///</summary>
    public static json_respuestas obtenerPermisos(int idUsuario)
    {
        try
        {
            using (var db = new tiendaEntities())
            {
                var permiso_app = db.permisos_app
                    .Where(p => p.idUsuario == idUsuario)
                    .AsNoTracking()
                    .ToList();

                return new json_respuestas(true, "Permiso creado con éxito", false, permiso_app);

            }
        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Error al obtener permisos: " + ex.Message, true);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Descripción breve de UsuariosEF
/// </summary>
public class UsuariosEF
{
    public UsuariosEF()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }


    /// <summary>
    /// 20210308 CM - Obtiene la información de un usuario
    /// </summary>
    public static usuario Obtener(int idUsuario)
    {


        try
        {

            using (var db = new tiendaEntities())
            {
                var Usuario = db.usuarios
                   .AsNoTracking()
                   .Where(s => s.id == idUsuario).FirstOrDefault();


                return Usuario;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }
    /// <summary>
    /// 20220525 CM - Obtiene los usuarios Incom activos
    /// </summary>
    public static async Task<List<usuario>> ObtenerUsuariosIncom()
    {


        try
        {

            using (var db = new tiendaEntities())
            {
                var Usuarios = await db.usuarios
                   .AsNoTracking()
                   .Where(s => s.tipo_de_usuario == "usuario" && s.cuenta_activa != false).ToListAsync();


                return Usuarios;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    /// <summary>
    /// 20220525 CM - Obtiene los usuarios VendedoresIncom activos
    /// </summary>
    public static async Task<List<usuario>> ObtenerUsuariosVendedores()
    {
        try
        {
            using (var db = new tiendaEntities())
            {
                var Usuarios = await db.usuarios
                   .AsNoTracking()
                   .Where(s => s.tipo_de_usuario == "usuario" && s.cuenta_activa != false)
                 .Where(s => s.departamento == "Telemarketing" || s.departamento == "Ventas").Distinct().ToListAsync();
                return Usuarios;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }
    /// <summary>
    /// 20210308 CM - Obtiene la información de un usuario
    /// </summary>
    public static usuario Obtener(string emailUsuario)
    {


        try
        {

            using (var db = new tiendaEntities())
            {
                var Usuario = db.usuarios
                    .AsNoTracking()
                    .Where(s => s.email == emailUsuario).FirstOrDefault();


                return Usuario;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }
    /// <summary>
    /// 20210514 CM - Obtiene la información de un usuario
    /// </summary>
    public static usuariosInfo ObtenerInfo(int idUsuario)
    {


        try
        {

            using (var db = new tiendaEntities())
            {
                var Usuario = db.usuariosInfoes
                    .AsNoTracking()
                    .Where(s => s.idUsuario == idUsuario).FirstOrDefault();


                return Usuario;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }
    /// <summary>
    /// 20210308 CM - Crea un usuario
    /// </summary>
    public static async Task<json_respuestas> Crear(usuario Usuario)
    {
        try
        {
            using (var db = new tiendaEntities())
            {
                db.usuarios.Add(Usuario);
                await db.SaveChangesAsync();

                return new json_respuestas(true, "Usuario creado con éxito", false, Usuario);
            }
        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Hubo un error al crear el usuario", true, ex);
        }
    }

    /// <summary>
    /// 20210514 CM - Crea la información extra de un usuario ya registrado
    /// </summary>
    public static async Task<json_respuestas> CrearInfo(usuariosInfo UsuarioInfo)
    {
        try
        {
            using (var db = new tiendaEntities())
            {
                db.usuariosInfoes.Add(UsuarioInfo);
                await db.SaveChangesAsync();

                return new json_respuestas(true, "Info creada con éxito", false, UsuarioInfo);
            }
        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Hubo un error al crear la info", true, ex);
        }
    }

    /// <summary>
    /// 20210420 CM - Devuelve true si el usuario existe -> [json_respuestas.result]
    /// </summary>
    public static json_respuestas ValidarExistenciaUsuario(string emailUsuario)
    {

        try
        {
            using (var db = new tiendaEntities())
            {
                var Usuario = db.usuarios
                    .AsNoTracking()
                    .Where(s => s.email == emailUsuario)
                    .Count();


                if (Usuario == 1)
                {
                    return new json_respuestas(true, $"La cuenta con el usuario {emailUsuario} existe", false);
                }
                else
                {
                    return new json_respuestas(false, "Cuenta no encontrada.", false);
                }
            }

        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Ocurrio un error al validar existencia", true, ex);
        }



    }


    /// <summary>
    /// 20210420 CM - Devuelve true si el usuario e esta activo -> [json_respuestas.result]
    /// </summary>
    public static json_respuestas ValidarCuentaActiva(string emailUsuario)
    {

        try
        {
            using (var db = new tiendaEntities())
            {
                var Usuario = db.usuarios
                    .AsNoTracking()
                    .Where(s => s.email == emailUsuario)
                    .FirstOrDefault();

                /*
                La propiedad [cuenta_confirmada]  se creó tiempo después, el valor default a los regisros ya creado se establecio en null, las cuentas
                nuevas se les asigna el valor false y es necesario activarlas
                */

                if (Usuario.cuenta_confirmada == true || Usuario.cuenta_confirmada == null)
                {
                    return new json_respuestas(true, "La cuenta se encuentra confirmada", false);
                }
                else
                {
                    return new json_respuestas(false, "La cuenta no ha sido confirmada o no existe", false);
                }
            }

        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Ocurrio un error al buscar la cuenta", true, ex);
        }



    }


    /// <summary>
    /// 20210420 CM - Confirma la cuenta del usuario recibido
    /// </summary>
    public static async Task<json_respuestas> ConfirmarCuenta(string emailUsuario)
    {
        try
        {

            using (var db = new tiendaEntities())
            {
                var usuario = db.usuarios
                    .Where(s => s.email == emailUsuario).FirstOrDefault();

                usuario.cuenta_confirmada = true;

                await db.SaveChangesAsync();

            }
            return new json_respuestas(true, "Cuenta activada correctamente.", false);
        }
        catch (Exception ex)
        {

            return new json_respuestas(false, "Error al activar cuenta, intenta más tarde.", true, ex);
        }
    }

    /// <summary>
    /// 20210420 CM - Genera una liga y lo guarda en la base de datos, la vigencia es de 2 dias
    /// </summary>
    public static async Task<json_respuestas> GenerarLigaConfirmacionDeCuenta(string emailUsuario)
    {

        try
        {

            var fechaCreacion = utilidad_fechas.obtenerCentral();
            var fechaVigencia = utilidad_fechas.obtenerCentral().AddDays(2);
            string clave = seguridad.cifrar(emailUsuario + " " + new Random().Next(0, 100) + fechaCreacion.Millisecond.ToString());

            var liga = new usuarios_ligas_confirmaciones()
            {
                usuario = emailUsuario,
                fecha_creacion = fechaCreacion,
                fecha_vigencia = fechaVigencia,
                clave = clave
            };

            using (var db = new tiendaEntities())
            {
                var usuario = db.usuarios_ligas_confirmaciones.Add(liga);
                await db.SaveChangesAsync();

            }

            return new json_respuestas(true, "Liga generada correctamente", false, liga);
        }
        catch (Exception ex)
        {

            return new json_respuestas(false, "Error al generar liga de confirmación intenta más tarde.", true, ex);
        }

    }

    public static async Task<json_respuestas> ObtenerLiga(string clave)
    {
        try
        {

            using (var db = new tiendaEntities())
            {
                var liga = await db.usuarios_ligas_confirmaciones
                    .AsNoTracking()
                    .Where(s => s.clave == clave)
                  .FirstOrDefaultAsync();

                if (liga != null)
                    return new json_respuestas(true, "Liga encontrada con éxito.", false, liga);
                else
                    return new json_respuestas(false, "No se encontró una liga.", false, null);
            }

        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Error al buscar liga.", true, ex);
        }
    }
    public static async Task<json_respuestas> ObtenerCantidadLigasCreadas(string emailUsuario)
    {
        try
        {

            using (var db = new tiendaEntities())
            {
                var cantidad = await db.usuarios_ligas_confirmaciones
                    .AsNoTracking()
                    .Where(s => s.usuario == emailUsuario)
                    .CountAsync();
                return new json_respuestas(true, "Cantidad de ligas obtenidas por usuario con éxito.", false, cantidad);

            }

        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Error interno al contar ligas.", true, ex);
        }
    }
    public static async Task<json_respuestas> BorrarLigas(string emailUsuario)
    {
        try
        {

            using (var db = new tiendaEntities())
            {
                var ligas = db.usuarios_ligas_confirmaciones
                    .Where(s => s.usuario == emailUsuario).ToList();

                db.usuarios_ligas_confirmaciones.RemoveRange(ligas);
                await db.SaveChangesAsync();

            }
            return new json_respuestas(true, "Ligas eliminadas con éxito.", false);
        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Error al eliminar ligas.", true);
        }
    }

    public static async Task<json_respuestas> EnviarEmailActivacion(usuario Usuario, string dominio)
    {
        try
        {

            var resultGeneracionLiga = await UsuariosEF.GenerarLigaConfirmacionDeCuenta(Usuario.email);

            if (resultGeneracionLiga.result == false && resultGeneracionLiga.exception == true)
            {
                return new json_respuestas(false, "Error interno, no fué posible enviar el email de activación", true);
            }

            usuarios_ligas_confirmaciones Liga = resultGeneracionLiga.response;

            string filePath = "/email_templates/ui/usuario_ConfirmarCuenta.html";

            Dictionary<string, string> datos = new Dictionary<string, string>();
            datos.Add("{nombre}", Usuario.nombre + " " + Usuario.apellido_paterno);
            datos.Add("{dominio}", dominio);
            datos.Add("{enlaceConfirmacion}", dominio + "/usuario-confirmacion-de-cuenta.aspx?clave=" + Liga.clave);

            string mensaje = archivosManejador.reemplazarEnArchivo(filePath, datos);
            emailTienda registro = new emailTienda("Confirma tu cuenta de Incom.mx: " + Usuario.nombre + " ", Usuario.email, mensaje, null);
            registro.general();

            return new json_respuestas(false, "Email enviado con éxito", true);
        }
        catch (Exception ex)
        {

            return new json_respuestas(false, "Error interno, no fué posible enviar el email de activación", true, ex);
        }
    }


}

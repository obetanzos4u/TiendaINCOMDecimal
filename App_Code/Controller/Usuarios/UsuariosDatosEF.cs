using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Descripción breve de UsuariosDatosEF
/// </summary>
public class UsuariosDatosEF
{
    public UsuariosDatosEF()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    /// <summary>
    /// 20211123 
    /// </summary>
    public static async Task<json_respuestas> GuardarTelefonoFijo(string emailUsuario, string Telefono)
    {
        try
        {

            using (var db = new tiendaEntities())
            {
                var usuario = db.usuarios
                    .Where(s => s.email == emailUsuario).FirstOrDefault();

                usuario.telefono = Telefono;

                await db.SaveChangesAsync();

            }
            return new json_respuestas(true, "Teléfono guardado correctamente.", false);
        }
        catch (Exception ex)
        {

            return new json_respuestas(false, "Error al guardar dato, intenta más tarde.", true, ex);
        }
    }

    /// <summary>
    /// 20211123 
    /// </summary>
    public static async Task<json_respuestas> GuardarCelular(string emailUsuario, string Celular)
    {
        try
        {
            using (var db = new tiendaEntities())
            {
                var usuario = db.usuarios
                    .Where(s => s.email == emailUsuario).FirstOrDefault();

                usuario.celular = Celular;

                await db.SaveChangesAsync();
            }
            return new json_respuestas(true, "Número de celular guardado correctamente.", false);
        }
        catch (Exception ex)
        {
            return new json_respuestas(false, "Error al guardar dato, intenta más tarde.", true, ex);
        }
    }
}
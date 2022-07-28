using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
 
/// <summary>
/// Descripción breve de productosComentarios
/// </summary>
public class productosComentarios
{

    public bool resultado { get; set; }
    public string msg_resultado { get; set; }

    /// <summary>
    /// Obtiene los comentarios de un producto
    /// </summary>
    static public dynamic obtenerComentarios(string numero_parte)
    {

        using (tiendaEntities db = new tiendaEntities())
        {

            var comentarios = db.productos_comentarios
                .Join(db.usuarios, // the source table of the inner join
                   comentario => comentario.idUsuario,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
                   usuario => usuario.id,   // Select the foreign key (the second part of the "on" clause)
                  (comentario, usuario) => new {
                      idComentario = comentario.idComentario,
                      numero_parte = comentario.numero_parte,
                      fechaComentario = comentario.fechaComentario,
                      idUsuario = comentario.idUsuario,
                      comentario = comentario.comentario,
                      calificación = comentario.calificación,
                      visible = comentario.visible,
                      nombreUsuario = usuario.nombre + " " + usuario.apellido_paterno,
                      id = usuario.id
                  })
           .Where(a => a.numero_parte == numero_parte).OrderByDescending(x => x.fechaComentario).ToList();



            return comentarios;
        }

    }

    /// <summary>
    /// Guarda un comentario
    /// </summary>
    public  async Task<bool> guardarComentario(productos_comentarios comentario)
    {

        try
        {
            using (tiendaEntities db = new tiendaEntities())
            {
                db.productos_comentarios.Add(comentario);
                int x = await db.SaveChangesAsync();

                if (x == 1)
                {
                    resultado = true;
                    msg_resultado = "Comentario agregado con éxito";
                    return true;

                }
                else
                {
                    resultado = false;
                    msg_resultado = "Error al agregar comentario [0]";
                    return false;
                }

            }

        }
        catch (Exception ex)
        {

            resultado = false;
            msg_resultado = "Error al agregar comentario";


            return false;
        }
    }
}
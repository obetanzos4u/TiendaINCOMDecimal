using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
public class carrito_producto
{
    public int id { get; set; }
    public string usuario { get; set; }
    public int activo { get; set; }
    public int tipo { get; set; }
    public System.DateTime fecha_creacion { get; set; }
    public string numero_parte { get; set; }
    public string descripcion { get; set; }
    public string marca { get; set; }
    public string moneda { get; set; }
    public decimal tipo_cambio { get; set; }
    public System.DateTime fecha_tipo_cambio { get; set; }
    public string unidad { get; set; }
    public decimal precio_unitario { get; set; }
    public decimal cantidad { get; set; }
    public decimal precio_total { get; set; }
    public Nullable<double> stock1 { get; set; }
    public Nullable<System.DateTime> stock1_fecha { get; set; }
    public Nullable<double> stock2 { get; set; }
    public Nullable<System.DateTime> stock2_fecha { get; set; }
    public string imagenes { get; set; }

    
}

    /// <summary>
    /// Descripción breve de carritoEF
    /// </summary>
    public class carritoEF
{
   
    public carritoEF()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    /// <summary>
    /// 20200709 CM- Obtiene el carrito de productos con otros datos del producto como imagenes (haciendo un .Join)
    /// </summary>
    public static List<carrito_productos> obtenerCarrito(string usuario)
    {
        try
        {

            using (var db = new tiendaEntities())
            {
                var carritoProductos = db.carrito_productos
                    .Where(s => s.usuario == usuario).ToList();


                return carritoProductos;
            }
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    /// <summary>
    /// 20200709 CM - Obtiene el carrito de productos con otros datos del producto como imagenes (haciendo un .Join)
    /// </summary>

    public static dynamic obtenerCarritoFull(string usuario)
    {


      
        try
        {

            using (var db = new tiendaEntities())
            {

                List <carrito_producto> carritoPrdoductos = new List<carrito_producto>();

                var carritoProductos = from carrito in db.carrito_productos
                                                      join datos in db.productos_Datos
                                                      on carrito.numero_parte equals datos.numero_parte
                                       where carrito.usuario == usuario
                                                      select new
                                                      {
                                                          carrito.id,
                                                           carrito.usuario,
                                                           carrito.activo,
                                                            carrito.tipo,
                                                           carrito.fecha_creacion,
                                                            carrito.numero_parte,
                                                           carrito.descripcion,
                                                          carrito.marca,
                                                          carrito.moneda,
                                                           carrito.tipo_cambio,
                                                          carrito.cantidad,
                                                          carrito.unidad,
                                                          carrito.precio_unitario,
                                                           carrito.precio_total,

                                                        datos.imagenes
                                                      }
                                                      
                                                      ;


                return carritoProductos.ToList();


                var t = db.carrito_productos
                    .Join(db.productos_Datos, // the source table of the inner join
                       carrito => carrito.numero_parte,        // Select the primary key (the first part of the "on" clause in an sql "join" statement)
                       productoDatos => productoDatos.numero_parte,   // Select the foreign key (the second part of the "on" clause)
                      (carrito, productoDatos) => new
                      {
                          id = carrito.id,
                          usuario = carrito.usuario,
                          activo = carrito.activo,
                          tipo = carrito.tipo,
                          fecha_creacion = carrito.fecha_creacion,
                          numero_parte = carrito.numero_parte,
                          descripcion = carrito.descripcion,
                          marca = carrito.marca,
                          moneda = carrito.moneda,
                          tipo_cambio = carrito.tipo_cambio,
                          unidad = carrito.unidad,
                          precio_unitario = carrito.precio_unitario,
                          precio_total = carrito.precio_total,

                          imagenes = productoDatos.imagenes

                      })
                    .Where(s => s.usuario == usuario).ToList();




                return carritoProductos;
            }
        }
        catch (Exception ex)
        {
            string exx = ex.ToString();
            return null;
        }
    }


    /// <summary>
    /// 20200624 RC - Elimina el registro de la infografía y también sus respectivos archivos (infografía y miniatura).
    /// </summary>
    public static void eliminar(string numero_parte,string usuario)
    {
        try
        {

            using (var context = new tiendaEntities())
            {

                var carritoProducto = context.carrito_productos
                    .Where(s => s.numero_parte == numero_parte && s.usuario == usuario)
                    .SingleOrDefault();

                context.carrito_productos.Remove(carritoProducto);

            }
        }
        catch (Exception ex)
        {


        }
    }
}
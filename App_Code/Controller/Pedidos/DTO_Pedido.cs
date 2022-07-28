using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de DTO_Pedidos
/// </summary>
public class DTO_Pedido {
    public DTO_Pedido() { }
    public pedidos_datos PedidoDatos { get; set; }

 
    public pedidos_datosNumericos PedidoDatosNumericos { get; set; }

    public dynamic Pagado { get; set; }
    public List<pedidos_productos> PedidoProductos { get; set; }
    public decimal MontoProductos_MXN_Sin_Impuestos {

        get {

            decimal Monto = 0;

            foreach (var p in PedidoProductos) {

                Monto += PedidoDatosNumericos.monedaPedido == "USD" ?
                        p.precio_total * PedidoDatosNumericos.tipo_cambio : p.precio_total;

            }

            return Monto;
        }
    }

     
    public bool ProbableRepetido { get; set; }

    public int TotalProductos { get {

            return PedidoProductos.Count;
        }
    }
  
    public bool? MétodoPago { get; set; }


}
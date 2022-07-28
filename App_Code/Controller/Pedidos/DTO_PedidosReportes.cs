using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de DTO_PedidosReportes
/// </summary>
/// 
public class DTO_PedidosReportesDesgloseMes {
    public string MesNombre { get; set; }
    public int Mes { get; set; }
    public int Year { get; set; }
    public int TotalPedidosMes { get; set; }
    public int TotalPedidosPagados { get; set; }
    public decimal MontoTotalMesSNImpuestos { get; set; }
}

public class DTO_PedidosReportes {



    public List<DTO_Pedido> Pedidos { get; set; }

    public  List<DTO_PedidosReportesDesgloseMes> DesgloseMeses { get; set; }
    public DTO_PedidosReportes(List<DTO_Pedido> _Pedidos) {
        Pedidos = _Pedidos;

        Procesar();
    }


    async void Procesar() {

        #region Procesamos los pedidos para obtener el reporte del comportamiento mensual
        Pedidos.RemoveAll(p => p.PedidoDatos.creada_por.Contains("@incom.mx"));
        Pedidos.RemoveAll(p => p.PedidoDatos.nombre_pedido.Contains("prueba") && p.PedidoDatos.creada_por.Contains("resback@gmail.com"));
        Pedidos.OrderBy(p => p.PedidoDatos.fecha_creacion);
        /*
      for (int i=0; i < Pedidos.Count; i++) {



         if (i+1 == Pedidos.Count) break;



         // Si el pedido siguiente es del mismo cliente, es posible repetido
             if (Pedidos[i].PedidoDatos.creada_por == Pedidos[i + 1].PedidoDatos.creada_por) {

             TimeSpan diff = Pedidos[i].PedidoDatos.fecha_creacion - Pedidos[i + 1].PedidoDatos.fecha_creacion;

             double TotalMinutes = diff.TotalMinutes * -1;

             //Si el pedido siguiente tiene N cantidad de horas de diferencia aumenta la probabilidad de pedido repetido

             if(TotalMinutes < 600) {
                 var itemToRemove = Pedidos[i];
                 Pedidos.Remove(itemToRemove);

             }

         }






     }
*/
        #endregion








        #region Procesamos los pedidos para obtener el reporte del comportamiento mensual
        DesgloseMeses = new List<DTO_PedidosReportesDesgloseMes>();


       var RegistrosAños  = Pedidos.Select(t => t.PedidoDatos) .GroupBy(x => x.fecha_creacion)
           .GroupBy(p => p.Key.Year)
            .ToList();


        var RegistroMeses = Pedidos.Select(t => t.PedidoDatos).GroupBy(x => x.fecha_creacion)
            .GroupBy(p => p.Key.Month)
            .OrderBy(p => p.Key)
             .ToList();
        


           foreach(var año in RegistrosAños) {

                foreach (var mes in RegistroMeses) {

                int TotalPedidosPagados = 0;
                var DesgloseMes = new DTO_PedidosReportesDesgloseMes();

                var PedidosMes = Pedidos
                    .Select(t => t.PedidoDatos)
                    .Where(p => p.fecha_creacion.Year == año.Key && p.fecha_creacion.Month == mes.Key)
                    .ToList();

                var TotalPedidosMes = PedidosMes.Count();


                if (TotalPedidosMes == 0)  continue;


                foreach(var p in PedidosMes) {

                    var result = await PedidosEF.ObtenerPagoPedido(p.numero_operacion);

                    if (result.result) {
                        TotalPedidosPagados += 1;
                    }
                }



                    DesgloseMes.TotalPedidosMes = TotalPedidosMes;
                    DesgloseMes.Year = año.Key;
                    DesgloseMes.Mes = mes.Key;
                    DesgloseMes.MontoTotalMesSNImpuestos = Pedidos
                    .Where(p => p.PedidoDatos.fecha_creacion.Year == año.Key && p.PedidoDatos.fecha_creacion.Month == mes.Key)
                    .Sum(p => p.MontoProductos_MXN_Sin_Impuestos);
                
                    DesgloseMes.MesNombre=  CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DesgloseMes.Mes);

                DesgloseMes.TotalPedidosPagados = TotalPedidosPagados;


                DesgloseMeses.Add(DesgloseMes);
                }



    

        }
         
        #endregion
    }
}

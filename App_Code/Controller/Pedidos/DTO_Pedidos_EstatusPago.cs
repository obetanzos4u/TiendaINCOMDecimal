using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Obtiene la información del pedido en cuanto a un pago realizado.
/// </summary>
public class DTO_Pedidos_EstatusPago
{
    public bool Estatus { get; set; }
    public string MetodoPago { get; set; }
    public decimal? Monto { get; set; }
    public string Moneda { get; set; }
    public DateTime? FechaPago { get; set; }

    public string Mensaje { get; set; }
    public DTO_Pedidos_EstatusPago()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    public static  DTO_Pedidos_EstatusPago ObtenerEstatusPago(string numero_operacion)
    {
        using (tiendaEntities db = new tiendaEntities())
        {
            var PagoPayPal = db.pedidos_pagos_paypal.Where(x => x.numero_operacion == numero_operacion).ToList()
                .OrderByDescending(t => t.fecha_primerIntento).FirstOrDefault();

            // Verificando en papypal
            if (PagoPayPal != null)
            {
                DTO_Pedidos_EstatusPago estatusPayPal = new DTO_Pedidos_EstatusPago();

                estatusPayPal.Estatus = false;
                if (PagoPayPal.estado == "COMPLETED")
                {
                    estatusPayPal.Estatus = true;
                    estatusPayPal.Mensaje = "Pago realizado con éxito por PayPal.";
                    estatusPayPal.FechaPago = PagoPayPal.fecha_primerIntento;
                    estatusPayPal.Monto = decimal.Parse(PagoPayPal.monto);
                    estatusPayPal.Moneda = PagoPayPal.moneda;
                }
                else
                {
                    estatusPayPal.Estatus = false;
                    estatusPayPal.Mensaje = "Intento de pago en PayPal aún no realizado.";
                    estatusPayPal.FechaPago = PagoPayPal.fecha_primerIntento;
                }

                return estatusPayPal;


            }
            else
            {
                var pagoSantander = SantanderResponse.obtener(numero_operacion);

                // Verificando en papypal
                if (pagoSantander != null)
                {
                    DTO_Pedidos_EstatusPago estatusSantander = new DTO_Pedidos_EstatusPago();

                    estatusSantander.Estatus = false;
                    if (estatusSantander.Estatus == true)
                    {
                        estatusSantander.Estatus = true;
                        estatusSantander.Mensaje = "Pago realizado con éxito mediante 3DS.";
                        estatusSantander.FechaPago = estatusSantander.FechaPago;
                        estatusSantander.Monto = estatusSantander.Monto;
                        estatusSantander.Moneda = estatusSantander.Moneda;
                    }
                    else
                    {
                        estatusSantander.Estatus = false;
                        estatusSantander.Mensaje = "Intento de pago en 3DS aún no realizado.";
                        estatusSantander.FechaPago = estatusSantander.FechaPago;
                    }

                    return estatusSantander;


                }
                 


                
            }




            DTO_Pedidos_EstatusPago estatus = new DTO_Pedidos_EstatusPago();

            estatus.Estatus = false;
            estatus.Mensaje = "No se ha realizado ningún pago.";
            estatus.FechaPago = null;
            estatus.Monto = decimal.Parse(PagoPayPal.monto);
            estatus.Moneda = PagoPayPal.moneda; 
            
            
            return estatus;
        }
    }


}
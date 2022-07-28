using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

/// <summary>
/// Descripción breve de PedidoToSAP
/// </summary>
public class SendOrderPromoToSAP
{
    private readonly string _numero_operacion;
    public string numero_operacion { get { return _numero_operacion; } }

    public PedidosDTOModel Pedido { get; set; }

    public List<PromocionesProductoModel> ProductosPromosSAP { get; set; }

    public SendOrderPromoToSAP(string _numero_operacion_, List<PromocionesProductoModel> _ProductosPromosSAP)
    {
        _numero_operacion = _numero_operacion_;
        ProductosPromosSAP = _ProductosPromosSAP;
    }

    public SendOrderPromoToSAP(PedidosDTOModel _Pedido, List<PromocionesProductoModel> _ProductosPromosSAP)
    {
        Pedido = _Pedido;
        ProductosPromosSAP = _ProductosPromosSAP;
    }

   
   static  public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
    public async Task<json_respuestas> Send()
    {
        if (Pedido == null || numero_operacion != null)
        {
            var resultGetPedido = await PedidosEF.ObtenerPedidoCompleto(numero_operacion);
            if (resultGetPedido.result == false)
                return new json_respuestas(false, "Error al obtener pedido." + resultGetPedido.message);
            else Pedido = resultGetPedido.response;
        }

        string data = await PedidoXML();



        var url = "https://my357755.sapbydesign.com/sap/bc/srt/scs/sap/managesalesorderin5?sap-vhost=my357755.sapbydesign.com";

        var httpRequest = (HttpWebRequest)WebRequest.Create(url);
        httpRequest.Method = "POST";

        httpRequest.ContentType = "application/soap+xml";
        httpRequest.Headers["Authorization"] = "Basic Q09OU1VMVEFTOlNwZWNpYWxpemVkMjAxOV8=";

      

        using (var streamWriter = new System.IO.StreamWriter(httpRequest.GetRequestStream()))
        {
            streamWriter.Write(data);
        }

        var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
        var result = "";
        using (var streamReader = new System.IO.StreamReader(httpResponse.GetResponseStream()))
        {
              result = streamReader.ReadToEnd();
        }

      

        return new json_respuestas(true,result);
    }


    private  async  Task<string>  PedidoXML() {


        string data = System.IO.File.ReadAllText(@"Y:\01 Desarrollo\01 Desarrollo\Tu y Yo\Incom.mx\_Documentación\Modulos\20220527 - Pedido campaña to SAP\xmlPedido.cs");
        var PriceDateTime = utilidad_fechas.obtenerCentral().ToString("yyyy-MM-ddTHH:mm:ssZ");
        string ProductosXML = "";

        data = data.Replace("{PriceDateTime}", PriceDateTime); 
        data = data.Replace("{numero_pedido}", Pedido.datos.numero_operacion);
        data = data.Replace("{descripcion}", Pedido.datos.email); 

        int Position = 10;

        foreach (var Producto in Pedido.productos)
        {
            // Obtenemos el número de parte SAP
            var ProductoInfo = await ProductosTiendaEF.ObtenerSoloNumerosParte(Producto.numero_parte);
           


            if (ProductoInfo != null)
            {

                var ResultExist = ProductosPromosSAP.Find(x => x.numero_parte == Producto.numero_parte);
                if (ResultExist  != null)
                {
                    ProductosXML += GenerateProductoXML(ProductoInfo.numero_parte, Producto.cantidad, null, Position);
                }
                else
                {
                    ProductosXML += GenerateProductoXML(ProductoInfo.numero_parte, Producto.cantidad, Producto.precio_unitario, Position);
                }
                Position += 10;
            }
            else
            {
                throw new Exception($"El producto {Producto.numero_parte} no tiene número de parte SAP");
            }

         
        }


        data = data.Replace("{Productos}", ProductosXML);
        return data;

    }
    private string  GenerateProductoXML(string numero_parte_SAP, decimal Cantidad, decimal? Precio, int Position) {

        Cantidad = (Decimal.Round(Cantidad,1));

        var ProductXML = $@"<Item actionCode=""04"">
                    <!--ID de posición, debe incrementarse de 10 en 10-->
                    <ID>{Position}</ID>
                    <!-- Liberar para ejecutar, corresponde al ATP? -->
                    <ReleaseToExecute>false</ReleaseToExecute>
                    <!-- Número de parte del producto pedido -->
                    <ItemProduct actionCode=""04"">
                        <ProductInternalID>{numero_parte_SAP}</ProductInternalID>
                    </ItemProduct>
                    <!--Línea de programación de árticulos, revisar -->
                    <ItemScheduleLine actionCode=""04"">
                        <ID>1</ID>
                        <TypeCode>1</TypeCode>
                        <!-- Cantidad de items solicitados en el pedido -->
                        <Quantity>{Cantidad}</Quantity>
                    </ItemScheduleLine>
                </Item> ";


        return ProductXML;
    }

}
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Wordprocessing;
using iText.Html2pdf.Jsoup.Nodes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using static ClosedXML.Excel.XLPredefinedFormat;

/// <summary>
/// SalesOrderInSAP: Módulo para administrar pedidos en SAP a través de webservice
/// </summary>
public class SalesOrderInSAP
{
    public SalesOrderInSAP()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    static public async Task<json_respuestas> PutOrder(string id_pedido, string moneda_pedido)
    {
        HttpWebRequest request = CreateSOAPWebRequest();
        System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);
        decimal costoEnvio = PedidosEF.ObtenerNumeros(id_pedido).envio;
        DataTable dtPedido = pedidosProductos.obtenerProductos(id_pedido);
        if (dtPedido.Rows.Count > 0)
        {
            XmlDocument pedidoXML = PedidoXML(dtPedido, id_pedido, moneda_pedido, costoEnvio);
            if (pedidoXML != null)
            {
                using (Stream stream = request.GetRequestStream())
                {
                    pedidoXML.Save(stream);
                }
                using (WebResponse service = await request.GetResponseAsync())
                {
                    //Task<WebResponse> t = request.GetResponseAsync();
                    //t.Wait();
                    //WebResponse service = await t;
                    //WebResponse service = await request.GetResponseAsync();
                    using (StreamReader reader = new StreamReader(service.GetResponseStream()))
                    {
                        var serviceResult = reader.ReadToEnd();
                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml(serviceResult);
                        XmlNode idNode = xmlDocument.SelectSingleNode("//SalesOrder/ID");
                        if (idNode != null)
                        {
                            var actualizarIDPedidoSAP = PedidosEF.ActualizarIDPedidoSAP(id_pedido, idNode.InnerText);
                            if (actualizarIDPedidoSAP.result)
                            {
                                return new json_respuestas(true, "Pedido creado en SAP con el ID: " + idNode.InnerText + " y almacenado");
                            }
                            else
                            {
                                return new json_respuestas(true, "Pedido creado en SAP pero no almacenado, ID: " + idNode.InnerText);
                            }
                        }
                        return new json_respuestas(false, "Pedido NO creado en SAP");
                    }
                }
            }
        }
        return new json_respuestas(false, "No se encontraron productos en el pedido");
    }
    static private XmlDocument PedidoXML(DataTable dtPedido, string id_pedido, string moneda_pedido, decimal costo_envio)
    {
        string productosXML = string.Empty;
        int posicion = 10;
        foreach (DataRow producto in dtPedido.Rows)
        {
            string cantidad = producto["cantidad"].ToString();
            string precio_unitario = producto["precio_unitario"].ToString();
            string noParteSAP = producto["noParteSAP"].ToString();
            string descripcion = producto["descripcion"].ToString();
            if (descripcion.Length > 40)
            {
                descripcion = descripcion.Substring(0, 39);
            }
            productosXML += $@"
                    <Item actionCode=""04"">
                        <!-- ID de posición, debe incrementarse de 10 en 10 -->
                        <ID>{posicion}</ID>
                        <Description languageCode=""es"">{descripcion}</Description>
                        <!-- Liberar para ejecutar, corresponde al ATP? -->
                        <ReleaseToExecute>true</ReleaseToExecute>
                        <!-- Número de parte del producto pedido -->
                        <ItemProduct actionCode=""04"">
                            <ProductInternalID>{noParteSAP}</ProductInternalID>
                        </ItemProduct>
                        <!-- Línea de programación de árticulos, revisar -->
                        <ItemScheduleLine actionCode=""04"">
                            <ID>1</ID>
                            <TypeCode>1</TypeCode>
                            <!-- Cantidad de items solicitados en el pedido -->
                            <Quantity>{cantidad}</Quantity>
                        </ItemScheduleLine>
                        <!--Optional:-->
	                    <PriceAndTaxCalculationItem actionCode=""04"">
	                        <ItemMainPrice actionCode=""04"">
	                            <Rate>
	                            <DecimalValue>{precio_unitario}</DecimalValue>
	                            <CurrencyCode>{moneda_pedido}</CurrencyCode>
	                            <BaseDecimalValue>1.0</BaseDecimalValue>
	                            </Rate>
	                        </ItemMainPrice>
	                    </PriceAndTaxCalculationItem>
                    </Item>
                ";
            posicion += 10;
        }
        if (costo_envio > 0 && productosXML != null)
        {
            productosXML += $@"
                    <Item actionCode=""04"">
                        <!-- ID de posición, debe incrementarse de 10 en 10 -->
                        <ID>{posicion}</ID>
                        <Description languageCode=""es"">MANEJO DE MATERIAL</Description>
                        <!-- Liberar para ejecutar, corresponde al ATP? -->
                        <ReleaseToExecute>false</ReleaseToExecute>
                        <!-- Número de parte del producto pedido -->
                        <ItemProduct actionCode=""04"">
                            <ProductInternalID>MANEJO_DE_MATERIAL</ProductInternalID>
                        </ItemProduct>
                        <ItemServiceTerms actionCode=""04"">
	                      <ResourceID>ENVIO_CDMX</ResourceID>
	                   </ItemServiceTerms>
                        <!-- Línea de programación de árticulos, revisar -->
                        <ItemScheduleLine actionCode=""04"">
                            <ID>1</ID>
                            <TypeCode>1</TypeCode>
                            <!-- Cantidad de items solicitados en el pedido -->
                            <Quantity>1</Quantity>
                        </ItemScheduleLine>
                        <!--Optional:-->
	                    <PriceAndTaxCalculationItem actionCode=""04"">
	                        <ItemMainPrice actionCode=""04"">
	                            <Rate>
	                            <DecimalValue>{costo_envio}</DecimalValue>
	                            <CurrencyCode>{moneda_pedido}</CurrencyCode>
	                            <BaseDecimalValue>1.0</BaseDecimalValue>
	                            </Rate>
	                        </ItemMainPrice>
	                    </PriceAndTaxCalculationItem>
                    </Item>
                ";
        }
        if (productosXML != null)
        {
            string dateTime = utilidad_fechas.obtenerCentral().ToString("yyyy-MM-ddTHH:mm:ssZ");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml($@"
                    <soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:glob=""http://sap.com/xi/SAPGlobal20/Global"" xmlns:glob1=""http://sap.com/xi/AP/Globalization"" xmlns:a0v=""http://sap.com/xi/AP/CustomerExtension/BYD/A0VKF"">
                       <soap:Header/>
                       <soap:Body>
                          <glob:SalesOrderBundleMaintainRequest_sync>
                             <BasicMessageHeader>
                             </BasicMessageHeader>
		                    <SalesOrder actionCode=""01"">
                                    <!-- Referencia externa -->
                                    <BuyerID>{id_pedido}</BuyerID>
                                        <!-- NODO CAMPAÑA -->
                                    <!-- Unidad de venta, 101024 corresponde a TERCEROS -->
                                    <SalesUnitParty actionCode=""04"">
                                        <PartyID>101024</PartyID>
                                    </SalesUnitParty>
                                    <!-- Canal de distribución, Z4 corresponde a Ecommerce -->
                                    <SalesAndServiceBusinessArea actionCode=""04"">
                                        <DistributionChannelCode>Z4</DistributionChannelCode>
                                    </SalesAndServiceBusinessArea>
                                    <!-- Empleado responsable, 1000 corresponde a Francisco -->
                                    <EmployeeResponsibleParty actionCode=""04"">
                                        <PartyID>8065</PartyID>
                                    </EmployeeResponsibleParty>
                                    <!-- Nombre de cliente -->
                                    <AccountParty actionCode=""04"">
                                    <!-- ID de cliente INCOM.MX: 1001205 | CLIENTE MOSTRADOR ML: 1017775 -->
                                        <PartyID>1001205</PartyID>
                                    </AccountParty>
                                    <!-- Términos de venta -->
                                    <PricingTerms actionCode=""04"">
                                        <CurrencyCode>{moneda_pedido}</CurrencyCode>
                                        <!-- Fecha y hora de operación -->
                                        <PriceDateTime timeZoneCode=""UTC"">{dateTime}</PriceDateTime>
                                        <!-- false indica que el desglose de impuestos lo realiza SAP, en caso de enviar monto bruto deben enviarse los nodos correspondientes -->
                                        <GrossAmountIndicator>false</GrossAmountIndicator>
                                    </PricingTerms>
                                    <!-- Nodo Item en la que se indican las posiciones -->
                                        {productosXML}
                                </SalesOrder>
                          </glob:SalesOrderBundleMaintainRequest_sync>
                       </soap:Body>
                    </soap:Envelope>
                ");
            return xmlDoc;
        }
        else
        {
            return null;
        }
    }
    static protected HttpWebRequest CreateSOAPWebRequest()
    {
        string host = HttpContext.Current.Request.Url.Host;
        HttpWebRequest request;
        if (host == "localhost" || host == "test1.incom.mx")
        {
            request = (HttpWebRequest)WebRequest.Create(@"https://my429740.businessbydesign.cloud.sap/sap/bc/srt/scs/sap/managesalesorderin5?sap-vhost=my429740.businessbydesign.cloud.sap");
            request.Credentials = new NetworkCredential("ASALINAS", "Pruebas00");
        }
        else
        {
            request = (HttpWebRequest)WebRequest.Create(@"https://my338095.sapbydesign.com/sap/bc/srt/scs/sap/managesalesorderin5?sap-vhost=my338095.sapbydesign.com");
            request.Credentials = new NetworkCredential("INCOM.MX", "Sistema$20.22");
        }
        request.ContentType = "application/soap+xml;charset='utf-8'";
        request.Method = "POST";
        return request;
    }
    static protected bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
}
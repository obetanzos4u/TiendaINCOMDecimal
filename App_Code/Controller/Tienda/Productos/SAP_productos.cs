using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;

/// <summary>
/// Conjunto de herramientas para consumir información en tiempo real de productos SAP BYDesign
/// </summary>
/// 
public class SAP_productos : SAP_conexion
{

    // [PRUEBAS] https://???.sapbydesign.com
    // [PRODUCTIVO]  https://my338095.sapbydesign.com/
    string numero_parte { get; set; }
    public int? cantidadRequeridoUsuario { get; set; }

    NumberFormatInfo myNumberFormatInfo = new CultureInfo("es-MX", true).NumberFormat;
    public SAP_productos(string _numero_parte)
    {
        numero_parte = _numero_parte.ToUpper();
    }
    public SAP_productos(string _numero_parte, int _cantidadRequeridoUsuario)
    {
        numero_parte = _numero_parte.ToUpper();
        cantidadRequeridoUsuario = _cantidadRequeridoUsuario;
    }


    /// <summary>
    /// Obtiene una relación de PlanningAreaID con su respectivo nombre
    /// </summary>
    /// 
    public static List<PlanningArea> obtenerPlanningArea()
    {
        PlanningArea _1 = new PlanningArea{ PlanningAreaID = "1", PlanningAreaName = "CDMX"};
        PlanningArea _101 = new PlanningArea { PlanningAreaID = "101", PlanningAreaName = "CDMX" };
        PlanningArea _102 = new PlanningArea { PlanningAreaID = "102", PlanningAreaName = "MONTERREY" };
        PlanningArea _103 = new PlanningArea { PlanningAreaID = "103", PlanningAreaName = "GUADALAJARA" };
        PlanningArea _104 = new PlanningArea { PlanningAreaID = "104", PlanningAreaName = "KM100" };
        PlanningArea _105 = new PlanningArea { PlanningAreaID = "105", PlanningAreaName = "P300 METALICO" };
        PlanningArea _201 = new PlanningArea { PlanningAreaID = "201", PlanningAreaName = "PLUTARCO 300" };

        List<PlanningArea> PlanningAreas = new List<PlanningArea>();
        PlanningAreas.Add(_1);
        PlanningAreas.Add(_101);
        PlanningAreas.Add(_102);
        PlanningAreas.Add(_103);
        PlanningAreas.Add(_104);
        PlanningAreas.Add(_105);
        PlanningAreas.Add(_201);
        PlanningAreas.Add(_1);

        return PlanningAreas;
    }

    public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
    {
        return true;
    }
    public  XDocument obtenerXML_querymaterialin()
    {
        try { 
         
        //Calling CreateSOAPWebRequest method  
        HttpWebRequest request = CreateSOAPWebRequest(@"https://my338095.sapbydesign.com/sap/bc/srt/scs/sap/querymaterialin?sap-vhost=my338095.sapbydesign.com");



            System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


            XmlDocument SOAPReqBody = new XmlDocument();
        //SOAP Body Request  
        SOAPReqBody.LoadXml(@"<soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:glob=""http://sap.com/xi/SAPGlobal20/Global"" xmlns:a0v=""http://sap.com/xi/AP/CustomerExtension/BYD/A0VKF"">
   <soap:Header/>
   <soap:Body>
      <glob:MaterialByElementsQuery_sync>
         <!--Optional:-->
         <MaterialSelectionByElements>
            <SelectionByInternalID>
               <InclusionExclusionCode>I</InclusionExclusionCode>
               <IntervalBoundaryTypeCode>1</IntervalBoundaryTypeCode>
               <LowerBoundaryInternalID schemeID=""?"" schemeAgencyID =""?"">" + numero_parte + @"</LowerBoundaryInternalID> 
            </SelectionByInternalID>
         </MaterialSelectionByElements>
         <ProcessingConditions>
            <QueryHitsMaximumNumberValue>100</QueryHitsMaximumNumberValue>
            <QueryHitsUnlimitedIndicator>false</QueryHitsUnlimitedIndicator>
         </ProcessingConditions>
      </glob:MaterialByElementsQuery_sync>
    </soap:Body>
</soap:Envelope>");


        using (Stream stream = request.GetRequestStream())
        {
            SOAPReqBody.Save(stream);
        }
        //Geting response from request  
        using (WebResponse Serviceres = request.GetResponse())
        {
            using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
            {

                var ServiceResult = rd.ReadToEnd();






                XDocument doc = XDocument.Parse(ServiceResult);

                return doc;

            }
        }
        }
        catch(Exception ex)
        {
            return null;
        }
    }

    /// <summary>
    /// Obtiene informacion del producto y sus ubicaciones
    /// </summary>
    /// 
    public List<productoSAPModel> obtenerProductoStock()
    {
        List<productoSAPModel> productoDisponibilidades = new List<productoSAPModel>();

        SAP_productos obtener = new SAP_productos(numero_parte);

        XDocument XMLproducto = obtener.obtenerXML_querymaterialin();

        decimal? totalDisponibleUbicacionesTodas = 0;

        System.Globalization.NumberFormatInfo myNumberFormatInfo = new System.Globalization.CultureInfo("es-MX", true).NumberFormat;
        string tipoUsuario = usuarios.userLogin().tipo_de_usuario;
        if (XMLproducto != null)
        {


            var query = XMLproducto.Descendants("PlanningAreaID");

            foreach (var item in query)
            {

                string PlanningAreaID = item.Value;
                XDocument disponibilidad = obtener.obtenerXML_productavailabilitydeterminati(PlanningAreaID);

                var CurrentStockQuantity = disponibilidad.Descendants("CurrentStockQuantity").First();
                var RequirementQuantity = disponibilidad.Descendants("RequirementQuantity").First();

                productoSAPModel producto = new productoSAPModel();
                producto.numero_parte = numero_parte;
                producto.PlanningAreaID = PlanningAreaID;


                producto.PlanningAreaName = obtenerPlanningArea().Find(x => x.PlanningAreaID == PlanningAreaID).PlanningAreaName;

                
                producto.CurrentStockQuantity = decimal.Parse(CurrentStockQuantity.Value);
                producto.RequirementQuantity = decimal.Parse(RequirementQuantity.Value);
                producto.totalDisponible = producto.CurrentStockQuantity - producto.RequirementQuantity;
                producto.totalDisponible_str = string.Format( producto.totalDisponible.ToString(),"C2", myNumberFormatInfo);

                totalDisponibleUbicacionesTodas += producto.totalDisponible;

                if (tipoUsuario == "cliente")
                {
                  
                    // Si el total disponible es mayor a [0] hacemos cálculos
                    if (producto.totalDisponible > 0) {

                        // Si se establecio el requerido por el usuario mostramos únicamente las piezas disponibles
                        if(cantidadRequeridoUsuario != null)
                        {
                            if (producto.totalDisponible > cantidadRequeridoUsuario) {
                                producto.totalDisponible_str = "Tus "+ cantidadRequeridoUsuario + " unidades estan disponibles en este almacén.";

                                if (cantidadRequeridoUsuario == 1)
                                {
                                    producto.totalDisponible_str = cantidadRequeridoUsuario + " pieza requerida esta disponible en este almacén.";
                                }
                            }
                            else
                            {
                                producto.totalDisponible_str = "Cotiza para saber el tiempo de entrega";
                            }

                          
                        }
                        else
                        {
                            producto.totalDisponible_str = "Disponible";
                        }
                        

                      
                    }
                    else
                    {
                        producto.totalDisponible_str = "Cotiza";

                    }

                    producto.totalDisponible = null;
                    producto.CurrentStockQuantity = null;
                    producto.RequirementQuantity = null;

                }
                    productoDisponibilidades.Add(producto);
            }

            /* INICIO - [Clientes] - Suma la cantidad de todos los almacenes y muestra solo uno. ¡Importante! solo es para clientes */

            if (tipoUsuario == "cliente")
            {
                List<productoSAPModel> productoDisponibilidadesSumatoria = new List<productoSAPModel>();

                productoSAPModel producto = new productoSAPModel();
                producto.numero_parte = numero_parte;
                producto.PlanningAreaID = "All";


                producto.PlanningAreaName = "Almacén Incom";


                if (totalDisponibleUbicacionesTodas > 0) {

                        if (totalDisponibleUbicacionesTodas >= cantidadRequeridoUsuario)
                        {
                            producto.totalDisponible_str = "Tus " + cantidadRequeridoUsuario + " unidades estan disponibles en este almacén.";

                            if (cantidadRequeridoUsuario == 1)
                            {
                                producto.totalDisponible_str = cantidadRequeridoUsuario + " pieza requerida esta disponible en este almacén.";
                            }
                        }
                        else
                        {
                            if (cantidadRequeridoUsuario == 1)
                                producto.totalDisponible_str = cantidadRequeridoUsuario + " pieza requerida esta disponible en este almacén.";
                           else 
                                producto.totalDisponible_str = totalDisponibleUbicacionesTodas +" piezas están disponibles en este almacén.";
                        }


                }
                else
                {
                    producto.totalDisponible_str = "0";


                }

                producto.totalDisponible = null;

                productoDisponibilidadesSumatoria.Add(producto);

                return productoDisponibilidadesSumatoria;
            }
            /* FIN  [Clientes] */

            return productoDisponibilidades;
        }

        return productoDisponibilidades;
    }
        public  XDocument obtenerXML_productavailabilitydeterminati(string PlanningAreaID)
    {

        //Calling CreateSOAPWebRequest method  
        HttpWebRequest request = CreateSOAPWebRequest(@"https://my338095.sapbydesign.com/sap/bc/srt/scs/sap/productavailabilitydeterminati?sap-vhost=my338095.sapbydesign.com");



        System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications);

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        XmlDocument SOAPReqBody = new XmlDocument();
        //SOAP Body Request  
        SOAPReqBody.LoadXml(@"<soap:Envelope xmlns:soap=""http://www.w3.org/2003/05/soap-envelope"" xmlns:glob=""http://sap.com/xi/SAPGlobal20/Global"">
            <soap:Header/>
            <soap:Body>
               <glob:ProductAvailabilityDeterminationQuery_sync>
                  <ProductAvailabilityDeterminationQuery>
                     <ConsiderScopeOfCheckIndicator>true</ConsiderScopeOfCheckIndicator>
                     <!--1 or more repetitions: -->
                     <ProductAndSupplyPlanningArea>
                        <!--Optional:-->
                        <ProductInternalID schemeID=""?"" schemeAgencyID=""?"">" + numero_parte + @"</ProductInternalID>
                        <ProductTypeCode>1</ProductTypeCode>
                        <SupplyPlanningAreaID schemeID=""?"" schemeAgencyID=""?"">" + PlanningAreaID + @"</SupplyPlanningAreaID>
                     </ProductAndSupplyPlanningArea>
                  <ProductAvailabilityDeterminationHorizonDuration>P365D</ProductAvailabilityDeterminationHorizonDuration>
                  </ProductAvailabilityDeterminationQuery>
               </glob:ProductAvailabilityDeterminationQuery_sync>
            </soap:Body>
         </soap:Envelope>");


        using (Stream stream = request.GetRequestStream())
        {
            SOAPReqBody.Save(stream);
        }
        //Geting response from request  
        using (WebResponse Serviceres = request.GetResponse())
        {
            using (StreamReader rd = new StreamReader(Serviceres.GetResponseStream()))
            {

                var ServiceResult = rd.ReadToEnd();


                XDocument doc = XDocument.Parse(ServiceResult);

                return doc;

            }
        }

    }

}

public class productoSAPModel
{
    public string numero_parte { get; set; }

    public string PlanningAreaID { get; set; }
    public string PlanningAreaName { get; set; }
    
    public decimal? CurrentStockQuantity { get; set; }


    public decimal? RequirementQuantity { get; set; }

    public decimal? totalDisponible { get; set; }
    public string totalDisponible_str { get; set; }
}

public class PlanningArea
{
    public string PlanningAreaID { get; set; }
    public string PlanningAreaName { get; set; }
}
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Xml;

/// <summary>
/// Descripción breve de SantanderLiga
/// </summary>
public class SantanderLigaCobro
{
    private string id_companySANDBOX = "SNBX";
    private string id_branchSANDBOX = "01SNBXBRNCH";
    private string userSANDBOX = "SNBXUSR01";
    private string pwdSANDBOX = "SECRETO";
    private string KeyCifradoSANDBOX = "5DCC67393750523CD165F17E1EFADD21";
    private string data0SANDBOX = "SNDBX123";
    //public string UrlSANDBOX = "https://wppsandbox.mit.com.mx/";
    public string UrlSANDBOX = "https://sandbox.mit.com.mx/";
    private readonly string _ResourceSANDBOX = "gen";

    /***     Productivo MXN // AFILIACION - 8487094 MXN  ***/
    private string id_companyPRODUCTIVO = "01HA";
    private string id_branchPRODUCTIVO_MXN = "0001";
    private string userPRODUCTIVO_MXN = "01HASIUS0";
    private string pwdPRODUCTIVO_MXN = "B8VUH3SM36";

    /***     Productivo USD // AFILIACION - 8487094 MXN  ***/
    private string id_branchPRODUCTIVO_USD = "0003";
    private string userPRODUCTIVO_USD = "01HASIUS1";
    private string pwdPRODUCTIVO_USD = "ZHY8EVPKXI";
    private string KeyCifradoPRODUCTIVO = "72CD51410E8BF2930432D71F93431323";
    private string data0PRODUCTIVO = "9265655591";
    public string UrlPRODUCTIVO = "https://bc.mitec.com.mx/";
    private readonly string _ResourcePRODUCTIVO = "p/gen";
    private string cadenaXML { get; set; }
    public string URL { get; set; }
    public SantanderLigaCobro(string reference, decimal amount, string moneda, SantanderData3ds Data3ds, DateTime Vigencia, string emailCliente)
    {
        amount = Decimal.Round(amount, 2);
        var id_company = "";
        var id_branch = "";
        var user = "";
        var pwd = "";

        string host = HttpContext.Current.Request.Url.Host;

        if (host == "localhost" || host == "test1.incom.mx")
        //   if(true)
        {
            id_company = id_companySANDBOX;
            id_branch = id_branchSANDBOX;
            user = userSANDBOX;
            pwd = pwdSANDBOX;
        }
        else
        {
            id_company = id_companyPRODUCTIVO;

            if (moneda == "MXN")
            {
                id_branch = id_branchPRODUCTIVO_MXN;
                user = userPRODUCTIVO_MXN;
                pwd = pwdPRODUCTIVO_MXN;
            }

            if (moneda == "USD")
            {
                id_branch = id_branchPRODUCTIVO_USD;
                user = userPRODUCTIVO_USD;
                pwd = pwdPRODUCTIVO_USD;
            }


        }
        cadenaXML = $@"
                            <P>
                              <business>
                                <id_company>{id_company}</id_company>
                                <id_branch>{id_branch}</id_branch>
                                <user>{user}</user>
                                <pwd>{pwd}</pwd>
                              </business>
                              <url>
                                <reference>{reference}</reference>
                                <amount>{amount}</amount>
                                <moneda>{moneda}</moneda>
                                <canal>W</canal>
                                <omitir_notif_default>1</omitir_notif_default>
                                <promociones>C</promociones>
                                <st_correo>1</st_correo>
                                <fh_vigencia>{Vigencia.ToString("dd/MM/yyy")}</fh_vigencia>
                                <mail_cliente>{emailCliente}</mail_cliente>

                              </url>
                            </P>";

        /*      <data3ds>
        <ml>{Data3ds.ml}</ml>
        <cl>{Data3ds.cl}</cl>
        <dir>{Data3ds.dir}</dir>
        <cd>{Data3ds.cd}</cd>
        <est>{Data3ds.est}</est>
        <cp>{Data3ds.cp}</cp>
        <idc>{Data3ds.idc}</idc>
      </data3ds>*/

        //  devNotificaciones.notificacionSimple(cadenaXML);
    }

    public async Task GenerarLigaAsync()
    {

        string data0 = "";
        string key = "";

        string host = HttpContext.Current.Request.Url.Host;


        var client = new RestClient();
        var request = new RestRequest();


        if (host == "localhost" || host == "test1.incom.mx")
        // if (true)
        {

            key = KeyCifradoSANDBOX;
            data0 = data0SANDBOX;

            client = new RestClient(UrlSANDBOX);
            request = new RestRequest(_ResourceSANDBOX, Method.POST);
        }
        else
        {
            key = KeyCifradoPRODUCTIVO;
            data0 = data0PRODUCTIVO;

            client = new RestClient(UrlPRODUCTIVO);
            request = new RestRequest(_ResourcePRODUCTIVO, Method.POST);
        }



        string encryptedString = AESCrypto.encrypt(cadenaXML, key);





        request.AddHeader("cache-control", "no-cache");
        request.AddHeader("content-type", "application/x-www-form-urlencoded");
        request.AddParameter("xml", $"<pgs><data0>{data0}</data0><data>{encryptedString}</data></pgs>");


        var response = await client.ExecuteAsync(request);
        var content = response.Content;


        string decryptedString = AESCrypto.decrypt(key, content);


        /*
            <P_RESPONSE>
            <cd_response>success</cd_response>
            <nb_response></nb_response>
            <nb_url>https://wppsandbox.mit.com.mx/i/AA9TD7MP</nb_url>
        </P_RESPONSE>
         */


        if (string.IsNullOrWhiteSpace(decryptedString)) URL = null;
        else
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(decryptedString);


            XmlNodeList nb_response = xmlDoc.GetElementsByTagName("nb_response");

            string str_nb_response = nb_response[0].InnerXml;

            // La petición fue procesada correctamente si response es vacía.
            if (!string.IsNullOrEmpty(str_nb_response))
            {
                throw new Exception(str_nb_response);


            }
            else
            {
                XmlNodeList nb_url = xmlDoc.GetElementsByTagName("nb_url");
                URL = nb_url[0].InnerXml;


            }

        }



    }
}

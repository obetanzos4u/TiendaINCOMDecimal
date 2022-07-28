using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _pruebas_Corrector : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_corregir_Click(object sender, EventArgs e)
    {

        string busqueda = txt_busqueda.Text;
        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        var client = new RestClient("https://api.bing.microsoft.com/v7.0/spellcheck");
        var request = new RestRequest(Method.POST);
       
        request.AddHeader("Ocp-Apim-Subscription-Key", "9e6df3607a554bcb8a066fb62b5032c1");
        request.AddParameter("text", busqueda);

           request.AddParameter("Accept-language", "es-MX");
          request.AddParameter("CountryCode", "MX");
          request.AddParameter("SetLang","es-MX");
        request.AddParameter("Mode", "spell");
        

       //       request.AddQueryParameter("text", txt_busqueda.Text);




        var response = client.Post(request);

        var result = response.Content;



        txt_response.Text = result;


        var correccion = Newtonsoft.Json.JsonConvert.DeserializeObject<Corrector>(result);


        int totalCorreciones = correccion.flaggedTokens.Count;

        foreach (var t in correccion.flaggedTokens)
        {

            busqueda= busqueda.Replace(t.token, t.suggestions[0].suggestion);
        }

        lbl_corregido.Text = busqueda;
    }
}

public class Suggestion
{
    public string suggestion { get; set; }
    public int score { get; set; }
}

public class FlaggedToken
{
    public int offset { get; set; }
    public string token { get; set; }
    public string type { get; set; }
    public List<Suggestion> suggestions { get; set; }
}

public class Corrector
{
    public string _type { get; set; }
    public List<FlaggedToken> flaggedTokens { get; set; }
    public string correctionType { get; set; }
}



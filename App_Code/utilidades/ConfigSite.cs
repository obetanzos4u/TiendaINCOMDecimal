
using System.Collections.Specialized;
using System.Configuration;


/// <summary>
/// Herramienta para obtener algunas configuraciones de el sitio
/// </summary>
public class ConfigSite {
   readonly static NameValueCollection appSettings = ConfigurationManager.AppSettings;
    public ConfigSite() {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }

    static public string ObtenerUrl() {

        string modo = appSettings["mode"];

        switch (modo) {
            case "Local": return ObtenerUrlLocal();
            case "Pruebas": return ObtenerUrlPruebas(); 
            case "Productivo": return ObtenerUrlProductivo();
            default: return ObtenerUrlProductivo();  
        }

     
    }
    static private string ObtenerUrlLocal() {
        return appSettings["UrlLocal"];
    }

    static private string ObtenerUrlProductivo() {
        return appSettings["UrlProductivo"];
    }

    static private string ObtenerUrlPruebas() {
        return appSettings["UrlPruebas"];
    }
}
using PayPalHttp;
using System;
using System.IO;
using PayPalCheckoutSdk.Core;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

/// <summary>
/// Descripción breve de PayPalClient
/// </summary>

public class PayPalClient
    {
    /**
        Set up PayPal environment with sandbox credentials.
        In production, use LiveEnvironment.
     */

    public static string client_id_Productivo  = "ASb_WuH0ark2vYRZwrVj45RzPxgFwHsL7uASDooPJxNefVNFxk30viAZcCI6bbiMaL76Xaa9REGvnROn";
    public static string client_id_Sandbox = "ARzpJ4mrRiUxniBFqNuxj_Yw-IBxYcAtClpXadJ4W_mPPPNDQqEv_Yjxu3p1jGfS-Wv1HQR_CVO1fjO5";
    public static PayPalEnvironment environment()
        {
        return new LiveEnvironment("ASb_WuH0ark2vYRZwrVj45RzPxgFwHsL7uASDooPJxNefVNFxk30viAZcCI6bbiMaL76Xaa9REGvnROn",
                  "EGtv4f5exHgzwacg0LA29wAlAFzkrSdT5NxOle__eV4bJa0LX7yX5VB48nlGyg1yIBZlKfrCh_h-Y1QD");



        return new SandboxEnvironment("ARzpJ4mrRiUxniBFqNuxj_Yw-IBxYcAtClpXadJ4W_mPPPNDQqEv_Yjxu3p1jGfS-Wv1HQR_CVO1fjO5",
             "EJLuvAy76w2vZc2AIq5CuRXmKTgwNSL9zugRjF37mGJnnLbMcW3kGWu0kzc4OKxVRqt2Z0bWLd3O-gm7");
      
       
     
    }
    /**
          Returns PayPalHttpClient instance to invoke PayPal APIs.
       */
    public static HttpClient client()
    {
        return new PayPalHttpClient(environment());
    }

    public static HttpClient client(string refreshToken)
    {
        return new PayPalHttpClient(environment(), refreshToken);
    }

    /**
        Use this method to serialize Object to a JSON string.
    */
    public static String ObjectToJSONString(Object serializableObject)
    {
        MemoryStream memoryStream = new MemoryStream();
        var writer = JsonReaderWriterFactory.CreateJsonWriter(
                    memoryStream, Encoding.UTF8, true, true, "  ");
        DataContractJsonSerializer ser = new DataContractJsonSerializer(serializableObject.GetType(), new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true });
        ser.WriteObject(writer, serializableObject);
        memoryStream.Position = 0;
        StreamReader sr = new StreamReader(memoryStream);
        return sr.ReadToEnd();
    }
}

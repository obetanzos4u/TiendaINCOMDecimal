using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;

public class SantanderController : ApiController
{

    private string key = "72CD51410E8BF2930432D71F93431323";

    // GET api/<controller>
    public IEnumerable<string> Get()
    {
        return new string[] { "null" };
    }

    // GET api/<controller>/5
    public string Get(int id)
    {
        return "null";
    }

    // POST api/<controller>
    public void Post()
    {
        var strResponse = System.Web.HttpContext.Current.Request.Params["strResponse"];



        if (!string.IsNullOrWhiteSpace(strResponse))
        {
            string finalString = null;



            XmlDocument xmlDoc = new XmlDocument();


            XmlNodeList nb_response = null;
            XmlNodeList reference = null;
            XmlNodeList amount = null;

            string response = null;
            string numero_operacion = null;
            string monto = null;
            try
            {

                //    strResponse = System.Web.HttpUtility.UrlDecode(strResponse);

                finalString = AESCrypto.decrypt(key, strResponse);



                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(finalString);

                nb_response = xmlDoc.GetElementsByTagName("response");
                reference = xmlDoc.GetElementsByTagName("reference");
                amount = xmlDoc.GetElementsByTagName("amount");

                response = nb_response[0].InnerXml;
                numero_operacion = reference[0].InnerXml;
                monto = amount[0].InnerXml;

            }

            catch (Exception ex)
            {
                devNotificaciones.ErrorSQL("Error al leer una respuesta de pago Santander: " + finalString + "<br> STR: <br>" + System.Web.HttpContext.Current.Request.Params["strResponse"] + "<br>" + ex, ex, "");
                devNotificaciones.error("Error al leer una respuesta de pago Santander", finalString + "<br> STR: <br>" + System.Web.HttpContext.Current.Request.Params["strResponse"] + "<br>" + ex.ToString());
                return;
            }
            using (var db = new tiendaEntities())
            {
                using (DbContextTransaction transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.pedidos_pagos_respuesta_santander.Add(new pedidos_pagos_respuesta_santander()
                        {
                            fecha_primerIntento = utilidad_fechas.obtenerCentral(),
                            numero_operacion = numero_operacion,
                            response = finalString,
                            estatus = response
                        });

                        db.SaveChanges();
                        transaction.Commit();

                        switch (response)
                        {
                            case "approved":
                                response = "Aprovado";
                                break;
                            case "denied":
                                response = "Rechazado";
                                break;
                        }

                        SantanderResponse.enviarEmail(numero_operacion, response, monto);


                    }
                    catch (Exception ex)
                    {

                        transaction.Rollback();
                        devNotificaciones.ErrorSQL("Error al guardar una respuesta de pago Santander: " + finalString + "<br>" + ex, ex, "");
                        devNotificaciones.error("Error al guardar una respuesta de pago Santander", finalString + "<br>" + ex.ToString());


                    }
                }
            }
        }


    }

    // PUT api/<controller>/5
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<controller>/5
    public void Delete(int id)
    {
    }
}

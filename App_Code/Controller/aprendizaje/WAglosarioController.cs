using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

public class WAglosarioController : ApiController {
    // GET api/<controller>

    public IEnumerable<glosario> Get() {


        using (var db = new tiendaEntities()) {
            var file_adjunto = db.glosario

                .Where(s => s.término.StartsWith("a")).ToList();

            return file_adjunto;
        }
           // return new string[] { "value1", "value2" };
    }


    /*  public HttpResponseMessage get([FromBody]glosario order) {
          if (order != null) {
              return Request.CreateResponse<glosario>(HttpStatusCode.Created, order);
          }
          return Request.CreateResponse(HttpStatusCode.BadRequest);
      }

       */


    // GET api/<controller>/5
    public string Get(int id) {
        return "value";
    }

    // POST api/<controller>
    public void Post([FromBody]string value) {
    }

    // PUT api/<controller>/5
    public void Put(int id, [FromBody]string value) {
    }

    // DELETE api/<controller>/5
    public void Delete(int id) {
    }
}

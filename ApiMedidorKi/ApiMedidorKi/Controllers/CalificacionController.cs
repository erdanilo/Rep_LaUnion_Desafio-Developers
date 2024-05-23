using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiMedidorKi.Controllers
{
    public class CalificacionController : ApiController
    {
        // GET: api/Luchador
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Luchador/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Luchador
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Luchador/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Luchador/5
        public void Delete(int id)
        {
        }
    }
}

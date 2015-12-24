using Interpreter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Interpreter.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InterpreterController : ApiController
    {
        // POST api/values      
        public int  Post([FromBody] string content)
        {
            return Operation.RunExpression(new Context() { Formula=content.ToUpper()});
        }      
    }
}

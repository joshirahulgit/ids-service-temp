using Scheduler.Business.Entity;
using Scheduler.Business.Implementation;
using Scheduler.Business.Specification;
using Scheduler.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Scheduler.Application.Controllers
{
    public class AccountController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(string enumType)
        {
            IRequestContext rc = new RequestContext("gdeleon", "!password!", 92749, "solismammotest", null, "", "");
            GlobalContext.Add(rc);

            IAccountService accSer = ServiceFactory<IAccountService>.CreateNew();
            var hcpcsEnums = accSer.GetAccountEnumsByType(enumType);
            return Ok(hcpcsEnums);
        }

        // POST api/<controller>
        public void Post([FromBody]AccountEnumDto accountEnumDto)
        {
            IRequestContext rc = new RequestContext("gdeleon", "!password!", 92749, "solismammotest", null, "", "");
            GlobalContext.Add(rc);

            IAccountService accSer = ServiceFactory<IAccountService>.CreateNew();
            AccountEnumsDto accEnums = new AccountEnumsDto()
            {
                AccountEnums = new List<AccountEnumDto>()
            };
            accEnums.AccountEnums.Add(accountEnumDto);
            accSer.InsertUpdateAccountEnum(accEnums);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
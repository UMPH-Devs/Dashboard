using System;
using System.Linq;
using System.Web.Http;
using Dashboard.Entities;
using Dashboard.Models;
using Dashboard.Extensions;
using Dashboard.Handlers;
using UCM;

namespace Dashboard.Controllers
{
    [RoutePrefix("api")]
    public partial class RouteController : ApiController
    {
        private IHttpActionResult trySend<T>(Response<T> res, bool EmptySuccess = false)
        {
            if (res.HasError)
            {
                return BadRequest(res.Error);
            }
            if (EmptySuccess)
            {
                return Ok();
            }
            return Ok(res.OK);
        }

        [Route("moduletypes/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetModuleType(int id)
        {
            var res = RequestHandler.Send(new GetModuleType(id));
            return Ok(res);
        }

        [Route("moduletypes")]
        [HttpPost]
        public IHttpActionResult PostModuleType(RefModuleType module)
        {
            var res = RequestHandler.Send(new AddModuleType(module));
            return trySend(res, true);
        }

        [Route("moduletypes")]
        [HttpPut]
        public IHttpActionResult PutModuleType(RefModuleType module)
        {
            var res = RequestHandler.Send(new UpdateModuleType(module));
            return trySend(res);
        }

        [Route("moduletypes/{id}")]
        [HttpDelete]
        public IHttpActionResult DelModuleType(int id)
        {
            RequestHandler.Send(new DeleteModuleType(id));
            return Ok();
        }

        [Route("moduletypes")]
        [HttpGet]
        public IHttpActionResult GetModuleTypes()
        {
            var res = RequestHandler.Send(new GetModuleTypes());
            return Ok(res);
        }



        [Route("modules/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetModule(int id)
        {
            var res = RequestHandler.Send(new GetModule(id));
            return Ok(res);
        }

        [Route("modules")]
        [HttpPost]
        public IHttpActionResult PostModules(ModuleStatu module)
        {
            var res = RequestHandler.Send(new AddModuleStatus(module));
            return trySend(res);

        }

        [Route("modules")]
        [HttpGet]
        public IHttpActionResult GetModules()
        {
            var statuses = RequestHandler.Send(new GetModules());
            return Ok(statuses);
        }

        [Route("modules/history/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetModuleHistory(int id)
        {
            var history = RequestHandler.Send(new GetModuleHistory(id));
            return Ok(history);
        }

        [Route("modules/status/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetModuleStatus(int id)
        {
            var status = RequestHandler.Send(new GetModuleStatus(id));
            return Ok(status);
        }

        [Route("modules/status")]
        [HttpGet]
        public IHttpActionResult GetOverallStatus()
        {
            var status = RequestHandler.Send(new GetOverallStatus());
            return Ok(status);
        }

        [Route("modules/success")]
        [HttpPatch]
        public IHttpActionResult ForceModuleGreen(JsonModuleStatus mod)
        {
            var status = RequestHandler.Send(new UpdateModuleSuccess(mod));
            return Ok(status);
        }

        [Route("modules/progress")]
        [HttpPatch]
        public IHttpActionResult UpdateModuleInProgress(JsonModuleStatus mod)
        {
            var status = RequestHandler.Send(new UpdateModuleInProgress(mod));
            return Ok(status);
        }

        [Route("statusitem/progress")]
        [HttpPost]
        public IHttpActionResult UpdateStatusItemInProgress(StatusItem si)
        {
            var i = RequestHandler.Send(new UpdateStatusItemProgress(si));
            return Ok(si);
        }

        [Route("env")]
        [HttpGet]
        public IHttpActionResult GetEnv()
        {
            var res = RequestHandler.Send(new GetEnv());
            return Ok(res);
        }
    }
}

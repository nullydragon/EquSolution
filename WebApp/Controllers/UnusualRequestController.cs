using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApp.Models;
using WebApp.Repository;
using System.Linq;

namespace WebApp.Controllers
{
    public class UnusualRequestController : ApiController
    {
        public IRequestRepository RequestRepo { get; set; }
        
        public UnusualRequestController()
        {
            RequestRepo = new RequestRepositiory();
        }

        public IEnumerable<UnusualRequests> GetAll()
        {
            return RequestRepo.GetAll().OrderByDescending(r => r.Occurrence);
        }
        
        public UnusualRequests GetById(int id)
        {
            var req = RequestRepo.GetById(id);
            if (req == null)
            {
                return null;
            }

            return req;
        }

        [HttpPost]
        public IHttpActionResult Create([FromBody] UnusualRequests req)
        {
            if (req == null)
            {
                //Cannot create with no data
                return BadRequest();
            }
            RequestRepo.Create(req);
			return new System.Web.Http.Results.OkResult(this);
        }

        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody] UnusualRequests req)
        {
            if (req == null)
            {
                //need data to process an update
                return BadRequest();
            }

            if (RequestRepo.GetById(id) == null)
            {
                //This record does not exist
                return NotFound();
            }

            RequestRepo.Update(req);
            return new System.Web.Http.Results.OkResult(this);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            RequestRepo.Remove(id);
        }
    }
}

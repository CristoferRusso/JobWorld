// Controllers/JobController.cs
using Microsoft.AspNetCore.Mvc;
using JobWorld.Models;
using System.Collections.Generic;

namespace JobWorld.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        // Simulazione di un database
        private static List<Job> jobs = new List<Job>();

        [HttpGet]
        public ActionResult<IEnumerable<Job>> GetJobs()
        {
            return Ok(jobs);
        }

        [HttpPost]
        public ActionResult<Job> CreateJob(Job job)
        {
            job.Id = jobs.Count + 1; // Logica semplice per generare un ID
            jobs.Add(job);
            return CreatedAtAction(nameof(GetJobs), new { id = job.Id }, job);
        }
    }
}

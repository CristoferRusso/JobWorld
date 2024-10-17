// Controllers/InterviewController.cs
using Microsoft.AspNetCore.Mvc;
using JobWorld.Models;
using System.Collections.Generic;

namespace JobWorld.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InterviewController : ControllerBase
    {
        // Simulazione di un database
        private static List<Interview> interviews = new List<Interview>();

        [HttpGet]
        public ActionResult<IEnumerable<Interview>> GetInterviews()
        {
            return Ok(interviews);
        }

        [HttpPost]
        public ActionResult<Interview> CreateInterview(Interview interview)
        {
            interview.Id = interviews.Count + 1; // Logica semplice per generare un ID
            interviews.Add(interview);
            return CreatedAtAction(nameof(GetInterviews), new { id = interview.Id }, interview);
        }
    }
}

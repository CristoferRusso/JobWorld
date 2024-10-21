using JobWorld.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using JobWorld.Models;

namespace JobWorld.Controllers
{
  [ApiController]
  [Route("api/job-offer/")]
  public class JobOfferController : ControllerBase
  {
    private readonly JobOfferService _jobOfferService;

    public JobOfferController(JobOfferService jobOfferService)
    {
      _jobOfferService = jobOfferService;
    }

     [HttpGet("offers")]
    public async Task<IActionResult> GetJobOffers(string keywords, string location)
    {
        var result = await _jobOfferService.GetJobOffersAsync(keywords, location);
        return Ok(result); // Restituisce la risposta JSON
    }
  }
}

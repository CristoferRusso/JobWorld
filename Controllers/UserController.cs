// Controllers/UserController.cs
using Microsoft.AspNetCore.Mvc;
using JobWorld.Models;
using System.Collections.Generic;

namespace JobWorld.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        // Simulazione di un database
        private static List<User> users = new List<User>();

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(users);
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            user.Id = users.Count + 1; // Logica semplice per generare un ID
            users.Add(user);
            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }
    }
}

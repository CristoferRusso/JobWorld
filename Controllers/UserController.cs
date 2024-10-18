using Microsoft.AspNetCore.Mvc;
using JobWorld.Models;
using System.Collections.Generic;
using System.Linq;

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

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            user.Id = users.Count + 1; // Logica semplice per generare un ID
            users.Add(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(int id, User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = updatedUser.Name; // Aggiorna le proprietà necessarie
            user.Email = updatedUser.Email; // Esempio di aggiornamento dell'email
            // Aggiungi qui altre proprietà da aggiornare

            return NoContent(); // Restituisce 204 No Content
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            users.Remove(user);
            return NoContent(); // Restituisce 204 No Content
        }

        [HttpPost("login")]
        public ActionResult<User> Login(User loginUser)
        {
            var user = users.FirstOrDefault(u => u.Email == loginUser.Email && u.PasswordHash == loginUser.PasswordHash);
            if (user == null)
            {
                return Unauthorized(); // Restituisce 401 Unauthorized
            }
            return Ok(user);
        }

    }
}

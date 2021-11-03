using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PeliRestApi.Models;

namespace PeliRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        List<User> users = new List<User>();

        public User perus = new User();
        public User admin = new User();


        // Metodi kirjautumista varten
        [HttpPost]
        public ActionResult Login([FromBody] User user)
        {
            perus.UserId = 1;
            perus.Username = "Perus";
            perus.Password = "Perus123";
            perus.Email = "pertti.perus@pelaaja.fi";
            perus.Admin = false;
            perus.Token = "ayi23rtyP987y";

            admin.UserId = 2;
            admin.Username = "Admin";
            admin.Password = "Admin123";
            admin.Email = "administrator@pelaaja.fi";
            admin.Admin = true;
            admin.Token = "Rtayi23tyP987ghX1";

            users.Add(perus);
            users.Add(admin);

            var match = users.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();

            if (match != null)
            {
                match.Password = null;
                return Ok(match);
            }
            else
            {
                return BadRequest("Väärä käyttäjätunnus tai salasana");
            }
        }

    }
}

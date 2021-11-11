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
    public class GenretController : ControllerBase
    {

        // Viitataan tietokantayhteyteen joka on määritetty models kansiossa
        private static readonly pelidbContext db = new pelidbContext();


        // Get metodi joka palauttaa genret tietokannasta
        [HttpGet]
        public ActionResult GetAllGenres()
        {
            var genret = db.Genrets.ToList();

            return Ok(genret);
        }


        // Haku nimellä
        [HttpGet]
        [Route("{key}")]
        public ActionResult GetGenresByName(string key)
        {
            var genret = db.Genrets.Where(g => g.Nimi.ToLower().Contains(key.ToLower()));

            return Ok(genret);
        }



        // Post metodi jonka avulla lisätään uusi genre
        [HttpPost]
        public ActionResult CreateNew([FromBody] Genret genre)
        {
            try
            {
                db.Genrets.Add(genre);
                db.SaveChanges();

                return Ok("Lisättiin uusi genre nimellä: " + genre.Nimi);
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Tässä lisätietoa: " + e);
            }
        }


        // Poista genre
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteGenre(int id)
        {
            Genret genre = db.Genrets.Find(id);

            if (genre != null) // Jos genre löytyy
            {
                db.Genrets.Remove(genre);
                db.SaveChanges();
                return Ok("Genre '" + genre.Nimi + "' poistettu.");
            }
            else
            {
                return NotFound("Genreä ei löydy id:llä " + id);
            }

        }


        // Olemassaolevan pelin muokkaaminen
        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateGenre(int id, [FromBody] Genret genre)
        {
            try
            {
                Genret entinen = db.Genrets.Find(id);
                if (entinen != null) // Jos id:llä löytyy genre tietokannasta
                {
                    entinen.Nimi = genre.Nimi;
                    entinen.Kuvaus = genre.Kuvaus;
              
                    db.SaveChanges();
                    return Ok("Muokattiin genreä: " + genre.Nimi);
                }
                else
                {
                    return NotFound("Genreä ei löydy id:llä " + id);
                }
            }
            catch (Exception e)
            {
                return BadRequest("Genren tietojen päivittäminen ei onnistunut: " + e);
            }

        }


    }
}

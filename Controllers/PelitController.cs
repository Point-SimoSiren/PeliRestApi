using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeliRestApi.Models;
using System;
using System.Linq;

namespace PeliRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PelitController : ControllerBase
    {
        // Viitataan tietokantayhteyteen joka on määritetty models kansiossa
        private static readonly pelidbContext db = new pelidbContext();


        // Get metodi joka palauttaa pelidatan tietokannasta
        [HttpGet]
        public ActionResult GetAllGames()
        {
            var pelit = db.Pelits.ToList();

            return Ok(pelit);
        }


        // Haku pelin nimellä
        [HttpGet]
        [Route("{key}")]
        public ActionResult GetGamesByName(string key)
        {
            var pelit = db.Pelits.Where(p => p.Nimi.ToLower().Contains(key.ToLower()));

            return Ok(pelit);
        }


        // Haku genren mukaan genre id:llä
        // https://localhost:5001/api/pelit/genreid/5   <---- esim jos haettaisiin genre id 5:lla
        [HttpGet]
        [Route("genreid/{key}")]
        public ActionResult GetGamesByGenreId(int key)
        {
            var pelit = db.Pelits.Where(p => p.GenreId == key);

            return Ok(pelit);
        }

        // Haku alkaen ja päättyen julkaisuvuoden mukaan
        // https://localhost:5001/api/pelit/year-from/2018/year-to/2020   <---- esim hausta
        [HttpGet]
        [Route("year-from/{from}/year-to/{to}")]
        public ActionResult GetGamesByYear(int from, int to)
        {
            var pelit = db.Pelits.Where(p => p.Julkaisuvuosi >= from && p.Julkaisuvuosi <= to);

            return Ok(pelit);
        }


        // Haku n-määrä eniten ladattua
        // https://localhost:5001/api/pelit/ladatuimmat/2   <---- esim hausta 2 ladatuinta
        [HttpGet]
        [Route("ladatuimmat/{key}")]
        public ActionResult GetMostLoadedGames(int key)
        {
            var pelit = db.Pelits.ToList();

            var ladatuimmat = pelit.OrderByDescending(p => p.Lataukset).Take(key);

            return Ok(pelit);
        }



        // Post metodi jonka avulla lisätään uusi peli
        [HttpPost]
        public ActionResult CreateNew([FromBody] Pelit peli)
        {
            try
            {
                db.Pelits.Add(peli);
                db.SaveChanges();

                return Ok("Lisättiin peli " + peli.Nimi);
            }
            catch(Exception e)
            {
                return BadRequest("Tapahtui virhe. Tässä lisätietoa: " + e);
            }
        }


        // Poista peli
        [HttpDelete]
        [Route("{id}/{token}")]
        public ActionResult DeleteGame(int id, string token)
        {
            if (token != "Rtayi23tyP987ghX1")
            {
                return Unauthorized("Token is invalid or missing");
            }


            Pelit peli = db.Pelits.Find(id);

            if (peli != null) // Jos peli löytyy
            {
                db.Pelits.Remove(peli);
                db.SaveChanges();
                return Ok("Peli '" + peli.Nimi + "' poistettu.");
            }
            else {
                return NotFound("Peliä ei löydy id:llä " + id);
            }

        }

        
        // Olemassaolevan pelin muokkaaminen
        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateGame(int id, [FromBody] Pelit peli)
        {
            try
            {
                Pelit entinen = db.Pelits.Find(id);
                if (entinen != null) // Jos id:llä löytyy peli tietokannasta
                {
                    entinen.Nimi = peli.Nimi;
                    entinen.GenreId = peli.GenreId;
                    entinen.Lataukset = peli.Lataukset;
                    entinen.Julkaisuvuosi = peli.Julkaisuvuosi;
                   
                    db.SaveChanges();
                    return Ok("Muokattiin peliä: " + peli.Nimi);
                }
                else
                {
                    return NotFound("Peliä ei löydy id:llä " + id);
                }
            }
            catch (Exception e)
            {
                return BadRequest("Pelin tietojen päivittäminen ei onnistunut: " + e);
            }

        }
        

    }
}

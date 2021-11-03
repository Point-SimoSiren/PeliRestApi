using System;
using System.Collections.Generic;

#nullable disable

namespace PeliRestApi.Models
{
    public partial class Pelit
    {
        public int PeliId { get; set; }
        public string Nimi { get; set; }
        public int GenreId { get; set; }
        public int Julkaisuvuosi { get; set; }
        public int Lataukset { get; set; }

        public virtual Genret Genre { get; set; }
    }
}

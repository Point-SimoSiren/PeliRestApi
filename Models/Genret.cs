using System;
using System.Collections.Generic;

#nullable disable

namespace PeliRestApi.Models
{
    public partial class Genret
    {
        public Genret()
        {
            Pelits = new HashSet<Pelit>();
        }

        public int GenreId { get; set; }
        public string Nimi { get; set; }
        public string Kuvaus { get; set; }

        public virtual ICollection<Pelit> Pelits { get; set; }
    }
}

using System;
using System.Collections.Generic;

#nullable disable

namespace auctionDL
{
    public partial class Artist
    {
        public Artist()
        {
            Artistcollections = new HashSet<Artistcollection>();
            Arts = new HashSet<Art>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Signature { get; set; }
        public string Artiststatement { get; set; }
        public string Biography { get; set; }
        public byte[] Digitalsignature { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Artistcollection> Artistcollections { get; set; }
        public virtual ICollection<Art> Arts { get; set; }
    }
}

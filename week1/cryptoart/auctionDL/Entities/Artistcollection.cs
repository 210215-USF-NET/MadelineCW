using System;
using System.Collections.Generic;

#nullable disable

namespace auctionDL
{
    public partial class Artistcollection
    {
        public int Artistid { get; set; }
        public int Artid { get; set; }

        public virtual Art Art { get; set; }
        public virtual Artist Artist { get; set; }
    }
}

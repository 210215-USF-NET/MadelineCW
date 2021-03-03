using System;
using System.Collections.Generic;
using mod = auctionModels;


namespace auctionDL
{
    public interface ISellerMapper
    {
        mod.Seller Parse(Seller seller);
        Seller Parse(mod.Seller seller);


    }
}

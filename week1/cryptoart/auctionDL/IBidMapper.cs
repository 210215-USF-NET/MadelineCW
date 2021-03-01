using mod = auctionModels;


namespace auctionDL
{
    interface IBidMapper
    {
        mod.Bid Parse(Bid bid);
       Bid Parse(mod.Bid bid);


    }
}

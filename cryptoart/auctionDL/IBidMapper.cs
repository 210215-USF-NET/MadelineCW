using mod = auctionModels;


namespace auctionDL
{
    public interface IBidMapper
    {
        mod.Bid Parse(Bid bid);
       Bid Parse(mod.Bid bid);


    }
}

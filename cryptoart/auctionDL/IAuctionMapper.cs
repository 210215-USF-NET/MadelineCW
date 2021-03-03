using mod = auctionModels;


namespace auctionDL
{
    public interface IAuctionMapper
    {
        mod.Auction Parse(Auction auction);
        Auction Parse(mod.Auction auction);


    }
}

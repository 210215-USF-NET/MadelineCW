using System;


namespace auctionDL
{
    public class Art
    {
        int id {get; set;}
        string name {get; set;}
        string description {get; set;}
        string ArtistStatement {get;set;}
        object[] blacklist {get; set;}
        object [] provenence{get; set;}
        object thumbnail{get; set;}
        object fullart{get; set;}
        string[] keywords{get;set;}
        int SeriesNumber{get;set;}
        int MaxSeries{get; set;}
        double BuyNoWPrice{get; set;}
        double CurrentValue{get; set;}
    }
}

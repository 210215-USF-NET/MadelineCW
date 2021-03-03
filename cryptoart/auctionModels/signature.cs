using System;


namespace auctionModels
{
    public class Signature
    {
        string name {get; set;}
        Byte[] cryptoKey {get; set;}
        object visualSignature {get; set;}
    }
}

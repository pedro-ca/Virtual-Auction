

namespace AuctionModel
{
    public class AuctionParticipant
    {
        public string UserName { get; set; }
        public string Ip { get; set; }

        public AuctionParticipant(string userName, string ip)
        {
            this.UserName = userName;
            this.Ip = ip;
        }
    }
}

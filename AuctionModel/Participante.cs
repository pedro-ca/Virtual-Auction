

namespace AuctionModel
{
    public class Participante
    {
        public string NomeUsuario { get; set; }
        public string Ip { get; set; }

        public Participante(string nomeUsuario, string ip)
        {
            this.NomeUsuario = nomeUsuario;
            this.Ip = ip;
        }
    }
}

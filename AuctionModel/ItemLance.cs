

namespace AuctionModel
{
    public class ItemLance
    {
        public string NomeItem { get; private set; }
        public float ValorInicial { get; private set; }
        public float ValorAdicionalMinimo { get; private set; }
        public float ValorAtual { get; set; }
        public string DonoAtual { get; set; }
        public int TempoRestante { get; set; }
        public bool EstaDisponivel { get; set; }

        public ItemLance(string nomeItem, float valorInicial, float valorAdicionalMinimo, float valorAtual, string donoAtual, int tempoRestante, bool estaDisponivel)
        {
            this.NomeItem = nomeItem;
            this.ValorInicial = valorInicial;
            this.ValorAdicionalMinimo = valorAdicionalMinimo;
            this.ValorAtual = valorAtual;
            this.DonoAtual = donoAtual;
            this.TempoRestante = tempoRestante;
            this.EstaDisponivel = estaDisponivel;
        }
    }
}

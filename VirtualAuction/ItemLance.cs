using System;
using System.Collections.Generic;
using System.Text;

namespace LeilaoServer
{
    public class CustomObject
    {
        public string NameFoo { get; set; }
        public float NumberFoo { get; set; }
        public bool conditionFoo { get; set; }
    }


    public class ItemLance
    {
        public string NomeItem { get; private set; }
        public float ValorInicial { get; private set; }
        public float ValorAdicionalMinimo { get; private set; }
        public float ValorAtual { get; set; }
        public string DonoAtual { get; set; }
        public int TempoRestante { get; set; }
        public bool EstaDisponivel { get; set; }

        public ItemLance(string nomeItem, float valorInicial, float valorAdicionalMinimonceMinimo, int tempoRestante)
        {
            this.NomeItem = nomeItem;
            this.ValorInicial = valorInicial;
            this.ValorAdicionalMinimo = valorAdicionalMinimonceMinimo;
            this.ValorAtual = valorInicial;
            this.DonoAtual = "Leiloeiro";
            this.TempoRestante = tempoRestante;
            this.EstaDisponivel = true;
        }
    }
}

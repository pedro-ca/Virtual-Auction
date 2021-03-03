using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeilaoServer
{
    public class Participante
    {
        public string NomeUsuario { get; set; }
        public string Ip { get; set; }
        public string CertifcadoDigital { get; set; }

        public Participante(string nomeUsuario, string ip, string certifcadoDigital)
        {
            this.NomeUsuario = nomeUsuario;
            this.Ip = ip;
            this.CertifcadoDigital = certifcadoDigital;
        }
    }
}

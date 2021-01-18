using System;
using System.Collections.Generic;
using System.IO;

namespace EPlayers_AspNetCore.Models
{
    public class Jogador
    {
        public int IdJogador { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int IdEquipe { get; set; }

        
    }
}
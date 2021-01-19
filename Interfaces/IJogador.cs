using System.Collections.Generic;
using EPlayers_AspNetCore.Models;

namespace EPlayers_AspNetCore.Interfaces
{
    public interface IJogador
    {
         void Create(Jogador j);

         List<Jogador> ReadAll();

         void Update(Jogador j);

         void Delete(int id);
    }
}
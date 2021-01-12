using System.Collections.Generic;
using System.IO;
using EPlayers_AspNetCore.Interfaces;

namespace EPlayers_AspNetCore.Models
{
    public class Equipe : EplayersBase , IEquipe
    {
        public int IdEquipe { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }

        private const string PATH = "Database/Equipe.csv";

        //Método costrutor que cria arquivos e pastas caso não existam
        public Equipe()
        {
            CreateFolderAndFile(PATH);
        }
        public string Prepare(Equipe e ){
            return $"{e.IdEquipe};{e.Nome};{e.Imagem}";
        }

        //Adiciona uma equipe ao CSV
        public void Create(Equipe e)
        {
            //Preparamos um array de string para o método AppendAllLines
            string[] linhas = { Prepare(e) };
            //Acrescentamos uma nova linha
            File.AppendAllLines(PATH, linhas);
        }

        public void Delete(int id)
        {
             List<string> linhas = ReadAllLinesCSV(PATH);
            //Removemos as linhas com o códigocomparado
            //ToString -> converte para texto(string)
            linhas.RemoveAll(x => x.Split(";")[0] == id.ToString());
          

            //Reescrevemos o CSV com a lista alterado
            RewriteCSV(PATH, linhas);
        }

        public List<Equipe> ReadAll()
        {
            List<Equipe> equipes = new List<Equipe>();
            //Lemos todas as linhas do csv
            string[] linhas = File.ReadAllLines(PATH);

            foreach (string item in linhas)
            {
                string[] linha = item.Split(";");

                Equipe novaEquipe = new Equipe();
                novaEquipe.IdEquipe = int.Parse( linha[0] );
                novaEquipe.Nome = linha[1];
                novaEquipe.Imagem = linha[2];

                equipes.Add(novaEquipe);
            }

            return equipes;
        }

        public void Update(Equipe e)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            //Removemos as linhas com o códigocomparado
            linhas.RemoveAll(x => x.Split(";")[0] == e.IdEquipe.ToString());
            //Adicionamos na lista a equipe alterada
            linhas.Add( Prepare(e) );

            //Reescrevemos o CSV com a lista alterado
            RewriteCSV(PATH, linhas);
        }
    }
}
using System;
using System.IO;
using EPlayers_AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPlayers_AspNetCore.Controllers
{
    [Route("Equipe")]
    public class EquipeController : Controller
    {
        //Criamos a instancia de equipeModel
        Equipe equipeModel = new Equipe();

        [Route("Listar")]
        public IActionResult Index()
        {
            //Listamos todas as equipes e enviamos para a View, através da ViewBag
            ViewBag.Equipes = equipeModel.ReadAll();

            return View();
        }

        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            Equipe novaEquipe   = new Equipe();
            novaEquipe.IdEquipe = Int32.Parse( form["IdEquipe"] );
            novaEquipe.Nome     = form["Nome"];

            //Upload Início
            
            //Verificamos se o usuário selecionou um arquivo
            if (form.Files.Count > 0)
            {
                //Recebemos o arquivo que o usuário enviou e armazenamos em uma variavel file
                var file   = form.Files[0];
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                //Verificamos se o diretório existe, e caso não, a criamos
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", folder, file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                novaEquipe.Imagem = file.FileName;
            }
            else
            {
                novaEquipe.Imagem = "padrao.jpg";
            }

            //Chamamos o método Create para salvar a novaEquipe no CSV
            equipeModel.Create(novaEquipe);
            //Atualizamos a lista de equipes na View
            ViewBag.Equipes = equipeModel.ReadAll();

            return LocalRedirect("~/Equipe/Listar");
        }

        [Route("{id}")]
        public IActionResult Excluir( int id )
        {
            equipeModel.Delete(id);
            ViewBag.Equipes = equipeModel.ReadAll();

            return LocalRedirect("~/Equipe/Listar");
        }
           
    }
}
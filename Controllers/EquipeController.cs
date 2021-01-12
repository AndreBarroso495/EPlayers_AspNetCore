using System;
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

        public IActionResult Index()
        {
            //Listamos todas as equipes e enviamos para a View, através da ViewBag
            ViewBag.Equipes = equipeModel.ReadAll();

            return View();
        }

        public IActionResult Cadastrar(IFormCollection form)
        {
            Equipe novaEquipe   = new Equipe();
            novaEquipe.IdEquipe = Int32.Parse( form["IdEquipe"] );
            novaEquipe.Nome     = form["Nome"];
            novaEquipe.Imagem   = form["Imagem"];

            //Chamamos o método Create para salvar a novaEquipe no CSV
            equipeModel.Create(novaEquipe);
            //Atualizamos a lista de equipes na View
            ViewBag.Equipes = equipeModel.ReadAll();

            return LocalRedirect("~/Equipe");
        }

           
    }
}
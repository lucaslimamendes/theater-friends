﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using theaterFriends.Models;

namespace theaterFriends.Controllers
{
    public class MoviesController : PadraoController<MoviesViewModel>
    {
        public MoviesController()
        {
            //DAO = new MoviesDAO();
        }

        /*private void PreencheComboBoxCidade()
        {
            var daoCidade = new CidadeDAO();

            ViewBag.cidades = new List<SelectListItem>();
            ViewBag.cidades.Add(new SelectListItem("Selecione uma cidade...", "0"));

            foreach (CidadeViewModel cid in daoCidade.Listagem())
            {
                var elemento = new SelectListItem(cid.Nome, cid.Id.ToString());
                ViewBag.cidades.Add(elemento);
            }
        }*/

        protected override void ValidaDados(MoviesViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);
            if (string.IsNullOrEmpty(model.Name))
                ModelState.AddModelError("Name", "Nome inválido!");

            if (string.IsNullOrEmpty(model.Description))
                ModelState.AddModelError("Description", "Descrição inválida!");

            if (string.IsNullOrEmpty(model.Type))
                ModelState.AddModelError("Type", "Tipo inválida!");

            if (model.Length <= 0)
            {
                ModelState.Remove("Length");
                ModelState.AddModelError("Lenght", "Duração Invalida");
            } 

            if (model.Min_age < 0 || model.Min_age > 18)
            {
                ModelState.Remove("Min_Age");
                ModelState.AddModelError("Min_age", "Idade minima Invalida");
            }

            if (string.IsNullOrEmpty(model.Language))
                ModelState.AddModelError("Language", "Linguagem inválida!");
        }
    }
}
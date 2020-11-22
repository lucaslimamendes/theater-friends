﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using theaterFriends.DAO;
using theaterFriends.Models;

namespace theaterFriends.Controllers
{
    public class EmployerController : PadraoController<EmployerViewModel>
    {
        public EmployerController()
        {
            DAO = new EmployerDAO();
        }

        protected override void PreencheDadosParaView(string Operacao, EmployerViewModel model)
        {
            base.PreencheDadosParaView(Operacao, model);
        }

        protected override void ValidaDados(EmployerViewModel model, string operacao)
        {
            if (operacao != "I")
                base.ValidaDados(model, operacao);

            if (string.IsNullOrEmpty(model.Name))
                ModelState.AddModelError("Name", "Nome inválido!");

            if (string.IsNullOrEmpty(model.Email))
                ModelState.AddModelError("Email", "Email inválido!");

            if (string.IsNullOrEmpty(model.Password) || model.Password.Length < 7)
                ModelState.AddModelError("Password", "Senha inválido (tamanho mínimo de 8 caracteres!");

            if (string.IsNullOrEmpty(model.ConfirmPassword) || model.ConfirmPassword.Length < 7)
                ModelState.AddModelError("ConfirmPassword", "Senha inválido (tamanho mínimo de 8 caracteres!");

            if (model.Password != model.ConfirmPassword)
                ModelState.AddModelError("Password", "As senhas não batem!");

            if (string.IsNullOrEmpty(model.Employer_role))
                ModelState.AddModelError("Employer_role", "Cargo Inválido!");

            if (ModelState["Hired_At"].ValidationState == ModelValidationState.Invalid || model.Hired_At > DateTime.Now)
            {
                ModelState.Remove("Hired_At");
                ModelState.AddModelError("Hired_At", "Data de contratação não pode ser superior à data atual!");
            }

        }

        public override IActionResult Salvar(EmployerViewModel model, string Operacao)
        {
            try
            {
                ValidaDados(model, Operacao);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    PreencheDadosParaView(Operacao, model);
                    return View(ViewParaCadastro, model);
                }
                else
                {
                    if (Operacao == "I")
                        DAO.Insert(model);
                    else
                        DAO.Update(model);
                    return RedirectToAction("Index", "Administracao");
                }
            }
            catch (Exception erro)
            {
                ViewBag.Erro = "Ocorreu um erro: " + erro.Message;
                ViewBag.Operacao = Operacao;
                PreencheDadosParaView(Operacao, model);
                return View(ViewParaCadastro, model);
            }
        }
    }
}

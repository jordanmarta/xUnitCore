﻿using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CursoOnline.Web.Models;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Web.Util;
using CursoOnline.Dominio._Base;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace CursoOnline.Web.Controllers
{
    public class CursoController : Controller
    {
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly IRepositorio<Curso> _cursoRepositorio;

        public CursoController(ArmazenadorDeCurso armazenadorDeCurso, IRepositorio<Curso> cursoRepositorio)
        {
            _armazenadorDeCurso = armazenadorDeCurso;
            _cursoRepositorio = cursoRepositorio;
        }
        public IActionResult Index()
        {
            var cursos = _cursoRepositorio.Consultar();

            if(cursos.Any())
            {
                var dtos = cursos.Select(c => new CursoParaListagemDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    CargaHoraria = c.CargaHoraria,
                    PublicoAlvo = c.PublicoAlvo.ToString(),
                    Valor = c.Valor
                });

                return View("Index", PaginatedList<CursoParaListagemDto>.Create(dtos, Request));
            }

            return View("Index", PaginatedList<CursoParaListagemDto>.Create(null, Request));
        }

        public IActionResult Novo()
        {
            return View("NovoOuEditar", new CursoDto());
        }

        public IActionResult Salvar(CursoDto model)
        {
            _armazenadorDeCurso.Armazenar(model);
            return Ok();
        }
    }
}

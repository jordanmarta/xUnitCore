using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CursoOnline.Web.Models;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Web.Util;
using CursoOnline.Dominio._Base;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using CursoOnline.Dominio.Alunos;

namespace CursoOnline.Web.Controllers
{
    public class AlunoController : Controller
    {
        private readonly ArmazenadorDeAluno _armazenadorDeAluno;
        private readonly IRepositorio<Aluno> _alunoRepositorio;

        public AlunoController(ArmazenadorDeAluno armazenadorDeAluno, IRepositorio<Aluno> alunoRepositorio)
        {
            _armazenadorDeAluno = armazenadorDeAluno;
            _alunoRepositorio = alunoRepositorio;
        }
        public IActionResult Index()
        {
            var alunos = _alunoRepositorio.Consultar();

            if(alunos.Any())
            {
                var dtos = alunos.Select(c => new AlunoParaListagemDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Cpf = c.Cpf,
                    Email = c.Email
                });

                return View("Index", PaginatedList<AlunoParaListagemDto>.Create(dtos, Request));
            }

            return View("Index", PaginatedList<AlunoParaListagemDto>.Create(null, Request));
        }

        public IActionResult Editar(int id)
        {
            var aluno = _alunoRepositorio.ObterPorId(id);
            var dto = new AlunoDto
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Cpf = aluno.Cpf,
                Email = aluno.Email,
                PublicoAlvo = aluno.PublicoAlvo.ToString()
            };

            return View("NovoOuEditar", dto);
        }

        public IActionResult Novo()
        {
            return View("NovoOuEditar", new AlunoDto());
        }

        public IActionResult Salvar(AlunoDto model)
        {
            _armazenadorDeAluno.Armazenar(model);
            return Ok();
        }
    }
}

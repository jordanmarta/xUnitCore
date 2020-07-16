using CursoOnline.Dados.Contextos;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CursoOnline.Dados.Repositorios
{
    public class AlunoRepositorio : RepositorioBase<Aluno>, IAlunoRepositorio
    {
        public AlunoRepositorio(ApplicationDbContext context) : base(context)
        {

        }

        public Aluno ObterPeloCpf(string cpf)
        {
            var entidade = Context.Set<Aluno>().Where(c => c.Cpf.Contains(cpf));
            if (entidade.Any())
                return entidade.First();
            return null;
        }
    }
}

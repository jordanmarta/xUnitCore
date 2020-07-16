using CursoOnline.Dominio._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.Alunos
{
    public interface IAlunoRepositorio : IRepositorio<Aluno>
    {
        Aluno ObterPeloCpf(string cpf);
    }
}

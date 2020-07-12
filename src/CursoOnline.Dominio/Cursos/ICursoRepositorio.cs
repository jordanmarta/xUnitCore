using CursoOnline.Dominio._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.Cursos
{
    public interface ICursoRepositorio : IRepositorio<Curso>
    {
        void Adicionar(Curso curso);
        Curso ObterPeloNome(string nome);
    }
}

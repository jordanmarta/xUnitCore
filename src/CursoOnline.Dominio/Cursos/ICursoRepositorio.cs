using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Cursos
{
    public interface ICursoRepositorio : IRepositorio<Curso>
    {
        void Adicionar(Curso curso);
        Curso ObterPeloNome(string nome);
    }
}

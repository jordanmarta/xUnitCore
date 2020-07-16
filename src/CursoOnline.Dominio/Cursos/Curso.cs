using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.PublicosAlvo;

namespace CursoOnline.Dominio.Cursos
{
    public class Curso : Entidade
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double CargaHoraria { get; private set; }
        public EPublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }

        private Curso() { }

        public Curso(string nome, string descricao, double cargaHoraria, EPublicoAlvo publicoAlvo, double valor)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), Resource.NOME_INVALIDO)
                .Quando(cargaHoraria < 1, Resource.CARGA_HORARIA_INVALIDA)
                .Quando(valor < 1, Resource.VALOR_INVALIDO)
                .DispararExcecaoSeExistir();

            this.Nome = nome;
            this.Descricao = descricao;
            this.CargaHoraria = cargaHoraria;
            this.PublicoAlvo = publicoAlvo;
            this.Valor = valor;
        }

        public void AlterarNome(string nome)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), Resource.NOME_INVALIDO)
                .DispararExcecaoSeExistir();

            this.Nome = nome;
        }

        public void AlterarCargaHoraria(double cargaHoraria)
        {
            ValidadorDeRegra.Novo()
                .Quando(cargaHoraria < 1, Resource.CARGA_HORARIA_INVALIDA)
                .DispararExcecaoSeExistir();

            this.CargaHoraria = cargaHoraria;
        }

        public void AlterarValor(double valor)
        {
            ValidadorDeRegra.Novo()
                .Quando(valor < 1, Resource.VALOR_INVALIDO)
                .DispararExcecaoSeExistir();

            this.Valor = valor;
        }
    }

}

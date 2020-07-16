using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.PublicosAlvo;
using System;

namespace CursoOnline.DominioTest._Builders
{
    public class CursoBuilder
    {
        private string _nome = "Informática básica";
        private string _descricao = "Uma descrição";
        private double _cargaHoraria = 40;
        private EPublicoAlvo _publicoAlvo = EPublicoAlvo.Estudante;
        private double _valor = 80;
        private int _id;

        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public CursoBuilder ComCargaHoraria(double cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }

        public CursoBuilder ComValor(double valor)
        {
            _valor = valor;
            return this;
        }

        public CursoBuilder ComPublicoAlvo(EPublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo ;
            return this;
        }

        public CursoBuilder ComId(int id)
        {
            _id = id;
            return this;
        }

        public Curso Build()
        {
            var curso = new Curso(_nome, _descricao, _cargaHoraria, _publicoAlvo, _valor);

            if(_id > 0)
            {
                // Setando o Id com reflection...
                var propertyInfo = curso.GetType().GetProperty("Id");
                propertyInfo.SetValue(curso, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
            }

            return curso;
        }

    }
}

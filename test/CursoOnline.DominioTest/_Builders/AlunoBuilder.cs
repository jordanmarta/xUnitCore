using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.DominioTest.Cursos;
using System;

namespace CursoOnline.DominioTest._Builders
{
    public class AlunoBuilder
    {
        protected int Id;
        protected string Nome;
        protected string Email;
        protected string Cpf;
        protected EPublicoAlvo PublicoAlvo;

        public static AlunoBuilder Novo()
        {
            var faker = new Faker();

            return new AlunoBuilder
            {
                Nome = faker.Person.FullName,
                Email = faker.Person.Email,
                Cpf = faker.Person.Cpf(),
                PublicoAlvo = EPublicoAlvo.Empregado
            };
        }

        public AlunoBuilder ComNome(string nome)
        {
            Nome = nome;
            return this;
        }

        public AlunoBuilder ComEmail(string email)
        {
            Email = email;
            return this;
        }

        public AlunoBuilder ComCpf(string cpf)
        {
            Cpf = cpf;
            return this;
        }

        public AlunoBuilder ComId(int id)
        {
            Id = id;
            return this;
        }

        public AlunoBuilder ComPublicoAlvo(EPublicoAlvo publicoAlvo)
        {
            PublicoAlvo = publicoAlvo;
            return this;
        }

        public Aluno Build()
        {
            var aluno = new Aluno(Nome, Email, Cpf, PublicoAlvo);

            if (Id < 0) return aluno;

            // Setando o Id com reflection...
            var propertyInfo = aluno.GetType().GetProperty("Id");
            propertyInfo.SetValue(aluno, Convert.ChangeType(Id, propertyInfo.PropertyType), null);

            return aluno ;
        }

    }
}

using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CursoOnline.DominioTest.Alunos
{
    public class AlunoTest
    {
        private readonly Faker _faker;

        public AlunoTest()
        {
            _faker = new Faker();
        }

        [Fact]
        public void DeveCriarAluno()
        {
            var alunoEsperado = new
            {
                Nome = _faker.Person.FullName,
                _faker.Person.Email,
                Cpf = _faker.Person.Cpf(),
                PublicoAlvo = EPublicoAlvo.Empreendedor
            };

            var aluno = new Aluno(alunoEsperado.Nome, alunoEsperado.Email, alunoEsperado.Cpf, alunoEsperado.PublicoAlvo);

            alunoEsperado.ToExpectedObject().ShouldMatch(aluno);
        }

        [Fact]
        public void DeveAlterarNome()
        {
            var novoNomeEsperado = _faker.Person.FullName;
            var aluno = AlunoBuilder.Novo().Build();

            aluno.AlterarNome(novoNomeEsperado);

            Assert.Equal(novoNomeEsperado, aluno.Nome);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCriarComNovoInvalido(string nomeInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                AlunoBuilder.Novo().ComNome(nomeInvalido).Build())
                .ComMensagem(Resource.NOME_INVALIDO);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("email invalido")]
        [InlineData("email@invalido")]
        public void NaoDeveCriarComEmailInvalido(string emailInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                AlunoBuilder.Novo().ComEmail(emailInvalido).Build())
                .ComMensagem(Resource.EMAIL_INVALIDO);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("cpf invalido")]
        [InlineData("00000000000")]
        public void NaoDeveCriarComCpfInvalido(string cpfInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                AlunoBuilder.Novo().ComCpf(cpfInvalido).Build())
                .ComMensagem(Resource.CPF_INVALIDO);
        }
    }
}

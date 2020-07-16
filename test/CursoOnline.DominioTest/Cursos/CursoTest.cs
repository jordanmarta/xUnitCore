 using Bogus;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.DominioTest.Cursos
{
    public class CursoTest : IDisposable
    {
        private readonly string _nome;
        private readonly string _descricao;
        private readonly double _cargaHoraria;
        private readonly EPublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly Faker _faker;

        public ITestOutputHelper _output { get; }

        public CursoTest(ITestOutputHelper outputHelper)
        {
            _faker = new Faker();

            _output = outputHelper;
            _output.WriteLine("Construtor sendo executado");

            _nome = _faker.Random.Word();
            _descricao = _faker.Lorem.Paragraph();
            _cargaHoraria = _faker.Random.Double(50, 1000);
            _publicoAlvo = EPublicoAlvo.Estudante;
            _valor = _faker.Random.Double(100, 1000);
        }

        public void Dispose()
        {
            _output.WriteLine("Dispose sendo executado");
        }

        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = "Informática",
                CargaHoraria = (double)40,
                PublicoAlvo = EPublicoAlvo.Estudante,
                Valor = (double)80,
                Descricao = _descricao
            };

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, 
                cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                CursoBuilder.Novo().ComNome(nomeInvalido).Build()) 
                .ComMensagem(Resource.NOME_INVALIDO);        
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaCargaInvalida(double cargaHorariaInvalida)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build())
                .ComMensagem(Resource.CARGA_HORARIA_INVALIDA);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmValorInvalido(double valorInvalido)
        {
            Assert.Throws<ExcecaoDeDominio>(() =>
                CursoBuilder.Novo().ComValor(valorInvalido).Build())
                .ComMensagem(Resource.VALOR_INVALIDO);
        }

        [Fact]
        public void DeveAlterarNome()
        {
            var nomeEsperado = _faker.Person.FullName;
            var curso = CursoBuilder.Novo().Build();

            curso.AlterarNome(nomeEsperado);

            Assert.Equal(nomeEsperado, curso.Nome);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarComNomeInvalido(string nomeInvalido)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ExcecaoDeDominio>(() => curso.AlterarNome(nomeInvalido))
                .ComMensagem(Resource.NOME_INVALIDO);
        }

        [Fact]
        public void DeveAlterarCargaHoraria()
        {
            var cargaHorariaEsperada = _faker.Random.Double(80, 100);
            var curso = CursoBuilder.Novo().Build();

            curso.AlterarCargaHoraria(cargaHorariaEsperada);

            Assert.Equal(cargaHorariaEsperada, curso.CargaHoraria);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveAlterarComCargaInvalida(double cargaHorariaInvalida)
        {
            var curso = CursoBuilder.Novo().Build();

            Assert.Throws<ExcecaoDeDominio>(() => curso.AlterarCargaHoraria(cargaHorariaInvalida))
                .ComMensagem(Resource.CARGA_HORARIA_INVALIDA);
        }


        [Fact]
        public void DeveAlterarValor()
        {
            var valorEsperado = _faker.Random.Double(100, 1000);
            var curso = CursoBuilder.Novo().Build();

            curso.AlterarValor(valorEsperado);

            Assert.Equal(valorEsperado, curso.Valor);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveAlterarComValorInvalido(double valorInvalido)
        {
            var curso = CursoBuilder.Novo().Build();
            
            Assert.Throws<ExcecaoDeDominio>(() => curso.AlterarValor(valorInvalido))
                .ComMensagem(Resource.VALOR_INVALIDO);
        }
    }    
}

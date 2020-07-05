using CursoOnline.DominioTest._Util;
using ExpectedObjects;
using System;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    public class CursoTest
    {
        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                nome = "Informática",
                cargaHoraria = (double)40,
                publicoAlvo = EPublicoAlvo.Estudante,
                valor = (double)80
            };

            var curso = new Curso(cursoEsperado.nome, cursoEsperado.cargaHoraria, cursoEsperado.publicoAlvo, cursoEsperado.valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {
            var cursoEsperado = new
            {
                nome = "Informática",
                cargaHoraria = (double)40,
                publicoAlvo = EPublicoAlvo.Estudante,
                valor = (double)80
            };


            Assert.Throws<ArgumentException>(() =>
                new Curso(nomeInvalido, cursoEsperado.cargaHoraria, cursoEsperado.publicoAlvo, cursoEsperado.valor))
                .ComMensagem("Nome inválido");        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaCargaMenorQue1(double cargaHorariaInvalida)
        {
            var cursoEsperado = new
            {
                nome = "Informática",
                cargaHoraria = (double)40,
                publicoAlvo = EPublicoAlvo.Estudante,
                valor = (double)80
            };


            Assert.Throws<ArgumentException>(() =>
                new Curso(cursoEsperado.nome, cargaHorariaInvalida, cursoEsperado.publicoAlvo, cursoEsperado.valor))
                .ComMensagem("Carga horária inválida");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmValorMenorQue1(double valorInvalido)
        {
            var cursoEsperado = new
            {
                nome = "Informática",
                cargaHoraria = (double)40,
                publicoAlvo = EPublicoAlvo.Estudante,
                valor = (double)80
            };

            Assert.Throws<ArgumentException>(() =>
                new Curso(cursoEsperado.nome, cursoEsperado.cargaHoraria, cursoEsperado.publicoAlvo, valorInvalido))
                .ComMensagem("Valor inválido");
        }
    }

    public enum EPublicoAlvo
    {
        Estudante,
        Universitário,
        Emprego,
        Empreendedor
    }

    public class Curso
    {
        public string nome { get; set; }
        public double cargaHoraria { get; set; }
        public EPublicoAlvo publicoAlvo { get; set; }
        public double valor { get; set; }

        public Curso(string nome, double cargaHoraria, EPublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome inválido");

            if (cargaHoraria < 1)
                throw new ArgumentException("Carga horária inválida");

            if (valor < 1)
                throw new ArgumentException("Valor inválido");

            this.nome = nome;
            this.cargaHoraria = cargaHoraria;
            this.publicoAlvo = publicoAlvo;
            this.valor = valor;
        }
    }
}
